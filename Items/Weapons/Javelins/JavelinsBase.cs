﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
using Idglibrary;
using Idglibrary.Bases;

namespace SGAmod.Items.Weapons.Javelins
{

        public class StoneJavelin : ModItem
    {

        //public delegate int PerformCalculation(int x, int y);
        //public Action<string> messageTarget;
        //public Func<int, int, bool> testForEquality = (x, y) => x == y;
        public virtual int Penetrate => 5;
        public virtual float Stabspeed => 1.20f;
        public virtual float Throwspeed => 6f;
        public virtual float Speartype => 0;
        public virtual int[] Usetimes => new int[] { 30,10};
        public virtual string[] Normaltext => new string[] { "It's a jab-lin made from stone" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Jab-lin");
            //Tooltip.SetDefault("Shoots a spread of bullets");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 5);
            recipe.AddIngredient(ItemID.StoneBlock, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
            //messageTarget = delegate(string s) { bool thisisit = testForEquality(2,6); };


            //messageTarget("this");
        }

        public void drawstuff(SpriteBatch spriteBatch, Vector2 position, Color drawColor, Color itemColor, float scale, bool inventory = true)
        {
            Texture2D textureSpear = ModContent.GetTexture(JavelinProj.tex[(int)Speartype] + "Spear");
            Texture2D textureJave = ModContent.GetTexture(JavelinProj.tex[(int)Speartype]);
            Vector2 slotSize = new Vector2(52f, 52f);
            Vector2 drawPos = position + slotSize * Main.inventoryScale / 2f;
            Vector2 textureOrigin = new Vector2(textureSpear.Width / 2, textureSpear.Height / 2);

            spriteBatch.Draw(textureSpear, drawPos - new Vector2(8, 8), null, drawColor, 0f, textureOrigin, inventory ? scale : Main.inventoryScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(textureJave, drawPos - new Vector2(8, 8), null, drawColor, 0f, textureOrigin, inventory ? scale : Main.inventoryScale, SpriteEffects.FlipHorizontally, 0f);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            drawstuff(spriteBatch,position,drawColor,itemColor,scale);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            drawstuff(spriteBatch, (item.Center-Main.screenPosition) - new Vector2(8, 8), lightColor, lightColor, scale,true);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] thetext = tt.text.Split(' ');
                string newline = "";
                List<string> valuez = new List<string>();
                foreach (string text2 in thetext)
                {
                    valuez.Add(text2 + " ");
                }
                valuez.Insert(1, "Melee/Throwing ");
                foreach (string text3 in valuez)
                {
                    newline += text3;
                }
                tt.text = newline;
            }

            tt = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] thetext = tt.text.Split(' ');
                string newline = "";
                List<string> valuez = new List<string>();
                int counter = 0;
                foreach (string text2 in thetext)
                {
                    counter += 1;
                    if (counter>1)
                    valuez.Add(text2 + " ");
                }
                int thecrit = Main.GlobalTime % 3f >= 1.5f ? Main.LocalPlayer.meleeCrit : Main.LocalPlayer.thrownCrit;
                string thecrittype = Main.GlobalTime % 3f >= 1.5f ? "Melee " : "Throwing ";
                valuez.Insert(0, thecrit+"% "+ thecrittype);
                foreach (string text3 in valuez)
                {
                    newline += text3;
                }
                tt.text = newline;
            }

