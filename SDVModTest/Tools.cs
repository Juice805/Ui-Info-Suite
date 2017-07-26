using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIInfoSuite {
	static class Tools {

		public static void CreateSafeDelayedDialogue(String dialogue, int timer) {
			Task.Factory.StartNew(() => {
				Thread.Sleep(timer);

				do {
					Thread.Sleep(TimeSpan.FromSeconds(1));
				}
				while (Game1.activeClickableMenu is GameMenu);
				Game1.setDialogue(dialogue, true);
			});
		}

		public static int GetWidthInPlayArea() {
			int result = 0;

			if (Game1.isOutdoorMapSmallerThanViewport()) {
				int right = Game1.graphics.GraphicsDevice.Viewport.TitleSafeArea.Right;
				int totalWidth = Game1.currentLocation.map.Layers[0].LayerWidth * Game1.tileSize;
				int someOtherWidth = Game1.graphics.GraphicsDevice.Viewport.TitleSafeArea.Right - totalWidth;

				result = right - someOtherWidth / 2;
			} else {
				result = Game1.graphics.GraphicsDevice.Viewport.TitleSafeArea.Right;
			}

			return result;
		}

		public static int GetTruePrice(Item item) {

			StardewValley.Object itemObject = item as StardewValley.Object;
			int price = 0;

			if (itemObject == null) {
				if (item.Name != "Scythe" && !(item is FishingRod)) // weird exceptions
					price = item.salePrice();
			} else {
				price = itemObject.sellToStorePrice();
			}
				return price;
			}

			public static Item GetHoveredItem()
			{
				Item hoverItem = null;

				for (int i = 0; i < Game1.onScreenMenus.Count; ++i) {
					Toolbar onScreenMenu = Game1.onScreenMenus[i] as Toolbar;
					if (onScreenMenu != null) {
						FieldInfo hoverItemField = typeof(Toolbar).GetField("hoverItem", BindingFlags.Instance | BindingFlags.NonPublic);
						hoverItem = hoverItemField.GetValue(onScreenMenu) as Item;
						//hoverItemField.SetValue(onScreenMenu, null);
					}
				}

				if (Game1.activeClickableMenu is GameMenu) {
					List<IClickableMenu> menuList = typeof(GameMenu).GetField("pages", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Game1.activeClickableMenu) as List<IClickableMenu>;
					foreach (var menu in menuList) {
						if (menu is InventoryPage) {
							FieldInfo hoveredItemField = typeof(InventoryPage).GetField("hoveredItem", BindingFlags.Instance | BindingFlags.NonPublic);
							hoverItem = hoveredItemField.GetValue(menu) as Item;
							//typeof(InventoryPage).GetField("hoverText", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(menu, "");
						}
					}
				}

				if (Game1.activeClickableMenu is ItemGrabMenu) {
					hoverItem = (Game1.activeClickableMenu as MenuWithInventory).hoveredItem;
					//(Game1.activeClickableMenu as MenuWithInventory).hoveredItem = null;
				}

				return hoverItem;
			}
		}

	public static class SourceRects {
		public static readonly Rectangle springIcon = new Rectangle(406, 441, 12, 8);
		public static readonly Rectangle summerIcon = new Rectangle(406, 449, 12, 8);
		public static readonly Rectangle fallIcon = new Rectangle(406, 457, 12, 8);
		public static readonly Rectangle winterIcon = new Rectangle(406, 465, 12, 8);
		public static readonly Rectangle sunnyIcon = new Rectangle(341, 421, 12, 8);
		public static readonly Rectangle rainIcon = new Rectangle(465, 421, 12, 8);
		public static readonly Rectangle nightIcon = new Rectangle(465, 344, 13, 13);

		public static readonly Rectangle fishIcon = new Rectangle(20, 428, 10, 10);
		public static readonly Rectangle cropIcon = new Rectangle(10, 428, 10, 10);
		public static readonly Rectangle bundleIcon = new Rectangle(331, 374, 15, 14);
		public static readonly Rectangle healingIcon = new Rectangle(140, 428, 10, 10);
		public static readonly Rectangle energyIcon = new Rectangle(0, 438, 10, 10);
		public static readonly Rectangle currencyIcon = new Rectangle(5, 69, 6, 6);


	}
}
