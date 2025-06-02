using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class BeholsterTentacle : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Beholster's Tentacle";
            string resourceName = "TurboItems/Resources/beholster_tentacle";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<BeholsterTentacle>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Don't blink";
            string longDesc = "One of the Beholster's tentacles, still fresh with the gun in its hand. Grants the user a weaker form of the Beholster's power.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
            item.CanBeDropped = false;
            List<string> mandatoryConsoleIDs = new List<string>
                {
                    "eye_of_the_beholster",
                    "turbo:beholster's_tentacle",

                };

            var synergy = CustomSynergies.Add("T H E   E Y E S", mandatoryConsoleIDs);
            synergy.statModifiers = new List<StatModifier>
                {
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.Damage,
                        amount = 0.66f,
                    },
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.ReloadSpeed,
                        amount = 1.33f,
                    },

                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.RateOfFire,
                        amount = 0.75f,
                    },
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.AmmoCapacityMultiplier,
                        amount = 0,
                    },
                };
            synergy.ActiveWhenGunUnequipped = false;
        }
        public override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 90)
            {
                Owner.InfiniteAmmo.SetOverride("ghghghfsdbrg", true, null);
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("ghghghfsdbrg", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(90).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(30).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(32).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(129).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(42).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(43).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}