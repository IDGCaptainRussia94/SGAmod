using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SGAmod.HavocGear.Items.Accessories;
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
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.Green;
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
			recipe.AddIngredient(mod.ItemType("EmptyCharm"), 1);
			recipe.AddIngredient(ItemID.Gel, 10);
			recipe.AddIngredient(ItemID.CopperPickaxe, 1);
			recipe.AddIngredient(mod.ItemType("CopperWraithNotch"), 2);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards", 15);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 8);
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
			Tooltip.SetDefault("buffs are 25% longer and debuffs are 25% shorter\nDoes not affect Potion Sickness\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 1, 0, 0); ;
			item.rare = ItemRarityID.Green;
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
			recipe.AddIngredient(mod.ItemType("EmptyCharm"), 1);
			recipe.AddIngredient(ItemID.Gel, 10);
			recipe.AddIngredient(ItemID.LesserHealingPotion, 5);
			recipe.AddIngredient(mod.ItemType("CopperWraithNotch"), 2);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards", 15);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}


	public class AnticipationCharmlv1 : MiningCharmlv1
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anticipation Charm Tier 1");
			Tooltip.SetDefault("When a boss fight starts, you are healed by 100 HP, but only every 2 minutes and while " + Idglib.ColorText(Color.Green, "Anticipation") + " is low" +
				"\nDuring a boss fight, you build up " + Idglib.ColorText(Color.Green, "Anticipation") + ", which causes your held weapon to do more damage, this caps at a 25% increase\n" +
	"You lose half your " + Idglib.ColorText(Color.Green, "Anticipation") + " when hurt, and passively drains while no boss is alive\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 1, 0, 0); ;
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.anticipationLevel = 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("EmptyCharm"), 1);
			recipe.AddIngredient(ItemID.Gel, 10);
			recipe.AddIngredient(ItemID.CopperBroadsword, 1);
			recipe.AddIngredient(mod.ItemType("CopperWraithNotch"), 2);
			recipe.AddRecipeGroup("SGAmod:BasicWraithShards", 15);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class AnticipationCharmlv2 : MiningCharmlv1
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anticipation Charm Tier 2");
			Tooltip.SetDefault("When a boss fight starts, you are healed by 200 HP, but only every 2 minutes and while " + Idglib.ColorText(Color.Green, "Anticipation") + " is low" +
				"\nDuring a boss fight, you build up " + Idglib.ColorText(Color.Green, "Anticipation") + ", which causes your held weapon to do more damage, this caps at a 50% increase\n" +
	"You lose half your " + Idglib.ColorText(Color.Green, "Anticipation") + " when hurt, and passively drains while no boss is alive\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 2, 50, 0);
			item.rare = ItemRarityID.Pink;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.anticipationLevel = 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AnticipationCharmlv1"), 1);
			recipe.AddIngredient(mod.ItemType("CobaltWraithNotch"), 15);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 10);
			recipe.AddIngredient(mod.ItemType("Fridgeflame"), 6);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class AnticipationCharmlv3 : MiningCharmlv1
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anticipation Charm Tier 3");
			Tooltip.SetDefault("When a boss fight starts, you are healed by 300 HP, but only every 2 minutes and while " + Idglib.ColorText(Color.Green, "Anticipation") + " is low" +
				"\nDuring a boss fight, you build up " + Idglib.ColorText(Color.Green, "Anticipation") + ", which causes your held weapon to do more damage, this caps at a near 100% increase\n" +
	"You lose half your " + Idglib.ColorText(Color.Green, "Anticipation") + " when hurt, and passively drains while no boss is alive\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Cyan;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			sgaplayer.anticipationLevel = 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AnticipationCharmlv2"), 1);
			recipe.AddIngredient(mod.ItemType("LuminiteWraithNotch"), 2);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 10);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class EnhancingCharmlv2 : MiningCharmlv1
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enhancing Charm Tier 2");
			Tooltip.SetDefault("buffs are 50% longer and debuffs are 33% shorter\nDoes not affect Potion Sickness\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 2, 50, 0);
			item.rare = ItemRarityID.Pink;
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
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 10);
			recipe.AddIngredient(mod.ItemType("Fridgeflame"), 6);
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
			Tooltip.SetDefault("buffs are 100% longer and debuffs are 50% shorter\nDoes not affect Potion Sickness\nCan only wear 1 Charm at a time");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Cyan;
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
			recipe.AddIngredient(mod.ItemType("LuminiteWraithNotch"), 2);
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
			item.value = Item.sellPrice(0, 2, 50, 0);
			item.rare = ItemRarityID.Pink;
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
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 10);
			recipe.AddIngredient(mod.ItemType("Fridgeflame"), 6);
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
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Cyan;
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
			recipe.AddIngredient(mod.ItemType("LuminiteWraithNotch"), 2);
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
			item.faceSlot = 6;
			item.rare+=1;
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
			Tooltip.SetDefault("You consume healing potions instead of dying when taking fatal damage\nUse mana potions when needed\nEffects of Obsidian Rose, 5% reduced mana costs");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ManaFlower);
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.LightPurple;
			item.faceSlot = 6;
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

	public class YoyoGauntlet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yoyo Gauntlet");
			Tooltip.SetDefault("15% increased melee damage and melee speed, inflict OnFire! on hit, and +5 armor penetration\nYoyo Bag Effect and fabulous rainbow strings!");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.lifeRegen = 3;
			item.rare = ItemRarityID.Lime;
			item.value = Item.sellPrice(0, 15, 0, 0); ;
			item.accessory = true;
			item.stringColor = 27;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.kbGlove = true;
			player.meleeSpeed += 0.15f;
			player.meleeDamage += 0.15f;
			player.magmaStone = true;
			player.armorPenetration += 5;
			player.counterWeight = 556 + Main.rand.Next(6);
			player.yoyoGlove = true;
			player.yoyoString = true;


		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FireGauntlet, 1);
			recipe.AddIngredient(ItemID.YoyoBag, 1);
			recipe.AddIngredient(ItemID.RainbowString, 1);
			recipe.AddIngredient(ItemID.SharkToothNecklace, 1);
			recipe.AddIngredient(mod.ItemType("SharkTooth"), 50);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	public class Photosynthesizer : MudAbsorber
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Photosynthesizer");
			Tooltip.SetDefault("Increased life regen while on the surface at day\n10% of the sum of all damage types is added to your current weapon's attack\nBeing near mud greatly increases your stats and life regen");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Lime;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.accessory = true;
			item.expert = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			base.UpdateAccessory(player,hideVisual);
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.Dankset = 3;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddIngredient(mod.ItemType("DankCore"), 2);
			recipe.AddIngredient(mod.ItemType("DankWoodHelm"), 1);
			recipe.AddIngredient(mod.ItemType("DankWoodChest"), 1);
			recipe.AddIngredient(mod.ItemType("DankLegs"), 1);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
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
			item.rare = ItemRarityID.Lime;
			item.value = Item.sellPrice(0, 15, 0, 0);
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
			item.backSlot = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HiveBackpack, 1);
			recipe.AddIngredient(ItemID.HoneyComb, 1);
			recipe.AddIngredient(ItemID.CrispyHoneyBlock, 10);
			recipe.AddIngredient(ItemID.BeeWax, 10);
			recipe.AddIngredient(ItemID.HoneyBucket, 2);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 10);
			recipe.AddTile(TileID.TinkerersWorkbench);
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
		float[] effectrotationadder = new float[20];
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul of Secrets");
			Tooltip.SetDefault("While worn, it will unlock the true nature of so called 'vanity' Dev Armors in your inventory...\nCombines the effects of:\n-Blood Charm Pendant\n-Lifeforce Quintessence\n-Havoc's Fragmented Remains\n-Portable Hive\ntoggle visiblity to disable bee spawning of Portable Hive");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.lifeRegen = 5;
			item.rare = 12;
			item.damage = 1;
			item.summon = true;
			item.value = Item.sellPrice(5, 0, 0, 0);
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
			ModContent.GetInstance<Havoc>().UpdateAccessory(player, false);
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
			recipe.AddIngredient(mod.ItemType("LunarRoyalGel"), 25);
			recipe.AddIngredient(mod.ItemType("MoneySign"), 15);
			recipe.AddIngredient(mod.ItemType("ByteSoul"), 50);
			recipe.AddIngredient(ItemID.ShroomiteBar, 30);
			recipe.AddIngredient(ItemID.SpectreBar, 30);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 30);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 30);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 30);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 100);
			recipe.AddIngredient(mod.ItemType("EldritchTentacle"), 15);
			recipe.AddIngredient(mod.ItemType("BloodCharmPendant"), 1);
			recipe.AddIngredient(mod.ItemType("LifeforceQuintessence"), 1);
			recipe.AddIngredient(mod.ItemType("Havoc"), 1);
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
			item.rare = ItemRarityID.LightRed;
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
			recipe.AddIngredient(ItemID.Obsidian, 10);
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
			Tooltip.SetDefault("One of the many treasures this greed infested abomination stole....\nPicking up coins grants small buffs depending on the coin\ndefensive/movement buffs while on the left side of your world, offensive buffs on the right, gold and platinum coins give you both\nIncreased damage with the more coins you have in your inventory (this caps at 25% at 10 platinum)\n15% increased damage against enemies inflicted with Midas\nShop prices are 20% cheaper\n" + Idglib.ColorText(Color.Red, "Any coins picked up are consumed in the process"));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 52;
			item.rare = ItemRarityID.Lime;
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
			if (GetType()==typeof(CorperateEpiphany))
			player.GetModPlayer<SGAPlayer>().MidasIdol = hideVisual ? 3 : player.Center.X > (Main.maxTilesX * 16) / 2f ? 2 : 1;
			else
			player.GetModPlayer<SGAPlayer>().MidasIdol = player.Center.X>(Main.maxTilesX*16)/2f ? 2 : 1;
		}

	}

	public class CorperateEpiphany : IdolOfMidas
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corperate Epiphany");
			Tooltip.SetDefault("'Money Money Money!'\n'Must be funny? In a rich man's world?'\n" +
				"Combined Effects of Idol of Midas and Omni-Magnet (hide to disable Idol of Midas's coin collecting)\n" +
				"EALogo's ability while worn as an accessory");
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/EALogo"); }
		}


		public override Color? GetAlpha(Color lightColor)
		{
			return Main.hslToRgb((Main.GlobalTime*1.443f)%1f,0.8f,0.75f);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line in tooltips)
			{
				if (line.mod == "Terraria" && line.Name == "ItemName")
				{
					line.overrideColor = Color.Lerp(Color.DarkRed, Color.Gold, 0.5f + (float)Math.Sin(Main.GlobalTime * 1f));
				}
			}
		}

		public override void SetDefaults()
		{
			item.value = 3000000;
			item.rare = ItemRarityID.Purple;
			item.width = 24;
			item.height = 24;
			item.accessory = true;
			item.expert = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			base.UpdateAccessory(player, hideVisual);
			ModContent.GetInstance<OmniMagnet>().UpdateAccessory(player, hideVisual);
			ModContent.GetInstance<EALogo>().UpdateInventory(player);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("IdolOfMidas"), 1);
			recipe.AddIngredient(mod.ItemType("EALogo"), 1);
			recipe.AddIngredient(mod.ItemType("OmniMagnet"), 1);
			recipe.AddIngredient(mod.ItemType("MoneySign"), 15);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 200);
			recipe.AddIngredient(mod.ItemType("CalamityRune"), 2);
			recipe.AddIngredient(ItemID.GoldDust, 200);
			recipe.AddIngredient(ItemID.PlatinumCoin, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}


