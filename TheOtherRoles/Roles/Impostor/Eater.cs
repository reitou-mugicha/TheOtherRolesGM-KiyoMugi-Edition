using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Modules;   
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;
using Hazel;
using System;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Eater : RoleBase<Eater>
    {
        public static Color color = Palette.ImpostorRed;
        public static CustomButton eatButton;
        public static float cooldown { get {return CustomOptionHolder.eaterEatCooldown.getFloat(); } }
        public static float eatTime { get {return CustomOptionHolder.eaterEatTime.getFloat(); } }
        public static PlayerControl currentTarget;

        public Eater()
        {
            RoleType = roleId = RoleType.Eater;
            currentTarget = null;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() 
        { 
            if (player == PlayerControl.LocalPlayer)
            {
                if (player.isAlive())
                {
                    currentTarget = setTarget();
                    setPlayerOutline(currentTarget, Fox.color);
                }
            }
        }
        public override void OnKill(PlayerControl target) 
        { 
            
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.VultureButton.png", 115f);
            return buttonSprite;
        }


        public static void MakeButtons(HudManager hm)
        {
            eatButton = new CustomButton(
                () =>
                {
                    
                    MurderAttemptResult murderAttemptResult = Helpers.checkMuderAttempt(PlayerControl.LocalPlayer, currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.PerformKill)
                    {
                        byte targetId = currentTarget.PlayerId;

                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EaterEat, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(PlayerControl.LocalPlayer.Data.PlayerId);
                        killWriter.Write(targetId);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.EaterEat(PlayerControl.LocalPlayer.Data.PlayerId, targetId);
                    }
                },//押したときの挙動
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Eater) && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => 
                { return currentTarget && PlayerControl.LocalPlayer.CanMove; },
                () => { 
                    eatButton.Timer = cooldown;
                },
                getButtonSprite(),//ボタンの絵
                new Vector3(0, 1, 0),//ボタンの位置
                hm,
                hm.KillButton,
                KeyCode.Q,//ショートカットキー
                true,//校歌を持つ(falseなら以下2つ不要)
                eatTime,//継続時間
                () => 
                { 
                    bool canEat = false;
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.MaxReportDistance, Constants.PlayersOnlyMask))
                        if (collider2D.tag == "DeadBody")
                            canEat = true;
                    
                    if(canEat)
                    {
                        foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), PlayerControl.LocalPlayer.MaxReportDistance, Constants.PlayersOnlyMask))
                        {
                            if (collider2D.tag == "DeadBody")
                            {
                                DeadBody component = collider2D.GetComponent<DeadBody>();
                                if (component && !component.Reported)
                                {
                                    Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                    Vector2 truePosition2 = component.TruePosition;
                                    if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false))
                                    {
                                        GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);

                                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CleanBody, Hazel.SendOption.Reliable, -1);
                                        writer.Write(playerInfo.PlayerId);
                                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                                        RPCProcedure.cleanBody(playerInfo.PlayerId);

                                        eatButton.Timer = cooldown;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    eatButton.Timer = cooldown;
                }//校歌終了時
            );
            eatButton.buttonText = ModTranslation.getString("EatText");//ボタンの文字
            eatButton.effectCancellable = false;
        }
        public static void SetButtonCooldowns() 
        { 
            eatButton.Timer = eatButton.MaxTimer = cooldown;
        }

        public static void Clear()
        {
            players = new List<Eater>();
        }
    }
}