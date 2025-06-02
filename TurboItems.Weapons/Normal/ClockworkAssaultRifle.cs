using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;

namespace TurboItems
{
    class ClockworkAssaultRifle : AdvancedGunBehaviour
    {


        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("Clockwork Assault Rifle", "clockwork");
            Game.Items.Rename("outdated_gun_mods:clockwork_assault_rifle", "turbo:clockwork_assault_rifle");
            gun.gameObject.AddComponent<ClockworkAssaultRifle>();
            gun.SetShortDescription("Rat-tat-tat");
            gun.SetLongDescription("A rifle modified to use gears and decorated with steampunk-era swirls. Only the first shot consumes ammo.");
            gun.SetupSprite(null, "clockwork_idle_001", 24);
            gun.SetAnimationFPS(gun.shootAnimation, 16);
            gun.AddProjectileModuleFrom("ak-47", true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Burst;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0.35f;
            gun.DefaultModule.burstCooldownTime = 0.08f;
            gun.DefaultModule.cooldownTime = 0.35f;
            gun.DefaultModule.numberOfShotsInClip = 30;
            gun.SetBaseMaxAmmo(750);
            gun.quality = PickupObject.ItemQuality.B;
            gun.encounterTrackable.EncounterGuid = "heeboobabababoba";
            gun.barrelOffset.transform.localPosition = new Vector3(2.125f, 0.1875f, 0f);
            gun.muzzleFlashEffects.type = VFXPoolType.None;
            Gun yes = (PickupObjectDatabase.GetById(15) as Gun);
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(yes.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 5f;
            projectile.baseData.speed = 22.5f;
            projectile.AdditionalScaleMultiplier = 0.5f;
            projectile.transform.parent = gun.barrelOffset;
            projectile.SetProjectileSpriteRight("musket_ball", 11, 11, null, null);
            ETGMod.Databases.Items.Add(gun, null, "ANY");

            ClockworkID = gun.PickupObjectId;
        }
        public static int ClockworkID;
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_beretta_shot_01", gameObject);
        }
        private bool HasReloaded;
        protected override void Update()
        {
            if (gun.CurrentOwner)
            {

                if (!gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
            }
        }
        //private GameObject projObj;

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            //projObj = null;
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
                AkSoundEngine.PostEvent("Play_WPN_crossbow_reload_01", base.gameObject);
            }
            //if (gun.ClipShotsRemaining == 0 && gun.CurrentAmmo != 0)
            //{
            //    Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(((Gun)ETGMod.Databases.Items[202]).DefaultModule.projectiles[0]);
            //    projectile.gameObject.SetActive(false);
            //    FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            //    UnityEngine.Object.DontDestroyOnLoad(projectile);
            //    projectile.SetProjectileSpriteRight("musket_ball", 11, 11, null, null);
            //    projObj = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter,
            //        Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
            //    Projectile component = projObj.GetComponent<Projectile>();
            //    if (component != null)
            //    {
            //        component.Owner = player;
            //        component.Shooter = player.specRigidbody;
            //        component.baseData.speed = 10f;
            //        component.baseData.range *= 1f;
            //        component.baseData.damage = 12f;
            //    }
            //}
        }
    }
}