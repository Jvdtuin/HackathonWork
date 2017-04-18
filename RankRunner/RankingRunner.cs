using HackathonWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RankRunner
{
    public partial class RankingRunner : Form
    {
        public RankingRunner()
        {
            InitializeComponent();
            _teams = new List<Team>();

        }


        private ListBox Compettitors;
        private ListBox Matches;
        private TextBox ApplicationTb;
        private TextBox TeamNameTb;
        private Button browse;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private Label label2;
        private Button button2;
        private Button button3;


        private List<Team> _teams;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ApplicationTb.Text = openFileDialog1.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TeamNameTb.Text))
            {
                if (File.Exists(ApplicationTb.Text))
                {
                    AddTeam(TeamNameTb.Text, ApplicationTb.Text);
                }
            }
        }


        private void AddTeam(string name, string application)
        {
            Team newTeam = new Team() { Name = name, Application = application };
            _teams.Add(newTeam);
            Compettitors.Items.Add(newTeam);
        }


        /// <summary>
        /// Run battels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // create battle schedule    

            // run the battles
        }


        private void Runbattle (Battle battle)
        {
            string[] players = new string[2];
            players[0] = battle.Team1.Application;
            players[1] = battle.Team2.Application;

            Referee referee = new Referee(players);

            referee.Seed = battle.Seed;

            referee.PlayGame(null);

            battle.Replay = referee.GetFrames();
            Frame frame = battle.Replay.LastOrDefault();
            if (frame != null)
            {
                battle.Score1 = frame.Players[0].Score;
                battle.Score2 = frame.Players[1].Score;

                if (battle.Score1 == battle.Score2)
                {
                    battle.Points1 = 1;
                    battle.Points2 = 1;
                }
                else if (battle.Score1 > battle.Score2)
                {
                    battle.Points2 = 0;
                    if (battle.Score2 == 0)
                    {
                        battle.Points1 = 3;
                    }
                    else
                    {
                        battle.Points1 = 2;
                    }
                }
                else
                {
                    battle.Points1 = 0;
                    if (battle.Score1 == 0)
                    {
                        battle.Points2 = 3;
                    }
                    else
                    {
                        battle.Points2 = 2;
                    }
                }               
            }      
             
        }

       
    }
}
