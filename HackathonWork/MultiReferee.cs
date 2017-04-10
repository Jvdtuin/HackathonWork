using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace HackathonWork
{
	public abstract class MultiReferee
	{
		private string[] _consoles;
		private Process[] _processes;

		private int _playerCount;

		protected MultiReferee(string[] consoles)
		{
			_consoles = consoles;

			
		}

		private Process CreatePlayerProcess(string filename)
		{
			Process process = new Process();

			process.StartInfo = new ProcessStartInfo(filename);
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.ErrorDialog = true;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			return process;
		}

		private static void ErrorStream(Process process)
		{
			new Thread(() =>
			{
				while (true)
				{
					int current;
					while ((current = process.StandardError.Read()) >= 0)
					{
						// error stream 
					}
				}
			});
		}

		private static string ReadLine(Process process)
		{
			return process.StandardOutput.ReadLine();
		}

		private static void WriteLine(Process process, string line)
		{
			process.StandardInput.WriteLine(line);
		}

		protected abstract void InitReferee(int playerCount);

		protected abstract string[] GetInitInputForPlayer(int playerIdx);

		protected abstract string[] GetInitInputForPlayer(int round, int playerIdx);

		protected abstract void HandlePlayerOutput(int frame, int round, int playerIdx, string[] outputs);

		protected abstract void UpdateGame(int round);

		public void PlayGame()
		{
			_playerCount = _consoles.Length;
			// initialize the player consoles
			_processes = new Process[_playerCount];
			for (int i = 0; i < _playerCount; i++)
			{
				Process p = CreatePlayerProcess(_consoles[i]);
				p.Start();
			}
			// create the player structure
			InitReferee(_playerCount);
			
			// send initialization data to the player consolose
			for (int i=0; i< _playerCount; i++)
			{
				string[] initLines = GetInitInputForPlayer(i);
				for (int j=0; j < initLines.Length; j++)
				{
					WriteLine(_processes[i], initLines[j]);
				}
			}

			try
			{
				int turnCounter = 0;
				int frameCounter = 0;
				while (turnCounter < 200)
				{
					string[] playerResponse = new string[_playerCount];
					for (int i=0; i<_playerCount; i++)
					{
						// initialize the turn
						string[] turnLines = GetInitInputForPlayer(turnCounter, i);
						for (int j = 0; j < turnLines.Length; j++)
						{
							WriteLine(_processes[i], turnLines[j]);
						}
						// and wait for the player to respond
						playerResponse[i] = ReadLine(_processes[i]);
					}
					// process the player responses 

					for (int i=0; i<_playerCount; i++)
					{
						HandlePlayerOutput(frameCounter++, turnCounter, i, new string[] { playerResponse[i] });
					}
					UpdateGame(turnCounter);


					turnCounter++;
				}
			}
			catch (Exception ex)
			{
				if (ex is GameOverException)
				{
					var goe = (GameOverException)ex;
				}
				else if (ex is InvalidInputException)
				{
					var iie = (InvalidInputException)ex;
				}
				else if (ex is LostException)
				{
					var le = (LostException)ex;
				}
				else
				{
					throw ex;
				}
			}
		}
	}
}