/*using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hazel;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Student : RoleBase<Student>
    {
        public static CustomButton intelligenceButton;
        public static Color color = new Color32(248, 205, 70, byte.MaxValue);

        public Student()
        {
            RoleType = roleId = RoleType.Student;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate()
        {

            if (Sheriff.sheriff.isDead())
            {

                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.StudentPromotion, Hazel.SendOption.Reliable, -1);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

            }

        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Student>();
        }
    }
}*/