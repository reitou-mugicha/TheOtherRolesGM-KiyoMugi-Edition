/*using HarmonyLib;
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
    public class Undertaker : RoleBase<Undertaker>
    {
        public static Color color = Palette.ImpostorRed;

        private bool ing = false;

        public Undertaker()
        {
            RoleType = roleId = RoleType.Undertaker;
        }

        public override void OnMeetingStart()
        {
            ing = false;
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null)
        {
            ing = false;
        }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Undertaker>();
        }
    }
}*/