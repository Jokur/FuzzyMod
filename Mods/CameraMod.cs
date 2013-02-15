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
using MyWoW.Classes;


namespace FuzzyMod.Mods {

	public partial class CameraMod : UserControl, Mod {

		private double cameraAngle = Math.PI / 4; // 45 degrees default
		private Thread threadAdjustCameraAngle;
		private bool isRunningAdjustCamera;


        public CameraMod() {
            InitializeComponent();

			LoadSettings();

			Mouse.Initialize();
        }

		~CameraMod() {
			SaveSettings();
        }

        public void OnBotStart(object sender, EventArgs args) {}
        public void OnBotStop(object sender, EventArgs args) {}

		public void SaveSettings() {
			Plugin.ini.IniWriteValue(DisplayName, "enableCameraAngle", chkCameraAngle.Checked.ToString());
			Plugin.ini.IniWriteValue(DisplayName, "cameraAngle", cameraAngle.ToString());
		}
		public void LoadSettings() {

			try {
				cameraAngle = double.Parse(Plugin.ini.IniReadValue(DisplayName, "cameraAngle"));
			} catch(Exception) { }
			txtCameraAngle.Text = Math.Floor(RadToDeg(cameraAngle)).ToString();

			try {
				chkCameraAngle.Checked = bool.Parse(Plugin.ini.IniReadValue(DisplayName, "enableCameraAngle"));
			} catch(Exception) {
				chkCameraAngle.Checked = false;
			}
		}

        public string DisplayName {
            get { return "Camera"; }
        }

        public UserControl Control {
            get { return this; }
        }

        private void Log(String message) {
            Console.WriteLine("[Camera] " + message);
        }

		private double RadToDeg(double rad) {
			return (rad / Math.PI) * 180.0;
		}
		private double DegToRad(double deg) {
			return ( deg / 180.0 ) * Math.PI;
		}

		private void chkCameraAngle_CheckedChanged(object sender, EventArgs e) {
			try {
				cameraAngle = DegToRad(double.Parse(txtCameraAngle.Text));
				if(cameraAngle > 90 || cameraAngle < 0)
					throw new Exception();
			} catch(Exception) {
				MessageBox.Show("Parsing Error:\nCamera angle must be between 0-90 degrees (default: 45)");
				txtCameraAngle.Text = Math.Floor(RadToDeg(cameraAngle)).ToString();
				return;
			}

			if(threadAdjustCameraAngle != null) {
				isRunningAdjustCamera = false;
				threadAdjustCameraAngle.Join();
				threadAdjustCameraAngle = null;
			}
			if(chkCameraAngle.Checked) {
				isRunningAdjustCamera = true;
				threadAdjustCameraAngle = new Thread(new ThreadStart(ThreadCameraAdjust));
				threadAdjustCameraAngle.Start();
			}
			SaveSettings();
		}

		private void ThreadCameraAdjust() {
			while(isRunningAdjustCamera) {
				if(API.Bot.Overrides.FiniteStateMachine.Engine.Running) {
					string currentState = API.Bot.Overrides.FiniteStateMachine.Engine.CurrentState;

					string[] disableChatStates = new string[] { "Moving", "Restingb", "Mount" };

					if(disableChatStates.Contains(currentState)) {
						Position cameraPos = new Position(Camera.X, Camera.Y, Camera.Z);
						double angle = cameraPos.AngleVertical();
						const double epsilon = Math.PI * 0.01;

						if(angle > Math.PI) {
							Log("Move camera up, we're too far down");
							MoveCameraUp(50);
						} else if(angle + epsilon < cameraAngle) {
							Log("Move camera up");
							MoveCameraUp(10);
						} else if(angle - epsilon > cameraAngle) {
							Log("Move camera down");
							MoveCameraDown(10);
						}
					}
				}
				Thread.Sleep(1000);
			}
		}

		private void MoveCameraUp(int amount) {
			int width = MyWoW.Helpers.Interface.WindowWidth;
			int height = MyWoW.Helpers.Interface.WindowHeight;
			POINT pDown = new POINT( width/2, height/2 );
			POINT pUp = new POINT(width / 2, height / 2 + amount);
			Mouse.SetCursorPos(pDown);
			Thread.Sleep(50);
			Mouse.LeftDown();
			Thread.Sleep(50);
			Mouse.SetCursorPos(pUp);
			Thread.Sleep(50);
			Mouse.LeftUp();
			Mouse.UnlockCursor();
		}

		private void MoveCameraDown(int amount) {
			int width = MyWoW.Helpers.Interface.WindowWidth;
			int height = MyWoW.Helpers.Interface.WindowHeight;
			POINT pDown = new POINT(width / 2, height / 2);
			POINT pUp = new POINT(width / 2, height / 2 - amount);
			Mouse.SetCursorPos(pDown);
			Thread.Sleep(50);
			Mouse.LeftDown();
			Thread.Sleep(50);
			Mouse.SetCursorPos(pUp);
			Thread.Sleep(50);
			Mouse.LeftUp();
			Mouse.UnlockCursor();
		}
    }
}
