using System;
using System.Collections.Generic;
using System.Linq;
using ShadowBot;
using System.Text;
using System.Threading;
using MyWoW;
using MyWoW.Helpers;
using MyWoW.ObjectManager;

namespace FuzzyMod {
    public class EntryPoint {
        public static void Plugin_OnLoad() {
            //Plugin.Log("Plugin_OnLoad");
            Plugin.Initialize();
        }

        public static void Plugin_OnDisable() {
            //Plugin.Log("Plugin_OnDisable");
            Plugin.OnDisable();
        }

        public static void Plugin_OnEnable() {
            //Plugin.Log("Plugin_OnEnable");
            Plugin.OnEnable();
        }

        public static void Plugin_OnUnload() {
            //Plugin.Log("Plugin_OnUnload");
        }

        public static void Plugin_Settings() {
            // If you mark your line with [DEBUG] the output will go to SB Debug tab
            // Console.WriteLine("[DEBUG] Plugin Plugin_Settings()");
        }
    }
}
