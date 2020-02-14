using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
using Idglibrary;

namespace SGAmod.Items.Weapons
{
	public class CrateBossWeaponRanged : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Jackpot");
			Tooltip.SetDefault("Launches money-filled rockets that explode into coins!\nInflicts Midas on enemies\n'Once was a pre-order bonus!'");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.SnowmanCannon);
			item.damage = 40;
			item.width = 48;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 20;
			item.knockBack = 6;
			item.value = 1000000;
			item.ranged = true;
			item.rare = 7;
			item.shootSpeed = 14f;
			item.noMelee = true;
			item.useAmmo = AmmoID.Rocket;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-18, -6);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float speed = 8f;
			float numberProjectiles = 1;
			float rotation = MathHelper.ToRadians(8);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;

			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = (new Vector2(speedX, speedY) * speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, (float)Main.rand.Next(0, 100) / 100f)) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("JackpotRocket"), damage, knockBack, player.whoAmI);
				Main.projectile[proj].friendly = true;
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].timeLeft = 600;
				Main.projectile[proj].knockBack = item.knockBack;
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 15);
			}
		}

	}

	public class PrismalLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prismal Launcher");
			Tooltip.SetDefault("Launches a myriad of rockets that may inflict a myriad of debuffs\n'Something something placeholder sprite'\n'Something else something rocket launcher upgrade'");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.RocketLauncher);
			item.damage = 100;
			item.width = 48;
			item.height = 48;
			item.useTime = 30;
			item.useAnimation = 30;
			item.knockBack = 6;
			item.value = 500000;
			item.ranged = true;
			item.rare = 9;
			item.shootSpeed = 14f;
			item.noMelee = true;
			item.useAmmo = AmmoID.Rocket;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-18, -6);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float speed = 4f;
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;

			for (int i = 0; i < 7; i += 3)
			{

				Vector2 perturbedSpeed = (new Vector2(speedX, speedY) * (speed+((float)i))).RotatedBy(MathHelper.Lerp(-rotation, rotation, (float)Main.rand.Next(0, 100) / 100f)) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				speedX = perturbedSpeed.X;
				speedY = perturbedSpeed.Y;

			int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].timeLeft = 600;
			Main.projectile[proj].knockBack = item.knockBack;

			if (Main.rand.Next(0,100)<20)
			IdgProjectile.AddOnHitBuff(proj,mod.BuffType("ThermalBlaze"), 60*10);
			if (Main.rand.Next(0, 100) < 20)
				IdgProjectile.AddOnHitBuff(proj, BuffID.DryadsWardDebuff, 60 * 10);
			if (Main.rand.Next(0, 100) < 20)
				IdgProjectile.AddOnHitBuff(proj, BuffID.ShadowFlame, 60 * 10);
			if (Main.rand.Next(0, 100) < 20)
				IdgProjectile.AddOnHitBuff(proj, BuffID.Venom, 60 * 10);

			}

			return false;

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new StarMetalRecipes(mod);
			recipe.AddIngredient(ItemID.RocketLauncher, 1);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 15);
			recipe.AddTile(mod.TileType("PrismalStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}


	}
}