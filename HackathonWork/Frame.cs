using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWork
{
	public class Frame
	{
		// factory info - owner, troops, production
        public Frame()
        {
            Factories = new List<FactoryInfo>();
            Troops = new List<TroopInfo>();
            Bombs = new List<BombInfo>();
            Incs = new List<int>();
            Players = new List<PlayerInfo>();
        }

		public struct FactoryInfo
		{
			public int? OwnerId;
			public int UnitCount;
			public int CurrentProduction;
            public int Id;
			public double Y;
			public double X;
		}

		public List<FactoryInfo> Factories { get; set; }

		// troop info - owner, source, destination, untis, remaining moves
		public struct TroopInfo
		{
			public int OwnerId;
			public int SourceId;
			public int DestinationId;
			public int UnitCount;
			public int RemaingTurns;
			public int TotalTurns;
            internal int Id;
        }

		public List<TroopInfo> Troops { get; set; }

        // bom info - owner, source, destinaition, remaining moves
        public struct BombInfo
        {
            public int OwnerId;
            public int SourceId;
            public int DestinationId;
            public int RemainingTurns;
            public int Id;
			public int TotalTurns;
		}
		public List<BombInfo> Bombs { get; set; }
		
        public struct PlayerInfo
        {
            public int Score;
            public int RemainingBombs;
        }
        public List<PlayerInfo> Players { get; set; }

		// inc info - source
		public List<int> Incs { get; set; }

       
	}
}
