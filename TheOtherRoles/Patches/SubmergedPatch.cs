using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using  TheOtherRoles.Objects;

namespace TheOtherRoles.Patches {
    [HarmonyPatch]
    public class SubmergedPatch
    {
        [HarmonyPatch(typeof(Submerged.Minigames.CustomMinigames.SpawnIn.SubmarineSelectSpawn), nameof(Submerged.Minigames.CustomMinigames.SpawnIn.SubmarineSelectSpawn.Update))]
        class SubmarineSelectSpawnUpdatePatch
        {
            public static void Postfix(Submerged.Minigames.CustomMinigames.SpawnIn.SubmarineSelectSpawn __instance)
            {
                CustomButton.ResetAllCooldowns();
                PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
            }
        }
    }
}