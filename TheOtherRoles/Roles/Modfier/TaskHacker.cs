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
    public class TaskHacker : ModifierBase<TaskHacker>
    {
        public static Color color = Palette.ImpostorRed;

        public enum TaskHackerType
        {
            Simple = 0,
            WithRole = 1,
            Random = 2,
        }

        public enum TaskHackerAbility
        {
            None = 0,
            Fanatic = 1,
        }

        public static bool canEnterVents { get { return CustomOptionHolder.madmateCanEnterVents.getBool(); } }
        public static bool hasImpostorVision { get { return CustomOptionHolder.madmateHasImpostorVision.getBool(); } }
        public static bool canSabotage { get { return CustomOptionHolder.madmateCanSabotage.getBool(); } }
        public static bool canFixComm { get { return CustomOptionHolder.madmateCanFixComm.getBool(); } }

        public static TaskHackerType madmateType { get { return TaskHackerType.Simple; } }
        public static TaskHackerAbility madmateAbility { get { return TaskHackerAbility.None; } }

        public static int numCommonTasks { get { return CustomOptionHolder.madmateTasks.commonTasks; } }
        public static int numLongTasks { get { return CustomOptionHolder.madmateTasks.longTasks; } }
        public static int numShortTasks { get { return CustomOptionHolder.madmateTasks.shortTasks; } }


        public static int addNumTasks { get { return (int)CustomOptionHolder.taskHackerNumTasks.getFloat(); } }
        public static int addCrewNumTask { get { return (int)CustomOptionHolder.taskHackerAddCrewNumTasks.getFloat(); } }

        public static bool hasTasks = true;

        public static string prefix
        {
            get
            {
                return ModTranslation.getString("madmatePrefix");
            }
        }

        public static string fullName
        {
            get
            {
                return ModTranslation.getString("madmate");
            }
        }

        public TaskHacker()
        {
            ModType = modId = ModifierType.TaskHacker;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }

        public override void OnDeath(PlayerControl killer = null)
        {
            player.clearAllTasks();
        }

        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }
        public void assignTasks()
        {
            player.generateAndAssignTasks(numCommonTasks, numShortTasks, numLongTasks);
        }
        public static void knowsImpostors(PlayerControl player)
        {
            return;
        }

        public static void tasksComplete(PlayerControl player)
        {
            foreach (var task in player.Data.Tasks)
            {
                if (task.Complete)
                {
                    RPCProcedure.taskHackerAddCrewTasks(player.PlayerId);
                    player.generateAndAssignTasks(0, addNumTasks, 0);
                }
            }
            return;
        }

        public static void Clear()
        {
            players = new List<TaskHacker>();
        }
    }
}