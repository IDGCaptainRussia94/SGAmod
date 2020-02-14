using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.HavocGear.Items
{
	public class FieryShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Shard");
		}
		
	public override void SetDefaults()
        {
		item.width = 22;
		item.height = 22;
		item.maxStack = 99;
		item.value = 1000;
		item.rare = 3;
		ItemID.Sets.ItemNoGravity[item.type] = true;
		ItemID.Sets.ItemIconPulse[item.type] = true;
		item.alpha = 30;	}

	public override void PostUpdate()
	{
		Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
	}
    }
}