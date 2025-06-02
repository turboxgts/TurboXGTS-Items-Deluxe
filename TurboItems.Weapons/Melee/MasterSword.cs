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
    class MasterSword : AdvancedGunBehaviour
    {
        public static int Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Master Sword", "master_sword");
            Game.Items.Rename("outdated_gun_mods:master_sword", "turbo:master_sword");
            var behav = gun.gameObject.AddComponent<MasterSword>();
            behav.overrideNormalFireAudio = "Play_WPN_blasphemy_shot_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("Comically large");
            gun.SetLongDescription("An exquisite sword, belonging to a lost adventurer. Angers the Jammed.");
            gun.SetupSprite(null, "master_sword_idle_001", 8);
            tk2dSpriteAnimationClip fireClip2 = gun.sprite.spriteAnimator.GetClipByName("master_sword_fire");

            float[] offsetsX2 = new float[] { -0.25f, -0.5625f, -0.5625f, -0.75f, -1.3125f };
            float[] offsetsY2 = new float[] { -3f, -2.5f, -2.5f, -2.6875f, -3.325f };

            for (int i = 0; i < offsetsX2.Length && i < offsetsY2.Length && i < fireClip2.frames.Length; i++)
            {
                int id = fireClip2.frames[i].spriteId;
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position0.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position0.y += offsetsY2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position1.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position1.y += offsetsY2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position2.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position2.y += offsetsY2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position3.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position3.y += offsetsY2[i];
            }
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 8);
            gun.AddProjectileModuleFrom("38_special", true, false);

            //Gun stats
            gun.AddPassiveStatModifier(PlayerStats.StatType.Curse, 3f, StatModifier.ModifyMethod.ADDITIVE);
            gun.DefaultModule.ammoCost = 0;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.CUSTOM;
            gun.DefaultModule.customAmmoType = "white";
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.angleVariance = 0f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.quality = PickupObject.ItemQuality.SPECIAL;
            gun.encounterTrackable.EncounterGuid = "ea sports it's in the game";
            gun.gunClass = GunClass.NONE;
            gun.barrelOffset.transform.localPosition = new Vector3(1.25f, 0f, 0);
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.reloadTime = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.SetBaseMaxAmmo(3);
            gun.DefaultModule.cooldownTime = 0.66f;
            gun.sprite.IsPerpendicular = true;
            gun.quality = PickupObject.ItemQuality.A;

            gun.SuppressLaserSight = true;


            //Projectile setup
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.transform.parent = gun.barrelOffset;
            projectile.baseData.damage *= 2;

            //The slash effect for the sword
            VFXPool MasterSwordVFXLibrary = VFXLibrary.CreateMuzzleflash("Master_sword_slash", new List<string> { "master_sword_slash_001", "master_sword_slash_002", "master_sword_slash_003", "master_sword_slash_004", }, 10, new List<IntVector2> { new IntVector2(64, 73), new IntVector2(64, 73), new IntVector2(64, 73), new IntVector2(64, 73), },
                new List<tk2dBaseSprite.Anchor> { tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft, tk2dBaseSprite.Anchor.MiddleLeft }, new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, }, false, false, false, false, 0, VFXAlignment.Fixed, true, new List<float> { 0, 0, 0, 0 }, new List<Color> { VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, });

            ProjectileSlashingBehaviour slashingBehaviour = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
            //Slash stats
            slashingBehaviour.SlashVFX = MasterSwordVFXLibrary;
            slashingBehaviour.SlashDimensions = 105;
            slashingBehaviour.SlashRange = 4f;
            slashingBehaviour.InteractMode = SlashDoer.ProjInteractMode.IGNORE;

            ETGMod.Databases.Items.Add(gun, null, "ANY");
            return gun.PickupObjectId;
        }
        private GameObject projObj;
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            base.OnPostFired(player, gun);
            if (player.healthHaver.GetMaxHealth() == player.healthHaver.GetCurrentHealth())
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[377]).DefaultModule.projectiles[0];
                projObj = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter,
                    Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
                Projectile component = projObj.GetComponent<Projectile>();
                if (component != null)
                {
                    component.Owner = player;
                    component.Shooter = player.specRigidbody;
                    component.baseData.speed = 33f;
                    component.baseData.range *= 1f;
                    component.baseData.damage = 12f;
                    component.AdditionalScaleMultiplier = 2f;
                }
            }
        }
        public MasterSword()
        {

        }
    }
}