using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class TrankGunPack : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Trank Gun pack";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/default_item_bundle";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<TrankGunPack>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Tranquilizers uwu";
            string longDesc = "Comes pre-equipped with the Trank Gun in its tranquilizer form with extra piercing.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.SPECIAL;
            item.CanBeDropped = false;
            item.AddItemToSynergy("#NEEDSCISSORS", true);

            List<string> mandatoryConsoleIDs = new List<string>
            {
                "trank_gun",
                "turbo:trank_gun_pack"
            };

            var synergy = CustomSynergies.Add("Why isn't the trank gun already piercing?", mandatoryConsoleIDs);
            synergy.statModifiers = new List<StatModifier>
                {
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.Damage,
                        amount = 0.8f,
                    },
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.ReloadSpeed,
                        amount = 1.2f,
                    },
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.ADDITIVE,
                        statToBoost = PlayerStats.StatType.AdditionalShotPiercing,
                        amount = 1f,
                    },
                };
            synergy.ActiveWhenGunUnequipped = false;
        }
        public override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 42)
            {
                Owner.InfiniteAmmo.SetOverride("lplplp", true, null);
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("lplplp", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(42).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}