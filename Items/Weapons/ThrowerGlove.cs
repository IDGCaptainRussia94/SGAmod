using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Idglibrary;

namespace SGAmod.Items.Weapons
{
	class ThrowerGlove : ModItem
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Grenadier's Glove");
			Tooltip.SetDefault("Throws hand grenades further, and increases their damage");
		}

		public int FindGrenadeToThrow(Player player)
		{
		List<int> grenadetypes = new List<int>();
			grenadetypes.Add(ItemID.Grenade);
			grenadetypes.Add(ItemID.BouncyGrenade);
			grenadetypes.Add(ItemID.StickyGrenade);
			grenadetypes.Add(ItemID.PartyGirlGrenade);
			grenadetypes.Add(ItemID.Beenade);
			grenadetypes.Add(mod.ItemType("AcidGrenade"));
			grenadetypes.Add(mod.ItemType("ThermalGrenade"));
			grenadetypes.Add(mod.ItemType("CelestialCocktail"));
			grenadetypes.Add(ItemID.MolotovCocktail);


			for (int i = 0; i < 58; i++)
			{
				int type = grenadetypes.Find(x => x == player.inventory[i].type);
				if (type > 0 && player.inventory[i].stack > 0)
				{
					return player.inventory[i].type;
				}
			}
			return -1;


		}

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.thrown = true;
			item.damage = 10;
			item.shootSpeed = 10f;
			item.shoot = ProjectileID.Grenade;
			item.useTurn = true;
			//ProjectileID.CultistBossLightningOrbArc
			item.width = 24;
			item.height = 24;
			item.maxStack = 1;
			item.knockBack = 9;
			item.consumable = false;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 60;
			item.useTime = 60;
			//item.noUseGraphic = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.value = Item.buyPrice(0, 0, 50, 0);
			item.rare = 3;
		}

		public override bool CanUseItem(Player player)
		{
			return (FindGrenadeToThrow(player)>0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int basetype = FindGrenadeToThrow(player);
			//if (player.CountItem(mod.ItemType("AcidGrenade"))>0)
			//basetype = mod.ItemType("AcidGrenade");

			Vector2 basespeed = new Vector2(speedX, speedY);
			float speedbase = basespeed.Length();
			basespeed.Normalize();

			Item basetype2 = new Item();
			basetype2.SetDefaults(basetype);
			float baseumtli = item.useTime/60f;
			player.itemAnimation = (int)(basetype2.useAnimation* baseumtli);
			player.itemAnimationMax = (int)(basetype2.useAnimation* baseumtli);
			player.itemTime = basetype2.useTime;
			type = basetype2.shoot;
			basespeed *= (basetype2.shootSpeed + speedbase);
			speedX = basespeed.X;
			speedY = basespeed.Y;
			damage += (int)(basetype2.damage * player.thrownDamage);

			player.ConsumeItem(basetype);

			return true;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/GLOOOVE_FINAL123"); }
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 15);
			recipe.AddIngredient(ItemID.BeeWax, 10);
			recipe.AddIngredient(ItemID.TatteredCloth, 5);
			recipe.AddRecipeGroup("SGAmod:Tier2Bars", 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	class AcidGrenade : ModItem
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Acid Grenade");
			Tooltip.SetDefault("Deals Acid Burn to all affected, but does not last long before exploding");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Grenade);
			item.useStyle = 1;
			item.thrown = true;
			item.damage = 70;
			item.shoot = mod.ProjectileType("AcidGrenadeProj");
			item.value = Item.buyPrice(0, 0, 2, 0);
			item.rare = 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Grenade, 15);
			recipe.AddIngredient(mod.ItemType("VialofAcid"), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this,15);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			speedX *= player.thrownVelocity;
			speedY *= player.thrownVelocity;

			return true;
		}

	}

	class ThermalGrenade : AcidGrenade
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Thermal Grenade");
			Tooltip.SetDefault("Deals Thermal Blaze to all affected");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Grenade);
			item.useStyle = 1;
			item.thrown = true;
			item.damage = 80;
			item.shoot = mod.ProjectileType("ThermalGrenadeProj");
			item.value = Item.buyPrice(0, 0, 2, 0);
			item.rare = 5;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Grenade, 15);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 15);
			recipe.AddRecipe();
		}

	}

	class AcidGrenadeProj : ModProjectile
	{

		bool hitonce = false;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Acid Grenade");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Grenade);
			projectile.thrown = true;
			projectile.timeLeft = 180;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/AcidGrenade"); }
		}

		public override bool PreKill(int timeLeft)
		{
			if (!hitonce)
			{
				projectile.width = 128;
				projectile.height = 128;
				projectile.position -= new Vector2(64, 64);
			}

			for (int i = 0; i < 100; i += 1)
			{
				float randomfloat = Main.rand.NextFloat(1f, 6f);
				Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();

				int dust = Dust.NewDust(new Vector2(projectile.Center.X - 32, projectile.Center.Y - 32), 64, 64, mod.DustType("AcidDust"));
				Main.dust[dust].scale = 2.5f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = (projectile.velocity * (float)(Main.rand.Next(10, 20) * 0.01f)) + (randomcircle * randomfloat);
			}

			int theproj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Explosion"), (int)((double)projectile.damage * 1f), projectile.knockBack, projectile.owner, 0f, 0f);
			Main.projectile[theproj].thrown = projectile.magic;
			IdgProjectile.AddOnHitBuff(theproj, mod.BuffType("AcidBurn"), 120);

			projectile.velocity = default(Vector2);
			projectile.type = ProjectileID.Grenade;
			return true;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AcidDust"));
			Main.dust[dust].scale = 0.75f;
			Main.dust[dust].noGravity = false;
			Main.dust[dust].velocity = projectile.velocity * (float)(Main.rand.Next(60, 100) * 0.01f);
			projectile.timeLeft -= 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!hitonce)
			{
				hitonce = true;
				projectile.position -= new Vector2(64, 64);
				projectile.width = 128;
				projectile.height = 128;
				projectile.timeLeft = 1;
			}
			//projectile.Center -= new Vector2(48,48);

			target.AddBuff(mod.BuffType("AcidBurn"), 120);
		}

	}

	class ThermalGrenadeProj : AcidGrenadeProj
	{

		bool hitonce = false;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Thermal Grenade");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Grenade);
			projectile.thrown = true;
			projectile.timeLeft = 240;
		}

	public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/ThermalGrenade"); }
		}

		public override bool PreKill(int timeLeft)
		{
			if (!hitonce)
			{
				projectile.width = 128;
				projectile.height = 128;
				projectile.position -= new Vector2(64, 64);
			}

			for (int i = 0; i < 100; i += 1)
			{
				float randomfloat = Main.rand.NextFloat(1f, 6f);
				Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();

				int dust = Dust.NewDust(new Vector2(projectile.Center.X - 32, projectile.Center.Y - 32), 64, 64, mod.DustType("HotDust"));
				Main.dust[dust].scale = 2.5f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = (projectile.velocity * (float)(Main.rand.Next(10, 20) * 0.01f)) + (randomcircle * randomfloat);
			}

			int theproj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Explosion"), (int)((double)projectile.damage * 1f), projectile.knockBack, projectile.owner, 0f, 0f);
			Main.projectile[theproj].thrown = projectile.magic;
			IdgProjectile.AddOnHitBuff(theproj, mod.BuffType("ThermalBlaze"), 60 * 3);

			projectile.velocity = default(Vector2);
			projectile.type = ProjectileID.Grenade;
			return true;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("HotDust"));
			Main.dust[dust].scale = 0.75f;
			Main.dust[dust].noGravity = false;
			Main.dust[dust].velocity = projectile.velocity * (float)(Main.rand.Next(60, 100) * 0.01f);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!hitonce)
			{
				hitonce = true;
				projectile.position -= new Vector2(64, 64);
				projectile.width = 128;
				projectile.height = 128;
				projectile.timeLeft = 1;
			}
			//projectile.Center -= new Vector2(48,48);

			target.AddBuff(mod.BuffType("ThermalBlaze"), 60*4);
		}



	}

	class CelestialCocktail : ModItem
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Cosmic Cocktail");
			Tooltip.SetDefault("Made from the sweet celestial essence of the lunar slime princess\nexplodes violently, sending celestial projectiles in the general direction the projectile was traveling");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.MolotovCocktail);
			item.useStyle = 1;
			item.thrown = true;
			item.damage = 60;
			item.useTime = 20;
			item.useAnimation = 20;
			item.maxStack = 999;
			item.shootSpeed = 16f;
			item.shoot = mod.ProjectileType("CelestialCocktailProj");
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = 10;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MolotovCocktail, 75);
			recipe.AddRecipeGroup("SGAmod:CelestialFragments", 3);
			recipe.AddIngredient(mod.ItemType("LunarRoyalGel"), 2);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 75);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddIngredient(ItemID.Torch, 10);
			recipe.AddIngredient(ItemID.Ale, 50);
			recipe.AddRecipeGroup("SGAmod:CelestialFragments", 1);
			recipe.AddIngredient(mod.ItemType("LunarRoyalGel"), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 150);
			recipe.AddRecipe();

		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			speedX *= player.thrownVelocity;
			speedY *= player.thrownVelocity;

			return true;
		}

	}

	class CelestialCocktailProj : ModProjectile
	{

		bool hitonce = false;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Acid Grenade");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Grenade);
			projectile.thrown = true;
			projectile.timeLeft = 180;
		}

		public override string Texture
		{
			get { return ("SGAmod/Projectiles/CelestialCocktail"); }
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return false;
		}

		public override bool PreKill(int timeLeft)
		{
			if (!hitonce)
			{
				projectile.width = 128;
				projectile.height = 128;
				projectile.position -= new Vector2(64, 64);
			}

			Vector2 gotohere = projectile.velocity;
			gotohere.Normalize();

			int[] projectiletype = { ProjectileID.NebulaBlaze1, ProjectileID.VortexBeaterRocket, ProjectileID.HeatRay, ProjectileID.DD2LightningBugZap };
			for (int zz = 0; zz < 4; zz += 1)
			{
				for (int i = 0; i < 10; i += 1)
				{
					Vector2 perturbedSpeed = new Vector2(gotohere.X, gotohere.Y).RotatedByRandom(MathHelper.ToRadians(120)) * Main.rand.NextFloat(6, 12);
					int proj = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), projectiletype[zz], (int)(projectile.damage * 0.75f), projectile.knockBack / 10f, projectile.owner);
					Main.projectile[proj].thrown = true;
					Main.projectile[proj].magic = false;
					Main.projectile[proj].friendly = true;
					Main.projectile[proj].velocity = new Vector2(perturbedSpeed.X, perturbedSpeed.Y);
					Main.projectile[proj].hostile = false;
					Main.projectile[proj].timeLeft = 300;
					Main.projectile[proj].penetrate = 4;
					IdgProjectile.Sync(proj);
				}

			}

			int theproj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("SlimeBlast"), (int)((double)projectile.damage * 1f), projectile.knockBack, projectile.owner, 0f, 0f);
			Main.projectile[theproj].thrown = true;

			projectile.velocity = default(Vector2);
			projectile.type = ProjectileID.Grenade;
			return true;
		}

		public override void AI()
		{
			int[] dustype = { mod.DustType("AcidDust"), mod.DustType("HotDust"), mod.DustType("MangroveDust"), mod.DustType("TornadoDust") };
			int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustype[Main.rand.Next(0,4)]);
			Main.dust[dust].scale = 0.75f;
			Main.dust[dust].noGravity = false;
			Main.dust[dust].velocity = projectile.velocity * (float)(Main.rand.Next(60, 100) * 0.01f);
			projectile.timeLeft -= 1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!hitonce)
			{
				hitonce = true;
				projectile.position -= new Vector2(64, 64);
				projectile.width = 128;
				projectile.height = 128;
				projectile.timeLeft = 1;
			}
			//projectile.Center -= new Vector2(48,48);
		}

	}




}
