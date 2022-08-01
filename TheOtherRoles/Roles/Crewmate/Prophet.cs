using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using Hazel;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Prophet : RoleBase<Prophet>
    {
        public static Color color = new Color32(238, 130, 238, byte.MaxValue);
        public static int Num {get{return Mathf.RoundToInt(CustomOptionHolder.prophetProphecyNum.getFloat());}}
        public static int remainingNum;
        public static bool isProphecy;

        public Prophet()
        {
            RoleType = roleId = RoleType.Prophet;
            remainingNum = Num;
            isProphecy = false;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() 
        { 
            if(isProphecy && remainingNum <= 0)
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.RPCExiled, Hazel.SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.RPCExiled(PlayerControl.LocalPlayer.PlayerId);
            }
        }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static int remainingNums()
        {
            int remainingShots = remainingNum;
            return remainingShots;
        }

        private static Sprite buttonSprite;
        public static Sprite getTargetSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ProphetTargetButton.png", 115f);
            return buttonSprite;
        }

        public static void MakeButtons(HudManager hm)
        {
        }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Prophet>();
        }
    }
}