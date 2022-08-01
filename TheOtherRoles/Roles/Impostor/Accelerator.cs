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
        public static bool TakeoverKillcooldown {get{return CustomOptionHolder.acceleratorTakeoverKillCooldown.getBool();}}

        public Accelerator()
        {
            RoleType = roleId = RoleType.Accelerator;
            killCooldown = PlayerControl.GameOptions.killCooldown;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() 
        { 
            if (TakeoverKillcooldown)
            {
                PlayerControl.LocalPlayer.SetKillTimer(killCooldown);
            }
        }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) 
        { 
            if (killCooldown > 0)
            {
                killCooldown -= decreaseCooldown;
                PlayerControl.LocalPlayer.SetKillTimer(killCooldown);
            } else {
                PlayerControl.LocalPlayer.SetKillTimer(0f);
            }
            
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