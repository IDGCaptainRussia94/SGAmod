using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Items
{
	public class AustraliumOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A priceless fragment from another universe");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 8;
			item.value = 0;//Item.sellPrice(0, 0, 20, 0);
			item.createTile = mod.TileType("AustraliumOre");
		}
	}
}