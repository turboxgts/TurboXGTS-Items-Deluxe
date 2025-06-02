using ItemAPI;

namespace TurboItems
{
    class GunList : AdvancedGunBehaviour
    {
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("This Gun is a Test", "you_should_not_have_this_like_it_is_literally_impossible_to_get_this");
            //any number with an f means it can be a float, otherwise, whole numbers only
            //similar things are grouped together, otherwise this is alphabetical

            //stuff I found interesting

            gun.UsesRechargeLikeActiveItem = false;
            gun.ActiveItemStyleRechargeAmount = 0f;
            gun.additionalMagnificenceModifier = 0f;
            gun.AppliesHoming = false;
            gun.AppliedHomingDetectRadius = 0f;
            gun.AppliedHomingAngularVelocity = 0f;
            gun.ArmorToGainOnPickup = 0;
            gun.blankDuringReload = false;
            gun.blankDamageScalingOnEmptyClip = 0f;
            gun.blankDamageToEnemies = 0f;
            gun.blankKnockbackPower = 0f;
            gun.blankReloadRadius = 0f;
            gun.HeroSwordDoesntBlank = false; //this stops the destruction of projectiles if is.HeroSword = true
            gun.CanAttackThroughObjects = false;
            gun.CanBeDropped = true;
            gun.CanBeSold = true;
            gun.CanCriticalFire = false; //crits are the special shot from Vorpal Gun and Vorpal Bullets, I believe. if not, someone from the modding server please correct me!!!
            gun.CriticalChance = 0f;
            gun.CriticalDamageMultiplier = 0f;
            gun.ForceNextShotCritical = false;
            gun.CanGainAmmo = true;
            gun.CanReloadNoMatterAmmo = false;
            gun.CanSneakAttack = false;
            gun.SneakAttackDamageMultiplier = 0f;
            gun.ClearsCooldownsLikeAWP = false;
            gun.clipsToLaunchOnReload = 0;
            gun.RequiresFundsToShoot = false; //makes it fire based on coin amount like Microtransaction Gun
            gun.CurrencyCostPerShot = 0;
            gun.GainsRateOfFireAsContinueAttack = false;
            gun.RateOfFireMultiplierAdditionPerSecond = 0f;
            gun.GoopReloadsFree = false; //makes goop (i.e. poison, oil, water, Blobulonian... jelly) restore ammo when reloaded on top of
            gun.IgnoredByRat = false; //makes the R.R. unable to steal it
            gun.IsLuteCompanionBuff = false; //makes the gun able to buff companions like the Really Special Lute
            gun.IsTrickGun = false; //makes the gun able to switch forms on reloading, I think. if not, someone from the modding server please correct me!!!
            gun.IsUndertaleGun = false; //lets the gun do... whatever the Gundertale was intended to do. ask Nevernamed for clarification
            gun.ItemRespectsHeartMagnificence = false; //...lets the gun ignore magnificence if it's dropped by an A tier chest? I think??? 
            gun.ItemSpansBaseQualityTiers = false; //probably lets it be D, C, and B tier all at the same time.
            gun.PreventOutlines = false;
            gun.reflectDuringReload = false; //makes the gun refelct bullets on reload like the Fightsaber
            gun.RespawnsIfPitfall = false; //appears back on the ground if it falls into a pit
            gun.SuppressLaserSight = false; //hides the laser sight line when held like Casey does

            //everything not included in list above
            //most stuffs set as default are related to things like ActiveReloadData or VFXPool, so I'm not bothering to set those
            //I, personally, also have no idea what many of these do, so this is best left to the more experienced modders
            //some of these also have more things after them, like gun.barrelOffset does, but uh... yeah I don't know how to deal with them

