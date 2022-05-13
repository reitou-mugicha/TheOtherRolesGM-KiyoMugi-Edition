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
    public class Watcher : ModifierBase<Watcher>
    {
        public static Color color = Palette.Purple;

        public static List<PlayerControl> candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new List<PlayerControl>();

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!player.hasModifier(ModifierType.Watcher))
                        validPlayers.Add(player);
                }

                return validPlayers;
            }
        }

        public static string postfix
        {
            get
            {
                return ModTranslation.getString("watcherPostfix");
            }
        }
        public static string fullName
        {
            get
            {
                return ModTranslation.getString("watcher");
            }
        }

        public Watcher()
        {
            ModType = modId = ModifierType.Watcher;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void clearAndReload()
        {
            players = new List<Watcher>();
        }
    }
}