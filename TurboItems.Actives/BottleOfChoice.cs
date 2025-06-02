using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    class ChoiceBottle : PlayerItem
    {
        public static void Init()
        {
            string itemName = "Bottle of Choice";
            string resourceName = "TurboItems/Resources/bottle_of_choice";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<ChoiceBottle>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Definitely not Identity Crisis";
            string longDesc = "If you look into it, you can see things swirling around.... they're calling to you. Which will you choose?\n\n   Definitely not a shameless ripoff of Nevernamed's Identity Crisis item from Once More Into the Breach.\n\nthanks for the code nn <3 :)";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.None, 1000);
            item.consumable = true;
            item.quality = ItemQuality.B;
        }
        public override void DoEffect(PlayerController player)
        {
            int Bundle = UnityEngine.Random.Range(1, 8);
            if (Bundle == 1)
            {
                player.GiveItem("turbo:samus'_helmet");
            }
            else if (Bundle == 2)
            {
                //player.GiveItem("turbo:blood_covered_cloak");
                player.GiveItem("turbo:ice_tray");
            }
            else if (Bundle == 3)
            {
                player.GiveItem("turbo:ac15_pack");
            }
            else if (Bundle == 4)
            {
                player.GiveItem("turbo:gunbow_pack");
            }
            else if (Bundle == 5)
            {
                player.GiveItem("turbo:trank_gun_pack");
            }
            else if (Bundle == 6)
            {
                player.GiveItem("turbo:monster_hunting_kit");
            }
            else if (Bundle == 7)
            {
                player.GiveItem("turbo:medical_box");
            }
            //else if (Bundle == 8)
            //{
                //player.GiveItem("turbo:ice_tray");
            //}
        }
    }
}