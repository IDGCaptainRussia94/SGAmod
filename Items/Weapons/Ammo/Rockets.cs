using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;
using SGAmod.Items.Consumable;
using SGAmod.Items.Weapons;

namespace SGAmod.Items.Weapons.Ammo
{

	public class Jarocket : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jarocket");
			Tooltip.SetDefault("Rocket Propelled Jar based karate!");
		}
		public override void SetDefaults()
		{
			item.damage = 72;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 3.5f;
			item.value = 250;
			item.rare = 8;
			item.shoot = ModContent.ProjectileType<JarocketProj>();   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 4.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Rocket;
		}

		public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
		{
			type = ModContent.ProjectileType<JarocketProj>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 3);
			recipe.AddIngredient(mod.ItemType("Jarate"), 1);
			recipe.AddIngredient(mod.ItemType("LuminiteWraithNotch"), 1);
			recipe.AddIngredient(ItemID.RocketIII, 50);
			recipe.AddTile(mod.TileType("ReverseEngineeringStation"));
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}

	public class JackpotRocketItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rigged Jackpot");
			Tooltip.SetDefault("Rigged to allow launchers to shoot jackpot rockets!\nNon Consumable Ammo Type");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 3.5f;
			item.value = 500000;
			item.rare = 10;
			item.shoot = mod.ProjectileType("JackpotRocket");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 4.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Rocket;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return false;
		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/CrateBossWeaponRanged"); }
		}

		public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
		{
			type = mod.ProjectileType("JackpotRocket");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("CrateBossWeaponRanged"), 1);
			recipe.AddIngredient(mod.ItemType("MoneySign"), 8);
			recipe.AddTile(mod.TileType("ReverseEngineeringStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}

	public class JarocketProj : JarateProj
	{

		double keepspeed=0.0;
		public Player P;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jarocket");
		}


		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			projectile.width = 16;
			projectile.height = 16;
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.hostile=false;
			projectile.friendly=true;
			projectile.tileCollide = true;
			projectile.ranged = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override string Texture
		{
			get { return "Terraria/Projectile_" + ProjectileID.RocketII; }
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("Sodden"), 60 * 15);
			target.immune[projectile.owner] = 5;
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.type=ProjectileID.RocketIII;
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
			Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
			int theproj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Explosion"), (int)((double)projectile.damage * 0.75f), projectile.knockBack, projectile.owner, 0f, 0f);
			Main.projectile[theproj].ranged = true;
			effects(1);

			return true;
		}

		public override void AI()
		{
		Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
		for (int num315 = 0; num315 < 3; num315 = num315 + 1)
			{
				Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
				int num316 = Dust.NewDust(new Vector2(projectile.position.X-1, projectile.position.Y)+positiondust, projectile.width, projectile.height, 75, 0f, 0f, 50, Main.hslToRgb(0.10f, 0.5f, 0.75f), 1.8f);
				Main.dust[num316].noGravity = true;
				Dust dust3 = Main.dust[num316];
				dust3.velocity = (-projectile.velocity)+(randomcircle*(0.5f))*((float)num315/3f);
				dust3.velocity.Normalize();
			}

		for (int num315 = 1; num315 < 16; num315 = num315 + 1)
			{
				if (Main.rand.Next(0,100)<25){
				Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
				int num316 = Dust.NewDust(new Vector2(projectile.position.X-1, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 50,Color.Goldenrod, 1.33f);
				Main.dust[num316].noGravity = true;
				Dust dust3 = Main.dust[num316];
				dust3.velocity = (randomcircle*2.5f*Main.rand.NextFloat())+(projectile.velocity);
				dust3.velocity.Normalize();
			}}

		projectile.ai[0]=projectile.ai[0]+1;
		projectile.velocity.Y+=0.1f;
		projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f; 
		}


	}

}