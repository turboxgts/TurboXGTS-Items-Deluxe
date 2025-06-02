using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;
using ItemAPI;
using UnityEngine;
using System.Reflection;


namespace TurboItems
{
    class MirrorSwordLaser : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Mirror Sword Laser", "mirror_sword_beam");
            Game.Items.Rename("outdated_gun_mods:mirror_sword_laser", "turbo:mirror_sword_laser");
            var behav = gun.gameObject.AddComponent<MirrorSwordLaser>();
            behav.overrideNormalFireAudio = "Play_WPN_stdissuelaser_shot_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("SHWING");
            gun.SetLongDescription("A dull sword, presumably for use in an old ceremony before the Great Bullet struck the Gungeon. Can deflect bullets. Slightly angers the Jammed.");
            gun.SetupSprite(null, "mirror_sword_beam_idle_001", 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 24);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.isAudioLoop = true;
            gun.AddProjectileModuleFrom("ak-47", true, false);

            //GUN STATS
            gun.CanBeDropped = false;
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.quality = PickupObject.ItemQuality.EXCLUDED;
            gun.encounterTrackable.EncounterGuid = "7weribgf3408yfh9wpcer2-d3qt6r8pygxeq859pv ygbocex8ius";
            gun.reloadTime = 0f;
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.cooldownTime = 0.1f;
            //gun.InfiniteAmmo = true
            gun.SetBaseMaxAmmo(25);
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SMALL_BLASTER;
            gun.barrelOffset.transform.localPosition = new Vector3(1f, 0.3125f, 0f);

            //projectile shite
            Gun yes = (PickupObjectDatabase.GetById(32) as Gun);
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(yes.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 5f;
            projectile.baseData.speed = 25f;
            projectile.pierceMinorBreakables = true;
            projectile.transform.parent = gun.barrelOffset;
            ETGMod.Databases.Items.Add(gun, null, "ANY");
        }
        public MirrorSwordLaser()
        {

        }
    }
}