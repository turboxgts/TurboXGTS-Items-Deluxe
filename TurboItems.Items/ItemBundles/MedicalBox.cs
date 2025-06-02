using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class MedicalBox : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Medical Box";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/medical_box";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<MedicalBox>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Heals!";
            string longDesc = "Used by the medics as an on-the-go solution for all healing-based needs!";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.SPECIAL;
            item.CanBeDropped = false;
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalItemCapacity, 1);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(208).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(63).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(259).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(424).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(453).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }


    }
}