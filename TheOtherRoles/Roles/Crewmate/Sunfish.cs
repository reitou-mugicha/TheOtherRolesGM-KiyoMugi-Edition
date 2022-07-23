/*using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Sunfish : RoleBase<Sunfish>
    {
        public static Color color = new Color32(211, 211, 211, byte.MaxValue);

        public Sunfish()
        {
            RoleType = roleId = RoleType.Sunfish;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
        }
        public static void SetButtonCooldowns() { }

        public static bool isSunfishCompletedTasks()
        {
            foreach (var sunfish in allPlayers)
            {
                if (sunfish.isAlive())
                {
                    if (sunfish.AllTasksCompleted())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Clear()
        {
            players = new List<Sunfish>();
        }
    }
}*/