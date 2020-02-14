using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.HavocGear.Items
{

	public class VirulentBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Virulent Bar");
			Tooltip.SetDefault("Condensed life essence in bar form");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.maxStack = 99;
			item.value = 250;
			item.rare = 5;
			item.alpha = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "BiomassBar");
			recipe.AddIngredient(null, "VirulentOre", 3);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
		public override string Texture
		{
			get { return ("SGAmod/Items/VirulentBar"); }
		}

	}
	public class VirulentOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Virulent Ore");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 99;
			item.value = 100;
			item.rare = 5;
			item.alpha = 0;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("VirulentOre");
		}
		public override string Texture
		{
			get { return ("SGAmod/Items/VirulentOre"); }
		}

	}
	public class BiomassBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomass Bar");
			Tooltip.SetDefault("'Hardened plant matter', in a bar");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.maxStack = 99;
			item.value = 100;
			item.rare = 1;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/BiomassBar"); }
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Biomass", 2);
			recipe.AddIngredient(null, "MurkyGel",1);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
	}
	public class Biomass : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("Biomass");
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Biomass"); }
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomass");
			Tooltip.SetDefault("'Hardened plant matter'");
		}

	}
	public class MurkyGel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Murky Gel");
			Tooltip.SetDefault("Extra sticky, stinky too");
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/MurkyGel"); }
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 50;
			item.rare = 2;
		}
	}
}

namespace SGAmod.Items
{
	public class IceFairyDust: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Fairy Dust");
			Tooltip.SetDefault("It doesn't feel like it's from this universe");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = 5000;
			item.rare = 5;
		}
	}
	public class VialofAcid : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vial of Acid");
			Tooltip.SetDefault("Highly Corrosive");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 16;
			item.height = 16;
			item.value = 4000;
			item.rare = 2;
		}
	}
	public class AdvancedPlating : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Advanced Plating");
			Tooltip.SetDefault("Advanced for the land of Terraria's standards, that is");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = 50000;
			item.rare = 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("UnmanedBar"), 2);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this,2);
			recipe.AddRecipe();
		}
	}
	public class PlasmaCell : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasma Cell");
			Tooltip.SetDefault("Heated plasmic energy resides within");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = Item.sellPrice(0,0,75,0);
			item.rare = 8;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 2);
			recipe.AddIngredient(mod.ItemType("WraithFragment4"), 2);
			recipe.AddIngredient(ItemID.MartianConduitPlating, 10);
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(mod.ItemType("VialofAcid"), 3);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
	public class CryostalBar: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cryostal Bar");
			Tooltip.SetDefault("Condensed ice magic has formed into this bar");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = Item.sellPrice(0,0,25,0);
			item.rare = 5;
		}
	}
	public class EldritchTentacle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eldritch Tentacle");
			Tooltip.SetDefault("Remains of an eldritch deity\nMy be used alongside fragments to craft all of Moonlord's drops");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 50, 0);
			item.rare = 9;
		}
	}	public class IlluminantEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Illuminant Essence");
			Tooltip.SetDefault("Shards of Heaven\nSometimes drops from specific hallow enemies after Moonlord is defeated");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.HotPink.ToVector3() * 0.55f * Main.essScale);
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 11;
		}
	}

