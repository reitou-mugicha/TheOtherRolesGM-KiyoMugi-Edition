using UnityEngine;

namespace TheOtherRoles.Objects
{
    public class CustomVentPos
    {
        static float z = PlayerControl.LocalPlayer.transform.position.z + 1f;
        public class AirShipVentPos
        {
            public static void VMAVent()
            {
                CustomVent vault = new(new Vector3(-8.75f, 12.5f, z)); //金庫上
                CustomVent meeting = new(new Vector3(16.17f, 15.2f, z)); //ミーティングID下
                CustomVent archives = new(new Vector3(19.85f, 11.5f, z)); //アーカイブ上

                vault.vent.Right = meeting.vent;
                meeting.vent.Left = vault.vent;
                meeting.vent.Right = archives.vent;
                archives.vent.Left = meeting.vent;
            }
        }

        public class PolusVentPos
        {
            public static void WOSVent()
            {
                CustomVent weapon = new(new Vector3(14.9f, -24.5f, z));
                CustomVent office = new(new Vector3(30.5f, -17.15f, z));
                CustomVent specimen = new(new Vector3(36.5f, -21.5f, z));

                weapon.vent.Right = office.vent;
                office.vent.Left = weapon.vent;
                office.vent.Right = specimen.vent;
                specimen.vent.Left = office.vent;
            }
        }

        public class SkeldVentPos
        {
            public static void SAOVent()
            {
                CustomVent storage = new(new Vector3(-3.6f, -11.5f, z));
                CustomVent admin = new(new Vector3(6.5f, -7.5f, z));
                CustomVent o2 = new(new Vector3(6.65f, -3.6f, z));

                storage.vent.Right = admin.vent;
                admin.vent.Left = storage.vent;
                admin.vent.Right = o2.vent;
                o2.vent.Left = admin.vent;
            }
        }

        public class SubmergedVentPos
        {
            public static void SLBVent()
            {
                CustomVent storage = new(new Vector3(4.875f, -33.6f, z));
                CustomVent lowerLobby = new(new Vector3(8.7f, -40.3f, z));
                CustomVent ballast = new(new Vector3(-8.2f, -36.35f, z));

                storage.vent.Left = lowerLobby.vent;
                lowerLobby.vent.Right = storage.vent;
                lowerLobby.vent.Left = ballast.vent;
                ballast.vent.Right = lowerLobby.vent;
                storage.vent.Right = ballast.vent;
                ballast.vent.Left = storage.vent;
            }
        }
    }
}