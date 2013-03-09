using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

using ShadowBot;
using MyWoW;
using MyWoW.Helpers;
using MyWoW.ObjectManager;


namespace FuzzyMod.Mods {
    public partial class ParanoiaMod : UserControl, Mod {
        private int logoutTime = 10;

		private bool accusedBotting = false;
		private bool whisperReceived = false;
		private static LinkedList<Chat.ChatMessageStruct> accusedBottingMessages = new LinkedList<Chat.ChatMessageStruct>();

        private bool samePlayerTargeting = false;
        private int samePlayerTargetingSeconds = 30;
        private bool samePlayerTargetingIncludeOpposingFaction = false;
		private Dictionary<ulong, int> playersTargetingMe = new Dictionary<ulong, int>();

		private bool samePlayerIsGankingMe = false;
		private int samePlayerIsGankingMeTimes = 3;
		private bool samePlayerIsGankingMeDead = false;
		private Dictionary<ulong, int> playersKilledMe = new Dictionary<ulong, int>();

		private bool beenPortedAway = false;
		private MyWoW.Classes.Position beenPortedAwayLastPosition = null;


		private volatile bool threadRunning = false;
        private Thread thread;
        private int logoutTick = 0;

        private bool isLogginIn = false;
        private bool isLogginOut = false;
		private bool isLoggedOut = false;

		private bool isLoadingSettings = false;


        public ParanoiaMod() {
            InitializeComponent();

			LoadSettings();
			
            thread = new Thread(new ThreadStart(ThreadMain));
            MyWoW.Helpers.Chat.Event_OnNewMessage += new Chat.OnMessageEventHandler(NewMessage);
        }

        ~ParanoiaMod() {
			SaveSettings();

            MyWoW.Helpers.Chat.Event_OnNewMessage -= new Chat.OnMessageEventHandler(NewMessage);
            StopThread();
        }

        public void OnBotStart(object sender, EventArgs args) {
            if(isLogginIn) {
                isLogginIn = false;
                return;
            }

			ResetAllConditions();

            if(!thread.IsAlive)
                StartThread();
        }

        public void OnBotStop(object sender, EventArgs args) {
            if(isLogginOut) {
                isLogginOut = false;
                return;
            }

            StopThread();
		}

		private void SaveSettings() {
			Plugin.ini.IniWriteValue(DisplayName, "logoutTime", logoutTime.ToString());

			Plugin.ini.IniWriteValue(DisplayName, "accusedBotting", accusedBotting.ToString());
			Plugin.ini.IniWriteValue(DisplayName, "whisperReceived", whisperReceived.ToString());


			Plugin.ini.IniWriteValue(DisplayName, "samePlayerTargeting", samePlayerTargeting.ToString());
			Plugin.ini.IniWriteValue(DisplayName, "samePlayerTargetingSeconds", samePlayerTargetingSeconds.ToString());
			Plugin.ini.IniWriteValue(DisplayName, "samePlayerTargetingIncludeOpposingFaction", samePlayerTargetingIncludeOpposingFaction.ToString());

			Plugin.ini.IniWriteValue(DisplayName, "samePlayerIsGankingMe", samePlayerIsGankingMe.ToString());
			Plugin.ini.IniWriteValue(DisplayName, "samePlayerIsGankingMeTimes", samePlayerIsGankingMeTimes.ToString());

			Plugin.ini.IniWriteValue(DisplayName, "beenPortedAway", beenPortedAway.ToString());

			int radioButton = 0;
			if(radStopBot.Checked) {
				radioButton = 1;
			} else if(radLogout.Checked) {
				radioButton = 2;
			}
			Plugin.ini.IniWriteValue(DisplayName, "panicReaction", radioButton.ToString());
		}

