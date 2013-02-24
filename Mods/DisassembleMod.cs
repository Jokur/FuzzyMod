using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using System.Threading;
using System.IO;

using FuzzyMod.Classes;

using ShadowBot;
using MyWoW;
using MyWoW.Helpers;
using MyWoW.ObjectManager;
using MyWoW.Classes;


namespace FuzzyMod.Mods {
	public partial class DisassembleMod : UserControl, Mod {

		enum DisassembleProfession {
			JEWELCRAFTING,
			INSCRIPTION,
			ENCHANTING,
			UNKNOWN,
		}

		class DisassembleItem {
			public DisassembleItem(string itemName, int itemId, DisassembleProfession profession, int requiredSkill) {
				this.Name = itemName;
				this.ItemId = itemId;
				this.Profession = profession;
				this.RequiredSkill = requiredSkill;
			}
			public string Name { get; set; }
			public int ItemId { get; set; }
			public DisassembleProfession Profession { get; set; }
			public int RequiredSkill { get; set; }
			public override string ToString() {
				string text = "";
				switch(Profession) {
					case DisassembleProfession.JEWELCRAFTING:
						text = "Prospecting: " + Name;
						break;
					case DisassembleProfession.INSCRIPTION:
						text = "Milling: " + Name;
						break;
					case DisassembleProfession.ENCHANTING:
						text = "Disenchant: " + Name;
						break;
				}
				return text;
			}
		}

		class BagItem {
			public BagItem(WowItem item, int bag, int slot) {
				this.wowItem = item;
				this.bag = bag;
				this.slot = slot;
			}
			public WowItem wowItem { get; set; }
			public int bag { get; set; }
			public int slot { get; set; }
		}

		private Thread thread = null;
		private bool isRunning = false;
		private int itemsLeft = 0;
		private WoWSpell disassembleSpell;
		private DisassembleItem disassembleItem;

