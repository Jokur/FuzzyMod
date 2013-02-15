namespace FuzzyMod.Mods
{
	partial class CameraMod
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
			this.txtCameraAngle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkCameraAngle = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtCameraAngle);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.chkCameraAngle);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(592, 317);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// txtCameraAngle
			// 
			this.txtCameraAngle.Location = new System.Drawing.Point(127, 17);
			this.txtCameraAngle.Name = "txtCameraAngle";
			this.txtCameraAngle.Size = new System.Drawing.Size(36, 20);
			this.txtCameraAngle.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(169, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(255, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "degree angle (Must recheck box if value is changed)";
			// 
			// chkCameraAngle
			// 
			this.chkCameraAngle.AutoSize = true;
			this.chkCameraAngle.Location = new System.Drawing.Point(18, 19);
			this.chkCameraAngle.Name = "chkCameraAngle";
			this.chkCameraAngle.Size = new System.Drawing.Size(113, 17);
			this.chkCameraAngle.TabIndex = 0;
			this.chkCameraAngle.Text = "Keep camera at a ";
			this.chkCameraAngle.UseVisualStyleBackColor = true;
			this.chkCameraAngle.CheckedChanged += new System.EventHandler(this.chkCameraAngle_CheckedChanged);
			// 
			// CameraMod
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "CameraMod";
			this.Size = new System.Drawing.Size(598, 323);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtCameraAngle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkCameraAngle;

    }
}
