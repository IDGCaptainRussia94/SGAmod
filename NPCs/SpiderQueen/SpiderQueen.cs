using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.NPCs.SpiderQueen
{
	[AutoloadBossHead]
	public class SpiderQueen : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spider Queen");
		}
		public override void SetDefaults()
		{
			npc.width = 48;
			npc.height = 48;
			npc.damage = 70;
			npc.defense = 5;
			npc.boss = true;
			npc.lifeMax = 5000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 50000f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = 0;
			music = MusicID.Boss3;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.value = 50000f;
		}

		public override void NPCLoot()
		{

				for (int i = 0; i <= Main.rand.Next(25,45)*(Main.expertMode ? 2 : 1); i++){
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VialofAcid"));
				}
				if (Main.expertMode)
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CorrodedShield"));
				if (Main.rand.Next(0,3)==0)
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AmberGlowSkull"));

		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}

		public void GetAngleDifferenceBlushiMagic(Vector2 targetPos, out float angle1, out float angle2)
		{

			Vector2 offset = targetPos - npc.Center;
			float rotation = MathHelper.PiOver2;
			if (offset != Vector2.Zero)
			{
				rotation = offset.ToRotation();
			}
			targetPos = Main.player[(int)npc.target].Center;
			offset = targetPos - npc.Center;
			float newRotation = MathHelper.PiOver2;
			if (offset != Vector2.Zero)
			{
				newRotation = offset.ToRotation();
			}
			if (newRotation < rotation - MathHelper.Pi)
			{
				newRotation += MathHelper.TwoPi;
			}
			else if (newRotation > rotation + MathHelper.Pi)
			{
				newRotation -= MathHelper.TwoPi;
			}

			angle1 = rotation;
			angle2 = newRotation;

		}

		public int phase
		{
			get
			{
				return (int)npc.ai[1];

			}
			set
			{
				npc.ai[1]=(int)value;

			}
		}

		public void DoPhase(int phasetype)
		{
		if (phasetype >  0)
			{
				if (phase == 1)
				{
					if (npc.life < npc.lifeMax * (Main.expertMode ? 0.5f : 0.33f))
					{
						npc.ai[0] = 10000;
						phase = 2;
						return;
					}
				}
				if (phase == 0)
				{
					npc.ai[0] = 10000;
					phase = 1;
					return;
				}


				//Phase 2-Charging
				if (npc.ai[0]>1999 && npc.ai[0] < 3000)
				{

					if (npc.ai[0] > 2998)
					{
						npc.ai[0] = 0;
						return;
					}
					if (npc.ai[0] % 210 < 90 && npc.ai[0] % 210 > 25)
					{
						npc.rotation = npc.rotation.AngleLerp((P.Center - npc.Center).ToRotation(), 0.15f);
						if (npc.ai[0] % 20 == 0 && Main.expertMode)
						{
							Idglib.Shattershots(npc.Center + npc.rotation.ToRotationVector2() * 32, npc.Center + npc.rotation.ToRotationVector2() * 200, new Vector2(0, 0), ProjectileID.WebSpit, 15, 20, 35, 1, true, 0, false, 1600);
							Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 102, 0.25f, -0.25f);
						}

					}
					if (npc.ai[0] % 210 > 100 && npc.ai[0] % 210 < 200)
					{
						if (npc.ai[0] %210 == 105)
						Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0, 1f, 0.25f);
						npc.velocity += npc.rotation.ToRotationVector2()*2f;

						if ((npc.ai[0] % (Main.expertMode ? 15 : 20)) == 0 && phase>1)
						{
							Idglib.Shattershots(npc.Center, npc.Center + (npc.rotation+MathHelper.ToRadians(90)).ToRotationVector2() * 64, new Vector2(0, 0), mod.ProjectileType("SpiderVenom"), 10, 7, 35, 1, true, 0, true, 1600);
							Idglib.Shattershots(npc.Center, npc.Center + (npc.rotation+MathHelper.ToRadians(-90)).ToRotationVector2() * 64, new Vector2(0, 0), mod.ProjectileType("SpiderVenom"), 10, 7, 35, 1, true, 0, true, 1600);
							Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 102, 0.25f, -0.25f);
						}

						if (npc.velocity.Length() > 96f) 
							{
							npc.velocity.Normalize();
							npc.velocity *= 96f;
							}



					}
					npc.localAI[0] += npc.velocity.Length() / 3f;
					npc.velocity /= 1.15f;

				}
			}

			//Spinning Trap Webs
			if (npc.ai[0] > 2999 && npc.ai[0] < 4000) {
				if (npc.ai[0]== 3005)
				Main.PlaySound(3, (int)npc.Center.X, (int)npc.Center.Y, 56, 0.25f, -0.25f);

				if (npc.ai[0] > 3100 && npc.ai[0]<3300)
				{

					float angle1; float angle2;
					GetAngleDifferenceBlushiMagic(new Vector2(npc.localAI[1], npc.localAI[2]), out angle1, out angle2);
					float rotSpeed = angle2 > angle1 ? 0.05f : -0.05f;
					rotSpeed *= 1f + ((float)(angle2 - angle1) * 0.2f);

						npc.rotation += rotSpeed;
						if (npc.ai[0] % 10 == 0)
						{
							int type = mod.ProjectileType("TrapWeb");
							Idglib.Shattershots(npc.Center + npc.rotation.ToRotationVector2() * 32, npc.Center + npc.rotation.ToRotationVector2() * 200, new Vector2(0, 0), type, 15, 7, 35, 1, true, 0, true, 1600);
							Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 102, 0.25f, -0.25f);
						}

						if (npc.ai[0] % 150 == 31)
						{
							npc.localAI[1] = P.Center.X;
							npc.localAI[2] = P.Center.Y;
						}


					}

				if (npc.ai[0] > 3350)
				{
					npc.ai[0] = Main.rand.Next(2400,2700);
					npc.netUpdate = true;
				}

				npc.velocity *= 0.96f;
			}


			//Wounded
			if (npc.ai[0] > 9999)
			{
			npc.velocity /= 1.25f;
			if (npc.ai[0]==10001)
			Main.PlaySound(3, (int)npc.Center.X, (int)npc.Center.Y, 51, 1f, 0.25f);
			npc.rotation += Main.rand.NextFloat(1f, -1f) * 0.08f;


				if (npc.ai[0] > 10100)
				{
				if (phase == 1)
					{
						npc.ai[0] = 2000;
					}

				}
				if (npc.ai[0] > 10100)
				{
					if (phase == 2)
					{
						npc.ai[0] = 3000;
					}

				}

			}

		}

		public Player P;

		public override void AI()
		{
			npc.direction=npc.velocity.X>0 ? -1 : 1;
			P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
				if (!P.active || P.dead)
				{
					float speed = ((-10f));
					npc.velocity = new Vector2(npc.velocity.X, npc.velocity.Y + speed);
					npc.active = false;
				}

			}
			else
			{
				npc.dontTakeDamage = false;
				bool sighttoplayer = (Collision.CanHitLine(new Vector2(npc.Center.X, npc.Center.Y), 6, 6, new Vector2(P.Center.X, P.Center.Y), 6, 6));
				bool underground = (int)((double)((npc.position.Y + (float)npc.height) * 2f / 16f) - Main.worldSurface * 2.0) > 0;
				if (!underground)
				{
					npc.dontTakeDamage = true;
				}
				npc.ai[0] += 1;

				Vector2 playerangledif = P.Center - npc.Center;
				float playerdist = playerangledif.Length();
				float maxspeed = 3f;
				if (Main.expertMode && !sighttoplayer)
				maxspeed += 3f;

				float maxrotate = 0.05f;
				playerangledif.Normalize();

				if (npc.ai[0] < 1201)//Standard Attacks
				{
					if (phase == 0)
					{
						if (npc.life < npc.lifeMax * 0.75)
						{
							DoPhase(1);
							return;
						}
					}

					npc.ai[0] %= 1200;
					if (npc.ai[0] % 1200 < 600)
					{

						if (npc.ai[0] == 10)
						{
							if (phase > 0)
							npc.ai[0] = Main.rand.Next(50, 400);
							npc.netUpdate = true;
						}

						npc.localAI[0] += 1f;
						npc.localAI[1] = P.Center.X;
						npc.localAI[2] = P.Center.Y;
						if ((npc.ai[0] + 26) % (Main.expertMode ? 60 : 90) == 0)
						{
							Idglib.Shattershots(npc.Center + npc.rotation.ToRotationVector2() * 32, npc.Center + npc.rotation.ToRotationVector2() * 200, new Vector2(0, 0), mod.ProjectileType("SpiderVenom"), 15, 8, 35 + phase * 5, phase + 1, true, 0, true, 1600);
							Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 102, 0.25f, -0.25f);
						}
						npc.rotation = npc.rotation.AngleLerp((P.Center - npc.Center).ToRotation(), maxrotate);
						npc.velocity += npc.rotation.ToRotationVector2() * 0.4f;
						npc.velocity += playerangledif * 0.075f;
						if (npc.velocity.Length() > maxspeed)
						{
							npc.velocity.Normalize(); npc.velocity *= maxspeed;
						}
					}
					else
					{
						float angle1; float angle2;
						GetAngleDifferenceBlushiMagic(new Vector2(npc.localAI[1], npc.localAI[2]), out angle1, out angle2);
						float rotSpeed = angle2 > angle1 ? 0.05f : -0.05f;
						rotSpeed *= 1f + ((float)(angle2 - angle1) * 0.2f);

						if (npc.ai[0] % 150 < 60)
						{
							npc.rotation += rotSpeed;
							if (npc.ai[0] % (Main.expertMode ? 5 : 10) == 0)
							{
								int type = mod.ProjectileType("SpiderVenom");
								Idglib.Shattershots(npc.Center + npc.rotation.ToRotationVector2() * 32, npc.Center + npc.rotation.ToRotationVector2() * 200, new Vector2(0, 0), type, 15, 7, 35+(phase * 15), 1+phase, true, 0, true, 1600);
								Main.PlaySound(SoundID.Item, (int)npc.Center.X, (int)npc.Center.Y, 102, 0.25f, -0.25f);
							}

							if (npc.ai[0] % 150 == 61)
							{
								npc.localAI[1] = P.Center.X;
								npc.localAI[2] = P.Center.Y;
							}


						}


						npc.velocity *= 0.96f;

					}
					if (npc.ai[0] == 1195 && phase > 0)
					{
						npc.ai[0] = 2100;
					}

				}//Standard Attacks Over

				DoPhase(phase);


				if (sighttoplayer)
				{
					if (npc.ai[2] > 1500)
					{
						npc.ai[2] = 0;
						npc.ai[0] = 3000;
						npc.netUpdate = true;
					}

					if (phase > 1 && npc.ai[0] < 3000)
						npc.ai[2] += 1;

				}








				//SgaLib.Shattershots(npc.Center,P.position,new Vector2(P.width,P.height),118,npc.lifeMax>2000 ? 25 : 40,(float)Main.rand.Next(60,80)/6,35,2,true,0,true,100);
			}


		}

    		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			Texture2D texBody = ModContent.GetTexture("SGAmod/NPCs/SpiderQueen/SpiderQueen");
			Texture2D texLegs = ModContent.GetTexture("SGAmod/NPCs/SpiderQueen/SpiderQueenLegs");
			SpriteEffects effects = SpriteEffects.FlipHorizontally;
			Vector2 drawOriginBody = new Vector2(texBody.Width, texBody.Height / 2);
			Vector2 drawOriginLegs = new Vector2(texLegs.Width, texLegs.Height / 2) / 3f;
			Vector2 drawPos = ((npc.Center - Main.screenPosition)) + ((npc.rotation+MathHelper.ToRadians(90)).ToRotationVector2() * 8f)+npc.rotation.ToRotationVector2() * -16f;
			Vector2 drawPosHead = ((npc.Center - Main.screenPosition)) + npc.rotation.ToRotationVector2()*38f;
			Color color = lightColor;//npc.GetAlpha(lightColor) * 1f;
			int timing = (int)(npc.localAI[0] / 10f);
			timing %= 3;
			timing *= ((texLegs.Height) / 3);



			spriteBatch.Draw(texLegs, drawPos, new Rectangle(0, timing, texLegs.Width, (texLegs.Height - 1) / 3), color, npc.rotation, drawOriginLegs, npc.scale,SpriteEffects.None, 0f);
			spriteBatch.Draw(texBody, drawPosHead, null, color, npc.rotation, drawOriginBody, npc.scale, SpriteEffects.None, 0f);

			return false;
		}



	}

	public class SpiderVenom : ModProjectile
	{
		private Vector2[] oldPos = new Vector2[6];
		private float[] oldRot = new float[6];
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spider Venom");
		}

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			projectile.width = 12;
			projectile.height = 12;
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.magic = true;
			projectile.timeLeft = 1200;
			projectile.penetrate = 1;
			projectile.extraUpdates = 5;
			aiType = ProjectileID.Bullet;
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.type = ProjectileID.CursedFlameFriendly;

			for(int i = 0; i < 20; i++) {
				Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
				randomcircle *= Main.rand.NextFloat(0f, 2f);
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AcidDust"));
				Main.dust[dust].scale = 1f;
				Main.dust[dust].noGravity = false;
				Main.dust[dust].velocity = projectile.velocity * (float)(Main.rand.Next(60, 100) * 0.01f);
				Main.dust[dust].velocity += new Vector2(randomcircle.X, randomcircle.Y);
			}
			Main.PlaySound(SoundID.Item, (int)projectile.Center.X, (int)projectile.Center.Y, 111, 0.33f, 0.25f);

			if (projectile.hostile)
			{
				for (int pro = 0; pro < Main.maxPlayers; pro += 1)
				{
					Player ply = Main.player[pro];
					if (ply.active && (ply.Center - projectile.Center).Length() < 48)
					{
						ply.AddBuff(mod.BuffType("AcidBurn"), 45);
					}
				}
			}

			if (projectile.friendly)
			{
				for (int pro = 0; pro < Main.maxNPCs; pro += 1)
				{
					NPC ply = Main.npc[pro];
					if (ply.active && !ply.friendly && (ply.Center - projectile.Center).Length() < 48)
					{
						ply.AddBuff(mod.BuffType("AcidBurn"), 45);
					}
				}
			}

			return true;
		}

		public override string Texture
		{
			get { return ("Terraria/Projectile_" + 14); }
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			Texture2D tex = ModContent.GetTexture("SGAmod/NPCs/SpiderQueen/SpiderVenom");
			Vector2 drawOrigin = new Vector2(tex.Width, tex.Height) / 2f;

			//oldPos.Length - 1
			for (int k = oldPos.Length - 1; k >= 0; k -= 1)
			{
				Vector2 drawPos = ((oldPos[k] - Main.screenPosition)) + new Vector2(0f, 0f);
				Color color = Color.Lerp(Color.White, lightColor, (float)k / (oldPos.Length + 1));
				float alphaz = (1f - (float)(k + 1) / (float)(oldPos.Length + 2)) * 0.25f;
				spriteBatch.Draw(tex, drawPos, null, color * alphaz, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		public override void AI()
		{

			if (Main.rand.Next(0, 3) == 1)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("AcidDust"));
				Main.dust[dust].scale = 0.75f;
				Main.dust[dust].noGravity = false;
				Main.dust[dust].velocity = projectile.velocity * (float)(Main.rand.Next(60, 100) * 0.01f);
			}

			projectile.position -= projectile.velocity * 0.8f;

			for (int k = oldPos.Length - 1; k > 0; k--)
			{
				oldPos[k] = oldPos[k - 1];
			}
			oldPos[0] = projectile.Center;

			projectile.rotation = projectile.velocity.ToRotation();
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(0, 3) < 2)
				target.AddBuff(mod.BuffType("AcidBurn"), 60 * (Main.rand.Next(0, 3) == 1 ? 2 : 1));
			projectile.Kill();
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(0, 3) < 2)
				target.AddBuff(mod.BuffType("AcidBurn"), 60 * (Main.rand.Next(0, 3) == 1 ? 2 : 1));
			projectile.Kill();
		}



	}


}

