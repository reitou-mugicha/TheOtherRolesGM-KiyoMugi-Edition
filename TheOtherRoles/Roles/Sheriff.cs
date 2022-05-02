using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using TheOtherRoles.Objects;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Sheriff : RoleBase<Sheriff>
    {
        private static CustomButton sheriffKillButton;
        //private static CustomButton studentCreateButton;
        public static TMPro.TMP_Text sheriffNumShotsText;
        public static PlayerControl sheriff;

        public static Color color = new Color32(248, 205, 70, byte.MaxValue);

        public static float cooldown { get { return CustomOptionHolder.sheriffCooldown.getFloat(); } }
        public static int maxShots { get { return Mathf.RoundToInt(CustomOptionHolder.sheriffNumShots.getFloat()); } }
        public static bool canKillNeutrals { get { return CustomOptionHolder.sheriffCanKillNeutrals.getBool(); } }
        public static bool misfireKillsTarget { get { return CustomOptionHolder.sheriffMisfireKillsTarget.getBool(); } }
        public static bool spyCanDieToSheriff { get { return CustomOptionHolder.spyCanDieToSheriff.getBool(); } }
        public static bool madmateCanDieToSheriff { get { return CustomOptionHolder.madmateCanDieToSheriff.getBool(); } }
        public static bool createdMadmateCanDieToSheriff { get { return CustomOptionHolder.createdMadmateCanDieToSheriff.getBool(); } }
        //public static bool canCreateStudent { get { return CustomOptionHolder.sheriffCreateStudent.getBool(); } }

        public int numShots = 2;
        public PlayerControl currentTarget;

        public Sheriff()
        {
            RoleType = roleId = RoleType.Sheriff;
            numShots = maxShots;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }

        public override void FixedUpdate()
        {
            if (player == PlayerControl.LocalPlayer && numShots > 0)
            {
                currentTarget = setTarget();
                setPlayerOutline(currentTarget, Sheriff.color);
            }/*
            if (player.isAlive() && !(studentCreateButton.PositionOffset == new Vector3(1000, 1000, 0)))
            {
                List<PlayerControl> untargetablePlayers = new List<PlayerControl>();
                foreach (var p in PlayerControl.AllPlayerControls)
                {
                    if (p.isRole(RoleType.Sheriff))
                    {
                        untargetablePlayers.Add(p);
                    }
                }
                currentTarget = setTarget(untargetablePlayers: untargetablePlayers);
                setPlayerOutline(currentTarget, Sheriff.color);
            }*/
        }

        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {

            // Sheriff Kill
            sheriffKillButton = new CustomButton(
                () =>
                {
                    if (local.numShots <= 0)
                    {
                        return;
                    }

                    MurderAttemptResult murderAttemptResult = Helpers.checkMuderAttempt(PlayerControl.LocalPlayer, local.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.SuppressKill) return;

                    if (murderAttemptResult == MurderAttemptResult.PerformKill)
                    {
                        bool misfire = false;
                        byte targetId = local.currentTarget.PlayerId; ;
                        if ((local.currentTarget.Data.Role.IsImpostor && (local.currentTarget != Mini.mini || Mini.isGrownUp())) ||
                            (Sheriff.spyCanDieToSheriff && Spy.spy == local.currentTarget) ||
                            (Sheriff.madmateCanDieToSheriff && local.currentTarget.hasModifier(ModifierType.Madmate)) ||
                            (Sheriff.createdMadmateCanDieToSheriff && local.currentTarget.hasModifier(ModifierType.CreatedMadmate)) ||
                            (Sheriff.canKillNeutrals && local.currentTarget.isNeutral()) ||
                            (Jackal.jackal == local.currentTarget || Sidekick.sidekick == local.currentTarget))
                        {
                            //targetId = Sheriff.currentTarget.PlayerId;
                            misfire = false;
                        }
                        else
                        {
                            //targetId = PlayerControl.LocalPlayer.PlayerId;
                            misfire = true;
                        }

                        // Mad sheriff always misfires.
                        if (local.player.hasModifier(ModifierType.Madmate))
                        {
                            misfire = true;
                        }
                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(PlayerControl.LocalPlayer.Data.PlayerId);
                        killWriter.Write(targetId);
                        killWriter.Write(misfire);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.sheriffKill(PlayerControl.LocalPlayer.Data.PlayerId, targetId, misfire);
                    }

                    sheriffKillButton.Timer = sheriffKillButton.MaxTimer;
                    local.currentTarget = null;
                },
                () => { return PlayerControl.LocalPlayer.isRole(RoleType.Sheriff) && local.numShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead; },
                () =>
                {
                    if (sheriffNumShotsText != null)
                    {
                        if (local.numShots > 0)
                            sheriffNumShotsText.text = String.Format(ModTranslation.getString("sheriffShots"), local.numShots);
                        else
                            sheriffNumShotsText.text = "";
                    }
                    return local.currentTarget && PlayerControl.LocalPlayer.CanMove;
                },
                () => { sheriffKillButton.Timer = sheriffKillButton.MaxTimer; },
                hm.KillButton.graphic.sprite,
                new Vector3(0f, 1f, 0),
                hm,
                hm.KillButton,
                KeyCode.Q
            );

            sheriffNumShotsText = GameObject.Instantiate(sheriffKillButton.actionButton.cooldownTimerText, sheriffKillButton.actionButton.cooldownTimerText.transform.parent);
            sheriffNumShotsText.text = "";
            sheriffNumShotsText.enableWordWrapping = false;
            sheriffNumShotsText.transform.localScale = Vector3.one * 0.5f;
            sheriffNumShotsText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);
/*
            if (canCreateStudent)
            {

                studentCreateButton = new CustomButton(
                () =>
                {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffCreateStudent, Hazel.SendOption.Reliable, -1);
                    writer.Write(local.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.SheriffCreateStudent(local.currentTarget.PlayerId);
                    studentCreateButton.PositionOffset = new Vector3(1000, 1000, 0);
                },
                () => { return canCreateStudent && PlayerControl.LocalPlayer.isRole(RoleType.Sheriff) && PlayerControl.LocalPlayer.isAlive(); },
                () => { return canCreateStudent && Sheriff.local.currentTarget != null && PlayerControl.LocalPlayer.CanMove; },
                () => { studentCreateButton.Timer = 0; },
                getButtonSprite(),
                new Vector3(-1.8f, 1f, 0),
                hm,
                hm.AbilityButton,
                KeyCode.I
            );
                studentCreateButton.buttonText = ModTranslation.getString("StudentCreateText");

            }*/

        }/*

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {

            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CreateStudentButton.png", 115f);
            return buttonSprite;

        }*/

        public static void SetButtonCooldowns()
        {
            sheriffKillButton.MaxTimer = Sheriff.cooldown;
        }

        public static void Clear()
        {
            players = new List<Sheriff>();
        }
    }
}