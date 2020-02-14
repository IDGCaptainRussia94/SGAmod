﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.HavocGear.Projectiles
{
    public class HeatTentacle : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.MaxUpdates = 3;
            projectile.magic = true;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 20);
		}

		public override void AI()
        {
        	if (projectile.velocity.X != projectile.velocity.X)
			{
				if (Math.Abs(projectile.velocity.X) < 1f)
				{
					projectile.velocity.X = -projectile.velocity.X;
				}
				else
				{
					projectile.Kill();
				}
			}
			if (projectile.velocity.Y != projectile.velocity.Y)
			{
				if (Math.Abs(projectile.velocity.Y) < 1f)
				{
					projectile.velocity.Y = -projectile.velocity.Y;
				}
				else
				{
					projectile.Kill();
				}
			}
        	Vector2 center10 = projectile.Center;
			projectile.scale = 1f - projectile.localAI[0];
			projectile.width = (int)(20f * projectile.scale);
			projectile.height = projectile.width;
			projectile.position.X = center10.X - (float)(projectile.width / 2);
			projectile.position.Y = center10.Y - (float)(projectile.height / 2);
			if ((double)projectile.localAI[0] < 0.1)
			{
				projectile.localAI[0] += 0.01f;
			}
			else
			{
				projectile.localAI[0] += 0.025f;
			}
			if (projectile.localAI[0] >= 0.95f)
			{
				projectile.Kill();
			}
			projectile.velocity.X = projectile.velocity.X + projectile.ai[0] * 1.5f;
			projectile.velocity.Y = projectile.velocity.Y + projectile.ai[1] * 1.5f;
			if (projectile.velocity.Length() > 16f)
			{
				projectile.velocity.Normalize();
				projectile.velocity *= 16f;
			}
			projectile.ai[0] *= 1.05f;
			projectile.ai[1] *= 1.05f;
			if (projectile.scale < 1f)
			{
				int num897 = 0;
				while ((float)num897 < projectile.scale * 10f)
				{
					int num898 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num898].position = (Main.dust[num898].position + projectile.Center) / 2f;
					Main.dust[num898].noGravity = true;
					Main.dust[num898].velocity *= 0.1f;
					Main.dust[num898].velocity -= projectile.velocity * (1.3f - projectile.scale);
					Main.dust[num898].fadeIn = (float)(100 + projectile.owner);
					Main.dust[num898].scale += projectile.scale * 0.75f;
					num897++;
				}
				return;
			}
        }
    }
}