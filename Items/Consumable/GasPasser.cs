﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SGAmod.Buffs;
using SGAmod.Projectiles;
using Idglibrary;

namespace SGAmod.Items.Consumable
{
    class GasPasser : ModItem
    {

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Gas Passer");
			Tooltip.SetDefault("Throws gasoline canisters on your enemies dosing them in gas, which you can ignite them for massive damage over time!\nDoes more damage against enemies with more max HP\nIncures 30 seconds of Action Cooldown, which prevents reuse\nCombustion increases the damage of burning-based debuffs greatly\nLess ass then the source material");

		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/GasPasser"); }
		}

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.shootSpeed = 12f;
			item.shoot = mod.ProjectileType("GasPasserProj");
			item.width = 16;
			item.height = 16;
			item.maxStack = 10;
			item.consumable = true;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 40;
			item.useTime = 40;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 0, 20, 0);
			item.rare = 2;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<Buffs.ActionCooldown>());
		}

		public override bool ConsumeItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<ActionCooldown>(), 30 * 60);
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/gas_can_throw").WithVolume(.5f).WithPitchVariance(.15f), item.Center);
			player.AddBuff(ModContent.BuffType<ActionCooldown>(), 30 * 60);
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Explosives);
			recipe.AddIngredient(mod.ItemType("IceFairyDust"), 3);
			recipe.AddIngredient(mod.ItemType("WraithFragment4"),5);
			recipe.AddIngredient(mod.ItemType("MurkyGel"), 10);
			recipe.AddIngredient(ItemID.CursedFlames, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this,3);
			recipe.AddRecipe();
		}

	}

	public class GasPasserProj : DosedArrow
	{

		double keepspeed = 0.0;
		float homing = 0.06f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gas Can");
		}
		public override float beginhoming => 99990f;

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/GasPasser"); }
		}

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			projectile.width = 16;
			projectile.height = 16;
			projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ranged = true;
			projectile.timeLeft = 240;
			projectile.arrow = true;
			projectile.damage = 0;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override void AI()
		{
			effects(0);

			projectile.ai[0] = projectile.ai[0] + 1;
			projectile.velocity.Y += 0.15f;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;


			NPC target = Main.npc[Idglib.FindClosestTarget(0, projectile.Center, new Vector2(0f, 0f), true, true, true, projectile)];
			if (target != null)
			{
				if (new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height).Intersects
					(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height)))
				{
					projectile.Kill();
				}
			}


		}

		public override void effects(int type)
		{
			base.effects(type);
			if (type == 1)
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/gas_can_explode").WithVolume(.5f).WithPitchVariance(.15f), projectile.Center);
				projectile.type = 0;
				int proj = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(0, 0), mod.ProjectileType("GasCloud"), 1, projectile.knockBack, Main.player[projectile.owner].whoAmI);
			}

		}


	}

}



