/*
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.GameHistory;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public class King : RoleBase<King>
    {
        public static PlayerControl king;
        public static CustomButton meetingButton;
        public static Color color = new Color32(0, 250, 154, byte.MaxValue);
        public static List<Arrow> arrows = new List<Arrow>();
        public static float updateTimer = 0f;
        public static float arrowUpdateInterval = 0.5f;
        public static bool crewWinsByTasks { get { return CustomOptionHolder.kingdomCrewWinsByTasks.getBool(); } }
        public static int numCommonTasks { get { return CustomOptionHolder.kingTasks.commonTasks; } }
        public static int numLongTasks { get { return CustomOptionHolder.kingTasks.longTasks; } }
        public static int numShortTasks { get { return CustomOptionHolder.kingTasks.shortTasks; } }
        public static List<byte> exiledKing = new List<byte>();

        public King()
        {
            RoleType = roleId = RoleType.King;
            exiledKing = new List<byte>();
        }

        public override void OnMeetingStart() { }
        public override void OnMeetingEnd() { }
        public override void FixedUpdate() { }
        public override void OnKill(PlayerControl target) { }
        public override void OnDeath(PlayerControl killer = null) { }
        public override void HandleDisconnect(PlayerControl player, DisconnectReasons reason) { }
        public static void MakeButtons(HudManager hm) { }
        public static void SetButtonCooldowns() { }

        public static void Clear()
        {
            players = new List<King>();
            foreach (Arrow arrow in arrows)
            {
                if (arrow?.arrow != null)
                {
                    arrow.arrow.SetActive(false);
                    UnityEngine.Object.Destroy(arrow.arrow);
                }
            }
            arrows = new List<Arrow>();
        }

        public static bool isKingAlive()
        {
            bool isAlive = false;
            foreach (var fox in King.allPlayers)
            {
                if (king.isAlive() && !exiledKing.Contains(king.PlayerId))
                {
                    isAlive = true;
                }
            }
            return isAlive;
        }

        public static bool isKingCompletedTasks()
        {
            // 生存中の王がタスクを終了しているかを確認
            bool isCompleted = false;
            foreach (var king in allPlayers)
            {
                if (king.isAlive())
                {
                    if (tasksComplete(king))
                    {
                        isCompleted = true;
                        break;
                    }
                }
            }
            return isCompleted;
        }

        private static bool tasksComplete(PlayerControl p)
        {
            int counter = 0;
            int totalTasks = numCommonTasks + numLongTasks + numShortTasks;
            if (totalTasks == 0) return true;
            foreach (var task in p.Data.Tasks)
            {
                if (task.Complete)
                {
                    counter++;
                }
            }
            return counter >= totalTasks;
        }

        public void assignTasks()
        {
            player.generateAndAssignTasks(numCommonTasks, numShortTasks, numLongTasks);
        }

        static void arrowUpdate()
        {

            // 前フレームからの経過時間をマイナスする
            updateTimer -= Time.fixedDeltaTime;

            // 1秒経過したらArrowを更新
            if (updateTimer <= 0.0f)
            {

                // 前回のArrowをすべて破棄する
                foreach (Arrow arrow in arrows)
                {
                    if (arrow?.arrow != null)
                    {
                        arrow.arrow.SetActive(false);
                        UnityEngine.Object.Destroy(arrow.arrow);
                    }
                }

                // Arrorw一覧
                arrows = new List<Arrow>();

                // インポスターの位置を示すArrorwを描画
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    if (p.isDead()) continue;
                    Arrow arrow;
                    // float distance = Vector2.Distance(p.transform.position, PlayerControl.LocalPlayer.transform.position);
                    if (p.isRole(RoleType.Minions))
                    {
                        if (p.isRole(RoleType.Minions))
                        {
                            arrow = new Arrow(Minions.color);
                        }
                        else
                        {
                            arrow = new Arrow(Palette.Black);
                        }
                        arrow.arrow.SetActive(true);
                        arrow.Update(p.transform.position);
                        arrows.Add(arrow);
                    }
                }

                // タイマーに時間をセット
                updateTimer = arrowUpdateInterval;
            }
            else
            {
                arrows.Do(x => x.Update());
            }
        }
    }
}
*/