using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using TheOtherRoles.Utilities;

namespace TheOtherRoles.Modules
{
    public class CustomColors
    {
        protected static Dictionary<int, string> ColorStrings = new Dictionary<int, string>();
        public static List<int> lighterColors = new List<int>() { 3, 4, 5, 7, 10, 11, 13, 14, 17 };
        public static uint pickableColors = (uint)Palette.ColorNames.Length;

        /* version 1
        private static readonly List<int> ORDER = new List<int>() { 7, 17, 5, 33, 4,
                                                                    30, 0, 19, 27, 3,
                                                                    13, 25, 18, 15, 23,
                                                                    8, 32, 1, 21, 31,
                                                                    10, 34, 12, 14, 28,
                                                                    22, 29, 11, 26, 2,
                                                                    20, 24, 9, 16, 6 }; */
        private static readonly List<int> ORDER = new List<int>() { 7, 14, 5, 33, 4, 30, 0, 19, 27, 3, 17, 25, 
                                                                    18, 13, 23, 8, 32, 1, 21, 31, 10, 34, 15, 28, 
                                                                    22, 29, 11, 2, 26, 16, 20, 24, 9, 12, 6, 35,
                                                                    36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47,
                                                                    48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
                                                                    60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71,
                                                                    72, 73, 74, 75, 76};
        public static void Load()
        {
            List<StringNames> longlist = Enumerable.ToList<StringNames>(Palette.ColorNames);
            List<Color32> colorlist = Enumerable.ToList<Color32>(Palette.PlayerColors);
            List<Color32> shadowlist = Enumerable.ToList<Color32>(Palette.ShadowColors);

            List<CustomColor> colors = new List<CustomColor>();

            /* Custom Colors */
            colors.Add(new CustomColor
            {
                longname = "colorSalmon",
                color = new Color32(239, 191, 192, byte.MaxValue), // color = new Color32(0xD8, 0x82, 0x83, byte.MaxValue),
                shadow = new Color32(182, 119, 114, byte.MaxValue), // shadow = new Color32(0xA5, 0x63, 0x65, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorBordeaux",
                color = new Color32(109, 7, 26, byte.MaxValue),
                shadow = new Color32(54, 2, 11, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorOlive",
                color = new Color32(154, 140, 61, byte.MaxValue),
                shadow = new Color32(104, 95, 40, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorTurqoise",
                color = new Color32(22, 132, 176, byte.MaxValue),
                shadow = new Color32(15, 89, 117, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorMint",
                color = new Color32(111, 192, 156, byte.MaxValue),
                shadow = new Color32(65, 148, 111, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorLavender",
                color = new Color32(173, 126, 201, byte.MaxValue),
                shadow = new Color32(131, 58, 203, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorNougat",
                color = new Color32(160, 101, 56, byte.MaxValue),
                shadow = new Color32(115, 15, 78, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorPeach",
                color = new Color32(255, 164, 119, byte.MaxValue),
                shadow = new Color32(238, 128, 100, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorWasabi",
                color = new Color32(112, 143, 46, byte.MaxValue),
                shadow = new Color32(72, 92, 29, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorHotPink",
                color = new Color32(255, 51, 102, byte.MaxValue),
                shadow = new Color32(232, 0, 58, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorPetrol",
                color = new Color32(0, 99, 105, byte.MaxValue),
                shadow = new Color32(0, 61, 54, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorLemon",
                color = new Color32(0xDB, 0xFD, 0x2F, byte.MaxValue),
                shadow = new Color32(0x74, 0xE5, 0x10, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorSignalOrange",
                color = new Color32(0xF7, 0x44, 0x17, byte.MaxValue),
                shadow = new Color32(0x9B, 0x2E, 0x0F, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorTeal",
                color = new Color32(0x25, 0xB8, 0xBF, byte.MaxValue),
                shadow = new Color32(0x12, 0x89, 0x86, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorBlurple",
                color = new Color32(0x59, 0x3C, 0xD6, byte.MaxValue),
                shadow = new Color32(0x29, 0x17, 0x96, byte.MaxValue),
                isLighterColor = false
            });

            colors.Add(new CustomColor
            {
                longname = "colorSunrise",
                color = new Color32(0xFF, 0xCA, 0x19, byte.MaxValue),
                shadow = new Color32(0xDB, 0x44, 0x42, byte.MaxValue),
                isLighterColor = true
            });

            colors.Add(new CustomColor
            {
                longname = "colorIce",
                color = new Color32(0xA8, 0xDF, 0xFF, byte.MaxValue),
                shadow = new Color32(0x59, 0x9F, 0xC8, byte.MaxValue),
                isLighterColor = true
            });


            //Color by Hawk
            colors.Add(new CustomColor { longname = "Red",
                                        color = new Color32(255, 0, 0, byte.MaxValue), // color = new Color32(0xD8, 0x82, 0x83, byte.MaxValue),
                                        shadow = new Color32(128, 0, 0, byte.MaxValue), // shadow = new Color32(0xA5, 0x63, 0x65, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Orange",
                                        color = new Color32(255, 128, 0, byte.MaxValue),
                                        shadow = new Color32(192, 64, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Yellow",
                                        color = new Color32(255, 255, 0, byte.MaxValue),
                                        shadow = new Color32(192, 128, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Lime",
                                        color = new Color32(128, 255, 0, byte.MaxValue),
                                        shadow = new Color32(64, 160, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Green",
                                        color = new Color32(0, 255, 0, byte.MaxValue),
                                        shadow = new Color32(0, 128, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Turquoise",
                                        color = new Color32(0, 255, 128, byte.MaxValue),
                                        shadow = new Color32(0, 160, 96, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Cyan",
                                        color = new Color32(0, 255, 255, byte.MaxValue),
                                        shadow = new Color32(0, 160, 160, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Azur",
                                        color = new Color32(0, 128, 255, byte.MaxValue),
                                        shadow = new Color32(0, 80, 144, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Blue",
                                        color = new Color32(24, 24, 255, byte.MaxValue),
                                        shadow = new Color32(0, 0, 128, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Purple",
                                        color = new Color32(128, 0, 255, byte.MaxValue),
                                        shadow = new Color32(96, 0, 160, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Magenta",
                                        color = new Color32(255, 0, 255, byte.MaxValue),
                                        shadow = new Color32(160, 0, 160, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Plum",
                                        color = new Color32(255, 0, 128, byte.MaxValue),
                                        shadow = new Color32(160, 0, 96, byte.MaxValue),
                                        isLighterColor = true });


            /* Custom Colors 30-41*/
            colors.Add(new CustomColor { longname = "LightRed",
                                        color = new Color32(255, 184, 200, byte.MaxValue), // color = new Color32(0xD8, 0x82, 0x83, byte.MaxValue),
                                        shadow = new Color32(240, 128, 144, byte.MaxValue), // shadow = new Color32(0xA5, 0x63, 0x65, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightOrange",
                                        color = new Color32(255, 184, 72, byte.MaxValue),
                                        shadow = new Color32(224, 104, 8, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightYellow",
                                        color = new Color32(255, 255, 96, byte.MaxValue),
                                        shadow = new Color32(224, 168, 24, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightLime",
                                        color = new Color32(192, 255, 80, byte.MaxValue),
                                        shadow = new Color32(112, 192, 16, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightGreen",
                                        color = new Color32(128, 255, 128, byte.MaxValue),
                                        shadow = new Color32(64, 176, 64, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightTurquoise",
                                        color = new Color32(112, 255, 176, byte.MaxValue),
                                        shadow = new Color32(32, 200, 96, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = " LightCyan",
                                        color = new Color32(128, 255, 255, byte.MaxValue),
                                        shadow = new Color32(32, 192, 192, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightAzur",
                                        color = new Color32(128, 196, 255, byte.MaxValue),
                                        shadow = new Color32(48, 144, 224, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightBlue",
                                        color = new Color32(128, 176, 255, byte.MaxValue),
                                        shadow = new Color32(80, 128, 240 , byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightPurple",
                                        color = new Color32(196, 128, 255, byte.MaxValue),
                                        shadow = new Color32(152, 48, 208, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightMagenta",
                                        color = new Color32(255, 160, 255, byte.MaxValue),
                                        shadow = new Color32(224, 96, 224, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "LightPlum",
                                        color = new Color32(255, 132, 208, byte.MaxValue),
                                        shadow = new Color32(232, 64, 176, byte.MaxValue),
                                        isLighterColor = true });


            /* Custom Colors 42-53*/
            colors.Add(new CustomColor { longname = "DarkRed",
                                        color = new Color32(128, 0, 16, byte.MaxValue), // color = new Color32(0xD8, 0x82, 0x83, byte.MaxValue),
                                        shadow = new Color32(64, 0, 8, byte.MaxValue), // shadow = new Color32(0xA5, 0x63, 0x65, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkOrange",
                                        color = new Color32(160, 64, 0, byte.MaxValue),
                                        shadow = new Color32(112, 24, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkYellow",
                                        color = new Color32(176, 112, 0, byte.MaxValue),
                                        shadow = new Color32(96, 48, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkLime",
                                        color = new Color32(64, 128, 0, byte.MaxValue),
                                        shadow = new Color32(32, 80, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkGreen",
                                        color = new Color32(0, 128, 0, byte.MaxValue),
                                        shadow = new Color32(0, 64, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkTurquoise",
                                        color = new Color32(0, 128, 64, byte.MaxValue),
                                        shadow = new Color32(0, 72, 48, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkCyan",
                                        color = new Color32(0, 128, 128, byte.MaxValue),
                                        shadow = new Color32(0, 64, 80, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkAzur",
                                        color = new Color32(0, 64, 128, byte.MaxValue),
                                        shadow = new Color32(0, 32, 80, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkBlue",
                                        color = new Color32(12, 12, 136, byte.MaxValue),
                                        shadow = new Color32(8, 8, 80, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkPurple",
                                        color = new Color32(64, 0, 128, byte.MaxValue),
                                        shadow = new Color32(32, 0, 64, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkMagenta",
                                        color = new Color32(144, 0, 96, byte.MaxValue),
                                        shadow = new Color32(80, 0, 48, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "DarkPlum",
                                        color = new Color32(144, 0, 32, byte.MaxValue),
                                        shadow = new Color32(96, 0, 32, byte.MaxValue),
                                        isLighterColor = true });


            colors.Add(new CustomColor { longname = "Vermilion",
                                        color = new Color32(255, 24, 0, byte.MaxValue), 
                                        shadow = new Color32(128, 12, 0, byte.MaxValue),
                                        isLighterColor = false });
            colors.Add(new CustomColor { longname = "Yamabuki",
                                        color = new Color32(255, 160, 0, byte.MaxValue), 
                                        shadow = new Color32(160, 96, 0, byte.MaxValue),
                                        isLighterColor = true });
            colors.Add(new CustomColor { longname = "Emerald", 
                                        color = new Color32(0, 160, 88, byte.MaxValue), 
                                        shadow = new Color32(0, 96, 80, byte.MaxValue),
                                        isLighterColor = false });
            colors.Add(new CustomColor { longname = "Iris",
                                        color = new Color32(40, 0, 224, byte.MaxValue), 
                                        shadow = new Color32(16, 0, 112, byte.MaxValue),
                                        isLighterColor = false });   
            colors.Add(new CustomColor { longname = "Sakura",
                                        color = new Color32(255, 192, 216, byte.MaxValue), 
                                        shadow = new Color32(224, 152, 176, byte.MaxValue),
                                        isLighterColor = true });   
            colors.Add(new CustomColor { longname = "Night",
                                        color = new Color32(0, 4, 56, byte.MaxValue), 
                                        shadow = new Color32(0, 2, 28, byte.MaxValue),
                                        isLighterColor = false });  

            pickableColors += (uint)colors.Count; // Colors to show in Tab
            /** Hidden Colors **/

            /** Add Colors **/
            int id = 50000;
            foreach (CustomColor cc in colors)
            {
                longlist.Add((StringNames)id);
                CustomColors.ColorStrings[id++] = cc.longname;
                colorlist.Add(cc.color);
                shadowlist.Add(cc.shadow);
                if (cc.isLighterColor)
                    lighterColors.Add(colorlist.Count - 1);
            }

            Palette.ColorNames = longlist.ToArray();
            Palette.PlayerColors = colorlist.ToArray();
            Palette.ShadowColors = shadowlist.ToArray();
        }

        protected internal struct CustomColor
        {
            public string longname;
            public Color32 color;
            public Color32 shadow;
            public bool isLighterColor;
        }

        [HarmonyPatch]
        public static class CustomColorPatches
        {
            [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString), new[] {
                typeof(StringNames),
                typeof(Il2CppReferenceArray<Il2CppSystem.Object>)
            })]
            private class ColorStringPatch
            {
                public static bool Prefix(ref string __result, [HarmonyArgument(0)] StringNames name)
                {
                    if ((int)name >= 50000)
                    {
                        string text = CustomColors.ColorStrings[(int)name];
                        if (text != null)
                        {
                            __result = ModTranslation.getString(text) + " (MOD)";
                            return false;
                        }
                    }
                    return true;
                }
            }
            [HarmonyPatch(typeof(PlayerTab), nameof(PlayerTab.OnEnable))]
            private static class PlayerTabEnablePatch
            {
                public static void Postfix(PlayerTab __instance)
                { // Replace instead
                    Il2CppArrayBase<ColorChip> chips = __instance.ColorChips.ToArray();

                    int cols = 12; // TODO: Design an algorithm to dynamically position chips to optimally fill space
                    for (int i = 0; i < ORDER.Count; i++)
                    {
                        int pos = ORDER[i];
                        if (pos < 0 || pos > chips.Length)
                            continue;
                        ColorChip chip = chips[pos];
                        int row = i / cols, col = i % cols; // Dynamically do the positioning
                        chip.transform.localPosition = new Vector3(-0.975f + (col * 0.485f), 1.475f - (row * 0.49f), chip.transform.localPosition.z);
                        chip.transform.localScale *= 0.78f;
                    }
                    for (int j = ORDER.Count; j < chips.Length; j++)
                    { // If number isn't in order, hide it
                        ColorChip chip = chips[j];
                        chip.transform.localScale *= 0f;
                        chip.enabled = false;
                        chip.Button.enabled = false;
                        chip.Button.OnClick.RemoveAllListeners();
                    }
                }
            }
            [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.LoadPlayerPrefs))]
            private static class LoadPlayerPrefsPatch
            { // Fix Potential issues with broken colors
                private static bool needsPatch = false;
                public static void Prefix([HarmonyArgument(0)] bool overrideLoad)
                {
                    if (!SaveManager.loaded || overrideLoad)
                        needsPatch = true;
                }
                public static void Postfix()
                {
                    if (!needsPatch) return;
                    SaveManager.colorConfig %= CustomColors.pickableColors;
                    needsPatch = false;
                }
            }
            [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CheckColor))]
            private static class PlayerControlCheckColorPatch
            {
                private static bool isTaken(PlayerControl player, uint color)
                {
                    foreach (GameData.PlayerInfo p in GameData.Instance.AllPlayers.GetFastEnumerator())
                        if (!p.Disconnected && p.PlayerId != player.PlayerId && p.DefaultOutfit.ColorId == color)
                            return true;
                    return false;
                }
                public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte bodyColor)
                { // Fix incorrect color assignment
                    uint color = (uint)bodyColor;
                    if (isTaken(__instance, color) || color >= Palette.PlayerColors.Length)
                    {
                        int num = 0;
                        while (num++ < 50 && (color >= CustomColors.pickableColors || isTaken(__instance, color)))
                        {
                            color = (color + 1) % CustomColors.pickableColors;
                        }
                    }
                    __instance.RpcSetColor((byte)color);
                    return false;
                }
            }
        }
    }
}