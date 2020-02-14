using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Items
{
	public class TerrariacoCrateKey: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terraria Co Supply Crate Key");
			Tooltip.SetDefault("Use this to open a Terraria Co Supply Crate");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 21;
			item.height = 21;
			item.value = 0;
			item.rare = 10;
		}
	}

}
