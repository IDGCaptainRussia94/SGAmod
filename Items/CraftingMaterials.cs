using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
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
			item.value = 500;
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
			recipe.AddIngredient(null, "Biomass", 5);
			recipe.AddIngredient(null, "MurkyGel",2);
			recipe.AddIngredient(null, "DecayedMoss", 1);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this, 3);
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
	public class DankWood : ModItem
	{
		public override void SetDefaults()
		{
			item.value = 50;
			item.rare = 1;
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dank Wood");
			Tooltip.SetDefault("It smells odd...");
		}

	}
	public class DankCore : ModItem
	{
		public override void SetDefaults()
		{
			item.value = 2500;
			item.rare = 2;
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dank Core");
			Tooltip.SetDefault("'Dark, Dank, Dangerous...'");
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
			item.rare = 3;
		}
	}
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
			item.alpha = 30;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.Orange.ToVector3() * 0.55f * Main.essScale);
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
	public class FrigidShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frigid Shard");
			Tooltip.SetDefault("Raw essence of ice");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = 500;
			item.rare = 1;
		}
		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.Aqua.ToVector3() * 0.25f);
		}
	}	
	public class Fridgeflame : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fridgeflame");
			Tooltip.SetDefault("Alloy of hot and cold essences");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.maxStack = 99;
			item.value = 10000;
			item.rare = 6;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.White.ToVector3() * 0.65f * Main.essScale);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("FrigidShard"), 1);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 1);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
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
	public class OmniSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omni Soul");
			Tooltip.SetDefault("'The essence of essences combined'");
			// ticksperframe, frameCount
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			Item refItem = new Item();
			refItem.SetDefaults(ItemID.SoulofSight);
			item.width = refItem.width;
			item.height = refItem.height;
			item.maxStack = 999;
			item.value = 10000;
			item.rare = 6;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Main.hslToRgb((Main.GlobalTime/3f)%1f, 0.85f, 0.50f);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 1);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddIngredient(ItemID.SoulofFlight, 1);
			recipe.AddIngredient(ItemID.SoulofFright, 1);
			recipe.AddIngredient(ItemID.SoulofMight, 1);
			recipe.AddIngredient(ItemID.SoulofSight, 1);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this, 3);
			recipe.AddRecipe();
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Main.hslToRgb((Main.GlobalTime / 3f)%1f, 0.85f, 0.80f).ToVector3() * 0.55f * Main.essScale);
		}
	}
	public class Entrophite : ModItem
	{
		public override void SetDefaults()
		{
			item.value = 5000;
			item.rare = 7;
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Entrophite");
			Tooltip.SetDefault("Corrupted beyond the veils of life");
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
			item.value = 1000;
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
	public class ManaBattery : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mana Battery");
			Tooltip.SetDefault("Capsulated mana to be used as a form of energy for techno weapons");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 16;
			item.height = 26;
			item.value = 15000;
			item.rare = 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 2);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 3);
			recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddIngredient(ItemID.RecallPotion, 1);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this, 1);
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
			item.value = Item.sellPrice(0,0,50,0);
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
			Tooltip.SetDefault("Remains of an eldritch deity\nMay be used alongside fragments to craft all of Moonlord's drops");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 50, 0);
			item.rare = 9;
		}
	}	
	public class IlluminantEssence : ModItem
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
			item.value = Item.sellPrice(0, 0, 50, 0);
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
			item.value = 100000;
			item.rare = 9;
		}
	}
	public class AncientFabricItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Fabric");
			Tooltip.SetDefault("Strands of Reality, predating back to the Big Bang");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, Color.DarkRed.ToVector3() * 0.15f * Main.essScale);
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = Item.sellPrice(0, 0, 25, 0);
			item.rare = 10;
		}
	}

	public class WatchersOfNull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("01001110 01010101 01001100 01001100");
			Tooltip.SetDefault("'Essence of N0ll Watchers, watching...'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 13));
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 32;
			item.height = 32;
			item.value = 100000;
			item.rare = 10;
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
			item.value = 0;
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
			item.value = Item.sellPrice(0, 0, 25, 0);
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
	public class DrakeniteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Drakenite Bar");
			Tooltip.SetDefault("A Bar forged from the same powers that created Draken...");
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
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line in tooltips)
			{
				if (line.mod == "Terraria" && line.Name == "ItemName")
				{
					line.overrideColor = Color.Lerp(Color.DarkGreen, Color.White, 0.5f + (float)Math.Sin(Main.GlobalTime * 8f));
				}
			}
		}
		public static Texture2D[] staticeffects = new Texture2D[32];
		public static void CreateTextures()
		{
			if (!Main.dedServ)
			{
				Texture2D atex = ModContent.GetTexture("SGAmod/Items/DrakeniteBarHalf");
				int width = atex.Width; int height = atex.Height;
				for (int index = 0; index < staticeffects.Length; index++)
				{
					Texture2D tex = new Texture2D(Main.graphics.GraphicsDevice, width, height);

					var datacolors2 = new Color[atex.Width * atex.Height];
					atex.GetData(datacolors2);
					tex.SetData(datacolors2);

					DrakeniteBar.staticeffects[index] = new Texture2D(Main.graphics.GraphicsDevice, width, height);
					Color[] dataColors = new Color[atex.Width * atex.Height];


					for (int y = 0; y < height; y++)
					{
						for (int x = 0; x < width; x += 1)
						{
							if (Main.rand.Next(0, 16) == 1)
							{
								int therex = (int)MathHelper.Clamp((x), 0, width);
								int therey = (int)MathHelper.Clamp((y), 0, height);
								if (datacolors2[(int)therex + therey * width].A > 0)
								{

									dataColors[(int)therex + therey * width] = Main.hslToRgb(Main.rand.NextFloat(0f, 1f) % 1f, 0.6f, 0.8f) * (0.5f);
								}
							}
							if (Main.rand.Next(0, 8) > Math.Abs(x-(index-8)))
							{
								int therex = (int)MathHelper.Clamp((x), 0, width);
								int therey = (int)MathHelper.Clamp((y), 0, height);
								if (datacolors2[(int)therex + therey * width].A > 0)
								{
									dataColors[(int)therex + therey * width] = Main.hslToRgb(((float)(index-8)/ (float)width) % 1f, 0.9f, 0.75f)*(0.80f*(1f-(Math.Abs((float)x - ((float)index -8f))/8f)));
								}
							}


						}

					}

					DrakeniteBar.staticeffects[index].SetData(dataColors);
				}
			}

		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor,
	Color itemColor, Vector2 origin, float scale)
		{
			if (!Main.gameMenu)
			{
				Texture2D texture = DrakeniteBar.staticeffects[(int)(Main.GlobalTime*20f)%DrakeniteBar.staticeffects.Length];
				Vector2 slotSize = new Vector2(52f, 52f);
				position -= slotSize * Main.inventoryScale / 2f - frame.Size() * scale / 2f;
				Vector2 drawPos = position + slotSize * Main.inventoryScale / 2f;
				Vector2 textureOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
				spriteBatch.Draw(texture, drawPos, null, drawColor, 0f, textureOrigin, Main.inventoryScale*2f, SpriteEffects.None, 0f);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(mod.ItemType("ByteSoul"), 10);
			recipe.AddIngredient(mod.ItemType("WatchersOfNull"), 1);
			recipe.AddIngredient(mod.ItemType("AncientFabricItem"), 5);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this, 1);
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
			item.value = 50;
			item.rare = 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("WraithFragment"), 2);
			recipe.AddIngredient(ItemID.TinOre, 4);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this,2);
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
			recipe.SetResult(this,2);
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
	public class MoneySign : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raw Avarice");
			Tooltip.SetDefault("'pure greed'");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 75000;
			item.rare = 10;
		}
	}

	public class ByteSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul of Byte");
			Tooltip.SetDefault("'remains of the Hellion Core'");
		}
		public override string Texture
		{
			get { return ("Terraria/Item_"+Main.rand.Next(0,2000)); }
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Main.hslToRgb(Main.rand.NextFloat(0f, 1f), 0.75f, 0.65f);
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 14;
			item.height = 14;
			item.value = 10000;
			item.rare = 10;
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
			item.value = Item.sellPrice(0, 0, 40, 0);
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
			Tooltip.SetDefault("The power inside is cracked wide open, ready to be used");
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
			recipe.AddIngredient(mod.ItemType("UnmanedOre"), 15);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 1);
			recipe.AddIngredient(mod.ItemType("WraithFragment4"), 1);
			recipe.AddIngredient(mod.ItemType("Fridgeflame"), 3);
			recipe.AddIngredient(mod.ItemType("OmniSoul"), 2);
			recipe.AddIngredient(ItemID.CrystalShard, 3);
			recipe.AddIngredient(ItemID.BeetleHusk, 1);
			recipe.AddTile(mod.GetTile("PrismalStation"));
			recipe.SetResult(this, 15);
			recipe.AddRecipe();
		}

	}
		public class CaliburnCompess : ModItem
		{
			private float effect = 0;
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("Caliburn Compass");
			Tooltip.SetDefault("While held, it points to Caliburn Alters in your world");

		}

		public override void SetDefaults()
			{
				item.width = 24;
				item.height = 24;
				item.rare = 2;
			}
		}

	public class EntropyTransmuter : ModItem
	{
		public override void SetDefaults()
		{
			item.value = 0;
			item.rare = 7;
			item.width = 16;
			item.height = 16;
			item.maxStack = 1;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "entropy", "Entropy Collected: "+Main.LocalPlayer.GetModPlayer<SGAPlayer>().entropycollected+"/100000"));
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Entropy Transmuter");
			Tooltip.SetDefault("As enemies die near you, the Transmuter absorbs their life essences\nWhich converts Converts Demonite or Crimtane ore in your inventory into Entrophite\nConverts a maximum of 20 per full charge");
		}

	}

	public class EALogo : ModItem
	{

		public override void SetDefaults()
		{
			item.value = 1000000;
			item.rare = 9;
			item.width = 16;
			item.height = 16;
			item.maxStack = 1;
			item.expert = true;
		}

		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<SGAPlayer>().EALogo = true;
			if (player.taxMoney >= Item.buyPrice(0, 10, 0, 0))
			{
				player.taxMoney = 0;
				player.QuickSpawnItem(ItemID.GoldCoin,10);
			}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EA Logo");
			Tooltip.SetDefault("Lets you charge maximum mirco-transactions against your town NPCs\nWhile in your inventory: you can reforge unique prefixes for accessories\nYou automatically collect taxes while you have a Tax Collector\nPicking up Hearts and Mana Stars gives you money\nPress the 'Collect Taxes' hotkey to collect a gold coin from your tax collector's purse\n'EA! It's NOT in the game, that's DLC!'");
		}

	}

}

