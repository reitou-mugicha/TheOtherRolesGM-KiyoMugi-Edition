using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BepInEx;
using BepInEx.IL2CPP;
using UnhollowerRuntimeLib;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;
using TheOtherRoles.Objects;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch]
    public class SubmergedPatch
    {
        public static void Patch()
        {
            var loaded = IL2CPPChainloader.Instance.Plugins.TryGetValue(SubmergedCompatibility.SUBMERGED_GUID, out PluginInfo pluginInfo);
            if (!loaded) return;
            var plugin = pluginInfo!.Instance as BasePlugin;
            var version = pluginInfo.Metadata.Version;
            var assembly = plugin!.GetType().Assembly;
            var types = AccessTools.GetTypesFromAssembly(assembly);
            var SubmarineSelectSpawnType = types.First(t => t.Name == "SubmarineSelectSpawn");
            var SubmarineSelectSpawnUpdateOriginal = AccessTools.Method(SubmarineSelectSpawnType, "OnDestroy");
            var SubmarineSelectSpawnUpdatePostfix = SymbolExtensions.GetMethodInfo(() => SubmarineSelectSpawnUpdatePatch.Postfix());
            var SubmarineSelectSpawnUpdatePrefix = SymbolExtensions.GetMethodInfo(() => SubmarineSelectSpawnUpdatePatch.Prefix());
            var harmony = new Harmony("Submerged");
            harmony.Patch(SubmarineSelectSpawnUpdateOriginal, new HarmonyMethod(SubmarineSelectSpawnUpdatePrefix), new HarmonyMethod(SubmarineSelectSpawnUpdatePostfix));
        }
        public class SubmarineSelectSpawnUpdatePatch
        {
            public static void Prefix() { }
            public static void Postfix()
            {
                PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
                ShipStatus.Instance.EmergencyCooldown = (float)PlayerControl.GameOptions.EmergencyCooldown;
                ExileControllerReEnableGameplayPatch.ReEnableGameplay();
            }
        }
    }
}