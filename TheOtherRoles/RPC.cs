using HarmonyLib;
using Hazel;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;
using static TheOtherRoles.HudManagerStartPatch;
using static TheOtherRoles.GameHistory;
using TheOtherRoles.Utilities;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using TheOtherRoles.Modules;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace TheOtherRoles
{

    enum CustomRPC
    {
        // Main Controls

        ResetVariables = 60,
        ShareOptions,
        CrewmateEnd,
        ImpostorEnd,
        SetRole,
        SetLovers,
        VersionHandshake,
        UseUncheckedVent,
        UncheckedMurderPlayer,
        UncheckedCmdReportDeadBody,
        OverrideNativeRole,
        UncheckedExilePlayer,
        UncheckedEndGame,
        UncheckedSetTasks,
        DynamicMapOption,
        UncheckedShapeShift,

        // Role functionality

        EngineerFixLights = 91,
        EngineerUsedRepair,
        EngineerFixSubmergedOxygen,
        CleanBody,
        SheriffKill,
        MedicSetShielded,
        ShieldedMurderAttempt,
        TimeMasterShield,
        TimeMasterRewindTime,
        ShifterShift = 100,
        SwapperSwap,
        MorphlingMorph,
        CamouflagerCamouflage,
        TrackerUsedTracker,
        VampireSetBitten,
        PlaceGarlic,
        EvilHackerCreatesMadmate,
        JackalCreatesSidekick,
        SidekickPromotes,
        ErasePlayerRoles = 110,
        SetFutureErased,
        SetFutureShifted,
        SetFutureShielded,
        SetFutureSpelled,
        WitchSpellCast,
        PlaceJackInTheBox,
        LightsOut,
        PlaceCamera,
        SealVent,
        ArsonistWin = 120,
        GuesserShoot,
        VultureWin,
        LawyerWin,
        LawyerSetTarget,
        LawyerPromotesToPursuer,
        SetBlanked,
        PlacePortal,
        UsePortal,
        PlaceAssassinTrace, // 129

        // GM Edition functionality
        AddModifier = 145, // 130~144をSubmergedが使用するらしい
        NinjaStealth,
        SetShifterType,
        GMKill,
        GMRevive,
        UseAdminTime = 150,
        UseCameraTime,
        UseVitalsTime,
        ArsonistDouse,
        VultureEat,
        PlagueDoctorWin,
        PlagueDoctorSetInfected,
        PlagueDoctorUpdateProgress,
        NekoKabochaExile,
        SerialKillerSuicide,
        FortuneTellerUsedDivine = 160,
        FoxStealth,
        FoxCreatesImmoralist,
        SwapperAnimate,
        SprinterSprint,
        NormalKill,
        GunKill,
        StaffKill,
        BossKill,
        DragPlaceBody,
        DevourBody = 170,
        /*CreatorCreateSheriff,
        StudentPromotion,
        SheriffCreateStudent,*/
        RPCExiled,
        TrapperTrap,
        SilencerSilenceKill,
        CamouflagedComms,
        ResetCamouflagedComms,
        EaterEat,
    }

    public static class RPCProcedure
    {
        // Main Controls

        public static void resetVariables()
        {
            Garlic.clearGarlics();
            JackInTheBox.clearJackInTheBoxes();
            MapOptions.clearAndReloadMapOptions();
            TheOtherRoles.clearAndReloadRoles();
            GameHistory.clearGameHistory();
            setCustomButtonCooldowns();
            AssassinTrace.clearTraces();
            Portal.clearPortals();
            AdminPatch.ResetData();
            CameraPatch.ResetData();
            VitalsPatch.ResetData();
            MapBehaviorPatch.resetIcons();
            CustomVent.clearAndReload();
            CustomOverlays.resetOverlays();

            KillAnimationCoPerformKillPatch.hideNextAnimation = false;
        }

        public static void ShareOptions(int numberOfOptions, MessageReader reader)
        {
            try
            {
                for (int i = 0; i < numberOfOptions; i++)
                {
                    uint optionId = reader.ReadPackedUInt32();
                    uint selection = reader.ReadPackedUInt32();
                    CustomOption option = CustomOption.options.FirstOrDefault(option => option.id == (int)optionId);
                    option.updateSelection((int)selection);
                }
            }
            catch (Exception e)
            {
                TheOtherRolesPlugin.Logger.LogError("Error while deserializing options: " + e.Message);
            }
        }

        public static void impostorEnd()
        {
            if (AmongUsClient.Instance.AmHost)
            {
                ShipStatus.Instance.enabled = false;
                ShipStatus.RpcEndGame(GameOverReason.ImpostorByKill, false);
            }
        }

        public static void crewmateEnd()
        {
            if (AmongUsClient.Instance.AmHost)
            {
                ShipStatus.Instance.enabled = false;
                ShipStatus.RpcEndGame(GameOverReason.HumansByTask, false);
            }
        }

        public static void setRole(byte roleId, byte playerId, byte flag)
        {
            PlayerControl.AllPlayerControls.ToArray().DoIf(
                x => x.PlayerId == playerId,
                x => x.setRole((RoleType)roleId)
            );
        }

        public static void addModifier(byte modId, byte playerId)
        {
            PlayerControl.AllPlayerControls.ToArray().DoIf(
                x => x.PlayerId == playerId,
                x => x.addModifier((ModifierType)modId)
            );
        }

        public static void setLovers(byte playerId1, byte playerId2)
        {
            Lovers.addCouple(Helpers.playerById(playerId1), Helpers.playerById(playerId2));
        }

        public static void overrideNativeRole(byte playerId, byte roleType)
        {
            var player = Helpers.playerById(playerId);
            player.roleAssigned = false;
            DestroyableSingleton<RoleManager>.Instance.SetRole(player, (RoleTypes)roleType);
        }

        public static void versionHandshake(int major, int minor, int build, int revision, Guid guid, int clientId)
        {
            System.Version ver;
            if (revision < 0)
                ver = new System.Version(major, minor, build);
            else
                ver = new System.Version(major, minor, build, revision);
            GameStartManagerPatch.playerVersions[clientId] = new GameStartManagerPatch.PlayerVersion(ver, guid);
        }

        public static void useUncheckedVent(int ventId, byte playerId, byte isEnter)
        {
            PlayerControl player = Helpers.playerById(playerId);
            if (player == null) return;
            // Fill dummy MessageReader and call MyPhysics.HandleRpc as the corountines cannot be accessed
            MessageReader reader = new MessageReader();
            byte[] bytes = BitConverter.GetBytes(ventId);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            reader.Buffer = bytes;
            reader.Length = bytes.Length;

            JackInTheBox.startAnimation(ventId);
            player.MyPhysics.HandleRpc(isEnter != 0 ? (byte)19 : (byte)20, reader);
        }

        public static void uncheckedMurderPlayer(byte sourceId, byte targetId, byte showAnimation)
        {
            PlayerControl source = Helpers.playerById(sourceId);
            PlayerControl target = Helpers.playerById(targetId);
            if (source != null && target != null)
            {
                if (showAnimation == 0) KillAnimationCoPerformKillPatch.hideNextAnimation = true;
                source.MurderPlayer(target);
            }
        }

        public static void uncheckedCmdReportDeadBody(byte sourceId, byte targetId)
        {
            PlayerControl source = Helpers.playerById(sourceId);
            var t = targetId == Byte.MaxValue ? null : Helpers.playerById(targetId).Data;
            if (source != null) source.ReportDeadBody(t);
        }

        public static void uncheckedExilePlayer(byte targetId)
        {
            PlayerControl target = Helpers.playerById(targetId);
            if (target != null)
            {
                target.Exiled();
            }
        }

        public static void uncheckedEndGame(byte reason)
        {
            AmongUsClient.Instance.GameState = InnerNet.InnerNetClient.GameStates.Ended;
            var obj2 = AmongUsClient.Instance.allClients;
            lock (obj2)
            {
                AmongUsClient.Instance.allClients.Clear();
            }

            var obj = AmongUsClient.Instance.Dispatcher;
            lock (obj)
            {
                AmongUsClient.Instance.Dispatcher.Add(new Action(() =>
                {
                    ShipStatus.Instance.enabled = false;
                    ShipStatus.Instance.ShouldCheckForGameEnd = false;
                    AmongUsClient.Instance.OnGameEnd(new EndGameResult((GameOverReason)reason, false));

                    if (AmongUsClient.Instance.AmHost)
                        ShipStatus.RpcEndGame((GameOverReason)reason, false);
                }));
            }
        }

        public static void uncheckedSetTasks(byte playerId, byte[] taskTypeIds)
        {
            var player = Helpers.playerById(playerId);
            player.clearAllTasks();

            GameData.Instance.SetTasks(playerId, taskTypeIds);
        }

        public static void UncheckedShapeShift(byte sourceId, byte targetId, byte useAnimation)
        {
            PlayerControl source = Helpers.playerById(sourceId);
            PlayerControl target = Helpers.playerById(targetId);
            bool animate = true;

            if (useAnimation != byte.MaxValue)
            {
                animate = false;
            }
            source.Shapeshift(target, animate);
        }

        public static void dynamicMapOption(byte mapId)
        {
            PlayerControl.GameOptions.MapId = mapId;
        }

        // Role functionality

        public static void engineerFixLights()
        {
            SwitchSystem switchSystem = MapUtilities.Systems[SystemTypes.Electrical].CastFast<SwitchSystem>();
            switchSystem.ActualSwitches = switchSystem.ExpectedSwitches;
        }

        public static void engineerUsedRepair()
        {
            Engineer.remainingFixes--;
        }

        public static void engineerFixSubmergedOxygen()
        {
            SubmergedCompatibility.RepairOxygen();
        }

        public static void cleanBody(byte playerId)
        {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++)
            {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId)
                {
                    UnityEngine.Object.Destroy(array[i].gameObject);
                }
            }
        }

        public static void sheriffKill(byte sheriffId, byte targetId, bool misfire)
        {
            PlayerControl sheriff = Helpers.playerById(sheriffId);
            PlayerControl target = Helpers.playerById(targetId);
            if (sheriff == null || target == null) return;

            Sheriff role = Sheriff.getRole(sheriff);
            if (role != null)
//                if (!CustomOptionHolder.yakuzaShotsShare.getBool())
                    role.numShots--;
//                else
//                    Gun.shareShots--;

//シェリフの弾数制限を修正するため、場当たり的にヤクザの弾数共有破壊してます。そっちまで対応する気力ないんで直すんならそちらで　by.hawk


            if (misfire)
            {
                sheriff.MurderPlayer(sheriff);
                finalStatuses[sheriffId] = FinalStatus.Misfire;

                if (!Sheriff.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;
            }

            sheriff.MurderPlayer(target);
        }

        public static void gunKill(byte gunId, byte targetId, bool misfire)
        {
            PlayerControl gun = Helpers.playerById(gunId);
            PlayerControl target = Helpers.playerById(targetId);
            if (gun == null || target == null) return;

            Gun role = Gun.getRole(gun);
            if (role != null && !CustomOptionHolder.yakuzaShotsShare.getBool())
                role.numShots--;
            else


            if (misfire)
            {
                gun.MurderPlayer(gun);
                finalStatuses[gunId] = FinalStatus.Misfire;

                if (!Gun.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;

                if (!Staff.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;

                if (!Boss.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;
            }

            gun.MurderPlayer(target);
        }

        public static void staffKill(byte gunId, byte targetId, bool misfire)
        {
            PlayerControl staff = Helpers.playerById(gunId);
            PlayerControl target = Helpers.playerById(targetId);
            if (staff == null || target == null) return;

            Staff role = Staff.getRole(staff);
            if (role != null)
                if (!CustomOptionHolder.yakuzaShotsShare.getBool())
                    role.numShots--;
                else
                    Gun.shareShots--;


            if (misfire)
            {
                staff.MurderPlayer(staff);
                finalStatuses[gunId] = FinalStatus.Misfire;

                if (!Gun.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;

                if (!Staff.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;

                if (!Boss.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;
            }

            staff.MurderPlayer(target);
        }

        public static void bossKill(byte gunId, byte targetId, bool misfire)
        {
            PlayerControl boss = Helpers.playerById(gunId);
            PlayerControl target = Helpers.playerById(targetId);
            if (boss == null || target == null) return;

            Boss role = Boss.getRole(boss);
            if (role != null)
                if (!CustomOptionHolder.yakuzaShotsShare.getBool())
                    role.numShots--;
                else
                    Gun.shareShots--;


            if (misfire)
            {
                boss.MurderPlayer(boss);
                finalStatuses[gunId] = FinalStatus.Misfire;

                if (!Gun.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;

                if (!Staff.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;

                if (!Boss.misfireKillsTarget) return;
                finalStatuses[targetId] = FinalStatus.Misfire;
            }

            boss.MurderPlayer(target);
        }

        public static void timeMasterRewindTime()
        {
            TimeMaster.shieldActive = false; // Shield is no longer active when rewinding
            if (TimeMaster.timeMaster != null && TimeMaster.timeMaster == PlayerControl.LocalPlayer)
            {
                resetTimeMasterButton();
            }
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0f, 0.5f, 0.8f, 0.3f);
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.gameObject.SetActive(true);
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(TimeMaster.rewindTime / 2, new Action<float>((p) =>
            {
                if (p == 1f) FastDestroyableSingleton<HudManager>.Instance.FullScreen.enabled = false;
            })));

            if (TimeMaster.timeMaster == null || PlayerControl.LocalPlayer == TimeMaster.timeMaster) return; // Time Master himself does not rewind
            if (PlayerControl.LocalPlayer.isGM()) return; // GM does not rewind

            TimeMaster.isRewinding = true;

            if (MapBehaviour.Instance)
                MapBehaviour.Instance.Close();
            if (Minigame.Instance)
                Minigame.Instance.ForceClose();
            PlayerControl.LocalPlayer.moveable = false;

            // Remove body dragging for UnderTaker
            if (UnderTaker.underTaker != null && UnderTaker.dragginBody)
            {
                UnderTaker.underTakerResetValuesAtDead();
            }
        }

        public static void timeMasterShield()
        {
            TimeMaster.shieldActive = true;
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(TimeMaster.shieldDuration, new Action<float>((p) =>
            {
                if (p == 1f) TimeMaster.shieldActive = false;
            })));
        }

        public static void medicSetShielded(byte shieldedId)
        {
            Medic.usedShield = true;
            Medic.shielded = Helpers.playerById(shieldedId);
            Medic.futureShielded = null;
        }

        public static void shieldedMurderAttempt()
        {
            if (Medic.shielded == null || Medic.medic == null) return;

            bool isShieldedAndShow = Medic.shielded == PlayerControl.LocalPlayer && Medic.showAttemptToShielded;
            bool isMedicAndShow = Medic.medic == PlayerControl.LocalPlayer && Medic.showAttemptToMedic;

            if (isShieldedAndShow || isMedicAndShow) Helpers.showFlash(Palette.ImpostorRed, duration: 0.5f);
        }

        public static void shifterShift(byte targetId)
        {
            PlayerControl oldShifter = Shifter.shifter;
            PlayerControl player = Helpers.playerById(targetId);
            if (player == null || oldShifter == null) return;

            Shifter.futureShift = null;
            if (!Shifter.isNeutral)
                Shifter.clearAndReload();

            if (player == GM.gm)
            {
                return;
            }

            // Suicide (exile) when impostor or impostor variants
            if (!Shifter.isNeutral && (player.Data.Role.IsImpostor || player.isNeutral() || player.hasModifier(ModifierType.Madmate) || player.hasModifier(ModifierType.CreatedMadmate)))
            {
                oldShifter.Exiled();
                finalStatuses[oldShifter.PlayerId] = FinalStatus.Suicide;
                return;
            }

            if (Shifter.shiftModifiers)
            {
                // Switch shield
                if (Medic.shielded != null && Medic.shielded == player)
                {
                    Medic.shielded = oldShifter;
                }
                else if (Medic.shielded != null && Medic.shielded == oldShifter)
                {
                    Medic.shielded = player;
                }

                player.swapModifiers(oldShifter);
                Lovers.swapLovers(oldShifter, player);
            }

            // Shift role
            player.swapRoles(oldShifter);

            if (Shifter.isNeutral)
            {
                Shifter.shifter = player;
                Shifter.pastShifters.Add(oldShifter.PlayerId);

                if (player.Data.Role.IsImpostor)
                {
                    DestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
                    DestroyableSingleton<RoleManager>.Instance.SetRole(oldShifter, RoleTypes.Impostor);
                }
            }

            if (Lawyer.lawyer != null && Lawyer.target == player)
            {
                Lawyer.target = oldShifter;
            }

            // Set cooldowns to max for both players
            if (PlayerControl.LocalPlayer == oldShifter || PlayerControl.LocalPlayer == player)
                CustomButton.ResetAllCooldowns();
        }

        public static void swapperSwap(byte playerId1, byte playerId2)
        {
            if (MeetingHud.Instance)
            {
                Swapper.playerId1 = playerId1;
                Swapper.playerId2 = playerId2;
            }
        }

        public static void swapperAnimate()
        {
            MeetingHudPatch.animateSwap = true;
        }

        public static void morphlingMorph(byte playerId)
        {
            PlayerControl target = Helpers.playerById(playerId);
            if (Morphling.morphling == null || target == null) return;
            Morphling.startMorph(target);
        }

        public static void camouflagerCamouflage()
        {
            if (Camouflager.camouflager == null) return;
            Camouflager.startCamouflage();
        }

        public static void vampireSetBitten(byte targetId, byte performReset)
        {
            if (performReset != 0)
            {
                Vampire.bitten = null;
                return;
            }

            if (Vampire.vampire == null) return;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls.GetFastEnumerator())
            {
                if (player.PlayerId == targetId && !player.Data.IsDead)
                {
                    Vampire.bitten = player;
                }
            }
        }

        public static void placeGarlic(byte[] buff)
        {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Garlic(position);
        }

        public static void evilHackerCreatesMadmate(byte targetId)
        {
            PlayerControl player = Helpers.playerById(targetId);
            if (!EvilHacker.canCreateMadmateFromJackal && player.isRole(RoleType.Jackal))
            {
                EvilHacker.fakeMadmate = player;
            }
            else if (!EvilHacker.canCreateMadmateFromFox && player.isRole(RoleType.Fox))
            {
                EvilHacker.fakeMadmate = player;
            }
            else
            {
                // Jackalバグ対応
                List<PlayerControl> tmpFormerJackals = new List<PlayerControl>(Jackal.formerJackals);

                // タスクがないプレイヤーがMadmateになった場合はショートタスクを必要数割り当てる
                if (Helpers.hasFakeTasks(player))
                {
                    if (CreatedMadmate.hasTasks)
                    {
                        Helpers.clearAllTasks(player);
                        player.generateAndAssignTasks(0, CreatedMadmate.numTasks, 0);
                    }
                }

                player.RemoveInfected();
                erasePlayerRoles(player.PlayerId, true, false);

                // Jackalバグ対応
                Jackal.formerJackals = tmpFormerJackals;

                player.addModifier(ModifierType.CreatedMadmate);
            }
            EvilHacker.canCreateMadmate = false;
            return;
        }

        public static void trackerUsedTracker(byte targetId)
        {
            Tracker.usedTracker = true;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls.GetFastEnumerator())
                if (player.PlayerId == targetId)
                    Tracker.tracked = player;
        }

        public static void jackalCreatesSidekick(byte targetId)
        {
            PlayerControl player = Helpers.playerById(targetId);
            if (player == null) return;

            if (!Jackal.canCreateSidekickFromImpostor && player.Data.Role.IsImpostor)
            {
                Jackal.fakeSidekick = player;
            }
            else if (!Jackal.canCreateSidekickFromFox && player.isRole(RoleType.Fox))
            {
                Jackal.fakeSidekick = player;
            }
            else
            {
                bool wasSpy = Spy.spy != null && player == Spy.spy;
                bool wasImpostor = player.Data.Role.IsImpostor;  // This can only be reached if impostors can be sidekicked.
                FastDestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
                if (player == Lawyer.lawyer && Lawyer.target != null)
                {
                    Transform playerInfoTransform = Lawyer.target.cosmetics.nameText.transform.parent.FindChild("Info");
                    TMPro.TextMeshPro playerInfo = playerInfoTransform != null ? playerInfoTransform.GetComponent<TMPro.TextMeshPro>() : null;
                    if (playerInfo != null) playerInfo.text = "";
                }
                erasePlayerRoles(player.PlayerId, true);
                Sidekick.sidekick = player;
                if (player.PlayerId == PlayerControl.LocalPlayer.PlayerId) PlayerControl.LocalPlayer.moveable = true;
                if (wasSpy || wasImpostor) Sidekick.wasTeamRed = true;
                Sidekick.wasSpy = wasSpy;
                Sidekick.wasImpostor = wasImpostor;
                if (Fox.exists && !Fox.isFoxAlive())
                {
                    foreach (var immoralist in Immoralist.allPlayers)
                    {
                        immoralist.MurderPlayer(immoralist);
                    }
                }
            }
            Jackal.canCreateSidekick = false;
        }

        public static void sidekickPromotes()
        {
            Jackal.removeCurrentJackal();
            Jackal.jackal = Sidekick.sidekick;
            Jackal.canCreateSidekick = Jackal.jackalPromotedFromSidekickCanCreateSidekick;
            Jackal.wasTeamRed = Sidekick.wasTeamRed;
            Jackal.wasSpy = Sidekick.wasSpy;
            Jackal.wasImpostor = Sidekick.wasImpostor;
            Sidekick.clearAndReload();
            return;
        }

        public static void erasePlayerRoles(byte playerId, bool ignoreLovers = false, bool clearNeutralTasks = true)
        {
            PlayerControl player = Helpers.playerById(playerId);
            if (player == null) return;

            // Don't give a former neutral role tasks because that destroys the balance.
            if (player.isNeutral() && clearNeutralTasks)
                player.clearAllTasks();

            player.eraseAllRoles();
            player.eraseAllModifiers();

            if (!ignoreLovers && player.isLovers())
            { // The whole Lover couple is being erased
                Lovers.eraseCouple(player);
            }
        }

        public static void setFutureErased(byte playerId)
        {
            PlayerControl player = Helpers.playerById(playerId);
            if (Eraser.futureErased == null)
                Eraser.futureErased = new List<PlayerControl>();
            if (player != null)
            {
                Eraser.futureErased.Add(player);
            }
        }

        public static void setFutureShifted(byte playerId)
        {
            if (Shifter.isNeutral && !Shifter.shiftPastShifters && Shifter.pastShifters.Contains(playerId))
                return;
            Shifter.futureShift = Helpers.playerById(playerId);
        }

        public static void setFutureShielded(byte playerId)
        {
            Medic.futureShielded = Helpers.playerById(playerId);
            Medic.usedShield = true;
        }

        public static void setFutureSpelled(byte playerId)
        {
            PlayerControl player = Helpers.playerById(playerId);
            if (Witch.futureSpelled == null)
                Witch.futureSpelled = new List<PlayerControl>();
            if (player != null)
            {
                Witch.futureSpelled.Add(player);
            }
        }

        public static void placePortal(byte[] buff)
        {
            Vector3 position = Vector2.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Portal(position);
        }

        public static void usePortal(byte playerId)
        {
            Portal.startTeleport(playerId);
        }

        public static void placeAssassinTrace(byte[] buff)
        {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new AssassinTrace(position, Assassin.traceTime);
        }

        public static void placeJackInTheBox(byte[] buff)
        {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new JackInTheBox(position);
        }

        public static void lightsOut()
        {
            Trickster.lightsOutTimer = Trickster.lightsOutDuration;
            // If the local player is impostor indicate lights out
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            {
                new CustomMessage(ModTranslation.getString("tricksterLightsOutText"), Trickster.lightsOutDuration);
            }
        }

        public static void placeCamera(byte[] buff, byte roomId)
        {
            var referenceCamera = UnityEngine.Object.FindObjectOfType<SurvCamera>();
            if (referenceCamera == null) return; // Mira HQ

            SecurityGuard.remainingScrews -= SecurityGuard.camPrice;
            SecurityGuard.placedCameras++;

            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));

            SystemTypes roomType = (SystemTypes)roomId;

            var camera = UnityEngine.Object.Instantiate<SurvCamera>(referenceCamera);
            camera.transform.position = new Vector3(position.x, position.y, referenceCamera.transform.position.z - 1f);
            camera.CamName = $"Security Camera {SecurityGuard.placedCameras}";
            camera.Offset = new Vector3(0f, 0f, camera.Offset.z);

            switch (roomType)
            {
                case SystemTypes.Hallway: camera.NewName = StringNames.Hallway; break;
                case SystemTypes.Storage: camera.NewName = StringNames.Storage; break;
                case SystemTypes.Cafeteria: camera.NewName = StringNames.Cafeteria; break;
                case SystemTypes.Reactor: camera.NewName = StringNames.Reactor; break;
                case SystemTypes.UpperEngine: camera.NewName = StringNames.UpperEngine; break;
                case SystemTypes.Nav: camera.NewName = StringNames.Nav; break;
                case SystemTypes.Admin: camera.NewName = StringNames.Admin; break;
                case SystemTypes.Electrical: camera.NewName = StringNames.Electrical; break;
                case SystemTypes.LifeSupp: camera.NewName = StringNames.LifeSupp; break;
                case SystemTypes.Shields: camera.NewName = StringNames.Shields; break;
                case SystemTypes.MedBay: camera.NewName = StringNames.MedBay; break;
                case SystemTypes.Security: camera.NewName = StringNames.Security; break;
                case SystemTypes.Weapons: camera.NewName = StringNames.Weapons; break;
                case SystemTypes.LowerEngine: camera.NewName = StringNames.LowerEngine; break;
                case SystemTypes.Comms: camera.NewName = StringNames.Comms; break;
                case SystemTypes.Decontamination: camera.NewName = StringNames.Decontamination; break;
                case SystemTypes.Launchpad: camera.NewName = StringNames.Launchpad; break;
                case SystemTypes.LockerRoom: camera.NewName = StringNames.LockerRoom; break;
                case SystemTypes.Laboratory: camera.NewName = StringNames.Laboratory; break;
                case SystemTypes.Balcony: camera.NewName = StringNames.Balcony; break;
                case SystemTypes.Office: camera.NewName = StringNames.Office; break;
                case SystemTypes.Greenhouse: camera.NewName = StringNames.Greenhouse; break;
                case SystemTypes.Dropship: camera.NewName = StringNames.Dropship; break;
                case SystemTypes.Decontamination2: camera.NewName = StringNames.Decontamination2; break;
                case SystemTypes.Outside: camera.NewName = StringNames.Outside; break;
                case SystemTypes.Specimens: camera.NewName = StringNames.Specimens; break;
                case SystemTypes.BoilerRoom: camera.NewName = StringNames.BoilerRoom; break;
                case SystemTypes.VaultRoom: camera.NewName = StringNames.VaultRoom; break;
                case SystemTypes.Cockpit: camera.NewName = StringNames.Cockpit; break;
                case SystemTypes.Armory: camera.NewName = StringNames.Armory; break;
                case SystemTypes.Kitchen: camera.NewName = StringNames.Kitchen; break;
                case SystemTypes.ViewingDeck: camera.NewName = StringNames.ViewingDeck; break;
                case SystemTypes.HallOfPortraits: camera.NewName = StringNames.HallOfPortraits; break;
                case SystemTypes.CargoBay: camera.NewName = StringNames.CargoBay; break;
                case SystemTypes.Ventilation: camera.NewName = StringNames.Ventilation; break;
                case SystemTypes.Showers: camera.NewName = StringNames.Showers; break;
                case SystemTypes.Engine: camera.NewName = StringNames.Engine; break;
                case SystemTypes.Brig: camera.NewName = StringNames.Brig; break;
                case SystemTypes.MeetingRoom: camera.NewName = StringNames.MeetingRoom; break;
                case SystemTypes.Records: camera.NewName = StringNames.Records; break;
                case SystemTypes.Lounge: camera.NewName = StringNames.Lounge; break;
                case SystemTypes.GapRoom: camera.NewName = StringNames.GapRoom; break;
                case SystemTypes.MainHall: camera.NewName = StringNames.MainHall; break;
                case SystemTypes.Medical: camera.NewName = StringNames.Medical; break;
                default: camera.NewName = StringNames.ExitButton; break;
            }

            if (PlayerControl.GameOptions.MapId == 2 || PlayerControl.GameOptions.MapId == 4) camera.transform.localRotation = new Quaternion(0, 0, 1, 1); // Polus and Airship

            if (SubmergedCompatibility.IsSubmerged)
            {
                // remove 2d box collider of console, so that no barrier can be created. (irrelevant for now, but who knows... maybe we need it later)
                var fixConsole = camera.transform.FindChild("FixConsole");
                if (fixConsole != null)
                {
                    var boxCollider = fixConsole.GetComponent<BoxCollider2D>();
                    if (boxCollider != null) UnityEngine.Object.Destroy(boxCollider);
                }
            }

            if (PlayerControl.LocalPlayer == SecurityGuard.securityGuard)
            {
                camera.gameObject.SetActive(true);
                camera.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                camera.gameObject.SetActive(false);
            }
            MapOptions.camerasToAdd.Add(camera);
        }

        public static void sealVent(int ventId)
        {
            Vent vent = MapUtilities.CachedShipStatus.AllVents.FirstOrDefault((x) => x != null && x.Id == ventId);
            if (vent == null) return;

            SecurityGuard.remainingScrews -= SecurityGuard.ventPrice;
            if (PlayerControl.LocalPlayer == SecurityGuard.securityGuard)
            {
                PowerTools.SpriteAnim animator = vent.GetComponent<PowerTools.SpriteAnim>();
                animator?.Stop();
                vent.EnterVentAnim = vent.ExitVentAnim = null;
                vent.myRend.sprite = animator == null ? SecurityGuard.getStaticVentSealedSprite() : SecurityGuard.getAnimatedVentSealedSprite();
                if (SubmergedCompatibility.IsSubmerged && vent.Id == 0) vent.myRend.sprite = SecurityGuard.getSubmergedCentralUpperSealedSprite();
                if (SubmergedCompatibility.IsSubmerged && vent.Id == 14) vent.myRend.sprite = SecurityGuard.getSubmergedCentralLowerSealedSprite();
                vent.myRend.color = new Color(1f, 1f, 1f, 0.5f);
                vent.name = "FutureSealedVent_" + vent.name;
            }

            MapOptions.ventsToSeal.Add(vent);
        }

        public static void arsonistDouse(byte playerId)
        {
            Arsonist.dousedPlayers.Add(Helpers.playerById(playerId));
        }

        public static void arsonistWin()
        {
            Arsonist.triggerArsonistWin = true;
            var livingPlayers = PlayerControl.AllPlayerControls.ToArray().Where(p => !p.isRole(RoleType.Arsonist) && p.isAlive());
            foreach (PlayerControl p in livingPlayers)
            {
                p.Exiled();
                finalStatuses[p.PlayerId] = FinalStatus.Torched;
            }
        }

        public static void vultureEat(byte playerId)
        {
            cleanBody(playerId);
            Vulture.eatenBodies++;
        }

        public static void vultureWin()
        {
            Vulture.triggerVultureWin = true;
        }

        public static void lawyerWin()
        {
            Lawyer.triggerLawyerWin = true;
        }

        public static void lawyerSetTarget(byte playerId)
        {
            Lawyer.target = Helpers.playerById(playerId);
        }

        public static void lawyerPromotesToPursuer()
        {
            PlayerControl player = Lawyer.lawyer;
            PlayerControl client = Lawyer.target;
            Lawyer.clearAndReload();
            Pursuer.pursuer = player;

            if (player.PlayerId == PlayerControl.LocalPlayer.PlayerId && client != null)
            {
                Transform playerInfoTransform = client.cosmetics.nameText.transform.parent.FindChild("Info");
                TMPro.TextMeshPro playerInfo = playerInfoTransform != null ? playerInfoTransform.GetComponent<TMPro.TextMeshPro>() : null;
                if (playerInfo != null) playerInfo.text = "";
            }
        }

        public static void guesserShoot(byte killerId, byte dyingTargetId, byte guessedTargetId, byte guessedRoleId)
        {
            PlayerControl killer = Helpers.playerById(killerId);
            PlayerControl dyingTarget = Helpers.playerById(dyingTargetId);
            if (dyingTarget == null) return;
            if (dyingTarget.isRole(RoleType.NekoKabocha))
            {
                NekoKabocha.meetingKill(dyingTarget, killer);
            }
            dyingTarget.Exiled();
            PlayerControl dyingLoverPartner = Lovers.bothDie ? dyingTarget.getPartner() : null; // Lover check

            Guesser.remainingShots(killerId, true);
            if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(dyingTarget.KillSfx, false, 0.8f);

            PlayerControl guesser = Helpers.playerById(killerId);
            if (FastDestroyableSingleton<HudManager>.Instance != null && guesser != null)
                if (PlayerControl.LocalPlayer == dyingTarget)
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(guesser.Data, dyingTarget.Data);
                else if (dyingLoverPartner != null && PlayerControl.LocalPlayer == dyingLoverPartner)
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(dyingLoverPartner.Data, dyingLoverPartner.Data);

            PlayerControl guessedTarget = Helpers.playerById(guessedTargetId);
            if (Guesser.showInfoInGhostChat && PlayerControl.LocalPlayer.Data.IsDead && guessedTarget != null)
            {
                RoleInfo roleInfo = RoleInfo.allRoleInfos.FirstOrDefault(x => (byte)x.roleType == guessedRoleId);
                string msg = string.Format(ModTranslation.getString("guesserGuessChat"), roleInfo.name, guessedTarget.Data.PlayerName);
                if (AmongUsClient.Instance.AmClient && FastDestroyableSingleton<HudManager>.Instance)
                    FastDestroyableSingleton<HudManager>.Instance.Chat.AddChat(guesser, msg);
                if (msg.IndexOf("who", StringComparison.OrdinalIgnoreCase) >= 0)
                    FastDestroyableSingleton<Assets.CoreScripts.Telemetry>.Instance.SendWho();
            }
        }

        public static void setBlanked(byte playerId, byte value)
        {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            Pursuer.blankedList.RemoveAll(x => x.PlayerId == playerId);
            if (value > 0) Pursuer.blankedList.Add(target);
        }

        public static void witchSpellCast(byte playerId)
        {
            uncheckedExilePlayer(playerId);
            finalStatuses[playerId] = FinalStatus.Spelled;
        }

        public static void setShifterType(bool isNeutral)
        {
            Shifter.isNeutral = isNeutral;
        }

        public static void ninjaStealth(byte playerId, bool stealthed)
        {
            PlayerControl player = Helpers.playerById(playerId);
            Ninja.setStealthed(player, stealthed);
        }

        public static void sprinterSprint(byte playerId, bool sprinting)
        {
            PlayerControl player = Helpers.playerById(playerId);
            Sprinter.setSprinting(player, sprinting);
        }

        public static void foxStealth(byte playerId, bool stealthed)
        {
            PlayerControl player = Helpers.playerById(playerId);
            Fox.setStealthed(player, stealthed);
        }

        public static void foxCreatesImmoralist(byte targetId)
        {
            PlayerControl player = Helpers.playerById(targetId);
            DestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
            erasePlayerRoles(player.PlayerId, true);
            player.setRole(RoleType.Immoralist);
            player.clearAllTasks();
        }

        public static void GMKill(byte targetId)
        {
            PlayerControl target = Helpers.playerById(targetId);

            if (target == null) return;
            target.MyPhysics.ExitAllVents();
            target.Exiled();
            finalStatuses[target.PlayerId] = FinalStatus.GMExecuted;

            PlayerControl partner = target.getPartner(); // Lover check
            if (partner != null)
            {
                partner?.MyPhysics.ExitAllVents();
                finalStatuses[partner.PlayerId] = FinalStatus.GMExecuted;
            }

            if (FastDestroyableSingleton<HudManager>.Instance != null && GM.gm != null)
            {
                if (PlayerControl.LocalPlayer == target)
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(GM.gm.Data, target.Data);
                else if (partner != null && PlayerControl.LocalPlayer == partner)
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(GM.gm.Data, partner.Data);
            }
        }
        public static void GMRevive(byte targetId)
        {
            PlayerControl target = Helpers.playerById(targetId);
            if (target == null) return;
            target.Revive();
            updateMeeting(targetId, false);
            finalStatuses[target.PlayerId] = FinalStatus.Alive;

            PlayerControl partner = target.getPartner(); // Lover check
            if (partner != null)
            {
                partner.Revive();
                updateMeeting(partner.PlayerId, false);
                finalStatuses[partner.PlayerId] = FinalStatus.Alive;
            }

            if (PlayerControl.LocalPlayer.isGM())
            {
                FastDestroyableSingleton<HudManager>.Instance.ShadowQuad.gameObject.SetActive(false);
            }
        }

        public static void updateMeeting(byte targetId, bool dead = true)
        {
            if (MeetingHud.Instance)
            {
                foreach (PlayerVoteArea pva in MeetingHud.Instance.playerStates)
                {
                    if (pva.TargetPlayerId == targetId)
                    {
                        pva.SetDead(pva.DidReport, dead);
                        pva.Overlay.gameObject.SetActive(dead);
                    }

                    // Give players back their vote if target is shot dead
                    if (Helpers.RefundVotes && dead)
                    {
                        if (pva.VotedFor != targetId) continue;
                        pva.UnsetVote();
                        var voteAreaPlayer = Helpers.playerById(pva.TargetPlayerId);
                        if (!voteAreaPlayer.AmOwner) continue;
                        MeetingHud.Instance.ClearVote();
                    }
                }

                if (AmongUsClient.Instance.AmHost)
                    MeetingHud.Instance.CheckForEndVoting();
            }
        }

        public static void useAdminTime(float time)
        {
            MapOptions.restrictAdminTime -= time;
        }

        public static void useCameraTime(float time)
        {
            MapOptions.restrictCamerasTime -= time;
        }

        public static void useVitalsTime(float time)
        {
            MapOptions.restrictVitalsTime -= time;
        }

        public static void plagueDoctorWin()
        {
            PlagueDoctor.triggerPlagueDoctorWin = true;
            var livingPlayers = PlayerControl.AllPlayerControls.ToArray().Where(p => !p.isRole(RoleType.PlagueDoctor) && p.isAlive());
            foreach (PlayerControl p in livingPlayers)
            {
                // Check again so we don't re-kill any lovers
                if (p.isAlive())
                    p.Exiled();
                finalStatuses[p.PlayerId] = FinalStatus.Diseased;
            }
        }

        public static void plagueDoctorInfected(byte targetId)
        {
            var p = Helpers.playerById(targetId);
            if (!PlagueDoctor.infected.ContainsKey(targetId))
            {
                PlagueDoctor.infected[targetId] = p;
            }
        }

        public static void plagueDoctorProgress(byte targetId, float progress)
        {
            PlagueDoctor.progress[targetId] = progress;
        }

        public static void nekoKabochaExile(byte playerId)
        {
            uncheckedExilePlayer(playerId);
            finalStatuses[playerId] = FinalStatus.Revenge;
        }

        public static void serialKillerSuicide(byte serialKillerId)
        {
            PlayerControl serialKiller = Helpers.playerById(serialKillerId);
            if (serialKiller == null) return;
            serialKiller.MurderPlayer(serialKiller);
        }

        public static void fortuneTellerUsedDivine(byte fortuneTellerId, byte targetId)
        {
            PlayerControl fortuneTeller = Helpers.playerById(fortuneTellerId);
            PlayerControl target = Helpers.playerById(targetId);
            if (target == null) return;
            if (target.isDead()) return;
            // 呪殺
            if (target.isRole(RoleType.Fox))
            {
                KillAnimationCoPerformKillPatch.hideNextAnimation = true;
                if (PlayerControl.LocalPlayer.isRole(RoleType.FortuneTeller))
                {
                    // 狐を殺せたことを分からなくするためにキル音を鳴らさないための処置
                    target.MurderPlayer(target);
                }
                else
                {
                    fortuneTeller.MurderPlayer(target);
                }
                finalStatuses[targetId] = FinalStatus.Divined;
            }

            // インポスターの場合は占い師の位置に矢印を表示
            if (PlayerControl.LocalPlayer.isImpostor())
            {
                FortuneTeller.fortuneTellerMessage(ModTranslation.getString("fortuneTellerDivinedSomeone"), 5f, Color.white);
                FortuneTeller.setDivinedFlag(fortuneTeller, true);
            }

            // 占われたのが背徳者の場合は通知を表示
            if (target.isRole(RoleType.Immoralist) && target == PlayerControl.LocalPlayer)
            {
                FortuneTeller.fortuneTellerMessage(ModTranslation.getString("fortuneTellerDivinedYou"), 5f, Color.white);
            }
        }

        public static void NormalKill(byte killerId, byte targetId)
        {
            var killer = Helpers.playerById(killerId);
            var target = Helpers.playerById(targetId);

            target.MurderPlayer(target);
            killer.transform.position = target.transform.position;
        }

        public static void EaterEat(byte killerId, byte targetId)
        {
            var killer = Helpers.playerById(killerId);
            var target = Helpers.playerById(targetId);

            killer.MurderPlayer(target);
        }

        public static void dragPlaceBody(byte playerId)
        {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++)
            {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId)
                {
                    if (!UnderTaker.dragginBody)
                    {
                        UnderTaker.dragginBody = true;
                        UnderTaker.bodyId = playerId;
                        if (PlayerControl.GameOptions.MapId == 5)
                        {
                            GameObject vent = GameObject.Find("LowerCentralVent");
                            vent.GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        UnderTaker.dragginBody = false;
                        UnderTaker.bodyId = 0;
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
                                GameObject vent = GameObject.Find("LowerCentralVent");
                                vent.GetComponent<BoxCollider2D>().enabled = true;
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

        public static void underTakerResetValues()
        {
            // Restore UnderTaker values when rewind time
            if (UnderTaker.underTaker != null && UnderTaker.dragginBody)
            {
                UnderTaker.dragginBody = false;
                UnderTaker.bodyId = 0;
            }
        }

        public static void devourBody(byte playerId)
        {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++)
            {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId)
                {
                    UnityEngine.Object.Destroy(array[i].gameObject);
                    if (UnderTaker.underTaker != null && UnderTaker.dragginBody && UnderTaker.bodyId == playerId)
                    {
                        underTakerResetValues();
                    }
                }
            }
        }/*

        public static void CreatorCreateSheriff(byte targetId)
        {
            PlayerControl player = Helpers.playerById(targetId);
            DestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
            erasePlayerRoles(player.PlayerId, true);
            player.setRole(RoleType.Sheriff);
        }

        public static void StudentPromotion()
        {
            PlayerControl player = PlayerControl.LocalPlayer;
            DestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
            erasePlayerRoles(player.PlayerId, true);
            player.setRole(RoleType.Sheriff);
        }

        public static void SheriffCreateStudent(byte playerId)
        {
            PlayerControl player = Helpers.playerById(playerId);
            DestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
            erasePlayerRoles(player.PlayerId, true);
            player.setRole(RoleType.Student);
        }*/

        public static void RPCExiled(byte targetId)
        {
            var target = Helpers.playerById(targetId);

            target.Exiled();
            FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(target.Data, target.Data);
        }

        public static void TrapperTrap(byte targetId, byte trapTime)
        {
            var target = Helpers.playerById(targetId);
            int time = (int)trapTime;

            PlayerControl.LocalPlayer.NetTransform.Halt();
            target.moveable = false; //動けなくする
            Trapper.isTrap = true;

            new LateTask(() =>
            {
                target.moveable = true;
                Trapper.isTrap = false;
            }, time, "trapperTrapTask");
        }

        public static void SilencerSilenceKill(byte targetId)
        {
            var target = Helpers.playerById(targetId);
            target.MurderPlayer(target);
        }

        public static void CamouflagedComms()
        {
            Camouflager.startCamouflage();
        }

        public static void ResetCamouflagedComms()
        {
            Camouflager.resetCamouflage();
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
        class RPCHandlerPatch
        {
            static void Postfix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
            {
                byte packetId = callId;
                switch (packetId)
                {

                    // Main Controls

                    case (byte)CustomRPC.ResetVariables:
                        RPCProcedure.resetVariables();
                        break;
                    case (byte)CustomRPC.ShareOptions:
                        RPCProcedure.ShareOptions((int)reader.ReadPackedUInt32(), reader);
                        break;
                    case (byte)CustomRPC.CrewmateEnd:
                        RPCProcedure.crewmateEnd();
                        break;
                    case (byte)CustomRPC.ImpostorEnd:
                        RPCProcedure.impostorEnd();
                        break;
                    case (byte)CustomRPC.SetRole:
                        byte roleId = reader.ReadByte();
                        byte playerId = reader.ReadByte();
                        byte flag = reader.ReadByte();
                        RPCProcedure.setRole(roleId, playerId, flag);
                        break;
                    case (byte)CustomRPC.SetLovers:
                        RPCProcedure.setLovers(reader.ReadByte(), reader.ReadByte());
                        break;
                    case (byte)CustomRPC.OverrideNativeRole:
                        RPCProcedure.overrideNativeRole(reader.ReadByte(), reader.ReadByte());
                        break;
                    case (byte)CustomRPC.VersionHandshake:
                        int major = reader.ReadPackedInt32();
                        int minor = reader.ReadPackedInt32();
                        int patch = reader.ReadPackedInt32();
                        int versionOwnerId = reader.ReadPackedInt32();
                        byte revision = 0xFF;
                        Guid guid;
                        if (reader.Length - reader.Position >= 17)
                        { // enough bytes left to read
                            revision = reader.ReadByte();
                            // GUID
                            byte[] gbytes = reader.ReadBytes(16);
                            guid = new Guid(gbytes);
                        }
                        else
                        {
                            guid = new Guid(new byte[16]);
                        }
                        RPCProcedure.versionHandshake(major, minor, patch, revision == 0xFF ? -1 : revision, guid, versionOwnerId);
                        break;
                    case (byte)CustomRPC.UseUncheckedVent:
                        int ventId = reader.ReadPackedInt32();
                        byte ventingPlayer = reader.ReadByte();
                        byte isEnter = reader.ReadByte();
                        RPCProcedure.useUncheckedVent(ventId, ventingPlayer, isEnter);
                        break;
                    case (byte)CustomRPC.UncheckedMurderPlayer:
                        byte source = reader.ReadByte();
                        byte target = reader.ReadByte();
                        byte showAnimation = reader.ReadByte();
                        RPCProcedure.uncheckedMurderPlayer(source, target, showAnimation);
                        break;
                    case (byte)CustomRPC.UncheckedExilePlayer:
                        byte exileTarget = reader.ReadByte();
                        RPCProcedure.uncheckedExilePlayer(exileTarget);
                        break;
                    case (byte)CustomRPC.UncheckedCmdReportDeadBody:
                        byte reportSource = reader.ReadByte();
                        byte reportTarget = reader.ReadByte();
                        RPCProcedure.uncheckedCmdReportDeadBody(reportSource, reportTarget);
                        break;
                    case (byte)CustomRPC.UncheckedShapeShift:
                        byte shapeshiftSource = reader.ReadByte();
                        byte shapeshiftTarget = reader.ReadByte();
                        byte useAnimation = reader.ReadByte();
                        RPCProcedure.UncheckedShapeShift(shapeshiftSource, shapeshiftTarget, useAnimation);
                        break;
                    /*case (byte)CustomRPC.UncheckedEndGame:
                        RPCProcedure.uncheckedEndGame(reader.ReadByte());
                        break;*/
                    case (byte)CustomRPC.UncheckedSetTasks:
                        RPCProcedure.uncheckedSetTasks(reader.ReadByte(), reader.ReadBytesAndSize());
                        break;
                    case (byte)CustomRPC.DynamicMapOption:
                        byte mapId = reader.ReadByte();
                        RPCProcedure.dynamicMapOption(mapId);
                        break;

                    // Role functionality

                    case (byte)CustomRPC.EngineerFixLights:
                        RPCProcedure.engineerFixLights();
                        break;
                    case (byte)CustomRPC.EngineerUsedRepair:
                        RPCProcedure.engineerUsedRepair();
                        break;
                    case (byte)CustomRPC.EngineerFixSubmergedOxygen:
                        RPCProcedure.engineerFixSubmergedOxygen();
                        break;
                    case (byte)CustomRPC.CleanBody:
                        RPCProcedure.cleanBody(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SheriffKill:
                        RPCProcedure.sheriffKill(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.GunKill:
                        RPCProcedure.gunKill(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.StaffKill:
                        RPCProcedure.staffKill(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.BossKill:
                        RPCProcedure.bossKill(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.TimeMasterRewindTime:
                        RPCProcedure.timeMasterRewindTime();
                        break;
                    case (byte)CustomRPC.TimeMasterShield:
                        RPCProcedure.timeMasterShield();
                        break;
                    case (byte)CustomRPC.MedicSetShielded:
                        RPCProcedure.medicSetShielded(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.ShieldedMurderAttempt:
                        RPCProcedure.shieldedMurderAttempt();
                        break;
                    case (byte)CustomRPC.ShifterShift:
                        RPCProcedure.shifterShift(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SwapperSwap:
                        byte playerId1 = reader.ReadByte();
                        byte playerId2 = reader.ReadByte();
                        RPCProcedure.swapperSwap(playerId1, playerId2);
                        break;
                    case (byte)CustomRPC.MorphlingMorph:
                        RPCProcedure.morphlingMorph(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.CamouflagerCamouflage:
                        RPCProcedure.camouflagerCamouflage();
                        break;
                    case (byte)CustomRPC.VampireSetBitten:
                        byte bittenId = reader.ReadByte();
                        byte reset = reader.ReadByte();
                        RPCProcedure.vampireSetBitten(bittenId, reset);
                        break;
                    case (byte)CustomRPC.PlaceGarlic:
                        RPCProcedure.placeGarlic(reader.ReadBytesAndSize());
                        break;
                    case (byte)CustomRPC.EvilHackerCreatesMadmate:
                        RPCProcedure.evilHackerCreatesMadmate(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.TrackerUsedTracker:
                        RPCProcedure.trackerUsedTracker(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.JackalCreatesSidekick:
                        RPCProcedure.jackalCreatesSidekick(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SidekickPromotes:
                        RPCProcedure.sidekickPromotes();
                        break;
                    case (byte)CustomRPC.ErasePlayerRoles:
                        RPCProcedure.erasePlayerRoles(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SetFutureErased:
                        RPCProcedure.setFutureErased(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SetFutureShifted:
                        RPCProcedure.setFutureShifted(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SetFutureShielded:
                        RPCProcedure.setFutureShielded(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.PlacePortal:
                        RPCProcedure.placePortal(reader.ReadBytesAndSize());
                        break;
                    case (byte)CustomRPC.UsePortal:
                        RPCProcedure.usePortal(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.PlaceAssassinTrace:
                        RPCProcedure.placeAssassinTrace(reader.ReadBytesAndSize());
                        break;
                    case (byte)CustomRPC.PlaceJackInTheBox:
                        RPCProcedure.placeJackInTheBox(reader.ReadBytesAndSize());
                        break;
                    case (byte)CustomRPC.LightsOut:
                        RPCProcedure.lightsOut();
                        break;
                    case (byte)CustomRPC.PlaceCamera:
                        RPCProcedure.placeCamera(reader.ReadBytesAndSize(), reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SealVent:
                        RPCProcedure.sealVent(reader.ReadPackedInt32());
                        break;
                    case (byte)CustomRPC.ArsonistWin:
                        RPCProcedure.arsonistWin();
                        break;
                    case (byte)CustomRPC.GuesserShoot:
                        byte killerId = reader.ReadByte();
                        byte dyingTarget = reader.ReadByte();
                        byte guessedTarget = reader.ReadByte();
                        byte guessedRoleId = reader.ReadByte();
                        RPCProcedure.guesserShoot(killerId, dyingTarget, guessedTarget, guessedRoleId);
                        break;
                    case (byte)CustomRPC.VultureWin:
                        RPCProcedure.vultureWin();
                        break;
                    case (byte)CustomRPC.LawyerWin:
                        RPCProcedure.lawyerWin();
                        break;
                    case (byte)CustomRPC.LawyerSetTarget:
                        RPCProcedure.lawyerSetTarget(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.LawyerPromotesToPursuer:
                        RPCProcedure.lawyerPromotesToPursuer();
                        break;
                    case (byte)CustomRPC.SetBlanked:
                        var pid = reader.ReadByte();
                        var blankedValue = reader.ReadByte();
                        RPCProcedure.setBlanked(pid, blankedValue);
                        break;
                    case (byte)CustomRPC.SetFutureSpelled:
                        RPCProcedure.setFutureSpelled(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.WitchSpellCast:
                        RPCProcedure.witchSpellCast(reader.ReadByte());
                        break;

                    // GM functionality
                    case (byte)CustomRPC.AddModifier:
                        RPCProcedure.addModifier(reader.ReadByte(), reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SetShifterType:
                        RPCProcedure.setShifterType(reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.NinjaStealth:
                        RPCProcedure.ninjaStealth(reader.ReadByte(), reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.ArsonistDouse:
                        RPCProcedure.arsonistDouse(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.VultureEat:
                        RPCProcedure.vultureEat(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SprinterSprint:
                        RPCProcedure.sprinterSprint(reader.ReadByte(), reader.ReadBoolean());
                        break;

                    case (byte)CustomRPC.GMKill:
                        RPCProcedure.GMKill(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.GMRevive:
                        RPCProcedure.GMRevive(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.UseAdminTime:
                        RPCProcedure.useAdminTime(reader.ReadSingle());
                        break;
                    case (byte)CustomRPC.UseCameraTime:
                        RPCProcedure.useCameraTime(reader.ReadSingle());
                        break;
                    case (byte)CustomRPC.UseVitalsTime:
                        RPCProcedure.useVitalsTime(reader.ReadSingle());
                        break;
                    case (byte)CustomRPC.PlagueDoctorWin:
                        RPCProcedure.plagueDoctorWin();
                        break;
                    case (byte)CustomRPC.PlagueDoctorSetInfected:
                        RPCProcedure.plagueDoctorInfected(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.PlagueDoctorUpdateProgress:
                        byte progressTarget = reader.ReadByte();
                        byte[] progressByte = reader.ReadBytes(4);
                        float progress = System.BitConverter.ToSingle(progressByte, 0);
                        RPCProcedure.plagueDoctorProgress(progressTarget, progress);
                        break;
                    case (byte)CustomRPC.NekoKabochaExile:
                        RPCProcedure.nekoKabochaExile(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SerialKillerSuicide:
                        RPCProcedure.serialKillerSuicide(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SwapperAnimate:
                        RPCProcedure.swapperAnimate();
                        break;
                    case (byte)CustomRPC.FortuneTellerUsedDivine:
                        byte fId = reader.ReadByte();
                        byte tId = reader.ReadByte();
                        RPCProcedure.fortuneTellerUsedDivine(fId, tId);
                        break;
                    case (byte)CustomRPC.FoxStealth:
                        RPCProcedure.foxStealth(reader.ReadByte(), reader.ReadBoolean());
                        break;
                    case (byte)CustomRPC.FoxCreatesImmoralist:
                        RPCProcedure.foxCreatesImmoralist(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.NormalKill:
                        RPCProcedure.NormalKill(reader.ReadByte(), reader.ReadByte());
                        break;
                    case (byte)CustomRPC.DragPlaceBody:
                        RPCProcedure.dragPlaceBody(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.DevourBody:
                        RPCProcedure.devourBody(reader.ReadByte());
                        break;/*
                    case (byte)CustomRPC.CreatorCreateSheriff:
                        RPCProcedure.CreatorCreateSheriff(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.StudentPromotion:
                        RPCProcedure.StudentPromotion();
                        break;
                    case (byte)CustomRPC.SheriffCreateStudent:
                        RPCProcedure.SheriffCreateStudent(reader.ReadByte());
                        break;*/
                    case (byte)CustomRPC.RPCExiled:
                        RPCProcedure.RPCExiled(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.TrapperTrap:
                        RPCProcedure.TrapperTrap(reader.ReadByte(), reader.ReadByte());
                        break;
                    case (byte)CustomRPC.SilencerSilenceKill:
                        RPCProcedure.SilencerSilenceKill(reader.ReadByte());
                        break;
                    case (byte)CustomRPC.CamouflagedComms:
                        RPCProcedure.CamouflagedComms();
                        break;
                    case (byte)CustomRPC.ResetCamouflagedComms:
                        RPCProcedure.ResetCamouflagedComms();
                        break;
                    case (byte)CustomRPC.EaterEat:
                        RPCProcedure.EaterEat(reader.ReadByte(), reader.ReadByte());
                        break;
                }
            }
        }
    }
}

/* RPCの呼び出し方
MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.RPCの名前, Hazel.SendOption.Reliable, -1);
writer.Write(引数);
AmongUsClient.Instance.FinishRpcImmediately(writer);
RPCProcedure.RPCの名前(引数);
*/