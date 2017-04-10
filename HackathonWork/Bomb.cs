using System;

namespace HackathonWork
{
    internal class Bomb : MovingEntity
    {
        public Bomb( Factory source, Factory destination) : base(EntityType.BOMB, source, destination)
        {
        }

        public override string ToPlayerString(int playerIdx)
        {
            if (_owner.Id == playerIdx  )
            {
                return ToPlayerString(1, _source.Id, _destination.Id, RemainingTurns, 0);
            }
            else
            {
                return ToPlayerString(-1, _source.Id, -1, -1, 0);
            }
        }

        public string ToViewString()
        {
            return $"{_id} {(_owner == null ? 0 : _owner.Id)} {_source.Id} {_destination.Id} {RemainingTurns}";
        }

        public void Explode()
        {
            int damage = Math.Min(_destination.UnitCount, Math.Max(10, _destination.UnitCount / 2));
            _destination.UnitCount -= damage;
            _destination.Disabled = Settings.DamageDuration;
        }
    }
}
