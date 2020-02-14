using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Shaders;
using SGAmod.NPCs;
using SGAmod.NPCs.Wraiths;
using SGAmod.NPCs.Cratrosity;
using SGAmod.NPCs.Murk;
using SGAmod.NPCs.Sharkvern;
using SGAmod.NPCs.SpiderQueen;
using CalamityMod;

namespace SGAmod
{

	public class SGAPlayer : ModPlayer
	{

		public int beefield = 0;
		public int beefieldtoggle = 0;
		public int beefieldcounter = 0;
		public bool HeavyCrates = false;
		public bool Microtransactions = false;
		public bool MoneyMismanagement = false;
		public bool NoFly = false;
		public bool Pressured = false;
		public bool MassiveBleeding = false;
		public bool ActionCooldown = false;
		public bool thermalblaze = false; public bool acidburn = false;
		public bool LifeFlower = false; public bool GeyserInABottle=false; public bool GeyserInABottleActive = false; public bool JavelinBaseBundle = false;
		public bool Matrix = false;
		public int EnhancingCharm = 0;
		public int FieryheartBuff = 0;
		public int creeperexplosion = 0;
		private Projectile[] projectileslunarslime = new Projectile[15];

		public bool lunarSlimeHeart = false;
		public int lunarSlimeCounter = 0;

		public bool Lockedin = false;
		public bool CirnoWings = false;
		public bool SerratedTooth = false;
		private int lockedelay = 0;
		public int Novusset = 0;	public bool Blazewyrmset = false;	public bool SpaceDiverset = false;	public bool MisterCreeperset = false;	public bool Mangroveset = false;
		public float SpaceDiverWings = 0f;
		public int Havoc = 0;
		public int breathingdelay = 0;
		public int sufficate = 200;
		public float UseTimeMul = 1f;
		public bool Noselfdamage = false;
		public float UseTimeMulPickaxe = 1f;
		public float TrapDamageMul = 1f;
		public float ThrowingSpeed = 1f;
		public Vector2 Locked = new Vector2(100, 300);
		public int ammoLeftInClip = 6; public int plasmaLeftInClip = 1000;
		public int ammoLeftInClipMax = 6; public int plasmaLeftInClipMax = 1000;
		public bool modcheckdelay = false;
		public bool gottf2 = false;
		public int floatyeffect = 0;
		public int PrismalShots = 0;
		public int devpower = 0;
		public float beedamagemul = 1f;
		public bool JaggedWoodenSpike = false;		public bool JuryRiggedSpikeBuckler = false;
		public bool devpowerbool = false; public int Redmanastar = 0;
		public int MidasIdol = 0;
		public bool MurkyDepths=false;
		public int[] ammoinboxes = new int[4];
		public int anticipation = 0; public int anticipationLevel = 0;

		public List<int> ExpertisePointsFromBosses;
		public List<int> ExpertisePointsFromBossesPoints;
		public int ExpertiseCollected = 0;
		public int ExpertiseCollectedTotal = 0;


		enum MessageType : byte
		{
			ClientSendInfo
		}

		public int Microtransactionsdelay = 0;

		public bool CalamityAbyss
		{
			get
			{
				/*Player player2 = (this as ModPlayer).player;
				if (ModLoader.GetMod("CalamityMod") != null)
				{
					CalamityPlayer CPlayer = player2.GetModPlayer(ModLoader.GetMod("CalamityMod"), "CalamityPlayer") as CalamityPlayer;
					Type CType = CPlayer.GetType();
					PropertyInfo CProperty = CType.GetProperty("ZoneAbyss");

					if (CProperty != null)
					{
						return !CPlayer.ZoneAbyss;
					}
				}*/
				return false;

			}
		}

		public bool RefilPlasma(bool checkagain=false)
		{
			if (plasmaLeftInClip > 0)
			{
				return true;
			}

			if (plasmaLeftInClip < 1 || checkagain)
			{

				if (player.HasItem(mod.ItemType("PlasmaCell")))
				{
					player.ConsumeItem(mod.ItemType("PlasmaCell"));
					plasmaLeftInClip = Math.Min(plasmaLeftInClip+1000,plasmaLeftInClipMax);
					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.LawnGreen, "Plasma Recharged!", false, false);
					if (plasmaLeftInClip<plasmaLeftInClipMax && checkagain)
					{
						RefilPlasma(true);

					}
					return true;
				}
			}
			return false;
		}

