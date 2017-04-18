﻿using GalaxyConquest;
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
        private static Random _random = new Random(0); 

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
            List<Battle> battles = CreateBattles();

            // run the battles
            foreach (Battle b in battles)
            {
                Runbattle(b);
            }
            //Parallel.ForEach(battles, b => { Runbattle(b); });
            
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

       
        private List<Battle> CreateBattles()
        {
            int[] seeds = new int[10];
            for (int i=0; i<10;i++)
            {
                seeds[i] = _random.Next(int.MaxValue);
            }
                      

            List<Battle> battles = new List<Battle>();
            foreach(Team team1 in _teams)
            {
                foreach(Team team2 in _teams)
                {
                    if (team1 != team2)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Battle battle = new Battle()
                            {
                                Team1 = team1,
                                Team2 = team2,
                                Seed = seeds[i],

                            };
                            battles.Add(battle);
                        }
                    }
                }
            }
            foreach (Battle battle in battles)
            {
                Matches.Items.Add(battle);
            }
            return battles;
        }

        private void Matches_DoubleClick(object sender, EventArgs e)
        {
            Battle b = (Battle)Matches.SelectedItem;

            MatchData md = new MatchData()
            {
                Frames = b.Replay,
                PlayerNames = new[] { b.Team1.Name, b.Team2.Name },



            };

            Viewer viewer = new Viewer(md);
            viewer.ShowDialog();
        }
    }
}
