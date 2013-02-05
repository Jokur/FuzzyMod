namespace FuzzyMod.Mods
{
    partial class ParanoiaMod
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radHearthstone = new System.Windows.Forms.RadioButton();
			this.radNothing = new System.Windows.Forms.RadioButton();
			this.radStopBot = new System.Windows.Forms.RadioButton();
			this.radLogout = new System.Windows.Forms.RadioButton();
			this.btnStartBotNow = new System.Windows.Forms.Button();
			this.lblStartingBot = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtGotKilledTimes = new System.Windows.Forms.TextBox();
			this.chkBeenKilled = new System.Windows.Forms.CheckBox();
			this.chkWhisper = new System.Windows.Forms.CheckBox();
			this.chkSamePlayerTargetingIncludeOppositeFaction = new System.Windows.Forms.CheckBox();
			this.txtSamePlayerTargetingSeconds = new System.Windows.Forms.TextBox();
			this.chkSamePlayerTargeting = new System.Windows.Forms.CheckBox();
			this.btnTest = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLogoutTime = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radHearthstone);
			this.groupBox1.Controls.Add(this.radNothing);
			this.groupBox1.Controls.Add(this.radStopBot);
			this.groupBox1.Controls.Add(this.radLogout);
			this.groupBox1.Controls.Add(this.btnStartBotNow);
			this.groupBox1.Controls.Add(this.lblStartingBot);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtGotKilledTimes);
			this.groupBox1.Controls.Add(this.chkBeenKilled);
			this.groupBox1.Controls.Add(this.chkWhisper);
			this.groupBox1.Controls.Add(this.chkSamePlayerTargetingIncludeOppositeFaction);
			this.groupBox1.Controls.Add(this.txtSamePlayerTargetingSeconds);
			this.groupBox1.Controls.Add(this.chkSamePlayerTargeting);
			this.groupBox1.Controls.Add(this.btnTest);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtLogoutTime);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(592, 317);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// radHearthstone
			// 
			this.radHearthstone.AutoSize = true;
			this.radHearthstone.Location = new System.Drawing.Point(26, 222);
			this.radHearthstone.Name = "radHearthstone";
			this.radHearthstone.Size = new System.Drawing.Size(185, 17);
			this.radHearthstone.TabIndex = 18;
			this.radHearthstone.TabStop = true;
			this.radHearthstone.Text = "Stop the bot and use Hearthstone";
			this.radHearthstone.UseVisualStyleBackColor = true;
			this.radHearthstone.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// radNothing
			// 
			this.radNothing.AutoSize = true;
			this.radNothing.Checked = true;
			this.radNothing.Location = new System.Drawing.Point(26, 245);
			this.radNothing.Name = "radNothing";
			this.radNothing.Size = new System.Drawing.Size(127, 17);
			this.radNothing.TabIndex = 17;
			this.radNothing.TabStop = true;
			this.radNothing.Text = "Do nothing (Disabled)";
			this.radNothing.UseVisualStyleBackColor = true;
			this.radNothing.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// radStopBot
			// 
			this.radStopBot.AutoSize = true;
			this.radStopBot.Location = new System.Drawing.Point(26, 199);
			this.radStopBot.Name = "radStopBot";
			this.radStopBot.Size = new System.Drawing.Size(83, 17);
			this.radStopBot.TabIndex = 16;
			this.radStopBot.Text = "Stop the bot";
			this.radStopBot.UseVisualStyleBackColor = true;
			this.radStopBot.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// radLogout
			// 
			this.radLogout.AutoSize = true;
			this.radLogout.Location = new System.Drawing.Point(26, 176);
			this.radLogout.Name = "radLogout";
			this.radLogout.Size = new System.Drawing.Size(76, 17);
			this.radLogout.TabIndex = 15;
			this.radLogout.Text = "Log out for";
			this.radLogout.UseVisualStyleBackColor = true;
			this.radLogout.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// btnStartBotNow
			// 
			this.btnStartBotNow.Location = new System.Drawing.Point(204, 282);
			this.btnStartBotNow.Name = "btnStartBotNow";
			this.btnStartBotNow.Size = new System.Drawing.Size(107, 23);
			this.btnStartBotNow.TabIndex = 14;
			this.btnStartBotNow.Text = "Start Bot NOW!";
			this.btnStartBotNow.UseVisualStyleBackColor = true;
			this.btnStartBotNow.Visible = false;
			this.btnStartBotNow.Click += new System.EventHandler(this.btnStartBotNow_Click);
			// 
			// lblStartingBot
			// 
			this.lblStartingBot.AutoSize = true;
			this.lblStartingBot.Location = new System.Drawing.Point(23, 287);
			this.lblStartingBot.Name = "lblStartingBot";
			this.lblStartingBot.Size = new System.Drawing.Size(84, 13);
			this.lblStartingBot.TabIndex = 13;
			this.lblStartingBot.Text = "Starting bot in ...";
			this.lblStartingBot.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(266, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "seconds";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(183, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(122, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "times by the same player";
			// 
			// txtGotKilledTimes
			// 
			this.txtGotKilledTimes.Location = new System.Drawing.Point(151, 86);
			this.txtGotKilledTimes.Name = "txtGotKilledTimes";
			this.txtGotKilledTimes.Size = new System.Drawing.Size(26, 20);
			this.txtGotKilledTimes.TabIndex = 10;
			this.txtGotKilledTimes.Text = "3";
			this.txtGotKilledTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtGotKilledTimes.TextChanged += new System.EventHandler(this.ValuesEdited);
			// 
			// chkBeenKilled
			// 
			this.chkBeenKilled.AutoSize = true;
			this.chkBeenKilled.Location = new System.Drawing.Point(26, 88);
			this.chkBeenKilled.Name = "chkBeenKilled";
			this.chkBeenKilled.Size = new System.Drawing.Size(129, 17);
			this.chkBeenKilled.TabIndex = 9;
			this.chkBeenKilled.Text = "When I\'ve been killed";
			this.chkBeenKilled.UseVisualStyleBackColor = true;
			this.chkBeenKilled.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// chkWhisper
			// 
			this.chkWhisper.AutoSize = true;
			this.chkWhisper.Location = new System.Drawing.Point(26, 19);
			this.chkWhisper.Name = "chkWhisper";
			this.chkWhisper.Size = new System.Drawing.Size(208, 17);
			this.chkWhisper.TabIndex = 8;
			this.chkWhisper.Text = "When someone accuses me of botting";
			this.chkWhisper.UseVisualStyleBackColor = true;
			this.chkWhisper.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// chkSamePlayerTargetingIncludeOppositeFaction
			// 
			this.chkSamePlayerTargetingIncludeOppositeFaction.AutoSize = true;
			this.chkSamePlayerTargetingIncludeOppositeFaction.Location = new System.Drawing.Point(47, 65);
			this.chkSamePlayerTargetingIncludeOppositeFaction.Name = "chkSamePlayerTargetingIncludeOppositeFaction";
			this.chkSamePlayerTargetingIncludeOppositeFaction.Size = new System.Drawing.Size(139, 17);
			this.chkSamePlayerTargetingIncludeOppositeFaction.TabIndex = 7;
			this.chkSamePlayerTargetingIncludeOppositeFaction.Text = "Include opposite faction";
			this.chkSamePlayerTargetingIncludeOppositeFaction.UseVisualStyleBackColor = true;
			this.chkSamePlayerTargetingIncludeOppositeFaction.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// txtSamePlayerTargetingSeconds
			// 
			this.txtSamePlayerTargetingSeconds.Location = new System.Drawing.Point(232, 40);
			this.txtSamePlayerTargetingSeconds.Name = "txtSamePlayerTargetingSeconds";
			this.txtSamePlayerTargetingSeconds.Size = new System.Drawing.Size(28, 20);
			this.txtSamePlayerTargetingSeconds.TabIndex = 6;
			this.txtSamePlayerTargetingSeconds.Text = "30";
			this.txtSamePlayerTargetingSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSamePlayerTargetingSeconds.TextChanged += new System.EventHandler(this.ValuesEdited);
			// 
			// chkSamePlayerTargeting
			// 
			this.chkSamePlayerTargeting.AutoSize = true;
			this.chkSamePlayerTargeting.Location = new System.Drawing.Point(26, 42);
			this.chkSamePlayerTargeting.Name = "chkSamePlayerTargeting";
			this.chkSamePlayerTargeting.Size = new System.Drawing.Size(208, 17);
			this.chkSamePlayerTargeting.TabIndex = 5;
			this.chkSamePlayerTargeting.Text = "When same player has targeted me for";
			this.chkSamePlayerTargeting.UseVisualStyleBackColor = true;
			this.chkSamePlayerTargeting.Click += new System.EventHandler(this.ValuesEdited);
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(423, 171);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(61, 26);
			this.btnTest.TabIndex = 4;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Visible = false;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(148, 178);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(253, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "minutes (requires Relogger for automatic login again)";
			// 
			// txtLogoutTime
			// 
			this.txtLogoutTime.Location = new System.Drawing.Point(108, 175);
			this.txtLogoutTime.Name = "txtLogoutTime";
			this.txtLogoutTime.Size = new System.Drawing.Size(34, 20);
			this.txtLogoutTime.TabIndex = 2;
			this.txtLogoutTime.Text = "10";
			this.txtLogoutTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtLogoutTime.TextChanged += new System.EventHandler(this.ValuesEdited);
			// 
			// ParanoiaMod
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "ParanoiaMod";
			this.Size = new System.Drawing.Size(598, 323);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btnStartBotNow;
        public System.Windows.Forms.Label lblStartingBot;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGotKilledTimes;
        private System.Windows.Forms.CheckBox chkBeenKilled;
        private System.Windows.Forms.CheckBox chkWhisper;
        private System.Windows.Forms.CheckBox chkSamePlayerTargetingIncludeOppositeFaction;
        private System.Windows.Forms.TextBox txtSamePlayerTargetingSeconds;
        private System.Windows.Forms.CheckBox chkSamePlayerTargeting;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLogoutTime;
        private System.Windows.Forms.RadioButton radLogout;
        private System.Windows.Forms.RadioButton radNothing;
        private System.Windows.Forms.RadioButton radStopBot;
        private System.Windows.Forms.RadioButton radHearthstone;

    }
}
