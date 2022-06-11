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
    public class Accelerator : RoleBase<Accelerator>
    {
        public static Color color = Palette.ImpostorRed;
        public static float decreaseCooldown {get{return CustomOptionHolder.acceleratorDecreaseCooldown.getFloat();}}
        public static float killCooldown;

        public Accelerator()
        {
            RoleType = roleId = RoleType.Accelerator;
            killCooldown = PlayerControl.GameOptions.killCooldown;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) 
        { 
            killCooldown -= decreaseCooldown;
            if(killCooldown >= 0)
                PlayerControl.LocalPlayer.SetKillTimer(killCooldown);
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }

        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<Accelerator>();
        }
    }
}