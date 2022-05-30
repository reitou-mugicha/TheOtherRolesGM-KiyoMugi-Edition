using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using TheOtherRoles.Modules;
using TheOtherRoles.Utilities;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class ForceEndPatch
    {
        public static bool triggerForceEnd = false;
    }
}