using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;
using Dungeonator;

namespace TurboItems
{
    class KoopaShell : PlayerItem
    {
        public static void Init()
        {
            string itemName = "Koopa Shell";
            string resourceName = "TurboItems/Resources/koopa_shell";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<KoopaShell>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "L IS REAL 2401";
            string longDesc = "Lets you ride on top of a shell! Grants contact damage immunity while active.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
            //ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 0.5f);
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.None, 1000f);
            item.consumable = false;
            item.quality = ItemQuality.EXCLUDED;
        }
        bool isActive = false;
        private GameObject projObj;
        public override void DoEffect(PlayerController player)
        {
            projObj = null;
            if (isActive)
            {
                if (projObj != null)
                    UnityEngine.Object.Destroy(projObj);
                isActive = false;
            }
            else
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[376]).DefaultModule.projectiles[0];
                projObj = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter,
                    Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
                Projectile component = projObj.GetComponent<Projectile>();
                BounceProjModifier bounce = component.GetComponent<BounceProjModifier>();
                bounce.chanceToDieOnBounce = 0;
                bounce.numberOfBounces = 6;
                if (component != null)
                {
                    component.Owner = player;
                    component.Shooter = player.specRigidbody;
                    component.baseData.speed = 10f;
                    component.baseData.range *= 1f;
                    component.baseData.damage = 12f;

                }
                isActive = true;
            }
        }
    }
}