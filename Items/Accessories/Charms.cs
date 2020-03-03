using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Idglibrary;

namespace SGAmod.Items.Accessories
{
	public class MiningCharmlv1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mining Charm Tier 1");
			Tooltip.SetDefault("25% increased mining/hammering/chopping speed\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 1, 0, 0); ;
			item.rare = 2;
			item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			bool canequip = true;
			for (int x = 3; x < 8 + player.extraAccessorySlots; x++)
			{
				if (player.armor[x].modItem != null) {
					Type myclass = player.armor[x].modItem.GetType();
					if (myclass.BaseType == typeof(MiningCharmlv1) || myclass == typeof(MiningCharmlv1)) {

						//if (myType==mod.ItemType("MiningCharmlv1") || myType==mod.ItemType("MiningCharmlv2") || myType == mod.ItemType("MiningCharmlv3")){
						canequip = false;
						break;
					} } }
			return canequip;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.UseTimeMulPickaxe += 0.25f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.CopperPickaxe, 1);
			recipe.AddIngredient(mod.ItemType("EmptyCharm"), 1);
			recipe.AddIngredient(mod.ItemType("CopperWraithNotch"), 3);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards", 8);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 15);
			recipe.AddRecipeGroup("SGAmod:Tier4Bars", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class EnhancingCharmlv1 : MiningCharmlv1
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enhancing Charm Tier 1");
			Tooltip.SetDefault("buffs are 25% longer and debuffs are 25% shorter\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 1, 0, 0); ;
			item.rare = 2;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.EnhancingCharm += 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.BottledWater, 10);
			recipe.AddIngredient(mod.ItemType("EmptyCharm"), 1);
			recipe.AddIngredient(mod.ItemType("CopperWraithNotch"), 3);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards", 8);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 15);
			recipe.AddRecipeGroup("SGAmod:Tier4Bars", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}


	public class AnticipationCharmlv1 : MiningCharmlv1
	{

		public override bool Autoload(ref string name)
		{
			return false;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anticipation Charm Tier 1");
			Tooltip.SetDefault("When a boss fight starts, you are healed by 100 HP, but only every 2 minutes and while " + Idglib.ColorText(Color.White, "Anticipation") + " is low" +
				"\nDuring a boss fight, you build up " + Idglib.ColorText(Color.White, "Anticipation") + ", which causes your held weapon to do more damage, this caps at a 25% increase+\n" +
	"You lose half your " + Idglib.ColorText(Color.White, "Anticipation") + " when hurt, and passively drains while no boss is alive\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 1, 0, 0); ;
			item.rare = 2;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.EnhancingCharm += 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.BottledWater, 10);
			recipe.AddIngredient(mod.ItemType("EmptyCharm"), 1);
			recipe.AddIngredient(mod.ItemType("CopperWraithNotch"), 3);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards", 8);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 15);
			recipe.AddRecipeGroup("SGAmod:Tier4Bars", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class EnhancingCharmlv2 : MiningCharmlv1
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enhancing Charm Tier 2");
			Tooltip.SetDefault("buffs are 50% longer and debuffs are 33% shorter\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 2, 50, 0); ;
			item.rare = 5;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.EnhancingCharm += 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("EnhancingCharmlv1"), 1);
			recipe.AddIngredient(mod.ItemType("CobaltWraithNotch"), 15);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 40);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class EnhancingCharmlv3 : MiningCharmlv1
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enhancing Charm Tier 3");
			Tooltip.SetDefault("buffs are 100% longer and debuffs are 50% shorter\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 10, 0, 0); ;
			item.rare = 9;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.EnhancingCharm += 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("EnhancingCharmlv2"), 1);
			recipe.AddIngredient(mod.ItemType("LuminiteWraithNotch"), 5);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 10);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class MiningCharmlv2 : MiningCharmlv1
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mining Charm Tier 2");
			Tooltip.SetDefault("50% increased mining/hammering/chopping speed\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 2, 50, 0); ;
			item.rare = 5;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.UseTimeMulPickaxe += 0.50f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MiningCharmlv1"), 1);
			recipe.AddIngredient(mod.ItemType("CobaltWraithNotch"), 15);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 40);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class MiningCharmlv3 : MiningCharmlv1
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mining Charm Tier 3");
			Tooltip.SetDefault("100% increased mining/hammering/chopping speed\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 10, 0, 0); ;
			item.rare = 9;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.UseTimeMulPickaxe += 1f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MiningCharmlv2"), 1);
			recipe.AddIngredient(mod.ItemType("LuminiteWraithNotch"), 5);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 10);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}


	public class LifeFlower : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Life Flower");
			Tooltip.SetDefault("You consume healing potions instead of dying when taking fatal damage\nEffects of Obsidian Rose");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ManaFlower);
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 2, 50, 0); ;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.LifeFlower = true;
			player.lavaRose = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ObsidianRose, 1);
			recipe.AddIngredient(ItemID.HealingPotion, 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class LifeforceQuintessence : LifeFlower
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lifeforce Quintessence");
			Tooltip.SetDefault("You consume healing potions instead of dying when taking fatal damage and use mana potions when needed\nEffects of Obsidian Rose, 5% reduced mana costs");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ManaFlower);
			item.width = 24;
			item.height = 24;
			item.rare = 6;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			base.UpdateAccessory(player, hideVisual);
			player.manaFlower = true;
			player.manaCost -= 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ManaFlower, 1);
			recipe.AddIngredient(mod.ItemType("LifeFlower"), 1);
			recipe.AddIngredient(ItemID.LifeforcePotion, 1);
			recipe.AddIngredient(ItemID.DD2EnergyCrystal, 15);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class BloodCharmPendant : LifeFlower
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Charm Pendant");
			Tooltip.SetDefault("Increased life regen and length of invincibility after taking damage\nStars rain down after taking damage\nReduced cooldown on potions");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.lifeRegen = 3;
			item.rare = 7;
			item.value = Item.sellPrice(0, 15, 0, 0); ;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.pStone = true;
			player.starCloak = true;
			player.longInvince = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CharmofMyths, 1);
			recipe.AddIngredient(ItemID.StarVeil, 1);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class PortableHive : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Portable Hive");
			Tooltip.SetDefault("Bees become enhanced and very aggressive\nSummons up to five enhanced bees to attack foes\nDamage is based on defense and summoner values\nToggle visibity to enable/disable the agressive bee movement\nEffects of Hive Pack, Honey Comb, and all bees do more damage\nGetting hit may give you the Honeyed Buff");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = Item.sellPrice(gold: 5);
			item.rare = -12;
			item.expert = true;
			item.accessory = true;
			item.defense = 5;
			item.damage = 1;
			item.summon = true;
			item.shieldSlot = 5;
			item.backSlot = 9;
			item.knockBack = 1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HiveBackpack, 1);
			recipe.AddIngredient(ItemID.HoneyComb, 1);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 10);
			recipe.AddTile(TileID.HoneyDispenser);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			flat = (float)((player.statDefense * 0.2) * (0.5f + ((player.minionDamage - 1f) * 8f)));
			base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().beefield = 5;
			if (!hideVisual)
				player.GetModPlayer<SGAPlayer>().beefieldtoggle = 5;

			player.strongBees = true;
			//Item shield = new Item();
			//shield.SetDefaults(ItemID.EoCShield);
			player.bee = true;

			float flat = 0f;
			if (GetType() == typeof(PortableHive))
				flat = (float)((player.statDefense * 0.2) * (0.5f + ((player.minionDamage - 1f) * 8f)));

			if (1f + (flat * 1f) > player.GetModPlayer<SGAPlayer>().beedamagemul)
				player.GetModPlayer<SGAPlayer>().beedamagemul = 1f + (flat * 1f);

		}

	}

	public class DevPower : ModItem
	{

		float[] effects = new float[20];
		float[] effectsangle = new float[20];
		float[] effectrotation = new float[20];
		float[] effectrotationadder = new float[20];
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul of Secrets");
			Tooltip.SetDefault("While worn, it will unlock the true nature of so called 'vanity' Dev Armors in your inventory...\nCombines the effects of Blood Charm Pendant, Lifeforce Quintessence, and Portable Hive (toggle visiblity to disable bee spawning of Portable Hive)");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.lifeRegen = 5;
			item.rare = 12;
			item.damage = 1;
			item.summon = true;
			item.value = Item.sellPrice(3, 0, 0, 0);
			item.accessory = true;
			item.expert = true;
			/*for (int i = 0; i < effects.Length; i += 1)
			{
				effects[i] = i*2f;
				effectsangle[i] = Main.rand.NextFloat(MathHelper.ToRadians(360));
				effectrotation[i] = Main.rand.NextFloat(-2,2f)/10f;
				effectrotationadder[i] = Main.rand.NextFloat((float)-Math.PI, (float)Math.PI);
			}*/
		}

		public override string Texture
		{
			get { return ("Terraria/Extra_57"); }
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{

			Texture2D inner = ModContent.GetTexture("Terraria/Extra_57");

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			for (int i = 0; i < 20; i += 1)
			{
				effectrotationadder[i] += 1f;//effectrotation[i];
				Double Azngle = i;
				Vector2 here = new Vector2((float)Math.Cos(Azngle), (float)Math.Sin(Azngle)) * (i * 2f);
				float scaler = (1f - (float)((float)i / (float)effects.Length));
				spriteBatch.Draw(inner, position + (new Vector2(14f, 14f)) + here, null, Color.Lerp(drawColor, Color.MediumPurple, 0.25f) * scaler, Main.GlobalTime *= (i % 2 == 0 ? -1f : 1f), new Vector2(inner.Width / 2, inner.Height / 2), scale * scaler, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(Main.itemTexture[item.type], position + new Vector2(14f, 14f), frame, drawColor, 0, new Vector2(inner.Width / 2, inner.Height / 2), scale * 1.5f * Main.essScale, SpriteEffects.None, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			return false;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line in tooltips)
			{
				if (line.mod == "Terraria" && line.Name == "ItemName")
				{
					line.overrideColor = Color.Lerp(Color.DarkMagenta, Color.SteelBlue, 0.5f + (float)Math.Sin(Main.GlobalTime * 2f));
				}
			}
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			flat = (float)((player.statDefense * 0.25) * (2f + (player.minionDamage - 1f) * 50f));
			base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().devpower = 3;
			ModContent.GetInstance<BloodCharmPendant>().UpdateAccessory(player, hideVisual);
			ModContent.GetInstance<LifeforceQuintessence>().UpdateAccessory(player, hideVisual);
			ModContent.GetInstance<PortableHive>().UpdateAccessory(player, false);
			if (hideVisual == true)
			{
				player.GetModPlayer<SGAPlayer>().beefield = 3;
			}
			float flat = (float)((player.statDefense * 0.25) * (2f + (player.minionDamage - 1f) * 50f));

			if (1f + (flat * 1f) > player.GetModPlayer<SGAPlayer>().beedamagemul)
				player.GetModPlayer<SGAPlayer>().beedamagemul = 1f + (flat * 1f);


		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("LunarRoyalGel"), 15);
			recipe.AddIngredient(ItemID.ShroomiteBar, 30);
			recipe.AddIngredient(ItemID.SpectreBar, 30);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 30);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 30);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 30);
			recipe.AddIngredient(mod.ItemType("CryostalBar"), 30);
			recipe.AddIngredient(mod.ItemType("EldritchTentacle"), 15);
			recipe.AddIngredient(mod.ItemType("BloodCharmPendant"), 1);
			recipe.AddIngredient(mod.ItemType("LifeforceQuintessence"), 1);
			recipe.AddIngredient(mod.ItemType("PortableHive"), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class GeyserInABottle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Geyser In A Bottle");
			Tooltip.SetDefault("Creates an eruption midair when you jump!\nNo damage taken from Geysers\nGrants a small ammount of lava immunity");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 4;
			item.value = Item.sellPrice(0, 1, 50, 0); ;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			int y_bottom_edge = (int)(player.position.Y + (float)player.height + 16f) / 16;
			int x_edge = (int)(player.Center.X) / 16;

			Tile mytile = Framing.GetTileSafely(x_edge, y_bottom_edge);

			if (mytile.active() && player.velocity.Y == 0)
			{
				player.GetModPlayer<SGAPlayer>().GeyserInABottle = true;

			}
			player.lavaMax += 100;
			player.GetModPlayer<SGAPlayer>().GeyserInABottleActive = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GeyserTrap, 1);
			recipe.AddIngredient(ItemID.Obsidian, 20);
			recipe.AddIngredient(ItemID.CloudinaBottle, 1);
			recipe.AddIngredient(ItemID.LavaBucket, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class IdolOfMidas : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Idol Of Midas");
			Tooltip.SetDefault("One of the many treasures this greed infested abomination stole....\nPicking up coins grants small buffs depending on the coin (hide accessory for defensive/movement buffs, otherwise you get offensive buffs, gold and platinum coins give you both)\nIncreased damage with the more coins you have in your inventory (this caps at 25% at 10 platinum)\n15% increased damage against enemies inflicted with Midas\nShop prices are 20% cheaper\n" + Idglib.ColorText(Color.Red, "Any coins picked up are consumed in the process"));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 52;
			item.rare = 7;
			item.value = Item.sellPrice(1, 0, 0, 0); ;
			item.accessory = true;
			item.expert = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			int coincount = player.CountItem(ItemID.CopperCoin) + (player.CountItem(ItemID.SilverCoin) * 100) + (player.CountItem(ItemID.GoldCoin) * 10000) + (player.CountItem(ItemID.PlatinumCoin) * 1000000);
			float howmuch = Math.Min(0.25f, (coincount / 10000000f) / 4f);
			player.magicDamage += howmuch;
			player.rangedDamage += howmuch;
			player.thrownDamage += howmuch;
			player.minionDamage += howmuch;
			player.meleeDamage += howmuch;
			player.GetModPlayer<SGAPlayer>().MidasIdol = hideVisual ? 2 : 1;
		}

	}

	public class JavelinBaseBundle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bundle of Javelin Bases");
			Tooltip.SetDefault("Improves the damage over time of javelins by 25%\nJavelin damage increased by 10%");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 2;
			item.value = Item.sellPrice(0, 0, 2, 0); ;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().JavelinBaseBundle = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SGAmod:EvilJavelins", 1);
			recipe.AddIngredient(mod.ItemType("IceJavelin"), 1);
			recipe.AddIngredient(mod.ItemType("StoneJavelin"), 1);
			recipe.AddIngredient(mod.ItemType("AmberJavelin"), 1);
			recipe.AddIngredient(ItemID.RopeCoil, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class JavelinSpearHeadBundle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bundle of Javelin Spear Heads");
			Tooltip.SetDefault("The ammount of javelins you can stick into a target increases by 1\nJavelin damage increased by 15%");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 5;
			item.value = Item.sellPrice(0, 0, 2, 0); ;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().JavelinSpearHeadBundle = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SGAmod:EvilJavelins", 1);
			recipe.AddIngredient(mod.ItemType("IceJavelin"), 1);
			recipe.AddIngredient(mod.ItemType("StoneJavelin"), 1);
			recipe.AddIngredient(mod.ItemType("AmberJavelin"), 1);
			recipe.AddIngredient(mod.ItemType("ShadowJavelin"), 1);
			recipe.AddIngredient(mod.ItemType("PearlWoodJavelin"), 1);
			recipe.AddIngredient(ItemID.RopeCoil, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class JavelinBundle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bundle of Javelin Parts");
			Tooltip.SetDefault("'Worthless money-wise, but won't discount your life'\nThe ammount of javelins you can stick into a target increases by 1\nImproves the damage over time of javelins by 25%\nJavelin damage increased by 25%\nDoesn't stack with the sum of its parts");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 6;
			item.value = Item.sellPrice(0, 0, 4, 0); ;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().JavelinSpearHeadBundle = true;
			player.GetModPlayer<SGAPlayer>().JavelinBaseBundle = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("JavelinSpearHeadBundle"), 1);
			recipe.AddIngredient(mod.ItemType("JavelinBaseBundle"), 1);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 10);
			recipe.AddIngredient(ItemID.RopeCoil, 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class Fieryheart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Heart");
			Tooltip.SetDefault("All attacks inflict a short ammount of Daybroken");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 4));
		}
		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.value = 100000;
			item.rare = -12;
			item.expert = true;
			item.accessory = true;
			item.defense = 4;
			//item.damage = 1;
			item.summon = true;
			item.knockBack = 1f;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().FieryheartBuff = 5;
		}
	}
	public class LunarSlimeHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Slime Heart");
			Tooltip.SetDefault("Heart of the surpreme lunar princess" +
				"\nSummons an array of lunar gels that damage enemies and cancel out projectiles\nWhen a projectile is canceled out or hits an enemy 5 times, the gel explodes into a damaging debuffing nova and grants the player a random buff for 8 seconds\nWhen the gel explodes from canceling out projectiles remains inactive for 10 seconds, otherwise only 6 seconds\nBase damage is based on your defense times the sum of your damage multipliers: (melee+thrown+summon+magic+ranged)*defense\nEach buff the player has grants +1 defense\ndebuffs grant 4 defense");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = Item.sellPrice(platinum: 1);
			item.rare = -12;
			item.expert = true;
			item.accessory = true;
			item.defense = 10;
			item.damage = 1;
			item.knockBack = 1f;
		}


		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			flat = (float)(player.statDefense * (player.minionDamage + player.rangedDamage + player.meleeDamage + player.thrownDamage + player.magicDamage));
			base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().lunarSlimeHeart = true;
		}
	}

	public class OmegaSigil : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omega Sigil");
			Tooltip.SetDefault("Proof of one having beaten one of Terraria's strongest foes\nWhile you have max HP, you gain 100 defense\n10% chance to dodge attacks that would kill you\nIs not consumed when making Wrath Arrows");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().OmegaSigil = true;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/OmegaSigil"); }
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 32;
			item.height = 32;
			item.value = 1500000;
			item.rare = 10;
			item.accessory = true;
		}
	}

	public class PrimordialSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Skull");
			Tooltip.SetDefault("While you have On-Fire!, you gain the Inferno buff and resist 50% contact damage\nFurthermore, enemies who also are on fire will take 25% increased damage from you during this\nImmunity against Thermal Blaze\nA most sinister looking skull, I wonder what else it's for?");
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/PrimordialSkull"); }
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.HasBuff(BuffID.OnFire))
			{
				player.AddBuff(BuffID.Inferno, 2);
				player.GetModPlayer<SGAPlayer>().PrimordialSkull = true;
			}
			player.buffImmune[mod.BuffType("ThermalBlaze")] = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 26;
			item.defense = 5;
			item.accessory = true;
			item.height = 14;
			item.value = 500000;
			item.rare = 6;
			item.expert = false;
		}
	}

	public class AmberGlowSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amber Glow Skull");
			Tooltip.SetDefault("'It seems very much uncorroded by the Spider Queen'\nImmunity against Acid Burn");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.HasBuff(BuffID.OnFire))
			{
				player.AddBuff(BuffID.Inferno, 2);
				player.GetModPlayer<SGAPlayer>().PrimordialSkull = true;
			}
			player.buffImmune[mod.BuffType("AcidBurn")] = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 26;
			item.defense = 0;
			item.accessory = true;
			item.height = 14;
			item.value = 5000;
			item.rare = 2;
			item.expert = false;
		}
	}
	public class DemonSteppers : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demon Steppers");
			Tooltip.SetDefault("'Obligatory Hardmode boots'\nAll effects of Frostspark boots and Lavawaders improved\nImmunity to Thermal Blaze and Acid Burn\nEffects of Primordial Skull\nOn Fire! doesn't hurt you and slightly heals you instead");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.OnFire] = false;
			player.GetModPlayer<SGAPlayer>().NoFireBurn = 3;
			player.accRunSpeed += 6;
			player.rocketBoots += 2;
			player.moveSpeed += 0.25f;
			player.iceSkate = true;
			player.lavaMax += 500;
			player.waterWalk = true;
			player.fireWalk = true;
			player.maxRunSpeed += 0.5f;
			player.runAcceleration += 0.25f;
			ModContent.GetInstance<PrimordialSkull>().UpdateAccessory(player, hideVisual);
			ModContent.GetInstance<AmberGlowSkull>().UpdateAccessory(player, hideVisual);

		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 26;
			item.defense = 0;
			item.accessory = true;
			item.height = 14;
			item.value = Item.sellPrice(gold: 25);
			item.rare = 7;
			item.expert = false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
			recipe.AddIngredient(ItemID.LavaWaders, 1);
			recipe.AddIngredient(ItemID.ObsidianSkull, 1);
			recipe.AddIngredient(mod.ItemType("AmberGlowSkull"), 1);
			recipe.AddIngredient(mod.ItemType("PrimordialSkull"), 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class BoosterMagnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Booster Magnet");
			Tooltip.SetDefault("Attracts Nebula Boosters from a longer range");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().BoosterMagnet = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 3);
			item.rare = 8;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentNebula, 10);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class HeartreachMagnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heartreach Magnet");
			Tooltip.SetDefault("Attracts Hearts from a longer range");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.lifeMagnet = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 2);
			item.rare = 7;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HeartLantern, 1);
			recipe.AddIngredient(ItemID.HeartreachPotion, 5);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class OmniMagnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omni Magnet");
			Tooltip.SetDefault("Increased grab range for Nebula Boosters, Coins, Hearts, and Mana Stars\nEnemies drop coins on hit and NPCs offer you a discount\n'Suck it all up!'");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().BoosterMagnet = true;
			player.manaMagnet = true;
			player.lifeMagnet = true;
			player.coins = true;
			player.discount = true;
			player.goldRing = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(gold: 75);
			item.rare = 10;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CelestialMagnet, 1);
			recipe.AddIngredient(mod.ItemType("BoosterMagnet"), 1);
			recipe.AddIngredient(mod.ItemType("HeartreachMagnet"), 1);
			recipe.AddIngredient(ItemID.GreedyRing, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}


}