		private DisassembleItem[] acceptableItems = { 
													// Prospecting Ore
													new DisassembleItem("Black Trillium Ore", 72094, DisassembleProfession.JEWELCRAFTING, 600),
													new DisassembleItem("White Trillium Ore", 72103, DisassembleProfession.JEWELCRAFTING, 600),
													new DisassembleItem("Kyparite", 72093, DisassembleProfession.JEWELCRAFTING, 550),
													new DisassembleItem("Ghost Iron Ore", 72092, DisassembleProfession.JEWELCRAFTING, 500),

													new DisassembleItem("Pyrite Ore", 52183, DisassembleProfession.JEWELCRAFTING, 500),
													new DisassembleItem("Elementium Ore", 52185, DisassembleProfession.JEWELCRAFTING, 475),
													new DisassembleItem("Obsidium Ore", 53038, DisassembleProfession.JEWELCRAFTING, 425),

													new DisassembleItem("Titanium Ore", 36910, DisassembleProfession.JEWELCRAFTING, 450),
													new DisassembleItem("Saronite Ore", 36912, DisassembleProfession.JEWELCRAFTING, 400),
													new DisassembleItem("Cobalt Ore", 36909, DisassembleProfession.JEWELCRAFTING, 350),

													new DisassembleItem("Adamantite Ore", 23425, DisassembleProfession.JEWELCRAFTING, 325),
													new DisassembleItem("Fel Iron Ore", 23424, DisassembleProfession.JEWELCRAFTING, 275),

													new DisassembleItem("Thorium Ore", 10620, DisassembleProfession.JEWELCRAFTING, 250),
													new DisassembleItem("Mithril Ore", 3858, DisassembleProfession.JEWELCRAFTING, 175),
													new DisassembleItem("Iron Ore", 2772, DisassembleProfession.JEWELCRAFTING, 125),
													new DisassembleItem("Tin Ore", 2771, DisassembleProfession.JEWELCRAFTING, 50),
													new DisassembleItem("Copper Ore", 2770, DisassembleProfession.JEWELCRAFTING, 1),

													// Milling Herbs
													new DisassembleItem("Fool's Cap", 79011, DisassembleProfession.INSCRIPTION, 500),
													new DisassembleItem("Snow Lily", 79010, DisassembleProfession.INSCRIPTION, 500),
													new DisassembleItem("Silkweed", 72235, DisassembleProfession.INSCRIPTION, 500),
													new DisassembleItem("Green Tea Leaf", 72234, DisassembleProfession.INSCRIPTION, 500),
													new DisassembleItem("Rain Poppy", 72237, DisassembleProfession.INSCRIPTION, 500),
													
													new DisassembleItem("Deathspore Pod", 52989, DisassembleProfession.INSCRIPTION, 500),
													new DisassembleItem("Whiptail", 52988, DisassembleProfession.INSCRIPTION, 475),
													new DisassembleItem("Twilight Jasmine", 52987, DisassembleProfession.INSCRIPTION, 475),
													new DisassembleItem("Heartblossom", 52986, DisassembleProfession.INSCRIPTION, 450),
													new DisassembleItem("Azshara's Veil", 52985, DisassembleProfession.INSCRIPTION, 450),
													new DisassembleItem("Stormvine", 52984, DisassembleProfession.INSCRIPTION, 425),
													new DisassembleItem("Cinderbloom", 52983, DisassembleProfession.INSCRIPTION, 425),
													
													new DisassembleItem("Icethorn", 36906, DisassembleProfession.INSCRIPTION, 325),
													new DisassembleItem("Adder's Tongue", 36903, DisassembleProfession.INSCRIPTION, 325),
													new DisassembleItem("Fire Leaf", 39970, DisassembleProfession.INSCRIPTION, 325),
													new DisassembleItem("Deadnettle", 37921, DisassembleProfession.INSCRIPTION, 325),
													new DisassembleItem("Goldclover", 36901, DisassembleProfession.INSCRIPTION, 325),
													new DisassembleItem("Tiger Lily", 36904, DisassembleProfession.INSCRIPTION, 325),
													new DisassembleItem("Talandra's Rose", 36907, DisassembleProfession.INSCRIPTION, 325),
													
													new DisassembleItem("Netherbloom", 22791, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Nightmare Vine", 22792, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Mana Thistle", 22793, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Ancient Lichen", 22790, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Ragveil", 22787, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Terocone", 22789, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Dreaming Glory", 22786, DisassembleProfession.INSCRIPTION, 275),
													new DisassembleItem("Felweed", 22785, DisassembleProfession.INSCRIPTION, 275),
													
													new DisassembleItem("Sorrowmoss", 13466, DisassembleProfession.INSCRIPTION, 225),
													new DisassembleItem("Mountain Silversage", 13465, DisassembleProfession.INSCRIPTION, 225),
													new DisassembleItem("Golden Sansam", 13464, DisassembleProfession.INSCRIPTION, 225),
													new DisassembleItem("Dreamfoil", 13463, DisassembleProfession.INSCRIPTION, 225),
													new DisassembleItem("Icecap", 13467, DisassembleProfession.INSCRIPTION, 200),
													new DisassembleItem("Gromsblood", 8846, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Ghost Mushroom", 8845, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Blindweed", 8839, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Sungrass", 8838, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Arthas' Tears", 8836, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Purple Lotus", 8831, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Firebloom", 4625, DisassembleProfession.INSCRIPTION, 175),
													new DisassembleItem("Dragon's Teeth", 3819, DisassembleProfession.INSCRIPTION, 125),
													new DisassembleItem("Khadgar's Whisker", 3358, DisassembleProfession.INSCRIPTION, 125),
													new DisassembleItem("Goldthorn", 3821, DisassembleProfession.INSCRIPTION, 125),
													new DisassembleItem("Fadeleaf", 3818, DisassembleProfession.INSCRIPTION, 125),
													new DisassembleItem("Liferoot", 3357, DisassembleProfession.INSCRIPTION, 75),
													new DisassembleItem("Kingsblood", 3356, DisassembleProfession.INSCRIPTION, 75),
													new DisassembleItem("Wild Steelbloom", 3355, DisassembleProfession.INSCRIPTION, 75),
													new DisassembleItem("Grave Moss", 3369, DisassembleProfession.INSCRIPTION, 75),
													new DisassembleItem("Bruiseweed", 2453, DisassembleProfession.INSCRIPTION, 25),
													new DisassembleItem("Stranglekelp", 3820, DisassembleProfession.INSCRIPTION, 25),
													new DisassembleItem("Briarthorn", 2450, DisassembleProfession.INSCRIPTION, 25),
													new DisassembleItem("Swiftthistle", 2452, DisassembleProfession.INSCRIPTION, 25),
													new DisassembleItem("Earthroot", 2449, DisassembleProfession.INSCRIPTION, 1),
													new DisassembleItem("Mageroyal", 785, DisassembleProfession.INSCRIPTION, 1),
													new DisassembleItem("Silverleaf", 765, DisassembleProfession.INSCRIPTION, 1),
													new DisassembleItem("Peacebloom", 2447, DisassembleProfession.INSCRIPTION, 1) };

