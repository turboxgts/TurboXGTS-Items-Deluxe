using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class BundleTemplate : PassiveItem
    {
        public static void Register()
        {
            string itemName = "ItemBundleTemplate";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/default_item_bundle";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<BundleTemplate>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Template";
            string longDesc = "If someone uses this code and doesn't change the description then I get to laugh at them";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.EXCLUDED; //EXCLUDED removes it from the item pool
            item.CanBeDropped = false; //prevents players from dropping the item

            List<string> mandatoryConsoleIDs = new List<string> //used for custom synergy, I use it to nerf guns specifically instead of directly changing player stats and usually give them infinite ammo
                {

                };

            var synergy = CustomSynergies.Add("TemplateSynergy", mandatoryConsoleIDs);
            synergy.statModifiers = new List<StatModifier>
                {
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE, //MULTIPLICITAVE multiplies by amount, ADDITIVE adds a flat amount
                        statToBoost = PlayerStats.StatType.Damage,
                        amount = 1f,
                    },
                };
            synergy.ActiveWhenGunUnequipped = false;
        }
        public override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 0)
            {
                Owner.InfiniteAmmo.SetOverride("", true, null); //set the quotations in both to be the same but unique to anything else
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(9).gameObject, base.Owner); //gives items to player on pickup
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}