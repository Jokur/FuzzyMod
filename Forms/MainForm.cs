using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyWoW;
using MyWoW.ObjectManager;
using System.Threading;
using MyWoW.Helpers;
using System.IO;
using FuzzyMod.Mods;
using ShadowBot;

namespace FuzzyMod.Forms {
    public partial class MainForm : Form {
        public MainForm() {
            this.InitializeComponent();
			this.Text = "FuzzyMod v" + Plugin.version + "  - By FuzzyHobo";
        }

        private void MainForm_Load(object sender, EventArgs e) {
            tabControl.TabPages.Clear();
            foreach (Mod mod in Plugin.mods) {
                TabPage page = new TabPage(mod.DisplayName);
                page.Controls.Clear();
                page.Controls.Add(mod.Control);
                tabControl.TabPages.Add(page);
            }
        }
    }
}
