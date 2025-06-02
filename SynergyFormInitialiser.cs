using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    class SynergyFormInitialiser
    {
        public static void SynergyInitialiser()
        { 
            //this is just a test, nobody look at this :(
            //and yes I stole Nevernamed's code, what are you gonna do about it? tell him?

            //test dual wield synergy
            AdvancedDualWieldSynergyProcessor TestGunDualWieldSynergyTESTONE = (PickupObjectDatabase.GetById(Yin.YinID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            TestGunDualWieldSynergyTESTONE.PartnerGunID = Yang.YangID;
            TestGunDualWieldSynergyTESTONE.SynergyNameToCheck = "Yin and Yang";
            AdvancedDualWieldSynergyProcessor TestGunDualWieldSynergyTESTTWO = (PickupObjectDatabase.GetById(Yang.YangID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            TestGunDualWieldSynergyTESTTWO.PartnerGunID = Yin.YinID;
            TestGunDualWieldSynergyTESTTWO.SynergyNameToCheck = "Yin and Yang";

            //Gargoyle's Hands synergy
            //AdvancedDualWieldSynergyProcessor GargoylesHandsDualWieldSynergyLEFTHAND = (PickupObjectDatabase.GetById(GargoyleHandLeft.GargoyleLeftID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            //GargoylesHandsDualWieldSynergyLEFTHAND.PartnerGunID = GargoyleHandRight.GargoyleRightID;
            //GargoylesHandsDualWieldSynergyLEFTHAND.SynergyNameToCheck = "Gargoyle's Hands";
            //AdvancedDualWieldSynergyProcessor GargoylesHandsDualWieldSynergyRIGHTHAND = (PickupObjectDatabase.GetById(GargoyleHandRight.GargoyleRightID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            //GargoylesHandsDualWieldSynergyRIGHTHAND.PartnerGunID = GargoyleHandLeft.GargoyleLeftID;
            //GargoylesHandsDualWieldSynergyRIGHTHAND.SynergyNameToCheck = "Gargoyle's Hands";

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