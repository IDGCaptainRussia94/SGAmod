using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.Items.Weapons
{
	public class DragonRevolver : ModItem
	{
		bool altfired=false;
		bool forcedreload=false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Serpent's Redemption");
			Tooltip.SetDefault("Hold Left Click and hover your mouse over targets to mark them for execution: releasing a dragon-fire burst on them!\nYou may mark targets as long as you have ammo in the clip and nothing is blocking your way\nUp to 6 targets may be marked for execution; a target that resists however can be marked more than once\nThe explosion is unable to crit but hits several times\nRight Click to fire 3 accurate rounds at once if the bullet does not pierce, otherwise 1\n'Thy time has come'ith for dragon slayers, repent!'");
			SGAmod.UsesClips.Add(SGAmod.Instance.ItemType("DragonRevolver"), 6);
		}
		
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Color c = Main.hslToRgb((float)(Main.GlobalTime/4)%1f, 0.4f, 0.45f);
            tooltips.Add(new TooltipLine(mod,"IDG Dev Item", Idglib.ColorText(c,"IDGCaptainRussia94's dev weapon")));
        }

		public override void SetDefaults()
		{
            item.CloneDefaults(ItemID.Revolver);
			item.damage = 2500;
			item.width = 48;
            item.height = 48;
			item.useTime = 40;
			item.useAnimation = 40;
			item.knockBack = 10;
			item.value = Item.sellPrice(1,0,0,0);
			item.rare = 12;
			item.crit = 15;
	        item.shootSpeed = 8f;
            item.noMelee = true;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = false;
            item.shoot = 10;
			item.crit = 15;
			item.shootSpeed = 50f;
			item.noUseGraphic = false;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Wide_Beam_Shot");
			item.useStyle = 5;
			item.expert = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

        if (player.ownedProjectileCounts[mod.ProjectileType("DragonRevolverAiming")]+player.ownedProjectileCounts[mod.ProjectileType("RevolverTarget")]>0){
        return false;
    	}

        SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
        altfired=player.altFunctionUse == 2 ? false : true;
        forcedreload=false;
        item.noUseGraphic = false;

        if (altfired && sgaplayer.ammoLeftInClip>0){
		item.useAnimation = 5;
		item.useTime = 5;
		item.useStyle = 5;
		item.UseSound = SoundID.Item35;
		item.channel = true;
		item.shoot = mod.ProjectileType("DragonRevolverAiming");
        }else{
        item.useStyle = 5;
		item.useTime = 50;
		item.useAnimation = 50;
		item.UseSound = SoundID.Item38;
		item.channel = false;
		item.shoot = 10;
		if (sgaplayer.ammoLeftInClip<1){item.UseSound = SoundID.Item98; forcedreload=true; item.useTime = 4; item.useAnimation = 4; item.noUseGraphic = true;}
		}
        return true;
    }

   		public override bool ConsumeAmmo(Player player)
   		{
   		return (item.shoot != mod.ProjectileType("DragonRevolverAiming"));
   		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-3, 2);
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
        //base.Shoot(player,ref position,ref speedX,ref speedY,ref type,ref damage,ref knockBack);
        SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;

			if (!altfired && sgaplayer.ammoLeftInClip > 0)
			{
				sgaplayer.ammoLeftInClip -= 1;
					Projectile proj = new Projectile();
					proj.SetDefaults(type);

					if (proj.penetrate < 5 && proj.penetrate > -1)
					{

						for (int i = 0; i < 2; i += 1)
						{
							int thisoned = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer);
						}
					}
			}

		if (sgaplayer.ammoLeftInClip==0 || forcedreload){
        player.itemTime = 50;
		player.itemAnimation = 50;
		if (forcedreload){
        player.itemTime = 1;
		player.itemAnimation = 1;
		}
        int thisone=Projectile.NewProjectile(player.Center.X, player.Center.Y,0f,0f, mod.ProjectileType("DragonRevolverReloading"), 0, knockBack, Main.myPlayer, 0.0f, 0f);
				// Main.projectile[thisone].spriteDirection=normalizedspeed.X>0f ? 1 : -1;
				//Main.projectile[thisone].rotation=(new Vector2(speedX,speedY)).ToRotation();

				return !forcedreload;
		}

		if (altfired){
		int thisone=Projectile.NewProjectile(player.Center.X, player.Center.Y,0f,0f, mod.ProjectileType("DragonRevolverAiming"), 1, 0f, Main.myPlayer, 0.0f, 0f);
		return false;
		}

			//if (sgaplayer.ammoLeftInClip > 0)
			//{
			//}
			return (sgaplayer.ammoLeftInClip>0);
		}




		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarRoyalGel"), 30);
            recipe.AddIngredient(mod.ItemType("StarMetalBar"), 20);
            recipe.AddIngredient(mod.ItemType("TheJacob"), 1);
            recipe.AddIngredient(mod.ItemType("CosmicFragment"), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
            recipe.AddRecipe();
		}
	
	}

	public class DragonRevolverAiming : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("You should not see this");
		}

		public override string Texture
		{
			get { return("SGAmod/Items/Weapons/DragonRevolver");}
		}

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			projectile.width = 8;
			projectile.height = 8;
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.hostile=false;
			projectile.friendly=true;
			projectile.tileCollide = false;
			projectile.timeLeft=180;
			projectile.penetrate=-1;
			projectile.scale=1f;
			projectile.damage=1;
			aiType = 0;
		}

		public override bool? CanCutTiles(){ return false; }

		public override bool? CanHitNPC(NPC target)
		{
		Player player = Main.player[projectile.owner];
		SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
		int ownedproj=player.ownedProjectileCounts[mod.ProjectileType("RevolverTarget")];
		if (!target.HasBuff(mod.BuffType("Targeted")) && !target.friendly && sgaplayer.ammoLeftInClip>0 && ownedproj<6 && projectile.ai[0]<1 && (Collision.CanHitLine(new Vector2(target.Center.X, target.Center.Y), 1, 1, new Vector2(player.Center.X, player.Center.Y), 1, 1))){
		return true;
		}
		return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		Player player = Main.player[projectile.owner];
		SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
		int ownedproj=player.ownedProjectileCounts[mod.ProjectileType("RevolverTarget")];
			
		IdgNPC.AddBuffBypass(target.whoAmI,mod.BuffType("Targeted"),3,false);
		int thisone=Projectile.NewProjectile(player.Center.X, player.Center.Y,0f,0f, mod.ProjectileType("RevolverTarget"), 0, 0f, projectile.owner, 0.0f, 0f);
		Main.projectile[thisone].ai[0]=target.whoAmI;
		Main.projectile[thisone].netUpdate=true;
		sgaplayer.ammoLeftInClip-=1;
		//Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Wide_Beam_Shot"),(int)Main.player[projectile.owner].position.X,(int)Main.player[projectile.owner].position.Y,1,1.15f,((float)ownedproj)/4f);
		//Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Wide_Beam_Shot").WithVolume(1.1f).WithPitchVariance(.25f));
		Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[projectile.owner].position.X,(int)Main.player[projectile.owner].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Wide_Beam_Shot"),1.15f,((float)-0.4+(ownedproj)/6f));
		}

		public override void AI()
		{
			Vector2 mousePos = Main.MouseWorld;
			Player player = Main.player[projectile.owner];

			if (projectile.ai[0]>1000f || player.dead)
			{
				projectile.Kill();
			}
			if (!player.channel || projectile.ai[0]>0){
			projectile.ai[0]+=1;
			if (projectile.ai[0]<5f)
			projectile.ai[0]=79f;
			}
			// Multiplayer support here, only run this code if the client running it is the owner of the projectile
			if (projectile.owner == Main.myPlayer)
			{
				Vector2 diff = mousePos - player.Center;
				diff.Normalize();
				projectile.velocity = diff;
				if (projectile.ai[0]<50f)
				projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
				projectile.netUpdate = true;
				projectile.Center = mousePos;
			}
			int dir = projectile.direction;
			player.ChangeDir(dir);
			player.itemTime = 5;
			player.itemAnimation = 5;
			if (projectile.ai[0]<50f)
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);



		for (int num475 = 0; num475 < 3; num475++)
		{
		int dust = Dust.NewDust(projectile.position, 16, 16, 20);
		Main.dust[dust].scale=0.5f+(((float)num475)/3.5f);
		Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
		Main.dust[dust].velocity=(randomcircle/2f)+(player.itemRotation.ToRotationVector2());
		Main.dust[dust].noGravity=true;
		}

			projectile.timeLeft=2;


		if (projectile.ai[0]>65){projectile.ai[0]=58;
			int ownedproj=player.ownedProjectileCounts[mod.ProjectileType("RevolverTarget")];
			Projectile thetarget;
			thetarget=null;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile him=Main.projectile[i];
				if (him.type==mod.ProjectileType("RevolverTarget")){
				if (him.active && him.owner==projectile.owner){
				thetarget=him;
				break;
				}}}
				if (thetarget!=null){
				Vector2 angle=thetarget.Center-Main.player[projectile.owner].Center;
				projectile.direction = angle.X > player.position.X ? 1 : -1;
				player.itemRotation = (float)Math.Atan2(angle.Y * dir, angle.X * dir);
				angle.Normalize();
				int proj=Projectile.NewProjectile(thetarget.Center.X, thetarget.Center.Y, 0f,0f, mod.ProjectileType("SlimeBlast"), (int)(3000 * (player.rangedDamage)), 15f, projectile.owner, 0f, 0f);
				Main.projectile[proj].direction=projectile.direction;
				Main.projectile[proj].ranged = true;
				Main.PlaySound(SoundID.Item45,thetarget.Center);
				Main.PlaySound(SoundID.Item41,player.Center);
				thetarget.Kill();
				}else{
				Main.PlaySound(SoundID.Item63,player.Center);
				player.itemTime = 80;
				player.itemAnimation = 80;
				projectile.Kill();
				}
			}



		}


		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
		return true;
		}

	}


	public class RevolverTarget : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("You should not see this");
		}

		public override string Texture
		{
			get { return("SGAmod/Items/Weapons/FieryMoon");}
		}

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			projectile.width = 32;
			projectile.height = 32;
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.hostile=false;
			projectile.friendly=true;
			projectile.tileCollide = false;
			projectile.timeLeft=100;
			projectile.penetrate=-1;
			projectile.scale=1f;
			aiType = 0;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			NPC target = Main.npc[(int)projectile.ai[0]];

			if (!target.active || player.dead)
			{
				projectile.Kill();
			}
			projectile.Center = target.Center;
			IdgNPC.AddBuffBypass(target.whoAmI,mod.BuffType("Targeted"),3,false);
			projectile.timeLeft=3;

		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
		Vector2 drawPos = projectile.Center - Main.screenPosition;
		Color glowingcolors1 = Color.Red;//Main.hslToRgb((float)lightColor.R*0.08f,(float)lightColor.G*0.08f,(float)lightColor.B*0.08f);
		spriteBatch.Draw(Main.blackTileTexture, drawPos, new Rectangle(0,0,120,10), glowingcolors1, projectile.rotation, new Vector2(60,5),new Vector2(1,1), SpriteEffects.None, 0f);
		spriteBatch.Draw(Main.blackTileTexture, drawPos, new Rectangle(0,0,10,120), glowingcolors1, projectile.rotation, new Vector2(5,60),new Vector2(1,1), SpriteEffects.None, 0f);
		return false;
		}

	}






	public class DragonRevolverReloading : ClipWeaponReloading
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("You should not see this");
		}

		public override string Texture
		{
			get { return("SGAmod/Items/Weapons/DragonRevolver");}
		}

		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
			projectile.width = 30;
			projectile.height = 24;
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.hostile=false;
			projectile.friendly=true;
			projectile.tileCollide = false;
			projectile.timeLeft=180;
			projectile.penetrate=10;
			projectile.scale=0.7f;
			aiType = 0;
			drawOriginOffsetX = 8;
			drawOriginOffsetY = 8;
			drawHeldProjInFrontOfHeldItemAndArms=true;
		}

	}

}