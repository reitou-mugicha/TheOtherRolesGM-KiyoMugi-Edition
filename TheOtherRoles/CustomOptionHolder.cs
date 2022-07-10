using System.Collections.Generic;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.TheOtherRolesGM;
using static TheOtherRoles.CustomOption;

namespace TheOtherRoles
{

    public class CustomOptionHolder
    {
        public static string[] rates = new string[] { "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%" };
        public static string[] presets = new string[] { "preset1", "preset2", "preset3", "preset4", "preset5" };

        public static CustomOption presetSelection;
        public static CustomOption activateRoles;
        public static CustomOption randomGen;
        public static CustomOption crewmateRolesCountMin;
        public static CustomOption crewmateRolesCountMax;
        public static CustomOption neutralRolesCountMin;
        public static CustomOption neutralRolesCountMax;
        public static CustomOption impostorRolesCountMin;
        public static CustomOption impostorRolesCountMax;

        public static CustomRoleOption mafiaSpawnRate;
        public static CustomOption mafiosoCanSabotage;
        public static CustomOption mafiosoCanRepair;
        public static CustomOption mafiosoCanVent;
        public static CustomOption janitorCooldown;
        public static CustomOption janitorCanSabotage;
        public static CustomOption janitorCanRepair;
        public static CustomOption janitorCanVent;

        public static CustomRoleOption morphlingSpawnRate;
        public static CustomOption morphlingCooldown;
        public static CustomOption morphlingDuration;

        public static CustomRoleOption camouflagerSpawnRate;
        public static CustomOption camouflagerCooldown;
        public static CustomOption camouflagerDuration;
        public static CustomOption camouflagerRandomColors;

        public static CustomRoleOption evilHackerSpawnRate;
        public static CustomOption evilHackerCanHasBetterAdmin;
        public static CustomOption evilHackerCanCreateMadmate;
        public static CustomOption evilHackerCanCreateMadmateFromFox;
        public static CustomOption evilHackerCanCreateMadmateFromJackal;
        public static CustomOption evilHackerCanMoveEvenIfUsesAdmin;
        public static CustomOption createdMadmateCanDieToSheriffOrYakuza;
        public static CustomOption createdMadmateCanEnterVents;
        public static CustomOption createdMadmateHasImpostorVision;
        public static CustomOption createdMadmateCanSabotage;
        public static CustomOption createdMadmateCanFixComm;
        public static CustomOption createdMadmateAbility;
        public static CustomOption createdMadmateNumTasks;
        public static CustomOption createdMadmateExileCrewmate;

        public static CustomRoleOption vampireSpawnRate;
        public static CustomOption vampireKillDelay;
        public static CustomOption vampireCooldown;
        public static CustomOption vampireCanKillNearGarlics;

        public static CustomRoleOption eraserSpawnRate;
        public static CustomOption eraserCooldown;
        public static CustomOption eraserCooldownIncrease;
        public static CustomOption eraserCanEraseAnyone;

        public static CustomRoleOption hawkEyeSpawnRate;
        public static CustomOption hawkZoomCooldown;
        public static CustomOption hawkEyeTime;
        public static CustomOption hawkCanUseVents;

        public static CustomRoleOption miniSpawnRate;
        public static CustomOption miniGrowingUpDuration;

        public static CustomRoleOption loversSpawnRate;
        public static CustomOption loversNumCouples;
        public static CustomOption loversImpLoverRate;
        public static CustomOption loversBothDie;
        public static CustomOption loversCanHaveAnotherRole;
        public static CustomOption loversSeparateTeam;
        public static CustomOption loversTasksCount;
        public static CustomOption loversEnableChat;

        public static CustomRoleOption guesserSpawnRate;
        public static CustomOption guesserIsImpGuesserRate;
        public static CustomOption guesserNumberOfShots;
        public static CustomOption guesserOnlyAvailableRoles;
        public static CustomOption guesserHasMultipleShotsPerMeeting;
        public static CustomOption guesserShowInfoInGhostChat;
        public static CustomOption guesserKillsThroughShield;
        public static CustomOption guesserEvilCanKillSpy;
        public static CustomOption guesserSpawnBothRate;
        public static CustomOption guesserCantGuessSnitchIfTaksDone;

        public static CustomRoleOption mayorSpawnRate;
        public static CustomOption mayorNumVotes;
        public static CustomOption mayorMeetingButton;
        public static CustomOption mayorNumMeetingButton;

        public static CustomRoleOption jesterSpawnRate;
        public static CustomOption jesterCanCallEmergency;
        public static CustomOption jesterCanSabotage;
        public static CustomOption jesterHasImpostorVision;

        public static CustomRoleOption arsonistSpawnRate;
        public static CustomOption arsonistCooldown;
        public static CustomOption arsonistDuration;
        public static CustomOption arsonistCanBeLovers;

        public static CustomRoleOption jackalSpawnRate;
        public static CustomOption jackalKillCooldown;
        public static CustomOption jackalCreateSidekickCooldown;
        public static CustomOption jackalCanUseVents;
        public static CustomOption jackalCanCreateSidekick;
        public static CustomOption sidekickPromotesToJackal;
        public static CustomOption sidekickCanKill;
        public static CustomOption sidekickCanUseVents;
        public static CustomOption jackalPromotedFromSidekickCanCreateSidekick;
        public static CustomOption jackalCanCreateSidekickFromImpostor;
        public static CustomOption jackalCanCreateSidekickFromFox;
        public static CustomOption jackalAndSidekickHaveImpostorVision;
        public static CustomOption jackalCanSeeEngineerVent;

        public static CustomRoleOption bountyHunterSpawnRate;
        public static CustomOption bountyHunterBountyDuration;
        public static CustomOption bountyHunterReducedCooldown;
        public static CustomOption bountyHunterPunishmentTime;
        public static CustomOption bountyHunterShowArrow;
        public static CustomOption bountyHunterArrowUpdateIntervall;

        public static CustomRoleOption witchSpawnRate;
        public static CustomOption witchCooldown;
        public static CustomOption witchAdditionalCooldown;
        public static CustomOption witchCanSpellAnyone;
        public static CustomOption witchSpellCastingDuration;
        public static CustomOption witchTriggerBothCooldowns;
        public static CustomOption witchVoteSavesTargets;

        public static CustomRoleOption shifterSpawnRate;
        public static CustomOption shifterIsNeutralRate;
        public static CustomOption shifterShiftsModifiers;
        public static CustomOption shifterPastShifters;

        public static CustomRoleOption fortuneTellerSpawnRate;
        public static CustomOption fortuneTellerNumTasks;
        public static CustomOption fortuneTellerResults;
        public static CustomOption fortuneTellerDistance;
        public static CustomOption fortuneTellerDuration;

        public static CustomRoleOption engineerSpawnRate;
        public static CustomOption engineerNumberOfFixes;
        public static CustomOption engineerHighlightForImpostors;
        public static CustomOption engineerHighlightForTeamJackal;

        public static CustomRoleOption sheriffSpawnRate;
        public static CustomOption sheriffCooldown;
        public static CustomOption sheriffNumShots;
        public static CustomOption sheriffCanKillNeutrals;
        public static CustomOption sheriffMisfireKillsTarget;

        public static CustomRoleOption lighterSpawnRate;
        public static CustomOption lighterModeLightsOnVision;
        public static CustomOption lighterModeLightsOffVision;
        public static CustomOption lighterCooldown;
        public static CustomOption lighterDuration;
        public static CustomOption lighterCanSeeNinja;

        public static CustomRoleOption detectiveSpawnRate;
        public static CustomOption detectiveAnonymousFootprints;
        public static CustomOption detectiveFootprintIntervall;
        public static CustomOption detectiveFootprintDuration;
        public static CustomOption detectiveReportNameDuration;
        public static CustomOption detectiveReportColorDuration;

        public static CustomRoleOption timeMasterSpawnRate;
        public static CustomOption timeMasterCooldown;
        public static CustomOption timeMasterRewindTime;
        public static CustomOption timeMasterShieldDuration;

        public static CustomRoleOption medicSpawnRate;
        public static CustomOption medicShowShielded;
        public static CustomOption medicShowAttemptToShielded;
        public static CustomOption medicSetShieldAfterMeeting;
        public static CustomOption medicShowAttemptToMedic;

        public static CustomRoleOption swapperSpawnRate;
        public static CustomOption swapperIsImpRate;
        public static CustomOption swapperCanCallEmergency;
        public static CustomOption swapperCanOnlySwapOthers;
        public static CustomOption swapperNumSwaps;

        public static CustomRoleOption seerSpawnRate;
        public static CustomOption seerMode;
        public static CustomOption seerSoulDuration;
        public static CustomOption seerLimitSoulDuration;

        public static CustomRoleOption hackerSpawnRate;
        public static CustomOption hackerCooldown;
        public static CustomOption hackerHackeringDuration;
        public static CustomOption hackerOnlyColorType;
        public static CustomOption hackerToolsNumber;
        public static CustomOption hackerRechargeTasksNumber;
        public static CustomOption hackerNoMove;

        public static CustomRoleOption trackerSpawnRate;
        public static CustomOption trackerUpdateIntervall;
        public static CustomOption trackerResetTargetAfterMeeting;
        public static CustomOption trackerCanTrackCorpses;
        public static CustomOption trackerCorpsesTrackingCooldown;
        public static CustomOption trackerCorpsesTrackingDuration;

        public static CustomRoleOption snitchSpawnRate;
        public static CustomOption snitchLeftTasksForReveal;
        public static CustomOption snitchIncludeTeamJackal;
        public static CustomOption snitchTeamJackalUseDifferentArrowColor;

        public static CustomRoleOption spySpawnRate;
        public static CustomOption spyCanDieToSheriffOrYakuza;
        public static CustomOption spyImpostorsCanKillAnyone;
        public static CustomOption spyCanEnterVents;
        public static CustomOption spyHasImpostorVision;

        public static CustomRoleOption tricksterSpawnRate;
        public static CustomOption tricksterPlaceBoxCooldown;
        public static CustomOption tricksterLightsOutCooldown;
        public static CustomOption tricksterLightsOutDuration;

        public static CustomRoleOption cleanerSpawnRate;
        public static CustomOption cleanerCooldown;

        public static CustomRoleOption warlockSpawnRate;
        public static CustomOption warlockCooldown;
        public static CustomOption warlockRootTime;

        public static CustomRoleOption securityGuardSpawnRate;
        public static CustomOption securityGuardCooldown;
        public static CustomOption securityGuardTotalScrews;
        public static CustomOption securityGuardCamPrice;
        public static CustomOption securityGuardVentPrice;
        public static CustomOption securityGuardCamDuration;
        public static CustomOption securityGuardCamMaxCharges;
        public static CustomOption securityGuardCamRechargeTasksNumber;
        public static CustomOption securityGuardNoMove;

