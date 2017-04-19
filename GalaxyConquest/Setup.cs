using GalaxyConquest.Properties;
using HackathonWork;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalaxyConquest
{
	public partial class Setup : Form
	{


		public Setup()
		{
			InitializeComponent();
            cbLevel.Items.Clear();
            int items = int.Parse(ConfigurationManager.AppSettings["Opponents"]);
            for (int i=0; i<items; i++)
            {
                cbLevel.Items.Add($"Level {i}");
            }
            cbLevel.SelectedIndex = 0;
            Leage.Items.Clear();
            items = int.Parse(ConfigurationManager.AppSettings["MaxLeague"]);
            for (int i =0; i< items; i++)
            {
                Leage.Items.Add($"Round {i + 1}");
            }

            Leage.SelectedIndex = 0;
		}

		private void Setup_Load(object sender, EventArgs e)
		{

		}

		private void Debug()
		{
			MessageBox.Show("You can attach your debugger now");
		}

        private List<Frame> _frames;
        private ConcurrentQueue<ConsoleOutputEventArgs> _outputEvents;

        private void button1_Click(object sender, EventArgs e)
        {
            _outputEvents = new ConcurrentQueue<ConsoleOutputEventArgs>();
            string filePath = textBox1.Text;
            
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Player program not found");
                return;
            }


            string opponentAI = ConfigurationManager.AppSettings[$"AI{cbLevel.SelectedIndex}"]; 
           
            if (!File.Exists(opponentAI))
            {
                MessageBox.Show("Oponent program not found");
                return;
            }


            string[] playerNames = new string[2];
            string[] players = new string[2];
            if (rbtnBlue.Checked)
            {
                players[0] = filePath;
                playerNames[0] = "User application";
                players[1] = opponentAI;
                playerNames[1] = "Boss program";
            }
            else
            {
                players[1] = filePath;
                playerNames[1] = "User application";
                players[0] = opponentAI;
                playerNames[0] = "Boss program";
            }

            int index = Leage.SelectedIndex;
            if (index == 2)
            { index = 3; }

            HackathonWork.Settings.SetLeageLevel(index);

            Referee referee = new Referee(players);
             int seed;
            if (int.TryParse(SeedTb.Text, out seed))
            {
                referee.Seed = seed;
            }

            
            DebugBreak debugMethod = null;
            if (chbxDebug.Checked)
            {
                HackathonWork.Settings.UseTimeOut = false;
                debugMethod = this.Debug;
            }
            else
            {
                HackathonWork.Settings.UseTimeOut = true;
            }
            try
            {
                if (rbtnBlue.Checked)
                {
                    referee.ConsoleErrorOutputPlayer1 += ConsoleErrorOutputPlayer;
                }
                else
                {
                    referee.ConsoleErrorOutputPlayer2 += ConsoleErrorOutputPlayer;
                }
                referee.ConsoleOutputPlayer += ConsoleErrorOutputPlayer;

                referee.PlayGame(debugMethod);
            }
            catch (Exception ex)
            {
                // who throw the exception?
            }

            _frames = referee.GetFrames();

           
            MatchData md = new MatchData()
            {
                Frames = _frames,
                PlayerNames = playerNames,
            };

			Viewer v = new Viewer(md);
			v.Show();
            
            PrintConsoleOUtput();
		}

        private void PrintConsoleOUtput()
        {
            StringBuilder sb = new StringBuilder();
            foreach(ConsoleOutputEventArgs e in _outputEvents.ToArray())
            {
                if (e.Error)
                {
                    sb.Append("DEBUG OUTPUT: ");
                }
                else
                {
                    sb.Append($"COMMAND {(e.Player == 0 ? "BLUE" : "RED")}: ");
                }
                sb.AppendLine(e.Line);
            }
            richTextBox1.Text = sb.ToString();
            
        }

        private void ConsoleErrorOutputPlayer(object sender, ConsoleOutputEventArgs e)
        {
            _outputEvents.Enqueue(e);             
        }

        private void browse_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = "Player.exe";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = openFileDialog1.FileName;
			}
		}
    }
}
