using UnityEngine;
using ItemAPI;
using System.Collections.Generic;

namespace TurboItems
{
	public class BloodCoveredCloak : PassiveItem
	{
		public static void Register()
		{
			string itemName = "Blood Covered Cloak";
			string resourceName = "TurboItems/Resources/ItemBundleSprites/blood_cloak";
			GameObject obj = new GameObject(itemName);
			var item = obj.AddComponent<BloodCoveredCloak>();
			ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
			string shortDesc = "Bat's curse";
			string longDesc = "A cloak, stained with blood. Grants the curse of the vampire upon whoever wears it.";
			ItemBuilder.SetupItem(item, shortDesc, longDesc, "turbo");
			ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 2, StatModifier.ModifyMethod.ADDITIVE);
			ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, 0.75f, StatModifier.ModifyMethod.ADDITIVE);
			item.quality = PickupObject.ItemQuality.EXCLUDED;
			item.CanBeDropped = false;

			List<string> mandatoryConsoleIDs = new List<string>
			{
				"turbo:wooden_stake",
				"turbo:blood_covered_cloak"
			};

			var synergy = CustomSynergies.Add("Might Slayer", mandatoryConsoleIDs);
			synergy.statModifiers = new List<StatModifier>
				{
					new StatModifier
					{
						modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
						statToBoost = PlayerStats.StatType.DamageToBosses,
						amount = 2.5f,
					},
				};
			synergy.ActiveWhenGunUnequipped = false;
		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(436).gameObject, base.Owner);
			LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(595).gameObject, base.Owner);
			LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(285).gameObject, base.Owner);
			player.GiveItem("turbo:wooden_stake");
		}

	}
}