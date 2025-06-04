using Alexandria.ItemAPI;
using Alexandria.SoundAPI;
using Gungeon;
using MonoMod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;


namespace TurboItems
{
    class Beamstone : GunBehaviour
    {
        public static void Add()
        { 
            Gun gun = ETGMod.Databases.Items.NewGun("Beamstone", "beamstone");
            Game.Items.Rename("outdated_gun_mods:beamstone", "turbo:beamstone");
            var behav = gun.gameObject.AddComponent<Beamstone>();

            gun.SetShortDescription("Definitely not brimstone");
            gun.SetLongDescription("The laser of a being only known as...  Mega Santa.");

            gun.SetupSprite(null, "beamstone_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 8);

            gun.gunSwitchGroup = "turbo:beamstone";

            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Shot_01", "play_ENM_shelleton_beam_01");
            gun.isAudioLoop = true;

            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(518) as Gun, true, false);

            gun.DefaultModule.ammoCost = 20;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BEAM;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;

            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.001f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.ammo = 1000;
            gun.SetBaseMaxAmmo(1000);

            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.angleVariance = 0;
            gun.barrelOffset.transform.localPosition = new Vector3(1.1875f, 0.6875f, 0f);
            gun.doesScreenShake = true;

            gun.AddPassiveStatModifier(PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);

            gun.quality = PickupObject.ItemQuality.A;

            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 1;

            List<string> BeamAnimPaths = new List<string>()
            {
                "TurboItems/Resources/Beams/beamstone_mid_001"
            };
            List<string> BeamEndPaths = new List<string>()
            {
                "TurboItems/Resources/Beams/beamstone_end_001"
            };

            //BULLET STATS
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(86) as Gun).DefaultModule.projectiles[0]);

            BasicBeamController beamComp = projectile.GenerateBeamPrefab(
                "TurboItems/Resources/Beams/beamstone_mid_001",
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
            projectile.baseData.damage = 25f;
            projectile.baseData.force *= 2f;
            projectile.baseData.range = 666f;
            projectile.baseData.speed *= 10f;

            beamComp.usesChargeDelay = true;
            beamComp.penetration = 1000;
            beamComp.boneType = BasicBeamController.BeamBoneType.Projectile;
            beamComp.interpolateStretchedBones = false;
            beamComp.chargeDelay = 2.3f;
            gun.DefaultModule.projectiles[0] = projectile;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        public Beamstone()
        {

        }
    }
}