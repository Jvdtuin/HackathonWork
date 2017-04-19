using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankRunner
{
    public class Team 
    {
        public string Name { get; set; }

        public string Application { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public int Points { get; set; }

        public int Matches { get; set; }
    }
}