		public static void LimitProjectiles(Player player, int maxprojs, ushort[] types)
		{

			int projcount = 0;
			for (int a = 0; a < types.Length; a++)
			{
				projcount += player.ownedProjectileCounts[(int)types[a]];
			}

			Projectile removethisone = null;
			int timecheck = 99999999;

			if (projcount > maxprojs) {
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile him = Main.projectile[i];
					if (types.Any(x => x == Main.projectile[i].type)) {
						if (him.active && him.owner == player.whoAmI && him.timeLeft < timecheck) {
							removethisone = him;
							timecheck = him.timeLeft;
						} } }
				if (removethisone != null) {
					removethisone.Kill();
				} }

		}

		public void upgradetf2()
		{
			if (!gottf2 && player == Main.LocalPlayer)
			{
				Main.NewText("You have received your TF2 Emblem!", 150, 150, 150);
				player.QuickSpawnItem(mod.ItemType("TF2Emblem"), 1);
				gottf2 = true;
			}
		}


		public override void ResetEffects()
		{
			HeavyCrates = false;
			Microtransactions = false;
			MoneyMismanagement = false;
			Lockedin = false;
			NoFly = false;
			CirnoWings = false;
			MassiveBleeding = false;
			thermalblaze = false; acidburn = false;
			SerratedTooth = false;
			UseTimeMul = 1f;
			UseTimeMulPickaxe = 1f;
			ThrowingSpeed = 1f;
			SpaceDiverset = false;
			Blazewyrmset = false;
			Mangroveset = false;
			Pressured = false;
			Havoc = 0;
			ammoLeftInClipMax = 6;
			SpaceDiverWings = 0f;
			ActionCooldown = false;
			lunarSlimeHeart = false;
			TrapDamageMul = 1f;
			LifeFlower = false; GeyserInABottleActive = false; JavelinBaseBundle = false;
			EnhancingCharm = 0;
			if (devpower>0)
			devpower -= 1;
			devpowerbool = false;
			MisterCreeperset = false;
			Noselfdamage = false;
			JaggedWoodenSpike = false; JuryRiggedSpikeBuckler = false;
			MidasIdol = 0;
			MurkyDepths = false;
			Matrix = false;
			plasmaLeftInClipMax = 1000;
			beedamagemul = 1f;
			anticipationLevel = -1;
		}

		private void SendClientChangesPacket()
		{

			if (Main.netMode == 1) {
				ModPacket packet = SGAmod.Instance.GetPacket();
				packet.Write((byte)MessageType.ClientSendInfo);
				packet.Write(player.whoAmI);
				packet.Write(ammoLeftInClip);
				packet.Write(sufficate);
				packet.Write(PrismalShots);
				packet.Write(devpower);
				packet.Write(plasmaLeftInClip);
				for (int i = 54; i < 58; i++)
				{
					packet.Write(ammoinboxes[i - 54]);
				}
				packet.Send();
			}

		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{

			if (damageSource.SourceCustomReason == (player.name + " went Kamikaze, but failed to blow up enemies"))
				return true;

			if (creeperexplosion > 9795)
				return false;

			if (LifeFlower && !player.HasBuff(BuffID.PotionSickness))
			{
				bool potionsickness = player.HasBuff(BuffID.PotionSickness);
				if (player.QuickHeal_GetItemToUse() == null)
				potionsickness = true;
				else
				player.QuickHeal();
				return potionsickness;
			}

			if (MisterCreeperset && !player.HasBuff(mod.BuffType("ActionCooldown")))
			{
				creeperexplosion = 10000;
				player.statLife = 1;
				player.AddBuff(mod.BuffType("ActionCooldown"), 60 * 60);
				return false;
			}

			return true;
		}

		public override void clientClone(ModPlayer clientClone)
		{
			SGAPlayer sgaplayer = clientClone as SGAPlayer;
			sgaplayer.ammoLeftInClip = ammoLeftInClip;
			sgaplayer.sufficate = sufficate;
			sgaplayer.PrismalShots = PrismalShots;
			sgaplayer.devpower = devpower;
			sgaplayer.plasmaLeftInClip = plasmaLeftInClip;
			sgaplayer.Redmanastar = Redmanastar;

			for (int i = 54; i < 58; i++)
			{

				sgaplayer.ammoinboxes[i - 54] = ammoinboxes[i - 54];
			}
		}

		public override void SendClientChanges(ModPlayer clientPlayer)
		{
			bool mismatch = false;
			SGAPlayer sgaplayer = clientPlayer as SGAPlayer;

			for (int i = 54; i < 58; i++)
			{
				if (sgaplayer.ammoinboxes[i - 54] != ammoinboxes[i - 54])
				{
					mismatch = true;
					break;
				}
			}
			if (sgaplayer.ammoLeftInClip != ammoLeftInClip || sgaplayer.sufficate != sufficate || sgaplayer.PrismalShots != PrismalShots || sgaplayer.devpower != devpower 
			|| sgaplayer.plasmaLeftInClip!= plasmaLeftInClip|| sgaplayer.Redmanastar != Redmanastar)
			mismatch = true;


			if (mismatch) {
				SendClientChangesPacket();
			}
		}


		public override void UpdateBadLifeRegen()
		{

			if (MassiveBleeding) {
				if (player.lifeRegen > 0)
					player.lifeRegen = 0;
				player.lifeRegenTime = 0;
				player.lifeRegen -= 10;
			}
			if (thermalblaze)
			{
				int boost = 0;
				if (player.HasBuff(BuffID.Oiled))
					boost = 50;
				player.lifeRegen -= 30+boost;
			}
			if (acidburn)
			{
				player.lifeRegen -= 20 + player.statDefense;
				player.statDefense -= 5;
			}

			if (Pressured && !SpaceDiverset)
			{
				player.lifeRegen -= 250;
			}
		}

		public override void PostUpdateRunSpeeds()
		{

			if (player.HeldItem.type == mod.ItemType("Powerjack"))
			{
				player.moveSpeed *= 1.15f;
				player.accRunSpeed *= 1.15f;
				player.maxRunSpeed *= 1.15f;
			}

		}

		public override void PreUpdate()
		{
			for (int i = 54; i < 58; i++)
			{

				ammoinboxes[i - 54] = player.inventory[i].type;
			}
		}

		public override void PostUpdateBuffs()
		{
			player.statManaMax2 += 20 * Redmanastar;
		}

		public override void PostUpdateEquips()
		{

			if (EnhancingCharm > 0)
			{
				if (SGAWorld.modtimer % (5 - EnhancingCharm) == 0)
				{
					//longerExpertDebuff
					for (int i = 0; i < Player.MaxBuffs; i += 1)
					{
						if (player.buffType[i] != BuffID.PotionSickness && player.buffType[i] != BuffID.ManaSickness && player.buffType[i] != mod.BuffType("Matrix"))
						{
							ModBuff buff = ModContent.GetModBuff(player.buffType[i]);
							bool isdebuff = Main.debuff[player.buffType[i]];
							if (player.buffTime[i] > 10 && ((buff != null && ((buff.longerExpertDebuff && isdebuff) || !isdebuff)) || buff == null))
							{
								player.buffTime[i] += isdebuff ? -2 : 1;
							}
						}
					}
				}
			}

			if (anticipationLevel > 0)
			{
				if (IdgNPC.bossAlive && !player.HasBuff(mod.BuffType("BossHealingCooldown")) && anticipation<20*anticipationLevel)
				{
					anticipation = 100;
					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Green, "Anticipated!", false, false);
					CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 48, player.width, player.height), Color.Green, "+" + anticipationLevel * 100 + "!", false, false);
					player.AddBuff(mod.BuffType("BossHealingCooldown"), 120 * 60);
					player.statLife += anticipationLevel * 100;
				}
				Item helditem = player.HeldItem;
				if (helditem.thrown)
				player.thrownDamage += (float)(anticipation / 3000);
				if (helditem.magic)
					player.magicDamage += (float)(anticipation / 3000);
				if (helditem.summon)
					player.minionDamage += (float)(anticipation / 3000);
				if (helditem.ranged)
					player.rangedDamage += (float)(anticipation / 3000);
				if (helditem.melee)
					player.meleeDamage += (float)(anticipation / 3000);
			}

			int adderlevel = Math.Max(-1, (int)Math.Pow(anticipationLevel, 0.75));
			int[] ammounts = { 0, 150, 400, 900};
			int adder2;
			if (anticipationLevel > -1)
			adder2 = ammounts[anticipationLevel];
			else
			adder2 = -1;

			anticipation = (int)MathHelper.Clamp(anticipation + (IdgNPC.bossAlive ? adderlevel : -1), 0, (100+(adder2))*3);

			if (creeperexplosion > 9700)
			{
				creeperexplosion -= 1;

				if (creeperexplosion==9998)
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Creeper_fuse").WithVolume(.7f).WithPitchVariance(.25f), player.Center);

				int dustIndexsmoke = Dust.NewDust(new Vector2(player.Center.X-4, player.position.Y-6), 8, 12, 31, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndexsmoke].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndexsmoke].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndexsmoke].noGravity = true;
				dustIndexsmoke = Dust.NewDust(new Vector2(player.Center.X, player.position.Y-6), 8, 12, 6, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndexsmoke].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndexsmoke].noGravity = true;


				if (creeperexplosion == 9800)
				{
					for (int xx = 64; xx < 200; xx += 48)
					{

						for (int i = 0; i < 359; i += 36)
						{
							double angles = MathHelper.ToRadians(i+(xx*4));
							float randomx = xx;//Main.rand.NextFloat(54f, 96f);
							Vector2 here = new Vector2((float)Math.Cos(angles), (float)Math.Sin(angles));

							int thisone = Projectile.NewProjectile(player.Center.X + (here.X * randomx) - 100, player.Center.Y + (here.Y * randomx) - 100, here.X, here.Y, mod.ProjectileType("CreepersThrowBoom2"), player.statDefense * 8, 0f, player.whoAmI, 0.0f, 0f);
							Main.projectile[thisone].timeLeft = 3;
							Main.projectile[thisone].width = 200;
							Main.projectile[thisone].height = 200;
							Main.projectile[thisone].scale = 0.001f;
							Main.projectile[thisone].netUpdate = true;
							Main.projectile[thisone].timeLeft = 2;
							Main.projectile[thisone].penetrate = 1;
						}

					}

				}


			}

			if (creeperexplosion < 9798 && creeperexplosion > 2000)
			{
				creeperexplosion = 0;
				Noselfdamage = false;
				PlayerDeathReason reason = PlayerDeathReason.ByCustomReason(player.name + " went Kamikaze, but failed to blow up enemies");
				reason.SourcePlayerIndex = -111;
				reason.SourceNPCIndex = 0;
				player.KillMe(reason, 1337000, player.direction);
			}


			if (floatyeffect>-1)
