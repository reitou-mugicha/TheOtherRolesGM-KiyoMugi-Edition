using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using TheOtherRoles.Objects;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;
using static TheOtherRoles.GameHistory;
using TheOtherRoles.Modules;
using TheOtherRoles.Patches;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Boss : RoleBase<Boss>
    {
        private static CustomButton bossKillButton;
        public static TMPro.TMP_Text bossNumShotsText;
        public static PlayerControl boss;

        public static Color color = new Color32(46, 84, 245, byte.MaxValue);

        public static float cooldown { get { return CustomOptionHolder.yakuzaKillCooldown.getFloat(); } }
        public static int maxShots { get { return Mathf.RoundToInt(CustomOptionHolder.yakuzaNumShots.getFloat()); } }
        public static bool canKillNeutrals { get { return CustomOptionHolder.yakuzaCanKillNeutrals.getBool(); } }
        public static bool misfireKillsTarget { get { return CustomOptionHolder.yakuzaMisfireKillsTarget.getBool(); } }
        public static bool spyCanDieToBoss { get { return CustomOptionHolder.spyCanDieToSheriffOrYakuza.getBool(); } }
        public static bool madmateCanDieToBoss { get { return CustomOptionHolder.madmateCanDieToSheriffOrYakuza.getBool(); } }
        public static bool createdMadmateCanDieToBoss { get { return CustomOptionHolder.createdMadmateCanDieToSheriffOrYakuza.getBool(); } }
        public static bool numShare { get { return CustomOptionHolder.yakuzaShotsShare.getBool(); } }

        public int numShots = 2;
        public static bool dead = false;
        public PlayerControl currentTarget;

        public Boss()
        {
            RoleType = roleId = RoleType.Boss;
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
                setPlayerOutline(currentTarget, Boss.color);
            }
        }

        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null)
        {
            dead = true;

            if (boss.isDead())
            {
                foreach (var gun in Gun.allPlayers)
                {
                    if (gun.isAlive())
                    {
                        if (killer == null)
                        {
                            gun.Exiled();
                        }
                        else
                        {
                            gun.MurderPlayer(gun);
                        }
                        finalStatuses[gun.PlayerId] = FinalStatus.Suicide;
                    }
                }
                foreach (var staff in Staff.allPlayers)
                {
                    if (staff.isAlive())
                    {
                        if (killer == null)
                        {
                            staff.Exiled();
                        }
                        else
                        {
                            staff.MurderPlayer(staff);
                        }
                        finalStatuses[staff.PlayerId] = FinalStatus.Suicide;
                    }
                }
            }
        }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {
            // Boss Kill
            bossKillButton = new CustomButton(
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
                            (Boss.spyCanDieToBoss && Spy.spy == local.currentTarget) ||
                            (Boss.madmateCanDieToBoss && local.currentTarget.hasModifier(ModifierType.Madmate)) ||
                            (Boss.createdMadmateCanDieToBoss && local.currentTarget.hasModifier(ModifierType.CreatedMadmate)) ||
                            (Boss.canKillNeutrals && local.currentTarget.isNeutral()) ||
                            (Jackal.jackal == local.currentTarget || Sidekick.sidekick == local.currentTarget))
                        {
                            //targetId = Boss.currentTarget.PlayerId;
                            misfire = false;
                        }
                        else
                        {
                            //targetId = PlayerControl.LocalPlayer.PlayerId;
                            misfire = true;
                        }

                        // Mad boss always misfires.
                        if (local.player.hasModifier(ModifierType.Madmate))
                        {
                            misfire = true;
                        }
                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BossKill, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(PlayerControl.LocalPlayer.Data.PlayerId);
                        killWriter.Write(targetId);
                        killWriter.Write(misfire);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.bossKill(PlayerControl.LocalPlayer.Data.PlayerId, targetId, misfire);
                    }

                    bossKillButton.Timer = bossKillButton.MaxTimer;
                    local.currentTarget = null;
                },
                () =>
                {
                    if (numShare == false)
                        return PlayerControl.LocalPlayer.isRole(RoleType.Boss) && local.numShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead && Gun.dead && Staff.dead;
                    else if (numShare == true)
                        return PlayerControl.LocalPlayer.isRole(RoleType.Boss) && Gun.shareShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead && Gun.dead && Staff.dead;
                    return true;
                },
                () =>
                {
                    if (numShare == false)
                    {
                        if (local.numShots > 0)
                            bossNumShotsText.text = String.Format(ModTranslation.getString("yakuzaShots"), local.numShots);
                        else
                            bossNumShotsText.text = "";
                    }
                    else if (numShare == true)
                    {
                        if (Gun.shareShots > 0)
                            bossNumShotsText.text = String.Format(ModTranslation.getString("yakuzaShots"), Gun.shareShots);
                        else
                            bossNumShotsText.text = "";
                    }
                    return local.currentTarget && PlayerControl.LocalPlayer.CanMove;
                },
                () => { bossKillButton.Timer = bossKillButton.MaxTimer; },
                hm.KillButton.graphic.sprite,
                new Vector3(0f, 1f, 0),
                hm,
                hm.KillButton,
                KeyCode.Q
            );

            bossNumShotsText = GameObject.Instantiate(bossKillButton.actionButton.cooldownTimerText, bossKillButton.actionButton.cooldownTimerText.transform.parent);
            bossNumShotsText.text = "";
            bossNumShotsText.enableWordWrapping = false;
            bossNumShotsText.transform.localScale = Vector3.one * 0.5f;
            bossNumShotsText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);
        }

        public static void SetButtonCooldowns()
        {
            bossKillButton.MaxTimer = Boss.cooldown;
        }

        public static bool isDead()
        {
            if(dead) return true;
            else return false;
        }

        public static void Clear()
        {
            players = new List<Boss>();
            dead = false;
        }
    }
}