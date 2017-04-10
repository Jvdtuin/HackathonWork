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
			source = 0;
			destination = 0;
			units = 0;
			string[] args = action.Split(' ');
			try
			{
				if (args.Length == 4)
				{
					if (args[0].ToUpper() == ActionType.MOVE.ToString())
					{
						source = int.Parse(args[1]);
						destination = int.Parse(args[2]);
						units = int.Parse(args[3]);
						return true;
					}
				}
			}
			catch
			{
				// do nothing just return false
			}
            return false;
        } 

        public static bool IsBomb(this string action, out int source, out int destination)
        {
            source = 0;
            destination = 0;
			string[] args = action.Split(' ');
			try
			{
				if (args.Length == 3)
				{
					if (args[0].ToUpper() == ActionType.BOMB.ToString())
					{
						source = int.Parse(args[1]);
						destination = int.Parse(args[2]);
						return true;
					}
				}
			}
			catch
			{
				// do nothing just return false
			}
			return false;
        }

        public static bool IsInc(this string action, out int source)
        {
            source = 0;
			string[] args = action.Split(' ');
			try
			{
				if (args.Length == 2)
				{
					if (args[0].ToUpper() == ActionType.INC.ToString())
					{
						source = int.Parse(args[1]);
						return true;
					}
				}
			}
			catch
			{
				// do nothing just return false
			}
			return false;
        }

        public static bool IsWait(this string action)
        {
			if (action.ToUpper() == ActionType.WAIT.ToString() )
			{
				return true;
			}
            return false;
        }

        public static bool IsMessage(this string action)
        {
            return false;
        }
    }
}
