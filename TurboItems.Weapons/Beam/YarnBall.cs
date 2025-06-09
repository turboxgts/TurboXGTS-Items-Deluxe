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
    class YarnBall : GunBehaviour
    {
        public static void Add()
        {

            Gun gun = ETGMod.Databases.Items.NewGun("Ball of Yarn", "yarn_ball");
            Game.Items.Rename("outdated_gun_mods:ball_of_yarn", "turbo:yarn_ball");
            var behav = gun.gameObject.AddComponent<YarnBall>();

            gun.SetShortDescription("Crochet Rocket");
            gun.SetLongDescription("Very fuzzy");

            gun.SetupSprite(null, "yarn_ball_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.gunHandedness = GunHandedness.HiddenOneHanded;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 1;

            gun.gunSwitchGroup = "turbo:yarn_ball";

            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Shot_01", "play_ENM_shelleton_beam_01");
            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Reload_01");
            gun.isAudioLoop = true;

            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(86) as Gun, true, false);

            //GUN STATS
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BEAM;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;

            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.001f;
            gun.DefaultModule.numberOfShotsInClip = 1000;
            gun.SetBaseMaxAmmo(1000);

            gun.muzzleFlashEffects.type = VFXPoolType.None;
            gun.DefaultModule.angleVariance = 0;
            gun.barrelOffset.transform.localPosition = new Vector3(0.875f, 0.5f, 0f);
            gun.doesScreenShake = false;

            gun.quality = PickupObject.ItemQuality.C;


            List<string> BeamAnimPaths = new List<string>()
            {
                "TurboItems/Resources/Beams/yarn_ball_mid_001",
            };
            List<string> BeamEndPaths = new List<string>()
            {
                "TurboItems/Resources/Beams/yarn_ball_end_001",
            };

            //BULLET STATS
            //Old beam method maybe?

            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(86) as Gun).DefaultModule.projectiles[0]);

            //New beam method maybe?

            //Projectile projectile = ProjectileUtility.SetupProjectile(86);

            BasicBeamController beamComp = projectile.GenerateBeamPrefab(
                "TurboItems/Resources/Beams/yarn_ball_mid_001",
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
            projectile.baseData.damage = 12f;
            projectile.baseData.speed *= 3f;
            projectile.baseData.force *= 0.5f;
            projectile.baseData.range = 15f;

            beamComp.penetration = 0;
            beamComp.boneType = BasicBeamController.BeamBoneType.Projectile;
            beamComp.interpolateStretchedBones = true;

            gun.DefaultModule.projectiles[0] = projectile;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }

        public YarnBall()
        {

        }
    }
}