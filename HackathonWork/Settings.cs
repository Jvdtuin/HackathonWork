using System;

namespace HackathonWork
{
    internal class Settings
    {
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

        internal static T GetProperty<T>(string v)
        {
            throw new NotImplementedException();
        }

        internal const int DamageDuration = 5;

        internal static bool MoveRestrictionEnabled;
        internal static bool IncreaseActionEnabled;
        internal static int FactoryRadius;

        public static int? CustomInitialUnitCount { get; internal set; }
        public const char ActionSplittingChar = ';';

        internal static void SetLeageLevel(int leageLevel)
        {
            switch(leageLevel)
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