using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    class HammerBro : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Hammer Bro. Hammer", "hammer_bro");
            var behav = gun.gameObject.AddComponent<HammerBro>();
            behav.overrideNormalFireAudio = "Play_OBJ_cigarette_throw_01";
            behav.preventNormalFireAudio = true;
            behav.preventNormalReloadAudio = true;
            Game.Items.Rename("outdated_gun_mods:hammer_bro._hammer", "turbo:hammer_bro._hammer");
            gun.SetShortDescription("Y E E T");
            gun.SetLongDescription("The seemingly infinitely replicating hammer of an odd hammer-throwing turtle. Grants the user the same abilities.");
            gun.SetupSprite(null, "hammer_bro_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 16);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            //gun.AddProjectileModuleFrom(PickupObjectDatabase.GetByEncounterName("H4mmer") as Gun, true, false);
            //gun.DefaultModule.ammoType = (PickupObjectDatabase.GetById(91) as Gun).DefaultModule.finalAmmoType;
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.33f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.SetBaseMaxAmmo(350);
            gun.quality = PickupObject.ItemQuality.C;
            gun.encounterTrackable.EncounterGuid = "jkzsrv gilsdtrbvhisgdrnhvkgesicngsdiulyrvglgibusd yi7ryv isr dyvdsliua";
            gun.AddPassiveStatModifier(PlayerStats.StatType.ThrownGunDamage, 10, StatModifier.ModifyMethod.MULTIPLICATIVE);
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(91) as Gun).DefaultModule.finalProjectile);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 5f;
            projectile.baseData.speed = 25f;
            projectile.baseData.force = 20f;
            projectile.baseData.range = 8.5f;
            projectile.pierceMinorBreakables = true;
            projectile.transform.parent = gun.barrelOffset;
            ETGMod.Databases.Items.Add(gun, null, "ANY");
        }
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
                AkSoundEngine.PostEvent("Play_WPN_brickgun_reload_01", base.gameObject);
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