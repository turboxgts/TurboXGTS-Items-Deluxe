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
    class MirrorSwordMeleeOnly : AdvancedGunBehaviour
    {
        public static int Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Mirror Sword", "mirror_sword");
            Game.Items.Rename("outdated_gun_mods:mirror_sword", "turbo:mirror_sword");
            var behav = gun.gameObject.AddComponent<MirrorSword>();
            behav.overrideNormalFireAudio = "Play_WPN_blasphemy_shot_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("SHWING");
            gun.SetLongDescription("A dull sword, presumably for use in an old ceremony before the Great Bullet struck the Gungeon. Has gained new power through the Gret Bullet's energy seeping into it. Slightly angers the Jammed.");
            gun.SetupSprite(null, "mirror_sword_idle_001", 8);
            tk2dSpriteAnimationClip fireClip2 = gun.sprite.spriteAnimator.GetClipByName("mirror_sword_fire");

            float[] offsetsX2 = new float[] { -0.25f, -0.375f, -0.375f, -0.25f, -0.4375f };
            float[] offsetsY2 = new float[] { -1f, -1.25f, -1.25f, -1.1875f, -1.1875f };

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
            gun.AddPassiveStatModifier(PlayerStats.StatType.Curse, 1f, StatModifier.ModifyMethod.ADDITIVE);

            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SMALL_BULLET; //mag sprite
            gun.DefaultModule.ammoCost = 0;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.angleVariance = 0f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.quality = PickupObject.ItemQuality.C;
            gun.encounterTrackable.EncounterGuid = "this is an idea";
            gun.gunClass = GunClass.NONE;
            gun.barrelOffset.transform.localPosition = new Vector3(0.25f, 0.375f, 0);
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.reloadTime = 0;
            gun.SetBaseMaxAmmo(2);
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
            projectile.baseData.damage *= 0;

            //The slash effect for the sword
            VFXPool MirrorSwordVFXLibrary = VFXLibrary.CreateMuzzleflash("mirror_sword_slash", new List<string> { "mirror_sword_slash_001", "mirror_sword_slash_002", "mirror_sword_slash_003", "mirror_sword_slash_004", }, 10, new List<IntVector2> { new IntVector2(38, 45), new IntVector2(38, 45), new IntVector2(38, 45), new IntVector2(38, 45), },
                new List<tk2dBaseSprite.Anchor> { tk2dBaseSprite.Anchor.MiddleCenter, tk2dBaseSprite.Anchor.MiddleCenter, tk2dBaseSprite.Anchor.MiddleCenter, tk2dBaseSprite.Anchor.MiddleCenter }, new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, }, false, false, false, false, 0, VFXAlignment.Fixed, true, new List<float> { 0, 0, 0, 0 }, new List<Color> { VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, VFXLibrary.emptyColor, });

            ProjectileSlashingBehaviour slashingBehaviour = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
            //Slash stats
            slashingBehaviour.SlashVFX = MirrorSwordVFXLibrary;
            slashingBehaviour.SlashDimensions = 105;
            slashingBehaviour.SlashRange = 2.25f;
            slashingBehaviour.InteractMode = SlashDoer.ProjInteractMode.REFLECT;

            ETGMod.Databases.Items.Add(gun, null, "ANY");
            return gun.PickupObjectId;
        }
    }
}