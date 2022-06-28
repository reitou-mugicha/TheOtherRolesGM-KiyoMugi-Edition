/*
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Modules;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Timer : RoleBase<Timer>
    {
        public static Color color = new Color32(230, 230, 250, byte.MaxValue);
        public static CustomButton timerButton;
        public static bool resetTimer {get{return CustomOptionHolder.timerMeetingEndTimeReset.getBool();}}

        public Timer()
        {
            RoleType = roleId = RoleType.Timer;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate()
        {
            new LateTask(() =>
            {
                timerButton.Timer += 1;
            }, 1, "timerTask");
        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            timerButton = new CustomButton(
                () =>
                {},//押したときの挙動
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Timer) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return false; },
                () =>
                {
                    if(resetTimer)
                    {
                        timerButton.Timer = 0;
                    }
                },//会議終了時どうなるか
                getButtonSprite(),//ボタンの絵
                new Vector3(-1.8f, -0.06f, 0),//ボタンの位置
                hm,
                hm.AbilityButton,
                KeyCode.None
            );
            timerButton.buttonText = ModTranslation.getString("timerText");//ボタンの文字
        }
        public static void SetButtonCooldowns()
        {
            timerButton.Timer = timerButton.MaxTimer = 0;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TimerButton.png", 115f);
            return buttonSprite;
        }

        public static void Clear()
        {
            players = new List<Timer>();
        }
    }
}*/