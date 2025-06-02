using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Gungeon;
using MonoMod;
using ItemAPI;
using UnityEngine;
using System.Reflection;

namespace TurboItems
{
    class SelfHarmBeamWeaponBecauseNevernamedToldMeItWasOkayTo : AdvancedGunBehaviour
    {
        public static void Add()
        {

            Gun gun = ETGMod.Databases.Items.NewGun("Laser Disk", "laser_disk");
            Game.Items.Rename("outdated_gun_mods:laser_disk", "turbo:laser_disk");
            var behav = gun.gameObject.AddComponent<SelfHarmBeamWeaponBecauseNevernamedToldMeItWasOkayTo>();
            behav.overrideNormalFireAudio = "Play_ENM_shelleton_beam_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("Don't look at it");
            gun.SetLongDescription("Fires a bouncy laser with absolutely NO DRAWBACKS WHATSOEVER!!!");

            gun.SetupSprite(null, "laser_disk_idle_001", 8);
            gun.gunHandedness = GunHandedness.HiddenOneHanded;
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.isAudioLoop = true;
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(86) as Gun, true, false);

            //GUN STATS
            gun.doesScreenShake = false;
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.cooldownTime = 0.001f;
            gun.SetBaseMaxAmmo(3000);
            gun.DefaultModule.numberOfShotsInClip = 3000;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BEAM;
            gun.barrelOffset.transform.localPosition = new Vector3(1.4375f, 0.7f, 0f);


            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 1;
            
            List<string> BeamAnimPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/laser_disk_mid_001",
            };
            List<string> BeamEndPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/laser_disk_end_001",
            };

            //BULLET STATS
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(86) as Gun).DefaultModule.projectiles[0]);

            BasicBeamController beamComp = projectile.GenerateBeamPrefab(
                "TurboItems/Resources/BeamSprites/laser_disk_mid_001",
                new Vector2(5, 3),
                new Vector2(0, 1),
                BeamAnimPaths,
                9,
                //Impact
                null,
                -1,
                null,
                null,
                //End
                BeamEndPaths,
                9,
                new Vector2(5, 3),
                new Vector2(0, 1),
                //Beginning
                null,
                -1,
                null,
                null
                );

            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            projectile.baseData.damage = 1f;
            projectile.baseData.force *= 1f;
            projectile.baseData.range = 500f;
            projectile.baseData.speed *= 3f;
            SelfHarmBulletBehaviour SuicidalTendancies = projectile.gameObject.AddComponent<SelfHarmBulletBehaviour>();
            beamComp.penetration = 0;
            beamComp.reflections = 2;
            beamComp.boneType = BasicBeamController.BeamBoneType.Projectile;
            beamComp.interpolateStretchedBones = false;

            gun.DefaultModule.projectiles[0] = projectile;

            gun.quality = PickupObject.ItemQuality.D;
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        private bool HasReloaded;
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("stop_wpn_all", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);

            }
        }
        public SelfHarmBeamWeaponBecauseNevernamedToldMeItWasOkayTo()
        {

        }
    }

    public class SelfHarmBulletBehaviour : MonoBehaviour
    {
        public SelfHarmBulletBehaviour()
        {
        }

        private void Awake()
        {
            try
            {
                //ETGModConsole.Log("Awake is being called");
                this.m_projectile = base.GetComponent<Projectile>();
                canDealDamage = false;
                Invoke("HandleCooldown", 1f);
                PlayerController playerController = this.m_projectile.Owner as PlayerController;
                //if (playerController.PlayerHasActiveSynergy("Even Worse Choices")) this.m_projectile.baseData.damage *= 2f;
                this.m_projectile.allowSelfShooting = true;
                this.m_projectile.collidesWithEnemies = true;
                this.m_projectile.collidesWithPlayer = true;
                this.m_projectile.SetNewShooter(this.m_projectile.Shooter);
                this.m_projectile.UpdateCollisionMask();
                this.m_projectile.specRigidbody.OnPreRigidbodyCollision += this.OnHitSelf;
                //ETGModConsole.Log("Awake finished");

            }
            catch (Exception e)
            {
                ETGModConsole.Log(e.Message);
                ETGModConsole.Log(e.StackTrace);
            }
        }
        private Projectile m_projectile;
        private bool canDealDamage = true;
        private void OnHitSelf(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider)
        {
            try
            {
                //ETGModConsole.Log("OnHitSelf Triggered");
                float selfHarmDamageAmount = 0.5f;
                FieldInfo field = typeof(Projectile).GetField("m_hasPierced", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(myRigidbody.projectile, false);
                PlayerController component = otherRigidbody.GetComponent<PlayerController>();
                if (component && !component.IsGhost)
                {
                    if (this.m_projectile.PossibleSourceGun)
                    {
                        if (canDealDamage && !component.IsDodgeRolling)
                        {
                            component.healthHaver.ApplyDamage(selfHarmDamageAmount, Vector2.zero, "Looking at the laser", CoreDamageTypes.None, DamageCategory.Normal, true, null, false);
                            canDealDamage = false;
                            Invoke("HandleCooldown", 1f);
                        }
                        PhysicsEngine.SkipCollision = true;
                    }
                }

            }
            catch (Exception e)
            {
                ETGModConsole.Log(e.Message);
                ETGModConsole.Log(e.StackTrace);
            }

        }
        private void HandleCooldown()
        {
            canDealDamage = true;
        }
        private void Update()
        {

        }
    }
}