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

		public struct FactoryInfo
		{
			public int OwnerId;
			public int UnitCount;
			public int CurrentProduction;
		}

		public List<FactoryInfo> Factories { get; set; }

		// troop info - owner, source, destination, untis, remaining moves
		public struct TroopInfo
		{
			public int OwnerId;
			public int SourceId;
			public int DestinationId;
			public int UnitCount;
			public int RemaingMoves;
		}

		public List<TroopInfo> Troops { get; set; }

		// bom info - owner, source, destinaition, remaining moves
		public struct BombInfo
		{
			public int OwnerId;
			public int SourceId;
			public int DestinationId;
			public int RemaingMoves;
		}

		public List<BombInfo> Bombs { get; set; }
		

		// inc info - source
		public List<int> Incs { get; set; }
	}
}
