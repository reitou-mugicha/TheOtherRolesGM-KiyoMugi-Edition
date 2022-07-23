using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Prophet : RoleBase<Prophet>
    {
        public static Color color = new Color32(238, 130, 238, byte.MaxValue);
        public static int Num {get{return Mathf.RoundToInt(CustomOptionHolder.prophetProphecyNum.getFloat());}}
        public static int remainingNum;

        public Prophet()
        {
            RoleType = roleId = RoleType.Prophet;
            remainingNum = Num;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

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