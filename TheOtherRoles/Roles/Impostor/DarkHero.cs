using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Utilities;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class DarkHero : RoleBase<DarkHero>
    {
        public static Color color = Palette.ImpostorRed;
        public static float cooldown { get { return CustomOptionHolder.darkHeroKillCooldown.getFloat(); } }

        public DarkHero()
        {
            RoleType = roleId = RoleType.DarkHero;
            PlayerControl.LocalPlayer.SetKillTimer(cooldown);
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() 
        { 
            if (PlayerControl.LocalPlayer.isRole(RoleType.DarkHero))
            {
                var sabo = MapUtilities.CachedShipStatus.Systems[SystemTypes.Electrical].CastFast<SwitchSystem>();
                FastDestroyableSingleton<HudManager>.Instance.KillButton.enabled = true;
                if (sabo != null && !sabo.IsActive)
                {
                    FastDestroyableSingleton<HudManager>.Instance.KillButton.enabled = false;
                }
            }
        }
        public override void OnKill(PlayerControl target) 
        { 
            PlayerControl.LocalPlayer.SetKillTimer(cooldown);
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {/*
            // Template
            buttonName = new CustomButton(
                () =>
                {},//押したときの挙動
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.aaaaa) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => { },//会議終了時どうなるか
                (RoleName).getButtonSprite(),//ボタンの絵
                new Vector3(-1.8f, -0.06f, 0),//ボタンの位置
                hm,
                hm.KillButton,
                KeyCode.F,//ショートカットキー
                true,//校歌を持つ(falseなら以下2つ不要)
                (),//継続時間
                () => { }//校歌終了時
            );
            buttonName.buttonText = ModTranslation.getString("ButtonText");//ボタンの文字
            buttonName.effectCancellable = true;//校歌を途中で停めれるか*/
        }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<DarkHero>();
        }
    }
}