        public static CustomRoleOption baitSpawnRate;
        public static CustomOption baitHighlightAllVents;
        public static CustomOption baitReportDelay;
        public static CustomOption baitShowKillFlash;

        public static CustomRoleOption vultureSpawnRate;
        public static CustomOption vultureCooldown;
        public static CustomOption vultureNumberToWin;
        public static CustomOption vultureCanUseVents;
        public static CustomOption vultureShowArrows;

        public static CustomRoleOption mediumSpawnRate;
        public static CustomOption mediumCooldown;
        public static CustomOption mediumDuration;
        public static CustomOption mediumOneTimeUse;

        public static CustomRoleOption lawyerSpawnRate;
        public static CustomOption lawyerTargetKnows;
        public static CustomOption lawyerWinsAfterMeetings;
        public static CustomOption lawyerNeededMeetings;
        public static CustomOption lawyerVision;
        public static CustomOption lawyerKnowsRole;
        public static CustomOption pursuerCooldown;
        public static CustomOption pursuerBlanksNumber;

        public static CustomOption specialOptions;
        public static CustomOption maxNumberOfMeetings;
        public static CustomOption blockSkippingInEmergencyMeetings;
        public static CustomOption noVoteIsSelfVote;
        public static CustomOption hidePlayerNames;

        public static CustomOption allowParallelMedBayScans;

        public static CustomOption ventAnimation;
        public static CustomOption enableDiePlayerZoomInOut;
        public static CustomOption onePlayerStart;
        public static CustomOption airshipReactorDuration;
        public static CustomOption helloweenMode;
        public static CustomOption oldAirShipAdmin;
        public static CustomOption enableRecordsAdmin;

        public static CustomOption betterStartButtons;

        public static CustomOption enableMirrorMap;
        public static CustomOption enableAddCustomVent;

        public static CustomOption camouflagedComms;

        public static CustomOption dynamicMap;
        public static CustomOption dynamicMapEnableSkeld;
        public static CustomOption dynamicMapEnableMira;
        public static CustomOption dynamicMapEnablePolus;
        public static CustomOption dynamicMapEnableDleks;
        public static CustomOption dynamicMapEnableAirShip;
        public static CustomOption dynamicMapEnableSubmerged;

        // GM Edition options
        public static CustomRoleOption madmateSpawnRate;
        public static CustomOption madmateCanDieToSheriffOrYakuza;
        public static CustomOption madmateCanEnterVents;
        public static CustomOption madmateHasImpostorVision;
        public static CustomOption madmateCanSabotage;
        public static CustomOption madmateCanFixComm;
        public static CustomOption madmateType;
        public static CustomRoleSelectionOption madmateFixedRole;
        public static CustomOption madmateAbility;
        public static CustomTasksOption madmateTasks;
        public static CustomOption madmateExilePlayer;
        public static CustomOption taskHackerEnabled;
        public static CustomOption taskHackerAddCrewNumTasks;
        public static CustomOption taskHackerNumTasks;

        public static CustomRoleOption opportunistSpawnRate;

        public static CustomRoleOption ninjaSpawnRate;
        public static CustomOption ninjaStealthCooldown;
        public static CustomOption ninjaStealthDuration;
        public static CustomOption ninjaKillPenalty;
        public static CustomOption ninjaSpeedBonus;
        public static CustomOption ninjaFadeTime;
        public static CustomOption ninjaCanVent;
        public static CustomOption ninjaCanBeTargeted;

        public static CustomOption gmEnabled;
        public static CustomOption gmIsHost;
        public static CustomOption gmHasTasks;
        public static CustomOption gmDiesAtStart;
        public static CustomOption gmCanWarp;
        public static CustomOption gmCanKill;

        public static CustomRoleOption plagueDoctorSpawnRate;
        public static CustomOption plagueDoctorInfectCooldown;
        public static CustomOption plagueDoctorNumInfections;
        public static CustomOption plagueDoctorDistance;
        public static CustomOption plagueDoctorDuration;
        public static CustomOption plagueDoctorImmunityTime;
        public static CustomOption plagueDoctorInfectKiller;
        public static CustomOption plagueDoctorResetMeeting;
        public static CustomOption plagueDoctorWinDead;

        public static CustomRoleOption nekoKabochaSpawnRate;
        public static CustomOption nekoKabochaRevengeCrew;
        public static CustomOption nekoKabochaRevengeNeutral;
        public static CustomOption nekoKabochaRevengeImpostor;
        public static CustomOption nekoKabochaRevengeExile;

        public static CustomRoleOption watcherSpawnRate;

        public static CustomOption hideSettings;
        public static CustomOption restrictDevices;
        public static CustomOption restrictAdmin;
        public static CustomOption restrictCameras;
        public static CustomOption restrictVitals;

        public static CustomOption hideOutOfSightNametags;
        public static CustomOption refundVotesOnDeath;

        public static CustomOption uselessOptions;
        public static CustomOption playerColorRandom;
        public static CustomOption playerNameDupes;
        public static CustomOption disableVents;

        public static CustomRoleOption serialKillerSpawnRate;
        public static CustomOption serialKillerKillCooldown;
        public static CustomOption serialKillerSuicideTimer;
        public static CustomOption serialKillerResetTimer;

        public static CustomRoleOption foxSpawnRate;
        public static CustomOption foxCanFixReactorAndO2;
        public static CustomOption foxCanCreateImmoralist;
        public static CustomOption foxNumRepair;
        public static CustomOption foxCrewWinsByTasks;
        public static CustomOption foxStealthCooldown;
        public static CustomOption foxStealthDuration;
        public static CustomTasksOption foxTasks;

        public static CustomRoleOption sprinterSpawnRate;
        public static CustomOption sprinterCooldown;
        public static CustomOption sprinterDuration;
        public static CustomOption sprinterSpeedBonus;

        //public static CustomRoleOption kingdomSpawnRate;
        //public static CustomTasksOption kingTasks;

        public static CustomRoleOption portalmakerSpawnRate;
        public static CustomOption portalmakerCooldown;
        public static CustomOption portalmakerUsePortalCooldown;
        public static CustomOption enablePortalLog;
        public static CustomOption portalmakerLogOnlyColorType;
        public static CustomOption portalmakerLogHasTime;

        public static CustomRoleOption assassinSpawnRate;
        public static CustomOption assassinCooldown;
        public static CustomOption assassinKnowsTargetLocation;
        public static CustomOption assassinStartAssassin;
        public static CustomOption assassinTraceTime;
        public static CustomOption assassinTraceColorTime;

        public static CustomRoleOption antiTeleportSpawnRate;

        public static CustomRoleOption sunglassesSpawnRate;
        public static CustomOption sunglass;

        public static CustomRoleOption customImpostorSpawnRate;
        public static CustomOption customImpostorKillCooldown;
        public static CustomOption customImpostorCanUseVents;
        public static CustomOption customImpostorCanSabotage;

        public static CustomRoleOption coolSpawnRate;

        public static CustomRoleOption doubleKillerSpawnRate;
        public static CustomOption doubleKillerKillButtonCooldown;
/*
        public static CustomRoleOption chunibyoSpawnRate;
        public static CustomOption chunibyoCooldown;
        public static CustomOption chunibyoDuration;
        public static CustomOption chunibyoEnableSpeedUp;
        public static CustomOption chunibyoEnableSpeedDown;
        public static CustomOption chunibyoEnableHighVision;
        public static CustomOption chunibyoEnableLowVision;
        public static CustomOption chunibyoAddNoneAbility;
        public static CustomOption chunibyoSpeedUpBonus;
        public static CustomOption chunibyoSpeedDownBonus;
        public static CustomOption chunibyoHighVisionBonus;
        public static CustomOption chunibyoLowVisionBonus;
*/
        public static CustomRoleOption yakuzaSpawnRate;
        public static CustomOption yakuzaKillCooldown;
        public static CustomOption yakuzaNumShots;
        public static CustomOption yakuzaShotsShare;
        public static CustomOption yakuzaCanKillNeutrals;
        public static CustomOption yakuzaMisfireKillsTarget;

        public static CustomRoleOption underTakerSpawnRate;
        public static CustomOption underTakerSpeed;

        public static CustomRoleOption randomizerSpawnRate;
        public static CustomOption randomizerKillCooldownMin;
        public static CustomOption randomizerKillCooldownMax;

        public static CustomRoleOption acceleratorSpawnRate;
        public static CustomOption acceleratorDecreaseCooldown;

        public static CustomRoleOption bakerySpawnRate;
        public static CustomOption bakeryEnableBombBread;
        public static CustomOption bakeryBombBreadRate;

        public static CustomRoleOption trapperSpawnRate;
        public static CustomOption trapperTrapCooldown;
        public static CustomOption trapperTrapTime;

        public static CustomRoleOption timeReviverSpawnRate;
        public static CustomOption timeReviverDeathReport;
        public static CustomOption timeReviverExileAddMeetingTime;
        public static CustomOption timeReviverTimeCutoffCooldown;
        public static CustomOption timeReviverTimeCutoffDuration;

        public static CustomRoleOption silencerSpawnRate;
        public static CustomOption silencerSilenceKillCooldown;

        public static CustomRoleOption jammerSpawnRate;
        public static CustomOption jammerAliveFriendlyFire;

        public static CustomRoleOption immortalitySpawnRate;
        public static CustomOption immortalityMeetingStartSuicideTime;

        internal static Dictionary<byte, byte[]> blockedRolePairings = new Dictionary<byte, byte[]>();

        public static string cs(Color c, string s)
        {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>{4}</color>", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a), s);
        }

        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }

        public static void Load()
        {

            // Role Options
            activateRoles = CustomOption.Create(1, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "selectMode"), true, null, true);

