using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using SGAmod.NPCs;


namespace SGAmod.Items.Weapons
{
	public class Snowfall : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snowfall");
			Tooltip.SetDefault("Summon a cloud to rain hardened snowballs on your foes\nLimits 2 clouds at a time");
		}
		
		public override void SetDefaults()
		{
			item.summon = true;
			item.damage = 40;
			item.mana = 50;
			item.width = 44;
			item.height = 52;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = 10000;
			item.noMelee = true;
			item.rare = 5;
			item.shoot = 10;
			item.shootSpeed = 10f;
	        item.UseSound = SoundID.Item60;
	     	item.autoReuse = true;
			item.useTurn = false;
		    
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			/*int projcount=player.ownedProjectileCounts[mod.ProjectileType("SnowfallCloud")]+player.ownedProjectileCounts[mod.ProjectileType("SnowCloud")];

			if (projcount>1){
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile him=Main.projectile[i];
				if (him.type==mod.ProjectileType("SnowfallCloud") || him.type==mod.ProjectileType("SnowCloud")){
				if (him.active && him.owner==projectile.owner){
				him.Kill();
				break;
			}}}}*/

		SGAPlayer.LimitProjectiles(player,1,new ushort[] {(ushort)mod.ProjectileType("SnowfallCloud"),(ushort)mod.ProjectileType("SnowCloud")});

		int theproj=Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SnowfallCloud"), damage, knockBack, player.whoAmI);
		float num12 = (float)Main.mouseX + Main.screenPosition.X;
        float num13 = (float)Main.mouseY + Main.screenPosition.Y;
		HalfVector2 half=new HalfVector2(num12,num13);
		Main.projectile[theproj].ai[0]=ReLogic.Utilities.ReinterpretCast.UIntAsFloat(half.PackedValue);
		Main.projectile[theproj].netUpdate=true;
		return false;
		}


	public override void MeleeEffects(Player player, Rectangle hitbox)
	{

		for (int num475 = 0; num475 < 3; num475++)
		{
		int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("HotDust"));
		Main.dust[dust].scale=0.5f+(((float)num475)/3.5f);
		Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
		Main.dust[dust].velocity=randomcircle/2f;
		Main.dust[dust].noGravity=true;
		//Main.dust[dust].velocity.Normalize();
		//Main.dust[dust].velocity+=new Vector2(player.velocity.X/4,0f);
		//Main.dust[dust].velocity*=(((float)Main.rand.Next(0,100))/30f);
		}
		Lighting.AddLight(player.position, 0.9f, 0.9f, 0f);
	}


}
	

	public class SnowfallCloud : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			//projectile.magic = true;
			//projectile.penetrate = 1;
			projectile.timeLeft = 60*30;
			projectile.tileCollide=false;
		}

				public override string Texture
		{
			get { return "Terraria/Item_" + 5; }
		}

	public override bool? CanHitNPC(NPC target){
		return false;
	}
	public override bool CanHitPlayer(Player target){
		return false;
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
		return false;
	}
	public override void AI()
	{

		Vector2 gohere=new HalfVector2() { PackedValue = ReLogic.Utilities.ReinterpretCast.FloatAsUInt(projectile.ai[0]) }.ToVector2();
		Vector2 anglevect=(gohere-projectile.position);
		float length=anglevect.Length();
		anglevect.Normalize();
		projectile.velocity=(10f*anglevect);
		bool reached=(length<projectile.velocity.Length()+1f);
		for (int q = 0; q < (reached ? 40 : 4); q++)
		{
		Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
		float reachfloat=reached ? 0f : 1f;
		int dust = Dust.NewDust(projectile.position-new Vector2(8,0), 16, 16, DustID.Smoke, ((projectile.velocity.X * 0.75f)*reachfloat)+(randomcircle*(reached ? 12f : 0f)).X, ((projectile.velocity.Y * 0.75f)*reachfloat)+(randomcircle*(reached ? 4f : 0f)).Y, 100, Main.hslToRgb(0.6f,0.8f, 0.8f), 3f);
		Main.dust[dust].noGravity = true;
		}
		if (reached){
		int theproj=Projectile.NewProjectile(projectile.position.X+16f, projectile.position.Y, 0f, 0f, mod.ProjectileType("SnowCloud"), projectile.damage, projectile.knockBack, projectile.owner);
		Main.projectile[theproj].friendly=true;
		Main.projectile[theproj].hostile=false;
		Main.projectile[theproj].timeLeft=projectile.timeLeft;
		Main.projectile[theproj].netUpdate=true;
		projectile.Kill();
		}
	}

}

	public class CursedHail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Hail");
			Tooltip.SetDefault("Summon a corrupted cloud to rain cursed ice shards on your foes\nLimits 2 clouds at a time");
		}

		public override void SetDefaults()
		{
			item.summon = true;
			item.damage = 65;
			item.mana = 50;
			item.width = 44;
			item.height = 52;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 10;
			item.value = 10000;
			item.noMelee = true;
			item.rare = 5;
			item.shoot = 10;
			item.shootSpeed = 10f;
			item.UseSound = SoundID.Item60;
			item.autoReuse = true;
			item.useTurn = false;

		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			SGAPlayer.LimitProjectiles(player, 1, new ushort[] { (ushort)mod.ProjectileType("CursedHailProj"), (ushort)mod.ProjectileType("CursedHailCloud") });

			int theproj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("CursedHailProj"), damage, knockBack, player.whoAmI);
			float num12 = (float)Main.mouseX + Main.screenPosition.X;
			float num13 = (float)Main.mouseY + Main.screenPosition.Y;
			HalfVector2 half = new HalfVector2(num12, num13);
			Main.projectile[theproj].ai[0] = ReLogic.Utilities.ReinterpretCast.UIntAsFloat(half.PackedValue);
			Main.projectile[theproj].netUpdate = true;
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Snowfall"), 1);
			recipe.AddIngredient(ItemID.CursedFlames, 25);
			recipe.AddIngredient(ItemID.BlizzardStaff, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

		}


		}

	public class CursedHailProj : SnowfallCloud
	{

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			//projectile.magic = true;
			//projectile.penetrate = 1;
			projectile.timeLeft = 60 * 30;
			projectile.tileCollide = false;
		}

		public override string Texture
		{
			get { return "Terraria/Item_" + 5; }
		}
		public override void AI()
		{

			Vector2 gohere = new HalfVector2() { PackedValue = ReLogic.Utilities.ReinterpretCast.FloatAsUInt(projectile.ai[0]) }.ToVector2();
			Vector2 anglevect = (gohere - projectile.position);
			float length = anglevect.Length();
			anglevect.Normalize();
			projectile.velocity = (10f * anglevect);
			bool reached = (length < projectile.velocity.Length() + 1f);
			for (int q = 0; q < (reached ? 40 : 4); q++)
			{
				Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
				float reachfloat = reached ? 0f : 1f;
				int dust = Dust.NewDust(projectile.position - new Vector2(8, 0), 16, 16, DustID.Smoke, ((projectile.velocity.X * 0.75f) * reachfloat) + (randomcircle * (reached ? 12f : 0f)).X, ((projectile.velocity.Y * 0.75f) * reachfloat) + (randomcircle * (reached ? 4f : 0f)).Y, 100, Color.LimeGreen, 3f);
				Main.dust[dust].noGravity = true;
			}
			if (reached)
			{
				int theproj = Projectile.NewProjectile(projectile.position.X + 16f, projectile.position.Y, 0f, 0f, mod.ProjectileType("CursedHailCloud"), projectile.damage, projectile.knockBack, projectile.owner);
				Main.projectile[theproj].friendly = true;
				Main.projectile[theproj].hostile = false;
				Main.projectile[theproj].timeLeft = projectile.timeLeft;
				Main.projectile[theproj].netUpdate = true;
				projectile.Kill();

			}

		}

	}


	public class CursedHailCloud : SnowCloud
	{

		public override int projectileid => ModContent.ProjectileType<CursedHailProjectile>();
		public override Color colorcloud => Color.LimeGreen;
		public override int rate => 4;

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.aiStyle = 1;
			projectile.friendly = false;
			projectile.hostile = true;
			//projectile.magic = true;
			//projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.tileCollide = false;
		}


	}


	public class CursedHailProjectile : ModProjectile
	{

		int fakeid = ProjectileID.FrostShard;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Hail");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.CloneDefaults(fakeid);
			projectile.width = 8;
			projectile.height = 8;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.penetrate = 5;
			projectile.magic = true;
			projectile.extraUpdates = 2;
		}

		public override string Texture
		{
			get { return "Terraria/Projectile_" + fakeid; }
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.localAI[0] == 0)
			projectile.localAI[0] = Main.rand.Next(0, 3);
		Texture2D tex = ModContent.GetTexture("Terraria/Projectile_" + fakeid);
			Vector2 drawOrigin = new Vector2(tex.Width, tex.Height / 5) / 2f;
			Vector2 drawPos = ((projectile.Center - Main.screenPosition)) + new Vector2(0f, 4f);
			int timing = (int)(projectile.localAI[0]-1);
			timing %= 5;
			timing *= ((tex.Height) / 5);
			spriteBatch.Draw(tex, drawPos, new Rectangle(0, timing, tex.Width, (tex.Height - 1) / 5), lightColor, MathHelper.ToRadians(180)+projectile.velocity.X * -0.08f, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.type = fakeid;
			return true;
		}

		public override void AI()
		{
			if (projectile.ai[0] == 0)
			{
				projectile.ai[0] = 1;
				projectile.velocity += new Vector2(0, 10);
				projectile.velocity *= 0.75f;
			}
			int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75);
			Main.dust[dust].scale = 1f;
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity = projectile.velocity * (float)(Main.rand.NextFloat(0.1f,0.25f));
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.CursedInferno, 60 * 5);
			target.immune[projectile.owner] = 4;
		}

	}


}