            foreach ( string line in Normaltext){
            tooltips.Add(new TooltipLine(mod, "JavaLine", line));
            }
            tooltips.Add(new TooltipLine(mod, "JavaLine1", "Left click to quickly jab like a spear (melee damage done, may break after using)"));
            tooltips.Add(new TooltipLine(mod, "JavaLine1", "Right click to more slowly throw (throwing damage done, benefits from throwing velocity)"));
            tooltips.Add(new TooltipLine(mod, "JavaLine1", "Melee attacks have a solid 50% to not be consumed"));
            tooltips.Add(new TooltipLine(mod, "JavaLine1", "Thrown jab-lins stick into foes and do extra damage"));
            tooltips.Add(new TooltipLine(mod, "JavaLine1", "benefits from Throwing item saving chance and melee attack speed"));
        }

        public override bool ConsumeItem(Player player)
        {
            if (player.altFunctionUse != 2 && Main.rand.Next(0, 100) < 50)
                return false;
            return TrapDamageItems.SavingChanceMethod(player,true);
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.DayBreak);
            item.damage = 6;
            item.width = 24;
            item.height = 24;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noMelee = true;
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.knockBack = 5;
            item.reuseDelay = 1;
            item.value = 100;
            item.consumable = true;
            item.rare = 1;
            item.maxStack = 999;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("JavelinProj");
            item.shootSpeed = 1f;

        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += player.GetModPlayer<SGAPlayer>().JavelinBaseBundle ? 0.10f : 0f;
            add += player.GetModPlayer<SGAPlayer>().JavelinSpearHeadBundle ? 0.15f : 0f;
            add += (player.thrownDamage + player.meleeDamage) - 2;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = (int)((Usetimes[0] * player.meleeSpeed)/player.GetModPlayer<SGAPlayer>().ThrowingSpeed);
                item.useAnimation = (int)((Usetimes[0] * player.meleeSpeed) / player.GetModPlayer<SGAPlayer>().ThrowingSpeed);
                item.shootSpeed = 1f;

            }
            else
            {
                SGAPlayer sgaply = player.GetModPlayer<SGAPlayer>();
                item.useTime = (int)((Usetimes[1] * (player.meleeSpeed)));
                item.useAnimation = (int)((Usetimes[1] * (player.meleeSpeed)));
                item.shootSpeed = (1f / player.meleeSpeed)*sgaply.UseTimeMultiplier(this.item);
            }
                return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 normalizedspeed = new Vector2(speedX, speedY);
            normalizedspeed.Normalize();
            bool melee = true;
            float basemul = item.shootSpeed;
            if (player.altFunctionUse == 2)
            {
                normalizedspeed *= (Throwspeed*player.thrownVelocity);
                normalizedspeed.Y -= Math.Abs(normalizedspeed.Y* 0.1f);
                Vector2 perturbedSpeed = new Vector2(normalizedspeed.X, normalizedspeed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                float scale = 1f - (Main.rand.NextFloat() * .01f);
                perturbedSpeed = perturbedSpeed * scale;
                speedX = normalizedspeed.X; speedY = normalizedspeed.Y;
                item.useStyle = 1;
                type = mod.ProjectileType("JavelinProj");
                melee = false;
            }
           else
            {
                normalizedspeed *= Stabspeed* basemul;
                Vector2 perturbedSpeed = new Vector2(normalizedspeed.X, normalizedspeed.Y).RotatedByRandom(MathHelper.ToRadians(10));
                float scale = 1f - (Main.rand.NextFloat() * .01f);
                perturbedSpeed = perturbedSpeed * scale;
                item.useStyle = 5;
                speedX = normalizedspeed.X; speedY = normalizedspeed.Y;
                type = mod.ProjectileType("JavelinProjMelee");
            }

            int thisoned = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer);
            Main.projectile[thisoned].ai[1] = Speartype;
            Main.projectile[thisoned].melee = melee;
            Main.projectile[thisoned].thrown = !melee;
            if (player.altFunctionUse == 2 && Speartype==7)
            Main.projectile[thisoned].aiStyle = 18;
            if (player.altFunctionUse == 2)
            {
                (Main.projectile[thisoned].modProjectile as JavelinProj).maxstick += (player.GetModPlayer<SGAPlayer>().JavelinSpearHeadBundle ? 1 : 0);
            }

            Main.projectile[thisoned].netUpdate = true;
            if (!melee)
            Main.projectile[thisoned].penetrate = Penetrate;
            IdgProjectile.Sync(thisoned);
            return false;

        }
    }

    public class JavelinProj : ModProjectile
    {
        public int stickin = -1;
        public Player P;
        public Vector2 offset;
        public int maxstick = 1;
        static public string[] tex =
    {"SGAmod/Items/Weapons/Javelins/StoneJavelin",
        "SGAmod/Items/Weapons/Javelins/IceJavelin",
        "SGAmod/Items/Weapons/Javelins/CrimsonJavelin",
        "SGAmod/Items/Weapons/Javelins/CorruptionJavelin",
        "SGAmod/Items/Weapons/Javelins/AmberJavelin",
        "SGAmod/Items/Weapons/Javelins/DynastyJavelin",
        "SGAmod/Items/Weapons/Javelins/PearlWoodJavelin",
        "SGAmod/Items/Weapons/Javelins/ShadowJavelin",
        };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Javelin");
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(stickin);
            writer.Write(maxstick);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            stickin = reader.ReadInt32();
            maxstick = reader.ReadInt32();
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (stickin>-1)
                return false;
            return base.CanHitNPC(target);
        }

        public static void JavelinOnHit(NPC target,Projectile projectile)
        {
        if (projectile.ai[1] == 1)
            {
                if (Main.rand.Next(0,4)==1)
                target.AddBuff(BuffID.Frostburn, 60 * (projectile.type==ModContent.ProjectileType<JavelinProj>() ? 2 : 3));
            }
            if (projectile.ai[1] == 7)
            {
                if (Main.rand.Next(0, 4) == 1)
                    target.AddBuff(BuffID.ShadowFlame, 60 * (projectile.type == ModContent.ProjectileType<JavelinProj>() ? 3 : 5));
            }
            if (projectile.ai[1] == 5)
            {
                if (projectile.penetrate > 1)
                {
                    int thisoned = Projectile.NewProjectile(projectile.Center.X+ Main.rand.NextFloat(-64, 64), projectile.Center.Y - 800, Main.rand.NextFloat(-2,2), 14f, projectile.type, projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
                    Main.projectile[thisoned].ai[1] = projectile.ai[1];
                    Main.projectile[thisoned].thrown = true;
                    Main.projectile[thisoned].penetrate = projectile.penetrate-1;
                    Main.projectile[thisoned].netUpdate = true;
                }
            }

            if (projectile.ai[1] == 6)
            {
                if (Main.rand.Next(0, projectile.modProjectile.GetType() == typeof(JavelinProjMelee) ? 3 : 0) == 0)
                {

                    int thisoned = Projectile.NewProjectile(projectile.Center.X + Main.rand.NextFloat(-64, 64), projectile.Center.Y - 800, Main.rand.NextFloat(-2, 2), 14f, ProjectileID.HallowStar, (int)(projectile.damage/2f), projectile.knockBack, Main.player[projectile.owner].whoAmI);
                    Main.projectile[thisoned].ai[1] = projectile.ai[1];
                    Main.projectile[thisoned].thrown = true;
                    Main.projectile[thisoned].penetrate = 2;
                    Main.projectile[thisoned].netUpdate = true;
                    IdgProjectile.Sync(thisoned);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, thisoned);

                }
            }
            projectile.netUpdate = true;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.aiStyle = -1;
            JavelinProj.JavelinOnHit(target,projectile);

            int foundsticker = 0;

            for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != projectile.whoAmI // Make sure the looped projectile is not the current javelin
                    && currentProjectile.active // Make sure the projectile is active
                    && currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
                    && currentProjectile.type == projectile.type // Make sure the projectile is of the same type as this javelin
                    && currentProjectile.modProjectile is JavelinProj JavelinProjz // Use a pattern match cast so we can access the projectile like an ExampleJavelinProjectile
                    && JavelinProjz.stickin == target.whoAmI)
                {
                    foundsticker += 1;
                   //projectile.Kill();
                }

            }

            if (foundsticker<maxstick)
            {

                if (projectile.penetrate > 1)
                {
                    offset = (target.Center - projectile.Center);
                    stickin = target.whoAmI;
                    projectile.netUpdate = true;
                }
            }

        }


        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.melee = false;
            projectile.thrown = true;
            projectile.tileCollide = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 300;
            projectile.light = 0.25f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }

        public override string Texture
        {
            get { return ("SGAmod/Items/Weapons/Javelins/StoneJavelin"); }
        }

        public override void AI()
        {
            float facingleft = projectile.velocity.X > 0 ? 1f : -1f;
            projectile.rotation = ((float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f) +(float)((Math.PI)*-0.25f);

            if (stickin > -1)
            {
                NPC himz = Main.npc[stickin];
                projectile.tileCollide = false;

                if ((himz != null && himz.active) && projectile.penetrate>0)
                {
                    projectile.Center = (himz.Center - offset) - (projectile.velocity * 0.2f);
                    if (projectile.timeLeft < 100)
                    {
                        projectile.penetrate -= 1;
                        projectile.timeLeft = 200;
                        JavelinProj.JavelinOnHit(himz,projectile);
                        double damageperc = 0.75;
                        if (Main.player[projectile.owner].GetModPlayer<SGAPlayer>().JavelinBaseBundle)
                        damageperc += 0.25;
                        himz.StrikeNPC((int)(projectile.damage*damageperc),0,1);

                    }

                }
                else
                {
                    projectile.Kill();
                }

            }
            else
            {

                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, DustID.LunarOre,
                        projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 0.5f);
                    dust.velocity += projectile.velocity * 0.3f;
                    dust.velocity *= 0.2f;
                }

                if (projectile.velocity.Y<16f)
                if (projectile.aiStyle < 1)
                projectile.velocity = projectile.velocity + new Vector2(0, 0.1f);
            }

        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
        {
            bool facingleft = projectile.velocity.X > 0;
            Microsoft.Xna.Framework.Graphics.SpriteEffects effect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            Texture2D texture = ModContent.GetTexture(JavelinProj.tex[(int)projectile.ai[1]]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), drawColor, projectile.rotation + (facingleft ? (float)(1f * Math.PI) : 0f) - (((float)Math.PI / 2)*(facingleft ? 0f : -1f)), origin, projectile.scale, facingleft ? effect : SpriteEffects.FlipHorizontally, 0);
            return false;
        }
    }

    public class JavelinProjMelee : ProjectileSpearBase
    {

        bool mousecurser;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Javelin");
        }

        public override string Texture
        {
            get { return ("SGAmod/Items/Weapons/Javelins/StoneJavelin"); }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            JavelinProj.JavelinOnHit(target, projectile);
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.scale = 1.2f;
            projectile.knockBack = 1f;
            projectile.aiStyle = 19;
            projectile.melee = true;
            projectile.timeLeft = 90;
            projectile.hide = true;

            movein = 3f;
            moveout = 3f;
            thrustspeed = 3.0f;
        }

        public override void AI()
        {
            base.AI();
            if (projectile.owner == Main.myPlayer)
            {
                mousecurser = (Main.MouseScreen.X - projectile.Center.X)>0;
                projectile.direction = mousecurser.ToDirectionInt();
                projectile.netUpdate = true;


            }

        }

        public new float movementFactor
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
        {

            bool facingleft = projectile.direction < 0f;
            Microsoft.Xna.Framework.Graphics.SpriteEffects effect = SpriteEffects.FlipHorizontally;
            Texture2D texture = ModContent.GetTexture(JavelinProj.tex[(int)projectile.ai[1]]+"Spear");
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            if (facingleft)
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), drawColor, ((projectile.rotation-(float)Math.PI/2) - (float)Math.PI / 2) + (facingleft ? (float)(1f * Math.PI) : 0f), origin, projectile.scale,effect, 0);
            if (!facingleft)
                Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), drawColor, (projectile.rotation - (float)Math.PI / 2) + (facingleft ? (float)(1f * Math.PI) : 0f), origin, projectile.scale,SpriteEffects.None, 0);

            return false;
        }

    }

}