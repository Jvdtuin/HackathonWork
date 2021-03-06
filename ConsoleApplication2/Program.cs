﻿using HackathonWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
		
            string applicationName = "ConsoleApplication1";
            string filePath = $".\\{applicationName}.exe";
          
			//Settings.Seed = 0;
			//Settings.FactoryCount = 5;
			//Settings.InitalUnitcount = 30;
			Settings.Timeout = 1000; // unlimitid 
            Referee referee = new Referee(new string[] { filePath, filePath });

            referee.PlayGame(null);

            List<Frame> frames = referee.GetFrames();

            
        }

        private static Process CreatePlayerProcess(string filename)
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



    }
}