floatyeffect-=1;

FieryheartBuff = FieryheartBuff-1;
beefield=beefield-1;
beefieldtoggle=beefieldtoggle-1;
Novusset-=1;

breathingdelay+=1; breathingdelay%=30;
if (sufficate<0){
if (breathingdelay%5==0)
sufficate=(int)MathHelper.Clamp(sufficate+1,-200,player.breathMax-1);
}else{
if (breathingdelay%29==0) 
sufficate=(int)MathHelper.Clamp(sufficate+1,-200,player.breathMax-1);
}


			//if (this.grappling[0] == -1 && this.carpet && !this.jumpAgainCloud && !this.jumpAgainSandstorm && !this.jumpAgainBlizzard && !this.jumpAgainFart && !this.jumpAgainSail && !this.jumpAgainUnicorn && this.jump == 0 && this.velocity.Y != 0f && this.rocketTime == 0 && this.wingTime == 0f && !this.mount.Active)
			//{
				if (SpaceDiverWings > 0)
				{
				float spacediverwingstemp = Math.Max(SpaceDiverWings, 1f);
				if (player.controlJump && player.velocity.Y != 0f)
					{
						bool pressdownonly = (!player.controlLeft && !player.controlRight);


					if (SpaceDiverset)
					{

						int dust = Dust.NewDust(new Vector2(player.Center.X - 12, player.Center.Y + 18), 24, 8, 27);
						Main.dust[dust].scale = 1.5f;
						Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
						Main.dust[dust].velocity = (randomcircle / 2f) + new Vector2(0, player.wingTime > 0 ? 12 : 3);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);

					}

						if (player.controlDown)
						{
							if (player.controlLeft && player.wingTime > 0)
								player.velocity.X -= ((float)spacediverwingstemp / 2f);
							if (player.controlRight && player.wingTime > 0)
								player.velocity.X += ((float)spacediverwingstemp / 2f);
							if (pressdownonly || player.wingTime < 1)
							{
								player.velocity.Y += 0.025f;
								int minTilePosX = (int)(player.Center.X / 16.0) - 1;
								int minTilePosY = (int)((player.Center.Y + 32f) / 16.0) - 1;
								int whereisity;
								whereisity = Idglib.RaycastDown(minTilePosX + 1, Math.Max(minTilePosY, 0));
								if ((whereisity - minTilePosY > 4 + (player.velocity.Y * 1)) || player.velocity.Y < 0)
									player.position.Y += 8 + (player.velocity.Y * 2);
							}
							else
							{
								if (player.wingTime > 0)
									player.velocity.Y /= 2f;
							}

							player.velocity.X /= 1.02f;

							int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 27);
							Main.dust[dust].scale = 1.25f;
						Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
							Main.dust[dust].velocity = (randomcircle / 3f) - player.velocity;
							Main.dust[dust].velocity.Normalize();
							Main.dust[dust].velocity *= 2f;
							Main.dust[dust].noGravity = true;
							Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);

						if (breathingdelay % 3 == 0)
							{
								float pitcher = -0.99f + ((float)player.wingTime / (float)player.wingTimeMax);
								pitcher = MathHelper.Clamp(pitcher, -0.9f, 1f);
								Main.PlaySound(SoundID.Item, (int)player.position.X, (int)player.position.Y, 75, 0.25f, pitcher);
							}

						}

					}
				}


			if (SpaceDiverset)
			{
				player.AddBuff(mod.BuffType("Pressured"), 180);

				player.gills=false;

			bool isbreathing=true;

if (SGAmod.Instance.Calamity && modcheckdelay){ isbreathing=CalamityAbyss;
}

if (isbreathing)
player.breath=(int)MathHelper.Clamp(sufficate,-5,player.breathMax-1);
if (sufficate<1){
player.suffocating=true;
}else{
player.endurance+=((float)player.breath/(float)player.breathMax)*0.4f;
player.statDefense+=(int)(((float)player.breath/(float)player.breathMax)*100f);
}

}

if (Havoc>0){

			for (int x = 3; x < 8 + player.extraAccessorySlots; x++)
			{
            if (player.armor[x].modItem!=null){
            var myType = (player.armor[x].modItem).GetType();
            var n = myType.Namespace;
            string asastring = (string)n;
            //int ishavocitem = (asastring.Split('.').Length - 1);
            int ishavocitem = asastring.Length - asastring.Replace("HavocGear.", "").Length;
            if (ishavocitem>0){
            player.statDefense+=(Main.hardMode ? 8 : 3);

			}}}

}


