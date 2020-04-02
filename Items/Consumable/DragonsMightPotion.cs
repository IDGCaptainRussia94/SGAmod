using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SGAmod.Generation;
using SGAmod;

namespace SGAmod.Items.Consumable
{
	public class DragonsMightPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon's Might Potion");
			Tooltip.SetDefault("50% increase to all damage types except Summon damage" +
				"\nLasts 20 seconds, inflicts Weakness after it ends\nThis cannot be stopped by being immune");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 24;
			item.maxStack = 30;
			item.rare = 8;
			item.value = 1000;
			item.useStyle = 2;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useTurn = true;
			item.UseSound = SoundID.Item3;
			item.consumable = true;
			item.buffType = mod.BuffType("DragonsMight");
			item.buffTime = 60*20;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RestorationPotion,2);
			recipe.AddIngredient(ItemID.BottledHoney);
			recipe.AddIngredient(null, "OmniSoul", 2);
			recipe.AddIngredient(null, "Fridgeflame", 2);
			recipe.AddIngredient(null, "MurkyGel", 4);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 20);
			recipe.AddTile(TileID.AlchemyTable);
			recipe.SetResult(this,2);
			recipe.AddRecipe();
		}

		public override void OnConsumeItem(Player player)
		{
			//RippleBoom.MakeShockwave(player.Center,8f,1f,10f,60,1f);
			//Achivements.SGAAchivements.UnlockAchivement("TPD", Main.LocalPlayer);
			//SGAmod.FileTest();
			//NormalWorldGeneration.PlaceCaiburnShrine(player.Center / 16f);
			//WorldGen.placeTrap((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f)+1, 0);
		}
	}
}