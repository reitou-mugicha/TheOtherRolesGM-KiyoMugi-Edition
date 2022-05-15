using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class DiePlayer : RoleBase<Template>
    {
        public static Color color = Palette.CrewmateBlue;
        private static CustomButton zoomIn;
        private static CustomButton zoomOut;
        private static bool enableZoomInOut { get { return CustomOptionHolder.enableDiePlayerZoomInOut.getBool(); } }
        public static Sprite zoomInIcon;
        public static Sprite zoomOutIcon;
        public static GameObject normalEye;
        public static void resetZoom()
        {
            Camera.main.orthographicSize = 3.0f;
            HudManager.Instance.UICamera.orthographicSize = 3.0f;
            HudManager.Instance.transform.localScale = Vector3.one;
            normalEye.active = false;
        }

        public DiePlayer()
        {
            RoleType = roleId = RoleType.NoRole;
            normalEye = GameObject.Find("ShadowQuad");
            normalEye.active = false;
        }

        public override void OnMeetingStart()
        {
            resetZoom();
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate()
        {
            normalEye.active = false;
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            zoomIn = new CustomButton(
                () =>
                {
                    if (Camera.main.orthographicSize > 3.0f)
                    {
                        Camera.main.orthographicSize /= 1.5f;
                        hm.UICamera.orthographicSize /= 1.5f;
                    }

                    if (hm.transform.localScale.x > 1.0f)
                    {
                        hm.transform.localScale /= 1.5f;
                    }
                },
                () => { return enableZoomInOut && PlayerControl.LocalPlayer.isDead() && !PlayerControl.LocalPlayer.isRole(RoleType.GM); },
                () => { return PlayerControl.LocalPlayer.isDead(); },
                () => { },
                DiePlayer.getZoomInSprite(),
                Vector3.zero + Vector3.up * 3.75f + Vector3.right * 0.2f,
                hm,
                hm.UseButton,
                KeyCode.PageUp,
                false
            );
            zoomIn.Timer = 0.0f;
            zoomIn.MaxTimer = 0.0f;
            zoomIn.showButtonText = false;
            zoomIn.LocalScale = Vector3.one * 0.5f;

            zoomOut = new CustomButton(
                () =>
                {
                    if (Camera.main.orthographicSize < 18.0f)
                    {
                        Camera.main.orthographicSize *= 1.5f;
                        hm.UICamera.orthographicSize *= 1.5f;
                    }

                    if (hm.transform.localScale.x < 6.0f)
                    {
                        hm.transform.localScale *= 1.5f;
                    }
                },
                () => { return enableZoomInOut && PlayerControl.LocalPlayer.isDead(); },
                () => { return PlayerControl.LocalPlayer.isDead(); },
                () => { },
                DiePlayer.getZoomOutSprite(),
                Vector3.zero + Vector3.up * 3.75f + Vector3.right * 0.55f,
                hm,
                hm.UseButton,
                KeyCode.PageDown,
                false
            );
            zoomOut.Timer = 0.0f;
            zoomOut.MaxTimer = 0.0f;
            zoomOut.showButtonText = false;
            zoomOut.LocalScale = Vector3.one * 0.5f;
        }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Template>();
        }

        public static Sprite getZoomInSprite()
        {
            if (zoomInIcon) return zoomInIcon;
            zoomInIcon = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.GMZoomIn.png", 115f);
            return zoomInIcon;
        }

        public static Sprite getZoomOutSprite()
        {
            if (zoomOutIcon) return zoomOutIcon;
            zoomOutIcon = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.GMZoomOut.png", 115f);
            return zoomOutIcon;
        }

        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public static class Zoom
        {
            public static bool flag = true;
            public static void Postfix(HudManager __instance)
            {
                if (enableZoomInOut && PlayerControl.LocalPlayer.isDead() && !PlayerControl.LocalPlayer.isRole(RoleType.GM))
                {
                    if ((AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started
                        || AmongUsClient.Instance.GameMode == GameModes.FreePlay)
                        && (PlayerControl.LocalPlayer.CanMove)
                        && !(MapBehaviour.Instance && MapBehaviour.Instance.IsOpen)
                        && !(MeetingHud.Instance))
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") < 0)
                        {
                            if (Camera.main.orthographicSize < 18.0f)
                            {
                                Camera.main.orthographicSize *= 1.5f;
                                __instance.transform.localScale *= 1.5f;
                                __instance.UICamera.orthographicSize *= 1.5f;
                            }
                        }
                        if (Input.GetAxis("Mouse ScrollWheel") > 0)
                        {
                            if (Camera.main.orthographicSize > 3.0f)
                            {
                                Camera.main.orthographicSize /= 1.5f;
                                __instance.transform.localScale /= 1.5f;
                                __instance.UICamera.orthographicSize /= 1.5f;
                            }
                        }
                    }
                    flag = false;
                }
                else
                {
                    if (flag == false)
                    {
                        Reset.Zoom();
                        flag = true;
                    }
                }
            }
        }

        public static class Reset
        {
            public static void Zoom()
            {
                Camera.main.orthographicSize = 3.0f;
                HudManager.Instance.UICamera.orthographicSize = 3.0f;
                HudManager.Instance.transform.localScale = Vector3.one;
                if (MeetingHud.Instance != null) MeetingHud.Instance.transform.localScale = Vector3.one;
                HudManager.Instance.Chat.transform.localScale = Vector3.one;
            }
        }
    }
}