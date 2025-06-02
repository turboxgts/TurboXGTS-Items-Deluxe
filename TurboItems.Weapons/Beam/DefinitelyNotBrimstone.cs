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
    class DefinitelyNotBrimstone : AdvancedGunBehaviour
    {
        public static void Add()
        { 
            Gun gun = ETGMod.Databases.Items.NewGun("Grimstone", "absolutely_not_brimstone");
            Game.Items.Rename("outdated_gun_mods:grimstone", "turbo:grimstone");
            var behav = gun.gameObject.AddComponent<DefinitelyNotBrimstone>();
            behav.overrideNormalFireAudio = "Play_ENM_shelleton_beam_01";
            behav.preventNormalFireAudio = true;
            gun.SetShortDescription("Definitely not brimstone");
            gun.SetLongDescription("The laser of a being only known as...  Mega Santa.");
            gun.SetupSprite(null, "absolutely_not_brimstone_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.isAudioLoop = true;
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(518) as Gun, true, false);
            gun.AddPassiveStatModifier(PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);
            gun.doesScreenShake = true;
            gun.DefaultModule.ammoCost = 20;
            gun.DefaultModule.angleVariance = 0;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.cooldownTime = 0.001f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BEAM;
            gun.barrelOffset.transform.localPosition = new Vector3(1.1875f, 0.6875f, 0f);
            gun.SetBaseMaxAmmo(1000);
            gun.ammo = 1000;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 1;

            List<string> BeamAnimPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/absolutely_not_brimstone_mid_001"
            };
            List<string> BeamEndPaths = new List<string>()
            {
                "TurboItems/Resources/BeamSprites/absolutely_not_brimstone_end_001"
            };

            //BULLET STATS
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(86) as Gun).DefaultModule.projectiles[0]);

            BasicBeamController beamComp = projectile.GenerateBeamPrefab(
                "TurboItems/Resources/BeamSprites/absolutely_not_brimstone_mid_001",
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

            gun.quality = PickupObject.ItemQuality.A;
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        public DefinitelyNotBrimstone()
        {

        }
    }
}