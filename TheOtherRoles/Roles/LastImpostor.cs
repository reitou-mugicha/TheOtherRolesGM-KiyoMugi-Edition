using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class LastImpostor : RoleBase<LastImpostor>
    {
        public static Color color = Palette.ImpostorRed;
        public static bool isEnable {get {return CustomOptionHolder.lastImpostorEnable.getBool();}}
        public static int killCounter = 0;
        public static int maxKillCounter {get {return (int)CustomOptionHolder.lastImpostorNumKills.getFloat();}}
        public static int numUsed = 0;

        public static bool resultIsCrewOrNot {get {return CustomOptionHolder.lastImpostorResultIsCrewOrNot.getBool();}}

        public LastImpostor()
        {
            RoleType = roleId = RoleId.LastImpostor;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { 
            killCounter += 1;
        }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static List<CustomButton> lastImpostorButtons = new List<CustomButton>();
        public static void MakeButtons(HudManager hm)
        {
            lastImpostorButtons = new List<CustomButton>();

            Vector3 lastImpostorCalcPos(byte index)
            {
                //return new Vector3(-0.25f, -0.25f, 0) + Vector3.right * index * 0.55f;
                return new Vector3(-0.25f, -0.15f, 0) + Vector3.right * index * 0.55f;
            }

            Action lastImpostorButtonOnClick(byte index)
            {
                return () =>
                {
                    PlayerControl p = Helpers.playerById(index);
                    LastImpostor.divine(p);
                };
            };

            Func<bool> lastImpostorHasButton(byte index)
            {
                return () =>
                {
                    var p = PlayerControl.LocalPlayer;
                    if (!MapOptions.playerIcons.ContainsKey(index) || !p.isRole(RoleId.LastImpostor)) return false;
                    else if (p.Data.IsDead || p.PlayerId == index || numUsed >= 1)
                    {
                        MapOptions.playerIcons[index].gameObject.SetActive(false);
                        return false;
                    }
                    else if (PlayerControl.LocalPlayer.isRole(RoleId.LastImpostor) && PlayerControl.LocalPlayer.CanMove && isCounterMax()) return true;
                    return false;
                };
            }


            Func<bool> lastImpostorCouldUse(byte index)
            {
                return () =>
                {
                    if (!MapOptions.playerIcons.ContainsKey(index) || !PlayerControl.LocalPlayer.isRole(RoleId.LastImpostor)) return false;
                    var p = Helpers.playerById(index);

                    Vector3 pos = lastImpostorCalcPos(index);
                    Vector3 scale = new Vector3(0.4f, 0.8f, 1.0f);

                    Vector3 iconBase = hm.UseButton.transform.localPosition;
                    iconBase.x *= -1;
                    if (lastImpostorButtons[index].PositionOffset != pos)
                    {
                        lastImpostorButtons[index].PositionOffset = pos;
                        lastImpostorButtons[index].LocalScale = scale;
                        MapOptions.playerIcons[index].transform.localPosition = iconBase + pos;
                    }

                    MapOptions.playerIcons[index].transform.localScale = Vector3.one * 0.25f;
                    MapOptions.playerIcons[index].gameObject.SetActive(PlayerControl.LocalPlayer.CanMove);
                    MapOptions.playerIcons[index].setSemiTransparent(false);
                    string buttonText =  PlayerControl.AllPlayerControls[index].isAlive() ? "生存" : "死亡";
                    lastImpostorButtons[index].buttonText = buttonText;
                    return PlayerControl.LocalPlayer.CanMove && numUsed < 1;
                };
            }


            for (byte i = 0; i < 15; i++)
            {
                //TheOtherRolesPlugin.Instance.Log.LogInfo($"Added {i}");
                // if(i >= PlayerControl.AllPlayerControls.Count) break;

                CustomButton lastImpostorButton = new CustomButton(
                    // Action OnClick
                    lastImpostorButtonOnClick(i),
                    // bool HasButton
                    lastImpostorHasButton(i),
                    // bool CouldUse
                    lastImpostorCouldUse(i),
                    // Action OnMeetingEnds
                    () => { },
                    // sprite
                    null,
                    // position
                    Vector3.zero,
                    // hudmanager
                    hm,
                    // keyboard shortcut
                    null,
                    KeyCode.None,
                    true
                );
                lastImpostorButton.Timer = 0.0f;
                lastImpostorButton.MaxTimer = 0.0f;

                lastImpostorButtons.Add(lastImpostorButton);
            }

        }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<LastImpostor>();
            killCounter = 0;
            numUsed = 0;
        }
        public static bool isCounterMax(){
            if(maxKillCounter <= killCounter) return true;
            return false;
        }

        public static void promoteToLastImpostor()
        {
            if(!isEnable) return;

            var impList = new List<PlayerControl>();
            foreach(var p in PlayerControl.AllPlayerControls)
            {
                if(p.isImpostor() && p.isAlive()) impList.Add(p);
            }
            if(impList.Count == 1)
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ImpostorPromotesToLastImpostor, Hazel.SendOption.Reliable, -1);
                writer.Write(impList[0].PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.impostorPromotesToLastImpostor(impList[0].PlayerId);
            }
        }
        public static void divine(PlayerControl p)
        {
            Uranai.divine(p, resultIsCrewOrNot);
            numUsed += 1;
        }
    }
}