public class LunarRoyalGel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Royal Gel");
			Tooltip.SetDefault("From the moon-infused Pinky");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 10));
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 16;
			item.height = 16;
			item.value = 10000;
			item.rare = 9;
		}
	}

	public class CosmicFragment: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Fragment");
			Tooltip.SetDefault("The core of a celestial experiment; it holds unmatched power\nUsed to make Dev items");
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 16;
			item.height = 16;
			item.value = 10;
			item.rare = 9;
			item.expert=true;
		}

		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange *= 5;
		}

		public override bool GrabStyle(Player player)
		{
			Vector2 vectorItemToPlayer = player.Center - item.Center;
			Vector2 movement = vectorItemToPlayer.SafeNormalize(default(Vector2)) * 0.1f;
			item.velocity = item.velocity + movement;
			item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
			return true;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
		}

	}

	public class EmptyCharm: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Empty Charm");
			Tooltip.SetDefault("An empty charm necklace, ready for enchanting");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 20;
			item.height = 20;
			item.value = 10000;
			item.rare = 0;
			item.consumable = false;
		}
	}

	public class StarMetalMold: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Metal Mold");
			Tooltip.SetDefault("A mold used to make Wraith Cores, it seems fit to mold bars from heaven\nIs not consumed in crafting Star Metal Bars");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 20;
			item.height = 20;
			item.value = 0;
			item.rare = 8;
			item.consumable = false;
		}
	}

	public class StarMetalBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Metal Bar");
			Tooltip.SetDefault("A bar made from the remnants of the cosmos");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 20;
			item.height = 20;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = 9;
			item.consumable = false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new StarMetalRecipes(mod);
			recipe.AddIngredient(mod.ItemType("StarMetalMold"), 1);
			recipe.AddIngredient(ItemID.LunarOre, 1);
			recipe.AddRecipeGroup("SGAmod:CelestialFragments", 4);
			recipe.SetResult(this,4);
			recipe.AddRecipe();
		}

	}
	public class CopperWraithNotch: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Wraith Notch");
			Tooltip.SetDefault("intact remains of the Copper Wraith's animated armor");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 20;
			item.rare = 0;
		}
	}
	public class CobaltWraithNotch: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Wraith Notch");
			Tooltip.SetDefault("intact remains of the Cobalt Wraith's animated armor, stronger than before");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 200;
			item.rare = 5;
		}
	}
	public class LuminiteWraithNotch: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Wraith Notch");
			Tooltip.SetDefault("intact remains of the Luminate Wraith's special armor");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 100000;
			item.rare = 10;
		}
	}
	public class WraithFragment: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Wraith Shard");
			Tooltip.SetDefault("The remains of a weak wraith; it is light and conductive");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 10;
			item.rare = 0;
		}
	}
	public class WraithFragment2: WraithFragment
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tin Wraith Shard");
			Tooltip.SetDefault("The remains of a weak wraith; it is soft and malleable");
		}
	}

	public class WraithFragment3: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bronze Alloy Wraith Shard");
			Tooltip.SetDefault("Tin and copper combined through the fires of a hellforge; thus stronger than a standard shard");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 10;
			item.rare = 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("WraithFragment"), 2);
			recipe.AddIngredient(ItemID.TinOre, 4);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("WraithFragment2"), 2);
			recipe.AddIngredient(ItemID.CopperOre, 4);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 1);
			recipe.AddIngredient(ItemID.LivingFireBlock, 3);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(mod.ItemType("FieryShard"));
			recipe.AddRecipe();

		}
	}

	public class WraithFragment4 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Wraith Shard");
			Tooltip.SetDefault("The remains of a stronger wraith; applyable uses in alloys and highly resistant to corrosion");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 100;
			item.rare = 2;
		}
	}

	public class UnmanedBar: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Novus Bar");
			Tooltip.SetDefault("This alloy of Novus and the power of the wraiths have awakened some of its doment power\nMay be interchanged for iron bars in some crafting recipes");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 16;
			item.height = 16;
			item.value = 100;
			item.rare = 1;
			item.alpha = 0;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("UnmanedBarTile");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("UnmanedOre"), 4);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards",3);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class UnmanedOre: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Novus Ore");
			Tooltip.SetDefault("Stone laden with doment power...");
		}
	public override void SetDefaults()
        {
		item.width = 16;
		item.height = 16;
		item.maxStack = 99;
		item.value = 50;
		item.rare = 1;
		item.alpha = 0;
		item.useTurn = true;
		item.autoReuse = true;
		item.useAnimation = 15;
		item.useTime = 10;
		item.useStyle = 1;
		item.consumable = true;
		item.createTile = mod.TileType("UnmanedOreTile");

	}
	}

	public class PrismalBar: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prismal Bar");
			Tooltip.SetDefault("It radiates the true energy of Novus");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 20;
			item.height = 20;
			item.value = Item.sellPrice(0, 0, 80, 0);
			item.rare = 8;
			item.consumable = true;
			item.createTile = mod.TileType("PrismalBarTile");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PrismalOre"), 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.SetResult(this,1);
			recipe.AddRecipe();
		}

	}

	public class PrismalOre: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prismal Ore");
			Tooltip.SetDefault("The power inside is cracked open, ready to be used");
		}
	public override void SetDefaults()
        {
		item.width = 16;
		item.height = 16;
		item.maxStack = 99;
		item.value = 5000;
		item.rare = 8;
		item.alpha = 0;
		item.useTurn = true;
		item.autoReuse = true;
		item.useAnimation = 15;
		item.useTime = 10;
		item.useStyle = 1;
		item.consumable = true;
		item.createTile = mod.TileType("PrismalTile");

	}
		public override string Texture
		{
			get { return ("SGAmod/Items/PrismalOre2"); }
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("UnmanedOre"), 5);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 3);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 1);
			recipe.AddIngredient(mod.ItemType("IceFairyDust"), 1);
			recipe.AddIngredient(mod.ItemType("WraithFragment4"), 3);
			recipe.AddTile(mod.GetTile("PrismalStation"));
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}

	}

	public class PrimordialSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Skull");
			Tooltip.SetDefault("A most sinister looking skull, I wonder what it's for?");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = 500000;
			item.rare = 6;
			item.expert = false;
		}
	}
	public class OmegaSigil : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omega Sigil");
			Tooltip.SetDefault("Proof of one having beaten one of Terraria's strongest foes\nIs not consumed when making Wrath Arrows");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 32;
			item.height = 32;
			item.value = 1500000;
			item.rare = 10;
		}
	}

}

