using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackathonWork
{
    public class Player
    {
        private int _id;

        private List<MoveAction> _lastMoveActions;
        private List<BombAction> _lastBombActions;
        private List<IncAction> _lastIncActions;

        private string _message;
        private int _score;
        private Factory[] _factories;

        private List<Troop> _troops;

        public Player(int id)
        {
            _id = id;
            _score = 0;
            RemainingBombs = Settings.BombsPerPlayer;
            _lastMoveActions = new List<MoveAction>();
            _lastBombActions = new List<BombAction>();
            _lastIncActions = new List<IncAction>();
        }

        public int Id { get { return _id; } }

        internal List<MoveAction> LastMoveActions { get { return _lastMoveActions; } }
        internal List<BombAction> LastBombActions { get { return _lastBombActions; } }
        internal List<IncAction> LastIncActions { get { return _lastIncActions; } }

        public int RemainingBombs { get; set;  }

        public string Message { get; internal set; }

        /// <summary>
        /// When a player is dead, it loses its factories and troops
        /// </summary>
        public void SetDead()
        {
            foreach (Factory factory in _factories)
            {
                if (factory.Owner == this)
                {
                    factory.Owner = null;
                }
            }
            for (int i = _troops.Count-1; i>=0; i--)
            {
                Troop troop = _troops[i];
                if (troop.Owner == this)
                {
                    _troops.Remove(troop);
                }
            }
            _score = 0;
        }

        internal void SetTroops(List<Troop> troops)
        {
            _troops = troops;
        }

        internal void SetFactories(Factory[] factories)
        {
            _factories = factories;
        }

    }
}
