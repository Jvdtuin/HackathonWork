using System;
using System.Collections.Generic;

namespace HackathonWork
{
	public class Settings
	{
		public static int Timeout { get; set; } = 100;

		internal static int LeagueLevel = 3;
		internal const int MinFactoryCount = 7;
		internal static int MaxFactoryCount = 15;

		internal const int MinProductionRate = 0;
		internal const int MaxProductionRate = 3;
		internal const int MinTotalProductionRatat = 4;

		internal static int BombsPerPlayer;

		internal const int PlayerInitUnitsMin = 15;
		internal const int PlayerInitUnitsMax = 30;

		internal const int Width = 16000;
		internal const int Height = 6500;

		internal const int ExtraSpaceBetweenFactories = 300;
		internal const int ConstIncreaseProduction = 10;

		internal static T GetProperty<T>(string key)
		{
			if (Properties.ContainsKey(key))
			{
				return (T)Properties[key];
			}
			return default(T);
		}

		internal static void SetProperty<T>(string key, T value)
		{
			lock (Properties)
			{
				if (Properties.ContainsKey(key))
				{
					Properties[key] = value;
				}
				else
				{
					Properties.Add(key, value);
				}
			}
		}

		internal static Dictionary<string, object> Properties = new Dictionary<string, object>();

		internal const int DamageDuration = 5;

		internal static bool MoveRestrictionEnabled;
		internal static bool IncreaseActionEnabled;
		internal static int FactoryRadius;

		public static int? CustomInitialUnitCount { get; internal set; }

		private static int? _seed = null;
		public static int Seed
		{
			get
			{
				if (_seed.HasValue)
				{
					return _seed.Value;
				}
				return (int)(DateTime.Now.Ticks % (int.MaxValue - 1));
			}
			set
			{
				_seed = value;
			}
		}

		private static int? _factoryCount = null;
		public static int FactoryCount
		{
			get
			{
				if (_factoryCount.HasValue)
				{
					return _factoryCount.Value;
				}
				int f = MinFactoryCount + Random.Next(MaxFactoryCount - MinFactoryCount + 1);
				return f;
			}
			set
			{
				_factoryCount = value;
			}
		}

		private static Random _random = null;
		public static Random Random
		{
			get
			{
				if (_random == null)
				{
					_random = new Random(Seed);
				}
				return _random;
			}

		}

		private static int? _initalUnitcount = null;
		public static int InitalUnitcount
		{
			get
			{
				if (_initalUnitcount.HasValue)
				{
					return _initalUnitcount.Value;
				}
				return PlayerInitUnitsMin + Random.Next(PlayerInitUnitsMax - PlayerInitUnitsMin + 1);
			}
			set
			{
				_initalUnitcount = value;
			}
		}

		public const char ActionSplittingChar = ';';

		internal static void SetLeageLevel(int leageLevel)
		{
			switch (leageLevel)
			{
				case 0:
					MaxFactoryCount = 9;
					MoveRestrictionEnabled = true;
					BombsPerPlayer = 0;
					IncreaseActionEnabled = false;
					break;
				case 1:
					MaxFactoryCount = 15;
					MoveRestrictionEnabled = false;
					BombsPerPlayer = 0;
					IncreaseActionEnabled = false;
					break;
				case 2:
					MaxFactoryCount = 15;
					MoveRestrictionEnabled = false;
					BombsPerPlayer = 2;
					IncreaseActionEnabled = false;
					break;
				default:
					MaxFactoryCount = 15;
					MoveRestrictionEnabled = false;
					BombsPerPlayer = 2;
					IncreaseActionEnabled = true;
					break;
			}
		}

	}
}