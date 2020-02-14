using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.Items.Weapons
{
	public class FSRG : ModItem
	{
		private int varityshot=0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("F.S.R.G");
			Tooltip.SetDefault("Furious Sting-Ray Gun\nRapidly fires flaming stingers");
		}

		public override void SetDefaults()
		{
			item.damage = 22;
			item.ranged = true;
			item.width = 32;
			item.height = 62;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = 10000;
			item.rare = 11;
			item.UseSound = SoundID.Item99;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 50f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-24, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			varityshot+=1;
			varityshot%=3;

			float speed=1.5f;
			float numberProjectiles = 1;
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;

			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = (new Vector2(speedX, speedY)*speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, (float)Main.rand.Next(0,100)/100f)) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				int proj=Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FlamingStinger"), damage, knockBack, player.whoAmI);
				Main.projectile[proj].friendly=true;
				Main.projectile[proj].hostile=false;
				Main.projectile[proj].timeLeft=60;
				Main.projectile[proj].penetrate=3;
				Main.projectile[proj].knockBack=item.knockBack;
				IdgProjectile.AddOnHitBuff(proj,BuffID.OnFire,60*6);
			}
			return false;
		}

		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(mod.ItemType("VirulentBar"), 5); 
			recipe.AddIngredient(ItemID.SoulofFright, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
            recipe.AddRecipe();
		}

	}

	public class FlamingStinger : ModProjectile
	{

		int fakeid=ProjectileID.Stinger;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Stinger");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.width = 8;
			projectile.height = 8;
		}

		public override string Texture
		{
			get { return "Terraria/Projectile_" + fakeid; }
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.type=fakeid;
			return true;
		}

		public override void AI()
		{
        int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6);
        Main.dust[dust].scale = 0.8f;
        Main.dust[dust].noGravity = false;
        Main.dust[dust].velocity = projectile.velocity*(float)(Main.rand.Next(20,100)*0.005f);
        projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        target.immune[projectile.owner] = 15;
        }

	}


}
