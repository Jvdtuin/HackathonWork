﻿namespace RankRunner
{
    partial class RankingRunner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Compettitors = new System.Windows.Forms.ListBox();
            this.Matches = new System.Windows.Forms.ListBox();
            this.ApplicationTb = new System.Windows.Forms.TextBox();
            this.TeamNameTb = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbRules = new System.Windows.Forms.ComboBox();
            this.lbRanking = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Compettitors
            // 
            this.Compettitors.FormattingEnabled = true;
            this.Compettitors.ItemHeight = 16;
            this.Compettitors.Location = new System.Drawing.Point(23, 215);
            this.Compettitors.Margin = new System.Windows.Forms.Padding(4);
            this.Compettitors.Name = "Compettitors";
            this.Compettitors.Size = new System.Drawing.Size(500, 180);
            this.Compettitors.TabIndex = 0;
            // 
            // Matches
            // 
            this.Matches.FormattingEnabled = true;
            this.Matches.ItemHeight = 16;
            this.Matches.Location = new System.Drawing.Point(532, 32);
            this.Matches.Margin = new System.Windows.Forms.Padding(4);
            this.Matches.Name = "Matches";
            this.Matches.Size = new System.Drawing.Size(432, 580);
            this.Matches.TabIndex = 1;
            this.Matches.SelectedIndexChanged += new System.EventHandler(this.Matches_SelectedIndexChanged);
            this.Matches.DoubleClick += new System.EventHandler(this.Matches_DoubleClick);
            // 
            // ApplicationTb
            // 
            this.ApplicationTb.Location = new System.Drawing.Point(115, 112);
            this.ApplicationTb.Margin = new System.Windows.Forms.Padding(4);
            this.ApplicationTb.Name = "ApplicationTb";
            this.ApplicationTb.Size = new System.Drawing.Size(353, 22);
            this.ApplicationTb.TabIndex = 2;
            // 
            // TeamNameTb
            // 
            this.TeamNameTb.Location = new System.Drawing.Point(115, 80);
            this.TeamNameTb.Margin = new System.Windows.Forms.Padding(4);
            this.TeamNameTb.Name = "TeamNameTb";
            this.TeamNameTb.Size = new System.Drawing.Size(408, 22);
            this.TeamNameTb.TabIndex = 2;
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(477, 112);
            this.browse.Margin = new System.Windows.Forms.Padding(4);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(47, 25);
            this.browse.TabIndex = 3;
            this.browse.Text = "...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 117);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Program:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Team name";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(424, 144);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 6;
            this.button2.Text = "AddTeam";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(424, 555);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 7;
            this.button3.Text = "Run battles";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Rules";
            // 
            // cbRules
            // 
            this.cbRules.FormattingEnabled = true;
            this.cbRules.Items.AddRange(new object[] {
            "Round 1",
            "Round 2",
            "Round 3"});
            this.cbRules.Location = new System.Drawing.Point(115, 12);
            this.cbRules.Name = "cbRules";
            this.cbRules.Size = new System.Drawing.Size(121, 24);
            this.cbRules.TabIndex = 9;
            // 
            // lbRanking
            // 
            this.lbRanking.FormattingEnabled = true;
            this.lbRanking.ItemHeight = 16;
            this.lbRanking.Location = new System.Drawing.Point(988, 32);
            this.lbRanking.Name = "lbRanking";
            this.lbRanking.Size = new System.Drawing.Size(357, 276);
            this.lbRanking.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(529, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Battles";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(985, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Ranking";
            // 
            // RankingRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1512, 625);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbRanking);
            this.Controls.Add(this.cbRules);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.TeamNameTb);
            this.Controls.Add(this.ApplicationTb);
            this.Controls.Add(this.Matches);
            this.Controls.Add(this.Compettitors);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RankingRunner";
            this.Text = "Ranking runner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbRules;
        private System.Windows.Forms.ListBox lbRanking;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }

    #endregion
}


