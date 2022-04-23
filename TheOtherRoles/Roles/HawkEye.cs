using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;
using Hazel;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class HawkEye : RoleBase<HawkEye>
    {
        private static CustomButton hawkButton;
        public static Color color = Palette.ImpostorRed;

        public static float hawkCooldown { get { return CustomOptionHolder.hawkZoomCooldown.getFloat(); } }
        public static float hawkTime { get { return CustomOptionHolder.hawkEyeTime.getFloat(); } }
        public static bool canUseVents{ get { return CustomOptionHolder.hawkCanUseVents.getBool(); } }
        public bool lightActive = false;

        public HawkEye()
        {
            RoleType = roleId = RoleType.HawkEye;
        }

        public override void OnMeetingStart()
        { 
            hawkButton.isEffectActive = false;
            hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void ResetRole()
        {
            hawkButton.isEffectActive = false;
            hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.hawkButton.png", 115f);
            return buttonSprite;
        }

        public static void MakeButtons(HudManager hm) 
        { 
            // Hawk Eye Button
            hawkButton = new CustomButton(
                () => {
                    hm.transform.localScale *= 4.5f;
                    Camera.main.orthographicSize *= 4.5f;
                    hm.UICamera.orthographicSize *= 4.5f;
                    hawkButton.Timer = 0;
                    local.lightActive = true;
                    return;
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.HawkEye) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
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
                () => {
                    hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
                    local.lightActive = false;
                    hawkButton.isEffectActive = false;
                },
                HawkEye.getButtonSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.KillButton,
                KeyCode.F,
                true,
                HawkEye.hawkTime,
                () => {
                    hawkButton.Timer = hawkButton.MaxTimer = HawkEye.hawkCooldown;
                    hm.transform.localScale /= 4.5f;
                    Camera.main.orthographicSize /= 4.5f;
                    hm.UICamera.orthographicSize /= 4.5f;
                    return;
                }
            );
            hawkButton.buttonText = ModTranslation.getString("HawkEyeText");
            hawkButton.effectCancellable = false;
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