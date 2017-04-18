using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
	static void Main(string[] args)
	{
		string[] inputs;
		int factoryCount = int.Parse(Console.ReadLine()); // the number of factories
		int linkCount = int.Parse(Console.ReadLine()); // the number of links between factories
		for (int i = 0; i < linkCount; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int factory1 = int.Parse(inputs[0]);
			int factory2 = int.Parse(inputs[1]);
			int distance = int.Parse(inputs[2]);
		}

		// game loop
		while (true)
		{
			int entityCount = int.Parse(Console.ReadLine()); // the number of entities (e.g. factories and troops)
			for (int i = 0; i < entityCount; i++)
			{
				inputs = Console.ReadLine().Split(' ');
				int entityId = int.Parse(inputs[0]);
				string entityType = inputs[1];
				int arg1 = int.Parse(inputs[2]);
				int arg2 = int.Parse(inputs[3]);
				int arg3 = int.Parse(inputs[4]);
				int arg4 = int.Parse(inputs[5]);
				int arg5 = int.Parse(inputs[6]);
			}

			// Write an action using Console.WriteLine()
			
			// Any valid action, such as "WAIT" or "MOVE source destination cyborgs"
			Console.WriteLine("WAIT");
		}
	}
}