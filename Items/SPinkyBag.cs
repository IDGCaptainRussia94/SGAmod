using Terraria;
using Terraria.ModLoader;

namespace SGAmod.Items
{
	public class SPinkyBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right click to open");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 32;
			item.height = 32;
			item.expert = true;
			item.rare = -12;
		}


		public override int BossBagNPC
		{
			get
			{
				return mod.NPCType("SPinky");
			}
		}


		public override bool CanRightClick()
		{
			return true;
		}
		public override void OpenBossBag(Player player)
		{
player.TryGettingDevArmor();
			for (int i = 0; i <= Main.rand.Next(75,100); i++)
			{
player.QuickSpawnItem(mod.ItemType("LunarRoyalGel"));
				//Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LunarRoyalGel"));
			}
			player.QuickSpawnItem(mod.ItemType("LunarSlimeHeart"));

		}

}

}
