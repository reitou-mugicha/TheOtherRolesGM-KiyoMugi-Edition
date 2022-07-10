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
            if(CustomRoleFolder != null && CustomRoleFolder.FolderName == taskFolder.FolderName)
            {
                foreach(RoleInfo roleInfo in RoleInfo.allRoleInfos)
                {
                    string RoleName = Helpers.cs(roleInfo.color, roleInfo.name);
                    TaskAddButton roleButton = UnityEngine.Object.Instantiate<TaskAddButton>(__instance.RoleButton);
                    roleButton.Text.text = "<size=80%>" + RoleName + "</size>";
                    roleButton.Text.transform.position += new Vector3(0f, 0.5f, 0f);
                    __instance.AddFileAsChild(CustomRoleFolder, roleButton, ref xCursor, ref yCursor, ref maxHeight);
                }
                //ここにファイルを追加するコード書く
                //xCursorとかはrefで渡す
            }
        }
    }
}