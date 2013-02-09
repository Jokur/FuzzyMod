namespace FuzzyMod.Mods
{
    partial class TempMod
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
			this.chkFixChoppyMovement = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkFixChoppyMovement);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(592, 317);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// chkFixChoppyMovement
			// 
			this.chkFixChoppyMovement.AutoSize = true;
			this.chkFixChoppyMovement.Location = new System.Drawing.Point(26, 19);
			this.chkFixChoppyMovement.Name = "chkFixChoppyMovement";
			this.chkFixChoppyMovement.Size = new System.Drawing.Size(266, 17);
			this.chkFixChoppyMovement.TabIndex = 5;
			this.chkFixChoppyMovement.Text = "Fix choppy movement (unchecking requires restart)";
			this.chkFixChoppyMovement.UseVisualStyleBackColor = true;
			this.chkFixChoppyMovement.CheckedChanged += new System.EventHandler(this.chkFixChoppyMovement_CheckedChanged);
			// 
			// TempMod
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "TempMod";
			this.Size = new System.Drawing.Size(598, 323);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFixChoppyMovement;

    }
}