        public DisassembleMod() {
            InitializeComponent();
        }

		~DisassembleMod() {
        }

        public void OnBotStart(object sender, EventArgs args) {}
        public void OnBotStop(object sender, EventArgs args) {}

        public string DisplayName {
            get { return "Disassemble"; }
        }

        public UserControl Control {
            get { return this; }
        }

        private void Log(String message) {
			Console.WriteLine("[Disassemble] " + message);
        }

		private void LogDebug(String message) {
			Console.WriteLine("[DEBUG][Disassemble] " + message);
		}

		private void RefreshActions() {
			if(isRunning)
				return;
			
			isRunning = true;
			thread = new Thread(new ThreadStart(ThreadRefresh));
			thread.Start();

			
		}

		private void cbActions_SelectedIndexChanged(object sender, EventArgs e) {
			if(cbActions.SelectedIndex < 0)
				return;

			DisassembleItem item = (DisassembleItem)cbActions.Items[cbActions.SelectedIndex];

			int count = Inventory.GetItemCountById(item.ItemId);
			if(item.Profession == DisassembleProfession.JEWELCRAFTING || item.Profession == DisassembleProfession.INSCRIPTION) {
				count /= 5; // Requires atleast 5 to disassemble
			}
			txtItems.Text = count.ToString();
		}

		private void btnStart_Click(object sender, EventArgs e) {
			if(isRunning) {
				isRunning = false;
				if( thread != null )
					thread.Join();
				return;
			}

			if(cbActions.SelectedIndex < 0)
				return;

			if(ShadowBot.API.Bot.GetSettings.MouseHook_Enabled) {
				Mouse.Initialize();
			}

			Spells.CloseMountFrame();
			Spells.CloseSpellBookFrame();

			DisassembleItem item = (DisassembleItem)cbActions.Items[cbActions.SelectedIndex];

			int count = Inventory.GetItemCountById(item.ItemId);
			if(item.Profession == DisassembleProfession.JEWELCRAFTING || item.Profession == DisassembleProfession.INSCRIPTION) {
				count /= 5; // Requires atleast 5 to disassemble
			}

			int disassembleCount = 0;
			try {
				disassembleCount = int.Parse(txtItems.Text);
			} catch(Exception) {
				MessageBox.Show("Items to disassemble must be an integer number");
				return;
			}


			int spellId = 0;
			switch(item.Profession) {
				case DisassembleProfession.JEWELCRAFTING:
					spellId = 31252;
					break;
				case DisassembleProfession.INSCRIPTION:
					spellId = 51005;
					break;
				case DisassembleProfession.ENCHANTING:
					spellId = 13262;
					break;
			}

			disassembleSpell = Spells.GetWoWSpellById(spellId);
			
			if(!disassembleSpell.OnActionBar) {
				bool success = Spells.PlaceGatheringSpellOnBarById(spellId);

				if(!success) {
					Log("Failed to place spell on bar");
					return;
				} else {
					Log("Placed on bar: " + success.ToString());
				}
			}

			itemsLeft = disassembleCount;

			disassembleItem = item;


			btnStart.Text = "Stop";
			isRunning = true;
			if(item.Profession == DisassembleProfession.JEWELCRAFTING || item.Profession == DisassembleProfession.INSCRIPTION) {
				thread = new Thread(new ThreadStart(ThreadSort));
				thread.Start();
			} else {
				thread = new Thread(new ThreadStart(ThreadDisassemble));
				thread.Start();
			}
		}


