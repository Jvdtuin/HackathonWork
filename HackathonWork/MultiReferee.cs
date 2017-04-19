using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace HackathonWork
{
    public delegate void DebugBreak();

    public abstract class MultiReferee
    {
        private string[] _consoles;
        private Process[] _processes;

        private Thread[] _errorStreamThreads;

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

        private Thread ErrorStream(Process process, int player)
        {
            Thread result = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        string current = process.StandardError.ReadLine();
                        OnConsoleErrorOutput(new ConsoleOutputEventArgs() { Line = current, Player = player, Error = true, });
                    }
                }
                catch
                { }
            });
            result.Start();
            return result;
        }

        private string ReadLine(Process process, int? timeOut, int player)
        {
            string result = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Thread worker = new Thread(() =>
                {
                    result = process.StandardOutput.ReadLine();
                    OnConsoleOutput(new ConsoleOutputEventArgs() { Error = false, Line = result, Player = player, });
                });
            worker.Start();
            try
            {
                while ((!timeOut.HasValue || (sw.ElapsedMilliseconds < timeOut.Value)) && string.IsNullOrEmpty(result))
                {

                }
                return result;
            }
            finally
            {
                worker.Abort();
            }

        }

        private void WriteLine(Process process, string line)
        {
            process.StandardInput.WriteLine(line);
        }

        protected abstract void InitReferee(int playerCount);

        protected abstract string[] GetInitInputForPlayer(int playerIdx);

        protected abstract string[] GetInitInputForPlayer(int round, int playerIdx);

        protected abstract void HandlePlayerOutput(int frame, int round, int playerIdx, string[] outputs);

        protected abstract void UpdateGame(int round);

        protected abstract void AddFrame();

        protected abstract void AddFinishFrame();

        public void PlayGame(DebugBreak debugBreak)
        {
            _playerCount = _consoles.Length;
            // initialize the player consoles
            _processes = new Process[_playerCount];
            _errorStreamThreads = new Thread[_playerCount];
            try
            {
                for (int i = 0; i < _playerCount; i++)
                {
                    Process p = CreatePlayerProcess(_consoles[i]);
                    _processes[i] = p;
                    p.Start();
                    _errorStreamThreads[i] = ErrorStream(p, i);
                }

                if (debugBreak != null)
                {
                    debugBreak();
                }

                // create the player structure
                InitReferee(_playerCount);

                // send initialization data to the player consolose
                for (int i = 0; i < _playerCount; i++)
                {
                    string[] initLines = GetInitInputForPlayer(i);
                    for (int j = 0; j < initLines.Length; j++)
                    {
                        WriteLine(_processes[i], initLines[j]);
                    }
                }
                try
                {
                    int turnCounter = 0;
                    int frameCounter = 0;
                    AddFrame();
                    while (turnCounter < Settings.MaxRounds)
                    {
                        string[] playerResponse = new string[_playerCount];
                        for (int i = 0; i < _playerCount; i++)
                        {

                            // initialize the turn
                            string[] turnLines = GetInitInputForPlayer(turnCounter, i);
                            for (int j = 0; j < turnLines.Length; j++)
                            {
                                WriteLine(_processes[i], turnLines[j]);
                            }
                            // and wait for the player to respond

                            int? timeout = Settings.UseTimeOut ? (turnCounter == 0 ? Settings.FirstTimeout : Settings.Timeout) : (int?)null;
                            playerResponse[i] = ReadLine(_processes[i], timeout, i);

                        }
                        // process the player responses 

                        for (int i = 0; i < _playerCount; i++)
                        {

                            HandlePlayerOutput(frameCounter++, turnCounter, i, new string[] { playerResponse[i] });



                        }
                        UpdateGame(turnCounter);
                        turnCounter++;
                        AddFrame();

                        Console.WriteLine(turnCounter);
                    }
                    AddFinishFrame();
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

                    }
                    AddFinishFrame();

                }
            }
            finally
            {
                for (int i = 0; i < _playerCount; i++)
                {
                    try
                    {
                        _processes[i].Kill();
                        _errorStreamThreads[i].Abort();
                    }
                    catch
                    { }
                }
            }
        }


        protected virtual void OnConsoleErrorOutput(ConsoleOutputEventArgs e)
        {
            if (e.Player == 0)
            {
                ConsoleErrorOutputPlayer1?.Invoke(this, e);
            }
            else
            {
                ConsoleErrorOutputPlayer2?.Invoke(this, e);
            }
        }

        protected virtual void OnConsoleOutput(ConsoleOutputEventArgs e)
        {
            ConsoleOutputPlayer?.Invoke(this, e);
        }

        public event EventHandler<ConsoleOutputEventArgs> ConsoleErrorOutputPlayer1;
        public event EventHandler<ConsoleOutputEventArgs> ConsoleErrorOutputPlayer2;

        public event EventHandler<ConsoleOutputEventArgs> ConsoleOutputPlayer;
    }

    public class ConsoleOutputEventArgs : EventArgs
    {
        public bool Error { get; set; }

        public string Line { get; set; }

        public int Player { get; set; }

    }
}