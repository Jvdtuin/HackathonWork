namespace RankRunner
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
            this.SuspendLayout();
            // 
            // Compettitors
            // 
            this.Compettitors.FormattingEnabled = true;
            this.Compettitors.Location = new System.Drawing.Point(17, 175);
            this.Compettitors.Name = "Compettitors";
            this.Compettitors.Size = new System.Drawing.Size(376, 147);
            this.Compettitors.TabIndex = 0;
            // 
            // Matches
            // 
            this.Matches.FormattingEnabled = true;
            this.Matches.Location = new System.Drawing.Point(458, 12);
            this.Matches.Name = "Matches";
            this.Matches.Size = new System.Drawing.Size(664, 472);
            this.Matches.TabIndex = 1;
            this.Matches.DoubleClick += new System.EventHandler(this.Matches_DoubleClick);
            // 
            // ApplicationTb
            // 
            this.ApplicationTb.Location = new System.Drawing.Point(86, 91);
            this.ApplicationTb.Name = "ApplicationTb";
            this.ApplicationTb.Size = new System.Drawing.Size(266, 20);
            this.ApplicationTb.TabIndex = 2;
            // 
            // TeamNameTb
            // 
            this.TeamNameTb.Location = new System.Drawing.Point(86, 65);
            this.TeamNameTb.Name = "TeamNameTb";
            this.TeamNameTb.Size = new System.Drawing.Size(307, 20);
            this.TeamNameTb.TabIndex = 2;
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(358, 91);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(35, 20);
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
            this.label1.Location = new System.Drawing.Point(14, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Program:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Team name";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(318, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "AddTeam";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(318, 461);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Run battles";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RankingRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 508);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.TeamNameTb);
            this.Controls.Add(this.ApplicationTb);
            this.Controls.Add(this.Matches);
            this.Controls.Add(this.Compettitors);
            this.Name = "RankingRunner";
            this.Text = "Ranking runner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }

    #endregion
}


