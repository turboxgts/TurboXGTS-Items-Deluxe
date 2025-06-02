using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurboItems
{
    class PhrenicBow : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Phrenic Bow", "phrenic_bow");
            var behav = gun.gameObject.AddComponent<PhrenicBow>();
            behav.overrideNormalFireAudio = "Play_WPN_woodbow_shot_01";
            behav.preventNormalFireAudio = true;
            behav.preventNormalReloadAudio = true;
            Game.Items.Rename("outdated_gun_mods:phrenic_bow", "turbo:phrenic_bow");
            gun.SetShortDescription("Bullet time activated");
            gun.SetLongDescription("An old bow belonging to an ancient race of technologically advanced people. Allows the user to briefly bring time to a near standstill for maximum accuracy when fully charged.");
            gun.SetupSprite(null, "phrenic_bow_idle_001", 16);
            gun.carryPixelOffset = new IntVector2(16, 0);
            tk2dSpriteAnimationClip fireClip = gun.sprite.spriteAnimator.GetClipByName("phrenic_bow_fire");

            float[] offsetsX = new float[] { -0.0625f, -0.0625f, -0.0625f, -0.0625f, };
            float[] offsetsY = new float[] { 0, 0, 0, 0, };

            for (int i = 0; i < offsetsX.Length && i < offsetsY.Length && i < fireClip.frames.Length; i++)
            {
                int id = fireClip.frames[i].spriteId;
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position0.x += offsetsX[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position0.y += offsetsY[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position1.x += offsetsX[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position1.y += offsetsY[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position2.x += offsetsX[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position2.y += offsetsY[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position3.x += offsetsX[i];
                fireClip.frames[i].spriteCollection.spriteDefinitions[id].position3.y += offsetsY[i];
            }
            tk2dSpriteAnimationClip fireClip2 = gun.sprite.spriteAnimator.GetClipByName("phrenic_bow_charge");
            float[] offsetsX2 = new float[] { -0.1875f, -0.1875f, -0.1875f, -0.1875f, -0.1875f, -0.1875f, -0.1875f, -0.1875f, };
            float[] offsetsY2 = new float[] { 0, 0, 0, 0, 0, 0, 0, 0,};

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
            gun.SetAnimationFPS(gun.chargeAnimation, 16);
            gun.SetAnimationFPS(gun.shootAnimation, 16);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.33f;
            gun.DefaultModule.numberOfShotsInClip = 1;
            gun.SetBaseMaxAmmo(100);
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.ARROW;
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "legend of lonk: boof of the woof";
            gun.barrelOffset.transform.localPosition = new Vector3(1f, 1.1875f, 0);

            //bullet shite
            Gun yes = (PickupObjectDatabase.GetById(8) as Gun);
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(yes.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 17f;
            projectile.baseData.speed = 35f;
            projectile.baseData.force = 20f;
            projectile.baseData.range = 1000f;
            PierceProjModifier pierce = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
            pierce.penetration += 1;

            projectile.pierceMinorBreakables = true;
            projectile.transform.parent = gun.barrelOffset;
            ProjectileModule.ChargeProjectile chargeProj = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile,
                ChargeTime = 0.95f,
            };
            gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile> { chargeProj };
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).loopStart = 5;
            ETGMod.Databases.Items.Add(gun, null, "ANY");
            PhrenicBowID = gun.PickupObjectId;
        }
        public static int PhrenicBowID;
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
            base.OnReloadPressed(player, gun, bSOMETHING);
        }

        public override void PostProcessProjectile(Projectile projectile)
        {
            PlayerController player = projectile.Owner as PlayerController;
            base.PostProcessProjectile(projectile);
            try
            {
                projectile.specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(projectile.specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.HandlePreCollision));
            }
            catch (Exception ex)
            {
                ETGModConsole.Log(ex.Message, false);
            }
        }
        private void HandlePreCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider)
        {
            if (otherRigidbody.gameObject.name != null)
            {
                if (otherRigidbody.gameObject.name == "Table_Vertical" || otherRigidbody.gameObject.name == "Table_Horizontal")
                {
                    PhysicsEngine.SkipCollision = true;
                }
            }
        }
    }
}