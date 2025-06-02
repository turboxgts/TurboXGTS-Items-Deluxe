using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class GunbowPack : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Gunbow pack";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/default_item_bundle";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<GunbowPack>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "BANG!";
            string longDesc = "Comes pre-equipped with Rocket-Powered Bullets and the Gunbow";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.SPECIAL;
            item.CanBeDropped = false;

            List<string> mandatoryConsoleIDs = new List<string>
            {
                "turbo:gunbow_pack",
                "gunbow"
            };

            var synergy = CustomSynergies.Add("Gunbow wheee", mandatoryConsoleIDs);
            synergy.statModifiers = new List<StatModifier>
                {
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.Damage,
                        amount = 0.33f,
                    },
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.ReloadSpeed,
                        amount = 2f,
                    },
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.ChargeAmountMultiplier,
                        amount = 0.5f,
                    },
                };
            synergy.ActiveWhenGunUnequipped = false;
        }
        public override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 210)
            {
                Owner.InfiniteAmmo.SetOverride("rybjtrfb", true, null);
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("rybjtrfb", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(210).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(113).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}