using System;
using System.Collections.Generic;

namespace HackathonWork
{
    public class Factory : Entity
    {
        public Factory(Player owner, int x, int y, int unitCount, int productionRate, GetId getId) : base(EntityType.FACTORY, getId)
        {
            Owner = owner;
            Position = new Point(x, y);
            UnitCount = unitCount;
            ProductionRate = productionRate;
        }

        public Player Owner { get; set; }
        public Point Position { get; set; }
        public int UnitCount { get; set; }
        public int ProductionRate { get; set; }
        public int Disabled { get;  set; }
        public Dictionary<int, int> Distances { get; private set; }

		public int[] UnitsReadyToFight { get; private set; } = new int[2];



		public void ComputeDistances(Factory[] factories)
        {
            Distances = new Dictionary<int, int>();
            foreach(Factory factory in factories)
            {
                if (this != factory)
                {
                    int d = (int)Math.Round((Position.Distance(factory.Position) - 2 * Settings.FactoryRadius) / 800.0);
                    Distances.Add(factory.Id, d);
                }
            }   
        }

        public int GetDistanceTo (Factory factory)
        {
            return Distances[factory.Id];
        }

        public int GetCurrentProductionRate()
        {
            return (Disabled == 0) ? this.ProductionRate : 0;
        }

        public override string ToPlayerString(int playerIdx)
        {
            int ownerShip = 0;
            if (Owner != null)
            {
                ownerShip = (playerIdx == Owner.Id) ? 1 : -1;
            }
            return ToPlayerString(ownerShip, UnitCount, ProductionRate, Disabled, 0);
        }

        public string ToViewStringInit()
        {
            return $"{Id} {ProductionRate} {Position.X} {Position.Y} {Settings.FactoryRadius}";
        }

        public string ToViewString()
        {
            return $"{(Owner == null ? -1 : Owner.Id)} {UnitCount} {ProductionRate} {Disabled}";
        }
    }
}