if (NPC.CountNPCS(mod.NPCType("Cirno"))>0 || (SGAWorld.downedCirno==false && Main.hardMode))
player.AddBuff(mod.BuffType("NoFly"), 1, true);

if (NPC.CountNPCS(mod.NPCType("CratrosityCrateOfSlowing"))>0){
player.AddBuff(BuffID.Slow, 2, true);
}
int pmlcrato=NPC.CountNPCS(mod.NPCType("Cratrogeddon"));
int npctype=NPC.CountNPCS(mod.NPCType("Cratrosity"))+pmlcrato;

if (pmlcrato>0 || NPC.CountNPCS(mod.NPCType("SPinky"))>9990){player.AddBuff(mod.BuffType("Locked"), 2, true);}

if (npctype>0){
int counter=(player.CountItem(ItemID.WoodenCrate));
counter+=(player.CountItem(ItemID.IronCrate));
counter+=(player.CountItem(ItemID.GoldenCrate));
counter+=(player.CountItem(ItemID.DungeonFishingCrate));
counter+=(player.CountItem(ItemID.JungleFishingCrate));
counter+=(player.CountItem(ItemID.CorruptFishingCrate));
counter+=(player.CountItem(ItemID.HallowedFishingCrate));
counter+=(player.CountItem(ItemID.FloatingIslandFishingCrate));
if (counter>0){
player.AddBuff(mod.BuffType("HeavyCrates"), 2, true);
}}

if (HeavyCrates){
player.runAcceleration/=3f;
}

if (Lockedin){
lockedelay+=1;
if (lockedelay>30)
player.position=new Vector2(Math.Min(Math.Max(player.position.X,Locked.X),Locked.X+Locked.Y),player.position.Y);
player.position=new Vector2(player.position.X,player.position.Y);
}else{
lockedelay=0;
}

if (NoFly){
player.wingTimeMax = player.wingTimeMax/10;
}

if (CirnoWings==true){
player.buffImmune[BuffID.Chilled]=true;
player.buffImmune[BuffID.Frozen]=true;
player.buffImmune[BuffID.Frostburn]=true;
}


int losingmoney=MoneyMismanagement==true ? 2 : (Microtransactions==true ? 1 : 0);
if (losingmoney>0){
Microtransactionsdelay+=1;
if (Microtransactionsdelay%30==0){
int taketype=3;
int [] types = {ItemID.CopperCoin,ItemID.SilverCoin,ItemID.GoldCoin,ItemID.PlatinumCoin};
int copper=player.CountItem(ItemID.CopperCoin);
int silver=player.CountItem(ItemID.SilverCoin);
int gold=player.CountItem(ItemID.GoldCoin);
int plat=player.CountItem(ItemID.PlatinumCoin);
taketype = plat>0 ? 3 : (gold>0 ? 2 : (silver>0 ? 1 : 0));
player.ConsumeItem(types[taketype]);
if (losingmoney>1){
//player.Hurt(PlayerDeathReason damageSource, int Damage, int hitDirection, bool pvp = false, bool quiet = false, bool Crit = false, int cooldownCounter = -1)
player.statLife-=taketype*5;

if (player.statLife<1){player.KillMe(PlayerDeathReason.ByCustomReason(player.name + (Main.rand.Next(0,100)>66 ? " Disgraced Gaben..." : (Main.rand.Next(0,100)>50 ? " couldn't stop spending money" : " couldn't resist the sale"))), 111111, 0, false);}

}
}}


