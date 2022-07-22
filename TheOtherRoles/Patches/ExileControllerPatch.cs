using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using TheOtherRoles.Objects;
using UnhollowerBaseLib;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    [HarmonyPriority(Priority.First)]
    class ExileControllerBeginPatch
    {
        public static GameData.PlayerInfo lastExiled;
        public static void Prefix(ExileController __instance, [HarmonyArgument(0)] ref GameData.PlayerInfo exiled, [HarmonyArgument(1)] bool tie)
        {
            lastExiled = exiled;

            // Medic shield
            if (Medic.medic != null && AmongUsClient.Instance.AmHost && Medic.futureShielded != null && !Medic.medic.Data.IsDead)
            { // We need to send the RPC from the host here, to make sure that the order of shifting and setting the shield is correct(for that reason the futureShifted and futureShielded are being synced)
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MedicSetShielded, Hazel.SendOption.Reliable, -1);
                writer.Write(Medic.futureShielded.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.medicSetShielded(Medic.futureShielded.PlayerId);
            }

            // Madmate exiled
            if (AmongUsClient.Instance.AmHost
                && exiled != null
                && ((CreatedMadmate.exileCrewmate
                && exiled.Object.hasModifier(ModifierType.CreatedMadmate))
                || (Madmate.exileCrewmate
                && exiled.Object.hasModifier(ModifierType.Madmate))))
            {
                // pick random crewmate
                PlayerControl target = pickRandomCrewmate(exiled.PlayerId);
                if (target != null)
                {
                    // exile the picked crewmate
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.UncheckedExilePlayer,
                        Hazel.SendOption.Reliable,
                        -1);
                    writer.Write(target.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.uncheckedExilePlayer(target.PlayerId);
                }
            }

            // Shifter shift
            if (Shifter.shifter != null && AmongUsClient.Instance.AmHost && Shifter.futureShift != null)
            { // We need to send the RPC from the host here, to make sure that the order of shifting and erasing is correct (for that reason the futureShifted and futureErased are being synced)
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ShifterShift, Hazel.SendOption.Reliable, -1);
                writer.Write(Shifter.futureShift.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.shifterShift(Shifter.futureShift.PlayerId);
            }
            Shifter.futureShift = null;

            // Eraser erase
            if (Eraser.eraser != null && AmongUsClient.Instance.AmHost && Eraser.futureErased != null)
            {  // We need to send the RPC from the host here, to make sure that the order of shifting and erasing is correct (for that reason the futureShifted and futureErased are being synced)
                foreach (PlayerControl target in Eraser.futureErased)
                {
                    if (target != null && target.canBeErased())
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ErasePlayerRoles, Hazel.SendOption.Reliable, -1);
                        writer.Write(target.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.erasePlayerRoles(target.PlayerId);
                    }
                }
            }
            Eraser.futureErased = new List<PlayerControl>();

            // Trickster boxes
            if (Trickster.trickster != null && JackInTheBox.hasJackInTheBoxLimitReached())
            {
                JackInTheBox.convertToVents();
            }

            // Witch execute casted spells
            if (Witch.witch != null && Witch.futureSpelled != null && AmongUsClient.Instance.AmHost)
            {
                bool exiledIsWitch = exiled != null && exiled.PlayerId == Witch.witch.PlayerId;
                bool witchDiesWithExiledLover = exiled != null && Lovers.bothDie && exiled.Object.isLovers() && exiled.Object.getPartner() == Witch.witch;

                if ((witchDiesWithExiledLover || exiledIsWitch) && Witch.witchVoteSavesTargets) Witch.futureSpelled = new List<PlayerControl>();
                foreach (PlayerControl target in Witch.futureSpelled)
                {
                    if (target != null && !target.Data.IsDead && Helpers.checkMuderAttempt(Witch.witch, target, true) == MurderAttemptResult.PerformKill)
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.WitchSpellCast, Hazel.SendOption.Reliable, -1);
                        writer.Write(target.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.witchSpellCast(target.PlayerId);
                    }
                }
            }
            Witch.futureSpelled = new List<PlayerControl>();

            // SecurityGuard vents and cameras
            var allCameras = ShipStatus.Instance.AllCameras.ToList();
            MapOptions.camerasToAdd.ForEach(camera =>
            {
                camera.gameObject.SetActive(true);
                camera.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                allCameras.Add(camera);
            });
            ShipStatus.Instance.AllCameras = allCameras.ToArray();
            MapOptions.camerasToAdd = new List<SurvCamera>();

            foreach (Vent vent in MapOptions.ventsToSeal)
            {
                PowerTools.SpriteAnim animator = vent.GetComponent<PowerTools.SpriteAnim>();
                animator?.Stop();
                vent.EnterVentAnim = vent.ExitVentAnim = null;
                vent.myRend.sprite = animator == null ? SecurityGuard.getStaticVentSealedSprite() : SecurityGuard.getAnimatedVentSealedSprite();
                if (SubmergedCompatibility.IsSubmerged && vent.Id == 0) vent.myRend.sprite = SecurityGuard.getSubmergedCentralUpperSealedSprite();
                if (SubmergedCompatibility.IsSubmerged && vent.Id == 14) vent.myRend.sprite = SecurityGuard.getSubmergedCentralLowerSealedSprite();
                vent.myRend.color = Color.white;
                vent.name = "SealedVent_" + vent.name;
            }
            MapOptions.ventsToSeal = new List<Vent>();

            // 1 = reset per turn
            if (MapOptions.restrictDevices == 1)
                MapOptions.resetDeviceTimes();
        }

        private static PlayerControl pickRandomCrewmate(int exiledPlayerId)
        {
            int numAliveCrewmates = 0;
            // count alive crewmates
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                if (player.Data.Role.IsImpostor)
                    continue;
                if (player.Data.IsDead)
                    continue;
                if (player.PlayerId == exiledPlayerId)
                    continue;
                numAliveCrewmates++;
            }
            // get random number range 0, num of alive crewmates
            int targetPlayerIndex = TheOtherRoles.rnd.Next(0, numAliveCrewmates);
            int currentPlayerIndex = 0;
            // return the player
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                if (player.Data.Role.IsImpostor)
                    continue;
                if (player.Data.IsDead)
                    continue;
                if (player.PlayerId == exiledPlayerId)
                    continue;
                if (currentPlayerIndex == targetPlayerIndex)
                    return player;
                currentPlayerIndex++;
            }
            return null;
        }
    }

    [HarmonyPatch]
    class ExileControllerWrapUpPatch
    {

        [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
        class BaseExileControllerPatch
        {
            public static void Postfix(ExileController __instance)
            {
                WrapUpPostfix(__instance.exiled);
            }
        }

        [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
        class AirshipExileControllerPatch
        {
            public static void Postfix(AirshipExileController __instance)
            {
                WrapUpPostfix(__instance.exiled);
            }
        }

        // Workaround to add a "postfix" to the destroying of the exile controller (i.e. cutscene) of submerged
        [HarmonyPatch(typeof(UnityEngine.Object), nameof(UnityEngine.Object.Destroy), new Type[] { typeof(GameObject) })]
        public static void Prefix(GameObject obj)
        {
            if (!SubmergedCompatibility.IsSubmerged) return;
            if (obj != null && obj.name.Contains("ExileCutscene"))
            {
                WrapUpPostfix(ExileControllerBeginPatch.lastExiled);
            }
        }

        static void WrapUpPostfix(GameData.PlayerInfo exiled)
        {
            // Mini exile lose condition
            if (exiled != null)
            {
                var p = Helpers.playerById(exiled.PlayerId);
                if (p.hasModifier(ModifierType.Mini) && !Mini.isGrownUp(p) && !p.Data.Role.IsImpostor && !p.isNeutral())
                {
                    Mini.triggerMiniLose = true;
                }

                // Jester win condition
                else if (p.isRole(RoleType.Jester))
                {
                    Jester.triggerJesterWin = true;
                }

                else if (p.isRole(RoleType.TimeReviver))
                {
                    PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement
                    Helpers.handleVampireBiteOnBodyReport(); // Manually call Vampire handling, since the CmdReportDeadBody Prefix won't be called
                    RPCProcedure.uncheckedCmdReportDeadBody(PlayerControl.LocalPlayer.PlayerId, Byte.MaxValue);

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedCmdReportDeadBody, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(Byte.MaxValue);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.uncheckedCmdReportDeadBody(PlayerControl.LocalPlayer.PlayerId, Byte.MaxValue);
                }
            }

            if (SubmergedCompatibility.IsSubmerged)
            {
                var fullscreen = UnityEngine.GameObject.Find("FullScreen500(Clone)");
                if (fullscreen) fullscreen.SetActive(false);
            }
        }
    }

    [HarmonyPatch(typeof(ExileController), nameof(ExileController.ReEnableGameplay))]
    class ExileControllerReEnableGameplayPatch
    {
        public static void Postfix(ExileController __instance)
        {
            ReEnableGameplay();
        }
        public static void ReEnableGameplay()
        {
            // Reset custom button timers where necessary
            CustomButton.MeetingEndedUpdate();

            // Custom role post-meeting functions
            TheOtherRolesGM.OnMeetingEnd();

            // Mini set adapted cooldown
            if (PlayerControl.LocalPlayer.hasModifier(ModifierType.Mini) && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            {
                var multiplier = Mini.isGrownUp(PlayerControl.LocalPlayer) ? 0.66f : 2f;
                PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown * multiplier);
            }

            // Seer spawn souls
            if (Seer.deadBodyPositions != null && Seer.seer != null && PlayerControl.LocalPlayer == Seer.seer && (Seer.mode == 0 || Seer.mode == 2))
            {
                foreach (Vector3 pos in Seer.deadBodyPositions)
                {
                    GameObject soul = new();
                    // soul.transform.position = pos;
                    soul.transform.position = new Vector3(pos.x, pos.y, pos.y / 1000 - 1f);
                    soul.layer = 5;
                    var rend = soul.AddComponent<SpriteRenderer>();
                    soul.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
                    rend.sprite = Seer.getSoulSprite();

                    if (Seer.limitSoulDuration)
                    {
                        HudManager.Instance.StartCoroutine(Effects.Lerp(Seer.soulDuration, new Action<float>((p) =>
                        {
                            if (rend != null)
                            {
                                var tmp = rend.color;
                                tmp.a = Mathf.Clamp01(1 - p);
                                rend.color = tmp;
                            }
                            if (p == 1f && rend != null && rend.gameObject != null) UnityEngine.Object.Destroy(rend.gameObject);
                        })));
                    }
                }
                Seer.deadBodyPositions = new List<Vector3>();
            }

            // Tracker reset deadBodyPositions
            Tracker.deadBodyPositions = new List<Vector3>();

            // Arsonist deactivate dead poolable players
            Arsonist.updateIcons();

            // Force Bounty Hunter Bounty Update
            if (BountyHunter.bountyHunter != null && BountyHunter.bountyHunter == PlayerControl.LocalPlayer)
                BountyHunter.bountyUpdateTimer = 0f;

            // Medium spawn souls
            if (Medium.medium != null && PlayerControl.LocalPlayer == Medium.medium)
            {
                if (Medium.souls != null)
                {
                    foreach (SpriteRenderer sr in Medium.souls) UnityEngine.Object.Destroy(sr.gameObject);
                    Medium.souls = new List<SpriteRenderer>();
                }

                if (Medium.featureDeadBodies != null)
                {
                    foreach ((DeadPlayer db, Vector3 ps) in Medium.featureDeadBodies)
                    {
                        GameObject s = new();
                        // s.transform.position = ps;
                        s.transform.position = new Vector3(ps.x, ps.y, ps.y / 1000 - 1f);
                        s.layer = 5;
                        var rend = s.AddComponent<SpriteRenderer>();
                        s.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
                        rend.sprite = Medium.getSoulSprite();
                        Medium.souls.Add(rend);
                    }
                    Medium.deadBodies = Medium.featureDeadBodies;
                    Medium.featureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
                }
            }

            if (Lawyer.lawyer != null && PlayerControl.LocalPlayer == Lawyer.lawyer && !Lawyer.lawyer.Data.IsDead)
                Lawyer.meetings++;

            if (PlayerControl.LocalPlayer.hasModifier(ModifierType.AntiTeleport))
            {
                if (AntiTeleport.position != new Vector3())
                {
                    PlayerControl.LocalPlayer.transform.position = AntiTeleport.position;
                    if (SubmergedCompatibility.IsSubmerged)
                    {
                        SubmergedCompatibility.ChangeFloor(AntiTeleport.position.y > -7);
                    }
                }
            }

            // Remove DeadBodys
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++)
            {
                UnityEngine.Object.Destroy(array[i].gameObject);
            }

        }
    }

    [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString), new Type[] { typeof(StringNames), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) })]
    class ExileControllerMessagePatch
    {
        static void Postfix(ref string __result, [HarmonyArgument(0)] StringNames id)
        {
            try
            {
                if (ExileController.Instance != null && ExileController.Instance.exiled != null)
                {
                    PlayerControl player = Helpers.playerById(ExileController.Instance.exiled.Object.PlayerId);
                    if (player == null) return;
                    // Exile role text
                    if (id is StringNames.ExileTextPN or StringNames.ExileTextSN or StringNames.ExileTextPP or StringNames.ExileTextSP)
                    {
                        __result = player.Data.PlayerName + " was The " + String.Join(" ", RoleInfo.getRoleInfoForPlayer(player).Select(x => x.name).ToArray());
                    }
                    // Hide number of remaining impostors on Jester win
                    if (id is StringNames.ImpostorsRemainP or StringNames.ImpostorsRemainS)
                    {
                        if (Jester.jester != null && player.PlayerId == Jester.jester.PlayerId) __result = "";
                    }
                }
            }
            catch
            {
                // pass - Hopefully prevent leaving while exiling to softlock game
            }
        }
    }
}