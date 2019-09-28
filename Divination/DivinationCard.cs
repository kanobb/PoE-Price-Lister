﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PoE_Price_Lister
{
	public class DivinationCard
	{
		public DivinationCard() { }

		public DivinationCard(string name)
		{
			Name = name;
		}

		public void Load(JsonData item)
		{
			Name = item.Name;
			ChaosValue = item.ChaosValue;
			Count = item.Count;
		}

		public bool IsLowConfidence => Count < 3;

		public string Name { get; set; }

		public string QuotedName {
			get {
				if (Name.Contains(' '))
					return "\"" + Name + "\"";
				return Name;
			}
		}

		public float ChaosValue { get; private set; } = -1.0f;

		public DivinationValue FilterValue { get; set; } = DivinationValue.Error;


		private readonly Dictionary<string, DivinationValue> DivinationCardsValueMap = new Dictionary<string, DivinationValue>()
		{
			// < 0.2c
			{ "Prosperity", DivinationValue.Worthless},
			{ "Struck by Lightning", DivinationValue.Worthless },
			{ "The Inoculated", DivinationValue.Worthless },
			{ "The Metalsmith's Gift", DivinationValue.Worthless },
			{ "The Surgeon", DivinationValue.Worthless },
			{ "Lantador's Lost Love", DivinationValue.Worthless },
			{ "The Carrion Crow", DivinationValue.Worthless },
			{ "The Lover", DivinationValue.Worthless },
			{ "The Rabid Rhoa", DivinationValue.Worthless },
			{ "The Warden", DivinationValue.Worthless },
			{ "Turn the Other Cheek", DivinationValue.Worthless },
			{ "Thunderous Skies", DivinationValue.Worthless },

			// 0.2c+
			{ "The Gambler", DivinationValue.NearlyWorthless },
			{ "Destined to Crumble", DivinationValue.NearlyWorthless },
			{ "The Lord in Black", DivinationValue.NearlyWorthless },
			{ "Rain of Chaos", DivinationValue.NearlyWorthless },
			{ "Her Mask", DivinationValue.NearlyWorthless },
			{ "Loyalty", DivinationValue.NearlyWorthless },
			{ "The Gemcutter", DivinationValue.NearlyWorthless },
			{ "The Scholar", DivinationValue.NearlyWorthless},
			{ "The Survivalist", DivinationValue.NearlyWorthless},
			{ "Cartographer's Delight", DivinationValue.NearlyWorthless },
			{ "The Puzzle", DivinationValue.NearlyWorthless },
			{ "The Hermit", DivinationValue.NearlyWorthless },
			{ "Boon of Justice", DivinationValue.NearlyWorthless },
			{ "The Mountain", DivinationValue.NearlyWorthless },
			{ "Shard of Fate", DivinationValue.NearlyWorthless },
			{ "The Doppelganger", DivinationValue.NearlyWorthless },

			// 0.4c+
			{ "Three Voices", DivinationValue.ChaosLess1},
			{ "The Catalyst", DivinationValue.ChaosLess1 },
			{ "Boundless Realms", DivinationValue.ChaosLess1 },
			{ "Coveted Possession", DivinationValue.ChaosLess1 },
			{ "Emperor's Luck", DivinationValue.ChaosLess1 },
			{ "Three Faces in the Dark", DivinationValue.ChaosLess1 },
			{ "The Master Artisan", DivinationValue.ChaosLess1 },

			// 1.1c+
			{ "No Traces", DivinationValue.Chaos1to10 },
			{ "The Fool", DivinationValue.Chaos1to10 },
			{ "The Heroic Shot", DivinationValue.Chaos1to10 },
			{ "The Inventor", DivinationValue.Chaos1to10 },
			{ "The Wrath", DivinationValue.Chaos1to10 },
			{ "Lucky Connections", DivinationValue.Chaos1to10 },
			{ "The Innocent", DivinationValue.Chaos1to10 },
			{ "Vinia's Token", DivinationValue.Chaos1to10 },
			{ "The Cartographer", DivinationValue.Chaos1to10 },
			{ "Chaotic Disposition", DivinationValue.Chaos1to10 },
			{ "Demigod's Wager", DivinationValue.Chaos1to10 },

			// 10c+
			{ "Wealth and Power", DivinationValue.Chaos10 },
			{ "Alluring Bounty", DivinationValue.Chaos10 },
			{ "The Dragon's Heart",DivinationValue.Chaos10 },
			{ "House of Mirrors",DivinationValue.Chaos10 },
			{ "The Doctor", DivinationValue.Chaos10 },
			{ "The Demon", DivinationValue.Chaos10 },
			{ "The Fiend", DivinationValue.Chaos10 },
			{ "The Immortal", DivinationValue.Chaos10 },
			{ "The Nurse", DivinationValue.Chaos10 },
			{ "The Iron Bard", DivinationValue.Chaos10 },
			{ "Seven Years Bad Luck", DivinationValue.Chaos10 },
			{ "The Saint's Treasure", DivinationValue.Chaos10 },
			{ "The Eye of Terror", DivinationValue.Chaos10 },
		};

		public bool HasHardCodedValue => DivinationCardsValueMap.ContainsKey(Name);

		public DivinationValue ExpectedFilterValue {
			get {
				if(DivinationCardsValueMap.TryGetValue(Name, out DivinationValue val)) {
					return val;
				}
				if (ChaosValue < 0.01f || IsLowConfidence)
					return FilterValue;
				if (FilterValue.LowValue <= ChaosValue && FilterValue.HighValue >= ChaosValue)
					return FilterValue;
				return DivinationValue.ValueOf(ChaosValue);
			}
		}

		public int SeverityLevel {
			get {
				DivinationValue expect = ExpectedFilterValue;
				if (FilterValue == expect || (FilterValue.Tier > expect.Tier && FilterValue.LowValue <= ChaosValue))
					return 0;
				return Math.Abs(FilterValue.Tier - expect.Tier);
			}
		}

		public int Tier => FilterValue.Tier;

		public int Count { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is DivinationCard other) {
				return other.Name == Name;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}


	}
}
