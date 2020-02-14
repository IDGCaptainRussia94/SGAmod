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
	public class IceScepter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Scepter");
			Tooltip.SetDefault("Launches homing ice bolts");
		}
		
		public override void SetDefaults()
		{
			item.damage = 35;
			item.magic = true;
			item.width = 34;    
			item.mana = 8;
            item.height = 24;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 5;
			item.knockBack = 2;
			item.value = 10000;
			item.rare = 5;
	        item.shootSpeed = 8f;
            item.noMelee = true; 
			item.shoot = 14;
            item.shootSpeed = 7f;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
            Item.staff[item.type] = true; 
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            float rotation = MathHelper.ToRadians(4);
            speedX*=4f;
            speedY*=4f;
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 48f;
            Vector2 perturbedSpeed = (new Vector2(speedX, speedY)).RotatedBy(MathHelper.Lerp(-rotation, rotation, (float)Main.rand.Next(0,100)/100f)) * .2f;

                int proj=Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CirnoBoltPlayer"), damage, knockBack, player.whoAmI);
                Main.projectile[proj].friendly=true;
                Main.projectile[proj].hostile=false;
                Main.projectile[proj].timeLeft=240;
                Main.projectile[proj].knockBack=item.knockBack;

           return false;
		}
	
	}

}

namespace SGAmod.NPCs
{

    public class CirnoBoltPlayer : CirnoBolt
    {

        double keepspeed=0.0;
        float homing=0.05f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cirno's Grace");
        }

        public override string Texture
        {
            get { return "Terraria/Projectile_" + ProjectileID.IceBolt; }
        }

    }

}