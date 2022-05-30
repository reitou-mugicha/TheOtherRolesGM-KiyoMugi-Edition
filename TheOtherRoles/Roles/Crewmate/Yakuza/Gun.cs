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
    public class Gun : RoleBase<Gun>
    {
        private static CustomButton gunKillButton;
        public static TMPro.TMP_Text gunNumShotsText;
        public static PlayerControl gun;

        public static Color color = new Color32(46, 84, 245, byte.MaxValue);

        public static float cooldown { get { return CustomOptionHolder.yakuzaKillCooldown.getFloat(); } }
        public static int maxShots { get { return Mathf.RoundToInt(CustomOptionHolder.yakuzaNumShots.getFloat()); } }
        public static bool canKillNeutrals { get { return CustomOptionHolder.yakuzaCanKillNeutrals.getBool(); } }
        public static bool misfireKillsTarget { get { return CustomOptionHolder.yakuzaMisfireKillsTarget.getBool(); } }
        public static bool spyCanDieToGun { get { return CustomOptionHolder.spyCanDieToSheriffOrYakuza.getBool(); } }
        public static bool madmateCanDieToGun { get { return CustomOptionHolder.madmateCanDieToSheriffOrYakuza.getBool(); } }
        public static bool createdMadmateCanDieToGun { get { return CustomOptionHolder.createdMadmateCanDieToSheriffOrYakuza.getBool(); } }
        public static bool numShare { get { return CustomOptionHolder.yakuzaShotsShare.getBool(); } }

        public int numShots = 2;
        public static bool dead = false;
        public static int shareShots = 2;
        public PlayerControl currentTarget;

        public Gun()
        {
            RoleType = roleId = RoleType.Gun;
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
                setPlayerOutline(currentTarget, Gun.color);
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

            // Gun Kill
            gunKillButton = new CustomButton(
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
                            (Gun.spyCanDieToGun && Spy.spy == local.currentTarget) ||
                            (Gun.madmateCanDieToGun && local.currentTarget.hasModifier(ModifierType.Madmate)) ||
                            (Gun.createdMadmateCanDieToGun && local.currentTarget.hasModifier(ModifierType.CreatedMadmate)) ||
                            (Gun.canKillNeutrals && local.currentTarget.isNeutral()) ||
                            (Jackal.jackal == local.currentTarget || Sidekick.sidekick == local.currentTarget))
                        {
                            //targetId = Gun.currentTarget.PlayerId;
                            misfire = false;
                        }
                        else
                        {
                            //targetId = PlayerControl.LocalPlayer.PlayerId;
                            misfire = true;
                        }

                        // Mad gun always misfires.
                        if (local.player.hasModifier(ModifierType.Madmate))
                        {
                            misfire = true;
                        }
                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.GunKill, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(PlayerControl.LocalPlayer.Data.PlayerId);
                        killWriter.Write(targetId);
                        killWriter.Write(misfire);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.gunKill(PlayerControl.LocalPlayer.Data.PlayerId, targetId, misfire);
                    }

                    gunKillButton.Timer = gunKillButton.MaxTimer;
                    local.currentTarget = null;
                },
                () =>
                {
                    if (numShare == false)
                        return PlayerControl.LocalPlayer.isRole(RoleType.Gun) && local.numShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead;
                    else if (numShare == true)
                        return PlayerControl.LocalPlayer.isRole(RoleType.Gun) && Gun.shareShots > 0 && !PlayerControl.LocalPlayer.Data.IsDead;
                    return true;
                },
                () =>
                {
                    if (numShare == false)
                    {
                        if (local.numShots > 0)
                            gunNumShotsText.text = String.Format(ModTranslation.getString("yakuzaShots"), local.numShots);
                        else
                            gunNumShotsText.text = "";
                    }
                    else if (numShare == true)
                    {
                        if (shareShots > 0)
                            gunNumShotsText.text = String.Format(ModTranslation.getString("yakuzaShots"), Gun.shareShots);
                        else
                            gunNumShotsText.text = "";
                    }
                    return local.currentTarget && PlayerControl.LocalPlayer.CanMove;
                },
                () => { gunKillButton.Timer = gunKillButton.MaxTimer; },
                hm.KillButton.graphic.sprite,
                new Vector3(0f, 1f, 0),
                hm,
                hm.KillButton,
                KeyCode.Q
            );

            gunNumShotsText = GameObject.Instantiate(gunKillButton.actionButton.cooldownTimerText, gunKillButton.actionButton.cooldownTimerText.transform.parent);
            gunNumShotsText.text = "";
            gunNumShotsText.enableWordWrapping = false;
            gunNumShotsText.transform.localScale = Vector3.one * 0.5f;
            gunNumShotsText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);
        }

        public static void SetButtonCooldowns()
        {
            gunKillButton.MaxTimer = Gun.cooldown;
        }

        public static void Clear()
        {
            players = new List<Gun>();
            dead = false;
        }
    }
}