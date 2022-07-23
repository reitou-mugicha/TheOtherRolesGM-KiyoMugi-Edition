using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hazel;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using TheOtherRoles.Modules;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;


namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Silencer : RoleBase<Silencer>
    {
        public static Color color = Palette.ImpostorRed;
        public static CustomButton silenceKillButton;
        public static float silenceCooldown { get { return CustomOptionHolder.silencerSilenceKillCooldown.getFloat(); } }
        public PlayerControl currentTarget;

        public Silencer()
        {
            RoleType = roleId = RoleType.Silencer;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() 
        { 
            if (player == PlayerControl.LocalPlayer)
            {
                currentTarget = setTarget();
                setPlayerOutline(currentTarget, Silencer.color);
            }
        }
        public override void OnKill(PlayerControl target) 
        { 
            PlayerControl.LocalPlayer.SetKillTimerUnchecked(PlayerControl.GameOptions.killCooldown);
            silenceKillButton.Timer = silenceCooldown;
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            silenceKillButton = new CustomButton(
                () =>
                {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMuderAttempt(PlayerControl.LocalPlayer, local.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.SuppressKill) return;

                    if (murderAttemptResult == MurderAttemptResult.PerformKill)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SilencerSilenceKill, Hazel.SendOption.Reliable, -1);
                        writer.Write(local.currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.SilencerSilenceKill(local.currentTarget.PlayerId);

                        silenceKillButton.Timer = silenceKillButton.MaxTimer = silenceCooldown;
                        PlayerControl.LocalPlayer.SetKillTimerUnchecked(PlayerControl.GameOptions.killCooldown);
                    }
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Silencer) && !PlayerControl.LocalPlayer.Data.IsDead; },
<<<<<<< HEAD
                () => { return local.currentTarget && PlayerControl.LocalPlayer.CanMove; },
=======
                () => { return local.currentTarget && !local.currentTarget.isImpostor() &&PlayerControl.LocalPlayer.CanMove; },
>>>>>>> master
                () => { silenceKillButton.Timer = silenceKillButton.MaxTimer = silenceCooldown; },
                getButtonSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.KillButton,
                KeyCode.F
            );
            silenceKillButton.buttonText = ModTranslation.getString("SilenceKillText");//ボタンの文字
        }
        public static void SetButtonCooldowns() 
        { 
            silenceKillButton.Timer = silenceKillButton.MaxTimer = silenceCooldown;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SilentKillButton.png", 115f);
            return buttonSprite;
        }

        public static void Clear()
        {
            players = new List<Silencer>();
        }
    }
}