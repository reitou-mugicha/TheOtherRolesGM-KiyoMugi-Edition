/*
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
    public class SimpleKiller : RoleBase<SimpleKiller>
    {
        public static Color color = Palette.ImpostorRed;

        public static bool canUseVents = false;
        public static bool canSabotage = false;
        public static bool canReport = true;
        //public static float killCooldown { get { return CustomOptionHolder.simpleKillerCooldown.getFloat(); } }

        public SimpleKiller()
        {
            RoleType = roleId = RoleType.SimpleKiller;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) 
        { 
            //if (PlayerControl.LocalPlayer == player)
                //player.SetKillTimerUnchecked(killCooldown);
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<SimpleKiller>();
        }
    }
}
*/