		public void ThreadRefresh() {
			Log("Refreshing list...");

			cbActions.Enabled = false;
			txtItems.Enabled = false;
			btnRefresh.Enabled = false;
			btnStart.Enabled = false;

			cbActions.Items.Clear();

			LogDebug("Refreshing list...");

			foreach(DisassembleItem item in acceptableItems) {
				LogDebug(item.Name);
				Thread.Sleep(50);
				int count = Inventory.GetItemCountByName(item.Name);
				if(item.Profession == DisassembleProfession.JEWELCRAFTING || item.Profession == DisassembleProfession.INSCRIPTION) {
					count /= 5; // Requires atleast 5 to disassemble
				}

				LogDebug(" Count: " + count.ToString());

				// have enough units
				if(count <= 0)
					continue;

				LogDebug(" Required Skill: " + item.RequiredSkill);

				// Check skill requirements
				int currentSkill = 0;
				switch(item.Profession) {
					case DisassembleProfession.JEWELCRAFTING:
						currentSkill = ObjectManager.Me.GetSkillInfoByID(MyWoW.Helpers.DBC.SkillLine.Skill.Jewelcrafting).CurrentSkill;

						// Draenei racial
						if(ObjectManager.Me.Race == WowUnit.WoWRace.Draenei)
							currentSkill += 10;
						break;
					case DisassembleProfession.INSCRIPTION:
						currentSkill = ObjectManager.Me.GetSkillInfoByID(MyWoW.Helpers.DBC.SkillLine.Skill.Inscription).CurrentSkill;
						
						break;
					case DisassembleProfession.ENCHANTING:
						currentSkill = ObjectManager.Me.GetSkillInfoByID(MyWoW.Helpers.DBC.SkillLine.Skill.Enchanting).CurrentSkill;

						// Belf racial
						if(ObjectManager.Me.Race == WowUnit.WoWRace.BloodElf)
							currentSkill += 10;
						break;
				}

				

				LogDebug(" Current Skill: " + currentSkill);

				if(currentSkill >= item.RequiredSkill)
					continue;

				cbActions.Items.Add(item);
			}
			LogDebug("Done!");

			Log("Done!");

			cbActions.Enabled = true;
			txtItems.Enabled = true;
			btnRefresh.Enabled = true;
			btnStart.Enabled = true;

			isRunning = false;
			thread = null;
		}

