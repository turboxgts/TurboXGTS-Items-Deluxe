using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    public class Yang : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Yang", "wip2");
            Game.Items.Rename("outdated_gun_mods:yang", "turbo:yang");
            gun.gameObject.AddComponent<Yang>();
            gun.SetShortDescription("Yang");
            gun.SetLongDescription("One half of a special pair of prototype guns.");
            gun.SetupSprite(null, "wip2_idle_001", 24);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.1f;
            gun.DefaultModule.cooldownTime = 0.1f;
            gun.DefaultModule.numberOfShotsInClip = 6;
            gun.InfiniteAmmo = true;
            gun.ItemSpansBaseQualityTiers = true;
            gun.encounterTrackable.EncounterGuid = "sumfink^2";
            Gun yes = (PickupObjectDatabase.GetById(156) as Gun);
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(yes.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 5.5f;
            projectile.baseData.speed = 22.5f;
            projectile.transform.parent = gun.barrelOffset;
            ETGMod.Databases.Items.Add(gun, null, "ANY");

            YangID = gun.PickupObjectId;
        }
        public static int YangID;
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
            //player.ForceDropGun(gun);
            base.OnReloadPressed(player, gun, bSOMETHING);
        }
    }
}