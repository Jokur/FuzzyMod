using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Threading;

using ShadowBot;
using MyWoW;
using MyWoW.Helpers;
using MyWoW.ObjectManager;
using FuzzyMod.Forms;
using FuzzyMod.Mods;

namespace FuzzyMod
{
    internal class Plugin {
        public static List<Mod> mods;

		public static Ini.IniFile ini = new Ini.IniFile(ShadowBot.Functions.Apps.StartupPath + @"\FuzzyMod.ini");

		public static string version = "1.1.2";
        
        internal static void Initialize() {}

        internal static void OnEnable() {
            mods = new List<Mod>();

            mods.Add(new ParanoiaMod());
            mods.Add(new ChatMod());
			mods.Add(new DisassembleMod());

            if(!Forms.AllForms.main.Visible) {
                Forms.AllForms.main.MdiParent = ShadowBot.Forms.AllForms.MainForm;
                Forms.AllForms.main.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                Forms.AllForms.main.Show();
            }

            API.Bot.Events.OnBotStart += new API.Bot.Events.OnBotStartEventHandler(OnBotStart);
            API.Bot.Events.OnBotStop += new API.Bot.Events.OnBotStopEventHandler(OnBotStop);
        }

        internal static void OnDisable() {
            mods.Clear();


            if(Forms.AllForms.main.Visible) {
                Forms.AllForms.main.Hide();
            }

            API.Bot.Events.OnBotStart -= new API.Bot.Events.OnBotStartEventHandler(OnBotStart);
            API.Bot.Events.OnBotStop -= new API.Bot.Events.OnBotStopEventHandler(OnBotStop);
        }

        internal static void Log(String message) {
            Console.WriteLine("[Fuzzy] " + message);
        }

        internal static void OnBotStart(object sender, EventArgs args) {
            try {
                foreach (Mod mod in mods) {
                    mod.OnBotStart(sender, args);
                }
            } catch (Exception e) {
                Log("OnBotStart");
                Log("Caught exception: " + e.Message);
            }
        }

        internal static void OnBotStop(object sender, EventArgs args) {
            try {
                foreach (Mod mod in mods) {
                    mod.OnBotStop(sender, args);
                }
            } catch (Exception e) {
                Log("OnBotStop");
                Log("Caught exception: " + e.Message);
            }
        }
    }
}
