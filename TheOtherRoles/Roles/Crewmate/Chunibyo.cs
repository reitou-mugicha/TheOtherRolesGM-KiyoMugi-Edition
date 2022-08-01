/*using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Modules;
using Hazel;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Chunibyo : RoleBase<Chunibyo>
    {
        public static CustomButton chunibyoButton;
        public static float cooldown { get { return CustomOptionHolder.chunibyoCooldown.getFloat(); } }
        public static float duration { get { return CustomOptionHolder.chunibyoDuration.getFloat(); } }
        public static float speedUpBonus { get { return CustomOptionHolder.chunibyoSpeedUpBonus.getFloat() / 100f; } }
        public static float speedDownBonus { get { return CustomOptionHolder.chunibyoSpeedDownBonus.getFloat() / 100f; } }
        public static float highVisionBonus { get { return CustomOptionHolder.chunibyoHighVisionBonus.getFloat(); } }
        public static float lowVisionBonus { get { return CustomOptionHolder.chunibyoLowVisionBonus.getFloat(); } }
        public static Color color = Color.yellow;
        public static float ab = 0;

        public Chunibyo()
        {
            RoleType = roleId = RoleType.Chunibyo;
        }

        public override void OnMeetingStart()
        {
            chunibyoButton.isEffectActive = false;
            chunibyoButton.Timer = chunibyoButton.MaxTimer = Chunibyo.cooldown;
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null)
        {
            chunibyoButton.isEffectActive = false;
        }

        public override void ResetRole()
        {
            chunibyoButton.isEffectActive = false;
            chunibyoButton.Timer = chunibyoButton.MaxTimer = Chunibyo.cooldown;
        }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            chunibyoButton = new CustomButton(
                () =>
                {
                    // 0 = Speed Up
                    // 1 = Speed Down
                    // 2 = High Vision
                    // 3 = Low Vision
                    // 4 = No Ability
                    List<int> Ability = new List<int>();
                    if (CustomOptionHolder.chunibyoEnableSpeedUp.getBool())
                        Ability.Add(0);
                    if (CustomOptionHolder.chunibyoEnableSpeedDown.getBool())
                        Ability.Add(1);
                    if (CustomOptionHolder.chunibyoEnableHighVision.getBool())
                        Ability.Add(2);
                    if (CustomOptionHolder.chunibyoEnableLowVision.getBool())
                        Ability.Add(3);
                    if (CustomOptionHolder.chunibyoAddNoneAbility.getBool())
                        Ability.Add(4);
                    int AbilityNum = Ability[TheOtherRoles.rnd.Next(Ability.Count)];

                    if (AbilityNum == 0)
                        ab = 0;
                    else if (AbilityNum == 1)
                        ab = 1;
                    else if (AbilityNum == 2)
                        ab = 2;
                    else if (AbilityNum == 3)
                        ab = 3;
                    else if (AbilityNum == 4)
                        ab = 4;

                    if (Chunibyo.ab == 0)
                        new CustomMessage(ModTranslation.getString("runFast"), Chunibyo.duration);
                    else if (Chunibyo.ab == 1)
                        new CustomMessage(ModTranslation.getString("runSlow"), Chunibyo.duration);
                    else if (Chunibyo.ab == 2)
                        new CustomMessage(ModTranslation.getString("highVision"), Chunibyo.duration);
                    else if (Chunibyo.ab == 3)
                        new CustomMessage(ModTranslation.getString("lowVision"), Chunibyo.duration);
                    else if (Chunibyo.ab == 4)
                        new CustomMessage(ModTranslation.getString("noAbility"), Chunibyo.duration);
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Chunibyo) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (chunibyoButton.isEffectActive)
                    {
                        chunibyoButton.buttonText = ModTranslation.getString("ChunibyoFinishText");
                    }
                    else
                    {
                        chunibyoButton.buttonText = ModTranslation.getString("ChunibyoText");
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () =>
                {
                    chunibyoButton.Timer = chunibyoButton.MaxTimer = Chunibyo.cooldown;
                },
                getButtonSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.UseButton,
                KeyCode.F,
                true,
                duration,
                () =>
                {
                    chunibyoButton.Timer = chunibyoButton.MaxTimer = Chunibyo.cooldown;
                }
            );
            chunibyoButton.buttonText = ModTranslation.getString("ChunibyoText");
            chunibyoButton.effectCancellable = false;
        }

        public static void SetButtonCooldowns()
        {
            chunibyoButton.MaxTimer = Chunibyo.cooldown;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ChunibyoButton.png", 115f);
            return buttonSprite;
        }

        public static void Clear()
        {
            players = new List<Chunibyo>();
        }
    }
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    public class SpeedUpOrDown
    {
        public static void Postfix(PlayerPhysics __instance)
        {
            if (Chunibyo.chunibyoButton.isEffectActive && Chunibyo.ab == 0)
            {
                __instance.body.velocity *= Chunibyo.speedUpBonus;
            }
            if (Chunibyo.chunibyoButton.isEffectActive && Chunibyo.ab == 1)
            {
                __instance.body.velocity *= Chunibyo.speedDownBonus;
            }
        }
    }
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CalculateLightRadius))]
    public class HighOrLowVision
    {
        public static void Postfix(ref float __result)
        {
            if (Chunibyo.chunibyoButton.isEffectActive && Chunibyo.ab == 2)
            {
                __result *= 1f + Chunibyo.highVisionBonus * 0.01f;
            }
            if (Chunibyo.chunibyoButton.isEffectActive && Chunibyo.ab == 3)
            {
                __result *= 1f - Chunibyo.lowVisionBonus * 0.01f;
            }
        }
    }
}
*/