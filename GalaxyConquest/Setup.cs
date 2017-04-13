using GalaxyConquest.Properties;
using HackathonWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

			string[] players = new string[2];
			if (rbtnBlue.Checked)
			{
				players[0] = filePath;
				players[1] = "ConsoleApplication1.exe";
			}
			else
			{
				players[1] = filePath;
				players[0] = "ConsoleApplication1.exe";
			}

			int index = Leage.SelectedIndex;
			if (index == 2)
			{ index = 3; }

			HackathonWork.Settings.SetLeageLevel(index);

            //Settings.Seed = 0;
            //Settings.FactoryCount = 5;
            //Settings.InitalUnitcount = 30;
            HackathonWork.Settings.MaxRounds = 50; 

			HackathonWork.Settings.Timeout = -1;  

			Referee referee = new Referee(players);
            DebugBreak debugMethod = null;
            if (chbxDebug.Checked)
			{
				debugMethod = this.Debug;
			}
            try
            {
                referee.PlayGame(debugMethod);
            }
            catch
            {
                // who throw the exception?
            }

			List<Frame> frames = referee.GetFrames();
			Viewer v = new Viewer(frames);
			v.Show();

		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{

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
