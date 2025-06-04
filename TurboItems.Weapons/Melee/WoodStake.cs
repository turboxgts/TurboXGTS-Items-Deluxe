using System.Collections;
using System.Collections.Generic;
using Gungeon;
using Alexandria.ItemAPI;
using UnityEngine;

namespace TurboItems
{
    class WoodStake : GunBehaviour
    {
        public static int Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Wooden Stake", "wooden_stake");
            Game.Items.Rename("outdated_gun_mods:wooden_stake", "turbo:wooden_stake");
            gun.gameObject.AddComponent<WoodStake>();

            gun.SetShortDescription("Stabby Stab");
            gun.SetLongDescription("Taken from the chest of a vampire who had an unsuccessful attempt on their life. Works in a pinch against tough enemies.");

            gun.SetupSprite(null, "wooden_stake_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 12);
            gun.SetAnimationFPS(gun.reloadAnimation, 2);

            gun.AddProjectileModuleFrom("38_special", true, false);

            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;

            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.5f;
            //TODO: test -1 clip size
            gun.DefaultModule.numberOfShotsInClip = 1000;
            gun.InfiniteAmmo = true;

            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.angleVariance = 0f;
            gun.barrelOffset.transform.localPosition = new Vector3(0.75f, 0f, 0f);
            gun.sprite.IsPerpendicular = true;

            gun.gunClass = GunClass.NONE;
            gun.quality = PickupObject.ItemQuality.EXCLUDED;

            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.transform.parent = gun.barrelOffset;
            projectile.baseData.damage *= 1.66f;
            VFXPool WoodenStakeVFXLibrary = VFXLibrary.CreateMuzzleflash("wooden_stake_slash", new List<string> { "wooden_stake_slash_001", "wooden_stake_slash_002", "wooden_stake_slash_003", "wooden_stake_slash_004", }, 10, new List<IntVector2> { new IntVector2(27, 27), new IntVector2(27, 27), new IntVector2(27, 27), new IntVector2(27, 27), }, new List<tk2dBaseSprite.Anchor> {
                tk2dBaseSprite.Anchor.LowerLeft, tk2dBaseSprite.Anchor.LowerLeft, tk2dBaseSprite.Anchor.LowerLeft, tk2dBaseSprite.Anchor.LowerLeft}, new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, }, false, false, false, false, 0, VFXAlignment.Fixed, true, new List<float> { 0, 0, 0, 0 }, new List<Color> { VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, });

            ProjectileSlashingBehaviour slashingBehaviour = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
            slashingBehaviour.SlashVFX = WoodenStakeVFXLibrary;
            slashingBehaviour.SlashDimensions = 65;
            slashingBehaviour.SlashRange = 2.5f;

            ETGMod.Databases.Items.Add(gun, null, "ANY");
            return gun.PickupObjectId;
        }

        public WoodStake()
        {

        }
    }
}