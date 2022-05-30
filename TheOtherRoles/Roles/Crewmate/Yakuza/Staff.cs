using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using TheOtherRoles.Objects;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;
using TheOtherRoles.Modules;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Staff : RoleBase<Staff>
    {
        private static CustomButton staffKillButton;
        public static TMPro.TMP_Text staffNumShotsText;
        public static PlayerControl staff;

        public static Color color = new Color32(46, 84, 245, byte.MaxValue);

        public static float cooldown { get { return CustomOptionHolder.yakuzaKillCooldown.getFloat(); } }
        public static int maxShots { get { return Mathf.RoundToInt(CustomOptionHolder.yakuzaNumShots.getFloat()); } }
        public static bool canKillNeutrals { get { return CustomOptionHolder.yakuzaCanKillNeutrals.getBool(); } }
        public static bool misfireKillsTarget { get { return CustomOptionHolder.yakuzaMisfireKillsTarget.getBool(); } }
        public static bool spyCanDieToStaff { get { return CustomOptionHolder.spyCanDieToSheriffOrYakuza.getBool(); } }
        public static bool madmateCanDieToStaff { get { return CustomOptionHolder.madmateCanDieToSheriffOrYakuza.getBool(); } }
        public static bool createdMadmateCanDieToStaff { get { return CustomOptionHolder.createdMadmateCanDieToSheriffOrYakuza.getBool(); } }
        public static bool numShare { get { return CustomOptionHolder.yakuzaShotsShare.getBool(); } }

        public int numShots = 2;
        public static bool dead = false;
        public PlayerControl currentTarget;

        public Staff()
        {
            RoleType = roleId = RoleType.Staff;
            if (numShare == true)
                Gun.shareShots = maxShots;
            else
                numShots = maxShots;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }

        public override void FixedUpdate()
        {
            if (player == PlayerControl.LocalPlayer && numShots > 0)
            {
                currentTarget = setTarget();
                setPlayerOutline(currentTarget, Staff.color);
            }
        }

        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null)
        {
            dead = true;
        }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {

            // Staff Kill
            staffKillButton = new CustomButton(
                () =>
                {
                    if (local.numShots <= 0 || Gun.shareShots <= 0)
                    {
                        return;
                    }

                    MurderAttemptResult murderAttemptResult = Helpers.checkMuderAttempt(PlayerControl.LocalPlayer, local.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.SuppressKill) return;

                    if (murderAttemptResult == MurderAttemptResult.PerformKill)
                    {
                        bool misfire = false;
                        byte targetId = local.currentTarget.PlayerId; ;
                        if ((local.currentTarget.Data.Role.IsImpostor && (!local.currentTarget.hasModifier(ModifierType.Mini) || Mini.isGrownUp(local.currentTarget))) ||
                            (Staff.spyCanDieToStaff && Spy.spy == local.currentTarget) ||
                            (Staff.madmateCanDieToStaff && local.currentTarget.hasModifier(ModifierType.Madmate)) ||
                            (Staff.createdMadmateCanDieToStaff && local.currentTarget.hasModifier(ModifierType.CreatedMadmate)) ||
                            (Staff.canKillNeutrals && local.currentTarget.isNeutral()) ||
                            (Jackal.jackal == local.currentTarget || Sidekick.sidekick == local.currentTarget))
                        {
                            //targetId = Staff.currentTarget.PlayerId;
                            misfire = false;
                        }
                        else
                        {
                            //targetId = PlayerControl.LocalPlayer.PlayerId;
                            misfire = true;
                        }

                        // Mad staff always misfires.
                        if (local.player.hasModifier(ModifierType.Madmate))
                        {
                            misfire = true;
                        }
                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.StaffKill, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(PlayerControl.LocalPlayer.Data.PlayerId);
                        killWriter.Write(targetId);
                        killWriter.Write(misfire);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.staffKill(PlayerControl.LocalPlayer.Data.PlayerId, targetId, misfire);
                    }

                    staffKillButton.Timer = staffKillButton.MaxTimer;
                    local.currentTarget = null;
                },
                () =>
                {
                    if (numShare == false)
                        return PlayerControl.LocalPlayer.isRole(RoleType.Staff) && local.numShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead && Gun.dead;
                    else if (numShare == true)
                        return PlayerControl.LocalPlayer.isRole(RoleType.Staff) && Gun.shareShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead && Gun.dead;
                    return true;
                },
                () =>
                {
                    if (staffNumShotsText != null)
                    {
                        if (numShare == false)
                        {
                            if (local.numShots > 0)
                                staffNumShotsText.text = String.Format(ModTranslation.getString("yakuzaShots"), local.numShots);
                            else
                                staffNumShotsText.text = "";
                        }
                        else if (numShare == true)
                        {
                            if (Gun.shareShots > 0)
                                staffNumShotsText.text = String.Format(ModTranslation.getString("yakuzaShots"), Gun.shareShots);
                            else
                                staffNumShotsText.text = "";
                        }
                    }
                    return local.currentTarget && PlayerControl.LocalPlayer.CanMove;
                },
                () => { staffKillButton.Timer = staffKillButton.MaxTimer; },
                hm.KillButton.graphic.sprite,
                new Vector3(0f, 1f, 0),
                hm,
                hm.KillButton,
                KeyCode.Q
            );

            staffNumShotsText = GameObject.Instantiate(staffKillButton.actionButton.cooldownTimerText, staffKillButton.actionButton.cooldownTimerText.transform.parent);
            staffNumShotsText.text = "";
            staffNumShotsText.enableWordWrapping = false;
            staffNumShotsText.transform.localScale = Vector3.one * 0.5f;
            staffNumShotsText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);
        }

        public static void SetButtonCooldowns()
        {
            staffKillButton.MaxTimer = Staff.cooldown;
        }

        public static void Clear()
        {
            players = new List<Staff>();
            dead = false;
        }
    }
}