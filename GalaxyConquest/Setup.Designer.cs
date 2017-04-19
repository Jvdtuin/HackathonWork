namespace GalaxyConquest
{
	partial class Setup
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.chbxDebug = new System.Windows.Forms.CheckBox();
            this.rbtnRed = new System.Windows.Forms.RadioButton();
            this.rbtnBlue = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Leage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SeedTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(682, 108);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Play Game !!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player program";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(431, 22);
            this.textBox1.TabIndex = 2;
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(453, 30);
            this.browse.Margin = new System.Windows.Forms.Padding(4);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(35, 28);
            this.browse.TabIndex = 3;
            this.browse.Text = "...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // chbxDebug
            // 
            this.chbxDebug.AutoSize = true;
            this.chbxDebug.Location = new System.Drawing.Point(13, 65);
            this.chbxDebug.Margin = new System.Windows.Forms.Padding(4);
            this.chbxDebug.Name = "chbxDebug";
            this.chbxDebug.Size = new System.Drawing.Size(126, 21);
            this.chbxDebug.TabIndex = 4;
            this.chbxDebug.Text = "I want to debug";
            this.chbxDebug.UseVisualStyleBackColor = true;
            // 
            // rbtnRed
            // 
            this.rbtnRed.AutoSize = true;
            this.rbtnRed.Location = new System.Drawing.Point(352, 63);
            this.rbtnRed.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnRed.Name = "rbtnRed";
            this.rbtnRed.Size = new System.Drawing.Size(90, 21);
            this.rbtnRed.TabIndex = 8;
            this.rbtnRed.Text = "Team red";
            this.rbtnRed.UseVisualStyleBackColor = true;
            // 
            // rbtnBlue
            // 
            this.rbtnBlue.AutoSize = true;
            this.rbtnBlue.Checked = true;
            this.rbtnBlue.Location = new System.Drawing.Point(229, 65);
            this.rbtnBlue.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnBlue.Name = "rbtnBlue";
            this.rbtnBlue.Size = new System.Drawing.Size(96, 21);
            this.rbtnBlue.TabIndex = 9;
            this.rbtnBlue.TabStop = true;
            this.rbtnBlue.Text = "Team blue";
            this.rbtnBlue.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cbLevel
            // 
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "Level 1",
            "Level 2",
            "Level 3"});
            this.cbLevel.Location = new System.Drawing.Point(621, 30);
            this.cbLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(160, 24);
            this.cbLevel.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(516, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Oponent level";
            // 
            // Leage
            // 
            this.Leage.FormattingEnabled = true;
            this.Leage.Items.AddRange(new object[] {
            "Round 1",
            "Round 2",
            "Round 3"});
            this.Leage.Location = new System.Drawing.Point(621, 74);
            this.Leage.Margin = new System.Windows.Forms.Padding(4);
            this.Leage.Name = "Leage";
            this.Leage.Size = new System.Drawing.Size(160, 24);
            this.Leage.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(516, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Rules";
            // 
            // SeedTb
            // 
            this.SeedTb.Location = new System.Drawing.Point(60, 111);
            this.SeedTb.Margin = new System.Windows.Forms.Padding(4);
            this.SeedTb.Name = "SeedTb";
            this.SeedTb.Size = new System.Drawing.Size(132, 22);
            this.SeedTb.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Seed";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 140);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(769, 386);
            this.richTextBox1.TabIndex = 16;
            this.richTextBox1.Text = "";
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 538);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SeedTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Leage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.rbtnBlue);
            this.Controls.Add(this.rbtnRed);
            this.Controls.Add(this.chbxDebug);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Setup";
            this.Text = "Galaxy Conquest Setup";
            this.Load += new System.EventHandler(this.Setup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button browse;
		private System.Windows.Forms.CheckBox chbxDebug;
		private System.Windows.Forms.RadioButton rbtnRed;
		private System.Windows.Forms.RadioButton rbtnBlue;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ComboBox cbLevel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox Leage;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SeedTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

