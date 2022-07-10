using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Modules;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class HawkEye : RoleBase<HawkEye>
    {
        private static CustomButton hawkButton;
        public static Color color = Palette.ImpostorRed;

        public static float hawkCooldown { get { return CustomOptionHolder.hawkZoomCooldown.getFloat(); } }
        public static float hawkTime { get { return CustomOptionHolder.hawkEyeTime.getFloat(); } }
        public static bool canUseVents { get { return CustomOptionHolder.hawkCanUseVents.getBool(); } }
        public static GameObject normalEye;

        public HawkEye()
        {
            RoleType = roleId = RoleType.HawkEye;
            normalEye = GameObject.Find("ShadowQuad");
        }

        public override void OnMeetingStart()
        {
            hawkButton.isEffectActive = false;
            hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
            normalEye.active = true;
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void ResetRole()
        {
            hawkButton.isEffectActive = false;
            hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
            normalEye.active = true;
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.HawkButton.png", 115f);
            return buttonSprite;
        }

        public static void MakeButtons(HudManager hm)
        {
            // Hawk Eye Button
            hawkButton = new CustomButton(
                () =>
                {
                    if (hawkButton.isEffectActive)
                    {
                        hawkButton.Timer = 0;
                        return;
                    }
                    hm.transform.localScale *= 4.5f;
                    Camera.main.orthographicSize *= 4.5f;
                    hm.UICamera.orthographicSize *= 4.5f;
                    normalEye.active = false;
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.HawkEye) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (hawkButton.isEffectActive)
                    {
                        hawkButton.buttonText = ModTranslation.getString("HawkEyeFinishText");
                    }
                    else
                    {
                        hawkButton.buttonText = ModTranslation.getString("HawkEyeText");
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () =>
                {
                    hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
                },
                HawkEye.getButtonSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.KillButton,
                KeyCode.F,
                true,
                HawkEye.hawkTime,
                () =>
                {
                    hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
                    hm.transform.localScale /= 4.5f;
                    Camera.main.orthographicSize /= 4.5f;
                    hm.UICamera.orthographicSize /= 4.5f;
                    normalEye.active = true;
                }
            );
            hawkButton.buttonText = ModTranslation.getString("HawkEyeText");
            hawkButton.effectCancellable = true;
        }

        public static void SetButtonCooldowns()
        {
            hawkButton.MaxTimer = HawkEye.hawkCooldown;
        }

        public static void Clear()
        {
            players = new List<HawkEye>();
        }
    }
}