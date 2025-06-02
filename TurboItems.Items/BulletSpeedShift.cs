using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    public class BulletSpeedShift : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Bullet Speed Shift";
            string resourceName = "TurboItems/Resources/devils_horns";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<BulletSpeedShift>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Debug";
            string longDesc = "Decreases player bullet speed by 10% and increases enemy bullet speed by 10%, additively.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, 0.1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.ProjectileSpeed, -0.1f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.EXCLUDED;
            item.CanBeDropped = true;
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