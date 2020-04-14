using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Items.Weapons.Ammo
{
	public class BlazeBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blaze Bullet");
			Tooltip.SetDefault("May inflict Thermal Blaze on enemies");
		}
		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/Ammo/BlazeBullet"); }
		}
		public override void SetDefaults()
		{
			item.damage = 13;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 25;
			item.rare = 5;
			item.shoot = mod.ProjectileType("BlazeBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("WraithFragment4"), 2);
			recipe.AddIngredient(mod.ItemType("FieryShard"), 1);
			recipe.AddIngredient(ItemID.MusketBall, 50);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}

	public class AcidBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acid Round");
			Tooltip.SetDefault("High chance of inflicting Acid Burn\nAcid Burn does more damage the more defense the enemy has, and reduces their defense by 5\nAcid Rounds quickly melt away after being fired and do not go far");
		}
		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/Ammo/AcidBullet"); }
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 25;
			item.rare = 5;
			item.shoot = mod.ProjectileType("AcidBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 2.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("VialofAcid"), 1);
			recipe.AddIngredient(ItemID.MusketBall, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}

	public class TungstenBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tungsten Bullet");
			Tooltip.SetDefault("Isn't slowed down in water");
		}
		public override string Texture
		{
			get { return ("SGAmod/Items/Weapons/Ammo/TungstenBullet"); }
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 25;
			item.rare = 1;
			item.shoot = mod.ProjectileType("TungstenBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 4.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 1);
			recipe.AddIngredient(ItemID.MusketBall, 70);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 70);
			recipe.AddRecipe();
		}
	}

	public class AimBotBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aim-Bot Bullet");
			Tooltip.SetDefault("Adjusts your aim to target the scrub nearest your mouse curser; bullet travels instantly\nAimbot bullets can pass through 2 targets ending on the 3rd, do not cause immunity frames\nBullets do 20% increased damage after each hit they pass through\n'GIT GUD, GET LMAOBOX!'\n(disclaimer, does not function in pvp)");
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return lightColor = Main.hslToRgb((Main.GlobalTime / 7f) % 1f, 0.85f, 0.45f);
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 2.0f;
			item.value = 500;
			item.rare = 10;
			item.shoot = mod.ProjectileType("AimBotBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 1f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("Entrophite"), 30);
			recipe.AddIngredient(mod.ItemType("MoneySign"), 1);
			recipe.AddIngredient(mod.ItemType("ByteSoul"), 10);
			recipe.AddIngredient(ItemID.MoonlordBullet, 100);
			recipe.AddIngredient(ItemID.MeteorShot, 50);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 150);
			recipe.AddRecipe();
		}
	}
	public class PortalBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Portal Bullet");
			Tooltip.SetDefault("Portals appear at the mouse curser which summon high velocity bullets to fly at the nearby enemies");
		}
		public override string Texture
		{
			get { return ("Terraria/Item_"+ItemID.HighVelocityBullet); }
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{

			Texture2D inner = ModContent.GetTexture("Terraria/Projectile_" + 658);
			/*Texture2D texture = ModContent.GetTexture("Terraria/Projectile_" + 641);
			Texture2D outer = ModContent.GetTexture("Terraria/Projectile_" + 657);
			spriteBatch.Draw(inner, projectile.Center - Main.screenPosition, null, Color.Lerp(Color.Magenta, lightColor, 0.6f), (float)Math.Sin((double)projectile.rotation), new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(1, 1) * scale, SpriteEffects.None, 0f); ;
			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.Lerp(Color.Magenta, lightColor, 0.75f), projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(1, 1) * scale, SpriteEffects.None, 0f); ;
			spriteBatch.Draw(outer, projectile.Center - Main.screenPosition, null, Color.Lerp(Color.Magenta, lightColor, 0.75f), -projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(1, 1) * scale, SpriteEffects.None, 0f); ;
*/

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			spriteBatch.Draw(inner, position+ (new Vector2(4f,8f)*scale), null, drawColor, Main.GlobalTime, new Vector2(inner.Width / 2, inner.Height / 2), scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(Main.itemTexture[item.type], position, frame, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			return false;
		}

		public override void SetDefaults()
		{
			item.damage = 18;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 100;
			item.rare = 9;
			item.shoot = mod.ProjectileType("PortalBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 4.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarMetalBar"), 1);
			recipe.AddIngredient(mod.ItemType("PlasmaCell"), 1);
			recipe.AddIngredient(ItemID.HighVelocityBullet, 100);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}


	public class PrismicBullet : ModItem
	{
		public virtual int maxvalue => 2;
		public int ammotype;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Prismic Bullet");
			Tooltip.SetDefault("Fires Bullets from your 2nd and 3rd ammo slots while placed in your first, will consume both ammo types\nOtherwise, it fires a weak musket bullet");
		}
		public override string Texture
		{
			get { return ("Terraria/Item_"+ItemID.SilverBullet); }
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(ItemID.IntenseRainbowDye);
			shader.UseOpacity(0.5f);
			shader.UseSaturation(0.25f);
			shader.Apply(null);
			spriteBatch.Draw(Main.itemTexture[item.type], position, frame, drawColor,0, origin, scale, SpriteEffects.None, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			return false;
		}
		public override void SetDefaults()
		{
			item.damage = 2;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 25;
			item.rare = 5;
			item.shoot = ProjectileID.Bullet;   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 4.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}
		public virtual int GetAmmo(Player player,out int weapontype,bool previous = false)
		{
			bool canuse = true;
			weapontype = item.type;
			SGAPlayer sgaplayer = player.GetModPlayer<SGAPlayer>();
			Item newitem = new Item();
			newitem.SetDefaults(sgaplayer.ammoinboxes[sgaplayer.PrismalShots + (1)]);

			if (sgaplayer.ammoinboxes[1] == 0 || sgaplayer.ammoinboxes[1] == item.type)
				canuse = false;
			if (sgaplayer.ammoinboxes[2] == 0 || sgaplayer.ammoinboxes[2] == item.type)
				canuse = false;
			if ((sgaplayer.ammoinboxes[3] == 0 || sgaplayer.ammoinboxes[3] == item.type) && maxvalue>2)
				canuse = false;
			if (newitem.ammo != item.ammo)
				canuse = false;

			if (canuse)
			{
				if (!previous)
				{
					sgaplayer.PrismalShots += 1;
					sgaplayer.PrismalShots %= maxvalue;
				}
				ammotype = newitem.type;
				return newitem.shoot;
			}
			else
			{
				return item.shoot;
			}

		}

		public override void OnConsumeAmmo(Player player)
		{
			if (ammotype!=item.type)
			player.ConsumeItem(ammotype);
		}

		public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
		{
			int nothing = 0;
		type = GetAmmo(player,out nothing);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("SGAmod:Tier1Bars", 1);
			recipe.AddRecipeGroup("SGAmod:Tier2Bars", 1);
			recipe.AddRecipeGroup("SGAmod:Tier4Bars", 1);
			recipe.AddIngredient(mod.ItemType("WraithFragment3"), 2);
			recipe.AddIngredient(ItemID.HallowedBar, 1);
			recipe.AddIngredient(ItemID.SilverBullet, 50);
			recipe.AddIngredient(ModContent.ItemType<TungstenBullet>(), 50);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}

	public class PrismalBullet : PrismicBullet
	{
		public override int maxvalue => 3;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prismal Bullet");
			Tooltip.SetDefault("Does greatly increased damage over it's precurser; fires Bullets from your 2nd, 3rd, and 4th ammo slots while placed in your first, will consume fired ammo types\nOtherwise, it fires a weak musket bullet");
		}
		public override string Texture
		{
			get { return ("Terraria/Item_" + ItemID.SilverBullet); }
		}
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(ItemID.LivingRainbowDye);
			shader.Apply(null);
			spriteBatch.Draw(Main.itemTexture[item.type], position, frame, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
			return false;
		}
		public override void SetDefaults()
		{
			item.damage = 17;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = 25;
			item.rare = 10;
			item.shoot = ProjectileID.Bullet;   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 4.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PrismalBar"), 1);
			recipe.AddIngredient(mod.ItemType("PrismicBullet"), 100);
			recipe.AddTile(TileID.ImbuingStation);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}


}
