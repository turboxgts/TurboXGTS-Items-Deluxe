using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexandria.ItemAPI;
using UnityEngine;

namespace TurboItems
{
    class SynergyFormInitialiser
    {
        public static void SynergyInitialiser()
        { 
            //this is just a test, nobody look at this :(
            //and yes I stole Nevernamed's code, what are you gonna do about it? tell him?

            // - Me, 4 years ago, apparently

            //Dragun's Roar synergy
            AdvancedDualWieldSynergyProcessor DragunsRoarDualWieldSynergyDRAGUNFIRE = (PickupObjectDatabase.GetById(146)).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            DragunsRoarDualWieldSynergyDRAGUNFIRE.PartnerGunID = 670;
            DragunsRoarDualWieldSynergyDRAGUNFIRE.SynergyNameToCheck = "Dragun's Roar";
            AdvancedDualWieldSynergyProcessor DragunsRoarDualWieldSynergyHIGHDRAGUNFIRE = (PickupObjectDatabase.GetById(670)).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            DragunsRoarDualWieldSynergyHIGHDRAGUNFIRE.PartnerGunID = 146;
            DragunsRoarDualWieldSynergyHIGHDRAGUNFIRE.SynergyNameToCheck = "Dragun's Roar";
        }
    }
}