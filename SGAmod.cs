using System.IO;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Idglibrary;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.World;


namespace SGAmod
{

	class SGAmod : Mod
	{
		public static int ScrapCustomCurrencyID;
		public static CustomCurrencySystem ScrapCustomCurrencySystem;
		public static SGAmod Instance;
		public static float ProgramSkyAlpha = 0f;
		public static int[,] WorldOres = {{ItemID.CopperOre,ItemID.TinOre},{ItemID.IronOre,ItemID.LeadOre},{ItemID.SilverOre,ItemID.TungstenOre},{ItemID.GoldOre,ItemID.PlatinumOre}
		,{ItemID.PalladiumOre,ItemID.CobaltOre},{ItemID.OrichalcumOre,ItemID.MythrilOre},{ItemID.TitaniumOre,ItemID.AdamantiteOre}};
		public static Dictionary<int, int> UsesClips;
		public static Dictionary<int, int> UsesPlasma;
		public static int[] otherimmunes = new int[3];
		public static bool Calamity = false;

		public SGAmod()
		{

			//SGAmod.AbsentItemDisc.Add(SGAmod.Instance.ItemType("Tornado"), "This is test");

			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

		public override void PreSaveAndQuit()
		{
			Overlays.Scene.Deactivate("SGAmod:SGAHUD");
		}

		public override void Load()
		{
			Instance = this;
			SGAmod.UsesClips = new Dictionary<int, int>();
			SGAmod.UsesPlasma = new Dictionary<int, int>();
			SGAmod.otherimmunes = new int[3];
			SGAmod.otherimmunes[0] = BuffID.Daybreak;
			SGAmod.otherimmunes[1] = this.BuffType("ThermalBlaze");
			SGAmod.otherimmunes[2] = this.BuffType("NapalmBurn");
			SGAmod.ScrapCustomCurrencySystem = new ScrapMetalCurrency(ModContent.ItemType<Items.Scrapmetal>(), 999L);
			SGAmod.ScrapCustomCurrencyID = CustomCurrencyManager.RegisterCurrency(SGAmod.ScrapCustomCurrencySystem);

			Filters.Scene["SGAmod:ProgramSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.5f, 0.5f, 0.5f).UseOpacity(0.4f), EffectPriority.High);
			SkyManager.Instance["SGAmod:ProgramSky"] = new ProgramSky();
			Overlays.Scene["SGAmod:SGAHUD"] = new SGAHUD();
		}

		public override uint ExtraPlayerBuffSlots => 14;

		public override void Unload()
		{
			SGAmod.UsesClips = null;
			SGAmod.UsesPlasma = null;
			SGAmod.ScrapCustomCurrencySystem = null;
			Instance = null;
			Calamity = false;
			otherimmunes = null;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(this);
			recipe.AddIngredient(this.ItemType("IceFairyDust"), 5);
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddTile(this.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(ItemID.IceMachine);
			recipe.AddRecipe();

			/*recipe = new ModRecipe(this);
			recipe.AddIngredient(this.ItemType("IceFairyDust"), 5);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 15);
			recipe.AddIngredient(ItemID.UnicornHorn, 1);
			recipe.AddTile(this.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(ItemID.RodofDiscord);
			recipe.AddRecipe();*/

			recipe = new ModRecipe(this);
			recipe.AddIngredient(this.ItemType("AdvancedPlating"), 5);
			recipe.AddRecipeGroup("SGAmod:IchorOrCursed", 5);
			recipe.AddRecipeGroup("SGAmod:Tier5Bars", 5);
			recipe.AddTile(this.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(ItemID.Uzi);
			recipe.AddRecipe();

			int[] moonlorditems = {ItemID.Terrarian,ItemID.LunarFlareBook,ItemID.RainbowCrystalStaff,ItemID.SDMG,ItemID.StarWrath,ItemID.Meowmere,ItemID.LastPrism,ItemID.MoonlordTurretStaff,ItemID.FireworksLauncher};

			foreach (int idofitem in moonlorditems)
			{
				recipe = new ModRecipe(this);
				if (idofitem != ItemID.LastPrism)
				recipe.AddIngredient(this.ItemType("EldritchTentacle"), 25);
				else
				recipe.AddIngredient(this.ItemType("EldritchTentacle"), 35);
				recipe.AddRecipeGroup("SGAmod:CelestialFragments", 5);
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.SetResult(idofitem);
				recipe.AddRecipe();
			}


		}

		public static void CalamityNoRevengenceNoDeathNoU()
		{
			if (SGAmod.Calamity)
			{
				bool nothing = CalamityFlipoffRevengence;
			}
		}

		public static bool CalamityFlipoffRevengence
		{
			get { Player player = Main.player[Main.myPlayer];

				if (!SGAmod.Calamity)
					return false;
				
				CalamityPlayer calply = player.GetModPlayer<CalamityPlayer>();
				if (CalamityMod.World.CalamityWorld.revenge)
				ModContent.GetInstance<CalamityMod.Items.DifficultyItems.Revenge>().UseItem(player);
				if (CalamityMod.World.CalamityWorld.death)
					ModContent.GetInstance<CalamityMod.Items.DifficultyItems.Death>().UseItem(player);

				return true;
			}
		}

		public override void PostSetupContent()
			{

			Calamity=ModLoader.GetMod("CalamityMod")!=null;

			Mod bossList = ModLoader.GetMod("BossChecklist");
			if (bossList != null)
			{
				//bossList.Call("AddBoss", "TPD", 5.5f, (Func<bool>)(() => ExampleWorld.SGAWorld.downedTPD));
				bossList.Call("AddBossWithInfo", "Copper Wraith", 0.05f, (Func<bool>)(() => (SGAWorld.downedWraiths > 0)), string.Format("Use a [i:{0}] at anytime, defeat this boss to unlock crafting the furnace", ItemType("WraithCoreFragment")));
				bossList.Call("AddMiniBossWithInfo", "The Caliburn Guardians", 1.4f, (Func<bool>)(() => SGAWorld.downedCaliburnGuardians > 2), "Find Caliburn Alters in Dank Shrines Underground and right click them to fight a Caliburn Spirit, after beating a sprite you can retrive your reward by breaking the Alter; each guardian is stronger than the previous");
				bossList.Call("AddBossWithInfo", "Spider Queen", 3.5f, (Func<bool>)(() => SGAWorld.downedSpiderQueen), string.Format("Use a [i:{0}] underground, anytime", ItemType("AcidicEgg")));
				bossList.Call("AddMiniBossWithInfo", "Killer Fly Swarm", 5.4f, (Func<bool>)(() => SGAWorld.downedMurk>0), string.Format("Use a [i:{0}] in the jungle", ItemType("RoilingSludge")));
				bossList.Call("AddBossWithInfo", "Murk", 5.5f, (Func<bool>)(() => SGAWorld.downedMurk>1), string.Format("Use a [i:{0}] in the jungle after killing the fly swarm", ItemType("RoilingSludge")));
				bossList.Call("AddBossWithInfo", "Murk (Hardmode)", 6.5f, (Func<bool>)(() => SGAWorld.downedMurk > 1 && SGAWorld.GennedVirulent), string.Format("Use a [i:{0}] in the jungle in Hardmode (fly swarm must be defeated), defeating this buffed version causes a new ore to generate", ItemType("RoilingSludge")));
				bossList.Call("AddBossWithInfo", "Cirno", 6.5f, (Func<bool>)(() => SGAWorld.downedCirno), string.Format("Use a [i:{0}] in the snow biome during the day", ItemType("Nineball")));
				bossList.Call("AddBossWithInfo", "Cobalt Wraith", 6.6f, (Func<bool>)(() => (SGAWorld.downedWraiths>1)), string.Format("Use a [i:{0}] at anytime, defeat this boss to unlock crafting a hardmode anvil", ItemType("WraithCoreFragment2")));
				bossList.Call("AddBossWithInfo", "Sharkvern", 9.5f, (Func<bool>)(() => SGAWorld.downedSharkvern), string.Format("Use a [i:{0}] at the ocean", ItemType("ConchHorn")));
				bossList.Call("AddBossWithInfo", "Cratrosity", 10.5f, (Func<bool>)(() => SGAWorld.downedCratrosity), string.Format("Use any key that is not a [i:{1}] with a [i:{0}] at night, get a contracker from the merchant to allow enemies to drop [i:{0}]", ItemType("TerrariacoCrateBase"), ItemType("TerrariacoCrateKey")));
				bossList.Call("AddBossWithInfo", "Twin Prime Destroyers", 11.25f, (Func<bool>)(() => SGAWorld.downedTPD), string.Format("Use a [i:{0}] anywhere at night", ItemType("Mechacluskerf")));
				bossList.Call("AddBossWithInfo", "Doom Harbinger", 11.33, (Func<bool>)(() => SGAWorld.downedHarbinger), string.Format("Can spawn randomly at the start of night after golem is beaten, the DD2 event is finished on tier 3, and the Martians are beaten, defeating him will allow the cultists to spawn (Single Player Only)", ItemType("Prettygel")));
				bossList.Call("AddBossWithInfo", "Luminite Wraith", 12.5f, (Func<bool>)(() => (SGAWorld.downedWraiths>2)), string.Format("Use a [i:{0}], defeat this boss to get the Ancient Manipulator", ItemType("WraithCoreFragment3")));
				bossList.Call("AddBossWithInfo", "Luminite Wraith (Rematch)", 14.8f, (Func<bool>)(() => (SGAWorld.downedWraiths>3)), string.Format("Use a [i:{0}] after the first fight when Moonlord is defeated to issue a rematch; the true battle begins...", ItemType("WraithCoreFragment3")));
				bossList.Call("AddBossWithInfo", "Supreme Pinky", 15f, (Func<bool>)(() => SGAWorld.downedSPinky), string.Format("Use a [i:{0}] anywhere at night", ItemType("Prettygel")));
			}

			Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
			if (yabhb != null)
			{
				yabhb.Call("hbStart");
				yabhb.Call("hbFinishMultiple", NPCType("BossCopperWraith"), NPCType("BossCopperWraith"));
				yabhb.Call("hbStart");
				yabhb.Call("hbFinishMultiple", NPCType("SPinkyClone"), NPCType("SPinkyClone"));
				yabhb.Call("hbStart");
				yabhb.Call("hbFinishMultiple", NPCType("Harbinger"), NPCType("Harbinger"));
			}


			//SGAWorld.downedCopperWraith==0 ? true : false)
			Idglib.AbsentItemDisc.Add(this.ItemType("Tornado"), "5% to drop from Wyverns after Golem");
			Idglib.AbsentItemDisc.Add(this.ItemType("Upheaval"), "20% to drop from Golem");
			Idglib.AbsentItemDisc.Add(this.ItemType("Powerjack"), "10% to drop from Wall of Flesh");
			Idglib.AbsentItemDisc.Add(this.ItemType("FieryShard"), "Drops from Hell bats and Red Devils");
			Idglib.AbsentItemDisc.Add(this.ItemType("StarMetalMold"), "Drops from Twin Prime Destroyers");
			Idglib.AbsentItemDisc.Add(this.ItemType("SwordofTheBlueMoon"), "10% (20% in expert mod) drop from Moonlord");
			Idglib.AbsentItemDisc.Add(this.ItemType("Fieryheart"), "This item is not obtainable yet");
			Idglib.AbsentItemDisc.Add(this.ItemType("Sunbringer"), "This item is not obtainable yet");
			Idglib.AbsentItemDisc.Add(this.ItemType("PrimordialSkull"), "Sold by the Dergon");
			Idglib.AbsentItemDisc.Add(this.ItemType("OmegaSigil"), "Drops from Betsy");
		}

	public override void AddRecipeGroups()
    {
    	RecipeGroup group = new RecipeGroup(() => "any" + " Copper or Tin ore", new int[]
    	{
    		ItemID.CopperOre,
    		ItemID.TinOre
    	});
    	RecipeGroup group2 = new RecipeGroup(() => "any" + " Tier 1 Hardmode ore", new int[]
    	{
    		ItemID.CobaltOre,
    		ItemID.PalladiumOre
    	});
    	RecipeGroup group3 = new RecipeGroup(() => "any" + " Celestial Fragments", new int[]
    	{
    		ItemID.FragmentVortex,
    		ItemID.FragmentNebula,
    		ItemID.FragmentSolar,
    		ItemID.FragmentStardust
    	});
    	RecipeGroup group4 = new RecipeGroup(() => "any" + " Basic Wraith Shards", new int[]
    	{
    		this.ItemType("WraithFragment"),
    		this.ItemType("WraithFragment2")
    	});
    	RecipeGroup group5 = new RecipeGroup(() => "any" + " Gold or Platinum bars", new int[]
    	{
    		ItemID.GoldBar,
    		ItemID.PlatinumBar
    	});
		RecipeGroup group6 = new RecipeGroup(() => "any" + " Evil hardmode drop", new int[]
		{
		ItemID.Ichor,
		ItemID.CursedFlame
		});

			RecipeGroup.RegisterGroup("SGAmod:Tier1Ore", group);
    	RecipeGroup.RegisterGroup("SGAmod:Tier1HardmodeOre", group2);
    	RecipeGroup.RegisterGroup("SGAmod:CelestialFragments", group3);
    	RecipeGroup.RegisterGroup("SGAmod:BasicWraithShards", group4);
    	RecipeGroup.RegisterGroup("SGAmod:Tier4Bars", group5);
		RecipeGroup.RegisterGroup("SGAmod:IchorOrCursed", group6);

			group6 = new RecipeGroup(() => "any" + " Copper or Tin bars", new int[]
			{
			ItemID.CopperBar,
			ItemID.TinBar
			});
			RecipeGroup.RegisterGroup("SGAmod:Tier1Bars", group6);
			group6 = new RecipeGroup(() => "any" + " Iron or Lead bars", new int[]
			{
			ItemID.IronBar,
			ItemID.LeadBar
			});
			RecipeGroup.RegisterGroup("SGAmod:Tier2Bars", group6);
			group6 = new RecipeGroup(() => "any" + " Silver or Tungsten Bars", new int[]
			{
			ItemID.TungstenBar,
			ItemID.SilverBar
			});
			RecipeGroup.RegisterGroup("SGAmod:Tier3Bars", group6);
			group6 = new RecipeGroup(() => "any" + " Crimtane or Demonite Bars", new int[]
			{
			ItemID.DemoniteBar,
			ItemID.CrimtaneBar
			});
			RecipeGroup.RegisterGroup("SGAmod:Tier5Bars", group6);
			group6 = new RecipeGroup(() => "any" + " Evil Javelins", new int[]
			{
			this.ItemType("CorruptionJavelin"),
			this.ItemType("CrimsonJavelin")
			});
			RecipeGroup.RegisterGroup("SGAmod:EvilJavelins", group6);
			group6 = new RecipeGroup(() => "any" + " Evil Boss Materials", new int[]
			{
			ItemID.ShadowScale,
			ItemID.TissueSample
			});
			RecipeGroup.RegisterGroup("SGAmod:EvilBossMaterials", group6);
			group6 = new RecipeGroup(() => "any" + " Pressure Plates found underground", new int[]
			{
			ItemID.BrownPressurePlate,
			ItemID.GrayPressurePlate,
			ItemID.BluePressurePlate,
			ItemID.YellowPressurePlate
			});
			RecipeGroup.RegisterGroup("SGAmod:PressurePlates", group6);
			group6 = new RecipeGroup(() => "a" + " Presserator, Wrench, MetalDetector, or Detonator", new int[]
			{
			ItemID.ActuationAccessory,
			ItemID.Wrench,
			ItemID.Detonator,
			ItemID.MetalDetector
			});
			RecipeGroup.RegisterGroup("SGAmod:TechStuff", group6);



			if (RecipeGroup.recipeGroupIDs.ContainsKey("IronBar"))
    	{
    		int index = RecipeGroup.recipeGroupIDs["IronBar"];
    		group = RecipeGroup.recipeGroups[index];
			group.ValidItems.Add(ItemType("UnmanedBar"));
    	}

	}

		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
			{
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<SGAPlayer>().DankShrineZone)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/Swamp");
				priority = MusicPriority.BiomeMedium;
			}
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			int atype = reader.ReadByte();
			MessageType type = (MessageType)atype;

			if (type==MessageType.CratrosityNetSpawn && Main.netMode>0){

			int crate=reader.ReadInt32();
			int vec1=reader.ReadInt32();
			int vec2=reader.ReadInt32();
			Player ply=Main.player[reader.ReadInt32()];
				NPC.SpawnOnPlayer(ply.whoAmI, crate);
				if (crate==NPCType("CratrosityPML")){
					//SgaLib.Chat("Test1",255,255,255);

					ModPacket packet = GetPacket();
					packet.Write((byte)MessageType.LockedinSetter);
					packet.Write(vec1);
					packet.Write(vec2);
					packet.Write(ply.whoAmI);
					packet.Send();

				}
				else
				{
					//hhh

				}


			}

			if (atype == (byte)1 && Main.netMode > 0)
			{

				int wherex = reader.ReadInt32();
				int wherey = reader.ReadInt32();
				int npc = reader.ReadInt32();
				int ai1 = reader.ReadInt32();
				int ai2 = reader.ReadInt32();
				int ai3 = reader.ReadInt32();
				int ai4 = reader.ReadInt32();

				Player ply = Main.player[reader.ReadInt32()];
				NPC.NewNPC(wherex, wherey, npc,0, ai1, ai2, ai3, ai4);


			}


			if (type==MessageType.ClientSendInfo){
			int player=reader.ReadInt32();
			int ammoLeftInClip=reader.ReadInt32();
			int sufficate=reader.ReadInt32();
			int PrismalShots = reader.ReadInt32();
			int devpower = reader.ReadInt32();
			int plasmaLeftInClip = reader.ReadInt32();
			int Redmanastar = reader.ReadInt32();
			int ExpertiseCollected = reader.ReadInt32();
			int ExpertiseCollectedTotal = reader.ReadInt32();
				SGAPlayer sgaplayer = Main.player[player].GetModPlayer(this,typeof(SGAPlayer).Name) as SGAPlayer;
    		sgaplayer.ammoLeftInClip = ammoLeftInClip;
    		sgaplayer.sufficate = sufficate;
				sgaplayer.PrismalShots = PrismalShots;
				sgaplayer.devpower = devpower;
				sgaplayer.plasmaLeftInClip = plasmaLeftInClip;
				sgaplayer.Redmanastar = Redmanastar;
				sgaplayer.ExpertiseCollected = ExpertiseCollected;
				sgaplayer.ExpertiseCollectedTotal = ExpertiseCollectedTotal;
				for (int i = 54; i < 58; i++)
				{
					sgaplayer.ammoinboxes[i - 54] = reader.ReadInt32();
				}
			}

			if (type==MessageType.LockedinSetter){
				//Main.NewText("Test2",255,255,255);
			Vector2 Vect=new Vector2(reader.ReadInt32(),reader.ReadInt32());
			Player sender=Main.player[reader.ReadInt32()];
			//Main.NewText("Testloc: "+Vect.X+" "+Vect.Y,255,255,255);
				SGAPlayer modplayer = sender.GetModPlayer<SGAPlayer>();
				modplayer.Locked=Vect;
				//Main.NewText("Testloc: "+modplayer.Locked.X+" "+modplayer.Locked.Y,255,255,255);

			for (int num172 = 0; num172 < 100; num172 = num172 + 1){
				Player ply=Main.player[num172];
				modplayer = ply.GetModPlayer<SGAPlayer>();
				modplayer.Locked=Vect;
			}
			}


		}


	enum MessageType : byte
	{
	CratrosityNetSpawn,
	ClientNPC,
	LockedinSetter,
	Filler,
	ClientSendInfo
	}


	}
}
