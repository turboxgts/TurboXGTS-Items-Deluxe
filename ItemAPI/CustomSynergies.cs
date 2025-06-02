using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gungeon;
using MonoMod.RuntimeDetour;

namespace ItemAPI
{
	public static class CustomSynergies
	{
		public static AdvancedSynergyEntry Add(string name, List<string> mandatoryConsoleIDs, List<string> optionalConsoleIDs = null, bool ignoreLichEyeBullets = true)
		{
			bool flag = mandatoryConsoleIDs == null || mandatoryConsoleIDs.Count == 0;
			AdvancedSynergyEntry result;
			if (flag)
			{
				Tools.PrintError<string>("Synergy " + name + " has no mandatory items/guns.", "FF0000");
				result = null;
			}
			else
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				List<int> list3 = new List<int>();
				List<int> list4 = new List<int>();
				foreach (string id in mandatoryConsoleIDs)
				{
					PickupObject pickupObject = Game.Items[id];
					bool flag2 = pickupObject && pickupObject.GetComponent<Gun>();
					if (flag2)
					{
						list2.Add(pickupObject.PickupObjectId);
					}
					else
					{
						bool flag3 = pickupObject && (pickupObject.GetComponent<PlayerItem>() || pickupObject.GetComponent<PassiveItem>());
						if (flag3)
						{
							list.Add(pickupObject.PickupObjectId);
						}
					}
				}
				bool flag4 = optionalConsoleIDs != null;
				if (flag4)
				{
					foreach (string id2 in optionalConsoleIDs)
					{
						PickupObject pickupObject = Game.Items[id2];
						bool flag5 = pickupObject && pickupObject.GetComponent<Gun>();
						if (flag5)
						{
							list4.Add(pickupObject.PickupObjectId);
						}
						else
						{
							bool flag6 = pickupObject && (pickupObject.GetComponent<PlayerItem>() || pickupObject.GetComponent<PassiveItem>());
							if (flag6)
							{
								list3.Add(pickupObject.PickupObjectId);
							}
						}
					}
				}
				AdvancedSynergyEntry advancedSynergyEntry = new AdvancedSynergyEntry
				{
					NameKey = name,
					MandatoryItemIDs = list,
					MandatoryGunIDs = list2,
					OptionalItemIDs = list3,
					OptionalGunIDs = list4,
					bonusSynergies = new List<CustomSynergyType>(),
					statModifiers = new List<StatModifier>()
				};
				CustomSynergies.Add(advancedSynergyEntry);
				result = advancedSynergyEntry;
			}
			return result;
		}
		public static void Add(AdvancedSynergyEntry synergyEntry)
		{
			AdvancedSynergyEntry[] second = new AdvancedSynergyEntry[]
			{
				synergyEntry
			};
			GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(second).ToArray<AdvancedSynergyEntry>();
		}
		public static string SynergyStringHook(Func<string, int, string> orig, string key, int index = -1)
		{
			string text = orig(key, index);
			bool flag = string.IsNullOrEmpty(text);
			bool flag2 = flag;
			if (flag2)
			{
				text = key;
			}
			return text;
		}
		public static bool HasMTGConsoleID(this PlayerController player, string consoleID)
		{
			bool flag = !Game.Items.ContainsID(consoleID);
			return !flag && player.HasPickupID(Game.Items[consoleID].PickupObjectId);
		}
		public static bool PlayerHasActiveSynergy(this PlayerController player, string synergyNameToCheck)
		{
			foreach (int num in player.ActiveExtraSynergies)
			{
				AdvancedSynergyEntry advancedSynergyEntry = GameManager.Instance.SynergyManager.synergies[num];
				bool flag = advancedSynergyEntry.NameKey == synergyNameToCheck;
				bool flag2 = flag;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}
		public static bool OwnerHasSynergy(this Gun gun, string synergyName)
		{
			return gun.CurrentOwner is PlayerController && (gun.CurrentOwner as PlayerController).PlayerHasActiveSynergy(synergyName);
		}
		public static void AddItemToSynergy(this PickupObject obj, string nameKey)
		{
			CustomSynergies.AddItemToSynergy(nameKey, obj.PickupObjectId);
		}
		public static void AddItemToSynergy(string nameKey, int id)
		{
			foreach (AdvancedSynergyEntry advancedSynergyEntry in GameManager.Instance.SynergyManager.synergies)
			{
				bool flag = advancedSynergyEntry.NameKey == nameKey;
				if (flag)
				{
					bool flag2 = PickupObjectDatabase.GetById(id) != null;
					if (flag2)
					{
						PickupObject byId = PickupObjectDatabase.GetById(id);
						bool flag3 = byId is Gun;
						if (flag3)
						{
							bool flag4 = advancedSynergyEntry.OptionalGunIDs != null;
							if (flag4)
							{
								advancedSynergyEntry.OptionalGunIDs.Add(id);
							}
							else
							{
								advancedSynergyEntry.OptionalGunIDs = new List<int>
								{
									id
								};
							}
						}
						else
						{
							bool flag5 = advancedSynergyEntry.OptionalItemIDs != null;
							if (flag5)
							{
								advancedSynergyEntry.OptionalItemIDs.Add(id);
							}
							else
							{
								advancedSynergyEntry.OptionalItemIDs = new List<int>
								{
									id
								};
							}
						}
					}
				}
			}
		}
		public static void AddItemToSynergy(this PickupObject obj, string nameKey, bool clearMandatory = false)
		{
			CustomSynergies.AddItemToSynergy(nameKey, obj.PickupObjectId, clearMandatory);
		}
		public static void AddItemToSynergy(string nameKey, int id, bool clearMandatory = false)
		{
			foreach (AdvancedSynergyEntry advancedSynergyEntry in GameManager.Instance.SynergyManager.synergies)
			{
				bool flag = advancedSynergyEntry.NameKey == nameKey;
				if (flag)
				{
					bool flag2 = PickupObjectDatabase.GetById(id) != null;
					if (flag2)
					{
						PickupObject byId = PickupObjectDatabase.GetById(id);
						bool flag3 = byId is Gun;
						if (flag3)
						{
							bool flag4 = advancedSynergyEntry.OptionalGunIDs != null;
							if (flag4)
							{
								bool flag5 = advancedSynergyEntry.MandatoryGunIDs != null && clearMandatory;
								if (flag5)
								{
									foreach (int item in advancedSynergyEntry.MandatoryGunIDs)
									{
										advancedSynergyEntry.OptionalItemIDs.Add(item);
									}
									advancedSynergyEntry.MandatoryItemIDs.Clear();
								}
								advancedSynergyEntry.OptionalGunIDs.Add(id);
							}
							else
							{
								advancedSynergyEntry.OptionalGunIDs = new List<int>
								{
									id
								};
							}
						}
						else
						{
							bool flag6 = advancedSynergyEntry.OptionalItemIDs != null;
							if (flag6)
							{
								bool flag7 = advancedSynergyEntry.MandatoryItemIDs != null && clearMandatory;
								if (flag7)
								{
									foreach (int item2 in advancedSynergyEntry.MandatoryItemIDs)
									{
										advancedSynergyEntry.OptionalItemIDs.Add(item2);
									}
									advancedSynergyEntry.MandatoryItemIDs.Clear();
								}
								advancedSynergyEntry.OptionalItemIDs.Add(id);
							}
							else
							{
								advancedSynergyEntry.OptionalItemIDs = new List<int>
								{
									id
								};
							}
						}
					}
				}
			}
		}
		public static Hook synergyHook = new Hook(typeof(StringTableManager).GetMethod("GetSynergyString", BindingFlags.Static | BindingFlags.Public), typeof(CustomSynergies).GetMethod("SynergyStringHook"));
	}
}
