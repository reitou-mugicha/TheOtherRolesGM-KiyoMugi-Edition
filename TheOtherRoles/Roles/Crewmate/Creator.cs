/*using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hazel;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.Patches.PlayerControlFixedUpdatePatch;
using static TheOtherRoles.TheOtherRoles;
  using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class Creator : RoleBase<Creator>
    {
        public static Color color = new Color32(152, 251, 152, byte.MaxValue);
        public static PlayerControl currentTarget;
        public static CustomButton sheriffCreateButton;
        public static bool cancreatesheriff { get { return CustomOptionHolder.creatorCanCreateSheriff.getBool(); } }
        public static float createCooldown { get { return CustomOptionHolder.creatorCreateCooldown.getFloat(); } }

        public Creator()
        {
            RoleType = roleId = RoleType.Creator;
            currentTarget = null;
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate()
        {

            if (PlayerControl.LocalPlayer.isAlive() && sheriffCreateButton.PositionOffset == new Vector3(-1.8f, 1f, 0))
            {
                List<PlayerControl> untargetablePlayers = new List<PlayerControl>();
                foreach (var p in PlayerControl.AllPlayerControls)
                {
                    if (p.isImpostor() || p.isRole(RoleType.Jackal) || p.isRole(RoleType.Sheriff))
                    {
                        untargetablePlayers.Add(p);
                    }
                }
                currentTarget = setTarget(untargetablePlayers: untargetablePlayers);
                setPlayerOutline(currentTarget, Creator.color);
            }

        }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }

        public static void MakeButtons(HudManager hm)
        {

            if (cancreatesheriff)
            {
                sheriffCreateButton = new CustomButton(
                    () =>
                    {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CreatorCreateSheriff, Hazel.SendOption.Reliable, -1);
                        writer.Write(currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.CreatorCreateSheriff(currentTarget.PlayerId);
                        sheriffCreateButton.PositionOffset = new Vector3(1000, 1000, 0);
                    },
                    () => { return cancreatesheriff && PlayerControl.LocalPlayer.isRole(RoleType.Creator) && PlayerControl.LocalPlayer.isAlive(); },
                    () => { return cancreatesheriff && Creator.currentTarget != null && PlayerControl.LocalPlayer.CanMove; },
                    () => { sheriffCreateButton.Timer = 20; },
                    getSheriffCreateButtonSprite(),
                    new Vector3(-1.8f, 1f, 0),
                    hm,
                    hm.KillButton,
                    KeyCode.None

                );
                sheriffCreateButton.buttonText = ModTranslation.getString("SidekickText");
            }


        }

        private static Sprite sheriffSprite;
        public static Sprite getSheriffCreateButtonSprite()
        {
            if (sheriffSprite) return sheriffSprite;
            sheriffSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SidekickButton.png", 115f);
            return sheriffSprite;
        }

        public static void SetButtonCooldowns()
        {

            sheriffCreateButton.MaxTimer = createCooldown;

        }

        public static void Clear()
        {
            players = new List<Creator>();
        }
    }
}*/