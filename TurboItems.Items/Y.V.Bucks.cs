using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    public class YVBucks : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Y.V.Bucks";
            string resourceName = "TurboItems/Resources/Y.V.Bucks";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<YVBucks>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Another one? Really?!";
            string longDesc = "Every casing gives an additive 0.2% chance to fire out 1 to 4 extra bullets.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, 0.1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.ProjectileSpeed, -0.1f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.EXCLUDED;
            item.CanBeDropped = false;
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}