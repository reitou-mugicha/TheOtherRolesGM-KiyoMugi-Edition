using HarmonyLib;
using static TheOtherRoles.TheOtherRolesGM;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    public class Speed
    {
        public static void Postfix(PlayerPhysics __instance)
        {
            if (PlayerControl.LocalPlayer.isRole(RoleType.UnderTaker) && UnderTaker.dragginBody)
            {
                __instance.body.velocity *= UnderTaker.speedDown / 100f;
            }
        }
    }
}