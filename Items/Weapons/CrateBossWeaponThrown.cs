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
	class CrateBossWeaponThrown : ModItem
	{

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Avarice");
			Tooltip.SetDefault("Throw coins that influx on one enemy\nInflicts Midas and Shadowflame on enemies\n'Greed is it's own corruption'");
		}

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.thrown=true;
			item.damage=75;
			item.shootSpeed = 3f;
			item.shoot = mod.ProjectileType("AvariceCoin");
			item.useTurn = true;
			//ProjectileID.CultistBossLightningOrbArc
			item.width = 8;
			item.height = 28;
			item.maxStack = 1;
			item.knockBack = 9;
			item.consumable = false;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 10;
			item.useTime = 10;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.value = Item.buyPrice(0, 15, 0, 0);
			item.rare = 7;
		}


        public override bool CanUseItem(Player player)
        {
        return true;
        }

	}

	public class AvariceCoin : ModProjectile
	{

		int fakeid=ProjectileID.GoldCoin;
		public int guyihit=-10;
		public int cooldown=-10;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Avarice Coin");
		}

		public override bool? CanHitNPC(NPC target)
		{
		if (guyihit<0){
		return null;
		}else{
		if (guyihit!=target.whoAmI && cooldown>0)
		return false;
		if (cooldown>0)
		return false;
		}
		return null;
	}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		if (guyihit<1)
		guyihit=target.whoAmI;
		cooldown=15;
		projectile.tileCollide=false;
		target.immune[projectile.owner] = 2;
		target.AddBuff(BuffID.ShadowFlame, 60*10);
        target.AddBuff(BuffID.Midas, 60*10);
		}


		public override string Texture
		{
			get { return "Terraria/Projectile_" + fakeid; }
		}

		public override void SetDefaults()
		{
		projectile.aiStyle=18;
		projectile.thrown=true;
		projectile.timeLeft=300;
		projectile.penetrate=3;
		projectile.tileCollide=true;
		projectile.friendly=true;
		projectile.hostile=false;
		guyihit=-1;
		cooldown=-1;
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.type=fakeid;
			return true;
		}

		public override void AI()
		{
			cooldown-=1;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (guyihit>-1){
			if (cooldown==1){
			Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
			projectile.Center=Main.npc[guyihit].Center+(randomcircle*256f);
			projectile.velocity=-randomcircle*8f;
			}
			if (Main.npc[guyihit].active==false || Main.npc[guyihit].life<1){
			guyihit=-10;
			}

			}
		}

	}

}
