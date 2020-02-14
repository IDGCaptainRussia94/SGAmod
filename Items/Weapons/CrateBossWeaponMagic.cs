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
	public class CrateBossWeaponMagic : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Philanthropist's Shower");
			Tooltip.SetDefault("Consumes Coins from the player's inventory\nDamage increases with higher value coins; more valable coins are used first\nInflicts Midas on enemies\n'and so, I shall make it rain!'");
		}
		
		public override void SetDefaults()
		{
			item.damage = 32;
			item.magic = true;
			item.width = 34;    
			item.mana = 10;
            item.height = 24;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.knockBack = 6;
			item.value = 500000;
			item.rare = 7;
	        item.shootSpeed = 8f;
            item.noMelee = true; 
			item.shoot = 14;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
		    item.useTurn = false;
            Item.staff[item.type] = true; 
		}

        public override bool CanUseItem(Player player)
        {
        return (player.CountItem(ItemID.CopperCoin)+player.CountItem(ItemID.SilverCoin)+player.CountItem(ItemID.GoldCoin)+player.CountItem(ItemID.PlatinumCoin)>0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

int taketype=3;
int [] types = {ItemID.CopperCoin,ItemID.SilverCoin,ItemID.GoldCoin,ItemID.PlatinumCoin};
int silver=player.CountItem(ItemID.SilverCoin);
int gold=player.CountItem(ItemID.GoldCoin);
int plat=player.CountItem(ItemID.PlatinumCoin);
taketype = plat>0 ? 3 : (gold>0 ? 2 : (silver>0 ? 1 : 0));
int coincount=player.CountItem(types[taketype]);
if (player.CountItem(types[taketype])>0){
player.ConsumeItem(types[taketype]);
float [,] typesproj = {{(float)ProjectileID.CopperCoin,1f},{(float)ProjectileID.SilverCoin,1.5f},{(float)ProjectileID.GoldCoin,2.25f},{(float)ProjectileID.PlatinumCoin,5f}};

		    int numberProjectiles = 8 + Main.rand.Next(7);  
            for (int index = 0; index < numberProjectiles; index=index+1)
            {
                Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                vector2_1.Y -= (float)(100 * (index/4));
                float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float morespeed=0.75f+((float)taketype*0.2f);
                float SpeedX = (num16*morespeed) + (float)Main.rand.Next(-40, 41) * 0.02f;  
                float SpeedY = (num17*morespeed) + (float)Main.rand.Next(-40, 41) * 0.02f;
                int thisone=Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, (int)typesproj[taketype,0], (int)(typesproj[taketype,1]*(float)damage), knockBack, Main.myPlayer, 0.0f, 0f);
                Main.projectile[thisone].magic=true;
                Main.projectile[thisone].ranged=false;
                IdgProjectile.AddOnHitBuff(thisone,BuffID.Midas,60*10);
            }



        }
           return false;
		}
				
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 15);
            }
        }
	
	}
}