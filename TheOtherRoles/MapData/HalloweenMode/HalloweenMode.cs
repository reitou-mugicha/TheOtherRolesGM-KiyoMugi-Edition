/*using System.Net;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using AssemblyUnhollower;

namespace TheOtherRoles.Mode.HalloweenMode
{
    [HarmonyPatch(typeof(ShipStatus), nameof(GameStartManager.Start))]
    public class halloween
    {
        public static GameObject skeldHelloween;
        public static GameObject miraHalloween;

        public static void Prefix()
        {
            if (PlayerControl.GameOptions.MapId == 0 && CustomOptionHolder.halloweenMode.getBool())
            {
                skeldHelloween = GameObject.Find("Helloween");
                skeldHelloween.active = true;
            }
            if (PlayerControl.GameOptions.MapId == 1 && CustomOptionHolder.halloweenMode.getBool())
            {
                miraHalloween = GameObject.Find("Halloween");
                miraHalloween.active = true;
            }
        }
    }
}*/