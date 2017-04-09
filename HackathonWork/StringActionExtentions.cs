using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonWork
{
    public static class StringActionExtentions
    {
        public static bool IsMove(this string action,  out int source, out int destination, out int units )
        {
            string[] args = action.Split(' ');
            if (args.Length == 4)
            {

            }
            source = 0;
            destination = 0;
            units = 0;
            return false;
        } 

        public static bool IsBomb(this string action, out int source, out int destination)
        {
            source = 0;
            destination = 0;
            return false;
        }

        public static bool IsInc(this string action, out int source)
        {
            source = 0;
            return false;
        }

        public static bool IsWait(this string action)
        {
            return false;
        }

        public static bool IsMessage(this string action)
        {
            return false;
        }
    }
}
