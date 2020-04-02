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
		public static string disc = "\nMay also be worn in place of a Grapple Hook to throw grenades with the grapple key\nHowever, the grenades are slower and has a cooldown";
		public virtual int level => 0;
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Grenadier's Glove");
			Tooltip.SetDefault("Throws hand grenades further, and increases their damage"+ disc);
		}

		public static int FindGrenadeToThrow(Mod mod,Player player, int level)
		{
		List<int> grenadetypes = new List<int>();
			grenadetypes.Add(ItemID.Grenade);
			grenadetypes.Add(ItemID.BouncyGrenade);
			grenadetypes.Add(ItemID.StickyGrenade);
			grenadetypes.Add(ItemID.PartyGirlGrenade);
			grenadetypes.Add(ItemID.Beenade);
			grenadetypes.Add(mod.ItemType("AcidGrenade"));
			grenadetypes.Add(mod.ItemType("ThermalGrenade"));
			if (level != 2 && level>0)
			{
				grenadetypes.Add(mod.ItemType("CelestialCocktail"));
				grenadetypes.Add(ItemID.MolotovCocktail);
				grenadetypes.Add(ItemID.Bone);
				grenadetypes.Add(ItemID.Ale);
			}

			if (level == 2)
			{
				grenadetypes.Add(ItemID.Dynamite);
				grenadetypes.Add(ItemID.BombFish);
				grenadetypes.Add(ItemID.BouncyDynamite);
				grenadetypes.Add(ItemID.StickyDynamite);
				grenadetypes.Add(ItemID.Bomb);
				grenadetypes.Add(ItemID.StickyBomb);
				grenadetypes.Add(ItemID.BouncyBomb);
			}

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
		public static int FindProjectile(int proj,int weapon)
		{
			if (proj == ProjectileID.Bone)
				proj = ProjectileID.BoneGloveProj;
			if (weapon == ItemID.AleThrowingGlove || proj == ItemID.AleThrowingGlove)
				proj = ProjectileID.Ale;
			return proj;

		}

		public static int FindItem(int weapon)
		{
			if (weapon == ItemID.Ale)
				weapon = ItemID.AleThrowingGlove;
			return weapon;

		}

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.thrown = true;
			item.damage = 4;
			item.shootSpeed = 5f;
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
			item.shoot = ModContent.ProjectileType<GrenadeNotAHook>();
			item.value = Item.buyPrice(0, 1, 50, 0);
			item.rare = 3;
		}

		public override bool CanUseItem(Player player)
		{
			return (ThrowerGlove.FindGrenadeToThrow(mod,player, level) >0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int basetype = ThrowerGlove.FindGrenadeToThrow(mod, player, level);
			//if (player.CountItem(mod.ItemType("AcidGrenade"))>0)
			//basetype = mod.ItemType("AcidGrenade");

			Vector2 basespeed = new Vector2(speedX, speedY);
			float speedbase = basespeed.Length()*player.thrownVelocity;
			basespeed.Normalize();

			Item basetype2 = new Item();
			basetype2.SetDefaults(FindItem(basetype));
			float baseumtli = (item.useTime/player.GetModPlayer<SGAPlayer>().ThrowingSpeed) /60f;
			player.itemAnimation = (int)(basetype2.useAnimation* baseumtli);
			player.itemAnimationMax = (int)(basetype2.useAnimation* baseumtli);
			player.itemTime = (int)(basetype2.useTime* baseumtli);
			type = FindProjectile(basetype2.shoot, basetype);
			basespeed *= (basetype2.shootSpeed + speedbase);
			speedX = basespeed.X;
			speedY = basespeed.Y;
			//if (type!=mod.ProjectileType("CelestialCocktailProj"))
			damage += (int)(basetype2.damage * player.thrownDamage);
			//else
			//damage = (int)(basetype2.damage * player.thrownDamage);

			player.ConsumeItem(basetype);

			Projectile proj = Main.projectile[Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI)];
			proj.thrown = true;
			proj.ranged = false;
			proj.netUpdate = true;

			return false;
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

	class ThrowerGloveMK2 : ThrowerGlove
	{
		public override int level => 1;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Rioter's Glove");
			Tooltip.SetDefault("Throws hand grenades further, and increases their damage\nUpgraded to now throw Ale, Molotovs, and Bones!\n" + disc);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.useStyle = 1;
			item.damage = 5;
			item.shoot = ModContent.ProjectileType<GrenadeNotAHook2>();
			item.shootSpeed = 5.5f;
			item.value = Item.buyPrice(0, 2, 50, 0);
			item.rare = 4;
			item.expert = true;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/MolotovGlove"); }
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("ThrowerGlove"), 1);
			recipe.AddIngredient(ItemID.AleThrowingGlove, 1);
			recipe.AddIngredient(ItemID.BoneGlove, 1);
			recipe.AddRecipeGroup("SGAmod:Tier5Bars", 15);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	class ThrowerGloveDynamite : ThrowerGlove
	{
		public override int level => 2;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Demolitionist's Glove");
			Tooltip.SetDefault("Throws hand grenades further, and increases their damage\nUpgraded to now throw Bombs and Dynamite!\n" + disc);
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			item.useStyle = 1;
			item.damage = 4;
			item.shoot = ModContent.ProjectileType<GrenadeNotAHook3>();
			item.shootSpeed = 5.5f;
			item.value = Item.buyPrice(0, 2, 50, 0);
			item.rare = 4;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/DynamiteGlove"); }
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("ThrowerGlove"), 1);
			recipe.AddIngredient(ItemID.Dynamite, 10);
			recipe.AddIngredient(ItemID.StickyBomb, 20);
			recipe.AddRecipeGroup("SGAmod:Tier3Bars", 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	internal class GrenadeNotAHook : ModProjectile
	{
		public virtual int level => 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("${ProjectileName.GemHookAmethyst}");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/AcidGrenade"); }
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			player.GetModPlayer<SGAPlayer>().greandethrowcooldown = 120;
			ThrowerGlove thrown;
			int basetype = ThrowerGlove.FindGrenadeToThrow(mod, player, level);
			//if (player.CountItem(mod.ItemType("AcidGrenade"))>0)
			//basetype = mod.ItemType("AcidGrenade");

			Vector2 basespeed = (projectile.velocity/2f);
			float speedbase = basespeed.Length() * player.thrownVelocity;
			basespeed.Normalize();

			Item basetype2 = new Item();
			basetype2.SetDefaults(ThrowerGlove.FindItem(basetype));
			player.itemTime = basetype2.useTime;
			int type = ThrowerGlove.FindProjectile(basetype2.shoot, basetype);
			basespeed *= (basetype2.shootSpeed + speedbase);
			int damage = (int)(basetype2.damage * player.thrownDamage);

			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, basespeed.X, basespeed.Y, type, damage, basetype2.knockBack, player.whoAmI);

			player.ConsumeItem(basetype);

			projectile.Kill();
		}

		// Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook
		public override bool? CanUseGrapple(Player player)
		{
			return (player.GetModPlayer<SGAPlayer>().greandethrowcooldown<1 && ThrowerGlove.FindGrenadeToThrow(mod, player, level) >-1);
		}

	}

	internal class GrenadeNotAHook2 : GrenadeNotAHook
	{
		public override int level => 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("${ProjectileName.GemHookAmethyst}");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/AcidGrenade"); }
		}

	}

	internal class GrenadeNotAHook3 : GrenadeNotAHook
	{
		public override int level => 2;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("${ProjectileName.GemHookAmethyst}");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/AcidGrenade"); }
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
			item.damage = 65;
			item.shootSpeed = 3f;
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
			item.damage = 72;
			item.shootSpeed = 3f;
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
			item.damage = 50;
			item.useTime = 40;
			item.useAnimation = 40;
			item.maxStack = 999;
			item.shootSpeed = 8f;
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
			DisplayName.SetDefault("Celestial Cocktail");
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

		public override bool? CanHitNPC(NPC target)
		{
			int player = projectile.owner;
			return base.CanHitNPC(target);
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
			float[] projectiledamage = { 1f, 1f, 2.5f, 0.25f };
			int[] projectilecount = { 9, 9,12,7 };
			for (int zz = 0; zz < 4; zz += 1)
			{
				for (int i = 0; i < projectilecount[zz]; i += 1)
				{
					Vector2 perturbedSpeed = new Vector2(gotohere.X, gotohere.Y).RotatedByRandom(MathHelper.ToRadians(120)) * Main.rand.NextFloat(6, 12);
					int proj = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), projectiletype[zz], (int)((projectile.damage * 1f)* projectiledamage[zz]), projectile.knockBack / 10f, projectile.owner);
					Main.projectile[proj].thrown = true;
					Main.projectile[proj].magic = false;
					Main.projectile[proj].ranged = false;
					Main.projectile[proj].friendly = true;
					Main.projectile[proj].velocity = new Vector2(perturbedSpeed.X, perturbedSpeed.Y);
					Main.projectile[proj].hostile = false;
					if (i != 2)
					{
						Main.projectile[proj].timeLeft = 300;
						Main.projectile[proj].penetrate = 4;
					}
					if (zz==3)
					Main.projectile[proj].penetrate = 15;
					projectile.netUpdate = true;
					IdgProjectile.Sync(proj);
				}

			}

			int theproj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("SlimeBlast"), (int)((double)projectile.damage * 1.5f), projectile.knockBack, projectile.owner, 0f, 0f);
			Main.projectile[theproj].thrown = true;

			projectile.velocity = default(Vector2);
			projectile.type = ProjectileID.Grenade;
			return true;
		}

		public override bool PreAI()
		{
			
				for (int zz = 0; zz < Main.maxNPCs; zz += 1)
				{
				NPC npc = Main.npc[zz];
				if (!npc.dontTakeDamage && !npc.townNPC  && npc.active && npc.life>0)
				{
					Rectangle rech = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
					Rectangle rech2 = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
					if (rech.Intersects(rech2))
					{
						projectile.Damage();
						projectile.Kill();

					}
				}
			}

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
