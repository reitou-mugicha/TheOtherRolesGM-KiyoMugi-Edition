using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Patches;

namespace TheOtherRoles
{
    static class MapOptions
    {
        // Set values
        public static int maxNumberOfMeetings = 10;
        public static bool blockSkippingInEmergencyMeetings = false;
        public static bool noVoteIsSelfVote = false;
        public static bool hidePlayerNames = false;
        public static bool hideSettings = false;
        public static bool hideOutOfSightNametags = false;

        public static bool randomizeColors = false;
        public static bool allowDupeNames = false;

        public static int restrictDevices = 0;
        public static float restrictAdminTime = 600f;
        public static float restrictAdminTimeMax = 600f;
        public static float restrictCamerasTime = 600f;
        public static float restrictCamerasTimeMax = 600f;
        public static float restrictVitalsTime = 600f;
        public static float restrictVitalsTimeMax = 600f;
        public static bool disableVents = false;

        public static bool ghostsSeeRoles = true;
        public static bool ghostsSeeTasks = true;
        public static bool ghostsSeeVotes = true;
        public static bool showRoleSummary = true;
        public static bool hideNameplates = false;
        public static bool allowParallelMedBayScans = false;
        public static bool showLighterDarker = false;
        public static bool hideTaskArrows = false;
        //public static bool showDebugData = false;
        public static bool enableHorseMode = false;

        // Updating values
        public static int meetingsCount = 0;
        public static List<SurvCamera> camerasToAdd = new List<SurvCamera>();
        public static List<Vent> ventsToSeal = new List<Vent>();
        public static Dictionary<byte, PoolablePlayer> playerIcons = new Dictionary<byte, PoolablePlayer>();

        public static void clearAndReloadMapOptions()
        {
            meetingsCount = 0;
            camerasToAdd = new List<SurvCamera>();
            ventsToSeal = new List<Vent>();
            playerIcons = new Dictionary<byte, PoolablePlayer>();

            maxNumberOfMeetings = Mathf.RoundToInt(CustomOptionHolder.maxNumberOfMeetings.getSelection());
            blockSkippingInEmergencyMeetings = CustomOptionHolder.blockSkippingInEmergencyMeetings.getBool();
            noVoteIsSelfVote = CustomOptionHolder.noVoteIsSelfVote.getBool();
            hidePlayerNames = CustomOptionHolder.hidePlayerNames.getBool();

            hideOutOfSightNametags = CustomOptionHolder.hideOutOfSightNametags.getBool();

            hideSettings = CustomOptionHolder.hideSettings.getBool();

            randomizeColors = CustomOptionHolder.uselessOptions.getBool() && CustomOptionHolder.playerColorRandom.getBool();
            allowDupeNames = CustomOptionHolder.uselessOptions.getBool() && CustomOptionHolder.playerNameDupes.getBool();

            restrictDevices = CustomOptionHolder.restrictDevices.getSelection();
            restrictAdminTime = restrictAdminTimeMax = CustomOptionHolder.restrictAdmin.getFloat();
            restrictCamerasTime = restrictCamerasTimeMax = CustomOptionHolder.restrictCameras.getFloat();
            restrictVitalsTime = restrictVitalsTimeMax = CustomOptionHolder.restrictVitals.getFloat();
            disableVents = CustomOptionHolder.disableVents.getBool();
        }

        public static void reloadPluginOptions()
        {
            allowParallelMedBayScans = CustomOptionHolder.allowParallelMedBayScans.getBool();
            ghostsSeeRoles = TheOtherRolesPlugin.GhostsSeeRoles.Value;
            ghostsSeeTasks = TheOtherRolesPlugin.GhostsSeeTasks.Value;
            ghostsSeeVotes = TheOtherRolesPlugin.GhostsSeeVotes.Value;
            showRoleSummary = TheOtherRolesPlugin.ShowRoleSummary.Value;
            hideNameplates = TheOtherRolesPlugin.HideNameplates.Value;
            showLighterDarker = TheOtherRolesPlugin.ShowLighterDarker.Value;
            hideTaskArrows = TheOtherRolesPlugin.HideTaskArrows.Value;
            //showDebugData = TheOtherRolesPlugin.ShowDebugData.Value;
            enableHorseMode = TheOtherRolesPlugin.EnableHorseMode.Value;
            IntroPatch.ShouldAlwaysHorseAround.isHorseMode = TheOtherRolesPlugin.EnableHorseMode.Value;
        }

        public static void resetDeviceTimes()
        {
            restrictAdminTime = restrictAdminTimeMax;
            restrictCamerasTime = restrictCamerasTimeMax;
            restrictVitalsTime = restrictVitalsTimeMax;
        }

        public static bool canUseAdmin
        {
            get
            {
                return restrictDevices == 0 || restrictAdminTime > 0f;
            }
        }

        public static bool couldUseAdmin
        {
            get
            {
                return restrictDevices == 0 || restrictAdminTimeMax > 0f;
            }
        }

        public static bool canUseCameras
        {
            get
            {
                return restrictDevices == 0 || restrictCamerasTime > 0f;
            }
        }

        public static bool couldUseCameras
        {
            get
            {
                return restrictDevices == 0 || restrictCamerasTimeMax > 0f;
            }
        }

        public static bool canUseVitals
        {
            get
            {
                return restrictDevices == 0 || restrictVitalsTime > 0f;
            }
        }

        public static bool couldUseVitals
        {
            get
            {
                return restrictDevices == 0 || restrictVitalsTimeMax > 0f;
            }
        }
    }
}