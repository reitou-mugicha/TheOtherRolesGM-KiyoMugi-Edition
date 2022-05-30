using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using Hazel;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class DoubleKiller : RoleBase<DoubleKiller>
    {
        public static Color color = Palette.ImpostorRed;
        public static CustomButton doubleKillerKillButton;
        public static PlayerControl doubleKiller;
        public PlayerControl currentTarget;
        public static float doubleKillerKillCooldown { get { return CustomOptionHolder.doubleKillerKillButtonCooldown.getFloat(); } }

        public DoubleKiller()
        {
            RoleType = roleId = RoleType.DoubleKiller;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate()
        {
            if (player == PlayerControl.LocalPlayer)
            {
                currentTarget = setTarget();
                setPlayerOutline(currentTarget, DoubleKiller.color);
            }
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            doubleKillerKillButton = new CustomButton(
                () =>
                {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMuderAttempt(PlayerControl.LocalPlayer, local.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.PerformKill)
                    {
                        byte targetId = local.currentTarget.PlayerId;

                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.NormalKill, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(PlayerControl.LocalPlayer.Data.PlayerId);
                        killWriter.Write(targetId);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.NormalKill(PlayerControl.LocalPlayer.Data.PlayerId, targetId);
                    }

                    doubleKillerKillButton.Timer = doubleKillerKillButton.MaxTimer;
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.DoubleKiller) && PlayerControl.LocalPlayer.isAlive(); },
                () => { return local.currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { doubleKillerKillButton.Timer = doubleKillerKillButton.MaxTimer; },
                hm.KillButton.graphic.sprite,
                new Vector3(0f, 2f, 0f),
                hm,
                hm.KillButton,
                KeyCode.Q
            );
        }
        public static void SetButtonCooldowns()
        {
            doubleKillerKillButton.Timer = doubleKillerKillButton.MaxTimer = doubleKillerKillCooldown;
        }

        public static void Clear()
        {
            players = new List<DoubleKiller>();
        }
    }
}