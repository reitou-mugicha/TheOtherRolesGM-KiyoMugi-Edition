using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class AntiTeleport : ModifierBase<AntiTeleport>
    {
        public static Color color = Palette.Orange;
        public static Vector3 position = new Vector3();
        public static List<PlayerControl> candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new List<PlayerControl>();

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!player.hasModifier(ModifierType.AntiTeleport))
                        validPlayers.Add(player);
                }

                return validPlayers;
            }
        }
        public static string postfix
        {
            get
            {
                return ModTranslation.getString("antiTeleportPostfix");
            }
        }
        public static string fullName
        {
            get
            {
                return ModTranslation.getString("antiTeleport");
            }
        }

        public AntiTeleport()
        {
            ModType = modId = ModifierType.AntiTeleport;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }
        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<AntiTeleport>();
        }
    }
}