            gun.activeReloadData = default;
            gun.activeReloadFailedEffects = default;
            gun.activeReloadSuccessEffects = default;
            gun.AdditionalClipCapacity = 0;
            gun.additionalHandState = default;
            gun.AdditionalShootSoundsByModule = default;
            gun.alternateIdleAnimation = default;
            gun.alternateReloadAnimation = default;
            gun.alternateShootAnimation = default;
            gun.alternateSwitchGroup = default;
            gun.alternateVolley = default;
            gun.ammo = 0;
            gun.associatedItemChanceMods = default;
            gun.barrelOffset = default;
            gun.baseLightIntensity = 0f;
            gun.carryPixelDownOffset = default;
            gun.carryPixelOffset = default;
            gun.carryPixelUpOffset = default;
            gun.chargeAnimation = default;
            gun.chargeOffset = default;
            gun.ClearIgnoredByRatFlagOnPickup = false;
            gun.clipObject = default;
            gun.contentSource = 0f; //what
            gun.criticalFireAnimation = default;
            gun.CriticalMuzzleFlashEffects = default;
            gun.CriticalReplacementProjectile = default;
            gun.currentGunDamageTypeModifiers = default;
            gun.currentGunStatModifiers = default;
            gun.CustomBossDamageModifier = 0f;
            gun.CustomCost = 0;
            gun.CustomLaserSightDistance = 0f;
            gun.CustomLaserSightHeight = 0f;
            gun.damageModifier = 0;
            gun.DidTransformGunThisFrame = false;
            gun.directionlessScreenShake = false;
            gun.DisablesRendererOnCooldown = false;
            gun.dischargeAnimation = default;
            gun.dodgeAnimation = default;
            gun.doesScreenShake = false;
            gun.DuctTapeMergedGunIDs = default;
            gun.emptyAnimation = default;
            gun.emptyReloadAnimation = default;
            gun.emptyReloadEffects = default;
            gun.enemyPreFireAnimation = default;
            gun.finalMuzzleFlashEffects = default;
            gun.finalShootAnimation = default;
            gun.ForcedPositionInAmmonomicon = 0;
            gun.forceFlat = false;
            gun.gunClass = 0f; //I refuse to touch this
            gun.gunHandedness = default;
            gun.gunName = default;
            gun.gunPosition = default;
            gun.gunScreenShake = default;
            gun.gunSwitchGroup = default;
            gun.HasBeenPickedUp = false;
            gun.HasBeenStatProcessed = false;
            gun.HasEverBeenAcquiredByPlayer = false;
            gun.HasFiredHolsterShot = false;
            gun.HasFiredReloadSynergy = false;
            gun.HasProcessedStatMods = false;
            gun.idleAnimation = default;
            gun.IgnoresAngleQuantization = false;
            gun.introAnimation = default; //pretty sure this is the animation that plays when you switch to the gun; Mimic Gun is a surprisingly good example for that
            gun.isAudioLoop = false; //used mainly for beam weapons and full auto iirc
            gun.IsBeingSold = false;
            gun.IsHeroSword = default;
            gun.LastLaserSightEnemy = default;
            gun.LastShotIndex = 0;
            gun.leftFacingPixelOffset = default;
            gun.light = default;
            gun.LocalActiveReload = false;
            gun.LocalInfiniteAmmo = false;
            gun.LockedHorizontalCenterFireOffset = 0f;
            gun.LockedHorizontalOnCharge = false;
            gun.LockedHorizontalOnReload = false;
            gun.lowersAudioWhileFiring = false;
            gun.modifiedFinalVolley = default;
            gun.modifiedOptionalReloadVolley = default;
            gun.modifiedVolley = default;
            gun.ModifyActiveCooldownDamage = default;
            gun.MovesPlayerForwardOnChargeFire = false;
            gun.muzzleFlashEffects = default;
            gun.muzzleOffset = default;
            gun.ObjectToInstantiateOnReload = default;
            //this section is On stuff, obviously
            gun.OnAmmoChanged = null;
            gun.OnAutoReload = null;
            gun.OnBurstContinued = null;
            gun.OnDropped = null;
            gun.OnFinishAttack = null;
            gun.OnInitializedWithOwner = null;
            gun.OnPostFired = null;
            gun.OnPreFireProjectileModifier = null;
            gun.OnReflectedBulletDamageModifier = null;
            gun.OnReflectedBulletScaleModifier = null;
            gun.OnReloadPressed = null;
            //end of hooks
            gun.outOfAmmoAnimation = default;
            gun.OnlyUsesIdleInWeaponBox = false;
            gun.OverrideAngleSnap = 0f;
            gun.OverrideFinaleAudio = false;
            gun.OverrideNormalFireAudioEvent = default;
            gun.overrideOutOfAmmoHandedness = default;
            gun.passiveStatModifiers = default;
            gun.PerCharacterPixelOffsets = default;
            gun.PersistsOnDeath = false;
            gun.PersistsOnPurchase = false;
            gun.PickupObjectId = 0;
            gun.PostProcessProjectile = default;
            gun.PostProcessVolley = default;
            gun.prefabName = default;
            gun.PreventNormalFireAudio = false;
            gun.PreventOutlines = false;
            gun.preventRotation = false;
            gun.PreventSaveSerialization = false;
            gun.PreventStartingOwnerFromDropping = false;
            gun.procGunData = default;
            gun.quality = default;
            gun.rampBullets = false;
            gun.rampStartHeight = 0f;
            gun.rampTime = 0f;
            gun.rawOptionalReloadVolley = default;
            gun.reloadAnimation = default;
            gun.reloadClipLaunchFrame = 0;
            gun.reloadEffects = default;
            gun.reloadOffset = default;
            gun.reloadShellLaunchFrame = 0;
            gun.reloadTime = 0f;
            gun.SaveFlagToSetOnAcquisition = default;
            gun.shellCasing = default;
            gun.shellCasingOnFireFrameDelay = 0;
            gun.shellsToLaunchOnFire = 0;
            gun.shellsToLaunchOnReload = 0;
            gun.shootAnimation = default;
            gun.ShouldBeExcludedFromShops = false;
            gun.singleModule = default;
            gun.StarterGunForAchievement = false;
            gun.thrownObject = default;
            gun.TrickGunAlternatesHandedness = false;
            gun.usesContinuousFireAnimation = false;
            gun.usesContinuousMuzzleFlash = false;
            gun.UsesCustomCost = false;
            gun.usesDirectionalAnimator = false;
            gun.usesDirectionalIdleAnimations = false;
            gun.UsesPerCharacterCarryPixelOffsets = false;
            gun.weaponPanelSpriteOverride = default;
        }
    }
}