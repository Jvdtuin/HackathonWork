using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWork
{
    public class MatchData
    {
        public string[] PlayerNames { get; set; }

        public List<Frame> Frames { get; set; }

        public int? Winner { get; set; }

        public int VictoryType { get; set; }
    }
}
