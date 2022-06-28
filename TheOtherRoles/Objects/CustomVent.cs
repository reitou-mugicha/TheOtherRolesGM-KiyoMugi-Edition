//Source code by TORGM-Haoming Edition

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheOtherRoles.Objects
{

    public class CustomVent
    {
        public Vent vent;
        public static System.Collections.Generic.List<CustomVent> AllVents = new();
        public static bool flag = false;
        public CustomVent(Vector3 p)
        {
            // Create the vent
            var referenceVent = UnityEngine.Object.FindObjectOfType<Vent>();
            vent = UnityEngine.Object.Instantiate<Vent>(referenceVent);
            vent.transform.position = p;
            vent.Left = null;
            vent.Right = null;
            vent.Center = null;
            Vent tmp = ShipStatus.Instance.AllVents[0];
            vent.EnterVentAnim = tmp.EnterVentAnim;
            vent.ExitVentAnim = tmp.ExitVentAnim;
            vent.Offset = new Vector3(0f, 0.25f, 0f);
            vent.Id = ShipStatus.Instance.AllVents.Select(x => x.Id).Max() + 1; // Make sure we have a unique id
            var allVentsList = ShipStatus.Instance.AllVents.ToList();
            allVentsList.Add(vent);
            ShipStatus.Instance.AllVents = allVentsList.ToArray();
            vent.gameObject.SetActive(true);
            vent.name = "AdditionalVent_" + vent.Id;
            switch(PlayerControl.GameOptions.MapId)
            {
                case 2:
                    vent.transform.localScale = new Vector3(1.05f, 1.05f, 0f);
                    break;
                case 4:
                    vent.transform.localScale = new Vector3(1.17f, 1.17f, 0f);
                    break;
            }
            vent.transform.localScale = new Vector3(1.17f, 1.17f, 0f);
            AllVents.Add(this);
        }

        public static void AddAdditionalVents()
        {
            if (CustomVent.flag) return;
            CustomVent.flag = true;
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;

            //ここにベントを追加していく
            if(PlayerControl.GameOptions.MapId == 4 && CustomOptionHolder.enableAddCustomVent.getBool()) //AirShip
            {
                CustomVentPos.AirShipVentPos.VMAVent();
            }
            else if(PlayerControl.GameOptions.MapId == 2 && CustomOptionHolder.enableAddCustomVent.getBool()) //Polus
            {
                CustomVentPos.PolusVentPos.WOSVent();
            }
            else if(PlayerControl.GameOptions.MapId == 0 && CustomOptionHolder.enableAddCustomVent.getBool()) //Skeld
            {
                CustomVentPos.SkeldVentPos.SAOVent();
            }
            else if(SubmergedCompatibility.IsSubmerged && CustomOptionHolder.enableAddCustomVent.getBool())
            {
                CustomVentPos.SubmergedVentPos.SLBVent();
            }
        }

        public static void clearAndReload()
        {
            flag = false;
            AllVents = new List<CustomVent>();
        }
    }
}
