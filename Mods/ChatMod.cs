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

	public partial class ChatMod : UserControl, Mod {
        private Thread thread;
        private bool isChatOpen = false;

        public ChatMod() {
            InitializeComponent();

			LoadSettings();

            thread = new Thread(new ThreadStart(ThreadMain));
            thread.Start();
        }

        ~ChatMod() {
			SaveSettings();
            try {
                thread.Abort();
            } catch (Exception) {}
        }

        public void OnBotStart(object sender, EventArgs args) {}
        public void OnBotStop(object sender, EventArgs args) {}

		public void SaveSettings() {
			Plugin.ini.IniWriteValue(DisplayName, "pauseWhenChatting", chkPauseWhenChat.Checked.ToString());
		}
		public void LoadSettings() {
			try {
				chkPauseWhenChat.Checked = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "pauseWhenChatting"));
			} catch(Exception) {
				chkPauseWhenChat.Checked = false;
			}
		}

        public string DisplayName {
            get { return "Chat"; }
        }

        public UserControl Control {
            get { return this; }
        }

        private void Log(String message) {
            Console.WriteLine("[Chat] " + message);
        }

        public void ThreadMain() {
            while(true) {
                if(chkPauseWhenChat.Checked) {
					if(Keyboard.IsChatboxOpened && !isChatOpen && !Mailbox.IsMailboxOpen) {
                        string currentState = API.Bot.Overrides.FiniteStateMachine.Engine.CurrentState;
                            
                        string[] disableChatStates = new string[] { "Moving", "Combat", "Loot", "SkinAround", "Mount" };

                        if (API.Bot.Overrides.FiniteStateMachine.Engine.Running /*&& disableChatStates.Contains(currentState)*/) {
                            // Pause
                            Log("Chatting - Pausing bot");
							isChatOpen = true; 
							//MyWoW.Helpers.Movements.StopMove();
                            API.Bot.Stop();
							Thread.Sleep(1000);
                        }

                    } else if(!Keyboard.IsChatboxOpened && isChatOpen) {
                        // unpause
                        Log("Done chatting - Resuming bot");
                        isChatOpen = false;
						API.Bot.Start();
						Thread.Sleep(1000);
						if(!API.Bot.Overrides.FiniteStateMachine.Engine.Running)
							API.Bot.Overrides.FiniteStateMachine.Engine.StartEngine();
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}
