using HackathonWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankRunner
{
    public class Battle
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        public int Seed { get; set; }

        public int? winner
        {
            get
            {
                if (Replay != null)
                {
                    Frame f = Replay.LastOrDefault();
                    if (f != null)
                    {
                        if (f.Players[0].Score > f.Players[1].Score) { return 1; }
                        if (f.Players[0].Score > f.Players[1].Score) { return 2; }
                        return 0;
                    }
                }
                return null;
            }
        }
        
        public int Score1 { get; set; }
        public int Score2 { get; set; }

        public int Points1 { get; set; }
        public int Points2 { get; set; }

        public List<Frame> Replay { get; set; }

        public override string ToString()
        {
            if (winner.HasValue)
            {
                return $"{Team1} ({Score1})-({Score2}) {Team2}  seed {Seed}";
            }
            return $"{Team1} - {Team2}";
        }


    }
}
