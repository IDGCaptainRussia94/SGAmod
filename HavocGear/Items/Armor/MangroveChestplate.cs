using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.HavocGear.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class MangroveChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mangrove Chestplate");
			Tooltip.SetDefault("33% to not consume thrown items\n8% increased throwing damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 50000;
			item.rare = 4;
			item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCost33 = true;
			player.thrownDamage += 0.08f;
		}


		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("MangroveHelmet") && legs.type == mod.ItemType("MangroveGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Crit throwing attacks grant Dryad's Blessing and spawn Mangrove Orbs from you that seek out enemies\nYou are limited to 4 of these Orbs at a time\nGreatly Increased regeneration while in the jungle";
			player.GetModPlayer<SGAPlayer>().Mangroveset = true;
			if (player.ZoneJungle)
			player.lifeRegen += 6;
		}

        	public override void AddRecipes()
        	{
            		ModRecipe recipe = new ModRecipe(mod);
            		recipe.AddIngredient(null, "VirulentBar", 11);
					recipe.AddIngredient(null, "DankWoodChest", 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
            		recipe.AddRecipe();
        	}
	}

	[AutoloadEquip(EquipType.Head)]
	public class MangroveHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mangrove Helmet");
			Tooltip.SetDefault("15% increased throwing velocity\n10% increased throwing damage\n10% increased throwing crit");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 50000;
			item.rare = 4;
			item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownVelocity += 0.15f;
			player.thrownDamage += 0.1f;
			player.thrownCrit += 10;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "VirulentBar", 7);
			recipe.AddIngredient(null, "DankWoodHelm", 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	[AutoloadEquip(EquipType.Legs)]
	public class MangroveGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mangrove Greaves");
			Tooltip.SetDefault("25% faster throwing item use speed\nIncreased movement speed\n7% increased throwing damage");
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<SGAPlayer>().ThrowingSpeed += 0.25f;
			player.maxRunSpeed += 0.5f;
			player.accRunSpeed += 0.5f;
			player.runAcceleration += 0.25f;
			player.thrownDamage += 0.07f;
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 50000;
			item.rare = 4;
			item.defense = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "VirulentBar", 9);
			recipe.AddIngredient(null, "DankLegs", 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

}