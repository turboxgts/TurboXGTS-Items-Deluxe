using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class AC15Pack : PassiveItem
    {
        public static void Register()
        {
            string itemName = "AC15 pack";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/default_item_bundle";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<AC15Pack>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Armorer's favorite!";
            string longDesc = "Comes pre-equipped with AC-15, Nanomachines, Holey Grail, and Armor Synthesizer";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.SPECIAL;
            item.CanBeDropped = false;
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(545).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(631).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(450).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(314).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}