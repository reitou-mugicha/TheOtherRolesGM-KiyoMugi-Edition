using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hazel;
using System;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Mayor : RoleBase<Mayor>
    {
        public static PlayerControl mayor;
        public static CustomButton mayorMeetingButton;
        public static Color color = new Color32(32, 77, 66, byte.MaxValue);
        public static Minigame emergency = null;
        public static Sprite emergencySprite = null;
        public static int numVotes = 2;
        public static int numButton = 2;
        public static bool meetingButton { get { return CustomOptionHolder.mayorMeetingButton.getBool(); } }
        public static int maxButton { get { return Mathf.RoundToInt(CustomOptionHolder.mayorNumMeetingButton.getFloat()); } }

        public static Sprite getMeetingSprite()
        {
            if (emergencySprite) return emergencySprite;
            emergencySprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.EmergencyButton.png", 550f);
            return emergencySprite;
        }

        public static bool isMayor(byte playerId)
        {
            if ((mayor != null && mayor.PlayerId == playerId)) return true;
            return false;
        }

        public static void clear(byte playerId)
        {
            if (mayor != null && mayor.PlayerId == playerId) mayor = null;
        }

        public static void clearAndReload()
        {
            mayor = null;
            emergency = null;
            emergencySprite = null;

            numVotes = (int)CustomOptionHolder.mayorNumVotes.getFloat();
        }

        public Mayor()
        {
            RoleType = roleId = RoleType.Mayor;
            numButton = maxButton;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            mayorMeetingButton = new CustomButton(
                () =>
                {
                    if (Mayor.numButton < 0)
                    {
                        return;
                    }
                    Mayor.numButton--;

                    PlayerControl.LocalPlayer.NetTransform.Halt(); // Stop current movement
                    Helpers.handleVampireBiteOnBodyReport(); // Manually call Vampire handling, since the CmdReportDeadBody Prefix won't be called
                    RPCProcedure.uncheckedCmdReportDeadBody(PlayerControl.LocalPlayer.PlayerId, Byte.MinValue);

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedCmdReportDeadBody, Hazel.SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(Byte.MaxValue);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    mayorMeetingButton.Timer = mayorMeetingButton.MaxTimer;
                },
                () => { return Mayor.mayor != null && Mayor.mayor == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Mayor.numButton > 0 && Mayor.meetingButton == true; },
                () =>
                {
                    mayorMeetingButton.actionButton.OverrideText(ModTranslation.getString("mayorMeetingText"));
                    bool sabotageActive = false;
                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                        if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles
                            || SubmergedCompatibility.isSubmerged() && task.TaskType == SubmergedCompatibility.RetrieveOxygenMask)
                            sabotageActive = true;
                    return !sabotageActive && PlayerControl.LocalPlayer.CanMove;
                },
                () => { mayorMeetingButton.Timer = mayorMeetingButton.MaxTimer; },
                Mayor.getMeetingSprite(),
                new Vector3(-1.8f, -0.06f, 0),
                hm,
                hm.UseButton,
                KeyCode.F,
                true,
                0f,
                () => { },
                false
            );
        }
        public static void SetButtonCooldowns()
        {
            mayorMeetingButton.MaxTimer = PlayerControl.GameOptions.EmergencyCooldown;
        }

        public static void Clear()
        {
            players = new List<Mayor>();
        }
    }
}