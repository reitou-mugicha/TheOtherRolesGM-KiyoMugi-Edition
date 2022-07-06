using System.Collections.Generic;
using HarmonyLib;
using TheOtherRoles.Objects;
using UnityEngine;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class EvilTracker : RoleBase<EvilTracker>
    {
        public static Color color = Palette.ImpostorRed;
        public static float cooldown { get { return CustomOptionHolder.evilTrackerCooldown.getFloat(); } }
        public static bool resetTargetAfterMeeting { get { return CustomOptionHolder.evilTrackerResetTargetAfterMeeting.getBool(); } }
        public static bool canSeeDeathFlash { get { return CustomOptionHolder.evilTrackerCanSeeDeathFlash.getBool(); } }
        public static PlayerControl target;
        public static PlayerControl currentTarget;
        public static CustomButton trackerButton;
        public static Sprite trackerButtonSprite;
        public static Sprite arrowSprite;


        public EvilTracker()
        {
            RoleType = roleId = RoleType.EvilTracker;
        }

        public override void OnMeetingStart()
        {
            if (resetTargetAfterMeeting)
            {
                target = null;
            }
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate()
        {
            if (PlayerControl.LocalPlayer.isRole(RoleType.EvilTracker))
            {
                arrowUpdate();
            }
            if (player.isAlive())
            {
                currentTarget = setTarget();
                setPlayerOutline(currentTarget, Palette.ImpostorRed);
            }
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static Sprite getTrackerButtonSprite()
        {
            if (trackerButtonSprite) return trackerButtonSprite;
            trackerButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TrackerButton.png", 115f);
            return trackerButtonSprite;
        }
        public static void MakeButtons(HudManager hm)
        {
            trackerButton = new CustomButton(
                () =>
                {
                    target = currentTarget;
                },
                () => { return target == null && PlayerControl.LocalPlayer.isRole(RoleType.EvilTracker) && PlayerControl.LocalPlayer.isAlive(); },
                () => { return currentTarget != null && target == null && PlayerControl.LocalPlayer.CanMove; },
                () => { trackerButton.Timer = trackerButton.MaxTimer; },
                getTrackerButtonSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.KillButton,
                KeyCode.F
            )
            {
                buttonText = ModTranslation.getString("TrackerText")
            };

        }
        public static void SetButtonCooldowns()
        {
            trackerButton.MaxTimer = cooldown;

        }

        public static void Clear()
        {
            players = new List<EvilTracker>();
            target = null;
            currentTarget = null;
            arrows = new List<Arrow>();
        }
        public static List<Arrow> arrows = new();
        public static float updateTimer = 0f;
        public static float arrowUpdateInterval = 0.5f;
        static void arrowUpdate()
        {

            // 前フレームからの経過時間をマイナスする
            updateTimer -= Time.fixedDeltaTime;

            // 1秒経過したらArrowを更新
            if (updateTimer <= 0.0f)
            {

                // 前回のArrowをすべて破棄する
                foreach (Arrow arrow in arrows)
                {
                    if (arrow != null && arrow.arrow != null)
                    {
                        arrow.arrow.SetActive(false);
                        UnityEngine.Object.Destroy(arrow.arrow);
                    }
                }

                // Arrows一覧
                arrows = new List<Arrow>();

                // インポスターの位置を示すArrowsを描画
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    if (p.Data.IsDead) continue;
                    Arrow arrow;
                    if (p.isImpostor() && p != PlayerControl.LocalPlayer)
                    {
                        arrow = new Arrow(Palette.ImpostorRed);
                        arrow.arrow.SetActive(true);
                        arrow.Update(p.transform.position);
                        arrows.Add(arrow);
                    }
                }

                // ターゲットの位置を示すArrowを描画
                if (target != null)
                {
                    Arrow arrow = new(Palette.CrewmateBlue);
                    arrow.arrow.SetActive(true);
                    arrow.Update(target.transform.position);
                    arrows.Add(arrow);
                }

                // タイマーに時間をセット
                updateTimer = arrowUpdateInterval;
            }
        }
        public static Sprite getArrowSprite()
        {
            if (!arrowSprite)
                arrowSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Arrow.png", 300f);
            return arrowSprite;
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
        public static class MurderPlayerPatch
        {
            public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
            {
                PlayerControl player = PlayerControl.LocalPlayer;
                if (__instance.isImpostor() && __instance != player && player.isRole(RoleType.EvilTracker) && player.isAlive() && canSeeDeathFlash)
                {
                    Helpers.showFlash(new Color(42f / 255f, 187f / 255f, 245f / 255f));
                }
            }
        }
    }
}
