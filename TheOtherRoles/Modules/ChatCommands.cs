using System;
<<<<<<< HEAD
using HarmonyLib;
using System.Linq;
using TheOtherRoles.Utilities;
=======
using System.Linq;
using HarmonyLib;
>>>>>>> master
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Modules
{
    [HarmonyPatch]
    public static class ChatCommands
    {
<<<<<<< HEAD
=======

>>>>>>> master
        [HarmonyPatch(typeof(ChatController), nameof(ChatController.SendChat))]
        private static class SendChatPatch
        {
            static bool Prefix(ChatController __instance)
            {
                string text = __instance.TextArea.text;
                bool handled = false;
                if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started)
                {
                    if (text.ToLower().StartsWith("/kick "))
                    {
<<<<<<< HEAD
                        string playerName = text.Substring(6);
=======
                        string playerName = text[6..];
>>>>>>> master
                        PlayerControl target = PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.Data.PlayerName.Equals(playerName));
                        if (target != null && AmongUsClient.Instance != null && AmongUsClient.Instance.CanBan())
                        {
                            var client = AmongUsClient.Instance.GetClient(target.OwnerId);
                            if (client != null)
                            {
                                AmongUsClient.Instance.KickPlayer(client.Id, false);
                                handled = true;
                            }
                        }
                    }
                    else if (text.ToLower().StartsWith("/ban "))
                    {
<<<<<<< HEAD
                        string playerName = text.Substring(5);
=======
                        string playerName = text[5..];
>>>>>>> master
                        PlayerControl target = PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.Data.PlayerName.Equals(playerName));
                        if (target != null && AmongUsClient.Instance != null && AmongUsClient.Instance.CanBan())
                        {
                            var client = AmongUsClient.Instance.GetClient(target.OwnerId);
                            if (client != null)
                            {
                                AmongUsClient.Instance.KickPlayer(client.Id, true);
                                handled = true;
                            }
                        }
                    }
                }

                if (AmongUsClient.Instance.GameMode == GameModes.FreePlay)
                {
                    if (text.ToLower().Equals("/murder"))
                    {
                        PlayerControl.LocalPlayer.Exiled();
                        HudManager.Instance.KillOverlay.ShowKillAnimation(PlayerControl.LocalPlayer.Data, PlayerControl.LocalPlayer.Data);
                        handled = true;
                    }
                    else if (text.ToLower().StartsWith("/color "))
                    {
                        handled = true;
<<<<<<< HEAD
                        int col;
                        if (!Int32.TryParse(text.Substring(7), out col))
=======
                        if (!Int32.TryParse(text[7..], out int col))
>>>>>>> master
                        {
                            __instance.AddChat(PlayerControl.LocalPlayer, "Unable to parse color id\nUsage: /color {id}");
                        }
                        col = Math.Clamp(col, 0, Palette.PlayerColors.Length - 1);
                        PlayerControl.LocalPlayer.SetColor(col);
<<<<<<< HEAD
                        __instance.AddChat(PlayerControl.LocalPlayer, "Changed color succesfully"); ;
=======
                        __instance.AddChat(PlayerControl.LocalPlayer, "Changed color successfully"); ;
>>>>>>> master
                    }
                }

                if (text.ToLower().StartsWith("/tp ") && PlayerControl.LocalPlayer.Data.IsDead)
                {
<<<<<<< HEAD
                    string playerName = text.Substring(4).ToLower();
=======
                    string playerName = text[4..].ToLower();
>>>>>>> master
                    PlayerControl target = PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.Data.PlayerName.ToLower().Equals(playerName));
                    if (target != null)
                    {
                        PlayerControl.LocalPlayer.transform.position = target.transform.position;
                        handled = true;
                    }
                }

                if (handled)
                {
                    __instance.TextArea.Clear();
                    __instance.quickChatMenu.ResetGlyphs();
                }
                return !handled;
            }
        }

        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public static class EnableChat
        {
            public static void Postfix(HudManager __instance)
            {
                if (__instance?.Chat?.isActiveAndEnabled == false && (AmongUsClient.Instance?.GameMode == GameModes.FreePlay || (PlayerControl.LocalPlayer.isLovers() && Lovers.enableChat)))
                    __instance?.Chat?.SetVisible(true);
            }
        }

        [HarmonyPatch(typeof(ChatBubble), nameof(ChatBubble.SetName))]
        public static class SetBubbleName
        {
            public static void Postfix(ChatBubble __instance, [HarmonyArgument(0)] string playerName)
            {
                PlayerControl sourcePlayer = PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.Data.PlayerName.Equals(playerName));
<<<<<<< HEAD
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data.Role.IsImpostor && (Spy.spy != null && sourcePlayer.PlayerId == Spy.spy.PlayerId || Sidekick.sidekick != null && Sidekick.wasTeamRed && sourcePlayer.PlayerId == Sidekick.sidekick.PlayerId || Jackal.jackal != null && Jackal.wasTeamRed && sourcePlayer.PlayerId == Jackal.jackal.PlayerId) && __instance != null) __instance.NameText.color = Palette.ImpostorRed;
=======
                if (PlayerControl.LocalPlayer.isImpostor() && Jammer.isJammerAlive())  __instance.NameText.color = Palette.White;
                if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data.Role.IsImpostor && ((Spy.spy != null && sourcePlayer.PlayerId == Spy.spy.PlayerId) || (Sidekick.sidekick != null && Sidekick.wasTeamRed && sourcePlayer.PlayerId == Sidekick.sidekick.PlayerId) || (Jackal.jackal != null && Jackal.wasTeamRed && sourcePlayer.PlayerId == Jackal.jackal.PlayerId)) && __instance != null) __instance.NameText.color = Palette.ImpostorRed;
>>>>>>> master
            }
        }

        [HarmonyPatch(typeof(ChatController), nameof(ChatController.AddChat))]
        public static class AddChatPatch
        {
            public static bool Prefix(ChatController __instance, [HarmonyArgument(0)] PlayerControl sourcePlayer)
            {
<<<<<<< HEAD
                if (__instance != FastDestroyableSingleton<HudManager>.Instance.Chat)
=======
                if (__instance != DestroyableSingleton<HudManager>.Instance.Chat)
>>>>>>> master
                    return true;
                PlayerControl localPlayer = PlayerControl.LocalPlayer;
                return localPlayer == null ||
                    MeetingHud.Instance != null || LobbyBehaviour.Instance != null ||
                    localPlayer.isDead() || localPlayer.PlayerId == sourcePlayer.PlayerId ||
                    (Lovers.enableChat && localPlayer.getPartner() == sourcePlayer);
            }
        }
    }
}