using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Items.Armors
{

	[AutoloadEquip(EquipType.Head)]
	public class BlazewyrmHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazewyrm Helm");
			Tooltip.SetDefault("20% faster melee speed and damage");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 6;
			item.defense=12;
		}
		public override void UpdateEquip(Player player)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
			player.meleeSpeed += 0.2f;
			player.meleeDamage += 0.2f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("UnmanedBar"), 10);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	[AutoloadEquip(EquipType.Body)]
	public class BlazewyrmBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazewyrm Breastplate");
			Tooltip.SetDefault("15% increase melee crit chance");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 6;
			item.defense=15;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 15; 
		}		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("UnmanedBar"), 15);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	[AutoloadEquip(EquipType.Legs)]
	public class BlazewyrmLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazewyrm Leggings");
			Tooltip.SetDefault("20% increase to movement speed\nFaster yet while in lava");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 6;
			item.defense=9;
		}
		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.2f;
			player.accRunSpeed *= 1.2f;
			player.maxRunSpeed *= 1.2f;
			if (player.lavaWet)
			{
				player.moveSpeed *= 1.2f;
				player.accRunSpeed *= 1.2f;
				player.maxRunSpeed *= 1.2f;
			}
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("UnmanedBar"), 12);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}


}