public class BlinkTechGear : IdolOfMidas
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tech Master's Gear");
			Tooltip.SetDefault("'Mastery over your advancements has led you to create this highly advanced suit'\nAllows you to blink teleport up to 6 seconds of Chaos State (hide accessory to disable blinking)\nHold UP and press left or right to blink, this gives you 2 seconds of chaos state\n10% increased Trap damage, 15% increased Trap armor penetration\n+5 Booster Recharge Rate and 15% increased Booster Capacity\nGrants the effects of:\n-Master Ninja Gear and Fridgeflame Canister\n-Jindosh Buckler (Both Evil Types)\n-Putrid Scene and Flesh Kunckles (only one needed to craft)");
		}

		public override void SetDefaults()
		{
			item.value = 1500000;
			item.rare = ItemRarityID.Red;
			item.width = 24;
			item.defense = 12;
			item.height = 24;
			item.accessory = true;
			item.handOnSlot = 11;
			item.handOffSlot = 6;
			item.shoeSlot = 14;
			item.waistSlot = 10;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.blackBelt = true;
			player.dash = 1;
			player.spikedBoots = 2;
			player.meleeDamage += 0.05f; player.magicDamage += 0.05f; player.rangedDamage += 0.05f; player.minionDamage += 0.05f; player.thrownDamage += 0.05f;
			player.meleeCrit += 5; player.magicCrit += 5; player.rangedCrit += 5; player.thrownCrit += 5;
			player.GetModPlayer<SGAPlayer>().maxblink += hideVisual ? 0 : 60 * 6;
			player.GetModPlayer<SGAPlayer>().TrapDamageMul += 0.15f;
			player.GetModPlayer<SGAPlayer>().TrapDamageAP += 0.10f;
			player.GetModPlayer<SGAPlayer>().boosterPowerLeftMax += (int)(10000f * 0.15f);
			player.GetModPlayer<SGAPlayer>().boosterrechargerate += 5;
			ModContent.GetInstance<FridgeFlamesCanister>().UpdateAccessory(player, hideVisual);
			ModContent.GetInstance<JindoshBuckler>().UpdateAccessory(player, hideVisual);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MasterNinjaGear, 1);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddRecipeGroup("SGAmod:HardmodeEvilAccessory",1);
			recipe.AddIngredient(mod.ItemType("BlinkTech"), 1);
			recipe.AddIngredient(mod.ItemType("JindoshBuckler"), 1);
			recipe.AddIngredient(mod.ItemType("FridgeFlamesCanister"), 1);
			recipe.AddIngredient(mod.ItemType("PlasmaCell"), 3);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
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
			item.rare = ItemRarityID.Green;
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
			item.rare = ItemRarityID.Pink;
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
			item.rare = ItemRarityID.LightPurple;
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
			Tooltip.SetDefault("Heart of the supreme lunar princess" +
				"\nSummons an array of lunar gels that damage enemies and nearly nullify projectile damage\nWhen a projectile is dampened or hits an enemy 5 times, the gel explodes into a damaging debuffing nova\nThis grants the player a random buff for 8 seconds\nWhen the gel explodes from canceling out projectiles remains inactive for 10 seconds, otherwise only 6 seconds\nBase damage is based on your defense times the sum of your damage multipliers: (melee+thrown+summon+magic+ranged)*defense\nEach buff the player has grants +1 defense\ndebuffs grant 4 defense");
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
			Tooltip.SetDefault("'it's time to put an end...'\n2% increased Apocalyptical chance\nWhile you have max HP, gain an extra 3% Apocalyptical chance\n10% chance to dodge attacks that would kill you\nIs not consumed when making Wrath Arrows");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "accapocalypticaltext", SGAGlobalItem.apocalypticaltext));
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			for (int i = 0; i < player.GetModPlayer<SGAPlayer>().apocalypticalChance.Length; i += 1)
			player.GetModPlayer<SGAPlayer>().apocalypticalChance[i] += 2.0;
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
			item.rare = ItemRarityID.Red;
			item.accessory = true;
		}
	}
	public class BrokenImmortalityCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flawed 1mm0rtal1ty Pr0t0call");
			Tooltip.SetDefault("'A fragment of Hellion's sheer power... Too bad it hardly even works'\nGrants 1000 defense! But getting hit causes you to lose it for 5 seconds");
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return lightColor = Main.hslToRgb((Main.GlobalTime / 15f) % 1f, 0.85f, 0.45f);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().BIP = true;
			if (!player.HasBuff(mod.BuffType("BIPBuff")))
				player.statDefense += 1000;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/StarMetalMold2"); }
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("ByteSoul"), 100);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 50);
			recipe.AddIngredient(mod.ItemType("WraithFragment4"), 100);
			recipe.AddIngredient(mod.ItemType("EldritchTentacle"), 25);
			recipe.AddIngredient(ItemID.FallenStar, 15);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 32;
			item.height = 32;
			item.value = 1500000;
			item.rare = ItemRarityID.Red;
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
			item.rare = ItemRarityID.LightPurple;
			item.expert = false;
		}
	}

	public class AmberGlowSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corroded Skull");
			Tooltip.SetDefault("'It seems suprisingly intact, yet corroded by the Spider Queen'\nGrants immunity against Acid Burn");
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
			item.rare = ItemRarityID.Green;
			item.expert = false;
		}
	}

	public class CalamityRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Calamity Rune");
			Tooltip.SetDefault("'The unrelenting heat of dismay shimmers, charged within this rune'\nApocalyptical Strength increased by 25%\nYou create fiery explosions on enemies when you score an Apocalyptical\nThese hit only once her enemy and inflict Thermal Blaze, short Daybreak, and Everlasting Suffering\nEverlasting Suffering increases enemy damage over time by 250% and makes other debuffs last until it ends\nDamage done, as well as Daybreak and Everlasting Suffering duration is boosted by your Apocalyptical Strength");
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/CalamityRune"); }
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().apocalypticalStrength += 0.25f;
			player.GetModPlayer<SGAPlayer>().CalamityRune = true;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 16;
			item.height = 16;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Red;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentSolar, 3);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 25);
			recipe.AddIngredient(mod.ItemType("Fridgeflame"), 5);
			recipe.AddIngredient(mod.ItemType("EldritchTentacle"), 3);
			recipe.AddIngredient(ItemID.HellstoneBar, 2);
			recipe.AddIngredient(ItemID.CrystalBall, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}


	}
	public class FridgeFlamesCanister : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fridgeflame Canister");
			Tooltip.SetDefault("Flamethrowers shoot frostflames alongside normal flames\nThis also applies to any weapon that uses gel as its ammo\nFrostflames do not cause immunity frames and do 50% of the weapon's base damage\n5% increased Technological Damage");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().FridgeflameCanister = true;
			player.GetModPlayer<SGAPlayer>().techdamage += 0.05f;
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 16;
			item.height = 16;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.LightPurple;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass, 20);
			recipe.AddIngredient(mod.ItemType("Fridgeflame"), 10);
			recipe.AddIngredient(mod.ItemType("CryostalBar"), 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
	public class BlinkTech : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blink Tech Canister");
			Tooltip.SetDefault("Enables a short ranged blink teleport\nHold UP and press left or right to teleport in the direction\ngives chaos state for 2 seconds, blinking not possible while you have chaos state\n5% increased Technological Damage");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().maxblink += 3;
			player.GetModPlayer<SGAPlayer>().techdamage += 0.05f;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Accessories/Canister4"); }
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 16;
			item.height = 16;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.LightRed;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass, 10);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddIngredient(ItemID.MeteoriteBar, 3);
			recipe.AddIngredient(mod.ItemType("VialofAcid"), 4);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}


	}
	public class HeartOfEntropy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heart of Entropy");
			Tooltip.SetDefault("You spawn vampric healing projectiles when you score an Apocalyptical\nThese heal based on damage done and are boosted by your Apocalyptical Strength\n1% increased Apocalyptical Chance\n'It lashes at at your soul to bring nothing but dismay'");
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "accapocalypticaltext", SGAGlobalItem.apocalypticaltext));
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().HoE = true;
			for (int i = 0; i < player.GetModPlayer<SGAPlayer>().apocalypticalChance.Length; i += 1)
			player.GetModPlayer<SGAPlayer>().apocalypticalChance[i] += 1.0;
		}

		/*public override string Texture
		{
			get { return ("Terraria/Item_"+ItemID.DemonHeart); }
		}*/

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 32;
			item.defense = 0;
			item.accessory = true;
			item.height = 32;
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Lime;
			item.expert = false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LifeCrystal, 1);
			recipe.AddIngredient(mod.ItemType("OmniSoul"), 5);
			recipe.AddIngredient(ItemID.SoulofNight, 15);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 75);
			recipe.AddTile(TileID.CrystalBall);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}	
	public class DemonSteppers : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demon Steppers");
			Tooltip.SetDefault("'Obligatory Hardmode boots'\nAll effects of Frostspark boots and Lavawaders improved\nJump Height significantly boosted, no Fall Damage suffered, and Double Jump ability\nImmunity to Thermal Blaze and Acid Burn\nEffects of Primordial Skull\nOn Fire! doesn't hurt you and slightly heals you instead");
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[BuffID.OnFire] = false;
			player.GetModPlayer<SGAPlayer>().NoFireBurn = 3;
			if (!player.GetModPlayer<SGAPlayer>().demonsteppers)
			{
				player.GetModPlayer<SGAPlayer>().demonsteppers = true;
				player.rocketBoots += 2;
				if (!player.GetModPlayer<SGAPlayer>().Walkmode)
				{
					player.moveSpeed += 0.25f;
					player.maxRunSpeed += 0.5f;
					player.runAcceleration += 0.25f;
					player.accRunSpeed += 6;
				}
				player.iceSkate = true;
				player.lavaMax += 500;
				player.waterWalk = true;
				player.fireWalk = true;
				player.jumpBoost = true;
				player.noFallDmg = true;
				player.autoJump = true;
				player.jumpSpeedBoost += 2.4f;
				player.extraFall += 15;
				player.doubleJumpCloud = true;
			}

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
			item.rare = ItemRarityID.Lime;
			item.expert = false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
			recipe.AddIngredient(ItemID.LavaWaders, 1);
			recipe.AddIngredient(ItemID.BlueHorseshoeBalloon, 1);
			recipe.AddIngredient(ItemID.FrogLeg, 1);
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
			item.rare = ItemRarityID.Yellow;
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
			item.rare = ItemRarityID.Lime;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HeartLantern, 1);
			recipe.AddIngredient(ItemID.HeartreachPotion, 5);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HeartLantern, 1);
			recipe.AddIngredient(ItemID.LifeFruit, 5);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
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
			item.rare = ItemRarityID.Red;
			item.accessory = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 10);
			recipe.AddIngredient(ItemID.CelestialMagnet, 1);
			recipe.AddIngredient(mod.ItemType("BoosterMagnet"), 1);
			recipe.AddIngredient(mod.ItemType("HeartreachMagnet"), 1);
			recipe.AddIngredient(ItemID.GreedyRing, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}


	public class IceFlames : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Icey Flames");
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 90;
			projectile.extraUpdates = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		public override string Texture
		{
			get { return ("SGAmod/HavocGear/Projectiles/BoulderBlast"); }
		}

		public override void AI()
		{
			int i = Main.rand.Next(0, 2);
			//for (int i = 0; i < 1; i += 1)
			//{
			int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 185, projectile.velocity.X * (i * 0.5f), projectile.velocity.Y * (i * 0.5f), 20, default(Color), 1.75f);
				Main.dust[DustID2].noGravity = true;
			//}

		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 60 * 4);
		}

	}

	public class GreenApocalypse : DarkApocalypse
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Herald of Death");
			Tooltip.SetDefault("'and now, the 4th seal is broken'\ngrants 50% increased Apocalyptical Strength\nAnd 4% throwing Apocalyptical chance while mounted\n" + Idglib.ColorText(Color.Red, "But your damage taken is slightly increased"));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}

		public override void Tooltipsfunc(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "saddletext", "'and now, the 4th seal is broken'"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "grants 50% increased Apocalyptical Strength"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "And 4% throwing Apocalyptical chance while mounted"));
			tooltips.Add(new TooltipLine(mod, "saddletext", Idglib.ColorText(Color.Red, "But your damage taken is slightly increased")));
		}

		public override void OnWear(SGAPlayer player)
		{
			player.damagetaken += 0.1f;
			player.apocalypticalChance[3] += 4.0;
		}
	}

	public class FireApocalypse : DarkApocalypse
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Herald of War");
			Tooltip.SetDefault("'then the 2nd seal was broken'\ngrants 50% increased Apocalyptical Strength\nAnd 4% melee Apocalyptical chance while mounted\n" + Idglib.ColorText(Color.Red, "But enemy spawn rates are increased"));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}

		public override void Tooltipsfunc(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "saddletext", "'then the 2nd seal was broken'"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "grants 50% increased Apocalyptical Strength"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "And 4% melee Apocalyptical chance while mounted"));
			tooltips.Add(new TooltipLine(mod, "saddletext", Idglib.ColorText(Color.Red, "But enemy spawn rates are increased")));
		}

		public override void OnWear(SGAPlayer player)
		{
			player.morespawns += 0.5f;
			player.apocalypticalChance[2] += 4.0;
		}
	}

	public class WhiteApocalypse : DarkApocalypse
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Herald of Pestilence");
			Tooltip.SetDefault("'the 1st seal is broken'\ngrants 50% increased Apocalyptical Strength\nAnd 4% magic Apocalyptical chance while mounted\n" + Idglib.ColorText(Color.Red, "But may inflict bleeding randomly"));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}
		public override void Tooltipsfunc(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "saddletext", "'the 1st seal is broken'"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "grants 50% increased Apocalyptical Strength"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "And 4% magic Apocalyptical chance while mounted"));
			tooltips.Add(new TooltipLine(mod, "saddletext", Idglib.ColorText(Color.Red, "But may inflict bleeding randomly")));

		}

		public override void OnWear(SGAPlayer player)
		{
			if (Main.rand.Next(0,300)==1)
			player.player.AddBuff(BuffID.Bleeding, 200);
			player.apocalypticalChance[2] += 4.0;
		}
	}

		public class DarkApocalypse : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Herald of Famine");
			//Tooltip.SetDefault("'thus the 3nd seal was broken'\ngrants 50% increased Apocalyptical Strength\nAnd 4% ranged Apocalyptical chance while mounted\n" + Idglib.ColorText(Color.Red,"But will nullify the effects of Well Fed"));
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}

		public virtual void Tooltipsfunc(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "saddletext", "'thus the 3nd seal was broken'"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "grants 50% increased Apocalyptical Strength"));
			tooltips.Add(new TooltipLine(mod, "saddletext", "And 4% ranged Apocalyptical chance while mounted"));
			tooltips.Add(new TooltipLine(mod, "saddletext", Idglib.ColorText(Color.Red, "But will nullify the effects of Well Fed")));

		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (GetType()==typeof(DarkApocalypse))
			Tooltipsfunc(tooltips);

			tooltips.Add(new TooltipLine(mod, "saddletext", Idglib.ColorText(Color.Red, "Limited to 1 saddle at a time")));
			tooltips.Add(new TooltipLine(mod, "saddletext", SGAGlobalItem.apocalypticaltext));
		}

		public virtual void OnWear(SGAPlayer player)
		{
			player.player.buffImmune[BuffID.WellFed] = true;
			player.apocalypticalChance[1] += 4.0;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
			if (player.mount.Active)
			{
				OnWear(sgaplayer);
				sgaplayer.apocalypticalStrength += 0.50f;
			}
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			bool canequip = true;
			for (int x = 3; x < 8 + player.extraAccessorySlots; x++)
			{
				if (player.armor[x].modItem != null)
				{
					Type myclass = player.armor[x].modItem.GetType();
					if (myclass.BaseType == typeof(DarkApocalypse) || myclass == typeof(DarkApocalypse))
					{

						//if (myType==mod.ItemType("MiningCharmlv1") || myType==mod.ItemType("MiningCharmlv2") || myType == mod.ItemType("MiningCharmlv3")){
						canequip = false;
						break;
					}
				}
			}
			return canequip;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SlimySaddle, 1);
			recipe.AddIngredient(ItemID.HardySaddle, 1);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 50);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class ApocalypticCrystalBringer : OmegaSigil
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cataclysmic Catalyst");
			Tooltip.SetDefault("'You wield power not even The Council could ever have...'\n2% increased Apocalyptical chance\nWhile you have max HP, gain an extra 3% Apocalyptical chance\n10% chance to dodge attacks that would kill you\n50% increased Apocalyptical Strength");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 20));
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Accessories/ApocalypticCrystalBringer"); }
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{

			Item itz = new Item();
			bool defined = false;
			if (Main.LocalPlayer.HeldItem != null) {
				if (Main.LocalPlayer.HeldItem.ranged)
				{
					itz.SetDefaults(ModContent.ItemType<DarkApocalypse>());
					defined = true;
				}
				if (Main.LocalPlayer.HeldItem.melee)
				{
					itz.SetDefaults(ModContent.ItemType<FireApocalypse>());
					defined = true;
				}
				if (Main.LocalPlayer.HeldItem.magic)
				{
					itz.SetDefaults(ModContent.ItemType<WhiteApocalypse>());
					defined = true;
				}
				if (Main.LocalPlayer.HeldItem.thrown)
				{
					itz.SetDefaults(ModContent.ItemType<GreenApocalypse>());
					defined = true;
				}
			}

			tooltips.Add(new TooltipLine(mod, "Saddletext", "--You copy the Saddle of your weapon's damage type--"));
			if (defined)
			{
				(itz.modItem as DarkApocalypse).Tooltipsfunc(tooltips);
			}

			tooltips.Add(new TooltipLine(mod, "accapocalypticaltext", SGAGlobalItem.apocalypticaltext));
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<SGAPlayer>().apocalypticalStrength += 0.50f;
			base.UpdateAccessory(player, hideVisual);

			Item itz = new Item();
			bool defined = false;
			if (Main.LocalPlayer.HeldItem != null)
			{
				if (Main.LocalPlayer.HeldItem.ranged)
				{
					itz.SetDefaults(ModContent.ItemType<DarkApocalypse>());
					defined = true;
				}
				if (Main.LocalPlayer.HeldItem.melee)
				{
					itz.SetDefaults(ModContent.ItemType<FireApocalypse>());
					defined = true;
				}
				if (Main.LocalPlayer.HeldItem.magic)
				{
					itz.SetDefaults(ModContent.ItemType<WhiteApocalypse>());
					defined = true;
				}
				if (Main.LocalPlayer.HeldItem.thrown)
				{
					itz.SetDefaults(ModContent.ItemType<GreenApocalypse>());
					defined = true;
				}
			}


			if (defined)
			{
				(itz.modItem as DarkApocalypse).UpdateAccessory(player, hideVisual);
			}

		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.width = 24;
			item.height = 32;
			item.value = 2000000;
			item.rare = ItemRarityID.Purple;
			item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<DarkApocalypse>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FireApocalypse>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GreenApocalypse>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteApocalypse>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MoneySign>(), 15);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 200);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class MVMUpgrade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MVM Upgrade");
			Tooltip.SetDefault("Upgrades the effects of the Gas Passer and Jarate\nGas Passer Gains Explode on Ignite\nEnemies Take 350 Classless damage when ignited\nJarate gains the ability to slow enemies when Soddened\n'100+ tour elitist be like: GET UPGRADES OR KICK! REEEEE!'");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.accessory = true;
		}

		public override string Texture
		{
			get { return ("Terraria/Gore_289"); }
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.MVMBoost = true;
		}

	}	
	public class ThrowerPouch : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thrower's Pouch");
			Tooltip.SetDefault("5% increased throwing damage, 3% increased throwing crit chance\n10% increase Throwing Velocity\n+5% increased chance to not consume throwing items");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 25, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
				player.thrownDamage += 0.05f;
				player.thrownCrit += 3;
				sgaply.Thrownsavingchance += 0.05f;
			player.thrownVelocity += 0.10f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("IronBar", 1);
			recipe.AddIngredient(ItemID.Chain, 2);
			recipe.AddIngredient(ItemID.Leather, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class TidalCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tidal Charm");
			Tooltip.SetDefault("Doubles your max breath meter\nHaving less breath boosts your defense\nThis boost increases through progression as you beat SGAmod bosses");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(0, 0, 75, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.breathMax *= 2;
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			int defensegiven = 6;
				defensegiven += SGAWorld.downedWraiths;
			if (SGAWorld.downedSpiderQueen)
				defensegiven += 2;
			if (SGAWorld.downedMurk>1)
				defensegiven += SGAWorld.GennedVirulent ? 3 :2;
			if (SGAWorld.downedSharkvern)
				defensegiven += 2;
			if (SGAWorld.downedCratrosity)
				defensegiven += 2;
			if (SGAWorld.downedTPD)
				defensegiven += 2;
			if (SGAWorld.downedHarbinger)
				defensegiven += 2;
			if (SGAWorld.downedCratrosityPML)
				defensegiven += 2;
			if (SGAWorld.downedSPinky)
				defensegiven += 2;

			player.statDefense+= (int)(1f-((float)player.breath/ (float)player.breathMax))*defensegiven;

		}

	}


	public class TwinesOfFate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twines Of Fate");
			Tooltip.SetDefault("Spin a Guide and Clothier Voodoo Doll pair around you on a string\nThey reflect projectiles and take hits that direct damage to your NPCs\nReflected projectiles do practically no damage and the dolls suffer a cooldown\nThe dolls cease to function if their respective NPCs die");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.accessory = true;
		}

		public override string Texture
		{
			get { return ("Terraria/Item_"+ItemID.GuideVoodooDoll); }
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.twinesoffate = true;
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			if (!Main.gameMenu)
			{
				Texture2D texture = ModContent.GetTexture("Terraria/Item_" + ItemID.GuideVoodooDoll);
				Vector2 textureOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
				spriteBatch.Draw(ModContent.GetTexture("Terraria/Item_" + ItemID.GuideVoodooDoll),  item.Center+ new Vector2(-6, 0)-Main.screenPosition, null, lightColor, 0f, textureOrigin, 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(ModContent.GetTexture("Terraria/Item_" + ItemID.ClothierVoodooDoll), item.Center + new Vector2(6, 0) - Main.screenPosition, null, lightColor, 0f, textureOrigin, 1f, SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (!Main.gameMenu)
			{
				Texture2D texture = ModContent.GetTexture("Terraria/Item_" + ItemID.GuideVoodooDoll);
				Vector2 slotSize = new Vector2(52f, 52f);
				position -= slotSize * Main.inventoryScale / 2f - frame.Size() * scale / 2f;
				Vector2 drawPos = position + slotSize * Main.inventoryScale / 2f;
				Vector2 textureOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
				spriteBatch.Draw(ModContent.GetTexture("Terraria/Item_"+ItemID.GuideVoodooDoll), drawPos+new Vector2(-6,0), null, drawColor, 0f, textureOrigin, Main.inventoryScale, SpriteEffects.None, 0f);
				spriteBatch.Draw(ModContent.GetTexture("Terraria/Item_" + ItemID.ClothierVoodooDoll), drawPos + new Vector2(6,0), null, drawColor, 0f, textureOrigin, Main.inventoryScale, SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HighTestFishingLine, 1);
			recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
			recipe.AddIngredient(ItemID.ClothierVoodooDoll, 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	/*


		public class PlasmaPack : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasma Pack");
			Tooltip.SetDefault("10% increased Booster and Plasma capacity\n%5 increased Technological Damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 8;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class GunBarrelParts : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gun Barrel Parts");
			Tooltip.SetDefault("Revolvers reload faster\n6% increased Bullet Damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 7;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

		public class SecondCylinder : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Second Cylinder");
			Tooltip.SetDefault("Grants 2 extra bullets per clip while using Revolvers");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 8;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

			public class GunsmithsBeltofTools : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gunsmith's Belt of Tools");
			Tooltip.SetDefault("Grants 2 extra bullets per clip while using Revolvers\nRevolvers reload faster\n8% increased Bullet Damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 8;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class PeacekeepersDuster : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peacekeeper's Duster");
			Tooltip.SetDefault("Take 20% less damage while reloading your revolver\nHostle bullets do 50% less damage to you\nGain a 25% damage buff while in your town (no bosses alive)\n'Keepin order will never be easier with this'");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 8;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.damagetaken/=1.20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
	
	public class SparingSpurs : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparing Spurs");
			Tooltip.SetDefault("Gain a movement speed buff while reloading your revolver\nGrants a Shield of Cuthulu Dash while firing your revolver\nFall Damage and fireblock immunity\n'You ready to dance pardner?'");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 8;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.damagetaken/=1.20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

		public class HighNoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("High Noon");
			Tooltip.SetDefault("Damage is increased during the day\nBase damage increase reaches its peak of 5% increased damage at noon\nAdditional 5% bullet damage at noon");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 3;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.damagetaken/=1.20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class DuelingDeity : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dueling Deity's Shades");
			Tooltip.SetDefault("Scoring an Apocalyptical summons 2 copies of your bullet to strike your opponent\nThese projectiles do 50% more damage but cannot crit\nThey do not count as ranged damage either\nEffects of High Noon");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 9;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.damagetaken/=1.20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	//Set Bonus stuff
	//Damaging an enemy marks them for a dual with you\nYour damage against your enemy is increased by 25%\nHowever your damage against other enemies is reduced by 50%\nThe duel ends when you damage a different enemy or your enemy dies\nThere is a cooldown between duels, and only ranged damage can start and stop duels"



		public class GunslingerLegend : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gunslinger of Song and Legend");
			Tooltip.SetDefault("'Riding high, in the sky...'\n'Its that time; its high Noon'\n20% increased Bullet Damage, 15% increased ranged crit chance\nDamage against your last hit enemy is increased by 15%\nCombines the Effects of:\n-Sparing Spurs and Peacekeeper's Duster\n-Gunsmith's Belt of Tools and Dueling Deity's Shades");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 24;
			item.rare = 11;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
			sgaply.damagetaken/=1.20f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class NinjaSash : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ninja's Stash");
			Tooltip.SetDefault("Scoring crits with your throwing weapons summon shurikens and throwing knives to strike hit enemies\nThese summoned attacks cannot crit or trigger Thrower damage\nFurthermore, they are consumed from the player's inventory");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 2;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

		public class ShinSash : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shin Stash");
			Tooltip.SetDefault("Scoring an Apocalyptical sends out a fan of projectiles from your sash around you\nThrowing damage and crit chance are increased by 10%\nEffects of Ninja Sash");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 5;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			base.UpdateAccessory(player,hideVisual);
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

			public class KouSash : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kou Stash");
			Tooltip.SetDefault("Scoring an Apocalyptical imbues all your currently thrown projectiles\nImbued projectiles gain 50% increased damage and inflict Moonlight Curse\nEffects of and stacks with Shin Sash\nThrowing damage and crit chance are increased by 20%");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.rare = 9;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			base.UpdateAccessory(player,hideVisual);
			SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MudAbsorber"), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	*/


}