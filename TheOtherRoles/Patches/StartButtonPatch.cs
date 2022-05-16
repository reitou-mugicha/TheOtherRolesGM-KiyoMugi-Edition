using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using System;
using System.Linq;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnhollowerBaseLib;
using Hazel;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Linq;
using Il2CppSystem;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace TheOtherRoles.Patch
{
    class GameStartPatch
    {
        [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
        public static class LobbyCountDownTimer
        {/*
            public static GameObject startButton;
            public static GameObject startButtonText;*/

            public static void Postfix(GameStartManager __instance)
            {
                if (CustomOptionHolder.betterStartButtons.getBool())
                {
                    GameStartManager.Instance.countDownTimer = 0;/*
                    startButton = GameObject.Find("StartButton");
                    startButton.transform.localScale = new Vector3(-0.8f, 0.8f, 0.8f);
                    startButton.transform.localPosition = new Vector3(-1f, 0f, -10f);

                    startButtonText = GameObject.Find("Text_TMP");
                    startButtonText.transform.localScale = new Vector3(-1f, 1f, -1f);*/
                }
            }
        }/*

        [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Start))]
        public static class ForceEnd
        {
            public static void Prefix(GameStartManager __instance)
            {
                var template = GameObject.Find("StartButton");
                if (template == null) return;

                var buttonForceEnd = UnityEngine.Object.Instantiate(template, null);
                buttonForceEnd.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                buttonForceEnd.transform.localPosition = new Vector3(buttonForceEnd.transform.localPosition.x, buttonForceEnd.transform.localPosition.y, buttonForceEnd.transform.localPosition.z);

                var textForceEnd = buttonForceEnd.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
                __instance.StartCoroutine(Effects.Lerp(0.1f, new System.Action<float>((p) =>
                {
                    textForceEnd.SetText(ModTranslation.getString("forceEndText"));
                })));

                PassiveButton passiveButtonDiscord = buttonForceEnd.GetComponent<PassiveButton>();
                SpriteRenderer buttonSpriteDiscord = buttonForceEnd.GetComponent<SpriteRenderer>();
            }
        }*/
    }
}
// 3.5 4