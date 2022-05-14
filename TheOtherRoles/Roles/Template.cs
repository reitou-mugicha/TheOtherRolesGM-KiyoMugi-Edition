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
    public class Template : RoleBase<Template>
    {
        public static Color color = Palette.CrewmateBlue;

        public Template()
        {
            RoleType = roleId = RoleType.NoRole;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {/*
            // Template
            buttonName = new CustomButton(
                () =>
                {//押したときの挙動},
                () => { //出現条件 },
                () => { //使ったらどうなるか },
                () => { //会議終了時どうなるか },
                (RoleName).getButtonSprite(),//ボタンの絵
                new Vector3(-1.8f, -0.06f, 0),//ボタンの位置
                hm,
                hm.KillButton,
                KeyCode.F,//ショートカットキー
                true,//校歌を持つ(falseなら以下2つ不要)
                (//継続時間),
                () => { //校歌終了時 }
            );
            buttonName.buttonText = ModTranslation.getString("ButtonText");//ボタンの文字
            buttonName.effectCancellable = true;//校歌を途中で停めれるか*/
        }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Template>();
        }
    }
}