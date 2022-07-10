using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Modules;
using System;
using Hazel;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class TimeReviver : RoleBase<TimeReviver>
    {
        public static Color color = new Color32(173, 216, 230, byte.MaxValue);
        public static CustomButton timeCutoffButton;
        public static bool killReport { get { return CustomOptionHolder.timeReviverDeathReport.getBool(); } }
        public static float cutoffTime { get {return CustomOptionHolder.timeReviverTimeCutoffDuration.getFloat(); } }
        public static float cutoffCooldown { get {return CustomOptionHolder.timeReviverTimeCutoffCooldown.getFloat(); } }
        public static bool isCutoff = false;

        public TimeReviver()
        {
            RoleType = roleId = RoleType.TimeReviver;
            isCutoff = false;
        }

        public override void OnMeetingStart() 
        { 
            isCutoff = false;
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) 
        { 
            /*if(killReport)
            {
                PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement
                Helpers.handleVampireBiteOnBodyReport(); // Manually call Vampire handling, since the CmdReportDeadBody Prefix won't be called
                RPCProcedure.uncheckedCmdReportDeadBody(getReviver().PlayerId, Byte.MinValue);

                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedCmdReportDeadBody, Hazel.SendOption.Reliable, -1);
                writer.Write(getReviver().PlayerId);
                writer.Write(Byte.MaxValue);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }*/
        }

        public static PlayerControl getReviver()
        {
            foreach (PlayerControl tr in PlayerControl.AllPlayerControls)
            {
                if(tr.isRole(RoleType.TimeReviver))
                {
                    return tr;
                }
            }

            return null;
        }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            timeCutoffButton = new CustomButton(
                () =>
                {
                    isCutoff = true;
                    
                    foreach(PlayerControl timeCutoff in PlayerControl.AllPlayerControls)
                    {
                        if(timeCutoff.isDead()) return;
                        if(!timeCutoff.isRole(RoleType.TimeReviver) )
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TrapperTrap, Hazel.SendOption.Reliable, -1);
                            writer.Write(timeCutoff.PlayerId);
                            writer.Write((byte)cutoffTime);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.TrapperTrap(timeCutoff.PlayerId, (byte)cutoffTime);
                        }else Helpers.showFlash(new Color(176, 196, 222), cutoffTime);
                    }
                },//押したときの挙動
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.TimeReviver) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove; },
                () => { },//会議終了時どうなるか
                getButtonSprite(),//ボタンの絵
                new Vector3(-1.8f, -0.06f, 0),//ボタンの位置
                hm,
                hm.AbilityButton,
                KeyCode.T,//ショートカットキー
                true,//校歌を持つ(falseなら以下2つ不要)
                cutoffTime,//継続時間
                () => 
                { 
                    timeCutoffButton.Timer = timeCutoffButton.MaxTimer = cutoffCooldown;
                    isCutoff = false;
                }//校歌終了時
            );
            timeCutoffButton.buttonText = ModTranslation.getString("TimeCutoffText");//ボタンの文字
            timeCutoffButton.effectCancellable = true;
        }
        public static void SetButtonCooldowns() 
        { 
            timeCutoffButton.Timer = timeCutoffButton.MaxTimer = cutoffCooldown;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TimeReviverButton.png", 115f);
            return buttonSprite;
        }

        public static void Clear()
        {
            players = new List<TimeReviver>();
        }
    }
}