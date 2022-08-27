using UnityEngine;
using HarmonyLib;
using TheOtherRoles.Modules;
using TheOtherRoles.Utilities;
using Hazel;

namespace TheOtherRoles.Patches
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    public class BreadPatch
    {
        private static TMPro.TextMeshPro breadText;
        public static bool isBomb = false;
        public static System.Random bombBread = new System.Random();
        public static System.Random luckyBread = new System.Random();
        public static int rate {get{return Mathf.RoundToInt(CustomOptionHolder.bakeryBombBreadRate.getFloat());}}
        
        public static void Postfix(ExileController __instance)
        {
            if(!Bakery.isBakeryAlive()) return;

            int num = bombBread.Next(1, 101);
            int lnum = Bakery.lnum;

            breadText = UnityEngine.Object.Instantiate(__instance.ImpostorText, __instance.Text.transform);

            string ExileStr;

            if(Bakery.isBakeryAlive())
            {   
                
                if(num >= 100 - rate && Bakery.enableBombBread) //爆発するパン
                {
                    ExileStr = ModTranslation.getString("BombBakeryText");
                    isBomb = true;
                }
                else if(lnum <= 20)
                {
                    ExileStr = ModTranslation.getString("FrenchBakeryText");
                }
                else if(lnum > 20 && lnum <= 40)
                {
                    ExileStr = ModTranslation.getString("AnpanBakeryText");
                } 
                else if(lnum > 90)
                {
                    ExileStr = ModTranslation.getString("MelonBakeryText");
                }
                else ExileStr = ModTranslation.getString("GoodBakeryText");
                breadText.text = ExileStr;

                //位置移動
                if (PlayerControl.GameOptions.ConfirmImpostor)
                {
                    breadText.transform.localPosition += new UnityEngine.Vector3(0f, -0.4f, 0f);
                }
                else
                {
                    breadText.transform.localPosition += new UnityEngine.Vector3(0f, -0.2f, 0f);
                }
                
                breadText.gameObject.SetActive(true);

                if(isBomb) //爆発するパンなら爆発させる
                {
                    foreach (var bakery in Bakery.allPlayers)
                    {
                        if (bakery.isAlive())
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.RPCExiled, Hazel.SendOption.Reliable, -1);
                            writer.Write(bakery.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.RPCExiled(bakery.PlayerId);
                            isBomb = false;
                        }
                    }
                }
            }
        }
    }
}