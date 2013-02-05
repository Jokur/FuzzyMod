using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyWoW;
using MyWoW.Helpers;

namespace FuzzyMod.Classes {
	public class InventoryExt : Inventory {
		/*public static Boolean PickUpItemById(int ItemId) {

			int Bag = -1;
			int Slot = -1;
			int MaxSlot = -1;

			#region Backpack

			if(Bag == -1 || Slot == -1)
				for(int i = 1; i <= BackpackMaxSlot; i++)
					if(GetBackpackItemBySlot(i).Entry == ItemId) {
						Bag = 0;
						Slot = i;
						MaxSlot = BackpackMaxSlot;
						//OpenBag(Inventory.Bag.BACKPACK);
						break;
					}

			#endregion

			#region Bag1

			if(Bag == -1 || Slot == -1)
				if(Bag1GUID != 0)
					for(int i = 1; i <= Bag1.MaxSlot; i++)
						if(Bag1.GetItemBySlot(i).Entry == ItemId) {
							Bag = 1;
							Slot = i;
							MaxSlot = Bag1.MaxSlot;
							//OpenBag(Inventory.Bag.BAG1);
							break;
						}

			#endregion

			#region Bag2

			if(Bag == -1 || Slot == -1)
				if(Bag2GUID != 0)
					for(int i = 1; i <= Bag2.MaxSlot; i++)
						if(Bag2.GetItemBySlot(i).Entry == ItemId) {
							Bag = 2;
							Slot = i;
							MaxSlot = Bag2.MaxSlot;
							//OpenBag(Inventory.Bag.BAG2);
							break;
						}

			#endregion

			#region Bag3

			if(Bag == -1 || Slot == -1)
				if(Bag3GUID != 0)
					for(int i = 1; i <= Bag3.MaxSlot; i++)
						if(Bag3.GetItemBySlot(i).Entry == ItemId) {
							Bag = 3;
							Slot = i;
							MaxSlot = Bag3.MaxSlot;
							//OpenBag(Inventory.Bag.BAG3);
							break;
						}

			#endregion

			#region Bag4

			if(Bag == -1 || Slot == -1)
				if(Bag4GUID != 0)
					for(int i = 1; i <= Bag4.MaxSlot; i++)
						if(Bag4.GetItemBySlot(i).Entry == ItemId) {
							Bag = 4;
							Slot = i;
							MaxSlot = Bag4.MaxSlot;
							//OpenBag(Inventory.Bag.BAG4);
							break;
						}

			#endregion

			if(Bag == -1 ||
				Slot == -1 ||
				MaxSlot == -1)
				return false;

			Int32 RealSlot = MaxSlot - ( Slot - 1 );

			var ContainerFrame1Item = MyWoW.Helpers.UIFrame.GetFrameByName("ContainerFrame" + (Bag+1).ToString() + "Item" + RealSlot.ToString());

			if(ContainerFrame1Item == null) {
				//CloseAllBags();
				return false;
			} else {
				System.Threading.Thread.Sleep(250);
				ContainerFrame1Item.LeftClick();
				//CloseAllBags();
				return true;
			}


		}*/

		public static Boolean LeftClickItemInBag(int bag, int slot) {
			Int32 MaxSlot = 16;
			if( bag == 1 ) {
				MaxSlot = Bag1.MaxSlot;
			} else if(bag == 2) {
				MaxSlot = Bag2.MaxSlot;
			} else if(bag == 3) {
				MaxSlot = Bag3.MaxSlot;
			} else if(bag == 4) {
				MaxSlot = Bag4.MaxSlot;
			}

			Int32 RealSlot = MaxSlot - ( slot - 1 );

			var ContainerFrame1Item = MyWoW.Helpers.UIFrame.GetFrameByName("ContainerFrame" + ( bag + 1 ).ToString() + "Item" + RealSlot.ToString());

			if(ContainerFrame1Item == null) {
				//CloseAllBags();
				return false;
			} else {
				System.Threading.Thread.Sleep(250);
				ContainerFrame1Item.LeftClick();
				//CloseAllBags();
				return true;
			}
		}
		/*
		public static Boolean PickUpItemByName(String ItemName) {

			int Bag = -1;
			int Slot = -1;
			int MaxSlot = -1;

			#region Backpack

			if(Bag == -1 || Slot == -1)
				for(int i = 1; i <= BackpackMaxSlot; i++)
					if(MyWoW.Helpers.ClientDB.DBCacheItem.GetItemInfoById((uint)GetBackpackItemBySlot(i).Entry).LocalizedName == ItemName) {
						Bag = 0;
						Slot = i;
						MaxSlot = BackpackMaxSlot;
						OpenBag(Inventory.Bag.BACKPACK);
						break;
					}

			#endregion

			#region Bag1

			if(Bag == -1 || Slot == -1)
				if(Bag1GUID != 0)
					for(int i = 1; i <= Bag1.MaxSlot; i++)
						if(MyWoW.Helpers.ClientDB.DBCacheItem.GetItemInfoById((uint)Bag1.GetItemBySlot(i).Entry).LocalizedName == ItemName) {
							Bag = 1;
							Slot = i;
							MaxSlot = Bag1.MaxSlot;
							OpenBag(Inventory.Bag.BAG1);
							break;
						}

			#endregion

			#region Bag2

			if(Bag == -1 || Slot == -1)
				if(Bag2GUID != 0)
					for(int i = 1; i <= Bag2.MaxSlot; i++)
						if(MyWoW.Helpers.ClientDB.DBCacheItem.GetItemInfoById((uint)Bag2.GetItemBySlot(i).Entry).LocalizedName == ItemName) {
							Bag = 2;
							Slot = i;
							MaxSlot = Bag2.MaxSlot;
							OpenBag(Inventory.Bag.BAG2);
							break;
						}

			#endregion

			#region Bag3

			if(Bag == -1 || Slot == -1)
				if(Bag3GUID != 0)
					for(int i = 1; i <= Bag3.MaxSlot; i++)
						if(MyWoW.Helpers.ClientDB.DBCacheItem.GetItemInfoById((uint)Bag3.GetItemBySlot(i).Entry).LocalizedName == ItemName) {
							Bag = 3;
							Slot = i;
							MaxSlot = Bag3.MaxSlot;
							OpenBag(Inventory.Bag.BAG3);
							break;
						}

			#endregion

			#region Bag4

			if(Bag == -1 || Slot == -1)
				if(Bag4GUID != 0)
					for(int i = 1; i <= Bag4.MaxSlot; i++)
						if(MyWoW.Helpers.ClientDB.DBCacheItem.GetItemInfoById((uint)Bag4.GetItemBySlot(i).Entry).LocalizedName == ItemName) {
							Bag = 4;
							Slot = i;
							MaxSlot = Bag4.MaxSlot;
							OpenBag(Inventory.Bag.BAG4);
							break;
						}

			#endregion

			if(Bag == -1 ||
				Slot == -1 ||
				MaxSlot == -1)
				return false;

			Int32 RealSlot = MaxSlot - ( Slot - 1 );

			var ContainerFrame1Item = MyWoW.Helpers.UIFrame.GetFrameByName("ContainerFrame1Item" + RealSlot.ToString());

			if(ContainerFrame1Item == null) {
				CloseAllBags();
				return false;
			} else {
				System.Threading.Thread.Sleep(250);
				ContainerFrame1Item.LeftClick();
				CloseAllBags();
				return true;
			}
		}*/
	}
}
