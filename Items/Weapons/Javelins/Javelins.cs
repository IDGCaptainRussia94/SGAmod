using System;
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

    public class ShadowJavelin : StoneJavelin
    {

        public override float Stabspeed => 1.70f;
        public override float Throwspeed => 1f;
        public override int Penetrate => 5;
        public override float Speartype => 7;
        public override int[] Usetimes => new int[] { 25, 12 };
        public override string[] Normaltext => new string[] { "Made from evil Javelins and the dark essence emited by a shadow key, attacks may inflict shadowflame", "accelerates forward, is not affected by gravity until it hits a target" };
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 25;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 400;
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
            DisplayName.SetDefault("Pearl Wood Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 32;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 500;
            item.rare = 5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Pearlwood, 5);
            recipe.AddIngredient(ItemID.PearlstoneBrick, 10);
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
        public override string[] Normaltext => new string[] { "The Battle calls!","Summons extra Dynasty Javelins to fall from the sky when they damage an enemy"};
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynasty Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 17;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 300;
            item.rare = 3;
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
            DisplayName.SetDefault("Amber Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 18;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 300;
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
            DisplayName.SetDefault("Corruption Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 12;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 250;
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
            DisplayName.SetDefault("Crimson Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 16;
            item.width = 24;
            item.height = 24;
            item.knockBack = 4;
            item.value = 250;
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
            DisplayName.SetDefault("Ice Javelin");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 10;
            item.width = 24;
            item.height = 24;
            item.knockBack = 5;
            item.value = 200;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 5);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }

    }


}