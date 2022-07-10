/*using Hazel;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Modules;
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Mode
{
    [HarmonyPatch]
    public static class TheOtherRolesGameMode
    {
        public enum ModeSelection
        {
            TheOtherRolesGM = 0,
            BattleRoyal = 1,
        }

        public static int GameModeNumber = 0;

        public static ModeSelection modeName { get { return (ModeSelection)CustomOptionHolder.activateMode.getSelection(); } }

        public static void SetGameMode()
        {
            if (modeName == ModeSelection.TheOtherRolesGM)
                GameModeNumber = 0;
            else if (modeName == ModeSelection.BattleRoyal)
                GameModeNumber = 1;
        }
    }
}*/