            presetSelection = CustomOption.Create(2, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "presetSelection"), presets, null, true);

            randomGen = CustomOption.Create(3, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "randomGen"), true, null, true);

            // Using new id's for the options to not break compatibilty with older versions
            crewmateRolesCountMin = CustomOption.Create(10, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "crewmateRolesCountMin"), 0f, 0f, 15f, 1f, null, true);
            crewmateRolesCountMax = CustomOption.Create(11, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "crewmateRolesCountMax"), 0f, 0f, 15f, 1f);
            neutralRolesCountMin = CustomOption.Create(12, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "neutralRolesCountMin"), 0f, 0f, 15f, 1f);
            neutralRolesCountMax = CustomOption.Create(13, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "neutralRolesCountMax"), 0f, 0f, 15f, 1f);
            impostorRolesCountMin = CustomOption.Create(14, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "impostorRolesCountMin"), 0f, 0f, 15f, 1f);
            impostorRolesCountMax = CustomOption.Create(15, CustomOptionType.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "impostorRolesCountMax"), 0f, 0f, 15f, 1f);

            gmEnabled = CustomOption.Create(100, CustomOptionType.Other, cs(GM.color, "gm"), false, null, true);
            gmIsHost = CustomOption.Create(101, CustomOptionType.Other, "gmIsHost", true, gmEnabled);
            //gmHasTasks = CustomOption.Create(102, CustomOptionType.Other, "gmHasTasks", false, gmEnabled);
            gmCanWarp = CustomOption.Create(103, CustomOptionType.Other, "gmCanWarp", true, gmEnabled);
            gmCanKill = CustomOption.Create(104, CustomOptionType.Other, "gmCanKill", false, gmEnabled);
            gmDiesAtStart = CustomOption.Create(105, CustomOptionType.Other, "gmDiesAtStart", true, gmEnabled);

            mafiaSpawnRate = new CustomRoleOption(110, CustomOptionType.Impostor, "mafia", Janitor.color, 1);
            mafiosoCanSabotage = CustomOption.Create(111, CustomOptionType.Impostor, "mafiosoCanSabotage", false, mafiaSpawnRate);
            mafiosoCanRepair = CustomOption.Create(112, CustomOptionType.Impostor, "mafiosoCanRepair", false, mafiaSpawnRate);
            mafiosoCanVent = CustomOption.Create(113, CustomOptionType.Impostor, "mafiosoCanVent", false, mafiaSpawnRate);
            janitorCooldown = CustomOption.Create(114, CustomOptionType.Impostor, "janitorCooldown", 30f, 2.5f, 60f, 2.5f, mafiaSpawnRate, format: "unitSeconds");
            janitorCanSabotage = CustomOption.Create(115, CustomOptionType.Impostor, "janitorCanSabotage", false, mafiaSpawnRate);
            janitorCanRepair = CustomOption.Create(116, CustomOptionType.Impostor, "janitorCanRepair", false, mafiaSpawnRate);
            janitorCanVent = CustomOption.Create(117, CustomOptionType.Impostor, "janitorCanVent", false, mafiaSpawnRate);

            morphlingSpawnRate = new CustomRoleOption(120, CustomOptionType.Impostor, "morphling", Morphling.color, 1);
            morphlingCooldown = CustomOption.Create(121, CustomOptionType.Impostor, "morphlingCooldown", 30f, 2.5f, 60f, 2.5f, morphlingSpawnRate, format: "unitSeconds");
            morphlingDuration = CustomOption.Create(122, CustomOptionType.Impostor, "morphlingDuration", 10f, 1f, 20f, 0.5f, morphlingSpawnRate, format: "unitSeconds");

            camouflagerSpawnRate = new CustomRoleOption(130, CustomOptionType.Impostor, "camouflager", Camouflager.color, 1);
            camouflagerCooldown = CustomOption.Create(131, CustomOptionType.Impostor, "camouflagerCooldown", 30f, 2.5f, 60f, 2.5f, camouflagerSpawnRate, format: "unitSeconds");
            camouflagerDuration = CustomOption.Create(132, CustomOptionType.Impostor, "camouflagerDuration", 10f, 1f, 20f, 0.5f, camouflagerSpawnRate, format: "unitSeconds");
            camouflagerRandomColors = CustomOption.Create(133, CustomOptionType.Impostor, "camouflagerRandomColors", false, camouflagerSpawnRate);

            evilHackerSpawnRate = new CustomRoleOption(140, CustomOptionType.Impostor, "evilHacker", EvilHacker.color, 1);
            evilHackerCanHasBetterAdmin = CustomOption.Create(141, CustomOptionType.Impostor, "evilHackerCanHasBetterAdmin", false, evilHackerSpawnRate);
            evilHackerCanCreateMadmate = CustomOption.Create(142, CustomOptionType.Impostor, "evilHackerCanCreateMadmate", false, evilHackerSpawnRate);
            evilHackerCanMoveEvenIfUsesAdmin = CustomOption.Create(153, CustomOptionType.Impostor, "evilHackerCanMoveEvenIfUsesAdmin", true, evilHackerSpawnRate);
            createdMadmateCanDieToSheriffOrYakuza = CustomOption.Create(143, CustomOptionType.Impostor, "createdMadmateCanDieToSheriff", false, evilHackerCanCreateMadmate);
            createdMadmateCanEnterVents = CustomOption.Create(144, CustomOptionType.Impostor, "createdMadmateCanEnterVents", false, evilHackerCanCreateMadmate);
            evilHackerCanCreateMadmateFromFox = CustomOption.Create(145, CustomOptionType.Impostor, "evilHackerCanCreateMadmateFromFox", false, evilHackerCanCreateMadmate);
            evilHackerCanCreateMadmateFromJackal = CustomOption.Create(146, CustomOptionType.Impostor, "evilHackerCanCreateMadmateFromJackal", false, evilHackerCanCreateMadmate);
            createdMadmateHasImpostorVision = CustomOption.Create(147, CustomOptionType.Impostor, "createdMadmateHasImpostorVision", false, evilHackerCanCreateMadmate);
            createdMadmateCanSabotage = CustomOption.Create(148, CustomOptionType.Impostor, "createdMadmateCanSabotage", false, evilHackerCanCreateMadmate);
            createdMadmateCanFixComm = CustomOption.Create(149, CustomOptionType.Impostor, "createdMadmateCanFixComm", true, evilHackerCanCreateMadmate);
            createdMadmateAbility = CustomOption.Create(150, CustomOptionType.Impostor, "madmateAbility", new string[] { "madmateNone", "madmateFanatic" }, evilHackerCanCreateMadmate);
            createdMadmateNumTasks = CustomOption.Create(151, CustomOptionType.Impostor, "createdMadmateNumTasks", 4f, 1f, 20f, 1f, createdMadmateAbility);
            createdMadmateExileCrewmate = CustomOption.Create(152, CustomOptionType.Impostor, "createdMadmateExileCrewmate", false, evilHackerCanCreateMadmate);

            vampireSpawnRate = new CustomRoleOption(160, CustomOptionType.Impostor, "vampire", Vampire.color, 1);
            vampireKillDelay = CustomOption.Create(161, CustomOptionType.Impostor, "vampireKillDelay", 10f, 1f, 20f, 1f, vampireSpawnRate, format: "unitSeconds");
            vampireCooldown = CustomOption.Create(162, CustomOptionType.Impostor, "vampireCooldown", 30f, 2.5f, 60f, 2.5f, vampireSpawnRate, format: "unitSeconds");
            vampireCanKillNearGarlics = CustomOption.Create(163, CustomOptionType.Impostor, "vampireCanKillNearGarlics", true, vampireSpawnRate);

            eraserSpawnRate = new CustomRoleOption(170, CustomOptionType.Impostor, "eraser", Eraser.color, 1);
            eraserCooldown = CustomOption.Create(171, CustomOptionType.Impostor, "eraserCooldown", 30f, 5f, 120f, 5f, eraserSpawnRate, format: "unitSeconds");
            eraserCooldownIncrease = CustomOption.Create(172, CustomOptionType.Impostor, "eraserCooldownIncrease", 10f, 0f, 120f, 2.5f, eraserSpawnRate, format: "unitSeconds");
            eraserCanEraseAnyone = CustomOption.Create(173, CustomOptionType.Impostor, "eraserCanEraseAnyone", false, eraserSpawnRate);

            tricksterSpawnRate = new CustomRoleOption(180, CustomOptionType.Impostor, "trickster", Trickster.color, 1);
            tricksterPlaceBoxCooldown = CustomOption.Create(181, CustomOptionType.Impostor, "tricksterPlaceBoxCooldown", 10f, 2.5f, 30f, 2.5f, tricksterSpawnRate, format: "unitSeconds");
            tricksterLightsOutCooldown = CustomOption.Create(182, CustomOptionType.Impostor, "tricksterLightsOutCooldown", 30f, 5f, 60f, 5f, tricksterSpawnRate, format: "unitSeconds");
            tricksterLightsOutDuration = CustomOption.Create(183, CustomOptionType.Impostor, "tricksterLightsOutDuration", 15f, 5f, 60f, 2.5f, tricksterSpawnRate, format: "unitSeconds");

            cleanerSpawnRate = new CustomRoleOption(190, CustomOptionType.Impostor, "cleaner", Cleaner.color, 1);
            cleanerCooldown = CustomOption.Create(191, CustomOptionType.Impostor, "cleanerCooldown", 30f, 2.5f, 60f, 2.5f, cleanerSpawnRate, format: "unitSeconds");

            warlockSpawnRate = new CustomRoleOption(200, CustomOptionType.Impostor, "warlock", Warlock.color, 1);
            warlockCooldown = CustomOption.Create(201, CustomOptionType.Impostor, "warlockCooldown", 30f, 2.5f, 60f, 2.5f, warlockSpawnRate, format: "unitSeconds");
            warlockRootTime = CustomOption.Create(202, CustomOptionType.Impostor, "warlockRootTime", 5f, 0f, 15f, 1f, warlockSpawnRate, format: "unitSeconds");

            bountyHunterSpawnRate = new CustomRoleOption(210, CustomOptionType.Impostor, "bountyHunter", BountyHunter.color, 1);
            bountyHunterBountyDuration = CustomOption.Create(211, CustomOptionType.Impostor, "bountyHunterBountyDuration", 60f, 10f, 180f, 10f, bountyHunterSpawnRate, format: "unitSeconds");
            bountyHunterReducedCooldown = CustomOption.Create(212, CustomOptionType.Impostor, "bountyHunterReducedCooldown", 2.5f, 2.5f, 30f, 2.5f, bountyHunterSpawnRate, format: "unitSeconds");
            bountyHunterPunishmentTime = CustomOption.Create(213, CustomOptionType.Impostor, "bountyHunterPunishmentTime", 20f, 0f, 60f, 2.5f, bountyHunterSpawnRate, format: "unitSeconds");
            bountyHunterShowArrow = CustomOption.Create(214, CustomOptionType.Impostor, "bountyHunterShowArrow", true, bountyHunterSpawnRate);
            bountyHunterArrowUpdateIntervall = CustomOption.Create(215, CustomOptionType.Impostor, "bountyHunterArrowUpdateIntervall", 15f, 2.5f, 60f, 2.5f, bountyHunterShowArrow, format: "unitSeconds");

            witchSpawnRate = new CustomRoleOption(220, CustomOptionType.Impostor, "witch", Witch.color, 1);
            witchCooldown = CustomOption.Create(221, CustomOptionType.Impostor, "witchSpellCooldown", 30f, 2.5f, 120f, 2.5f, witchSpawnRate, format: "unitSeconds");
            witchAdditionalCooldown = CustomOption.Create(222, CustomOptionType.Impostor, "witchAdditionalCooldown", 10f, 0f, 60f, 5f, witchSpawnRate, format: "unitSeconds");
            witchCanSpellAnyone = CustomOption.Create(223, CustomOptionType.Impostor, "witchCanSpellAnyone", false, witchSpawnRate);
            witchSpellCastingDuration = CustomOption.Create(224, CustomOptionType.Impostor, "witchSpellDuration", 1f, 0f, 10f, 1f, witchSpawnRate, format: "unitSeconds");
            witchTriggerBothCooldowns = CustomOption.Create(225, CustomOptionType.Impostor, "witchTriggerBoth", true, witchSpawnRate);
            witchVoteSavesTargets = CustomOption.Create(226, CustomOptionType.Impostor, "witchSaveTargets", true, witchSpawnRate);

            ninjaSpawnRate = new CustomRoleOption(230, CustomOptionType.Impostor, "ninja", Ninja.color, 3);
            ninjaStealthCooldown = CustomOption.Create(231, CustomOptionType.Impostor, "ninjaStealthCooldown", 30f, 2.5f, 60f, 2.5f, ninjaSpawnRate, format: "unitSeconds");
            ninjaStealthDuration = CustomOption.Create(232, CustomOptionType.Impostor, "ninjaStealthDuration", 15f, 2.5f, 60f, 2.5f, ninjaSpawnRate, format: "unitSeconds");
            ninjaFadeTime = CustomOption.Create(233, CustomOptionType.Impostor, "ninjaFadeTime", 0.5f, 0.0f, 2.5f, 0.5f, ninjaSpawnRate, format: "unitSeconds");
            ninjaKillPenalty = CustomOption.Create(234, CustomOptionType.Impostor, "ninjaKillPenalty", 10f, 0f, 60f, 2.5f, ninjaSpawnRate, format: "unitSeconds");
            ninjaSpeedBonus = CustomOption.Create(235, CustomOptionType.Impostor, "ninjaSpeedBonus", 125f, 50f, 200f, 5f, ninjaSpawnRate, format: "unitPercent");
            ninjaCanBeTargeted = CustomOption.Create(236, CustomOptionType.Impostor, "ninjaCanBeTargeted", true, ninjaSpawnRate);
            ninjaCanVent = CustomOption.Create(237, CustomOptionType.Impostor, "ninjaCanVent", false, ninjaSpawnRate);

            serialKillerSpawnRate = new CustomRoleOption(240, CustomOptionType.Impostor, "serialKiller", SerialKiller.color, 3);
            serialKillerKillCooldown = CustomOption.Create(241, CustomOptionType.Impostor, "serialKillerKillCooldown", 15f, 2.5f, 60f, 2.5f, serialKillerSpawnRate, format: "unitSeconds");
            serialKillerSuicideTimer = CustomOption.Create(242, CustomOptionType.Impostor, "serialKillerSuicideTimer", 40f, 2.5f, 60f, 2.5f, serialKillerSpawnRate, format: "unitSeconds");
            serialKillerResetTimer = CustomOption.Create(243, CustomOptionType.Impostor, "serialKillerResetTimer", true, serialKillerSpawnRate);

            nekoKabochaSpawnRate = new CustomRoleOption(250, CustomOptionType.Impostor, "nekoKabocha", NekoKabocha.color, 3);
            nekoKabochaRevengeCrew = CustomOption.Create(251, CustomOptionType.Impostor, "nekoKabochaRevengeCrew", true, nekoKabochaSpawnRate);
            nekoKabochaRevengeNeutral = CustomOption.Create(252, CustomOptionType.Impostor, "nekoKabochaRevengeNeutral", true, nekoKabochaSpawnRate);
            nekoKabochaRevengeImpostor = CustomOption.Create(253, CustomOptionType.Impostor, "nekoKabochaRevengeImpostor", true, nekoKabochaSpawnRate);
            nekoKabochaRevengeExile = CustomOption.Create(254, CustomOptionType.Impostor, "nekoKabochaRevengeExile", false, nekoKabochaSpawnRate);

            hawkEyeSpawnRate = new CustomRoleOption(260, CustomOptionType.Impostor, "hawkEye", HawkEye.color, 1);
            hawkZoomCooldown = CustomOption.Create(261, CustomOptionType.Impostor, "hawkZoomCooldown", 30f, 2.5f, 60f, 2.5f, hawkEyeSpawnRate, format: "unitSeconds");
            hawkEyeTime = CustomOption.Create(262, CustomOptionType.Impostor, "hawkTime", 5f, 5f, 30f, 5f, hawkEyeSpawnRate, format: "unitSeconds");
            hawkCanUseVents = CustomOption.Create(263, CustomOptionType.Impostor, "hawkCanUseVents", false, hawkEyeSpawnRate);

            assassinSpawnRate = new CustomRoleOption(270, CustomOptionType.Impostor, "assassin", Assassin.color, 1);
            assassinCooldown = CustomOption.Create(271, CustomOptionType.Impostor, "assassinMarkCooldown", 30f, 10f, 120f, 5f, assassinSpawnRate, format: "unitSeconds");
            assassinKnowsTargetLocation = CustomOption.Create(272, CustomOptionType.Impostor, "assassinKnowsLocationOfTarget", true, assassinSpawnRate);
            assassinStartAssassin = CustomOption.Create(273, CustomOptionType.Impostor, "startAssassin", 5f, 2.5f, 30f, 2.5f, assassinSpawnRate, format: "unitSeconds");
            assassinTraceTime = CustomOption.Create(274, CustomOptionType.Impostor, "traceDuration", 5f, 1f, 20f, 0.5f, assassinSpawnRate, format: "unitSeconds");
            assassinTraceColorTime = CustomOption.Create(275, CustomOptionType.Impostor, "timeTillTraceColorHasFaded", 2f, 0f, 20f, 0.5f, assassinSpawnRate, format: "unitSeconds");

            customImpostorSpawnRate = new CustomRoleOption(650, CustomOptionType.Impostor, "customImpostor", CustomImpostor.color, 15);
            customImpostorKillCooldown = CustomOption.Create(651, CustomOptionType.Impostor, "customImpostorKillCooldown", 20f, 2.5f, 60f, 2.5f, customImpostorSpawnRate, format: "unitSeconds");
            customImpostorCanUseVents = CustomOption.Create(652, CustomOptionType.Impostor, "customImpostorCanUseVents", false, customImpostorSpawnRate);
            customImpostorCanSabotage = CustomOption.Create(653, CustomOptionType.Impostor, "customImpostorCanSabotage", false, customImpostorSpawnRate);

            underTakerSpawnRate = new CustomRoleOption(700, CustomOptionType.Impostor, "underTaker", UnderTaker.color, 1);
            underTakerSpeed = CustomOption.Create(701, CustomOptionType.Impostor, "underTakerSpeed", 125f, 50f, 200f, 5f, underTakerSpawnRate, format: "unitPercent");

            randomizerSpawnRate = new CustomRoleOption(980, CustomOptionType.Impostor, "randomizer", Randomizer.color, 1);
            randomizerKillCooldownMin = CustomOption.Create(981, CustomOptionType.Impostor, "randomizerKillCooldownMin", 2.5f, 2.5f, 60f, 2.5f, randomizerSpawnRate, format: "unitSeconds");
            randomizerKillCooldownMax = CustomOption.Create(982, CustomOptionType.Impostor, "randomizerKillCooldownMax", 30f, 2.5f, 60f, 2.5f, randomizerSpawnRate, format: "unitSeconds");

            acceleratorSpawnRate = new CustomRoleOption(7550, CustomOptionType.Impostor, "accelerator", Accelerator.color, 1);
            acceleratorDecreaseCooldown = CustomOption.Create(7551, CustomOptionType.Impostor, "acceleratorDecreaseCooldown", 2f, 1f, 60f, 1f, acceleratorSpawnRate, format: "unitSeconds");

            trapperSpawnRate = new CustomRoleOption(7225, CustomOptionType.Impostor, "trapper", Trapper.color, 1);
            trapperTrapCooldown = CustomOption.Create(7226, CustomOptionType.Impostor, "trapperTrapCooldown", 30f, 2.5f, 60f, 2.5f, trapperSpawnRate, format: "unitSeconds");
            trapperTrapTime = CustomOption.Create(7227, CustomOptionType.Impostor, "trapperTrapTime", 5f, 2.5f, 60f, 2.5f, trapperSpawnRate, format: "unitSeconds");

            silencerSpawnRate = new CustomRoleOption(1090, CustomOptionType.Impostor, "silencer", Silencer.color, 1);
            silencerSilenceKillCooldown = CustomOption.Create(1091, CustomOptionType.Impostor, "silencerSilenceKillCooldown", 45f, 2.5f, 60f, 2.5f, silencerSpawnRate, format: "unitSeconds");

            madmateSpawnRate = new CustomRoleOption(280, CustomOptionType.Modifier, "madmate", Madmate.color);
            madmateType = CustomOption.Create(281, CustomOptionType.Modifier, "madmateType", new string[] { "madmateDefault", "madmateWithRole", "madmateRandom" }, madmateSpawnRate);
            madmateFixedRole = new CustomRoleSelectionOption(282, CustomOptionType.Modifier, "madmateFixedRole", Madmate.validRoles, madmateType);
            madmateAbility = CustomOption.Create(283, CustomOptionType.Modifier, "madmateAbility", new string[] { "madmateNone", "madmateFanatic" }, madmateSpawnRate);
            madmateTasks = new CustomTasksOption(284, CustomOptionType.Modifier, 1, 1, 3, madmateAbility);
            madmateCanDieToSheriffOrYakuza = CustomOption.Create(285, CustomOptionType.Modifier, "madmateCanDieToSheriffOrYakuza", false, madmateSpawnRate);
            madmateCanEnterVents = CustomOption.Create(286, CustomOptionType.Modifier, "madmateCanEnterVents", false, madmateSpawnRate);
            madmateHasImpostorVision = CustomOption.Create(287, CustomOptionType.Modifier, "madmateHasImpostorVision", false, madmateSpawnRate);
            madmateCanSabotage = CustomOption.Create(288, CustomOptionType.Modifier, "madmateCanSabotage", false, madmateSpawnRate);
            madmateCanFixComm = CustomOption.Create(289, CustomOptionType.Modifier, "madmateCanFixComm", true, madmateSpawnRate);
            madmateExilePlayer = CustomOption.Create(290, CustomOptionType.Modifier, "madmateExileCrewmate", false, madmateSpawnRate);
            //taskHackerEnabled = CustomOption.Create(291, CustomOptionType.Modifier, "taskHackerEnabled", false, madmateSpawnRate);
            //taskHackerNumTasks = CustomOption.Create(292, CustomOptionType.Modifier, "taskHackerAdditionalNumTasks", 5f, 1f, 10f, 1f, taskHackerEnabled);
            //taskHackerAddCrewNumTasks = CustomOption.Create(293, CustomOptionType.Modifier, "taskHackerAdditionalCrewNumTasks", 5f, 1f, 10f, 1f, taskHackerEnabled);

            miniSpawnRate = new CustomRoleOption(300, CustomOptionType.Other, "mini", Mini.color, 1);
            miniGrowingUpDuration = CustomOption.Create(301, CustomOptionType.Other, "miniGrowingUpDuration", 400f, 100f, 1500f, 100f, miniSpawnRate, format: "unitSeconds");

            loversSpawnRate = new CustomRoleOption(310, CustomOptionType.Other, "lovers", Lovers.color, 1);
            loversImpLoverRate = CustomOption.Create(311, CustomOptionType.Other, "loversImpLoverRate", rates, loversSpawnRate);
            loversNumCouples = CustomOption.Create(312, CustomOptionType.Other, "loversNumCouples", 1f, 1f, 7f, 1f, loversSpawnRate, format: "unitCouples");
            loversBothDie = CustomOption.Create(313, CustomOptionType.Other, "loversBothDie", true, loversSpawnRate);
            loversCanHaveAnotherRole = CustomOption.Create(314, CustomOptionType.Other, "loversCanHaveAnotherRole", true, loversSpawnRate);
            loversSeparateTeam = CustomOption.Create(315, CustomOptionType.Other, "loversSeparateTeam", true, loversSpawnRate);
            loversTasksCount = CustomOption.Create(316, CustomOptionType.Other, "loversTasksCount", false, loversSpawnRate);
            loversEnableChat = CustomOption.Create(317, CustomOptionType.Other, "loversEnableChat", true, loversSpawnRate);

            guesserSpawnRate = new CustomRoleOption(320, CustomOptionType.Other, "guesser", Guesser.color, 1);
            guesserIsImpGuesserRate = CustomOption.Create(321, CustomOptionType.Other, "guesserIsImpGuesserRate", rates, guesserSpawnRate);
            guesserSpawnBothRate = CustomOption.Create(322, CustomOptionType.Other, "guesserSpawnBothRate", rates, guesserSpawnRate);
            guesserNumberOfShots = CustomOption.Create(323, CustomOptionType.Other, "guesserNumberOfShots", 2f, 1f, 15f, 1f, guesserSpawnRate, format: "unitShots");
            guesserOnlyAvailableRoles = CustomOption.Create(324, CustomOptionType.Other, "guesserOnlyAvailableRoles", true, guesserSpawnRate);
            guesserHasMultipleShotsPerMeeting = CustomOption.Create(325, CustomOptionType.Other, "guesserHasMultipleShotsPerMeeting", false, guesserSpawnRate);
            guesserShowInfoInGhostChat = CustomOption.Create(326, CustomOptionType.Other, "guesserToGhostChat", true, guesserSpawnRate);
            guesserKillsThroughShield = CustomOption.Create(327, CustomOptionType.Other, "guesserPierceShield", true, guesserSpawnRate);
            guesserEvilCanKillSpy = CustomOption.Create(328, CustomOptionType.Other, "guesserEvilCanKillSpy", true, guesserSpawnRate);
            guesserCantGuessSnitchIfTaksDone = CustomOption.Create(329, CustomOptionType.Other, "guesserCantGuessSnitchIfTaksDone", true, guesserSpawnRate);

            swapperSpawnRate = new CustomRoleOption(330, CustomOptionType.Other, "swapper", Swapper.color, 1);
            swapperIsImpRate = CustomOption.Create(331, CustomOptionType.Other, "swapperIsImpRate", rates, swapperSpawnRate);
            swapperNumSwaps = CustomOption.Create(332, CustomOptionType.Other, "swapperNumSwaps", 2f, 1f, 15f, 1f, swapperSpawnRate, format: "unitTimes");
            swapperCanCallEmergency = CustomOption.Create(333, CustomOptionType.Other, "swapperCanCallEmergency", false, swapperSpawnRate);
            swapperCanOnlySwapOthers = CustomOption.Create(334, CustomOptionType.Other, "swapperCanOnlySwapOthers", false, swapperSpawnRate);

            jesterSpawnRate = new CustomRoleOption(340, CustomOptionType.Neutral, "jester", Jester.color, 1);
            jesterCanCallEmergency = CustomOption.Create(341, CustomOptionType.Neutral, "jesterCanCallEmergency", true, jesterSpawnRate);
            jesterCanSabotage = CustomOption.Create(342, CustomOptionType.Neutral, "jesterCanSabotage", true, jesterSpawnRate);
            jesterHasImpostorVision = CustomOption.Create(343, CustomOptionType.Neutral, "jesterHasImpostorVision", false, jesterSpawnRate);

            arsonistSpawnRate = new CustomRoleOption(350, CustomOptionType.Neutral, "arsonist", Arsonist.color, 1);
            arsonistCooldown = CustomOption.Create(351, CustomOptionType.Neutral, "arsonistCooldown", 12.5f, 2.5f, 60f, 2.5f, arsonistSpawnRate, format: "unitSeconds");
            arsonistDuration = CustomOption.Create(352, CustomOptionType.Neutral, "arsonistDuration", 3f, 0f, 10f, 1f, arsonistSpawnRate, format: "unitSeconds");
            arsonistCanBeLovers = CustomOption.Create(353, CustomOptionType.Neutral, "arsonistCanBeLovers", false, arsonistSpawnRate);

            opportunistSpawnRate = new CustomRoleOption(360, CustomOptionType.Modifier, "opportunist", Opportunist.color);

            jackalSpawnRate = new CustomRoleOption(365, CustomOptionType.Neutral, "jackal", Jackal.color, 1);
            jackalKillCooldown = CustomOption.Create(366, CustomOptionType.Neutral, "jackalKillCooldown", 30f, 2.5f, 60f, 2.5f, jackalSpawnRate, format: "unitSeconds");
            jackalCanUseVents = CustomOption.Create(367, CustomOptionType.Neutral, "jackalCanUseVents", true, jackalSpawnRate);
            jackalAndSidekickHaveImpostorVision = CustomOption.Create(368, CustomOptionType.Neutral, "jackalAndSidekickHaveImpostorVision", false, jackalSpawnRate);
            jackalCanCreateSidekick = CustomOption.Create(369, CustomOptionType.Neutral, "jackalCanCreateSidekick", false, jackalSpawnRate);
            jackalCreateSidekickCooldown = CustomOption.Create(370, CustomOptionType.Neutral, "jackalCreateSidekickCooldown", 30f, 2.5f, 60f, 2.5f, jackalCanCreateSidekick, format: "unitSeconds");
            sidekickPromotesToJackal = CustomOption.Create(371, CustomOptionType.Neutral, "sidekickPromotesToJackal", false, jackalCanCreateSidekick);
            sidekickCanKill = CustomOption.Create(372, CustomOptionType.Neutral, "sidekickCanKill", false, jackalCanCreateSidekick);
            sidekickCanUseVents = CustomOption.Create(373, CustomOptionType.Neutral, "sidekickCanUseVents", true, jackalCanCreateSidekick);
            jackalPromotedFromSidekickCanCreateSidekick = CustomOption.Create(374, CustomOptionType.Neutral, "jackalPromotedFromSidekickCanCreateSidekick", true, jackalCanCreateSidekick);
            jackalCanCreateSidekickFromImpostor = CustomOption.Create(375, CustomOptionType.Neutral, "jackalCanCreateSidekickFromImpostor", true, jackalCanCreateSidekick);
            jackalCanCreateSidekickFromFox = CustomOption.Create(376, CustomOptionType.Neutral, "jackalCanCreateSidekickFromFox", true, jackalCanCreateSidekick);

            antiTeleportSpawnRate = new CustomRoleOption(630, CustomOptionType.Modifier, "antiTeleport", AntiTeleport.color, 15);

            sunglassesSpawnRate = new CustomRoleOption(640, CustomOptionType.Modifier, "sunglasses", Sunglasses.color, 15);
            sunglass = CustomOption.Create(641, CustomOptionType.Modifier, "sunglassesEye", 50f, 10f, 90f, 10f, sunglassesSpawnRate, format: "unitPercent");

            vultureSpawnRate = new CustomRoleOption(390, CustomOptionType.Neutral, "vulture", Vulture.color, 1);
            vultureCooldown = CustomOption.Create(391, CustomOptionType.Neutral, "vultureCooldown", 15f, 2.5f, 60f, 2.5f, vultureSpawnRate, format: "unitSeconds");
            vultureNumberToWin = CustomOption.Create(392, CustomOptionType.Neutral, "vultureNumberToWin", 4f, 1f, 12f, 1f, vultureSpawnRate);
            vultureCanUseVents = CustomOption.Create(393, CustomOptionType.Neutral, "vultureCanUseVents", true, vultureSpawnRate);
            vultureShowArrows = CustomOption.Create(394, CustomOptionType.Neutral, "vultureShowArrows", true, vultureSpawnRate);

            lawyerSpawnRate = new CustomRoleOption(400, CustomOptionType.Neutral, "lawyer", Lawyer.color, 1);
            lawyerTargetKnows = CustomOption.Create(401, CustomOptionType.Neutral, "lawyerTargetKnows", true, lawyerSpawnRate);
            lawyerWinsAfterMeetings = CustomOption.Create(402, CustomOptionType.Neutral, "lawyerWinsMeeting", false, lawyerSpawnRate);
            lawyerNeededMeetings = CustomOption.Create(403, CustomOptionType.Neutral, "lawyerMeetingsNeeded", 5f, 1f, 15f, 1f, lawyerWinsAfterMeetings);
            lawyerVision = CustomOption.Create(404, CustomOptionType.Neutral, "lawyerVision", 1f, 0.25f, 3f, 0.25f, lawyerSpawnRate, format: "unitMultiplier");
            lawyerKnowsRole = CustomOption.Create(405, CustomOptionType.Neutral, "lawyerKnowsRole", false, lawyerSpawnRate);
            pursuerCooldown = CustomOption.Create(406, CustomOptionType.Neutral, "pursuerBlankCool", 30f, 2.5f, 60f, 2.5f, lawyerSpawnRate, format: "unitSeconds");
            pursuerBlanksNumber = CustomOption.Create(407, CustomOptionType.Neutral, "pursuerNumBlanks", 5f, 0f, 20f, 1f, lawyerSpawnRate, format: "unitShots");

            shifterSpawnRate = new CustomRoleOption(410, CustomOptionType.Other, "shifter", Shifter.color, 1);
            shifterIsNeutralRate = CustomOption.Create(411, CustomOptionType.Other, "shifterIsNeutralRate", rates, shifterSpawnRate);
            shifterShiftsModifiers = CustomOption.Create(412, CustomOptionType.Other, "shifterShiftsModifiers", false, shifterSpawnRate);
            shifterPastShifters = CustomOption.Create(413, CustomOptionType.Other, "shifterPastShifters", false, shifterSpawnRate);

            plagueDoctorSpawnRate = new CustomRoleOption(420, CustomOptionType.Neutral, "plagueDoctor", PlagueDoctor.color, 1);
            plagueDoctorInfectCooldown = CustomOption.Create(421, CustomOptionType.Neutral, "plagueDoctorInfectCooldown", 10f, 2.5f, 60f, 2.5f, plagueDoctorSpawnRate, format: "unitSeconds");
            plagueDoctorNumInfections = CustomOption.Create(422, CustomOptionType.Neutral, "plagueDoctorNumInfections", 1f, 1f, 15, 1f, plagueDoctorSpawnRate, format: "unitPlayers");
            plagueDoctorDistance = CustomOption.Create(423, CustomOptionType.Neutral, "plagueDoctorDistance", 1.0f, 0.25f, 5.0f, 0.25f, plagueDoctorSpawnRate, format: "unitMeters");
            plagueDoctorDuration = CustomOption.Create(424, CustomOptionType.Neutral, "plagueDoctorDuration", 5f, 1f, 30f, 1f, plagueDoctorSpawnRate, format: "unitSeconds");
            plagueDoctorImmunityTime = CustomOption.Create(425, CustomOptionType.Neutral, "plagueDoctorImmunityTime", 10f, 1f, 30f, 1f, plagueDoctorSpawnRate, format: "unitSeconds");
            //plagueDoctorResetMeeting = CustomOption.Create(426, CustomOptionType.Neutral, "plagueDoctorResetMeeting", false, plagueDoctorSpawnRate);
            plagueDoctorInfectKiller = CustomOption.Create(427, CustomOptionType.Neutral, "plagueDoctorInfectKiller", true, plagueDoctorSpawnRate);
            plagueDoctorWinDead = CustomOption.Create(428, CustomOptionType.Neutral, "plagueDoctorWinDead", true, plagueDoctorSpawnRate);

            watcherSpawnRate = new CustomRoleOption(430, CustomOptionType.Modifier, "watcher", Watcher.color, 15);

            foxSpawnRate = new CustomRoleOption(440, CustomOptionType.Neutral, "fox", Fox.color, 1);
            foxCanFixReactorAndO2 = CustomOption.Create(441, CustomOptionType.Neutral, "foxCanFixReactorAndO2", false, foxSpawnRate);
            foxCrewWinsByTasks = CustomOption.Create(442, CustomOptionType.Neutral, "foxCrewWinsByTasks", true, foxSpawnRate);
            foxTasks = new CustomTasksOption(443, CustomOptionType.Neutral, 1, 1, 3, foxSpawnRate);
            foxStealthCooldown = CustomOption.Create(444, CustomOptionType.Neutral, "foxStealthCooldown", 15f, 1f, 30f, 1f, foxSpawnRate, format: "unitSeconds");
            foxStealthDuration = CustomOption.Create(445, CustomOptionType.Neutral, "foxStealthDuration", 15f, 1f, 30f, 1f, foxSpawnRate, format: "unitSeconds");
            foxCanCreateImmoralist = CustomOption.Create(446, CustomOptionType.Neutral, "foxCanCreateImmoralist", true, foxSpawnRate);
            foxNumRepair = CustomOption.Create(447, CustomOptionType.Neutral, "foxNumRepair", 1f, 0f, 5f, 1f, foxSpawnRate, format: "unitTimes");

            fortuneTellerSpawnRate = new CustomRoleOption(450, CustomOptionType.Crewmate, "fortuneTeller", FortuneTeller.color, 15);
            fortuneTellerNumTasks = CustomOption.Create(451, CustomOptionType.Crewmate, "fortuneTellerNumTasks", 4f, 0f, 25f, 1f, fortuneTellerSpawnRate);
            fortuneTellerResults = CustomOption.Create(452, CustomOptionType.Crewmate, "fortuneTellerResults ", new string[] { "fortuneTellerResultCrew", "fortuneTellerResultTeam", "fortuneTellerResultRole" }, fortuneTellerSpawnRate);
            fortuneTellerDuration = CustomOption.Create(453, CustomOptionType.Crewmate, "fortuneTellerDuration ", 20f, 1f, 50f, 0.5f, fortuneTellerSpawnRate, format: "unitSeconds");
            fortuneTellerDistance = CustomOption.Create(454, CustomOptionType.Crewmate, "fortuneTellerDistance ", 2.5f, 1f, 10f, 0.5f, fortuneTellerSpawnRate, format: "unitMeters");

            engineerSpawnRate = new CustomRoleOption(460, CustomOptionType.Crewmate, "engineer", Engineer.color, 1);
            engineerNumberOfFixes = CustomOption.Create(461, CustomOptionType.Crewmate, "engineerNumFixes", 1f, 0f, 3f, 1f, engineerSpawnRate);
            engineerHighlightForImpostors = CustomOption.Create(462, CustomOptionType.Crewmate, "engineerImpostorsSeeVent", true, engineerSpawnRate);
            engineerHighlightForTeamJackal = CustomOption.Create(463, CustomOptionType.Crewmate, "engineerJackalSeeVent", true, engineerSpawnRate);

            sheriffSpawnRate = new CustomRoleOption(470, CustomOptionType.Crewmate, "sheriff", Sheriff.color, 15);
            sheriffCooldown = CustomOption.Create(471, CustomOptionType.Crewmate, "sheriffCooldown", 30f, 2.5f, 60f, 2.5f, sheriffSpawnRate, format: "unitSeconds");
            sheriffNumShots = CustomOption.Create(472, CustomOptionType.Crewmate, "sheriffNumShots", 2f, 1f, 15f, 1f, sheriffSpawnRate, format: "unitShots");
            sheriffMisfireKillsTarget = CustomOption.Create(473, CustomOptionType.Crewmate, "sheriffMisfireKillsTarget", false, sheriffSpawnRate);
            sheriffCanKillNeutrals = CustomOption.Create(474, CustomOptionType.Crewmate, "sheriffCanKillNeutrals", false, sheriffSpawnRate);

            mayorSpawnRate = new CustomRoleOption(480, CustomOptionType.Crewmate, "mayor", Mayor.color, 1);
            mayorNumVotes = CustomOption.Create(481, CustomOptionType.Crewmate, "mayorNumVotes", 2f, 2f, 10f, 1f, mayorSpawnRate, format: "unitVotes");
            mayorMeetingButton = CustomOption.Create(482, CustomOptionType.Crewmate, "mayorMeetingButton", true, mayorSpawnRate);
            mayorNumMeetingButton = CustomOption.Create(483, CustomOptionType.Crewmate, "mayorNumMeetingButton", 1f, 1f, 10f, 1f, mayorMeetingButton, format: "unitTimes");

            lighterSpawnRate = new CustomRoleOption(490, CustomOptionType.Crewmate, "lighter", Lighter.color, 15);
            lighterModeLightsOnVision = CustomOption.Create(491, CustomOptionType.Crewmate, "lighterModeLightsOnVision", 2f, 0.25f, 5f, 0.25f, lighterSpawnRate, format: "unitMultiplier");
            lighterModeLightsOffVision = CustomOption.Create(492, CustomOptionType.Crewmate, "lighterModeLightsOffVision", 0.75f, 0.25f, 5f, 0.25f, lighterSpawnRate, format: "unitMultiplier");
            lighterCooldown = CustomOption.Create(493, CustomOptionType.Crewmate, "lighterCooldown", 30f, 5f, 120f, 5f, lighterSpawnRate, format: "unitSeconds");
            lighterDuration = CustomOption.Create(494, CustomOptionType.Crewmate, "lighterDuration", 5f, 2.5f, 60f, 2.5f, lighterSpawnRate, format: "unitSeconds");
            lighterCanSeeNinja = CustomOption.Create(495, CustomOptionType.Crewmate, "lighterCanSeeNinja", true, lighterSpawnRate);

            detectiveSpawnRate = new CustomRoleOption(500, CustomOptionType.Crewmate, "detective", Detective.color, 1);
            detectiveAnonymousFootprints = CustomOption.Create(501, CustomOptionType.Crewmate, "detectiveAnonymousFootprints", false, detectiveSpawnRate);
            detectiveFootprintIntervall = CustomOption.Create(502, CustomOptionType.Crewmate, "detectiveFootprintIntervall", 0.5f, 0.25f, 10f, 0.25f, detectiveSpawnRate, format: "unitSeconds");
            detectiveFootprintDuration = CustomOption.Create(503, CustomOptionType.Crewmate, "detectiveFootprintDuration", 5f, 0.25f, 10f, 0.25f, detectiveSpawnRate, format: "unitSeconds");
            detectiveReportNameDuration = CustomOption.Create(504, CustomOptionType.Crewmate, "detectiveReportNameDuration", 0, 0, 60, 2.5f, detectiveSpawnRate, format: "unitSeconds");
            detectiveReportColorDuration = CustomOption.Create(505, CustomOptionType.Crewmate, "detectiveReportColorDuration", 20, 0, 120, 2.5f, detectiveSpawnRate, format: "unitSeconds");

            timeMasterSpawnRate = new CustomRoleOption(510, CustomOptionType.Crewmate, "timeMaster", TimeMaster.color, 1);
            timeMasterCooldown = CustomOption.Create(511, CustomOptionType.Crewmate, "timeMasterCooldown", 30f, 2.5f, 120f, 2.5f, timeMasterSpawnRate, format: "unitSeconds");
            timeMasterRewindTime = CustomOption.Create(512, CustomOptionType.Crewmate, "timeMasterRewindTime", 3f, 1f, 10f, 1f, timeMasterSpawnRate, format: "unitSeconds");
            timeMasterShieldDuration = CustomOption.Create(513, CustomOptionType.Crewmate, "timeMasterShieldDuration", 3f, 1f, 20f, 1f, timeMasterSpawnRate, format: "unitSeconds");

            medicSpawnRate = new CustomRoleOption(520, CustomOptionType.Crewmate, "medic", Medic.color, 1);
            medicShowShielded = CustomOption.Create(521, CustomOptionType.Crewmate, "medicShowShielded", new string[] { "medicShowShieldedAll", "medicShowShieldedBoth", "medicShowShieldedMedic" }, medicSpawnRate);
            medicShowAttemptToShielded = CustomOption.Create(522, CustomOptionType.Crewmate, "medicShowAttemptToShielded", false, medicSpawnRate);
            medicSetShieldAfterMeeting = CustomOption.Create(523, CustomOptionType.Crewmate, "medicSetShieldAfterMeeting", false, medicSpawnRate);
            medicShowAttemptToMedic = CustomOption.Create(524, CustomOptionType.Crewmate, "medicSeesMurderAttempt", false, medicSpawnRate);

            seerSpawnRate = new CustomRoleOption(530, CustomOptionType.Crewmate, "seer", Seer.color, 1);
            seerMode = CustomOption.Create(531, CustomOptionType.Crewmate, "seerMode", new string[] { "seerModeBoth", "seerModeFlash", "seerModeSouls" }, seerSpawnRate);
            seerLimitSoulDuration = CustomOption.Create(532, CustomOptionType.Crewmate, "seerLimitSoulDuration", false, seerSpawnRate);
            seerSoulDuration = CustomOption.Create(533, CustomOptionType.Crewmate, "seerSoulDuration", 15f, 0f, 120f, 5f, seerLimitSoulDuration, format: "unitSeconds");

            hackerSpawnRate = new CustomRoleOption(540, CustomOptionType.Crewmate, "hacker", Hacker.color, 1);
            hackerCooldown = CustomOption.Create(541, CustomOptionType.Crewmate, "hackerCooldown", 30f, 5f, 60f, 5f, hackerSpawnRate, format: "unitSeconds");
            hackerHackeringDuration = CustomOption.Create(542, CustomOptionType.Crewmate, "hackerHackeringDuration", 10f, 2.5f, 60f, 2.5f, hackerSpawnRate, format: "unitSeconds");
            hackerOnlyColorType = CustomOption.Create(543, CustomOptionType.Crewmate, "hackerOnlyColorType", false, hackerSpawnRate);
            hackerToolsNumber = CustomOption.Create(544, CustomOptionType.Crewmate, "hackerToolsNumber", 5f, 1f, 30f, 1f, hackerSpawnRate);
            hackerRechargeTasksNumber = CustomOption.Create(545, CustomOptionType.Crewmate, "hackerRechargeTasksNumber", 2f, 1f, 5f, 1f, hackerSpawnRate);
            hackerNoMove = CustomOption.Create(546, CustomOptionType.Crewmate, "hackerNoMove", true, hackerSpawnRate);

            trackerSpawnRate = new CustomRoleOption(550, CustomOptionType.Crewmate, "tracker", Tracker.color, 1);
            trackerUpdateIntervall = CustomOption.Create(551, CustomOptionType.Crewmate, "trackerUpdateIntervall", 5f, 1f, 30f, 1f, trackerSpawnRate, format: "unitSeconds");
            trackerResetTargetAfterMeeting = CustomOption.Create(552, CustomOptionType.Crewmate, "trackerResetTargetAfterMeeting", false, trackerSpawnRate);
            trackerCanTrackCorpses = CustomOption.Create(553, CustomOptionType.Crewmate, "trackerTrackCorpses", true, trackerSpawnRate);
            trackerCorpsesTrackingCooldown = CustomOption.Create(554, CustomOptionType.Crewmate, "trackerCorpseCooldown", 30f, 0f, 120f, 5f, trackerCanTrackCorpses, format: "unitSeconds");
            trackerCorpsesTrackingDuration = CustomOption.Create(555, CustomOptionType.Crewmate, "trackerCorpseDuration", 5f, 2.5f, 30f, 2.5f, trackerCanTrackCorpses, format: "unitSeconds");

            snitchSpawnRate = new CustomRoleOption(560, CustomOptionType.Crewmate, "snitch", Snitch.color, 1);
            snitchLeftTasksForReveal = CustomOption.Create(561, CustomOptionType.Crewmate, "snitchLeftTasksForReveal", 1f, 0f, 5f, 1f, snitchSpawnRate);
            snitchIncludeTeamJackal = CustomOption.Create(562, CustomOptionType.Crewmate, "snitchIncludeTeamJackal", false, snitchSpawnRate);
            snitchTeamJackalUseDifferentArrowColor = CustomOption.Create(563, CustomOptionType.Crewmate, "snitchTeamJackalUseDifferentArrowColor", true, snitchIncludeTeamJackal);

            spySpawnRate = new CustomRoleOption(570, CustomOptionType.Crewmate, "spy", Spy.color, 1);
            spyCanDieToSheriffOrYakuza = CustomOption.Create(571, CustomOptionType.Crewmate, "spyCanDieToSheriffOrYakuza", false, spySpawnRate);
            spyImpostorsCanKillAnyone = CustomOption.Create(572, CustomOptionType.Crewmate, "spyImpostorsCanKillAnyone", true, spySpawnRate);
            spyCanEnterVents = CustomOption.Create(573, CustomOptionType.Crewmate, "spyCanEnterVents", false, spySpawnRate);
            spyHasImpostorVision = CustomOption.Create(574, CustomOptionType.Crewmate, "spyHasImpostorVision", false, spySpawnRate);

            securityGuardSpawnRate = new CustomRoleOption(580, CustomOptionType.Crewmate, "securityGuard", SecurityGuard.color, 1);
            securityGuardCooldown = CustomOption.Create(581, CustomOptionType.Crewmate, "securityGuardCooldown", 30f, 2.5f, 60f, 2.5f, securityGuardSpawnRate, format: "unitSeconds");
            securityGuardTotalScrews = CustomOption.Create(582, CustomOptionType.Crewmate, "securityGuardTotalScrews", 7f, 1f, 15f, 1f, securityGuardSpawnRate, format: "unitScrews");
            securityGuardCamPrice = CustomOption.Create(583, CustomOptionType.Crewmate, "securityGuardCamPrice", 2f, 1f, 15f, 1f, securityGuardSpawnRate, format: "unitScrews");
            securityGuardVentPrice = CustomOption.Create(584, CustomOptionType.Crewmate, "securityGuardVentPrice", 1f, 1f, 15f, 1f, securityGuardSpawnRate, format: "unitScrews");
            securityGuardCamDuration = CustomOption.Create(585, CustomOptionType.Crewmate, "securityGuardCamDuration", 10f, 2.5f, 60f, 2.5f, securityGuardSpawnRate, format: "unitSeconds");
            securityGuardCamMaxCharges = CustomOption.Create(586, CustomOptionType.Crewmate, "securityGuardCamMaxCharges", 5f, 1f, 30f, 1f, securityGuardSpawnRate);
            securityGuardCamRechargeTasksNumber = CustomOption.Create(587, CustomOptionType.Crewmate, "securityGuardCamRechargeTasksNumber", 3f, 1f, 10f, 1f, securityGuardSpawnRate);
            securityGuardNoMove = CustomOption.Create(588, CustomOptionType.Crewmate, "securityGuardNoMove", true, securityGuardSpawnRate);

            baitSpawnRate = new CustomRoleOption(590, CustomOptionType.Crewmate, "bait", Bait.color, 1);
            baitHighlightAllVents = CustomOption.Create(591, CustomOptionType.Crewmate, "baitHighlightAllVents", false, baitSpawnRate);
            baitReportDelay = CustomOption.Create(592, CustomOptionType.Crewmate, "baitReportDelay", 0f, 0f, 10f, 1f, baitSpawnRate, format: "unitSeconds");
            baitShowKillFlash = CustomOption.Create(593, CustomOptionType.Crewmate, "baitShowKillFlash", true, baitSpawnRate);

            mediumSpawnRate = new CustomRoleOption(600, CustomOptionType.Crewmate, "medium", Medium.color, 1);
            mediumCooldown = CustomOption.Create(601, CustomOptionType.Crewmate, "mediumCooldown", 30f, 5f, 120f, 5f, mediumSpawnRate, format: "unitSeconds");
            mediumDuration = CustomOption.Create(602, CustomOptionType.Crewmate, "mediumDuration", 3f, 0f, 15f, 1f, mediumSpawnRate, format: "unitSeconds");
            mediumOneTimeUse = CustomOption.Create(603, CustomOptionType.Crewmate, "mediumOneTimeUse", false, mediumSpawnRate);

            portalmakerSpawnRate = new CustomRoleOption(610, CustomOptionType.Crewmate, "portalmaker", Portalmaker.color, 1);
            portalmakerCooldown = CustomOption.Create(611, CustomOptionType.Crewmate, "portalmakerCooldown", 30f, 2.5f, 60f, 2.5f, portalmakerSpawnRate, format: "unitSeconds");
            portalmakerUsePortalCooldown = CustomOption.Create(612, CustomOptionType.Crewmate, "usePortalCooldown", 30f, 2.5f, 60f, 2.5f, portalmakerSpawnRate, format: "unitSeconds");
            enablePortalLog = CustomOption.Create(613, CustomOptionType.Crewmate, "enablePortalLog", true, portalmakerSpawnRate);
            portalmakerLogOnlyColorType = CustomOption.Create(614, CustomOptionType.Crewmate, "logOnlyShowsColorType", true, enablePortalLog);
            portalmakerLogHasTime = CustomOption.Create(615, CustomOptionType.Crewmate, "logShowsTime", true, enablePortalLog);

            sprinterSpawnRate = new CustomRoleOption(620, CustomOptionType.Crewmate, "sprinter", Sprinter.color, 15);
            sprinterCooldown = CustomOption.Create(621, CustomOptionType.Crewmate, "sprinterCooldown", 30f, 2.5f, 60f, 2.5f, sprinterSpawnRate, format: "unitSeconds");
            sprinterDuration = CustomOption.Create(622, CustomOptionType.Crewmate, "sprinterDuration", 15f, 2.5f, 60f, 2.5f, sprinterSpawnRate, format: "unitSeconds");
            sprinterSpeedBonus = CustomOption.Create(623, CustomOptionType.Crewmate, "sprinterSpeedBonus", 125f, 50f, 200f, 5f, sprinterSpawnRate, format: "unitPercent");

            doubleKillerSpawnRate = new CustomRoleOption(660, CustomOptionType.Impostor, "doubleKiller", DoubleKiller.color, 1);
            doubleKillerKillButtonCooldown = CustomOption.Create(661, CustomOptionType.Impostor, "doubleKillerKillCooldown", 0f, 0f, 60f, 2.5f, doubleKillerSpawnRate, format: "unitSeconds");

