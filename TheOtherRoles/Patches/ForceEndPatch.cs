using HarmonyLib;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class ForceEndPatch
    {
        public static bool triggerForceEnd = false;
    }
}