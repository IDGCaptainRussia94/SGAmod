using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
using Terraria.Utilities;
using Idglibrary;
using Idglibrary.Bases;

namespace SGAmod.Items.Weapons.Javelins
{

    public class SanguineBident : StoneJavelin
    {

        public override float Stabspeed => 3.6f;
        public override float Throwspeed => 10f;
        public override int Penetrate => 3;
        public override float Speartype => 8;
        public override int[] Usetimes => new int[] { 25, 10 };
        public override string[] Normaltext => new string[] {"Launch 3 projectiles on throw at foes", "Impaled targets may leach life, more likely to leach from bleeding targets","Melee strikes will make enemies bleed","Is considered a Jab-lin, but non consumable and able to have prefixes" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Bident");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 42;
            item.width = 32;
            item.height = 32;
            item.knockBack = 5;
            item.value = Item.buyPrice(gold: 5);
            item.rare = 7;
            item.consumable = false;
            item.maxStack = 1;
        }
        public override void OnThrow(int type,Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type2, ref int damage, ref float knockBack, JavelinProj madeproj)
        {
            if (type == 1)
            {
                Vector2 normalizedspeed = new Vector2(speedX, speedY);
                normalizedspeed.Normalize();
                normalizedspeed *= (Throwspeed * player.thrownVelocity);
                normalizedspeed.Y -= Math.Abs(normalizedspeed.Y * 0.1f);
                if (player.altFunctionUse == 2)
                {
                    for (int i = -15; i < 16; i += 30)
                    {
                        Vector2 perturbedSpeed = ((new Vector2(normalizedspeed.X, normalizedspeed.Y)).RotatedBy(MathHelper.ToRadians(i))).RotatedByRandom(MathHelper.ToRadians(10)) * 0.85f;
                        float scale = 1f - (Main.rand.NextFloat() * .01f);
                        perturbedSpeed = perturbedSpeed * scale;
                        type2 = mod.ProjectileType("JavelinProj");

                        int thisoneddddd = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type2, damage, knockBack, Main.myPlayer);
                        Main.projectile[thisoneddddd].ai[1] = Speartype;
                        Main.projectile[thisoneddddd].melee = false;
                        Main.projectile[thisoneddddd].thrown = true;

                        (Main.projectile[thisoneddddd].modProjectile as JavelinProj).maxstick = madeproj.maxstick;
                        Main.projectile[thisoneddddd].penetrate = madeproj.projectile.penetrate;
                        Main.projectile[thisoneddddd].netUpdate = true;
                        IdgProjectile.Sync(thisoneddddd);

                    }
                }

            }
            
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
                switch (rand.Next(16))
                {
                    case 1:
                        return PrefixID.Demonic;
                    case 2:
                        return PrefixID.Frenzying;
                    case 3:
                        return PrefixID.Dangerous;
                    case 4:
                        return PrefixID.Savage;
                    case 5:
                        return PrefixID.Furious;
                    case 6:
                        return PrefixID.Terrible;
                    case 7:
                        return PrefixID.Awful;
                    case 8:
                        return PrefixID.Dull;
                    case 9:
                        return PrefixID.Unhappy;
                    case 10:
                        return PrefixID.Unreal;
                    case 11:
                        return PrefixID.Shameful;
                    case 12:
                        return PrefixID.Heavy;
                    case 13:
                        return PrefixID.Zealous;
                    case 14:
                        return mod.PrefixType("Tossable");
                    case 15:
                        return mod.PrefixType("Impacting");
                    default:
                        return mod.PrefixType("Olympian");
                }
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrimsonJavelin"), 150);
            recipe.AddIngredient(ItemID.Vertebrae, 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 8);
            recipe.AddIngredient(ItemID.Trident, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

    }
    public class ShadowJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.70f;
        public override float Throwspeed => 1f;
        public override int Penetrate => 5;
        public override float Speartype => 7;
        public override int[] Usetimes => new int[] { 25, 12 };
        public override string[] Normaltext => new string[] { "Made from evil Jab-lins and the dark essence emited by a shadow key, attacks may inflict shadowflame", "Javelins accelerates forward, is not affected by gravity until it hits a target" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 25;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 40;
            item.rare = 4;
        }
        public override void AddRecipes()
        {
            ShadowJavelinRecipe recipe = new ShadowJavelinRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowKey, 1);
            recipe.AddRecipeGroup("SGAmod:EvilJavelins", 50);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }

    public class PearlWoodJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.70f;
        public override float Throwspeed => 11f;
        public override int Penetrate => 5;
        public override float Speartype => 6;
        public override int[] Usetimes => new int[] { 25, 8 };
        public override string[] Normaltext => new string[] { "The Hallow's wrath makes stars fall down on jabbed or impaled targets" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PearlWood Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 32;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 50;
            item.rare = 5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Pearlwood, 5);
            recipe.AddIngredient(ItemID.CrystalShard, 2);
            recipe.AddIngredient(ItemID.UnicornHorn, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }


    public class DynastyJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.70f;
        public override float Throwspeed => 10f;
        public override int Penetrate => 4;
        public override float Speartype => 5;
        public override int[] Usetimes => new int[] { 35, 12 };
        public override string[] Normaltext => new string[] { "The Battle calls!", "Summons extra Dynasty Javelins to fall from the sky when they damage an enemy" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynasty Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 17;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 30;
            item.rare = 3;
        }
        public override void AddRecipes()
        {
            //null
        }
    }

    public class AmberJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.70f;
        public override float Throwspeed => 10f;
        public override int Penetrate => 8;
        public override float Speartype => 4;
        public override int[] Usetimes => new int[] { 25, 12 };
        public override string[] Normaltext => new string[] { "Made from sandy materials, Sticks into targets for longer" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 18;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 30;
            item.rare = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalmWood, 5);
            recipe.AddIngredient(ItemID.Sandstone, 10);
            recipe.AddIngredient(ItemID.Amber, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }

        public class CorruptionJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.50f;
        public override float Throwspeed => 9f;
        public override float Speartype => 3;
        public override int[] Usetimes => new int[] { 30, 10 };
        public override string[] Normaltext => new string[] { "Made from corrupt materials" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corruption Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 12;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 25;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ebonwood, 5);
            recipe.AddIngredient(ItemID.EbonstoneBlock, 10);
            recipe.AddIngredient(ItemID.DemoniteBar, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }

    public class CrimsonJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.20f;
        public override float Throwspeed => 8f;
        public override float Speartype => 2;
        public override int[] Usetimes => new int[] { 40, 15 };
        public override string[] Normaltext => new string[] { "Made from bloody materials" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 16;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 25;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Shadewood, 5);
            recipe.AddIngredient(ItemID.CrimstoneBlock, 10);
            recipe.AddIngredient(ItemID.CrimtaneBar, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }

    public class IceJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.20f;
        public override float Throwspeed => 6f;
        public override float Speartype => 1;
        public override int[] Usetimes => new int[] { 40, 15 };
        public override string[] Normaltext => new string[] { "Made from cold materials, attacks may inflict frostburn" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Jab-lin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 11;
            item.width = 24;
            item.height = 24;
            item.knockBack = 5;
            item.value = 15;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 5);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(mod.ItemType("FrigidShard"), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }


}