/*
            chunibyoSpawnRate = new CustomRoleOption(670, CustomOptionType.Crewmate, "chunibyo", Chunibyo.color, 15);
            chunibyoDuration = CustomOption.Create(671, CustomOptionType.Crewmate, "chunibyoDuration", 10f, 2.5f, 30f, 2.5f, chunibyoSpawnRate, format: "unitSeconds");
            chunibyoCooldown = CustomOption.Create(672, CustomOptionType.Crewmate, "chunibyoCooldown", 30f, 2.5f, 60f, 2.5f, chunibyoSpawnRate, format: "unitSeconds");
            chunibyoEnableSpeedUp = CustomOption.Create(673, CustomOptionType.Crewmate, "chunibyoEnableSpeedUp", true, chunibyoSpawnRate);
            chunibyoEnableSpeedDown = CustomOption.Create(674, CustomOptionType.Crewmate, "chunibyoEnableSpeedDown", true, chunibyoSpawnRate);
            chunibyoEnableHighVision = CustomOption.Create(675, CustomOptionType.Crewmate, "chunibyoEnableHighVision", true, chunibyoSpawnRate);
            chunibyoEnableLowVision = CustomOption.Create(676, CustomOptionType.Crewmate, "chunibyoEnableLowVision", true, chunibyoSpawnRate);
            chunibyoAddNoneAbility = CustomOption.Create(677, CustomOptionType.Crewmate, "chunibyoAddNoneAbility", true, chunibyoSpawnRate);
            chunibyoSpeedUpBonus = CustomOption.Create(678, CustomOptionType.Crewmate, "chunibyoSpeedUpBonus", 125f, 50f, 200f, 5f, chunibyoEnableSpeedUp, format: "unitPercent");
            chunibyoSpeedDownBonus = CustomOption.Create(679, CustomOptionType.Crewmate, "chunibyoSpeedDownBonus", 125f, 50f, 200f, 5f, chunibyoEnableSpeedDown, format: "unitPercent");
            chunibyoHighVisionBonus = CustomOption.Create(680, CustomOptionType.Crewmate, "chunibyoHighVisionBonus", 150f, 110f, 200f, 10f, chunibyoEnableHighVision, format: "unitPercent");
            chunibyoLowVisionBonus = CustomOption.Create(681, CustomOptionType.Crewmate, "chunibyoLowVisionBonus", 50f, 10f, 90f, 10f, chunibyoEnableLowVision, format: "unitPercent");*/

            yakuzaSpawnRate = new CustomRoleOption(690, CustomOptionType.Crewmate, "yakuza", Boss.color, 1);
            yakuzaNumShots = CustomOption.Create(691, CustomOptionType.Crewmate, "yakuzaNumShots", 2f, 1f, 15f, 1f, yakuzaSpawnRate, format: "unitShots");
            yakuzaShotsShare = CustomOption.Create(692, CustomOptionType.Crewmate, "yakuzaNumShare", true, yakuzaSpawnRate);
            yakuzaKillCooldown = CustomOption.Create(693, CustomOptionType.Crewmate, "yakuzaKillCooldown", 30f, 2.5f, 60f, 2.5f, yakuzaSpawnRate, format: "unitSeconds");
            yakuzaCanKillNeutrals = CustomOption.Create(694, CustomOptionType.Crewmate, "yakuzaCanKillNeutral", true, yakuzaSpawnRate);
            yakuzaMisfireKillsTarget = CustomOption.Create(695, CustomOptionType.Crewmate, "yakuzaMisfireKillTarget", false, yakuzaSpawnRate);

            bakerySpawnRate = new CustomRoleOption(698, CustomOptionType.Crewmate, "bakery", Bakery.color, 15);
            bakeryEnableBombBread = CustomOption.Create(699, CustomOptionType.Crewmate, "bakeryEnableBombBread", true, bakerySpawnRate);
            bakeryBombBreadRate = CustomOption.Create(697, CustomOptionType.Crewmate, "bakeryBombBreadRate", 1, 1, 100, 1, bakerySpawnRate, format: "unitPercent");

            timeReviverSpawnRate = new CustomRoleOption(7590, CustomOptionType.Crewmate, "timeReviver", TimeReviver.color, 15);
            //timeReviverDeathReport = CustomOption.Create(7591, CustomOptionType.Crewmate, "timeReviverDeathReport", false, timeReviverSpawnRate);
            //timeReviverExileAddMeetingTime = CustomOption.Create(7592, CustomOptionType.Crewmate, "timeReviverExileAddMeetingTime", 20f, 2.5f, 60f, 2.5f, timeReviverSpawnRate, format: "unitSeconds");
            timeReviverTimeCutoffCooldown = CustomOption.Create(7593, CustomOptionType.Crewmate, "timeReviverTimeCutoffCooldown", 30f, 2.5f, 60f, 2.5f, timeReviverSpawnRate, format: "unitSeconds");
            timeReviverTimeCutoffDuration = CustomOption.Create(7594, CustomOptionType.Crewmate, "timeReviverTimeCutoffDuration", 10f, 2.5f, 60f, 2.5f, timeReviverSpawnRate, format: "unitSeconds");

            jammerSpawnRate = new CustomRoleOption(1950, CustomOptionType.Crewmate, "jammer", Jammer.color, 15);
            jammerAliveFriendlyFire = CustomOption.Create(1951, CustomOptionType.Crewmate, "jammerAliveFriendlyFire", true, jammerSpawnRate);

            immortalitySpawnRate = new CustomRoleOption(3670, CustomOptionType.Crewmate, "immortality", Immortality.color, 15);
            immortalityMeetingStartSuicideTime = CustomOption.Create(3671, CustomOptionType.Crewmate, "immortalityMeetingStartSuicideTime", 10f, 2.5f, 60f, 2.5f, immortalitySpawnRate, format: "unitSeconds");

            // Other options
            specialOptions = new CustomOptionBlank(null);
            betterStartButtons = CustomOption.Create(45, CustomOptionType.General, "betterStart", true, specialOptions);
            maxNumberOfMeetings = CustomOption.Create(16, CustomOptionType.General, "maxNumberOfMeetings", 10, 0, 15, 1, specialOptions, true);
            blockSkippingInEmergencyMeetings = CustomOption.Create(17, CustomOptionType.General, "blockSkippingInEmergencyMeetings", false, specialOptions);
            noVoteIsSelfVote = CustomOption.Create(18, CustomOptionType.General, "noVoteIsSelfVote", false, specialOptions);
            hideOutOfSightNametags = CustomOption.Create(19, CustomOptionType.General, "hideOutOfSightNametags", false, specialOptions);
            refundVotesOnDeath = CustomOption.Create(20, CustomOptionType.General, "refundVotesOnDeath", true, specialOptions);
            allowParallelMedBayScans = CustomOption.Create(21, CustomOptionType.General, "parallelMedbayScans", false, specialOptions);
            ventAnimation = CustomOption.Create(22, CustomOptionType.General, "ventAnimation", true, specialOptions);
            enableDiePlayerZoomInOut = CustomOption.Create(23, CustomOptionType.General, "diePlayerCanZoomInOut", false, specialOptions);
            onePlayerStart = CustomOption.Create(24, CustomOptionType.General, "onePlayerStart", false, specialOptions);
            airshipReactorDuration = CustomOption.Create(25, CustomOptionType.General, "airShipReactorDuration", 60f, 0f, 600f, 5f, specialOptions, format: "unitSeconds");
            //helloweenMode = CustomOption.Create(26, CustomOptionType.General, "helloweenMode", false, specialOptions);

            enableMirrorMap = CustomOption.Create(27, CustomOptionType.General, "enableMirror", false, specialOptions);
            enableAddCustomVent = CustomOption.Create(46, CustomOptionType.General, "enableAddCustomVent", false, specialOptions);

            //camouflagedComms = CustomOption.Create(49, CustomOptionType.General, "camouflagedComms", false, specialOptions);

            oldAirShipAdmin = CustomOption.Create(47, CustomOptionType.General, "oldAirShipAdmin", true, specialOptions);
            enableRecordsAdmin = CustomOption.Create(48, CustomOptionType.General, "enableRecordsAdmin", false, specialOptions);

            hideSettings = CustomOption.Create(28, CustomOptionType.General, "hideSettings", false, specialOptions);

            restrictDevices = CustomOption.Create(29, CustomOptionType.General, "restrictDevices", new string[] { "optionOff", "restrictPerTurn", "restrictPerGame" }, specialOptions);
            restrictAdmin = CustomOption.Create(30, CustomOptionType.General, "disableAdmin", 30f, 0f, 600f, 5f, restrictDevices, format: "unitSeconds");
            restrictCameras = CustomOption.Create(31, CustomOptionType.General, "disableCameras", 30f, 0f, 600f, 5f, restrictDevices, format: "unitSeconds");
            restrictVitals = CustomOption.Create(32, CustomOptionType.General, "disableVitals", 30f, 0f, 600f, 5f, restrictDevices, format: "unitSeconds");

            uselessOptions = CustomOption.Create(33, CustomOptionType.General, "uselessOptions", false, null, isHeader: true);
            dynamicMap = CustomOption.Create(34, CustomOptionType.General, "playRandomMaps", false, uselessOptions);
            dynamicMapEnableSkeld = CustomOption.Create(35, CustomOptionType.General, "dynamicMapEnableSkeld", true, dynamicMap, false);
            dynamicMapEnableMira = CustomOption.Create(36, CustomOptionType.General, "dynamicMapEnableMira", true, dynamicMap, false);
            dynamicMapEnablePolus = CustomOption.Create(37, CustomOptionType.General, "dynamicMapEnablePolus", true, dynamicMap, false);
            dynamicMapEnableAirShip = CustomOption.Create(38, CustomOptionType.General, "dynamicMapEnableAirShip", true, dynamicMap, false);
            //dynamicMapEnableDleks = CustomOption.Create(39, CustomOptionType.General, CustomOptionType.General, "dynamicMapEnableDleks", false, dynamicMap, false);
            dynamicMapEnableSubmerged = CustomOption.Create(40, CustomOptionType.General, "dynamicMapEnableSubmerged", true, dynamicMap, false);

            disableVents = CustomOption.Create(41, CustomOptionType.General, "disableVents", false, uselessOptions);
            hidePlayerNames = CustomOption.Create(42, CustomOptionType.General, "hidePlayerNames", false, uselessOptions);
            playerNameDupes = CustomOption.Create(43, CustomOptionType.General, "playerNameDupes", false, uselessOptions);
            playerColorRandom = CustomOption.Create(44, CustomOptionType.General, "playerColorRandom", false, uselessOptions);

            blockedRolePairings.Add((byte)RoleType.Vampire, new[] { (byte)RoleType.Warlock });
            blockedRolePairings.Add((byte)RoleType.Warlock, new[] { (byte)RoleType.Vampire });
            blockedRolePairings.Add((byte)RoleType.Vulture, new[] { (byte)RoleType.Cleaner });
            blockedRolePairings.Add((byte)RoleType.Cleaner, new[] { (byte)RoleType.Vulture });
        }
    }
}

/*
unitSeconds	{0}秒
unitMinutes	{0}分
unitShots	{0}発
unitScrews	{0}本
unitMultiplier	{0}x
unitTimes	{0}回
unitMeters	{0}m
unitVotes	{0}票
unitCouples	{0}組
unitPlayers	{0}人
unitPercent	{0}%
*/