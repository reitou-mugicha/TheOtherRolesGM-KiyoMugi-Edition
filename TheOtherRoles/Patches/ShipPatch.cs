using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(NormalPlayerTask), nameof(NormalPlayerTask.PickRandomConsoles))]
    class NormalPlayerTaskPickRandomConsolesPatch
    {
        private static int numWireTask { get { return (int)CustomOptionHolder.numWireTask.getFloat(); } }
        static void Postfix(NormalPlayerTask __instance, TaskTypes taskType, byte[] consoleIds)
        {
            if (taskType != TaskTypes.FixWiring || !CustomOptionHolder.enableRandomWireTask.getBool()) return;
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