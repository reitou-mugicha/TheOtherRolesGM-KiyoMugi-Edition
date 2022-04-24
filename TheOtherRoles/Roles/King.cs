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
    public class King : RoleBase<King>
    {
        public static PlayerControl king;
        public static Color color = Palette.CrewmateBlue;
        public static int numCommonTasks { get { return CustomOptionHolder.kingTasks.commonTasks; } }
        public static int numLongTasks { get { return CustomOptionHolder.kingTasks.longTasks; } }
        public static int numShortTasks { get { return CustomOptionHolder.kingTasks.shortTasks; } }
        public static List<PlayerControl> formerKingdoms = new List<PlayerControl>();
        public static List<byte> exiledKing = new List<byte>();

        public King()
        {
            RoleType = roleId = RoleType.King;
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
            players = new List<King>();
        }

        public static bool isKingAlive()
        {
            bool isAlive = false;
            foreach (var king in Fox.allPlayers)
            {
                if (king.isAlive() && !exiledKing.Contains(king.PlayerId))
                {
                    isAlive = true;
                }
            }
            return isAlive;
        }

        public static bool isKingCompletedTasks()
        {
            bool isCompleted = false;
            foreach (var king in allPlayers)
            {
                if (king.isAlive())
                {
                    if (tasksComplete(king))
                    {
                        isCompleted = true;
                        break;
                    }
                }
            }
            return isCompleted;
        }

        private static bool tasksComplete(PlayerControl p)
        {
            int counter = 0;
            int totalTasks = numCommonTasks + numLongTasks + numShortTasks;
            if (totalTasks == 0) return true;
            foreach (var task in p.Data.Tasks)
            {
                if (task.Complete)
                {
                    counter++;
                }
            }
            return counter >= totalTasks;
        }
    }
}*/