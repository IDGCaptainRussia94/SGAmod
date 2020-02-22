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
			Tooltip.SetDefault("Launches homing ice bolts\nAlt Fire summons a wall of ice blocks");
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
			item.value = 50000;
			item.rare = 5;
	        item.shootSpeed = 8f;
            item.noMelee = true; 
			item.shoot = 14;
            item.shootSpeed = 7f;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
            Item.staff[item.type] = true; 
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (player.altFunctionUse == 2)
            {
                if (player.statMana > 70)
                {
                    speedX *= 2f;
                    speedY *= 2f;
                    position += Vector2.Normalize(new Vector2(speedX, speedY)) * 48f;
                    for (int i = -4; i < 5; i += 1)
                    {

                        float rotation = MathHelper.ToRadians(32);
                        Vector2 perturbedSpeed = (new Vector2(speedX, speedY)).RotatedBy(MathHelper.ToRadians(i*5)) * 1.2f;
                        Vector2 dister = perturbedSpeed;
                        dister.Normalize();

                        int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.IceBlock, (int)(damage/5), knockBack, player.whoAmI, ((position+ dister * 160f).X)/16f, (position + dister * 160f).Y/16f);
                        Main.projectile[proj].friendly = true;
                        Main.projectile[proj].hostile = false;
                        Main.projectile[proj].timeLeft = 180;
                        Main.projectile[proj].knockBack = item.knockBack;
                        player.itemTime = 90;
                        
                    }
                    player.statMana -= (int)(60f * player.manaCost);
                    player.manaRegenDelay = 1200;
                }


            }
            else
            {

                float rotation = MathHelper.ToRadians(4);
                speedX *= 4f;
                speedY *= 4f;
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 48f;
                Vector2 perturbedSpeed = (new Vector2(speedX, speedY)).RotatedBy(MathHelper.Lerp(-rotation, rotation, (float)Main.rand.Next(0, 100) / 100f)) * .2f;

                int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CirnoBoltPlayer"), damage, knockBack, player.whoAmI);
                Main.projectile[proj].friendly = true;
                Main.projectile[proj].hostile = false;
                Main.projectile[proj].timeLeft = 240;
                Main.projectile[proj].knockBack = item.knockBack;

            }
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