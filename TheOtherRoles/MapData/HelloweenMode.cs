/*
using HarmonyLib;
using UnityEngine;
using TheOtherRoles.Patches;

namespace TheOtherRoles.Mode
{

    [HarmonyPatch(typeof(ShipStatus), nameof(GameStartManager.Start))]
    public class inversion
    {

        public static GameObject skeldHelloween;
        public static GameObject miraHelloween;
        public static void Prefix()
        {

            if (PlayerControl.GameOptions.MapId == 0 && CustomOptionHolder.helloweenMode.getBool())
            {
                var horseModeSelectionBehavior = new ClientOptionsPatch.SelectionBehaviour("Enable Helloween", () => MapOptions.enableHelloween = TheOtherRolesPlugin.EnableHelloween.Value = !TheOtherRolesPlugin.EnableHelloween.Value, TheOtherRolesPlugin.EnableHelloween.Value);
                skeldHelloween = GameObject.Find("Helloween");
                skeldHelloween.active = CustomOptionHolder.helloweenMode.getBool();
            }
            else if (PlayerControl.GameOptions.MapId == 1 && CustomOptionHolder.helloweenMode.getBool())
            {

            }
        }
    }
}
*/