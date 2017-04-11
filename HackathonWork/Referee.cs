﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWork
{
    public class Referee : MultiReferee
    {
        private Player[] _players;
        private Factory[] _factories;
        private List<Troop> _troops;
        private List<Troop> _newTroops;
        private List<Bomb> _bombs;
        private List<Bomb> _newBombs;
        private List<Frame> _frames;

        private Random _random;

		public Referee(string[] players) : base(players)
		{
			Entity.Reset();
		}

		public int Seed { get; set; }
        public int CustomFactoryCount { get; set; }
        public int CustomInitialUnitCount { get; set; }

        protected override void InitReferee(int playerCount)
        {
            

            int factoryCount = Settings.FactoryCount;
            int InitUnitCount = Settings.InitalUnitcount;
            _newTroops = new List<Troop>();
            _newBombs = new List<Bomb>();
			_random = Settings.Random;

            _players = GeneratePlayers(playerCount);
            _factories = GenerateFactories(factoryCount, playerCount);

            _troops = new List<Troop>();
            _bombs = new List<Bomb>();
            _frames = new List<Frame>();
        }

        public List<Frame> GetFrames()
        {
            return _frames;
        }
       
		/// <summary>
        /// Generate the factory objects 
        /// </summary>
        /// <param name="factoryCount"></param>
        /// <param name="playerCount"></param>
        /// <returns></returns>
        private Factory[] GenerateFactories(int factoryCount, int playerCount)
        {
            while (factoryCount % playerCount != 1)
            {
                factoryCount++;
            }
            Settings.FactoryRadius = factoryCount > 10 ? 600 : 700;

            int minSpaceBetweenFactories = 2 * (Settings.FactoryRadius + Settings.ExtraSpaceBetweenFactories);
            Factory[] factories = new Factory[factoryCount];

            int i = 0;
            factories[i++] = new Factory(null, Settings.Width / 2, Settings.Height / 2, 0, 0);
            while (i < factoryCount - 1)
            {
                int x = _random.Next(Settings.Width / 2 - 2 * Settings.FactoryRadius) + Settings.FactoryRadius + Settings.ExtraSpaceBetweenFactories;
                int y = _random.Next(Settings.Height - 2 * Settings.FactoryRadius) + Settings.FactoryRadius + Settings.ExtraSpaceBetweenFactories;

                bool valid = true;
                for (int j = 0; j < i; j++)
                {
                    Factory factory = factories[j];
                    if (factory.Position.Distance(x, y) < minSpaceBetweenFactories)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    int productionRate = Settings.MinProductionRate + _random.Next(Settings.MaxProductionRate - Settings.MinProductionRate + 1);
                    if (i == 1)
                    {
                        int unitCount;
                        if (Settings.CustomInitialUnitCount.HasValue && Settings.CustomInitialUnitCount.Value >= Settings.PlayerInitUnitsMin
                            && Settings.CustomInitialUnitCount.Value <= Settings.PlayerInitUnitsMax)
                        {
                            unitCount = Settings.CustomInitialUnitCount.Value;
                        }
                        else
                        {
                            unitCount = Settings.PlayerInitUnitsMin + _random.Next(Settings.PlayerInitUnitsMax - Settings.PlayerInitUnitsMin + 1);
                        }
                        factories[i++] = new Factory(_players[0], x, y, unitCount, productionRate);
                        factories[i++] = new Factory(_players[1], Settings.Width - x, Settings.Height - y, unitCount, productionRate);
                    }
                    else
                    {
                        int unitCount = _random.Next(5 * productionRate + 1);
                        factories[i++] = new Factory(null, x, y, unitCount, productionRate);
                        factories[i++] = new Factory(null, Settings.Width - x, Settings.Height - y, unitCount, productionRate);
                    }
                }
            }
            int totalProductionRate = 0;
            foreach (Factory factory in factories)
            {
                factory.ComputeDistances(factories);
                totalProductionRate += factory.ProductionRate;
            }
            // Make sure that the initial accumulated production rate for all the factories is 
            // at least MIN_TOTAL_PRODUCTION_RATE
            for (int j = 1; totalProductionRate < Settings.MinTotalProductionRatat
                 && j < factories.Length; j++)
            {
                if (factories[j].ProductionRate < Settings.MaxProductionRate)
                {
                    factories[j].ProductionRate++;
                    totalProductionRate++;
                }
            }

            return factories;
        }

        private Player[] GeneratePlayers(int playerCount)
        {

            Player[] players = new Player[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new Player(i);
            }
            return players;
        }

     //   protected Properties GetConfiguration() => 450

        protected override string[] GetInitInputForPlayer(int playerIdx)
        {
            List<string> data = new List<string>();
            data.Add(_factories.Length.ToString()); // print number of factories

            List<string> links = new List<string>();
            for (int i =0; i< _factories.Length; i++)
            {
                for (int j= i+1; j < _factories.Length; j++)
                {
                    links.Add($"{_factories[i].Id} {_factories[j].Id} {_factories[i].GetDistanceTo(_factories[j])}");
                }
            }
            data.Add(links.Count.ToString());
            data.AddRange(links);
            return data.ToArray();
        }

        protected override string[] GetInitInputForPlayer(int round, int playerIdx)
        {
            List<string> data = new List<string>();
            List<string> entities = new List<string>();

            foreach(Factory factory in _factories)
            {
                entities.Add(factory.ToPlayerString(playerIdx));
            }

            foreach(Troop troop in _troops)
            {
                entities.Add(troop.ToPlayerString(playerIdx));
            }
            foreach(Bomb bomb in _bombs)
            {
                entities.Add(bomb.ToPlayerString(playerIdx));
            }
            data.Add(entities.Count.ToString());
            data.AddRange(entities);
            return data.ToArray();
        }

        protected override void HandlePlayerOutput(int frame, int round, int playerIdx, string[] outputs)
        {
            Player player = _players[playerIdx];
            player.LastBombActions.Clear();
            player.LastIncActions.Clear();
            player.LastMoveActions.Clear();

            player.Message = null;
            try
            {
                foreach (string line in outputs)
                {
                    int source;
                    int destination;
                    int units;
					if (string.IsNullOrEmpty(line))
					{
						throw new InvalidInputException("Line was empty");
					}
                    string[] actions = line.Split(Settings.ActionSplittingChar);
                    foreach(string action in actions)
                    {
                
                        if (action.IsMove(out  source, out destination, out units))
                        {
                            if (Settings.MoveRestrictionEnabled && !(player.LastBombActions.Count > 0))
                            {
                                continue;
                            }
                            if (source >= _factories.Length)
                            {
                                throw new InvalidInputException($"0 <= source < {_factories.Length}", source.ToString());
                            }
                            if (destination >= _factories.Length)
                            {
                                throw new InvalidInputException($"0 <= destination < {_factories.Length}", destination.ToString());
                            }
                            if (_factories[source].Owner != player)
                            {
                                throw new LostException("Move from not controlled factory", source);
                            }
                            if (source == destination)
                            {
                                throw new LostException("Move same source and destination", destination);
                            }
                            player.LastMoveActions.Add(new MoveAction(_factories[source], _factories[destination], units));
                        }
                        else if (action.IsBomb(out source, out destination))
                        {
                            if (source >= _factories.Length)
                            {
                                throw new InvalidInputException($"0 <= source < {_factories.Length}", source.ToString());
                            }
                            if (destination >= _factories.Length)
                            {
                                throw new InvalidInputException($"0 <= destination < {_factories.Length}", destination.ToString());
                            }
                            if (_factories[source].Owner != player)
                            {
                                throw new LostException("Bomb from not controlled factory", source);
                            }
                            if (source == destination)
                            {
                                throw new LostException("Bomb same source and destination", destination);
                            }
                            player.LastBombActions.Add(new BombAction(_factories[source], _factories[destination]));
                        }
                        else if (action.IsInc(out source))
                        {
                            if (!Settings.IncreaseActionEnabled)
                            {
                                // silently ignore increase actions
                                continue;
                            }
                            if (source >= _factories.Length)
                            {
                                throw new InvalidInputException($"0 <= source < {_factories.Length}", source.ToString());
                            }
                            if (_factories[source].Owner != player)
                            {
                                throw new LostException("Inc from not controlled factory", source);
                            }
                            player.LastIncActions.Add(new IncAction(_factories[source]));
                        }
                        else if (action.IsWait())
                        {
                            // do nothing
                        }
                        else if (action.IsMessage())
                        {

                            player.Message = action;
                        }
                        else
                        {
                            throw new InvalidInputException("A invalid action", action);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                player.SetDead();
                throw ex;
            }
        }

        protected override void UpdateGame(int round)
        {
            _newTroops.Clear();
            _newBombs.Clear();

            // move troops and bombs
            foreach (Troop troop in _troops)
            {
                troop.Move();
            }
            foreach (Bomb bomb in _bombs)
            {
                bomb.Move();
            }

            // decrease disabled countdown
            foreach(Factory factory in _factories)
            {
                if (factory.Disabled > 0 )
                {
                    factory.Disabled--;
                }
            }

            // execute orders
            foreach(Player player in _players)
            {
                // send bombs
                foreach (BombAction bombAction in player.LastBombActions)
                {
                    Bomb bomb = new Bomb(bombAction.Source, bombAction.Destination);
                    if (player.RemainingBombs > 0 && bomb.FindWithSameRouteInList(_newBombs) == null) // todo hier check of niet 2 keer dezelfde actie gedaan wordt
                    {
                        _newBombs.Add(bomb);
                        _bombs.Add(bomb);
                        player.RemainingBombs--;
						AddToolTip(player.Id, $"BombAction {player.Id} {bombAction.Source.Id} {bombAction.Destination.Id}");
                    }
                }
                
                // send troops
                foreach (MoveAction moveAction in player.LastMoveActions)
                {
                    int unitsToMove = Math.Min(moveAction.Units, moveAction.Source.UnitCount);
                    Troop troop = new Troop(moveAction.Source, moveAction.Destination, unitsToMove);
                    if (unitsToMove >0 && troop.FindWithSameRouteInList(_newBombs) == null )
                    {
						moveAction.Source.UnitCount -= unitsToMove;
						Troop other = troop.FindWithSameRouteInList(_newTroops);
						if (other !=null)
						{
							other.UnitCount += unitsToMove;  
						}
						else
						{
							_troops.Add(troop);
							_newTroops.Add(troop);
						}
                    }
                }

                // increase production
				foreach (IncAction incAction in player.LastIncActions)
				{
					if (incAction.Source.UnitCount >= Settings.ConstIncreaseProduction && incAction.Source.ProductionRate < Settings.MaxProductionRate )
					{
						incAction.Source.ProductionRate++;
						incAction.Source.UnitCount -= Settings.ConstIncreaseProduction;
						AddToolTip(player.Id, $"IncAction {player.Id} {incAction.Source.Id}");
					}
				}
            }

            // create new units
			foreach (Factory factory in _factories)
			{
				if (factory.Owner != null)
				{
					factory.UnitCount += factory.GetCurrentProductionRate();
				}
			}

            // solve battels
			foreach (Factory factory in _factories)
			{
				factory.UnitsReadyToFight[0] = 0;
				factory.UnitsReadyToFight[1] = 0;
			}
			for (int i= _troops.Count -1; i>=0; i-- )
			{
				Troop troop = _troops[i];
				if (troop.RemainingTurns <= 0)
				{
					troop.Destination.UnitsReadyToFight[troop.Owner.Id] += troop.UnitCount;
					_troops.Remove(troop);
				}

			}
			foreach(Factory factory in _factories)
			{
				// units form both players fight first
				int units = Math.Min(factory.UnitsReadyToFight[0], factory.UnitsReadyToFight[1]);
				factory.UnitsReadyToFight[0] -= units;
				factory.UnitsReadyToFight[1] -= units;
				// Remaining units fight on the factory
				foreach(Player player in _players)
				{
					if (factory.Owner == player)
					{
						// allied
						factory.UnitCount += factory.UnitsReadyToFight[player.Id];
					}
					else
					{
						// opponent
						if (factory.UnitsReadyToFight[player.Id] > factory.UnitCount)
						{
							factory.Owner = player;
							factory.UnitCount = factory.UnitsReadyToFight[player.Id] - factory.UnitCount;
						}
						else
						{
							factory.UnitCount -= factory.UnitsReadyToFight[player.Id];
						}
					}
				}
			}

            // solve bombs
			for (int i=_bombs.Count-1; i>=0;i--)
			{
				Bomb bomb = _bombs[i];
				if (bomb.RemainingTurns <=0 )
				{
					bomb.Explode();
					_bombs.Remove(bomb);
				}
			}

            // update score
			foreach (Player player in _players)
			{
				player.Score = 0;
			}
			foreach(Factory factory in _factories)
			{
				if (factory.Owner != null)
				{
					factory.Owner.Score += factory.UnitCount;
				}
			}
			foreach(Troop troop in _troops)
			{
				if (troop.Owner != null)
				{
					troop.Owner.Score += troop.UnitCount;
				}
			}

			// check end conditions
			bool gameOver = false;
			foreach(Player player in _players)
			{
				if (player.Score == 0)
				{
					int production = 0;
					foreach (Factory factory in _factories)
					{
						if (factory.Owner == player)
						{
							production += factory.ProductionRate;
						}
					}
					if (production == 0)
					{
						gameOver = true;
					}
				}
			}
            

			if (gameOver)
			{
				throw new GameOverException("EndReached");
			}
        }

        protected override void AddFrame()
        {
            Frame frame = new Frame();
            foreach(Factory factory in _factories)
            {
				frame.Factories.Add(new Frame.FactoryInfo()
				{
					Id = factory.Id,
					CurrentProduction = factory.GetCurrentProductionRate(),
					OwnerId = (factory.Owner != null) ? factory.Owner.Id : (int?)null,
					UnitCount = factory.UnitCount,
					X = factory.Position.X,
					Y = factory.Position.Y,
					
                });                
            }
            foreach(Troop troop in _troops)
            {
                frame.Troops.Add(new Frame.TroopInfo()
                {
                    Id = troop.Id,
                    OwnerId = troop.Owner.Id,
                    SourceId = troop.Source.Id,
                    DestinationId = troop.Destination.Id,
                    RemaingTurns = troop.RemainingTurns,
					TotalTurns = troop.Source.Distances[troop.Destination.Id],
                    UnitCount = troop.UnitCount,
                });
            }
            foreach(Bomb bomb in _bombs)
            {
                frame.Bombs.Add(new Frame.BombInfo()
                {
                    Id = bomb.Id,
                    OwnerId = bomb.Owner.Id,
                    SourceId = bomb.Source.Id,
                    DestinationId = bomb.Destination.Id,
                    RemainingTurns = bomb.RemainingTurns,
					TotalTurns = bomb.Source.Distances[bomb.Destination.Id],
				});
            }
            foreach(Player player in _players)
            {
                foreach (IncAction action in player.LastIncActions)
                {
                    frame.Incs.Add(action.Source.Id);
                }
                frame.Players.Add(new Frame.PlayerInfo()
                {
                    RemainingBombs = player.RemainingBombs,
                    Score = player.Score,
                });
            }
            _frames.Add(frame);
        }

		private void AddToolTip(int id, string v)
		{
			
		}
	}
}