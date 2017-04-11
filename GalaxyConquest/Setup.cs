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

		private void button1_Click(object sender, EventArgs e)
		{
			string applicationName = "ConsoleApplication1";
			string filePath = $".\\ConsoleApplication1.exe";

			HackathonWork.Settings.SetLeageLevel(1);

			//Settings.Seed = 0;
			//Settings.FactoryCount = 5;
			//Settings.InitalUnitcount = 30;
			HackathonWork.Settings.Timeout = -1; // unlimitid 
			Referee referee = new Referee(new string[] { ".\\ConsoleApplication1.exe", ".\\ConsoleApplication3.exe" });

			referee.PlayGame();

			List<Frame> frames = referee.GetFrames();

			Viewer v = new Viewer(frames);
			v.Show();

		}
	}
}
