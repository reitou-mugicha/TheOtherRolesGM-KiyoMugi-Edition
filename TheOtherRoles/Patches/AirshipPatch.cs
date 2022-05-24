using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using Hazel;
using UnityEngine;
using static TheOtherRoles.GameHistory;
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Awake))]
    class OptimizeMapPatch
    {
        public static void Postfix(ShipStatus __instance)
        {
            addWireTasks(PlayerControl.GameOptions.MapId);
            optimizeMap(PlayerControl.GameOptions.MapId);
        }
        public static void optimizeMap(int mapId)
        {
            if (!CustomOptionHolder.airshipOptimizeMap.getBool()) return;
            if (mapId == 4)
            {
                var obj = ShipStatus.Instance.FastRooms[SystemTypes.GapRoom].gameObject;
                //インポスターについてのみ影を無効化
                obj.transform.FindChild("Shadow").FindChild("LedgeShadow").GetComponent<OneWayShadows>().IgnoreImpostor = true;

                SpriteRenderer renderer;

                GameObject fance = new("ModFance")
                {
                    layer = LayerMask.NameToLayer("Ship")
                };
                fance.transform.SetParent(obj.transform);
                fance.transform.localPosition = new Vector3(4.2f, 0.15f, 0.5f);
                fance.transform.localScale = new Vector3(1f, 1f, 1f);
                fance.SetActive(true);
                var Collider = fance.AddComponent<EdgeCollider2D>();
                Collider.points = new Vector2[] { new Vector2(1.5f, -0.2f), new Vector2(-1.5f, -0.2f), new Vector2(-1.5f, 1.5f) };
                Collider.enabled = true;
                renderer = fance.AddComponent<SpriteRenderer>();
                renderer.sprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AirshipFance.png", 100f);

                GameObject pole = new("DownloadPole")
                {
                    layer = LayerMask.NameToLayer("Ship")
                };
                pole.transform.SetParent(obj.transform);
                pole.transform.localPosition = new Vector3(4.1f, 0.75f, 0.8f);
                pole.transform.localScale = new Vector3(1f, 1f, 1f);
                renderer = pole.AddComponent<SpriteRenderer>();
                renderer.sprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AirshipDownloadG.png", 100f);

                var panel = obj.transform.FindChild("panel_data");
                panel.localPosition = new Vector3(4.1f, 0.72f, 0.1f);
                panel.gameObject.GetComponent<Console>().usableDistance = 0.9f;
            }

        }
        public static void addWireTasks(int mapId)
        {
            if (!CustomOptionHolder.additionalWireTask.getBool()) return;
            // Airshipの場合
            if (mapId == 4)
            {
                ActivateWiring("task_wiresHallway2", 2);
                ActivateWiring("task_electricalside2", 3).Room = SystemTypes.Armory;
                ActivateWiring("task_wireShower", 4);
                ActivateWiring("taks_wiresLounge", 5);
                ActivateWiring("panel_wireHallwayL", 6);
                ActivateWiring("task_wiresStorage", 7);
                ActivateWiring("task_electricalSide", 8).Room = SystemTypes.VaultRoom;
                ActivateWiring("task_wiresMeeting", 9);
            }
        }
        protected static Console ActivateWiring(string consoleName, int consoleId)
        {
            Console console = ActivateConsole(consoleName);

            if (console == null)
            {
                Logger.error($"consoleName \"{consoleName}\" is null", "ActivateWiring");
                return null;
            }

            if (!console.TaskTypes.Contains(TaskTypes.FixWiring))
            {
                var list = console.TaskTypes.ToList();
                list.Add(TaskTypes.FixWiring);
                console.TaskTypes = list.ToArray();
            }
            console.ConsoleId = consoleId;
            return console;
        }
        protected static Console ActivateConsole(string objectName)
        {
            GameObject obj = UnityEngine.GameObject.Find(objectName);
            if (obj == null)
            {
                Logger.error($"Object \"{objectName}\" was not found!", "ActivateConsole");
                return null;
            }
            obj.layer = LayerMask.NameToLayer("ShortObjects");
            Console console = obj.GetComponent<Console>();
            PassiveButton button = obj.GetComponent<PassiveButton>();
            CircleCollider2D collider = obj.GetComponent<CircleCollider2D>();
            if (!console)
            {
                console = obj.AddComponent<Console>();
                console.checkWalls = true;
                console.usableDistance = 0.7f;
                console.TaskTypes = new TaskTypes[0];
                console.ValidTasks = new UnhollowerBaseLib.Il2CppReferenceArray<TaskSet>(0);
                var list = ShipStatus.Instance.AllConsoles.ToList();
                list.Add(console);
                ShipStatus.Instance.AllConsoles = new UnhollowerBaseLib.Il2CppReferenceArray<Console>(list.ToArray());
            }
            if (console.Image == null)
            {
                console.Image = obj.GetComponent<SpriteRenderer>();
                console.Image.material = new Material(ShipStatus.Instance.AllConsoles[0].Image.material);
            }
            if (!button)
            {
                button = obj.AddComponent<PassiveButton>();
                button.OnMouseOut = new UnityEngine.Events.UnityEvent();
                button.OnMouseOver = new UnityEngine.Events.UnityEvent();
                button._CachedZ_k__BackingField = 0.1f;
                button.CachedZ = 0.1f;
            }
            if (!collider)
            {
                collider = obj.AddComponent<CircleCollider2D>();
                collider.radius = 0.4f;
                collider.isTrigger = true;
            }
            return console;
        }
    }

    [HarmonyPatch(typeof(NormalPlayerTask), nameof(NormalPlayerTask.PickRandomConsoles))]
    class NormalPlayerTaskPickRandomConsolesPatch
    {
        private static int numWireTask { get { return (int)CustomOptionHolder.numWireTask.getFloat(); } }
        static void Postfix(NormalPlayerTask __instance, TaskTypes taskType, byte[] consoleIds)
        {
            if (taskType != TaskTypes.FixWiring || !CustomOptionHolder.randomWireTask.getBool()) return;
            List<Console> orgList = ShipStatus.Instance.AllConsoles.Where((global::Console t) => t.TaskTypes.Contains(taskType)).ToList<global::Console>();
            List<Console> list = new(orgList);

            __instance.MaxStep = numWireTask;
            __instance.Data = new byte[numWireTask];
            for (int i = 0; i < __instance.Data.Length; i++)
            {
                if (list.Count == 0)
                    list = new List<Console>(orgList);
                int index = list.RandomIdx<global::Console>();
                __instance.Data[i] = (byte)list[index].ConsoleId;
                list.RemoveAt(index);
            }
        }
    }
}
