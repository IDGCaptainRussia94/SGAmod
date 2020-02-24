using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Items
{

	public class WraithCoreFragment3: WraithCoreFragment
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Wraith Core Fragment");
			Tooltip.SetDefault("Summons forth the third and final of the Wraiths, who has stolen your ability to make Luminite Bars (and also the Ancient Manipulator from the Cultist)");
		}

		public override string Texture
		{
			get { return "SGAmod/Items/LunarCore"; }
		}

			public override void SetDefaults()
		{
			base.SetDefaults();
			item.rare = 8;
		}

		public override bool CanUseItem(Player player)
		{
			if (SGAWorld.downedWraiths==3 && !NPC.downedMoonlord)
			{
			item.consumable=false;
			}else{
			item.consumable=true;
			}
			return base.CanUseItem(player);
		}

		public override bool UseItem(Player player)
		{
			if (item.consumable==false){
			Main.NewText("Our time has not yet come",25, 25, 80);
			return false;
			}else{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("LuminiteWraith"));
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SGAmod:CelestialFragments",4);
			recipe.AddIngredient(null,"WraithCoreFragment2", 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class WraithCoreFragment2: WraithCoreFragment
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Empowered Wraith Core Fragment");
			Tooltip.SetDefault("Summons forth the second of the Wraiths, who has stolen your ability to make a hardmode anvil.");
		}

		public override string Texture
		{
			get { return "SGAmod/Items/EmpoweredCore"; }
		}

			public override void SetDefaults()
		{
			base.SetDefaults();
			item.rare = 5;
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("CobaltWraith"));
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SGAmod:Tier1HardmodeOre",10);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 5);
			recipe.AddIngredient(null,"WraithCoreFragment", 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class WraithCoreFragment: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wraith Core Fragment");
			Tooltip.SetDefault("Summons forth the first of the Wraiths, who has stolen your ability to make a furnace.");
		}

		public override string Texture
		{
			get { return "SGAmod/Items/BasicCore"; }
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = 4;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.value = 0;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player)
		{
			if (!NPC.AnyNPCs(mod.NPCType("CopperWraith")) && !NPC.AnyNPCs(mod.NPCType("CobaltWraith")) && !NPC.AnyNPCs(mod.NPCType("LuminiteWraith")))
			{
			return true;
			}else{
			return false;
			}
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("CopperWraith"));
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SGAmod:Tier1Ore",10);
			recipe.AddIngredient(ItemID.FallenStar, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

		public class ConchHorn : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Conch Horn");
			Tooltip.SetDefault("'It's call pierces the depths of the ocean.' \nSummons the Sharkvern");
		}
		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 99;
			item.rare = 3;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.ZoneBeach && !NPC.AnyNPCs(mod.NPCType("SharkvernHead"))){
			return true;
			}else{
			Main.NewText("The couch blows but no waves are shaken by its ring...",100, 100, 250);
			return false;

			}
		}

		public override bool UseItem(Player player)
		{
			if (player.ZoneBeach && !NPC.AnyNPCs(mod.NPCType("SharkvernHead"))){
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SharkvernHead"));
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		return false;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Seashell, 1);
			recipe.AddIngredient(ItemID.SharkFin, 1);
			recipe.AddIngredient(ItemID.SoulofLight,5);
			recipe.AddIngredient(ItemID.ChlorophyteBar,5);			
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	public class AcidicEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acidic Egg");
			Tooltip.SetDefault("'No words for this...' \nSummons the Spider Queen\nRotten Eggs drop from spiders");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RottenEgg, 1);
			recipe.AddIngredient(ItemID.Cobweb, 25);
			recipe.AddRecipeGroup("SGAmod:EvilBossMaterials", 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 99;
			item.rare = 2;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			bool underground = (int)((double)((player.position.Y + (float)player.height) * 2f / 16f) - Main.worldSurface * 2.0)>0;
			;
			if (underground && !NPC.AnyNPCs(mod.NPCType("SpiderQueen")))
			{
				return true;
			}
			else
			{
				Main.NewText("There are no spiders here, try using it underground", 30, 200, 30);
				return false;

			}
		}
		public override bool UseItem(Player player)
		{
		NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SpiderQueen"));
		Main.PlaySound(SoundID.Roar, player.position, 0);
		return true;
		}


	}

	public class RoilingSludge : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Roiling Sludge");
			Tooltip.SetDefault("'Ew, Gross!' \nSummons the Murk");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 5);
			recipe.AddIngredient(ItemID.MudBlock, 10);
			recipe.AddIngredient(ItemID.Gel, 30);
			recipe.AddIngredient(ItemID.Bone, 5);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 99;
			item.rare = 2;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.ZoneJungle && !NPC.AnyNPCs(mod.NPCType("Murk")) && !NPC.AnyNPCs(mod.NPCType("BossFlyMiniboss1"))){
			return true;
			}else{
			Main.NewText("There is a lack of mud and sludge for Murk to even exist here...",40, 180, 60);
			return false;

			}
		}

		public override bool UseItem(Player player)
		{
			if (player.ZoneJungle && !NPC.AnyNPCs(mod.NPCType("Murk")) && !NPC.AnyNPCs(mod.NPCType("BossFlyMiniboss1"))){
			NPC.SpawnOnPlayer(player.whoAmI,mod.NPCType(SGAWorld.downedMurk==0 ? "BossFlyMiniboss1" : "Murk"));
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		return false;
		}


	}

	public class Prettygel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminous Gel");
			Tooltip.SetDefault("Makes pinky very JELLLLYYYYY");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = 4;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.value = 0;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player)
		{
			if (!NPC.AnyNPCs(mod.NPCType("SPinky")) && !NPC.AnyNPCs(50) && !Main.dayTime)
			{
			return true;
			}else{
			Main.NewText("this gel shimmers only in moonlight...",100, 40, 100);
			return false;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (SGAmod.Calamity)
			tooltips.Add(new TooltipLine(mod, "NoU", "Summoning this boss with automatically disable Revengence and Death Modes"));
		}

		public override bool UseItem(Player player)
		{
		if (item.consumable==true && !Main.dayTime){
				SGAmod.CalamityNoRevengenceNoDeathNoU();
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SPinky"));
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			//player.GetModPlayer<SGAPlayer>().Locked=new Vector2(player.Center.X-2000,4000);
}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 4);
			recipe.AddIngredient(3111, 10); //pink gel
			recipe.AddTile(220); //Soldifier
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(mod.ItemType("IlluminantEssence"), 2); //pink gel
			recipe.AddIngredient(mod.ItemType("MurkyGel"), 10); //pink gel
			recipe.AddTile(220); //Soldifier
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	public class Nineball : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nineball");
			Tooltip.SetDefault("Summons the strongest ice fairy");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = 4;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.value = 0;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player)
		{
			if (!NPC.AnyNPCs(mod.NPCType("Cirno")))
			{
				if (!Main.dayTime || !player.ZoneSnow)
				{
					item.consumable = false;
				}
				else
				{
					item.consumable = true;
				}
				return true;
			}
			else
			{
				return false;
			}
		}
		public override bool UseItem(Player player)
		{
			if (item.consumable == false)
			{
				Main.NewText("It's power lies in the snow biome during the day", 50, 50, 250);
			}
			else
			{
				NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Cirno"));
				Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddIngredient(mod.ItemType("IceFairyDust"), 9);
			recipe.AddTile(TileID.IceMachine); //IceMachine
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class Mechacluskerf : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Clusterfuck");
			Tooltip.SetDefault("Summons the Twin-Prime-Destroyers\nIt is highly encourged you do not fight this before late hardmode...");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = 4;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.value = 0;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
		}

		public override bool CanUseItem(Player player)
		{
			if (!NPC.AnyNPCs(mod.NPCType("TPD")) && !NPC.AnyNPCs(50))
			{
				if (Main.dayTime)
				{
					item.consumable = false;
				}
				else
				{
					item.consumable = true;
				}
				return true;
			}
			else
			{
				return false;
			}
		}
		public override bool UseItem(Player player)
		{
			if (item.consumable == false || Main.dayTime)
			{
				Main.NewText("Their terror only rings at night", 150, 5, 5);
			}
			else
			{
				NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TPD"));
				Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(544, 1);
			recipe.AddIngredient(556, 1);
			recipe.AddIngredient(557, 1);
			recipe.AddIngredient(547, 3);
			recipe.AddIngredient(548, 3);
			recipe.AddIngredient(549, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

}
