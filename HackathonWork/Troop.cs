using System;

namespace HackathonWork
{
    internal class Troop : MovingEntity
    {
        private int _unitCount;

        public Troop( Factory source, Factory destination, int unitCount) : base(EntityType.TROOP, source, destination)
        {
            _unitCount = unitCount;
        }

    
        public override string ToPlayerString(int playerIdx)
        {
            int ownerShip = 0;
            if (_owner != null)
            {
                ownerShip = (playerIdx == _owner.Id) ? 1 : -1;
            }
            return ToPlayerString(ownerShip, _source.Id, _destination.Id, _unitCount, _remainingTurns);
        }

        public string ToViewString()
        {
            return $"{_id} {(_owner == null ? 0 : _owner.Id)} {_source.Id} {_destination.Id} {_unitCount} {_remainingTurns}";
        }
    }
}