using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using System.Threading;
using System.IO;

using ShadowBot;
using MyWoW;
using MyWoW.Helpers;
using MyWoW.ObjectManager;


namespace FuzzyMod.Mods {

	public partial class TempMod : UserControl, Mod {

		TextWriter oldOut;
		TextWriter oldError;

        public TempMod() {
            InitializeComponent();

			LoadSettings();
        }

		~TempMod() {
			SaveSettings();
        }

        public void OnBotStart(object sender, EventArgs args) {}
        public void OnBotStop(object sender, EventArgs args) {}

		public void SaveSettings() {
			Plugin.ini.IniWriteValue(DisplayName, "fixChoppyMovement", chkFixChoppyMovement.Checked.ToString());
		}
		public void LoadSettings() {
			try {
				chkFixChoppyMovement.Checked = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "fixChoppyMovement"));
			} catch(Exception) {
				chkFixChoppyMovement.Checked = false;
			}
		}

        public string DisplayName {
            get { return "Temp"; }
        }

        public UserControl Control {
            get { return this; }
        }

        private void Log(String message) {
            Console.WriteLine("[Temp] " + message);
        }

		private void chkFixChoppyMovement_CheckedChanged(object sender, EventArgs e) {
			if(chkFixChoppyMovement.Checked) {
				oldOut = Console.Out;
				oldError = Console.Error;
				Console.SetOut(new TempConsole());
				Console.SetError(new TempConsole());
			} else {
				Console.SetOut(oldOut);
				Console.SetError(oldError);
			}
			SaveSettings();
		}
    }

	class TempConsole : TextWriter {

		// Summary:
		//     When overridden in a derived class, returns the System.Text.Encoding in which
		//     the output is written.
		//
		// Returns:
		//     The Encoding in which the output is written.
		public override Encoding Encoding { get { return Encoding.Default; } }
	}
}