		private void LoadSettings() {
			isLoadingSettings = true;
			try {
				logoutTime = int.Parse(Plugin.ini.IniReadValue(DisplayName, "logoutTime"));

				accusedBotting = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "accusedBotting"));
				whisperReceived = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "whisperReceived"));

				samePlayerTargeting = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "samePlayerTargeting"));
				samePlayerTargetingSeconds = int.Parse(Plugin.ini.IniReadValue(DisplayName, "samePlayerTargetingSeconds"));
				samePlayerTargetingIncludeOpposingFaction = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "samePlayerTargetingIncludeOpposingFaction"));

				samePlayerIsGankingMe = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "samePlayerIsGankingMe"));
				samePlayerIsGankingMeTimes = int.Parse(Plugin.ini.IniReadValue(DisplayName, "samePlayerIsGankingMeTimes"));

				beenPortedAway = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "beenPortedAway"));

				
				int radioButton = int.Parse(Plugin.ini.IniReadValue(DisplayName, "panicReaction"));
				switch(radioButton) {
					case 1:
						radStopBot.Checked = true;
						break;
					case 2:
						radLogout.Checked = true;
						break;
					default:
						radNothing.Checked = true;
						break;
				}
			} catch(Exception e) {
				Log("Caught exception on LoadSettings(): " + e.Message);
			}
			ResetValues();
			isLoadingSettings = false;
		}

        public string DisplayName {
            get { return "Paranoia"; }
        }

        public UserControl Control {
            get { return this; }
        }

        private void Log(String message) {
            Console.WriteLine("[Paranoia] " + message);
        }

        private void btnTest_Click(object sender, EventArgs e) {
            Logout();
        }

        private void btnStartBotNow_Click(object sender, EventArgs e) {
            Login();
        }

		private void ValuesEdited(object sender, EventArgs e) {
			if(isLoadingSettings)
				return;
            try {
				samePlayerTargeting = chkSamePlayerTargeting.Checked;
				samePlayerTargetingIncludeOpposingFaction = chkSamePlayerTargetingIncludeOppositeFaction.Checked;
				samePlayerTargetingSeconds = int.Parse(txtSamePlayerTargetingSeconds.Text);

				accusedBotting = chkWhisper.Checked;
				whisperReceived = chkWhisperAnything.Checked;

                samePlayerIsGankingMe = chkBeenKilled.Checked;
                samePlayerIsGankingMeTimes = int.Parse(txtGotKilledTimes.Text);

				beenPortedAway = chkPortedByGM.Checked;

                logoutTime = int.Parse(txtLogoutTime.Text);
            } catch(Exception) {
                MessageBox.Show("Error parsing text", "Parsing Error");
				ResetValues();
            }

			SaveSettings();
        }

		private void ResetValues() {
			chkSamePlayerTargeting.Checked = samePlayerTargeting;
			chkSamePlayerTargetingIncludeOppositeFaction.Checked = samePlayerTargetingIncludeOpposingFaction;
			txtSamePlayerTargetingSeconds.Text = samePlayerTargetingSeconds.ToString();

			chkWhisper.Checked = accusedBotting;
			chkWhisperAnything.Checked = whisperReceived;

			chkBeenKilled.Checked = samePlayerIsGankingMe;
			txtGotKilledTimes.Text = samePlayerIsGankingMeTimes.ToString();

			chkPortedByGM.Checked = beenPortedAway;

			txtLogoutTime.Text = logoutTime.ToString();
		}



        public void StartThread() {
			threadRunning = true;
            thread.Start();
        }
        public void StopThread() {
            try {
				threadRunning = false;
                thread.Join();
            } catch (Exception) {
				try {
					thread.Abort();
				} catch(Exception) {} 
			}
            thread = new Thread(new ThreadStart(ThreadMain));
        }

        public void ThreadMain() {
			while(threadRunning) {
                // If bot isn't running then we aren't logged in
                if(API.Bot.Overrides.FiniteStateMachine.Engine.Running) {
					bool conditionToPanic = false;

                    // Check conditions
					CheckForAccuses(ref conditionToPanic);
					CheckForPlayersTargetingMe(ref conditionToPanic);
					CheckForPlayersKilledMe(ref conditionToPanic);
					CheckForPortedAway(ref conditionToPanic);

					if(conditionToPanic && !ObjectManager.Me.InCombat) {
						Log("Panicking...");

						if(chkHearthstone.Checked) {
							Log("I'm hearthing the hell out of here!");
							MyWoW.Helpers.Movements.StopMove();
							API.Bot.Overrides.FiniteStateMachine.Engine.StopEngine();
							MyWoW.Helpers.Inventory.UseItemByName("Hearthstone");
							Thread.Sleep(11000);
						}

                        if(radLogout.Checked) {
							Log("I'm logging out!");
							Logout();
							ResetAllConditions();
						} else if(radStopBot.Checked) {
							Log("I'm stopping the bot!");
							API.Bot.Stop();
							ResetAllConditions();
                            break;
						}

                        Thread.Sleep(10000);
                    }

                } else {
                    if(radLogout.Checked && isLoggedOut ) {
                        // Bot isn't running, but we are enabled and running
                        // therefor we are waiting to be able to login
                        int waitTimeInMilliseconds = logoutTime * 60 * 1000;
                        if(logoutTick + waitTimeInMilliseconds <= Environment.TickCount) {
                            Login();
                            Thread.Sleep(5000);
                        } else {
                            int millis = (logoutTick + waitTimeInMilliseconds) - Environment.TickCount;
                            int seconds = millis / 1000;
                            int minutes = seconds / 60;

                            string text = "Starting bot in " + (seconds > 60 ? minutes.ToString() + " minutes..." : seconds.ToString() + " seconds...");

                            lblStartingBot.Text = text;
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

		void ResetAllConditions() {
			playersTargetingMe.Clear();
			playersKilledMe.Clear();
			accusedBottingMessages.Clear();
		}

		void CheckForPortedAway(ref bool condition) {
			if(!beenPortedAway)
				return;

			if(beenPortedAwayLastPosition == null) {
				beenPortedAwayLastPosition = ObjectManager.Me.Position.Clone();
			}

			double distance = beenPortedAwayLastPosition.Distance2D(ObjectManager.Me.Position);
			
			// distance 60 is an arbitrary big number to count in charge, blink, death grip, etc...
			// we might in the future need to do more advanced check
			if(distance > 60 && ObjectManager.Me.IsAlive) { 
				condition = true;
				Log("I've just been teleported away");
			}

			beenPortedAwayLastPosition = ObjectManager.Me.Position.Clone();

		}

        void CheckForAccuses(ref bool condition) {
			if(!accusedBotting)
                return;

			string[] words = new string[] { "bot", "cheat", "hack", "auto", "report" };

			foreach(Chat.ChatMessageStruct msg in accusedBottingMessages) {
                Log("Msg: " + msg.Message);
				foreach(string word in words) {
					if(msg.Message.ToLower().IndexOf(word) > 0) {
						Log("Someone is accusing us of botting");
						condition = true;
						break;
					}
					if(whisperReceived && msg.Type == Chat.ChatType.WHISPER_FROM) {
						Log("Someone whispered us");
						condition = true;
						break;
					}
				}
            }
			accusedBottingMessages.Clear();
        }

		void CheckForPlayersTargetingMe(ref bool condition) {
			if(!samePlayerTargeting)
				return;

			const int range = 100;
			List<WowPlayer> playersAroundMe = ObjectManager.GetPlayersAroundPosition(ObjectManager.Me.Position, range);

			// Remove players that is no longer in the vicinity or targeting me
			List<ulong> guids = new List<ulong>(playersTargetingMe.Keys);
			foreach(ulong guid in guids) {
				WowPlayer p = playersAroundMe.Find(x => x.GUID == guid);
				if(p == null || p.TargetGUID != ObjectManager.Me.GUID || p.InCombatWithOtherPlayer) {
					playersTargetingMe.Remove(guid);
				}
			}

			// Add players that are targeting me
			foreach(WowPlayer p in playersAroundMe) {
				if(( p.IsHorde != ObjectManager.Me.IsHorde ) && !samePlayerTargetingIncludeOpposingFaction)
					continue;

				if(p.TargetGUID == ObjectManager.Me.GUID) {
					if(!playersTargetingMe.ContainsKey(p.GUID)) {
						playersTargetingMe.Add(p.GUID, Environment.TickCount);
					}
				}
			}

			// Do we meet the conditions?
			foreach(KeyValuePair<ulong, int> pair in playersTargetingMe) {
				if(Environment.TickCount - pair.Value >= samePlayerTargetingSeconds * 1000) {
					condition = true;
					Log("Someone has been staring at me for too long.");
					break;
				}
			}
		}

		void CheckForPlayersKilledMe(ref bool condition) {
			if(!samePlayerIsGankingMe)
				return;

			if(ObjectManager.Me.IsAlive) {
				samePlayerIsGankingMeDead = false;
				return;
			}

			if(samePlayerIsGankingMeDead)
				return;

			const int range = 40;
			List<WowPlayer> playersAroundMe = ObjectManager.GetPlayersAroundPosition(ObjectManager.Me.Position, range);
			
			foreach(WowPlayer p in playersAroundMe) {
				if(p.TargetGUID == ObjectManager.Me.GUID) {
					if(playersKilledMe.ContainsKey(p.GUID)) {
						playersKilledMe[p.GUID]++;
						Log("Was killed by " + p.Name + "(" + p.Level + "), he has killed me " + playersKilledMe[p.GUID] + " times");
					} else {
						playersKilledMe.Add(p.GUID, 1);
						Log("Was killed by " + p.Name + "(" + p.Level + ") for the first time");
					}

					if(playersKilledMe[p.GUID] >= samePlayerIsGankingMeTimes) {
						condition = true;
						Log("This guy has been killing me too many times.");
						break;
					}
				}
			}

			samePlayerIsGankingMeDead = true;
		}
					

        void NewMessage(Object obj, Chat.OnMessageEventArgs args) {
            Chat.ChatMessageStruct e = args.EventMessage;
            //Log("Msg: " + e.Message);
            if(e.Type == Chat.ChatType.WHISPER_FROM || e.Type == Chat.ChatType.SAY)
				accusedBottingMessages.AddLast(e);
        }

        void Logout() {
            isLogginOut = true;

			isLoggedOut = true;

            API.Bot.Overrides.FiniteStateMachine.Engine.StopEngine();
            logoutTick = Environment.TickCount;

            UIFrame.Update();

            UIFrame.Frame GameMenuFrame = UIFrame.GetFrameByName("GameMenuFrame");

            while (!GameMenuFrame.IsVisible) {
                MyWoW.Helpers.Keybindings.UseBinding("TOGGLEGAMEMENU");
                Thread.Sleep(1000);
                UIFrame.Update();
            }

            UIFrame.Frame GameMenuButtonLogout = UIFrame.GetFrameByName("GameMenuButtonLogout");
            GameMenuButtonLogout.LeftClick();

            lblStartingBot.Text = "Logging out...";
            lblStartingBot.Visible = true;
            btnStartBotNow.Visible = true;
        }

        void Login() {
            // Rely on Relogger, we just start the bot again
            isLogginIn = true;
			isLoggedOut = false;

            API.Bot.Overrides.FiniteStateMachine.Engine.StartEngine();

            lblStartingBot.Visible = false;
            btnStartBotNow.Visible = false;
        }
    }
}
