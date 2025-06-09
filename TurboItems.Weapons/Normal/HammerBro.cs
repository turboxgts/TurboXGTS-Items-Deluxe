using Alexandria.ItemAPI;
using Alexandria.SoundAPI;
using Gungeon;
using MonoMod;
using System;
using System.Collections;
using UnityEngine;

namespace TurboItems
{
    class HammerBro : GunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Hammer Bro. Hammer", "hammer_bro");
            Game.Items.Rename("outdated_gun_mods:hammer_bro._hammer", "turbo:hammer_bro");
            var behav = gun.gameObject.AddComponent<HammerBro>();

            gun.SetShortDescription("Y E E T");
            gun.SetLongDescription("The seemingly infinitely replicating hammer of an odd hammer-throwing turtle. Grants the user the same abilities.");

            gun.SetupSprite(null, "hammer_bro_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 16);

            gun.gunSwitchGroup = "turbo:hammer_bro";

            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Shot_01", "Play_OBJ_cigarette_throw_01");
            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Reload_01");

            gun.AddProjectileModuleFrom("ak-47", true, false);

            gun.DefaultModule.ammoCost = 1;
            //TODO: set ammo type to be the h4mmer's hammer
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.33f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.SetBaseMaxAmmo(350);

            gun.AddPassiveStatModifier(PlayerStats.StatType.ThrownGunDamage, 10, StatModifier.ModifyMethod.MULTIPLICATIVE);
            gun.quality = PickupObject.ItemQuality.C;

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