if (beefield>3){
beefieldcounter=beefieldcounter+1;
if (player.ownedProjectileCounts[181] < 5 && beefieldcounter>60)
{
beefieldcounter=0;
			bool beeflag = false;
			int x = 3;
			for (x = 3; x < 8 + player.extraAccessorySlots; x++)
				{
				if (player.armor[x].type == mod.ItemType("PortableHive") || player.armor[x].type == mod.ItemType("DevPower"))
					{
					beeflag = true;
					//if (1f + (player.armor[x].damage * 0.05f)>beedamagemul)
					//beedamagemul = 1f+(player.armor[x].damage*0.05f);
					break;
					}
				}
			if (beeflag==true){
int prog=Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, 181, (int)(player.GetWeaponDamage(player.armor[x])), (float)player.GetWeaponKnockback(player.armor[x], player.armor[x].knockBack)*0.01f, player.whoAmI);
SGAprojectile modeproj=Main.projectile[prog].GetGlobalProjectile<SGAprojectile>();
Main.projectile[prog].penetrate=-1;
modeproj.enhancedbees=true;
			}
		}

}

			if (lunarSlimeHeart)
			{

				int Buffscounter = 0;
				for (int z = 0; z < Player.MaxBuffs; z++)
				{

					if (player.buffType[z] > 0)
					Buffscounter += Main.debuff[player.buffType[z]] ? 4 : 1;

				}
				player.statDefense += Buffscounter*2;

					lunarSlimeCounter = lunarSlimeCounter + 1;
				if (player.ownedProjectileCounts[mod.ProjectileType("LunarSlimeProjectile")] < 8)
				{
					bool beeflag = false;
					int x = 3;
					for (x = 3; x < 8 + player.extraAccessorySlots; x++)
					{
						if (player.armor[x].type == mod.ItemType("LunarSlimeHeart"))
						{
							beeflag = true;
							break;
						}
					}
					if (beeflag == true)
					{
						for (int i = 0; i < 7; i++)
						{
							if (projectileslunarslime[i]==null || !projectileslunarslime[i].active)
							{
								int prog = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("LunarSlimeProjectile"), (int)player.GetWeaponDamage(player.armor[x]), (float)player.GetWeaponKnockback(player.armor[x], player.armor[x].knockBack) * 0.01f, player.whoAmI, (float)i);
								SGAprojectile modeproj = Main.projectile[prog].GetGlobalProjectile<SGAprojectile>();
								//Main.projectile[prog].netUpdate = true;
								projectileslunarslime[i]= Main.projectile[prog];
							}
						}
					}
				}


			}
			if (player.ownedProjectileCounts[mod.ProjectileType("TimeEffect")] < 1 && Matrix)
			{
				int prog = Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("TimeEffect"), 1, 0, player.whoAmI);
			}


			modcheckdelay =true;
		}

		public override void PreUpdateMovement()
		{
			if (GeyserInABottleActive && GeyserInABottle)
			{
					if (player.controlJump && !player.jumpAgainCloud)
					{
						List<Projectile> itz = Idglib.Shattershots(player.Center + new Vector2(Main.rand.Next(-15, 15),player.height), player.Center + new Vector2(0, player.height+32), new Vector2(0, 0), ProjectileID.GeyserTrap, 30, 5f, 30, 1, true, 0, false, 400);
						//itz[0].damage = 30;
						itz[0].owner = player.whoAmI;
						itz[0].friendly = true;
						itz[0].hostile = true;
						Main.projectile[itz[0].whoAmI].netUpdate = true;
						if (Main.netMode == 2 && itz[0].whoAmI < 200)
						{
							NetMessage.SendData(27, -1, -1, null, itz[0].whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}

						itz = Idglib.Shattershots(player.Center + new Vector2(Main.rand.Next(-15, 15), player.height), player.Center + new Vector2(0, player.height - 180), new Vector2(0, 0), ProjectileID.GeyserTrap, 30, 10f, 30, 1, true, 0, false, 400);
						//itz[0].damage = 30;
						itz[0].owner = player.whoAmI;
						itz[0].friendly = true;
						itz[0].hostile = true;
						Main.projectile[itz[0].whoAmI].netUpdate = true;
						if (Main.netMode == 2 && itz[0].whoAmI < 200)
						{
							NetMessage.SendData(27, -1, -1, null, itz[0].whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}

						GeyserInABottle = false;
						player.velocity.Y = -15;
					}


			}

		}

		public override void ModifyHitByNPC (NPC npc, ref int damage, ref bool crit)
		{
			damage=OnHit(ref damage,crit,npc,null);
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref bool crit)
		{
			damage=OnHit(ref damage,crit,null,projectile);
		}

		public override bool PreHurt (bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{

			if (damageSource.SourceCustomReason == (player.name + " went Kamikaze, but failed to blow up enemies"))
			return true;

			if (anticipation > 0)
			{
				anticipation = (int)(anticipation / 2);
			}

		if (MurkyDepths)
			{
				damage = (int)(damage * 1.5);
			}



		if (Noselfdamage)
			{
			if (creeperexplosion<9800)
			if (damageSource.SourcePlayerIndex == player.whoAmI)
			return false;
			}

			if (damageSource.SourceProjectileType == ProjectileID.GeyserTrap && (GeyserInABottleActive))
			return false;

			if (creeperexplosion > 9795)
				return false;

		if (SpaceDiverset)
		{
		if (player.breath>player.breathMax-2){
		player.immune=true;
		player.immuneTime=45;
		damage*=3;

		int lifelost=(int)(((float)damage/(float)player.statLifeMax)*100f);
		sufficate-=lifelost;
		if (sufficate<0)
		sufficate=(int)MathHelper.Clamp(-lifelost,sufficate,0);

		return false;
		}}

			if (beefield > 0)
			{
				if (Main.rand.Next(0, 10) < 5)
					player.AddBuff(BuffID.Honey, 60 * 5);
			}

			return true;
		}

		private int OnHit(ref int damage, bool crit,NPC npc, Projectile projectile)
		{

			if (NPC.CountNPCS(mod.NPCType("Murk")) > 0 && Main.hardMode)
			{
				player.AddBuff(mod.BuffType("MurkyDepths"), damage * 5);
			}
			if (NPC.CountNPCS(mod.NPCType("TPD")) > 0 && Main.rand.Next(0,10)<(Main.expertMode ? 6 : 3))
			{
				player.AddBuff(BuffID.Electrified, 60+(damage * 4));
			}
			if (CirnoWings)
		{
				if (npc != null)
					if (npc.coldDamage)
						damage = (int)(damage*0.80);
				if (projectile != null)
					if (projectile.coldDamage)
						damage = (int)(damage * 0.80);


			}

		if (MisterCreeperset)
			{
				Vector2 myspeed = new Vector2(0, 0);
				if (npc != null)
				{
					myspeed = npc.Center - player.Center;
					myspeed.Normalize();
				}
				if (projectile != null)
				{
					myspeed = projectile.Center - player.Center;
					myspeed.Normalize();
				}
				myspeed *= 20f;
				int prog = Projectile.NewProjectile(player.Center.X, player.Center.Y, myspeed.X, myspeed.Y, ProjectileID.Grenade, 1000, 10f, player.whoAmI);
				IdgProjectile.Sync(prog);

			}

		if (SpaceDiverset)
		{
		int lifelost=(int)(((float)damage/(float)player.statLifeMax)*150f);
		sufficate-=lifelost;
		if (sufficate<0)
		sufficate=(int)MathHelper.Clamp(-lifelost,sufficate,0);
		}

			if (player.HeldItem.type==mod.ItemType("Powerjack"))
			{
			damage=(int)(damage*1.20);
			}

			if (npc!=null){
				if (npc.type==NPCID.BlazingWheel && npc.life==88){
				SGAnpcs nyx=npc.GetGlobalNPC<SGAnpcs>();
				damage=(int)(damage*7.5);
			}}


		return damage;
		}

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{

			if (Lockedin)
			{
			int q = 3;
			for (q = 0; q < 2; q++)
				{

					int dust = Dust.NewDust(new Vector2(Main.rand.Next(0,100)<50 ? Locked.X : Locked.X+Locked.Y,drawInfo.position.Y), player.width + 4, player.height + 4, DustID.AncientLight, 0f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].color=Main.hslToRgb((float)(Main.GlobalTime/50)%1, 0.9f, 0.65f);
					//Main.dust[dust].velocity *= 1.8f;
					//Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
				//r *= 0.1f;
				//g *= 0.2f;
				//b *= 0.7f;
				//fullBright = true;
			}
				if (MassiveBleeding){
					Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
					int dust = Dust.NewDust(new Vector2(drawInfo.position.X,drawInfo.position.Y)+randomcircle*8f, player.width + 4, player.height + 4, 5, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 30, default(Color), 1.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].color=Main.hslToRgb(0f, 0.5f, 0.35f);
					//Main.dust[dust].velocity *= 1.8f;
					//Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}

			if (thermalblaze)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<Dusts.HotDust>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
				r *= 0.1f;
				g *= 0.2f;
				b *= 0.7f;
				fullBright = true;
			}

			if (acidburn)
			{
				if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<Dusts.AcidDust>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
				r *= 0.1f;
				g *= 0.7f;
				b *= 0.1f;
				fullBright = true;
			}


			if (Blazewyrmset)
			{
				if (Main.rand.Next(8) == 0 && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<Dusts.HotDust>(), player.velocity.X * 0.8f, player.velocity.Y * 0.8f, 200, default(Color), 0.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
			}

		}

		public static readonly PlayerLayer WaveBeamArm = new PlayerLayer("SGAmod", "WaveBeamArm", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
			{
				Player drawPlayer = drawInfo.drawPlayer;
				SGAmod mod = SGAmod.Instance;
				SGAPlayer modply = drawPlayer.GetModPlayer<SGAPlayer>();

				//Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
				//better version, from Qwerty's Mod
				Color color = drawInfo.bodyColor;//;*drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawInfo.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawInfo.position.Y + (double)drawPlayer.height * 0.5) / 16, Microsoft.Xna.Framework.Color.White), 0f);

				Texture2D texture = mod.GetTexture("Items/Armors/BeamArms");
					int drawX = (int)((drawInfo.position.X+drawPlayer.bodyPosition.X+10) - Main.screenPosition.X);
					int drawY = (int)(((drawPlayer.bodyPosition.Y-4)+drawPlayer.MountedCenter.Y) - Main.screenPosition.Y);//gravDir 
					DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0,drawPlayer.bodyFrame.Y,drawPlayer.bodyFrame.Width,drawPlayer.bodyFrame.Height), color, (float)drawPlayer.fullRotation, new Vector2(drawPlayer.bodyFrame.Width/2,drawPlayer.bodyFrame.Height/2), 1f, (drawPlayer.direction==-1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (drawPlayer.gravDir>0 ? SpriteEffects.None : SpriteEffects.FlipVertically), 0);
					Main.playerDrawData.Add(data);
			});

		public static readonly PlayerLayer SpaceDiverTank = new PlayerLayer("SGAmod", "SpaceDiverTank", PlayerLayer.BackAcc, delegate (PlayerDrawInfo drawInfo)
			{
				Player drawPlayer = drawInfo.drawPlayer;
				SGAmod mod = SGAmod.Instance;
				SGAPlayer modply = drawPlayer.GetModPlayer<SGAPlayer>();

				//Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
				//better version, from Qwerty's Mod
				//Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawInfo.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawInfo.position.Y + (double)drawPlayer.height * 0.5) / 16, Microsoft.Xna.Framework.Color.White), 0f);

				Color color = drawInfo.bodyColor;//;*drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawInfo.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawInfo.position.Y + (double)drawPlayer.height * 0.5) / 16, Microsoft.Xna.Framework.Color.White), 0f);


				Texture2D texture = mod.GetTexture("Items/Armors/SpaceDiverTank");
					int drawX = (int)((drawInfo.position.X+drawPlayer.bodyPosition.X+10) - Main.screenPosition.X);
					int drawY = (int)(((drawPlayer.bodyPosition.Y-4)+drawPlayer.MountedCenter.Y) - Main.screenPosition.Y);//gravDir 
					DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0,drawPlayer.bodyFrame.Y,drawPlayer.bodyFrame.Width,drawPlayer.bodyFrame.Height), color, (float)drawPlayer.fullRotation, new Vector2(drawPlayer.bodyFrame.Width/2,drawPlayer.bodyFrame.Height/2), 1f, (drawPlayer.direction==-1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (drawPlayer.gravDir>0 ? SpriteEffects.None : SpriteEffects.FlipVertically), 0);
					Main.playerDrawData.Add(data);
			});

		public static readonly PlayerLayer BulletCount = new PlayerLayer("SGAmod", "BulletCount", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			if (drawPlayer == Main.LocalPlayer && Main.netMode < 2 && !drawPlayer.dead)
			{
				SGAmod mod = SGAmod.Instance;
				SGAPlayer modply = drawPlayer.GetModPlayer<SGAPlayer>();
				int maxclip;
				bool check = SGAmod.UsesClips.TryGetValue(drawPlayer.HeldItem.type, out maxclip);

				if (check)
				{
					Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
					Texture2D texture = mod.GetTexture("AmmoHud");
					int drawX = (int)((drawInfo.position.X + (drawPlayer.width / 2)) - Main.screenPosition.X);
					int drawY = (int)((drawInfo.position.Y + (drawInfo.drawPlayer.gravDir == -1 ? drawInfo.drawPlayer.height + 10 : -10)) - Main.screenPosition.Y);//gravDir 
					for (int q = 0; q < modply.ammoLeftInClip; q++)
					{
						DrawData data = new DrawData(texture, new Vector2((drawX - (q * texture.Width)) + (int)((maxclip * texture.Width) / 2), drawY), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
						Main.playerDrawData.Add(data);
					}
				}
			}
		});

		public static readonly PlayerLayer PlasmaCount = new PlayerLayer("SGAmod", "PlasmaCount", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
			{
				Player drawPlayer = drawInfo.drawPlayer;
				if (drawPlayer==Main.LocalPlayer && Main.netMode<2 && !drawPlayer.dead){
				SGAmod mod = SGAmod.Instance;
				SGAPlayer modply = drawPlayer.GetModPlayer<SGAPlayer>();
				int maxclip;
				bool check=SGAmod.UsesPlasma.TryGetValue(drawPlayer.HeldItem.type,out maxclip);

					if (check){
					Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
					Texture2D texture = mod.GetTexture("Items/PlasmaCell");
					int drawX = (int)((drawInfo.position.X+(drawPlayer.width/2)) - Main.screenPosition.X);
					int drawY = (int)((drawInfo.position.Y+(drawInfo.drawPlayer.gravDir == -1 ? drawInfo.drawPlayer.height + 20 : -20)) - Main.screenPosition.Y);//gravDir 

						float percent = ((float)modply.plasmaLeftInClip / (float)modply.plasmaLeftInClipMax);



						DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Color.Lerp(Color.Black,Color.DarkGray,0.25f), (float)Math.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
						DrawData data2 = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0,0, texture.Width,(int)((float)texture.Height*percent)), Color.White, (float)Math.PI, new Vector2(texture.Width/2,texture.Height/2), 1f, SpriteEffects.None, 0);
					Main.playerDrawData.Add(data);
					Main.playerDrawData.Add(data2);
					}
				}
			});


		public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
		{
			//drawInfo.
		}
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			//plasmaLeftInClip
			SGAPlayer sgaplayer = player.GetModPlayer<SGAPlayer>();

			if (SGAmod.UsesClips.ContainsKey(player.HeldItem.type)){
			int armLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsFront"));
			BulletCount.visible = true;
			layers.Insert(armLayer+1, BulletCount);
			}

			if (SGAmod.UsesPlasma.ContainsKey(player.HeldItem.type))
			{
				int armLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsFront"));
				PlasmaCount.visible = true;
				layers.Insert(armLayer + 1, PlasmaCount);
			}

			if (sgaplayer.SpaceDiverset)
			{
			int wingsLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("Wings"));
			int backacclayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("BackAcc"));
			if (SpaceDiverWings < 0.6f)
			layers.RemoveAt(wingsLayer);
			SpaceDiverTank.visible = true;
			layers.Insert(backacclayer, SpaceDiverTank);
			}

			if (player.HeldItem.type==mod.ItemType("WaveBeam"))
			{
			int armLayer2 = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("HandOnAcc"));
			WaveBeamArm.visible = true;
			layers.Insert(armLayer2, WaveBeamArm);
			}

		}

		public override void UpdateBiomeVisuals()
		{
			//TheProgrammer
			player.ManageSpecialBiomeVisuals("SGAmod:ProgramSky",(SGAmod.ProgramSkyAlpha>0f || NPC.CountNPCS(mod.NPCType("SPinky"))>0) ? true : false, player.Center);

		}

		public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();
			tag["gottf2"] = gottf2;
			tag["devpower"] = devpowerbool;
			tag["devpowerint"] = devpower;
			tag["Redmanastar"] = Redmanastar;

			tag["ZZZExpertiseCollectedZZZ"] = ExpertiseCollected;
			tag["ZZZExpertiseCollectedTotalZZZ"] = ExpertiseCollectedTotal;

			tag["enemyvaluesTotal"] = ExpertisePointsFromBosses.Count;
			for (int i = 0; i < ExpertisePointsFromBosses.Count; i += 1)
			{
				int value = ExpertisePointsFromBosses[i];
				string tagname = "enemyvalues" + ((string)i.ToString());
				tag[tagname] = value;
				string tagname2 = "enemyvaluesPoints" + ((string)i.ToString());
				tag[tagname2] = ExpertisePointsFromBossesPoints[i];
			}

			//ExpertisePointsFromBosses = null;
			//ExpertisePointsFromBossesPoints = null;
			return tag;
		}

		public override void Load(TagCompound tag)
		{
			ExpertiseCollected = 0;
			ExpertiseCollectedTotal = 0;
			gottf2 = tag.GetBool("gottf2");
			devpowerbool = tag.GetBool("devpower");
			devpower = tag.GetInt("devpowerint");
			Redmanastar = tag.GetInt("Redmanastar");

			ExpertiseCollected = tag.GetInt("ZZZExpertiseCollectedZZZ");
			int maybeExpertiseCollected = tag.GetInt("ZZZExpertiseCollectedTotalZZZ");
			ExpertiseCollectedTotal = maybeExpertiseCollected;

			if (maybeExpertiseCollected < 1)
			{

				GenerateNewBossList();
			}
			else
			{
				ExpertisePointsFromBosses = new List<int>();
				ExpertisePointsFromBossesPoints = new List<int>();
				int maxx = tag.GetInt("enemyvaluesTotal");
				for (int i = 0; i < maxx; i += 1)
				{
					int v1 = tag.GetInt("enemyvalues" + ((string)i.ToString()));
					int v2 = tag.GetInt("enemyvaluesPoints" + ((string)i.ToString()));

					ExpertisePointsFromBosses.Add(v1);
					ExpertisePointsFromBossesPoints.Add(v2);
				}

			}




		}

		public int? FindBossEXP(int npcid)
		{
			int? found = -1;


			if (npcid == NPCID.EaterofWorldsHead || npcid == NPCID.EaterofWorldsBody || npcid == NPCID.EaterofWorldsTail)
			{
				found = ExpertisePointsFromBosses.FindIndex(x => (x == NPCID.EaterofWorldsHead));
				goto gohere;
			}
			if (npcid == NPCID.GoblinSorcerer || npcid == NPCID.GoblinPeon || npcid == NPCID.GoblinThief || npcid == NPCID.GoblinWarrior || npcid == NPCID.GoblinArcher)
			{
				found = ExpertisePointsFromBosses.FindIndex(x => (x == NPCID.GoblinPeon));
				goto gohere;
			}
			found = ExpertisePointsFromBosses.FindIndex(x => x == npcid);

			gohere:

			return found;

		}

		public void DoExpertiseCheck(NPC npc)
		{

			if (!npc.active)
				return;
			if (npc.lifeMax < 100)
				return;
			if (ExpertisePointsFromBosses == null)
			{
				Main.NewText("The enemy list was somehow null... HOW?!");
				return;
			}

			if (ExpertisePointsFromBosses.Count<1)
				return;

			int npcid = npc.type;

			int? found = FindBossEXP(npcid);

			if (found!=null && found > -1)
			{

				int collected = ExpertisePointsFromBossesPoints[(int)found];
				ExpertiseCollected += collected;
				ExpertiseCollectedTotal += collected;

				CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.LimeGreen, "+" + collected + " Expertise", false, false);

				ExpertisePointsFromBosses.RemoveAt((int)found);
				ExpertisePointsFromBossesPoints.RemoveAt((int)found);

				int? findagain = FindBossEXP(npcid);

				if (findagain == null || findagain < 0)
				{
					if (Main.myPlayer == player.whoAmI)
						Main.NewText("You have gained Expertise! (you now have " + ExpertiseCollected + ")");


				}

			}


		}

		public void GenerateNewBossList()
		{
			ExpertisePointsFromBosses = new List<int>();
			ExpertisePointsFromBossesPoints = new List<int>();

			//Prehardmode Bosses (+2000 total)

			ExpertisePointsFromBosses.Add(ModContent.NPCType<CopperWraith>());
			ExpertisePointsFromBossesPoints.Add(100);

			ExpertisePointsFromBosses.Add(NPCID.KingSlime);
			ExpertisePointsFromBossesPoints.Add(100);

			ExpertisePointsFromBosses.Add(NPCID.EyeofCthulhu);
			ExpertisePointsFromBossesPoints.Add(100);

			for (int i = 0; i < 50; i += 1)
			{
				ExpertisePointsFromBosses.Add(NPCID.EaterofWorldsHead);
				ExpertisePointsFromBossesPoints.Add(3);
			}

			ExpertisePointsFromBosses.Add(NPCID.BrainofCthulhu);
			ExpertisePointsFromBossesPoints.Add(150);

			ExpertisePointsFromBosses.Add(NPCID.QueenBee);
			ExpertisePointsFromBossesPoints.Add(150);

			ExpertisePointsFromBosses.Add(ModContent.NPCType<SpiderQueen>());
			ExpertisePointsFromBossesPoints.Add(250);

			ExpertisePointsFromBosses.Add(NPCID.SkeletronHead);
			ExpertisePointsFromBossesPoints.Add(200);

			ExpertisePointsFromBosses.Add(ModContent.NPCType<Murk>());
			ExpertisePointsFromBossesPoints.Add(300);

			ExpertisePointsFromBosses.Add(NPCID.WallofFlesh);
			ExpertisePointsFromBossesPoints.Add(500);


			//Hardmode Bosses (+7000 total)

			ExpertisePointsFromBosses.Add(ModContent.NPCType<CobaltWraith>());
			ExpertisePointsFromBossesPoints.Add(300);
			ExpertisePointsFromBosses.Add(ModContent.NPCType<Cirno>());
			ExpertisePointsFromBossesPoints.Add(300);
			ExpertisePointsFromBosses.Add(NPCID.SkeletronPrime);
			ExpertisePointsFromBossesPoints.Add(300);
			ExpertisePointsFromBosses.Add(NPCID.TheDestroyer);
			ExpertisePointsFromBossesPoints.Add(300);
			ExpertisePointsFromBosses.Add(NPCID.Spazmatism);
			ExpertisePointsFromBossesPoints.Add(150);
			ExpertisePointsFromBosses.Add(NPCID.Retinazer);
			ExpertisePointsFromBossesPoints.Add(150);
			ExpertisePointsFromBosses.Add(ModContent.NPCType<SharkvernHead>());
			ExpertisePointsFromBossesPoints.Add(500);
			ExpertisePointsFromBosses.Add(NPCID.Plantera);
			ExpertisePointsFromBossesPoints.Add(500);
			ExpertisePointsFromBosses.Add(ModContent.NPCType<Cratrosity>());
			ExpertisePointsFromBossesPoints.Add(700);
			ExpertisePointsFromBosses.Add(NPCID.Golem);
			ExpertisePointsFromBossesPoints.Add(500);
			ExpertisePointsFromBosses.Add(NPCID.DD2Betsy);
			ExpertisePointsFromBossesPoints.Add(700);
			ExpertisePointsFromBosses.Add(ModContent.NPCType<TPD>());
			ExpertisePointsFromBossesPoints.Add(800);
			ExpertisePointsFromBosses.Add(ModContent.NPCType<Harbinger>());
			ExpertisePointsFromBossesPoints.Add(800);
			ExpertisePointsFromBosses.Add(NPCID.MoonLordCore);
			ExpertisePointsFromBossesPoints.Add(1000);

			//Post-moonlord Bosses (+3000 total)

			ExpertisePointsFromBosses.Add(ModContent.NPCType<LuminiteWraith>());
			ExpertisePointsFromBossesPoints.Add(1500);
			ExpertisePointsFromBosses.Add(ModContent.NPCType<SPinky>());
			ExpertisePointsFromBossesPoints.Add(1500);


			//Not bosses (+500 total)
			for (int i = 0; i < 75; i += 1)
			{
				ExpertisePointsFromBosses.Add(NPCID.GoblinPeon);
				ExpertisePointsFromBossesPoints.Add(2);
			}

			ExpertisePointsFromBosses.Add(ModContent.NPCType<TidalElemental>());
			ExpertisePointsFromBossesPoints.Add(75);

			ExpertisePointsFromBosses.Add(NPCID.Tim);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.DoctorBones);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.Nymph);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.TheGroom);
			ExpertisePointsFromBossesPoints.Add(25);
			ExpertisePointsFromBosses.Add(NPCID.TheBride);
			ExpertisePointsFromBossesPoints.Add(25);
			ExpertisePointsFromBosses.Add(NPCID.DD2DarkMageT1);
			ExpertisePointsFromBossesPoints.Add(75);






			//Not bosses: Hardmode (+2500 total)
			for (int i = 0; i < 2; i += 1)//800
			{
				ExpertisePointsFromBosses.Add(NPCID.GoblinSummoner);
				ExpertisePointsFromBossesPoints.Add(50);
				ExpertisePointsFromBosses.Add(NPCID.Mothron);
				ExpertisePointsFromBossesPoints.Add(75);
				ExpertisePointsFromBosses.Add(NPCID.Mimic);
				ExpertisePointsFromBossesPoints.Add(25);
				ExpertisePointsFromBosses.Add(NPCID.MartianSaucerCore);
				ExpertisePointsFromBossesPoints.Add(150);
				ExpertisePointsFromBosses.Add(NPCID.PirateShip);
				ExpertisePointsFromBossesPoints.Add(50);
				ExpertisePointsFromBosses.Add(NPCID.PirateCaptain);
				ExpertisePointsFromBossesPoints.Add(50);
			}
			//500
			ExpertisePointsFromBosses.Add(NPCID.MartianProbe);
			ExpertisePointsFromBossesPoints.Add(75);
			ExpertisePointsFromBosses.Add(NPCID.Medusa);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.Clown);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.RuneWizard);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.RainbowSlime);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.Moth);
			ExpertisePointsFromBossesPoints.Add(75);
			ExpertisePointsFromBosses.Add(NPCID.DD2OgreT2);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.IceGolem);
			ExpertisePointsFromBossesPoints.Add(50);
			ExpertisePointsFromBosses.Add(NPCID.SandElemental);
			ExpertisePointsFromBossesPoints.Add(50);
			for (int i = 0; i < 3; i += 1)//1200
			{
				ExpertisePointsFromBosses.Add(NPCID.MourningWood);
				ExpertisePointsFromBossesPoints.Add(50);
				ExpertisePointsFromBosses.Add(NPCID.Pumpking);
				ExpertisePointsFromBossesPoints.Add(100);
				ExpertisePointsFromBosses.Add(NPCID.Everscream);
				ExpertisePointsFromBossesPoints.Add(50);
				ExpertisePointsFromBosses.Add(NPCID.SantaNK1);
				ExpertisePointsFromBossesPoints.Add(75);
				ExpertisePointsFromBosses.Add(NPCID.IceQueen);
				ExpertisePointsFromBossesPoints.Add(125);
			}


			for (int i = 0; i < 100; i += 1)
			{
				ExpertisePointsFromBosses.Add(NPCID.CultistArcherWhite);
				ExpertisePointsFromBossesPoints.Add(1);
			}

			//Tally-15000 Expertise

		}


	}
}