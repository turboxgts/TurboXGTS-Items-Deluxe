using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;

namespace TurboItems
{
    class InitialiseSynergies
    {
        public static void DoInitialisation()
        {
            List<string> mandatorySynergyItemsTestSynergy = new List<string>() { "turbo:yin", "turbo:yang" };
            CustomSynergies.Add("Yin and Yang", mandatorySynergyItemsTestSynergy);
            //List<string> mandatorySynergyGargoylesHandsSynergy = new List<string>() { "turbo:gargoyles_hand_left", "turbo:gargoyles_right_hand" };
            //CustomSynergies.Add("Gargoyle's Hands", mandatorySynergyGargoylesHandsSynergy);
            List<string> mandatorySynergyItemsDragunsRoar = new List<string>() { "dragunfire", "high_dragunfire" };
            CustomSynergies.Add("Dragun's Roar", mandatorySynergyItemsDragunsRoar);
            List<string> mandatorySynergyItemsTexasRed = new List<string>() { "big_iron", "hip_holster" };
            CustomSynergies.Add("Texas Red", mandatorySynergyItemsTexasRed);
        }
    }
}
