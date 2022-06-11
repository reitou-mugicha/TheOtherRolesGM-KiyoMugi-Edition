using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(TaskAdderGame), nameof(TaskAdderGame.ShowFolder))]
    public class TaskPCPatch
    {
        private static TaskFolder KMCustomFolder;
        private static TaskFolder CustomRoleFolder;

        public static void Prefix(TaskAdderGame __instance, [HarmonyArgument(0)] TaskFolder taskFolder)
        {
            if(__instance.Root == taskFolder && KMCustomFolder == null)
            {
                //Create KM Original Folder
                TaskFolder kmFolder = UnityEngine.Object.Instantiate<TaskFolder>(
                    __instance.RootFolderPrefab,
                    __instance.transform
                );
                kmFolder.gameObject.SetActive(false);
                kmFolder.FolderName = "TORGM KM";
                KMCustomFolder = kmFolder;
                __instance.Root.SubFolders.Add(kmFolder);

                //Create CustomRoles Folder
                if(__instance.Root == taskFolder && CustomRoleFolder == null)
                {
                    TaskFolder crFolder = UnityEngine.Object.Instantiate<TaskFolder>(
                        __instance.RootFolderPrefab,
                        __instance.transform
                    );
                    crFolder.gameObject.SetActive(false);
                    crFolder.FolderName = "CustomRoles";
                    CustomRoleFolder = crFolder;
                    KMCustomFolder.SubFolders.Add(crFolder);
                }
            }
        }

        public static void Postfix(TaskAdderGame __instance, [HarmonyArgument(0)] TaskFolder taskFolder)
        {
            float xCursor = 0f;
            float yCursor = 0f;
            float maxHeight = 0f;
            if(KMCustomFolder != null && KMCustomFolder.FolderName == taskFolder.FolderName)
            {
                //ここにファイルを追加するコード書く
                //xCursorとかはrefで渡す
            }
        }
    }
}