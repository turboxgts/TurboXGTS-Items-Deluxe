using Alexandria.ItemAPI;
using Alexandria.SoundAPI;
using Gungeon;
using ItemAPI;
using MonoMod;
using System;
using System.Collections;
using UnityEngine;

//TODO: list below
//FIX - hand position is too high (and possibly to the right) - done
//FIX - ammonomicon sprite - done

//make idle sprite not huge (crop to just fist) - done
//set gunhandedness to hidden - done

//maybe make hand position more visible?
//maybe make spread just a tad wider?
//probably make mag hidden
//maybe make custom bullet casing? oraoraoraora
//set where bullet comes out of
//disable muzzleflash or make custom flash

//completely modify projectile - see next lines
//base off boxing glove if possible
//short range, low-med dmg, very fast speed, invisible
//generally just tone down the dmg output - or make it bypass dps cap to be funny

//disable bullet impact noise? or make custom one/use diff noise
//same with impact puff

//either make custom sound, use diff sound, or make cigarette throw not deafen during spawn events or etc
//setup anim looping - done, but see next lines
//idle sprite gets displaced after anim loop is stopped

//on the topic of sprites, try giving active punch a flame effect and making the silhouettes bluer, and maybe a bit of a whoosh line?

namespace TurboItems
{

    public class DisembodiedHand : GunBehaviour
    {


        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Disembodied Hand", "disembodied_hand");
            Game.Items.Rename("outdated_gun_mods:disembodied_hand", "turbo:disembodied_hand");
            gun.gameObject.AddComponent<DisembodiedHand>();

            gun.SetShortDescription("Become the Stand");
            gun.SetLongDescription("The strange purple fist of a time-stopping spirit ghost with a penchant for punching.\n\n" +
                "Time stop not included.");

            gun.SetupSprite(null, "disembodied_hand_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 20);
            gun.gunHandedness = GunHandedness.HiddenOneHanded;
            gun.usesContinuousFireAnimation = true;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).loopStart = 3;
            gun.TrimGunSprites();

            gun.gunSwitchGroup = "turbo:disembodied_hand";

            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Shot_01", "Play_OBJ_cigarette_throw_01");
            SoundManager.AddCustomSwitchData("WPN_Guns", gun.gunSwitchGroup, "Play_WPN_Gun_Reload_01");

            gun.AddProjectileModuleFrom("ak-47", true, false);

            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;
            gun.DefaultModule.cooldownTime = 0.05f;
            gun.DefaultModule.numberOfShotsInClip = -1;
            //gun.SetBaseMaxAmmo(250);
            gun.InfiniteAmmo = true;

            gun.quality = PickupObject.ItemQuality.S;

            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;

            //projectile.baseData.damage = 5f;
            //projectile.baseData.speed = 1.7f;
            projectile.transform.parent = gun.barrelOffset;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        private void Start()
        {
            gun.spriteAnimator.AnimationCompleted += this.AnimationCompleted;
        }

        private void AnimationCompleted(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip)
        {
            if (clip == null)
                gun.PlayIdleAnimation();
        }
    }
}
