using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System.IO;
using Terraria.ModLoader;
using Idglibrary;
using SGAmod.Items.Weapons.SeriousSam;

namespace SGAmod.Items.Weapons.Technical
{
	public class AssaultRifle : SeriousSamWeapon
	{
		int firemode = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tactical SMG Rifle");
			Tooltip.SetDefault("Adjustable Machinegun!\nVery Fast! But no ammo saving chance and causes very bad recoil if held down for too long.\nRight click to toggle firemodes");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.width = 42;
			item.height = 16;
			item.useTime = 3;
			item.useAnimation = 3;
			item.useStyle = 5;
			item.reuseDelay = 0;
			item.noMelee = true;
			item.knockBack = 1;
			item.value = 750000;
			item.rare = 8;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 40f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(firemode);
		}

		public override void NetRecieve(BinaryReader reader)
		{
			firemode = reader.ReadInt32();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.itemTime > 0 && player.altFunctionUse == 2)
				return false;
			if (player.altFunctionUse == 2)
			{
				string[] things = { "Fully Automatic", "Burst"};
				item.reuseDelay = 15;
				player.itemTime = 20;
				firemode += 1;
				firemode %= 2;
				Main.PlaySound(40, player.Center);
				if (Main.myPlayer == player.whoAmI)
				Main.NewText("Toggled: " + things[firemode] + " mode");
			}
			else
			{
				item.reuseDelay = 0;
				item.useTime = 3;
				item.useAnimation = 3;
				if (firemode == 1)
				{
					item.useTime = 2;
					item.useAnimation = 12;
					item.reuseDelay = 25;
				}

			}
			return (player.altFunctionUse!=2);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 8);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{

			if (player.GetModPlayer<SGAPlayer>().recoil<75f)
			player.GetModPlayer<SGAPlayer>().recoil += firemode==1 ? 0.4f : 0.75f;

			Main.PlaySound(SoundID.Item41, player.Center);

			float speed = 1.5f;
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(1+ player.GetModPlayer<SGAPlayer>().recoil);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;

			Vector2 perturbedSpeed2 = (new Vector2(speedX, speedY) * speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, (float)Main.rand.Next(0, 100) / 100f)) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.

			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = perturbedSpeed2.RotatedBy(MathHelper.ToRadians(MathHelper.Lerp(-0.1f, 0.1f, i/ (numberProjectiles-1)))); // Watch out for dividing by 0 if there is only 1 projectile.

				int proj = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				Main.projectile[proj].friendly = true;
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].knockBack = item.knockBack;
				player.itemRotation = Main.projectile[proj].velocity.ToRotation();
				if (player.direction < 0)
					player.itemRotation += (float)Math.PI;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts, 1);
			recipe.AddIngredient(ItemID.PlatinumBar, 10);
			recipe.AddIngredient(ItemID.ChainGun, 1);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 15);
			recipe.AddIngredient(mod.ItemType("PlasmaCell"), 2);
			recipe.AddIngredient(ItemID.Nanites, 100);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class OnyxTacticalShotgun : SeriousSamWeapon
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Onyx Tactical Shotgun");
			Tooltip.SetDefault("Fires a Spread of Both Bullets and Onyx Rockets");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.width = 56;
			item.height = 28;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 500000;
			item.rare = 7;
			item.UseSound = SoundID.Item38;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Bullet;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-18, -4);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "HeatBeater", 1);
			recipe.AddIngredient(null, "SharkTooth", 50);
			recipe.AddIngredient(ItemID.OnyxBlaster, 1);
			recipe.AddIngredient(ItemID.TacticalShotgun, 1);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 10);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 normz = new Vector2(speedX, speedY);normz.Normalize();
			position += normz * 24f;

			int numberProjectiles = 9 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale;
				int prog = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, (int)(damage*0.75), knockBack, player.whoAmI);
				IdgProjectile.Sync(prog);
			}
			numberProjectiles = 3 + Main.rand.Next(1);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				float scale = 1f - (Main.rand.NextFloat() * .6f);
				perturbedSpeed = perturbedSpeed * scale;
				int prog = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.BlackBolt, (int)(damage*2.0), knockBack, player.whoAmI);
				IdgProjectile.Sync(prog);

			}
			return false;
		}
	}

	public class CircuitBreakerBlade : SeriousSamWeapon
	{
		public static NPC FindClosestTarget(Player ply,Vector2 loc, Vector2 size, bool block = true, bool friendlycheck = true, bool chasecheck = false)
		{
			int num;
			float num170 = 1000000;
			NPC num171 = null;

			for (int num172 = 0; num172 < Main.maxNPCs; num172 = num + 1)
			{
				float num173 = Main.npc[num172].position.X + (float)(Main.npc[num172].width / 2);
				float num174 = Main.npc[num172].position.Y + (float)(Main.npc[num172].height / 2);
				float num175 = Math.Abs(loc.X + (float)(size.X / 2) - num173) + Math.Abs(loc.Y + (float)(size.Y / 2) - num174);
				if (Main.npc[num172].active)
				{

					if (num175 < num170 && !Main.npc[num172].dontTakeDamage && ((Collision.CanHit(new Vector2(loc.X, loc.Y), 1, 1, Main.npc[num172].position, Main.npc[num172].width, Main.npc[num172].height) && block) || block == false) && (Main.npc[num172].townNPC == false && (Main.npc[num172].CanBeChasedBy(new Projectile(), false) || !chasecheck)))
					{
						if (Main.npc[num172].immune[ply.whoAmI]<1)
						{
							num170 = num175;
							num171 = Main.npc[num172];
						}
					}
				}
				num = num172;
			}
			if (num170 > 400)
				return null;

			return num171;

		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Circuit Breaker Blade");
			Tooltip.SetDefault("Melee hits against enemies discharge bolts of energy at nearby enemies that chain to other enemies on hit\nChains up to a max of 3 times, and each bolt may hit 2 targets max\nCounts as a True Melee sword");
		}
		public override void SetDefaults()
		{
			base.SetDefaults();

			item.damage = 65;
			item.width = 48;
			item.height = 48;
			item.melee = true;
			item.useTurn = true;
			item.rare = 7;
			item.value = 400000;
			item.useStyle = 1;
			item.useAnimation = 35;
			item.useTime = 35;
			item.knockBack = 8;
			item.autoReuse = true;
			item.consumable = false;
			item.UseSound = SoundID.Item1;
			if (!Main.dedServ)
			{
				item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/GlowMasks/CircuitBreakerBlade_Glow");
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Vector2 position = player.Center;
			Vector2 eree = player.itemRotation.ToRotationVector2();
			eree *= player.direction;

			position += (eree * Main.rand.NextFloat(58f,160f));


			target.immune[player.whoAmI] = 15;
			NPC target2 = CircuitBreakerBlade.FindClosestTarget(player, position, new Vector2(0, 0));
			if (target2 != null)
			{
				Vector2 Speed = (target2.Center - target.Center);
				Speed.Normalize(); Speed *= 2f;
				int prog = Projectile.NewProjectile(target.Center.X, target.Center.Y, Speed.X, Speed.Y, ModContent.ProjectileType<CBreakerBolt>(), (int)(damage * 0.80), knockBack / 2f, player.whoAmI,3);
				IdgProjectile.Sync(prog);
				Main.PlaySound(SoundID.Item93, position);

			}

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("TeslaStaff"), 1);
			recipe.AddIngredient(ItemID.BreakerBlade, 1);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.Cog, 50);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 10);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}

	public class CBreakerBolt : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 2;
			projectile.melee = true;
			projectile.timeLeft = 120;
			projectile.light = 0.1f;
			projectile.extraUpdates = 120;
			aiType = -1;
			Main.projFrames[projectile.type] = 1;
		}

		public override string Texture
		{
			get { return "SGAmod/HavocGear/Projectiles/BoulderBlast"; }
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Breaker Bolt");
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.ai[0] > 0)
			{
				projectile.ai[0] -= 1;
				NPC target2 = CircuitBreakerBlade.FindClosestTarget(Main.player[projectile.owner], projectile.Center, new Vector2(0, 0));
				if (target2 != null)
				{
					Vector2 Speed = (target2.Center - target.Center);
					Speed.Normalize(); Speed *= 2f;
					int prog = Projectile.NewProjectile(target.Center.X, target.Center.Y, Speed.X, Speed.Y, ModContent.ProjectileType<CBreakerBolt>(),projectile.damage, projectile.knockBack / 2f, projectile.owner, projectile.ai[0]);
					IdgProjectile.Sync(prog);
					Main.PlaySound(SoundID.Item93, projectile.Center);
				}
			}
		}

		public override bool PreKill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize(); Vector2 ogcircle = randomcircle; randomcircle *= 1f;
				int num655 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 206, projectile.velocity.X + randomcircle.X * 8f, projectile.velocity.Y + randomcircle.Y * 8f, 100, new Color(30, 30, 30, 20), 1.5f);
				Main.dust[num655].noGravity = true;
				Main.dust[num655].velocity *= 0.5f;
			}


			return true;
		}

		public override void AI()
		{
			Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize(); Vector2 ogcircle = randomcircle; randomcircle *= 0.1f;
			int num655 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 206, projectile.velocity.X + randomcircle.X * 8f, projectile.velocity.Y + randomcircle.Y * 8f, 100, new Color(30, 30, 30, 20), 1f);
			Main.dust[num655].noGravity = true;
			Main.dust[num655].velocity *= 0.5f;

			if (projectile.localAI[1] == 0f)
			{
				projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			}
		}
	}

	public class TeslaStaff : SeriousSamWeapon
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tesla Staff");
			Tooltip.SetDefault("Zaps nearby enemies with a shock of electricity that is able to pierce twice");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.magic = true;
			item.mana = 3;
			item.width = 40;
			item.height = 40;
			item.useTime = 4;
			item.useAnimation = 4;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 0;
			item.value = 75000;
			item.rare = 3;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("UnmanedBolt");
			item.shootSpeed = 4f;
			if (!Main.dedServ)
			{
				item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/GlowMasks/TeslaStaff_Glow");
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 normz = new Vector2(speedX, speedY); normz.Normalize();
			position += normz * 58f;


				NPC target2 = CircuitBreakerBlade.FindClosestTarget(player, position, new Vector2(0, 0));
				if (target2 != null)
				{
					Vector2 Speed = (target2.Center - position);
					Speed.Normalize(); Speed *= 2f;
					int prog = Projectile.NewProjectile(position.X, position.Y, Speed.X, Speed.Y, ModContent.ProjectileType<CBreakerBolt>(), (int)(damage * 1), knockBack, player.whoAmI);
					Main.projectile[prog].melee = false;
				Main.projectile[prog].magic = true;
				IdgProjectile.Sync(prog);
					Main.PlaySound(SoundID.Item93, position);
				}

			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wire, 50);
			recipe.AddIngredient(mod.ItemType("UnmanedStaff"), 1);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 6);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

		public class Massacre : SeriousSamWeapon
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Massacre Prototype");
			Tooltip.SetDefault("Fires a chain of Stardust Explosions\nFiring this weapon throws you back\n'Ansaksie would not approve'");
		}

		public override void SetDefaults()
		{
			item.damage = 250;
			item.magic = true;
			item.width = 56;
			item.height = 28;
			item.useTime = 90;
			item.useAnimation = 90;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = Item.sellPrice(platinum: 2);
			item.rare = 11;
			item.UseSound = SoundID.Item122;
			item.autoReuse = true;
			item.shoot = 10;
			item.mana = 150;
			item.shootSpeed = 200f;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, -4);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PrismalLauncher", 1);
			recipe.AddIngredient(ItemID.ProximityMineLauncher,1);
			recipe.AddIngredient(ItemID.Stynger, 1);
			recipe.AddIngredient(ItemID.FragmentStardust, 10);
			recipe.AddIngredient(mod.ItemType("AdvancedPlating"), 6);
			recipe.AddIngredient(mod.ItemType("LunarRoyalGel"), 15);
			recipe.AddTile(mod.GetTile("ReverseEngineeringStation"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.velocity += new Vector2(Math.Sign(-player.direction) * 20, (-10f-(speedY / 15f)));
			int numberProjectiles = 4;// + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale;
				int prog = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MassacreShot>(), damage, knockBack, player.whoAmI);
				IdgProjectile.Sync(prog);
			}
			return false;
		}
	}

	public class MassacreShot : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.timeLeft = 10;
			projectile.light = 0.1f;
			projectile.extraUpdates = 0;
			projectile.tileCollide = false;
			aiType = -1;
			Main.projFrames[projectile.type] = 1;
		}

		public override string Texture
		{
			get { return "SGAmod/HavocGear/Projectiles/BoulderBlast"; }
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Massa proj");
		}

		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}


		public override void AI()
		{
			projectile.velocity = projectile.velocity.RotatedByRandom(MathHelper.ToRadians(25));
			Vector2 vex = Main.rand.NextVector2Circular(160, 160);
			int prog = Projectile.NewProjectile(projectile.Center.X+ vex.X, projectile.Center.Y+ vex.Y, 0,0, ProjectileID.StardustGuardianExplosion, projectile.damage, projectile.knockBack, projectile.owner,0f,8f);
			Main.projectile[prog].scale = 3f;
			Main.projectile[prog].usesLocalNPCImmunity = true;
			Main.projectile[prog].localNPCHitCooldown = -1;
			Main.projectile[prog].magic = true;
			Main.projectile[prog].minion = false;
			Main.projectile[prog].netUpdate = true;
			IdgProjectile.Sync(prog);
		}
	}


}
