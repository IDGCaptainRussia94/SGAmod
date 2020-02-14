using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SGAmod.Items.Tools;
using Idglibrary;
using SGAmod.Buffs;

namespace SGAmod.Items.Weapons
{
	public class ElementalCascade : ModItem
	{
		int projectiletype = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elemental Cascade");
			Tooltip.SetDefault("Unleashes 4 elemental beams in cardinal directions towards the mouse cursor, swapping elements with each fire\nthe beams bounce off walls and are non solid until they stop moving, and deal different debuffs to enemies");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.magic = true;
			item.mana = 15;
			item.width = 40;
			item.height = 40;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 6;
			item.UseSound = SoundID.Item78;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("UnmanedBolt");
			item.shootSpeed = 4f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 5);
			recipe.AddIngredient(mod.ItemType("CryostalBar"), 5);
			recipe.AddIngredient(mod.ItemType("VirulentBar"), 5);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			projectiletype += 1;
			projectiletype = projectiletype % 4;
			type = mod.ProjectileType("ElementalCascadeShot");

			for (int i = 0; i < 4; i += 1)
			{

				Vector2 speez = new Vector2(speedX, speedY);
				speez=speez.RotatedBy(MathHelper.ToRadians(90 *i));
				Vector2 offset = speez;
				offset.Normalize();
				offset *= 48f;
			int probg = Projectile.NewProjectile(position.X + offset.X, position.Y + offset.Y, speez.X, speez.Y, type, damage, knockBack, player.whoAmI);
			Main.projectile[probg].friendly = true;
			Main.projectile[probg].hostile = false;
			Vector2 perturbedSpeed = new Vector2(speez.X, speez.Y).RotatedByRandom(MathHelper.ToRadians(5));
			Main.projectile[probg].velocity.X = perturbedSpeed.X;
			Main.projectile[probg].velocity.Y = perturbedSpeed.Y;
			Main.projectile[probg].owner = player.whoAmI;
			SGAprojectile modeproj = Main.projectile[probg].GetGlobalProjectile<SGAprojectile>();
				Main.projectile[probg].ai[0] = ((i + projectiletype) % 4);
				Main.projectile[probg].netUpdate = true;

				IdgProjectile.Sync(probg);

			}


