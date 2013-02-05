using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FuzzyMod.Mods {
    public interface Mod {
        void OnBotStart(object sender, EventArgs args);
        void OnBotStop(object sender, EventArgs args);

        string DisplayName { get; }
        UserControl Control { get; }
    }
}
