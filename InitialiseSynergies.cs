using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alexandria.ItemAPI;

namespace TurboItems
{
    class InitialiseSynergies
    {
        public static void DoInitialisation()
        {
            List<string> mandatorySynergyItemsDragunsRoar = new List<string>() { "dragunfire", "high_dragunfire" };
            CustomSynergies.Add("Dragun's Roar", mandatorySynergyItemsDragunsRoar);
            List<string> mandatorySynergyItemsTexasRed = new List<string>() { "big_iron", "hip_holster" };
            CustomSynergies.Add("Texas Red", mandatorySynergyItemsTexasRed);
        }
    }
}
