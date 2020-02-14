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

	public class CrateBossWeaponSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prosperity Rod");
			Tooltip.SetDefault("Summons Midas Portals to shower your enemies in wealth, painfully\nOrdering your minions to attack a target will move the center of the circle to the target and the portals will gain an extra weaker attack VS the closest enemy\nAttacks inflict Midas\n'money money, it acts so funny...'");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 45;
			item.knockBack = 3f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.value = Item.buyPrice(0, 20, 0, 0);
			item.rare = 7;
			item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			item.noMelee = true;
			item.summon = true;
			item.buffType = mod.BuffType("MidasMinionBuff");
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			item.shoot = mod.ProjectileType("MidasPortal");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(item.buffType, 2);

			return true;
		}

	}

		public class MidasPortal : ModProjectile
	{
		protected float idleAccel = 0.05f;
		protected float spacingMult = 1f;
		protected float viewDist = 400f;
		protected float chaseDist = 200f;
		protected float chaseAccel = 6f;
		protected float inertia = 40f;
		protected float shootCool = 90f;
		protected float shootSpeed;
		protected int shoot;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Midas Portal");
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[projectile.type] = 1;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

			// These below are needed for a minion
			// Denotes that this projectile is a pet or minion
			Main.projPet[projectile.type] = true;
			// This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public sealed override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.tileCollide = false;
			projectile.friendly = false;
			projectile.minion = true;
			// Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			projectile.minionSlots = 1f;
			// Needed so the minion doesn't despawn on collision with enemies or tiles
			projectile.penetrate = -1;
			projectile.timeLeft = 60;
		}


		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles()
		{
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage()
		{
			return false;
		}

		public virtual void CreateDust()
		{
		}

		public virtual void SelectFrame()
		{
		}

		public override void AI()
		{
			//if (projectile.owner == null || projectile.owner < 0)
			//return;


			Player player = Main.player[projectile.owner];
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<MidasMinionBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<MidasMinionBuff>()))
			{
				projectile.timeLeft = 2;
			}
			Vector2 gothere = player.Center;
			projectile.localAI[0] += 1;

			int target2 = Idglib.FindClosestTarget(0, projectile.Center, new Vector2(0f, 0f), true, true, true, projectile);
			NPC them = Main.npc[target2];
			NPC oldthem = null;

			if (player.HasMinionAttackTargetNPC)
			{
				oldthem = them;
				them = Main.npc[player.MinionAttackTargetNPC];
				gothere = them.Center;
			}

			if (them!=null && them.active)
			{
			if ((them.Center-projectile.Center).Length() < 500 && Collision.CanHitLine(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, new Vector2(them.Center.X, them.Center.Y), 1, 1) && them.CanBeChasedBy())
				{
					projectile.ai[0] += 1;

					if (projectile.ai[0]%20==0)
					{
						Main.PlaySound(18, (int)projectile.Center.X, (int)projectile.Center.Y, 0, 1f, 0.25f);
						int thisoned = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y,0,0, ProjectileID.GoldCoin, projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
						Main.projectile[thisoned].minion = true;
						Main.projectile[thisoned].velocity = (them.Center - projectile.Center);
						Main.projectile[thisoned].velocity.Normalize(); Main.projectile[thisoned].velocity *= 12f; Main.projectile[thisoned].velocity=Main.projectile[thisoned].velocity.RotateRandom(MathHelper.ToRadians(15));
						Main.projectile[thisoned].penetrate = 1;
						Main.projectile[thisoned].ranged = false;
						Main.projectile[thisoned].netUpdate = true;
						IdgProjectile.AddOnHitBuff(thisoned, BuffID.Midas, 60 * 5);
						IdgProjectile.Sync(thisoned);
					}

				}

				if (oldthem != null) {
					if ((oldthem.Center - projectile.Center).Length() < 500 && Collision.CanHitLine(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, new Vector2(oldthem.Center.X, oldthem.Center.Y), 1, 1) && oldthem.CanBeChasedBy())
					{

						if (projectile.ai[0] % 35 == 0)
						{
							Main.PlaySound(18, (int)projectile.Center.X, (int)projectile.Center.Y, 0, 0.75f, -0.5f);
							int thisoned = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileID.SilverCoin, (int)((float)projectile.damage*0.75f), projectile.knockBack, Main.player[projectile.owner].whoAmI);
							Main.projectile[thisoned].minion = true;
							Main.projectile[thisoned].velocity = (oldthem.Center - projectile.Center);
							Main.projectile[thisoned].velocity.Normalize(); Main.projectile[thisoned].velocity *= 10f; Main.projectile[thisoned].velocity = Main.projectile[thisoned].velocity.RotateRandom(MathHelper.ToRadians(15));
							Main.projectile[thisoned].penetrate = 1;
							Main.projectile[thisoned].ranged = false;
							Main.projectile[thisoned].netUpdate = true;
							IdgProjectile.AddOnHitBuff(thisoned, BuffID.Midas, 60 * 2);
							IdgProjectile.Sync(thisoned);
						}
					}

				}


			}

			int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 124);
			Main.dust[dust].scale = 0.7f;
			Main.dust[dust].velocity = projectile.velocity*0.2f;
			Main.dust[dust].noGravity = true;

			float us = 0f;
			float maxus = 0f;

			for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
			{
				Projectile currentProjectile = Main.projectile[i];
					if (currentProjectile.active // Make sure the projectile is active
					&& currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
					&& currentProjectile.type == projectile.type){ // Make sure the projectile is of the same type as this javelin

					if (i == projectile.whoAmI)
						us = maxus;
					maxus += 1f;

				}

			}
			Vector2 there = player.Center;

			double angles = MathHelper.ToRadians((float)((us/maxus)*360.00)-90f);
			float dist = 256f;//Main.rand.NextFloat(54f, 96f);
			Vector2 here = new Vector2((float)Math.Cos(angles), (float)Math.Sin(angles))* dist;
			Vector2 where = gothere + here;

			if ((where - projectile.Center).Length() > 8f)
			{
				projectile.velocity += (where - projectile.Center) * 0.005f;
				projectile.velocity *= 0.975f;
			}
			float maxspeed = Math.Min(projectile.velocity.Length(), 16);
			projectile.velocity.Normalize();
			projectile.velocity *= maxspeed;



			Lighting.AddLight(projectile.Center, Color.Yellow.ToVector3() * 0.78f);

		}

		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/Javelins/StoneJavelin"); }
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			Texture2D tex = ModContent.GetTexture("Terraria/Projectile_"+ProjectileID.CoinPortal);
			
			Vector2 drawOrigin = new Vector2(tex.Width, tex.Height / 4) / 2f;
			Vector2 drawPos = ((projectile.Center - Main.screenPosition)) + new Vector2(0f, 4f);
			Color color = Color.Lerp((projectile.GetAlpha(lightColor) * 0.5f),Color.White,0.5f); //* ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
			int timing = (int)(projectile.localAI[0] / 8f);
			timing %= 4;
			timing *= ((tex.Height) / 4);
			spriteBatch.Draw(tex, drawPos, new Rectangle(0, timing, tex.Width, (tex.Height - 1) / 4), color, projectile.velocity.X * 0.04f, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

	}
	public class MidasMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Midas Portal");
			Description.SetDefault("Portals to planes of wealth will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[mod.ProjectileType("MidasPortal")] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}

}