			return false;

		}


	}

	public class LunarCascade : ElementalCascade
	{
		int projectiletype = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Cascade");
			Tooltip.SetDefault("Unleashes several beams in a complete circle around the player that travel far and completely melt enemies\nthe beams bounce off walls and are non solid until they stop moving, and deal different debuffs to enemies");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.magic = true;
			item.mana = 25;
			item.width = 40;
			item.height = 40;
			item.useTime = 5;
			item.useAnimation = 50;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 10;
			item.UseSound = SoundID.Item78;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("UnmanedBolt");
			item.shootSpeed = 8f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("ElementalCascade"), 1);
			recipe.AddRecipeGroup("SGAmod:CelestialFragments", 10);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			projectiletype += 1;
			projectiletype = projectiletype % 4;
			type = mod.ProjectileType("LunarCascadeShot");

				Vector2 speez = new Vector2(speedX, speedY);
				speez = speez.RotatedBy(MathHelper.ToRadians((float)player.itemAnimation*(360f/player.itemAnimationMax)));
				Vector2 offset = speez;
				offset.Normalize();
				offset *= 48f;
				int probg = Projectile.NewProjectile(position.X + offset.X, position.Y + offset.Y, speez.X, speez.Y, type, damage, knockBack, player.whoAmI);
				Main.projectile[probg].friendly = true;
				Main.projectile[probg].hostile = false;
				Vector2 perturbedSpeed = new Vector2(speez.X, speez.Y).RotatedByRandom(MathHelper.ToRadians(5));
				Main.projectile[probg].velocity.X = perturbedSpeed.X;
				Main.projectile[probg].velocity.Y = perturbedSpeed.Y;
				Main.projectile[probg].owner = player.whoAmI;
				SGAprojectile modeproj = Main.projectile[probg].GetGlobalProjectile<SGAprojectile>();
				Main.projectile[probg].ai[0] = ((projectiletype));
				Main.projectile[probg].netUpdate = true;

				IdgProjectile.Sync(probg);

			return false;

		}


	}

	public class LunarCascadeShot : ElementalCascadeShot
	{
		public override int stopmoving => 240;
		public override int fadeinouttime => 30;

		//public Color[] colors = { Color.Orange, Color.Purple, Color.LimeGreen, Color.Yellow };
		//public int[] buffs = { ModContent.BuffType<ThermalBlaze>(), BuffID.ShadowFlame, ModContent.BuffType<AcidBurn>(), BuffID.Ichor };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Cascade");
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1000;
			projectile.light = 0.25f;
			projectile.width = 24;
			projectile.timeLeft = 400;
			projectile.height = 24;
			projectile.extraUpdates = 1;
			projectile.magic = true;
			projectile.tileCollide = true;
		}

	}

		public class ElementalCascadeShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elemental Cascade");
		}

		private List<Vector2> oldPos = new List<Vector2>();
		public Color[] colors = { Color.Orange, Color.Purple, Color.LawnGreen, Color.Aqua };
		public int[] buffs = {BuffID.OnFire,BuffID.ShadowFlame,BuffID.DryadsWardDebuff,BuffID.Frostburn};

		public virtual int stopmoving => 90;
		public virtual int fadeinouttime => 30;
		public virtual int bufftime => 60 * 8;

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1000;
			projectile.light = 0.25f;
			projectile.width = 24;
			projectile.timeLeft = 60 * 3;
			projectile.height = 24;
			projectile.magic = true;
			projectile.tileCollide = true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			foreach (Vector2 position in oldPos)
			{
				projHitbox.X = (int)position.X;
				projHitbox.Y = (int)position.Y;
				if (projHitbox.Intersects(targetHitbox))
				{
					return true;
				}
			}
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(buffs[(int)projectile.ai[0]],bufftime);
			if (this.GetType() == typeof(LunarCascadeShot))
			{
				target.immune[projectile.owner] = 3;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(buffs[(int)projectile.ai[0]],bufftime);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModContent.GetTexture("Terraria/Projectile_" + 540);
			float fadin = MathHelper.Clamp(1f-((float)projectile.timeLeft-stopmoving) / fadeinouttime, 0.1f,0.75f);
			if (projectile.timeLeft<(int)fadeinouttime)
				fadin = ((float)projectile.timeLeft/ fadeinouttime) *0.75f;
			for (int i = 0; i < oldPos.Count; i += 1)
			{
				Vector2 drawPos = oldPos[i] - Main.screenPosition;
				spriteBatch.Draw(texture, drawPos, null, Color.Lerp(lightColor, colors[(int)projectile.ai[0]], 0.75f)* fadin, 1, new Vector2(texture.Width / 2f, texture.Height / 2f), new Vector2(0.4f, 0.4f), SpriteEffects.None, 0f);
			}
			return false;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (projectile.velocity.Length() > 0f || projectile.timeLeft < fadeinouttime)
				return false;
			return base.CanHitNPC(target);
		}

		public override bool CanHitPlayer(Player target)
		{
			if (projectile.velocity.Length()>0f || projectile.timeLeft < fadeinouttime)
			return false;
			return base.CanHitPlayer(target);
		}

		public override string Texture
		{
			get { return "Terraria/Projectile_" + 5; }
		}


		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			{
				Main.PlaySound(SoundID.Item10, projectile.Center);
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}

		public override void AI()
		{
			if (projectile.timeLeft < stopmoving+ (fadeinouttime/2))
			{
				projectile.velocity = default(Vector2);
			}
			else
			{
				oldPos.Add(projectile.Center);
			}



		}
	}


}