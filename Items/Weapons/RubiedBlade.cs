using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SGAmod.Items.Tools;
using Idglibrary;

namespace SGAmod.Items.Weapons
{
	public class RubiedBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rubied Blade");
			Tooltip.SetDefault("Rains down ruby bolts from the sky above enemies hit by the blade");
		}
		public override void SetDefaults()
		{
			item.damage = 36;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 15, 0, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			if (!Main.dedServ)
			{
				item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/GlowMasks/RubiedBlade_Glow");
			}

		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Vector2 hereas = new Vector2(Main.rand.Next(-1000, 1000), Main.rand.Next(-1000, 1000)); hereas.Normalize();
			hereas *= Main.rand.NextFloat(50f, 100f);
			hereas += target.Center;
			hereas += new Vector2(0, -800);
			Vector2 gohere = ((hereas+new Vector2(Main.rand.NextFloat(-100f, 100f), 800f)) - hereas); gohere.Normalize(); gohere *= 15f;
			int proj = Projectile.NewProjectile(hereas, gohere, ProjectileID.RubyBolt, (int)(damage*1), knockBack, player.whoAmI);
			Main.projectile[proj].melee = true;
			Main.projectile[proj].magic = false;
			Main.projectile[proj].timeLeft = 300;
			IdgProjectile.Sync(proj);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{

			int dustIndex = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 32, 32, 113);
			Dust dust = Main.dust[dustIndex];
			Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
			dust.velocity = (randomcircle / 2f) + (player.itemRotation.ToRotationVector2());
			dust.scale *= 1.5f + Main.rand.Next(-30, 31) * 0.01f;
			dust.fadeIn = 0f;
			dust.noGravity = true;
			dust.color = Color.LightSkyBlue;
		}

	}


}