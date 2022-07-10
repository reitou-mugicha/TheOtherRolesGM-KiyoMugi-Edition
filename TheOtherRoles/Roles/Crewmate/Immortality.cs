using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Modules;
using Hazel;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Immortality : RoleBase<Immortality>
    {
        public static Color color = new Color32(255, 215, 0, byte.MaxValue);
        public static float time {get{return CustomOptionHolder.immortalityMeetingStartSuicideTime.getFloat();}}
        public static bool isDeaded;

        public Immortality()
        {
            RoleType = roleId = RoleType.Immortality;
            isDeaded = false;
        }

        public override void OnMeetingStart() 
        { 
            if(isDeaded)
            {
                new LateTask(() =>
                {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.uncheckedMurderPlayer(PlayerControl.LocalPlayer.PlayerId, PlayerControl.LocalPlayer.PlayerId, 1);
                }, time, "murder");
            }
        }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) 
        { 
            if(!isDeaded)
            {
                isDeaded = true; //FlagをON

                //復活 => 死体消し
                player.Revive();
                DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
                for (int i = 0; i < array.Length; i++)
                {
                    
                    if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == player.PlayerId)
                    {
                        array[i].gameObject.active = false;
                    }
                }
            }
        }
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
            players = new List<Immortality>();
        }
    }
}