using GalaxyConquest.Properties;
using HackathonWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
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
            cbLevel.SelectedIndex = 0;
            Leage.SelectedIndex = 0;
		}

		private void Setup_Load(object sender, EventArgs e)
		{

		}

		private void Debug()
		{
			MessageBox.Show("You can attach your debugger now");
		}

        private void button1_Click(object sender, EventArgs e)
        {

            string filePath = textBox1.Text;

            string opponentAI = "";
            switch ( cbLevel.SelectedIndex)
            {
                case 0: opponentAI = ConfigurationManager.AppSettings["AI0"]; break;
                case 1: opponentAI = ConfigurationManager.AppSettings["AI1"]; break;
                case 2: opponentAI = ConfigurationManager.AppSettings["AI2"]; break;                         
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


            //Settings.Seed = 0;
            //Settings.FactoryCount = 5;
            //Settings.InitalUnitcount = 30;

            Referee referee = new Referee(players);
            HackathonWork.Settings.Timeout = -1;
            int seed;
            if (int.TryParse(SeedTb.Text, out seed))
            {
                referee.Seed = seed;
            }

            
            DebugBreak debugMethod = null;
            if (chbxDebug.Checked)
            {
                debugMethod = this.Debug;
            }
            try
            {
                referee.PlayGame(debugMethod);
            }
            catch (Exception ex)
            {
                // who throw the exception?
            }

            List<Frame> frames = referee.GetFrames();
            
            MatchData md = new MatchData()
            {
                Frames = frames,
                PlayerNames = playerNames,
            };

			Viewer v = new Viewer(md);
			v.Show();

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
