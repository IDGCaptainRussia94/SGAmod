using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
using Idglibrary;

namespace SGAmod.Items.Weapons
{
	public class TheJacob : ModItem
	{
		bool altfired=false;
		bool forcedreload=false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Jakob");
			Tooltip.SetDefault("Right click to fan the hammer-rapidly fire the remaining clip with less accuracy\n'If it took more than 1 shot, you wern't using a Jakobs!'");
			SGAmod.UsesClips.Add(SGAmod.Instance.ItemType("TheJacob"), 6);
		}
		
		public override void SetDefaults()
		{
            item.CloneDefaults(ItemID.Revolver);
			item.damage = 200;
			item.width = 48;
            item.height = 48;
			item.useTime = 40;
			item.useAnimation = 40;
			item.knockBack = 10;
			item.value = 10000;
			item.rare = 5;
			item.crit = 15;
			item.shootSpeed = 8f;
            item.noMelee = true;
            item.useAmmo = AmmoID.Bullet;
            item.autoReuse = false;
            item.shoot = 10;
			item.shootSpeed = 50f;
			item.noUseGraphic = false;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

        SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
        altfired=player.altFunctionUse == 2 ? true : false;
        forcedreload=false;
        item.noUseGraphic = false;

        if (altfired && sgaplayer.ammoLeftInClip>0){
		item.useAnimation = 2000;
		item.useTime = 10;
		item.UseSound = SoundID.Item38;
        }else{
		item.useTime = 40;
		item.useAnimation = 40;
		item.UseSound = SoundID.Item38;
		if (sgaplayer.ammoLeftInClip<1){item.UseSound = SoundID.Item98; forcedreload=true; item.useTime = 4; item.useAnimation = 4; item.noUseGraphic = true;}
		}
        return true;
    }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-3, 2);
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
        //base.Shoot(player,ref position,ref speedX,ref speedY,ref type,ref damage,ref knockBack);
        SGAPlayer sgaplayer = player.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
        sgaplayer.ammoLeftInClip-=1;
		if (item.useAnimation>1000){
		Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
		speedX = perturbedSpeed.X;
		speedY = perturbedSpeed.Y;
		Main.PlaySound(SoundID.Item38,player.Center);
		}
		if (sgaplayer.ammoLeftInClip==0 || forcedreload){
        player.itemTime = 40;
		player.itemAnimation = 40;
        int thisone=Projectile.NewProjectile(player.Center.X, player.Center.Y,0f,0f, mod.ProjectileType("TheJacobReloading"), 0, knockBack, Main.myPlayer, 0.0f, 0f);
       // Main.projectile[thisone].spriteDirection=normalizedspeed.X>0f ? 1 : -1;
   		//Main.projectile[thisone].rotation=(new Vector2(speedX,speedY)).ToRotation();
        return !forcedreload;
		}
		return (sgaplayer.ammoLeftInClip>0);
		}




		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("FiberglassRifle"), 1);
			recipe.AddIngredient(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.SoulofSight, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("FiberglassRifle"), 1);
			recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(ItemID.SoulofSight, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
            recipe.AddRecipe();
		}
	
	}

	public class TheJacobReloading : ClipWeaponReloading
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("You should not see this");
		}

		public override string Texture
		{
			get { return("SGAmod/Items/Weapons/TheJacob");}
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