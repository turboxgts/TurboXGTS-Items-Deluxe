using System.Collections.Generic;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    public class HuntingKit : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Monster Hunting Kit";
            string resourceName = "TurboItems/Resources/ItemBundleSprites/monster_hunting_kit";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<HuntingKit>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Bane of the Jammed";
            string longDesc = "Contains a pouch of silver bullets, a dueling pistol, and a bullet stamper that engraves an insignia that angers the Jammed.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 6);
            item.quality = PickupObject.ItemQuality.SPECIAL;
            item.CanBeDropped = false;

            List<string> mandatoryConsoleIDs = new List<string>
                {
                    "dueling_pistol",
                    "turbo:monster_hunting_kit"
                };

            var synergy = CustomSynergies.Add("Dueling Master", mandatoryConsoleIDs);
            synergy.statModifiers = new List<StatModifier>
                {
                    new StatModifier
                    {
                        modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                        statToBoost = PlayerStats.StatType.Damage,
                        amount = 0.75f,
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
                        statToBoost = PlayerStats.StatType.DamageToBosses,
                        amount = 1.2f,
                    },
                };
            synergy.ActiveWhenGunUnequipped = false;
        }
        public override void Update()
        {
            if (base.Owner.CurrentGun.PickupObjectId == 9)
            {
                Owner.InfiniteAmmo.SetOverride("haglaglagl", true, null);
            }
            else
            {
                Owner.InfiniteAmmo.SetOverride("haglaglagl", false, null);
            }
            base.Update();
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(9).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(538).gameObject, base.Owner);
            LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(571).gameObject, base.Owner);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}