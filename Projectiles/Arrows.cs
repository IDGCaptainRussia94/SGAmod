using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;
using SGAmod.Dusts;

namespace SGAmod.Projectiles
{

	public class UnmanedArrow : ModProjectile
	{

		double keepspeed = 0.0;
		float homing = 0.03f;
		public Player P;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Novus Arrow");
		}

		public virtual float beginhoming
		{
			get
			{
				return 20f;
			}
		}

		public virtual float gravity
		{
			get
			{
				return 0.1f;
			}
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
			projectile.arrow = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.type = ProjectileID.WoodenArrowHostile;
			effects(1);
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!target.friendly)
				projectile.Kill();
		}

		public virtual void effects(int type)
		{
			if (type == 0)
			{
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 1; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, mod.DustType("NovusSparkle"), 0f, 0f, 50, Main.hslToRgb(0.83f, 0.5f, 0.25f), 0.8f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.3f;
				}
			}
			if (type == 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 15; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, mod.DustType("NovusSparkle"), projectile.velocity.X + (float)(Main.rand.Next(-50, 50) / 15f), projectile.velocity.Y + (float)(Main.rand.Next(-50, 50) / 15f), 50, Main.hslToRgb(0.83f, 0.5f, 0.25f), 2.2f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.7f;
				}

			}

		}

		public override void AI()
		{
			effects(0);

			projectile.ai[0] = projectile.ai[0] + 1;
			projectile.velocity.Y += gravity;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;



			float previousspeed = projectile.velocity.Length();
			if (projectile.ai[0] < (beginhoming))
				return;
			NPC target = Main.npc[Idglib.FindClosestTarget(0, projectile.Center, new Vector2(0f, 0f), true, true, true, projectile)];
			if (target != null) {
				if ((target.Center - projectile.Center).Length() < 400f) {
					if (projectile.ai[0] < (250f)) {
						projectile.velocity = projectile.velocity + (projectile.DirectionTo(target.Center) * ((float)previousspeed * homing));
						projectile.velocity.Normalize();
						projectile.velocity = projectile.velocity * previousspeed;
					} } }


		}






	}
	public class UnmanedArrow2 : UnmanedArrow
	{

		double keepspeed = 0.0;
		float homing = 0.12f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Notchvos Arrow");
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
			projectile.arrow = true;
			projectile.penetrate = 2;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.velocity = projectile.velocity * -1;
			projectile.position += projectile.velocity * 1f;
			target.immune[projectile.owner] = 1;
			//base.OnHitNPC(target, damage, knockback, crit);
		}

		public override void effects(int type)
		{
			if (type == 0)
			{
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 1; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, mod.DustType("NovusSparkleBlue"), 0f, 0f, 50, Color.Lerp(Color.AliceBlue, Color.White, Main.rand.NextFloat()), 1.3f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.3f;
				}
			}
			if (type == 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 15; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, mod.DustType("NovusSparkleBlue"), projectile.velocity.X + (float)(Main.rand.Next(-50, 50) / 15f), projectile.velocity.Y + (float)(Main.rand.Next(-50, 50) / 15f), 50, Color.Lerp(Color.AliceBlue, Color.White, Main.rand.NextFloat()), 3f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.7f;
				}

			}

		}


	}

	public class PitchArrow : UnmanedArrow
	{

		double keepspeed = 0.0;
		float homing = 0.06f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("pitched Arrow");
		}

		public float getvalue()
		{


		return (float)projectile.timeLeft;
		}


		public override float beginhoming => 99990f;

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
			projectile.arrow = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(0,100)<20)
			IdgNPC.AddBuffBypass(target.whoAmI, BuffID.Oiled, 60 * 5);
			target.AddBuff(BuffID.Oiled, 60 * 20);
		}

		public override void effects(int type)
		{
			if (type == 0)
			{
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 1; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, 109, projectile.velocity.X*0.4f, projectile.velocity.Y * 0.4f, 50, Color.Lerp(Color.AliceBlue, Color.White, Main.rand.NextFloat()), 1.3f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.3f;
				}
			}
			if (type == 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 25; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, 109, projectile.velocity.X / 4f + (float)(Main.rand.Next(-100, 100) / 15f), projectile.velocity.Y / 4f + (float)(Main.rand.Next(-100, 100) / 15f), 50, Color.Lerp(Color.AliceBlue, Color.White, Main.rand.NextFloat()), 1f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.7f;
				}

			}

		}


	}

	public class DosedArrow : UnmanedArrow2
	{

		double keepspeed = 0.0;
		float homing = 0.5f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dosed Arrow");
		}

		public override float beginhoming
		{
			get
			{
				return 1f;
			}
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
			projectile.arrow = true;
			projectile.penetrate = 2;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(0, 100) < 50)
			target.AddBuff(mod.BuffType("DosedInGas"), 60 * 5);
			if (Main.rand.Next(0, 100) < 20)
			IdgNPC.AddBuffBypass(target.whoAmI, BuffID.Oiled, 60 * 10);
			base.OnHitNPC(target, damage, knockback, crit);
			if (target.HasBuff(BuffID.OnFire))
			{
				projectile.type = ProjectileID.HellfireArrow;
				projectile.penetrate = 0;
				projectile.Kill();
				return;
			}
		}

		public override bool PreKill(int timeLeft)
		{
			if (projectile.type != ProjectileID.HellfireArrow)
			projectile.type = ProjectileID.WoodenArrowFriendly;
			effects(1);
			return true;
		}


		public override void effects(int type)
		{
			if (type == 0)
			{
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 1; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 211, projectile.velocity.X, projectile.velocity.Y, 50, Color.DarkGreen, 0.8f);
					Main.dust[num316].noLight = true;
					Dust dust = Main.dust[num316];
					dust.velocity *= 0.2f;
					Dust dust9 = Main.dust[num316];
					dust9.velocity.Y = dust9.velocity.Y + 0.2f;
					dust = Main.dust[num316];
					dust.velocity += projectile.velocity*Main.rand.NextFloat(1f/6f, 0.5f);
				}
			}
			if (type == 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 25; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, 211, projectile.velocity.X / 4f + (float)(Main.rand.Next(-100, 100) / 15f), projectile.velocity.Y / 4f + (float)(Main.rand.Next(-100, 100) / 15f), 50, Color.DarkGreen, Main.rand.NextFloat(0.7f,1.2f));
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.7f;
				}

			}

		}

	}

	public class WraithArrow : PitchArrow
	{

		double keepspeed = 0.0;
		float homing = 0.06f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wraith Arrow");
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
			projectile.arrow = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.type = ProjectileID.DD2BetsyArrow;
			if (Main.rand.Next(0, 100) < 50)
				target.AddBuff(BuffID.BetsysCurse, 60 * 8);
		}

		public override bool PreKill(int timeLeft)
		{
			effects(1);
			return true;
		}


		public override void effects(int type)
		{
			if (type == 0)
			{
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 3; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(projectile.position+positiondust, projectile.width, projectile.height, 211, projectile.velocity.X, projectile.velocity.Y, 50, Color.DarkGreen, 1.8f);
					Main.dust[num316].noGravity = true;
					Dust dust = Main.dust[num316];
					dust.velocity *= 0.05f;
					Dust dust9 = Main.dust[num316];
					dust9.velocity.Y = dust9.velocity.Y + 0.2f;
					dust = Main.dust[num316];
					//dust.velocity += projectile.velocity * Main.rand.NextFloat(1f / 6f, 0.5f);
				}
			}
			if (type == 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 25; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, 211, projectile.velocity.X / 4f + (float)(Main.rand.Next(-100, 100) / 15f), projectile.velocity.Y / 4f + (float)(Main.rand.Next(-100, 100) / 15f), 50, Color.DarkGreen, Main.rand.NextFloat(0.7f, 4.2f));
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.7f;
				}

			}

		}
	}


		public class WindfallArrow : UnmanedArrow
	{
		private Vector2[] oldPos = new Vector2[5];
		private float[] oldRot = new float[5];
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Windfall Arrow");
		}

		public override float beginhoming => 99990f;
		public override float gravity => 0.025f;

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			base.SetDefaults();
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player owner=Main.player[projectile.owner];
			if (owner!=null && !owner.dead)
			{
				owner.wingTime = owner.wingTime > owner.wingTimeMax ? owner.wingTimeMax : owner.wingTime + 10;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			if (projectile.ai[1] > 0)
				return false;

			Texture2D tex = Main.projectileTexture[projectile.type];
			Vector2 drawOrigin = new Vector2(tex.Width, tex.Height / 10) / 2f;

			//oldPos.Length - 1
			for (int k = oldPos.Length - 1; k >= 0; k -= 1)
			{
				Vector2 drawPos = ((oldPos[k] - Main.screenPosition)) + new Vector2(0f, 0f);
				Color color = Color.Lerp(Color.White, lightColor, (float)k/5);
				float alphaz= (1f - (float)(k + 1) / (float)(oldPos.Length + 2));
				spriteBatch.Draw(tex, drawPos, null, color* alphaz, oldRot[k], drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		public override void effects(int type)
		{
			if (type == 0)
			{
				for (int k = oldPos.Length - 1; k > 0; k--)
				{
					oldPos[k] = oldPos[k - 1];
					oldRot[k] = oldRot[k - 1];
				}
				oldPos[0] = projectile.Center;
				oldRot[0] = projectile.rotation;
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * Main.rand.NextFloat(-0.75f,0.75f);
				for (int num315 = 0; num315 < 1; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, ModContent.DustType<TornadoDust>(), projectile.velocity.X * 0.0f, projectile.velocity.Y * 0.0f, 50, Color.Lerp(Color.AliceBlue, Color.White, Main.rand.NextFloat())*0.5f, 0.5f);
					Main.dust[num316].noGravity = true;
				}
			}
			if (type == 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
				for (int num315 = 0; num315 < 25; num315 = num315 + 1)
				{
					int num316 = Dust.NewDust(new Vector2(projectile.position.X - 1, projectile.position.Y) + positiondust, projectile.width, projectile.height, 109, projectile.velocity.X / 4f + (float)(Main.rand.Next(-100, 100) / 15f), projectile.velocity.Y / 4f + (float)(Main.rand.Next(-100, 100) / 15f), 50, Color.Lerp(Color.AliceBlue, Color.White, Main.rand.NextFloat()), 1f);
					Main.dust[num316].noGravity = true;
					Dust dust3 = Main.dust[num316];
					dust3.velocity *= 0.7f;
				}

			}

		}


	}


}