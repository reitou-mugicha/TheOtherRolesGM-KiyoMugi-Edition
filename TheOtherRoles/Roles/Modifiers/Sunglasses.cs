using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Modules;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Sunglasses : ModifierBase<Sunglasses>
    {
        public static Color color = new Color32(119, 136, 153, byte.MaxValue);
        public static int vision { get { return Mathf.RoundToInt(CustomOptionHolder.sunglass.getFloat()); } }

        public static List<PlayerControl> candidates
        {
            get
            {
                List<PlayerControl> validPlayers = new List<PlayerControl>();

                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!player.hasModifier(ModifierType.Sunglasses))
                        validPlayers.Add(player);
                }

                return validPlayers;
            }
        }

        public static string postfix
        {
            get
            {
                return ModTranslation.getString("sunglassesPostfix");
            }
        }
        public static string fullName
        {
            get
            {
                return ModTranslation.getString("sunglasses");
            }
        }

        public Sunglasses()
        {
            ModType = modId = ModifierType.Sunglasses;
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
            players = new List<Sunglasses>();
        }
    }
}