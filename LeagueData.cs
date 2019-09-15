﻿using System.Collections.Generic;

namespace PoE_Price_Lister
{
	public class LeagueData
	{
		public LeagueData(bool hardcore)
		{
			IsHardcore = hardcore;
		}

		public bool IsHardcore { get; private set; }
		public readonly Dictionary<string, DivinationCard> DivinationCards = new Dictionary<string, DivinationCard>();
		public readonly Dictionary<string, UniqueBaseType> Uniques = new Dictionary<string, UniqueBaseType>();
		public readonly Dictionary<string, Enchantment> Enchantments = new Dictionary<string, Enchantment>();
		public readonly Dictionary<string, Enchantment> EnchantmentsDescriptions = new Dictionary<string, Enchantment>();

		public void ClearJson()
		{
			foreach (DivinationCard div in DivinationCards.Values) {
				div.ClearJson();
			}
			foreach (UniqueBaseType uniq in Uniques.Values) {
				uniq.ClearJson();
			}
			foreach (Enchantment ench in Enchantments.Values) {
				ench.ClearJson();
			}
		}

		public void ClearFilterValues()
		{
			foreach (DivinationCard div in DivinationCards.Values) {
				div.FilterValue = DivinationValue.Error;
			}
			foreach (UniqueBaseType uniq in Uniques.Values) {
				uniq.FilterValue = UniqueValue.Unknown;
			}
			foreach (Enchantment ench in Enchantments.Values) {
				ench.FilterValue = EnchantmentValue.Worthless;
			}
		}
	}
}
