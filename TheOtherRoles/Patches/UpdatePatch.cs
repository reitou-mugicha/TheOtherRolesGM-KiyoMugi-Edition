using HarmonyLib;
using TheOtherRoles.Utilities;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;
using TheOtherRoles.Objects;
using System.Collections.Generic;
using TheOtherRoles.Modules;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    class HudManagerUpdatePatch
    {
        static void resetNameTagsAndColors()
        {
            var localPlayer = PlayerControl.LocalPlayer;
            var myData = localPlayer.Data;
            var amImpostor = myData.Role.IsImpostor;
            var morphTimerNotUp = Morphling.morphTimer > 0f;
            var morphTargetNotNull = Morphling.morphTarget != null;

            var dict = new Dictionary<byte, (string name, Color color)>();

            foreach (var data in GameData.Instance.AllPlayers.GetFastEnumerator())
            {
                var player = data.Object;
                string text = data.PlayerName;
                Color color;
                if (player)
                {
                    var playerName = text;
                    if (morphTimerNotUp && morphTargetNotNull && Morphling.morphling == player) playerName = Morphling.morphTarget.Data.PlayerName;
                    var nameText = player.cosmetics.nameText;

                    nameText.text = Helpers.hidePlayerName(localPlayer, player) ? "" : playerName;
                    nameText.color = color = amImpostor && data.Role.IsImpostor ? Palette.ImpostorRed : Color.white;
                }
                else
                    color = Color.white;

                dict.Add(data.PlayerId, (text, color));
            }

            if (MeetingHud.Instance != null)
                foreach (PlayerVoteArea playerVoteArea in MeetingHud.Instance.playerStates)
                {
                    var data = dict[playerVoteArea.TargetPlayerId];
                    var text = playerVoteArea.NameText;
                    text.text = data.name;
                    text.color = data.color;
                }
        }

        static void setPlayerNameColor(PlayerControl p, Color color)
        {
            p.cosmetics.nameText.color = color;
            if (MeetingHud.Instance != null)
                foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                    if (player.NameText != null && p.PlayerId == player.TargetPlayerId)
                        player.NameText.color = color;
        }

        static void setNameColors()
        {
            if (PlayerControl.LocalPlayer.isRole(RoleType.Jester))
                setPlayerNameColor(PlayerControl.LocalPlayer, Jester.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Engineer))
                setPlayerNameColor(PlayerControl.LocalPlayer, Engineer.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Sheriff))
                setPlayerNameColor(PlayerControl.LocalPlayer, Sheriff.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Lighter))
                setPlayerNameColor(PlayerControl.LocalPlayer, Lighter.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Detective))
                setPlayerNameColor(PlayerControl.LocalPlayer, Detective.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.TimeMaster))
                setPlayerNameColor(PlayerControl.LocalPlayer, TimeMaster.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Medic))
                setPlayerNameColor(PlayerControl.LocalPlayer, Medic.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Shifter))
                setPlayerNameColor(PlayerControl.LocalPlayer, Shifter.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Swapper))
                setPlayerNameColor(PlayerControl.LocalPlayer, Swapper.swapper.Data.Role.IsImpostor ? Palette.ImpostorRed : Swapper.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Seer))
                setPlayerNameColor(PlayerControl.LocalPlayer, Seer.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Hacker))
                setPlayerNameColor(PlayerControl.LocalPlayer, Hacker.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Tracker))
                setPlayerNameColor(PlayerControl.LocalPlayer, Tracker.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Snitch))
                setPlayerNameColor(PlayerControl.LocalPlayer, Snitch.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Portalmaker))
                setPlayerNameColor(PlayerControl.LocalPlayer, Portalmaker.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Spy))
                setPlayerNameColor(PlayerControl.LocalPlayer, Spy.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.SecurityGuard))
                setPlayerNameColor(PlayerControl.LocalPlayer, SecurityGuard.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Arsonist))
                setPlayerNameColor(PlayerControl.LocalPlayer, Arsonist.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.NiceGuesser))
                setPlayerNameColor(PlayerControl.LocalPlayer, Guesser.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.EvilGuesser))
                setPlayerNameColor(PlayerControl.LocalPlayer, Palette.ImpostorRed);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Mayor))
                setPlayerNameColor(PlayerControl.LocalPlayer, Mayor.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Boss))
                setPlayerNameColor(PlayerControl.LocalPlayer, Boss.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Staff))
                setPlayerNameColor(PlayerControl.LocalPlayer, Staff.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Gun))
                setPlayerNameColor(PlayerControl.LocalPlayer, Gun.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Bait))
                setPlayerNameColor(PlayerControl.LocalPlayer, Bait.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Vulture))
                setPlayerNameColor(PlayerControl.LocalPlayer, Vulture.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Medium))
                setPlayerNameColor(PlayerControl.LocalPlayer, Medium.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Lawyer))
                setPlayerNameColor(PlayerControl.LocalPlayer, Lawyer.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Pursuer))
                setPlayerNameColor(PlayerControl.LocalPlayer, Pursuer.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.PlagueDoctor))
                setPlayerNameColor(PlayerControl.LocalPlayer, PlagueDoctor.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Fox))
                setPlayerNameColor(PlayerControl.LocalPlayer, Fox.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Immoralist))
                setPlayerNameColor(PlayerControl.LocalPlayer, Immoralist.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.FortuneTeller) && (FortuneTeller.isCompletedNumTasks(PlayerControl.LocalPlayer) || PlayerControl.LocalPlayer.Data.IsDead))
                setPlayerNameColor(PlayerControl.LocalPlayer, FortuneTeller.color);
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Jackal))
            {
                // Jackal can see his sidekick
                setPlayerNameColor(PlayerControl.LocalPlayer, Jackal.color);
                if (Sidekick.sidekick != null)
                    setPlayerNameColor(Sidekick.sidekick, Jackal.color);
                if (Jackal.fakeSidekick != null)
                    setPlayerNameColor(Jackal.fakeSidekick, Jackal.color);
            }
            if (PlayerControl.LocalPlayer.hasModifier(ModifierType.Madmate))
            {
                setPlayerNameColor(PlayerControl.LocalPlayer, Madmate.color);

                if (Madmate.knowsImpostors(PlayerControl.LocalPlayer))
                    foreach (var p in PlayerControl.AllPlayerControls)
                        if (p.isImpostor() || p.isRole(RoleType.Spy))
                            setPlayerNameColor(p, Palette.ImpostorRed);
                        else if (PlayerControl.LocalPlayer.hasModifier(ModifierType.Opportunist))
                            setPlayerNameColor(PlayerControl.LocalPlayer, Opportunist.color);
            }

            else if (PlayerControl.LocalPlayer.hasModifier(ModifierType.CreatedMadmate))
            {
                setPlayerNameColor(PlayerControl.LocalPlayer, Madmate.color);

                if (CreatedMadmate.knowsImpostors(PlayerControl.LocalPlayer))
                    foreach (var p in PlayerControl.AllPlayerControls)
                        if (p.isImpostor() || p.isRole(RoleType.Spy))
                            setPlayerNameColor(p, Palette.ImpostorRed);
            }

            else if (PlayerControl.LocalPlayer.hasModifier(ModifierType.AntiTeleport) && PlayerControl.LocalPlayer.Data.IsDead)
                setPlayerNameColor(PlayerControl.LocalPlayer, AntiTeleport.color);
            else if (PlayerControl.LocalPlayer.hasModifier(ModifierType.Opportunist) && PlayerControl.LocalPlayer.Data.IsDead)
                setPlayerNameColor(PlayerControl.LocalPlayer, Opportunist.color);
            else if (PlayerControl.LocalPlayer.hasModifier(ModifierType.Sunglasses) && PlayerControl.LocalPlayer.Data.IsDead)
                setPlayerNameColor(PlayerControl.LocalPlayer, Sunglasses.color);
            else if (PlayerControl.LocalPlayer.hasModifier(ModifierType.Watcher) && PlayerControl.LocalPlayer.Data.IsDead)
                setPlayerNameColor(PlayerControl.LocalPlayer, Watcher.color);

            if (GM.gm != null)
                setPlayerNameColor(GM.gm, GM.color);

            // No else if here, as a Lover of team Jackal needs the colors
            if (PlayerControl.LocalPlayer.isRole(RoleType.Sidekick))
            {
                // Sidekick can see the jackal
                setPlayerNameColor(Sidekick.sidekick, Sidekick.color);
                if (Jackal.jackal != null)
                    setPlayerNameColor(Jackal.jackal, Jackal.color);
            }

            // No else if here, as the Impostors need the Spy name to be colored
            if (Spy.spy != null && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
                setPlayerNameColor(Spy.spy, Spy.color);
            if (Sidekick.sidekick != null && Sidekick.wasTeamRed && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
                setPlayerNameColor(Sidekick.sidekick, Spy.color);
            if (Jackal.jackal != null && Jackal.wasTeamRed && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
                setPlayerNameColor(Jackal.jackal, Spy.color);

            if (Immoralist.exists && PlayerControl.LocalPlayer.isRole(RoleType.Fox))
                foreach (var immoralist in Immoralist.allPlayers)
                    setPlayerNameColor(immoralist, Immoralist.color);

            if (PlayerControl.LocalPlayer.isRole(RoleType.Immoralist))
                foreach (var fox in Fox.allPlayers)
                    setPlayerNameColor(fox, Fox.color);

            // Crewmate roles with no changes: Mini
            // Impostor roles with no changes: Morphling, Camouflager, Vampire, Godfather, Eraser, Janitor, Cleaner, Warlock, BountyHunter,  Witch and Mafioso
        }

        static void setNameTags()
        {
            // Mafia
            if (PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            {
                foreach (PlayerControl player in PlayerControl.AllPlayerControls.GetFastEnumerator())
                {
                    if (player.cosmetics.nameText.text == "") continue;
                    if (Godfather.godfather != null && Godfather.godfather == player)
                        player.cosmetics.nameText.text = player.Data.PlayerName + $" ({ModTranslation.getString("mafiaG")})";
                    else if (Mafioso.mafioso != null && Mafioso.mafioso == player)
                        player.cosmetics.nameText.text = player.Data.PlayerName + $" ({ModTranslation.getString("mafiaM")})";
                    else if (Janitor.janitor != null && Janitor.janitor == player)
                        player.cosmetics.nameText.text = player.Data.PlayerName + $" ({ModTranslation.getString("mafiaJ")})";
                }
                if (MeetingHud.Instance != null)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                        if (Godfather.godfather != null && Godfather.godfather.PlayerId == player.TargetPlayerId)
                            player.NameText.text = Godfather.godfather.Data.PlayerName + $" ({ModTranslation.getString("mafiaG")})";
                        else if (Mafioso.mafioso != null && Mafioso.mafioso.PlayerId == player.TargetPlayerId)
                            player.NameText.text = Mafioso.mafioso.Data.PlayerName + $" ({ModTranslation.getString("mafiaM")})";
                        else if (Janitor.janitor != null && Janitor.janitor.PlayerId == player.TargetPlayerId)
                            player.NameText.text = Janitor.janitor.Data.PlayerName + $" ({ModTranslation.getString("mafiaJ")})";
            }

            // Lovers
            if (PlayerControl.LocalPlayer.isLovers() && PlayerControl.LocalPlayer.isAlive())
            {
                string suffix = Lovers.getIcon(PlayerControl.LocalPlayer);
                var lover1 = PlayerControl.LocalPlayer;
                var lover2 = PlayerControl.LocalPlayer.getPartner();

                lover1.cosmetics.nameText.text += suffix;
                if (!Helpers.hidePlayerName(lover2))
                    lover2.cosmetics.nameText.text += suffix;

                if (Helpers.ShowMeetingText)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                        if (lover1.PlayerId == player.TargetPlayerId || lover2.PlayerId == player.TargetPlayerId)
                            player.NameText.text += suffix;
            }

            // Lawyer
            bool localIsLawyer = Lawyer.lawyer != null && Lawyer.target != null && Lawyer.lawyer == PlayerControl.LocalPlayer;
            bool localIsKnowingTarget = Lawyer.lawyer != null && Lawyer.target != null && Lawyer.targetKnows && Lawyer.target == PlayerControl.LocalPlayer;
            if (localIsLawyer || (localIsKnowingTarget && !Lawyer.lawyer.Data.IsDead))
            {
                string suffix = Helpers.cs(Lawyer.color, " §");
                if (!Helpers.hidePlayerName(Lawyer.target))
                    Lawyer.target.cosmetics.nameText.text += suffix;

                if (Helpers.ShowMeetingText)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                        if (player.TargetPlayerId == Lawyer.target.PlayerId)
                            player.NameText.text += suffix;
            }

            // Hacker and Detective
            if (PlayerControl.LocalPlayer != null && MapOptions.showLighterDarker)
                if (Helpers.ShowMeetingText)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                    {
                        var target = Helpers.playerById(player.TargetPlayerId);
                        if (target != null) player.NameText.text += $" ({(Helpers.isLighterColor(target.Data.DefaultOutfit.ColorId) ? ModTranslation.getString("detectiveLightLabel") : ModTranslation.getString("detectiveDarkLabel"))})";
                    }
        }

        static void updateShielded()
        {
            if (Medic.shielded == null) return;

            if (Medic.shielded.Data.IsDead || Medic.medic == null || Medic.medic.Data.IsDead)
                Medic.shielded = null;
        }

        static void timerUpdate()
        {
            var dt = Time.deltaTime;
            Hacker.hackerTimer -= dt;
            Trickster.lightsOutTimer -= dt;
            Tracker.corpsesTrackingTimer -= dt;
        }

        public static void miniUpdate()
        {
            foreach (var mini in Mini.players)
            {
                _miniUpdate(mini);
            }
        }

        static void underTakerUpdate()
        {

            if (UnderTaker.underTaker == null)
                return;

            if (UnderTaker.dragginBody)
            {
                DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
                for (int i = 0; i < array.Length; i++)
                {
                    if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == UnderTaker.bodyId)
                    {
                        var currentPosition = UnderTaker.underTaker.GetTruePosition();
                        var velocity = UnderTaker.underTaker.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
                        var newPos = ((Vector2)UnderTaker.underTaker.GetTruePosition()) - (velocity / 3) + new Vector2(0.15f, 0.25f) + array[i].myCollider.offset;
                        if (!PhysicsHelpers.AnythingBetween(
                            currentPosition,
                            newPos,
                            Constants.ShipAndObjectsMask,
                            false
                        ))
                        {
                            if (PlayerControl.GameOptions.MapId == 5)
                            {
                                array[i].transform.position = newPos;
                                array[i].transform.position += new Vector3(0, 0, -0.5f);
                            }
                            else
                            {
                                array[i].transform.position = newPos;
                            }
                        }
                    }
                }
            }
        }
        public static void _miniUpdate(Mini mini)
        {
            if (Camouflager.camouflageTimer > 0f) return;

            float growingProgress = mini.growingProgress();
            float scale = growingProgress * 0.35f + 0.35f;
            string suffix = "";
            if (growingProgress != 1f)
                suffix = " <color=#FAD934FF>(" + Mathf.FloorToInt(growingProgress * 18) + ")</color>";

            if (!Helpers.hidePlayerName(mini.player))
                mini.player.cosmetics.nameText.text += suffix;

            if (MeetingHud.Instance != null)
            {
                foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                    if (player.NameText != null && mini.player.PlayerId == player.TargetPlayerId)
                        player.NameText.text += suffix;
            }

            if (Morphling.morphling != null && Morphling.morphTarget == mini.player && Morphling.morphTimer > 0f && !Helpers.hidePlayerName(Morphling.morphling))
                Morphling.morphling.cosmetics.nameText.text += suffix;
        }

        static void updateImpostorKillButton(HudManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Data.Role.IsImpostor || MeetingHud.Instance) return;
            bool enabled = Helpers.ShowButtons;
            if (PlayerControl.LocalPlayer.isRole(RoleType.Vampire))
                enabled &= false;
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Mafioso) && !Mafioso.canKill)
                enabled &= false;
            else if (PlayerControl.LocalPlayer.isRole(RoleType.Janitor))
                enabled &= false;
            else if (UnderTaker.underTaker != null && UnderTaker.dragginBody && PlayerControl.LocalPlayer == UnderTaker.underTaker)
                enabled = false;

            if (enabled) __instance.KillButton.Show();
            else __instance.KillButton.Hide();
        }

        static void camouflageAndMorphActions()
        {
            float oldCamouflageTimer = Camouflager.camouflageTimer;
            float oldMorphTimer = Morphling.morphTimer;

            Camouflager.camouflageTimer -= Time.deltaTime;
            Morphling.morphTimer -= Time.deltaTime;

            // Everyone but morphling reset
            if (oldCamouflageTimer > 0f && Camouflager.camouflageTimer <= 0f)
                Camouflager.resetCamouflage();

            // Morphling reset
            if (oldMorphTimer > 0f && Morphling.morphTimer <= 0f)
                Morphling.resetMorph();
        }

        static void Postfix(HudManager __instance)
        {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;

            CustomButton.HudUpdate();
            resetNameTagsAndColors();
            setNameColors();
            updateShielded();
            setNameTags();

            // Camouflager and Morphling
            camouflageAndMorphActions();

            // Impostors
            updateImpostorKillButton(__instance);
            // Timer updates
            timerUpdate();
            // Mini
            miniUpdate();
            // UnderTaker
            underTakerUpdate();
        }
    }
}