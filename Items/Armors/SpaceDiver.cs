using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.Items.Armors
{

	[AutoloadEquip(EquipType.Head)]
	public class SpaceDiverHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Space Diver Helm");
			Tooltip.SetDefault("You emit blue light while in water\nEffects of Arctic Diving Gear\n" + Idglib.ColorText(Color.Red, "5 % less damage"));
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 6;
			item.defense=8;
		}
		public override void UpdateEquip(Player player)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
        if (player.wet){
		Lighting.AddLight(player.Center, 0.2f, 0.0f, 0.5f);
		}
		player.arcticDivingGear = true;
		player.magicDamage -= 0.05f; player.rangedDamage -= 0.05f; player.minionDamage -= 0.05f; player.thrownDamage -= 0.05f; player.meleeDamage -= 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 10);
			recipe.AddIngredient(ItemID.ArcticDivingGear, 1);
			recipe.AddTile(mod.TileType("PrismalStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	[AutoloadEquip(EquipType.Body)]
	public class SpaceDiverChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Space Diver Chestplate");
			Tooltip.SetDefault("Grants unmatched movement in water\n10% faster item use times\n"+Idglib.ColorText(Color.Red,"5 % less damage"));
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 6;
			item.defense=16;
		}
		public override void UpdateEquip(Player player)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
            player.arcticDivingGear=true;
            player.accFlipper = true;
			player.accDivingHelm = true;
			player.iceSkate = true;
			player.ignoreWater = true;
            sgaplayer.UseTimeMul+=0.10f;
			player.magicDamage -= 0.05f; player.rangedDamage -= 0.05f; player.minionDamage -= 0.05f; player.thrownDamage -= 0.05f; player.meleeDamage -= 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 15);
			recipe.AddTile(mod.TileType("PrismalStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	[AutoloadEquip(EquipType.Legs)]
	public class SpaceDiverLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Space Diver Leggings");
			Tooltip.SetDefault("20% faster movement speed\nthis increases by another 20% when in water\n" + Idglib.ColorText(Color.Red, "5 % less damage"));
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 6;
			item.defense=8;
		}
		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.20f;
			player.accRunSpeed *= 1.20f;
			player.maxRunSpeed *= 1.20f;
			if (player.wet){
			player.moveSpeed *= 1.20f;
			player.accRunSpeed *= 1.20f;
			player.maxRunSpeed *= 1.20f;
		}
			player.magicDamage -= 0.05f; player.rangedDamage -= 0.05f; player.minionDamage -= 0.05f; player.thrownDamage -= 0.05f; player.meleeDamage -= 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 12);
			recipe.AddTile(mod.TileType("PrismalStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}


}