using Hazel;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Modules;
using TheOtherRoles.Objects;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Trapper : RoleBase<Trapper>
    {
        public static Color color = Palette.ImpostorRed;
        public static CustomButton trapButton;
        public static PlayerControl currentTarget;
        public static float trapCooldown { get { return CustomOptionHolder.trapperTrapCooldown.getFloat(); } }
        public static float trapTime { get { return CustomOptionHolder.trapperTrapTime.getFloat(); } }
        public static bool isTrap;

        public Trapper()
        {
            RoleType = roleId = RoleType.Trapper;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() 
        { 
            if (player.isAlive())
            {
                currentTarget = setTarget();
                setPlayerOutline(currentTarget, Trapper.color);
            }
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            trapButton = new CustomButton(
                () =>
                {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TrapperTrap, Hazel.SendOption.Reliable, -1);
                    writer.Write(currentTarget.PlayerId);
                    writer.Write((byte)trapTime);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.TrapperTrap(currentTarget.PlayerId, (byte)trapTime);
                    trapButton.Timer = trapButton.MaxTimer = trapCooldown;
                },//押したときの挙動
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Trapper) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { trapButton.Timer = trapButton.MaxTimer = trapCooldown; },//会議終了時どうなるか
                getButtonSprite(),//ボタンの絵
                new Vector3(-1.8f, -0.06f, 0),//ボタンの位置
                hm,
                hm.KillButton,
                KeyCode.T
            );
            trapButton.buttonText = ModTranslation.getString("trapperTrapText");//ボタンの文字
        }
        public static void SetButtonCooldowns() 
        { 
            trapButton.Timer = trapButton.MaxTimer = trapCooldown;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            //Resource by TheOtherRoles GM Haoming Edition
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TrapperTrapButton.png", 115f);
            return buttonSprite;
        }

        public static void Clear()
        {
            players = new List<Trapper>();
        }
    }
}