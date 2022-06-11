using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Patches;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Bakery : RoleBase<Bakery>
    {
        public static Color color = new Color32(217, 166, 46, byte.MaxValue);
        public static PlayerControl bakery;
        public static bool enableBombBread { get { return CustomOptionHolder.bakeryEnableBombBread.getBool(); } }
        public static int num = BreadPatch.bombBread.Next(1, 101);
        public static int lnum = BreadPatch.luckyBread.Next(1, 100);

        public Bakery()
        {
            RoleType = roleId = RoleType.Bakery;
        }

        public static bool isBakeryAlive()
        {
            foreach(PlayerControl bakery in Bakery.allPlayers)
            {
                if(bakery.isAlive())
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Bakery>();
            bakery = null;
        }
    }
}