		public void ThreadSort() {
			Inventory.CloseAllBags();
			if(!Keybindings.IsValidKeyForAction("OPENALLBAGS")) {
				Log("Keybinding \"OPENALLBAGS\" is unbound, please bind it.");
				thread = null;
				isRunning = false;
				return;
			}

			Keybindings.UseBinding("OPENALLBAGS");

			Log("Sorting items in to stacks...");
			bool runBagCheck = true;
			do {
				runBagCheck = false;

				List<BagItem> bagItems = new List<BagItem>();

				for(int i = 1; i <= 16; i++) {
					WowItem bagItem = Inventory.GetBackpackItemBySlot(i);
					if(bagItem.Name.Equals(disassembleItem.Name)) {
						bagItems.Add(new BagItem(bagItem, 0, i));
					}
				}
				for(int i = 1; i <= Inventory.Bag1.MaxSlot; i++) {
					WowItem bagItem = Inventory.Bag1.GetItemBySlot(i);
					if(bagItem.Name.Equals(disassembleItem.Name)) {
						bagItems.Add(new BagItem(bagItem, 1, i));
					}
				}
				for(int i = 1; i <= Inventory.Bag2.MaxSlot; i++) {
					WowItem bagItem = Inventory.Bag2.GetItemBySlot(i);
					if(bagItem.Name.Equals(disassembleItem.Name)) {
						bagItems.Add(new BagItem(bagItem, 2, i));
					}
				}
				for(int i = 1; i <= Inventory.Bag3.MaxSlot; i++) {
					WowItem bagItem = Inventory.Bag3.GetItemBySlot(i);
					if(bagItem.Name.Equals(disassembleItem.Name)) {
						bagItems.Add(new BagItem(bagItem, 3, i));
					}
				}
				for(int i = 1; i <= Inventory.Bag4.MaxSlot; i++) {
					WowItem bagItem = Inventory.Bag4.GetItemBySlot(i);
					if(bagItem.Name.Equals(disassembleItem.Name)) {
						bagItems.Add(new BagItem(bagItem, 4, i));
					}
				}

				foreach(BagItem bagItem in bagItems) {
					if(bagItem.wowItem.StackCount % 5 > 0 && bagItems.Count > 1) {

						BagItem lastBagItem = bagItems[bagItems.Count - 1];

						LogDebug("Merging " + lastBagItem.wowItem.StackCount);
						LogDebug(" with " + bagItem.wowItem.StackCount);

						InventoryExt.LeftClickItemInBag(lastBagItem.bag, lastBagItem.slot);
						Thread.Sleep(500);
						InventoryExt.LeftClickItemInBag(bagItem.bag, bagItem.slot);
						Thread.Sleep(1000);
						runBagCheck = true;
						break;
					}
				}

			} while(runBagCheck && isRunning);

			LogDebug("Done!");
			Log("Done!");


			thread = null;
			if(isRunning) {
				Thread.Sleep(3000);
				thread = new Thread(new ThreadStart(ThreadDisassemble));
				thread.Start();
			}
		}


		public void ThreadDisassemble() {
			while(isRunning && itemsLeft > 0) {

				// Make sure we're on the right action bar
				if(ActionBar.CurrentActionBar != disassembleSpell.GetActionBarAction.Bar) {
					ActionBar.ChangeActionBar(disassembleSpell.GetActionBarAction.Bar);
					Thread.Sleep(1000);
				}

				disassembleSpell.GetActionBarAction.Push();
				Thread.Sleep(500);
				bool success = InventoryExt.UseItemByIdExt(disassembleItem.ItemId);
				if(success) {
					itemsLeft--;
				} else {
					isRunning = false;
					Log("Unable to disassemble, maybe someone closed the bag");
				}

				txtItems.Text = itemsLeft.ToString();
				Log(disassembleItem.ToString() + ": " + itemsLeft.ToString() + " left");

				while(ObjectManager.Me.IsCasting && isRunning) {
					Thread.Sleep(100);
				}

				UIFrame.Update();
				UIFrame.Frame frame = UIFrame.GetFrameByName("LootFrame");
				while(!frame.IsVisible && isRunning) {
					//Log("Waiting for loot window to open");
					Thread.Sleep(100);
					UIFrame.Update();
					frame = UIFrame.GetFrameByName("LootFrame");
				}

				while(frame.IsVisible && isRunning) {
					//Log("Waiting for loot window to close");
					Thread.Sleep(100);
					UIFrame.Update();
					frame = UIFrame.GetFrameByName("LootFrame");
				}

				Thread.Sleep(100);
			}


			Log("All done!");

			btnStart.Text = "Start";
			isRunning = false;
			thread = null;


			RefreshActions();
		}

		private void btnRefresh_Click(object sender, EventArgs e) {
			RefreshActions();
		}

		private void DisassembleMod_Load(object sender, EventArgs e) {
			RefreshActions();
		}
	}
}
