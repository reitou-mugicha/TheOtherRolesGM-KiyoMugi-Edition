using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class CustomImpostor : RoleBase<CustomImpostor>
    {
        public static Color color = Palette.ImpostorRed;

        public static float killCooldown { get { return CustomOptionHolder.customImpostorKillCooldown.getFloat(); } }
        public static bool canUseVents { get { return CustomOptionHolder.customImpostorCanUseVents.getBool(); } }
        public static bool canSabotage { get { return CustomOptionHolder.customImpostorCanSabotage.getBool(); } }
        //public static bool canReport { get { return CustomOptionHolder.customImpostorCanReport.getBool(); } }

        public CustomImpostor()
        {
            RoleType = roleId = RoleType.CustomImpostor;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd()
        {
            if (PlayerControl.LocalPlayer == player)
                player.SetKillTimerUnchecked(killCooldown);
        }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target)
        {
            if (PlayerControl.LocalPlayer == player)
                player.SetKillTimerUnchecked(killCooldown);
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<CustomImpostor>();
        }
    }
}