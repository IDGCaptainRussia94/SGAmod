using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.Items.Consumable
{
	public class IceFirePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fridgeframe Concoction");
			Tooltip.SetDefault("'A Potion of Ice and Fire'" +
				"\nGrants 25% reduced Damage-over-time caused by Debuffs\nGain an extra 15% less damage while you have Frostburn or OnFire! (Both do not stack)\n25% less damage from cold sources, Obsidian Rose effect\n" + Idglib.ColorText(Color.Red,"Removes Immunity to both Frostburn and OnFire!"));
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 24;
			item.maxStack = 30;
			item.rare = 8;
			item.value = 20000;
			item.useStyle = 2;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useTurn = true;
			item.UseSound = SoundID.Item3;
			item.consumable = true;
			item.buffType = mod.BuffType("IceFirePotion");
			item.buffTime = 60*90;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LavaBucket,1);
			recipe.AddIngredient(ItemID.WarmthPotion, 2);
			recipe.AddIngredient(null, "CryostalBar", 2);
			recipe.AddIngredient(null, "IceFairyDust", 2);
			recipe.AddIngredient(null, "Fridgeflame", 2);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this,3);
			recipe.AddRecipe();
		}
	}
}