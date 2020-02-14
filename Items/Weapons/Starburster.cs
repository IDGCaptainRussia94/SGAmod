using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SGAmod.Items.Weapons
{
	public class Starburster : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Burster");
			Tooltip.SetDefault("Fires 4 stars in bursts at the cost of 1, but requires a small ammount of mana\nuses Fallen Stars for Ammo, the stars cannot pass through platforms");
		}
		public override void SetDefaults()
		{
            item.CloneDefaults(ItemID.GoldenShower);
			item.damage = 38;
		 	item.useAnimation = 18;
			item.useTime = 5;
			item.reuseDelay = 20;
			item.knockBack = 6;
			item.value = 75000;
			item.rare = 5;
			item.magic = false;
			item.ranged = true;
			item.mana = 10;
			item.shoot = ProjectileID.StarAnise;
			item.shootSpeed = 9f;
			item.useAmmo = AmmoID.FallenStar;
		}

		public override bool ConsumeAmmo(Player player)
		{
			if (player.itemAnimation > 6)
				return false;

			return base.ConsumeAmmo(player);
		}

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int Itd = type;
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			float rander=Main.rand.Next(7000,8000)/2000;
			if (type == ProjectileID.StarAnise)
			Itd = 9;
			int probg=Terraria.Projectile.NewProjectile(position.X+(int)speedX*4, position.Y + (int)speedY * 4, speedX*(rander), speedY*(rander), Itd, damage, knockBack, player.whoAmI);
			Main.projectile[probg].ranged = true;
			Main.projectile[probg].tileCollide = true;
			return false;
		} 

	}

	public class Starfishburster : Starburster
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star'Fish' Burster");
			Tooltip.SetDefault("Fires 4 starfish in bursts at the cost of 1, but requires a small ammount of mana\nStarfish bounce off walls and pierce\nuses Starfish as ammo");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.GoldenShower);
			item.damage = 50;
			item.useAnimation = 18;
			item.useTime = 5;
			item.reuseDelay = 20;
			item.knockBack = 6;
			item.value = 250000;
			item.rare = 7;
			item.magic = false;
			item.ranged = true;
			item.mana = 10;
			//item.shoot = mod.ProjectileType("SunbringerFlare");
			item.shoot = mod.ProjectileType("StarfishProjectile");
			item.shootSpeed = 11f;
			item.useAmmo = 2626;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "StarfishBlaster", 1);
			recipe.AddIngredient(null, "Starburster", 1);
			recipe.AddIngredient(null, "WraithFragment4", 10);
			recipe.AddIngredient(null, "CryostalBar", 8);
			recipe.AddIngredient(null, "SharkTooth", 100);
			recipe.AddIngredient(ItemID.Coral, 8);
			recipe.AddIngredient(ItemID.Starfish, 5);
			recipe.AddIngredient(ItemID.HallowedBar, 6);
			recipe.AddTile(mod.TileType("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

}
