using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using static TheOtherRoles.GameHistory;
using static TheOtherRoles.TheOtherRoles;
using TheOtherRoles.Patches;
using TheOtherRoles.Modules;
using TheOtherRoles.Utilities;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public static class TheOtherRolesGM
    {
        public static void clearAndReloadRoles()
        {
            Morphling.clearAndReload();
            Camouflager.clearAndReload();
            Shifter.clearAndReload();
            Swapper.clearAndReload();
            Portalmaker.clearAndReload();
            UnderTaker.clearAndReload();
            Mayor.clearAndReload();
            GM.clearAndReload();

            Lovers.Clear();
            Opportunist.Clear();
            Ninja.Clear();
            Madmate.Clear();
            CreatedMadmate.Clear();
            PlagueDoctor.Clear();
            Lighter.Clear();
            SerialKiller.Clear();
            Fox.Clear();
            FortuneTeller.Clear();
            HawkEye.Clear();
            DoubleKiller.Clear();
            Chunibyo.Clear();
            Boss.Clear();
            Staff.Clear();
            Gun.Clear();
            Mini.Clear();
            AntiTeleport.Clear();
            Sprinter.Clear();
            Bakery.Clear();
            //Creator.Clear();
            //Student.Clear();
            Role.ClearAll();
        }

        public static void FixedUpdate(PlayerControl player)
        {
            Role.allRoles.DoIf(x => x.player == player, x => x.FixedUpdate());
            Modifier.allModifiers.DoIf(x => x.player == player, x => x.FixedUpdate());
        }

        public static void OnMeetingStart()
        {
            Role.allRoles.Do(x => x.OnMeetingStart());
            Modifier.allModifiers.Do(x => x.OnMeetingStart());

            GM.resetZoom();
            Camouflager.resetCamouflage();
            Morphling.resetMorph();
        }

        public static void OnMeetingEnd()
        {
            Role.allRoles.Do(x => x.OnMeetingEnd());
            Modifier.allModifiers.Do(x => x.OnMeetingEnd());

            CustomOverlays.hideInfoOverlay();
            CustomOverlays.hideBlackBG();
        }

        [HarmonyPatch(typeof(GameData), nameof(GameData.HandleDisconnect), new Type[] { typeof(PlayerControl), typeof(DisconnectReasons) })]
        class HandleDisconnectPatch
        {
            public static void Postfix(GameData __instance, PlayerControl player, DisconnectReasons reason)
            {
                if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started)
                {
                    Role.allRoles.Do(x => x.HandleDisconnect(player, reason));
                    Modifier.allModifiers.Do(x => x.HandleDisconnect(player, reason));

                    Lovers.HandleDisconnect(player, reason);
                    Shifter.HandleDisconnect(player, reason);

                    finalStatuses[player.PlayerId] = FinalStatus.Disconnected;
                }
            }
        }

        public static class Morphling
        {
            public static PlayerControl morphling;
            public static Color color = Palette.ImpostorRed;
            private static Sprite sampleSprite;
            private static Sprite morphSprite;

            public static float cooldown = 30f;
            public static float duration = 10f;

            public static PlayerControl currentTarget;
            public static PlayerControl sampledTarget;
            public static PlayerControl morphTarget;
            public static float morphTimer = 0f;

            public static void handleMorphing()
            {
                if (morphling == null) return;

                // first, if camo is active, don't do anything
                if (Camouflager.camouflager != null && Camouflager.camouflageTimer > 0f) return;

                // next, if we're currently morphed, set our skin to the target
                if (morphTimer > 0f && morphTarget != null)
                {
                    morphling.morphToPlayer(morphTarget);
                }
                else
                {
                    morphling.resetMorph();
                }
            }

            public static void startMorph(PlayerControl target)
            {
                morphTarget = target;
                morphTimer = duration;
                handleMorphing();
            }

            public static void resetMorph()
            {
                morphTarget = null;
                morphTimer = 0f;
                handleMorphing();
            }

            public static void clearAndReload()
            {
                resetMorph();
                morphling = null;
                currentTarget = null;
                sampledTarget = null;
                morphTarget = null;
                morphTimer = 0f;
                cooldown = CustomOptionHolder.morphlingCooldown.getFloat();
                duration = CustomOptionHolder.morphlingDuration.getFloat();
            }

            public static Sprite getSampleSprite()
            {
                if (sampleSprite) return sampleSprite;
                sampleSprite = ModTranslation.getImage("SampleButton", 115f);
                return sampleSprite;
            }

            public static Sprite getMorphSprite()
            {
                if (morphSprite) return morphSprite;
                morphSprite = ModTranslation.getImage("MorphButton", 115f);
                return morphSprite;
            }
        }

        public static class Camouflager
        {
            public static PlayerControl camouflager;
            public static Color color = Palette.ImpostorRed;

            public static float cooldown = 30f;
            public static float duration = 10f;
            public static float camouflageTimer = 0f;
            public static bool randomColors = false;

            public static GameData.PlayerOutfit camoData;

            private static Sprite buttonSprite;
            public static Sprite getButtonSprite()
            {
                if (buttonSprite) return buttonSprite;
                buttonSprite = ModTranslation.getImage("CamoButton", 115f);
                return buttonSprite;
            }

            public static void startCamouflage()
            {
                camouflageTimer = duration;

                if (randomColors)
                    camoData.ColorId = (byte)TheOtherRoles.rnd.Next(0, Palette.PlayerColors.Length);
                else
                    camoData.ColorId = 6;

                foreach (PlayerControl p in PlayerControl.AllPlayerControls.GetFastEnumerator())
                {
                    if (p == null) continue;
                    p.setOutfit(camoData, visible: false);
                }
            }

            public static void resetCamouflage()
            {
                camouflageTimer = 0f;
                foreach (PlayerControl p in PlayerControl.AllPlayerControls.GetFastEnumerator())
                {
                    if (p == null) continue;

                    // special case for morphling
                    if (Morphling.morphling?.PlayerId == p.PlayerId)
                    {
                        Morphling.handleMorphing();
                    }
                    else
                    {
                        p.resetMorph();
                    }
                }
            }

            public static void clearAndReload()
            {
                resetCamouflage();
                camouflager = null;
                camouflageTimer = 0f;
                cooldown = CustomOptionHolder.camouflagerCooldown.getFloat();
                duration = CustomOptionHolder.camouflagerDuration.getFloat();
                randomColors = CustomOptionHolder.camouflagerRandomColors.getBool();

                camoData = new GameData.PlayerOutfit();
                camoData.PlayerName = "";
                camoData.HatId = "";
                camoData.ColorId = 6;
                camoData.SkinId = "";
                camoData.PetId = "";
                camoData.VisorId = "";
                camoData.NamePlateId = "";
            }
        }

        public static class Shifter
        {
            public static PlayerControl shifter;
            public static List<int> pastShifters = new List<int>();
            public static Color color = new Color32(102, 102, 102, byte.MaxValue);

            public static PlayerControl futureShift;
            public static PlayerControl currentTarget;
            public static bool shiftModifiers = false;

            public static bool isNeutral = false;
            public static bool shiftPastShifters = false;

            public static void HandleDisconnect(PlayerControl player, DisconnectReasons reason)
            {
                if (futureShift == player) futureShift = null;
            }

            private static Sprite buttonSprite;
            public static Sprite getButtonSprite()
            {
                if (buttonSprite) return buttonSprite;
                buttonSprite = ModTranslation.getImage("ShiftButton", 115f);
                return buttonSprite;
            }

            public static void clearAndReload()
            {
                shifter = null;
                pastShifters = new List<int>();
                currentTarget = null;
                futureShift = null;
                shiftModifiers = CustomOptionHolder.shifterShiftsModifiers.getBool();
                shiftPastShifters = CustomOptionHolder.shifterPastShifters.getBool();
                isNeutral = false;
            }
        }

        public static class Swapper
        {
            public static PlayerControl swapper;
            public static Color color = new Color32(134, 55, 86, byte.MaxValue);
            private static Sprite spriteCheck;
            public static bool canCallEmergency = false;
            public static bool canOnlySwapOthers = false;
            public static int numSwaps = 2;

            public static byte playerId1 = Byte.MaxValue;
            public static byte playerId2 = Byte.MaxValue;

            public static Sprite getCheckSprite()
            {
                if (spriteCheck) return spriteCheck;
                spriteCheck = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SwapperCheck.png", 150f);
                return spriteCheck;
            }

            public static void clearAndReload()
            {
                swapper = null;
                playerId1 = Byte.MaxValue;
                playerId2 = Byte.MaxValue;
                canCallEmergency = CustomOptionHolder.swapperCanCallEmergency.getBool();
                canOnlySwapOthers = CustomOptionHolder.swapperCanOnlySwapOthers.getBool();
                numSwaps = Mathf.RoundToInt(CustomOptionHolder.swapperNumSwaps.getFloat());
            }
        }

        public static class Assassin
        {
            public static PlayerControl assassin;
            public static Color color = Palette.ImpostorRed;

            public static PlayerControl assassinMarked;
            public static PlayerControl currentTarget;
            public static float cooldown { get { return CustomOptionHolder.assassinCooldown.getFloat(); } }
            public static float start { get { return CustomOptionHolder.assassinStartAssassin.getFloat(); } }
            public static float traceTime { get { return CustomOptionHolder.assassinTraceTime.getFloat(); } }
            public static bool knowsTargetLocation { get { return CustomOptionHolder.assassinKnowsTargetLocation.getBool(); } }

            private static Sprite markButtonSprite;
            private static Sprite killButtonSprite;
            public static Arrow arrow = new Arrow(Color.black);
            public static Sprite getMarkButtonSprite()
            {
                if (markButtonSprite) return markButtonSprite;
                markButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AssassinMarkButton.png", 115f);
                return markButtonSprite;
            }

            public static Sprite getKillButtonSprite()
            {
                if (killButtonSprite) return killButtonSprite;
                killButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AssassinAssassinateButton.png", 115f);
                return killButtonSprite;
            }

            public static void clearAndReload()
            {
                assassin = null;
                currentTarget = assassinMarked = null;

                if (arrow?.arrow != null) UnityEngine.Object.Destroy(arrow.arrow);
                arrow = new Arrow(Color.black);
                if (arrow.arrow != null) arrow.arrow.SetActive(false);
            }
        }

        public static class UnderTaker
        {
            public static PlayerControl underTaker;
            public static Color color = Palette.ImpostorRed;

            public static float speedDown = 100f;
            public static bool dragginBody = false;
            public static byte bodyId = 0;

            private static Sprite buttonSprite;
            public static Sprite getButtonSprite()
            {
                if (buttonSprite) return buttonSprite;
                buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.UnderTakerButton.png", 115f);
                return buttonSprite;
            }

            public static void clearAndReload()
            {
                underTaker = null;
                speedDown = CustomOptionHolder.underTakerSpeed.getFloat();
                dragginBody = false;
                bodyId = 0;
            }
            public static void underTakerResetValuesAtDead()
            {
                // Restore janitor values when dead
                dragginBody = false;
                bodyId = 0;
                if (PlayerControl.GameOptions.MapId == 5)
                {
                    GameObject vent = GameObject.Find("LowerCentralVent");
                    vent.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        public static class GM
        {
            public static PlayerControl gm;
            public static Color color = new Color32(255, 91, 112, byte.MaxValue);

            public static bool gmIsHost = true;
            public static bool diesAtStart = true;
            public static bool hasTasks = false;
            public static bool canSabotage = false;
            public static bool canWarp = true;
            public static bool canKill = false;

            private static Sprite zoomInSprite;
            private static Sprite zoomOutSprite;

            public static Sprite getZoomInSprite()
            {
                if (zoomInSprite) return zoomInSprite;
                zoomInSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.GMZoomIn.png", 115f / 2f);
                return zoomInSprite;
            }
            public static Sprite getZoomOutSprite()
            {
                if (zoomOutSprite) return zoomOutSprite;
                zoomOutSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.GMZoomOut.png", 115f / 2f);
                return zoomOutSprite;
            }

            public static void resetZoom()
            {
                Camera.main.orthographicSize = 3.0f;
                FastDestroyableSingleton<HudManager>.Instance.UICamera.orthographicSize = 3.0f;
                FastDestroyableSingleton<HudManager>.Instance.transform.localScale = Vector3.one;
            }

            public static void FixedUpdate()
            {
            }

            public static void clearAndReload()
            {
                gm = null;
                gmIsHost = CustomOptionHolder.gmIsHost.getBool();
                diesAtStart = CustomOptionHolder.gmDiesAtStart.getBool();
                hasTasks = false;
                canSabotage = false;
                zoomInSprite = null;
                zoomOutSprite = null;
                canWarp = CustomOptionHolder.gmCanWarp.getBool();
                canKill = CustomOptionHolder.gmCanKill.getBool();

                foreach (PoolablePlayer p in MapOptions.playerIcons.Values)
                {
                    if (p != null && p.gameObject != null) p.gameObject.SetActive(false);
                }
            }
        }
    }
}