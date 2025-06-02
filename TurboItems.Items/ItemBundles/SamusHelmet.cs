using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class SamusHelmet : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Samus' Helmet";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/samus_helmet";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<SamusHelmet>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Target Locked";
            string longDesc = "A helmet belonging to a very successful galactic bounty hunter. Use its power wisely.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Coolness, 3);
            item.quality = PickupObject.ItemQuality.SPECIAL;
			item.CanBeDropped = false;

            List<string> mandatoryConsoleIDs = new List<string>
                {
                    "heroine",
                    "turbo:samus'_helmet"

                };

            var synergy = CustomSynergies.Add("See you, space cowboy...", mandatoryConsoleIDs);
            synergy.statModifiers = new List<StatModifier>
                {
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.Damage,
                        amount = 0.5f,
                    },

                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.ChargeAmountMultiplier,
                        amount = 0.5f,
                    },

                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.ReloadSpeed,
                        amount = 2,
                    },

                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.RateOfFire,
                        amount = 0.5f,
                    },
                };
            synergy.ActiveWhenGunUnequipped = false;
            item.AddItemToSynergy("#SAMUSPLASMA", true);
            item.AddItemToSynergy("#SAMUSICE", true);
            item.AddItemToSynergy("#SAMUSWAVE", true);
        }
        public override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 41)
            {
                Owner.InfiniteAmmo.SetOverride("isdubsoeri", true, null);
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("isdubsoeri", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(41).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(567).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}