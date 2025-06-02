using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace TurboItems
{
    class GargoyleHandLeft : AdvancedGunBehaviour
    {
        public static int Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Gargoyle's Left Hand", "gargoyle_hand_left");
            Game.Items.Rename("outdated_gun_mods:gargoyles_left_hand", "turbo:gargoyles_left_hand");
            var behav = gun.gameObject.AddComponent<GargoyleHandLeft>();
            behav.overrideNormalFireAudio = "Play_WPN_blasphemy_shot_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("Rock-hard and ready to rumble");
            gun.SetLongDescription("The left hand of a gargoyle. Can inflict bleeding on enemies, due to its sharp, jagged edges.");
            gun.SetupSprite(null, "mirror_sword_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 8);
            gun.AddProjectileModuleFrom("38_special", true, false);

            //Gun stats
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET; //mag sprite
            gun.DefaultModule.ammoCost = 0;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.angleVariance = 0f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "never gonna give you up,";
            gun.gunClass = GunClass.NONE;
            gun.barrelOffset.transform.localPosition = new Vector3(0.25f, 0.375f, 0);
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.InfiniteAmmo = true;
            gun.IsHeroSword = true;
            gun.HeroSwordDoesntBlank = true;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.cooldownTime = 0.8f;
            gun.sprite.IsPerpendicular = true;

            gun.SuppressLaserSight = true;


            //Projectile setup
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.transform.parent = gun.barrelOffset;
            projectile.baseData.damage *= 5;

            //The slash effect for the sword
            VFXPool GargoyleHandLeftVFXLibrary = VFXLibrary.CreateMuzzleflash("gargoyle_hand_left_slash", new List<string> { "gargoyle_hand_left_slash_001", "gargoyle_hand_left_slash_002", "gargoyle_hand_left_slash_003", "gargoyle_hand_left_slash_004", }, 10, new List<IntVector2> { new IntVector2(38, 45), new IntVector2(38, 45), new IntVector2(38, 45), new IntVector2(38, 45), },
                new List<tk2dBaseSprite.Anchor> { tk2dBaseSprite.Anchor.MiddleCenter, tk2dBaseSprite.Anchor.MiddleCenter, tk2dBaseSprite.Anchor.MiddleCenter, tk2dBaseSprite.Anchor.MiddleCenter }, new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, }, false, false, false, false, 0, VFXAlignment.Fixed, true, new List<float> { 0, 0, 0, 0 }, new List<Color> { VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, });

            ProjectileSlashingBehaviour slashingBehaviour = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
            //Slash stats
            slashingBehaviour.SlashVFX = GargoyleHandLeftVFXLibrary;
            slashingBehaviour.SlashDimensions = 65;
            slashingBehaviour.SlashRange = 2.25f;
            slashingBehaviour.InteractMode = SlashDoer.ProjInteractMode.IGNORE;

            ETGMod.Databases.Items.Add(gun, null, "ANY");
            GargoyleLeftID = gun.PickupObjectId;
            return gun.PickupObjectId;
        }
        public static int GargoyleLeftID;
        public GargoyleHandLeft()
        {

        }
    }
}