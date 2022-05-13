using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Opportunist : ModifierBase<Opportunist>
    {
        public static Color color = new Color32(0, 255, 00, byte.MaxValue);

        public static List<PlayerControl> candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new List<PlayerControl>();

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!player.hasModifier(ModifierType.Opportunist))
                        validPlayers.Add(player);
                }

                return validPlayers;
            }
        }

        public Opportunist()
        {
            ModType = modId = ModifierType.Opportunist;
        }

        public static void Clear()
        {
            players = new List<Opportunist>();
        }

        public static string postfix
        {
            get
            {
                return ModTranslation.getString("opportunistPostfix");
            }
        }
        public static string fullName
        {
            get
            {
                return ModTranslation.getString("opportunist");
            }
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }
    }
}