using HarmonyLib;
using UnityEngine;

namespace TheOtherRoles.Mode.InversionMode
{
    [HarmonyPatch(typeof(ShipStatus), nameof(GameStartManager.Start))]
    public class inversion
    {

        public static GameObject skeld;
        public static GameObject mira;
        public static GameObject polus;
        public static GameObject airship;
        public static GameObject airshipsp;
        public static void Prefix()
        {

            if (PlayerControl.GameOptions.MapId == 0 && CustomOptionHolder.enableMirrorMap.getBool())
            {
                skeld = GameObject.Find("SkeldShip(Clone)");
                skeld.transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
                SkeldShipStatus.Instance.InitialSpawnCenter = new Vector2(0.8f, 0.6f);
                SkeldShipStatus.Instance.MeetingSpawnCenter = new Vector2(0.8f, 0.6f);
            }
            else if (PlayerControl.GameOptions.MapId == 1 && CustomOptionHolder.enableMirrorMap.getBool())
            {
                mira = GameObject.Find("MiraShip(Clone)");
                mira.transform.localScale = new Vector3(-1f, 1f, 1f);
                MiraShipStatus.Instance.InitialSpawnCenter = new Vector2(4.4f, 2.2f);
                MiraShipStatus.Instance.MeetingSpawnCenter = new Vector2(-25.3921f, 2.5626f);
                MiraShipStatus.Instance.MeetingSpawnCenter2 = new Vector2(-25.3921f, 2.5626f);
            }
            else if (PlayerControl.GameOptions.MapId == 2 && CustomOptionHolder.enableMirrorMap.getBool())
            {
                polus = GameObject.Find("PolusShip(Clone)");
                polus.transform.localScale = new Vector3(-1f, 1f, 1f);
                PolusShipStatus.Instance.InitialSpawnCenter = new Vector2(-16.7f, -2.1f);
                PolusShipStatus.Instance.MeetingSpawnCenter = new Vector2(-19.5f, -17f);
                PolusShipStatus.Instance.MeetingSpawnCenter2 = new Vector2(-19.5f, -17f);
            }
            else if (PlayerControl.GameOptions.MapId == 4 && CustomOptionHolder.enableMirrorMap.getBool())
            {/*
                AirshipStatus.Instance.InitialSpawnCenter = new Vector2(-3.4f, -28.35f);
                AirshipStatus.Instance.MeetingSpawnCenter = new Vector2(-3.4f, -28.35f);
                AirshipStatus.Instance.MeetingSpawnCenter2 = new Vector2(-3.4f, -28.35f);
                AirshipStatus.Instance.transform.localScale = new Vector3(-0.8f, 0.8f, 1f);*/
            }
        }
    }
}

/*めも
反転AirShip湧き位置
宿舎前 0.8 8.5
エンジン 0.7 -0.5
アーカイブ -19.8 10
メインホール -12 0
貨物室 -33 0
キッチン 7 -11
*/