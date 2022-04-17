/*
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
    public class Chunibyo : RoleBase<Chunibyo>
    {
        public static CustomButton chunibyoButton;
        public static float cooldown { get { return CustomOptionHolder.chunibyoCooldown.getFloat(); } }
        public static float duration { get { return CustomOptionHolder.chunibyoDuration.getFloat(); } }
        public static float lighterModeLightsOnVision = 2f;
        public static float lighterModeLightsOffVision = 0.75f;
        public static float speed = 2f;
        public static Color color = Color.yellow;
        public bool lightActive = false;
        public static bool speedDown = false;
        public static bool speedUp = false;

        public Chunibyo()
        {
            RoleType = roleId = RoleType.Chunibyo;
        }

        public static void randomAbility()
        {
            System.Random random = new System.Random();
            int abilityNum = (random.Next(1,3));//1から2

            if (abilityNum == 0)
            {
                local.lightActive = true;
            }
            else if (abilityNum == 1)
            {
                speedDown = true;
            }
            else if (abilityNum == 2)
            {
                speedUp = true;
            }
        }

        public static bool isLightActive(PlayerControl player)
        {
            if (isRole(player) && player.isAlive())
            {
                Chunibyo r = players.First(x => x.player == player);
                return r.lightActive;
            }
            return false;
        }

        public override void OnMeetingStart()
        {
            chunibyoButton.Timer = chunibyoButton.MaxTimer = cooldown;
            local.lightActive = false;
            speedDown = false;
            speedUp = false;
            chunibyoButton.isEffectActive = false;
        }
        public override void OnMeetingEnd() { }
        public override void ResetRole()
        {
            local.lightActive = false;
            speedDown = false;
            speedUp = false;
            chunibyoButton.isEffectActive = false;
            chunibyoButton.Timer = chunibyoButton.MaxTimer = cooldown;
        }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer)
        {
            local.lightActive = false;
            speedDown = false;
            speedUp = false;
            chunibyoButton.isEffectActive = false;
        }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) 
        {

            chunibyoButton = new CustomButton(
                () => {
                    if (chunibyoButton.isEffectActive)
                    {
                        chunibyoButton.Timer = 0;
                        return;
                    }
                    randomAbility();
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Chunibyo) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (chunibyoButton.isEffectActive)
                    {
                        chunibyoButton.buttonText = ModTranslation.getString("ChunibyoStopText");
                    }
                    else
                    {
                        chunibyoButton.buttonText = ModTranslation.getString("ChunibyoText");
                    }
                    return PlayerControl.LocalPlayer.CanMove;
                },
                () => {
                    chunibyoButton.Timer = chunibyoButton.MaxTimer = Chunibyo.cooldown;
                },
                Chunibyo.getButtonSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.AbilityButton,
                KeyCode.F,
                true,
                Chunibyo.duration,
                () => {
                    chunibyoButton.Timer = chunibyoButton.MaxTimer = cooldown;
                    local.lightActive = false;
                    speedDown = false;
                    speedUp = false;
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
    public class speed
    {
        public static void Postfix(PlayerPhysics __instance)
        {
            if (Chunibyo.speedDown)
            {
                __instance.body.velocity /= Chunibyo.speed;
            }
            else if (Chunibyo.speedUp)
            {
                __instance.body.velocity *=Chunibyo.speed;
            }
        }
    }
}*/