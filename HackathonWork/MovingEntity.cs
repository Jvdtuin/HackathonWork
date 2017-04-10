using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWork
{
    internal abstract class MovingEntity : Entity
    {
        protected Player _owner;
        public int RemainingTurns { get; protected set; }
        protected Factory _source;
        protected Factory _destination;
		 
        public MovingEntity(EntityType type, Factory source, Factory destination) : base(type)
        {
            _owner = source.Owner;
            _source = source;
            _destination = destination;
            RemainingTurns = source.GetDistanceTo(destination);
        }

        public Factory Source { get { return _source; } }
        public Factory Destination { get { return _destination; } }


        public void Move()
        {
            this.RemainingTurns--;
        }

        public Player Owner { get { return _owner; } }

        public A FindWithSameRouteInList<A>(List<A> list) where A:MovingEntity
        {
            foreach (A other in list)
            {
                if (other.Source == this.Source && other.Destination == this.Destination)
                {
                    return other;
                }
            }
            return null;
        }
    }
}
