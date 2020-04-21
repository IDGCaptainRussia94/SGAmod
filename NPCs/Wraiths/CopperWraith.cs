using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.NPCs.Wraiths
{
	public class CopperArmorPiece: ModNPC
	{


		public int armortype=ItemID.CopperChainmail;
		public int attachedID=0;
		public int CoreID=0;
		public float friction=0.75f;
		public float speed=0.3f;
		public string attachedType="CopperWraith";
		public bool selfdestruct;

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(attachedID);
			//Texture2D Texz = new Texture2D(Main.graphics.GraphicsDevice, width, height, false, SurfaceFormat.Color);
			//Texz.SetData
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			attachedID = reader.Read();
		}

		public virtual void ArmorMalfunction()
        {  
        selfdestruct=true;
        CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
        npc.velocity=new Vector2(Main.rand.Next(-5,5),Main.rand.Next(-5,5));
        npc.StrikeNPCNoInteraction((int)(npc.lifeMax*0.15f), 0f, 0);
        }

		public override void NPCLoot()
		{
		if (Main.rand.Next(0,3)<2)
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CopperWraithNotch"));
		}

		public override bool CheckActive()
		{
        CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		return (!myowner.active);
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Armor Piece");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + armortype; }
		}
		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 24;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = -1;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
		}

		public override bool PreAI()
		{
			if (Hellion.Hellion.GetHellion() != null)
			{
				if (Hellion.Hellion.GetHellion().npc.ai[1] > 999)
					npc.ai[0] = 0;

			}

			return true;
		}
		public override void AI()
		{
		int npctype=mod.NPCType(attachedType);
		if (NPC.CountNPCS(npctype)>0){
		NPC myowner=Main.npc[NPC.FindFirstNPC(npctype)];
		}else{npc.active=false;}

		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 0f;
			return null;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.expertMode)
			{
				double damagemul = 1.0;
				if (projectile.penetrate > 1 || projectile.penetrate < 0)
					damagemul = 0.5;
				base.OnHitByProjectile(projectile, (int)(damage * damagemul), knockback, crit);
			}

		}

	}


	public class CopperArmorChainmail: CopperArmorPiece
	{
		public int armortype=ItemID.CopperChainmail;

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = -1;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chainmail");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + (SGAWorld.WorldIsTin ? ItemID.TinChainmail : ItemID.CopperChainmail); }
		}
		public override void AI()
		{
		CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
		NPC myowner=Main.npc[myself.attachedID];
		if (myowner.active==false){
		myself.ArmorMalfunction();
		}else{
		npc.velocity=npc.velocity+(myowner.Center+new Vector2((float)npc.ai[1],(float)npc.ai[2])-npc.Center)*(myself.speed);
		npc.velocity=npc.velocity*myself.friction;
		npc.rotation=(float)npc.velocity.X*0.1f;
		//npc.position=myowner.position;
		npc.timeLeft=999;
		}


		}

	}

	public class CopperArmorHelmet: CopperArmorChainmail
	{
		public int armortype=ItemID.CopperHelmet;

		public override void SetDefaults()
		{
			base.SetDefaults();

		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Helmet");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + (SGAWorld.WorldIsTin ? ItemID.TinHelmet : ItemID.CopperHelmet); }
		}

	}

		public class CopperArmorGreaves: CopperArmorChainmail
	{
		public int armortype=ItemID.CopperGreaves;

		public override void SetDefaults()
		{
			base.SetDefaults();
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greaves");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + (SGAWorld.WorldIsTin ? ItemID.TinGreaves : ItemID.CopperGreaves); }
		}

	} 

		public class CopperArmorBow: CopperArmorPiece
	{
		public int armortype=ItemID.CopperBow;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Bow");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + (SGAWorld.WorldIsTin ? ItemID.TinBow : ItemID.CopperBow); }
		}
		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 24;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 400;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = -1;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
		}
		public override void AI()
		{
			float speedmulti = 0.75f;
			if (!Main.expertMode)
				speedmulti = 0.5f;
			if (SGAWorld.NightmareHardcore > 0)
				speedmulti = 1f;
			CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		if (myowner.active==false){
		myself.ArmorMalfunction();
		}else{
			Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
				if (!P.active || P.dead)
				{
					npc.active=false;
					Main.npc[(int)npc.ai[1]].active=false;
				}
				}else{
		npc.ai[0]+=1;
		Vector2 itt=(myowner.Center-npc.Center+new Vector2(npc.ai[1]*npc.spriteDirection,npc.ai[2]));
		if (npc.ai[0]%1500>1250){
		itt=(P.position-npc.position+new Vector2(3f*npc.ai[1]*npc.spriteDirection,npc.ai[2]*2f));
		}
		float locspeed=0.25f* speedmulti;
		if (npc.ai[0]%900>550){
		Vector2 cas=new Vector2(npc.position.X-P.position.X,npc.position.Y-P.position.Y);
		double dist=cas.Length();
		float rotation = (float)Math.Atan2(npc.position.Y - (P.position.Y - (new Vector2(0,(float)(dist*0.15f))).Y + (P.height * 0.5f)), npc.position.X - (P.position.X + (P.width * 0.5f)));
		npc.rotation=rotation;//npc.rotation+((rotation-npc.rotation)*0.1f);
		npc.velocity=npc.velocity*0.86f;
		if (npc.ai[0]%50==0 && npc.ai[0]%900>650){


		List<Projectile> one = Idglib.Shattershots(npc.Center,npc.Center+new Vector2(-15*npc.spriteDirection,0),new Vector2(0,0),Math.Abs(npc.ai[1])<18 ? ProjectileID.DD2BetsyArrow : (SGAWorld.NightmareHardcore>0 ? mod.ProjectileType("UnmanedArrow") : ProjectileID.WoodenArrowHostile), 7,12,0,1,true,(Main.rand.Next(-100,100)*0.000f)-npc.rotation,true,300);
							one[0].hostile = true;
							one[0].friendly = false;
							one[0].localAI[0] = P.whoAmI;
							one[0].netUpdate = true;
						}
		npc.spriteDirection=1;
		}else{
		if (Math.Abs(npc.velocity.X)>2){ npc.spriteDirection=npc.velocity.X>0 ? -1 : 1; }
		npc.rotation=(float)npc.velocity.X*0.09f;
		locspeed=0.5f* speedmulti;
		}
		npc.velocity=npc.velocity*0.96f;
		itt.Normalize();
		npc.velocity=npc.velocity+(itt*locspeed);
		npc.timeLeft=999;
		}

		}
	}


	}

	public class CopperArmorSword: CopperArmorPiece
	{
		public int armortype=ItemID.CopperBroadsword;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Sword");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + (SGAWorld.WorldIsTin ? ItemID.TinShortsword : ItemID.CopperShortsword); }
		}
		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 24;
			npc.damage = 5;
			npc.defDamage = 5;
			npc.defense = 0;
			npc.lifeMax = 400;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = -1;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
		}
		public override void AI()
		{
			float speedmulti = 0.75f;
			if (!Main.expertMode)
				speedmulti = 0.5f;
			if (SGAWorld.NightmareHardcore>0)
				speedmulti = 1f;
			CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		if (myowner.active==false){
		myself.ArmorMalfunction();
		}else{
		Player P = Main.player[myowner.target];
		npc.ai[0]+=1;
		npc.spriteDirection=npc.velocity.X>0 ? -1 : 1;
		Vector2 itt=(myowner.Center-npc.Center+new Vector2(npc.ai[1]*npc.spriteDirection,npc.ai[2]));
		float locspeed=0.25f* speedmulti;
		if (npc.ai[0]%600>350){

		npc.damage = (int)npc.defDamage*3;
		itt=itt=(P.position-npc.position+new Vector2(npc.ai[1]*npc.spriteDirection,-8));

					if (npc.ai[0] % 180 == 0 && SGAWorld.NightmareHardcore>0)
					{
						Vector2 zxx = itt;
						zxx += P.velocity * 3f;
						zxx.Normalize();
						npc.velocity += zxx*18;
					}
		npc.rotation=npc.rotation+(0.65f*npc.spriteDirection);
		}else{
		npc.damage = (int)npc.defDamage;
		if (npc.ai[0]%300<60){
		locspeed=2.5f* speedmulti;
		npc.velocity=npc.velocity*0.92f;
		}
		npc.rotation=(float)npc.velocity.X*0.09f;
		locspeed=0.5f* speedmulti;
		}
		npc.velocity=npc.velocity*0.96f;
		itt.Normalize();
		npc.velocity=npc.velocity+(itt*locspeed);
		npc.timeLeft=999;
		}


		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode || Main.rand.Next(2) == 0)
			{
				player.AddBuff(BuffID.Bleeding, 60*10, true);
			}
		}


	}

























	public class CopperWraith : ModNPC
	{
		public int level=0;
		public Vector2 OffsetPoints=new Vector2(0f,0f);
		public float speed=0.2f;
		public bool coreonlymode=false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Wraith");
			Main.npcFrameCount[npc.type] = 1;
			NPCID.Sets.NeedsExpertScaling[npc.type] = true;
		}
		public override void SetDefaults()
		{
			npc.width = 16;
			npc.height = 16;
			npc.damage = 10;
			npc.defense = 0;
			npc.lifeMax = 400;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.knockBackResist = 0.05f;
			npc.aiStyle = -1;
			npc.boss=true;
			music=MusicID.Boss5;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.value = Item.buyPrice(0,2,0,0);
		}
				public override string Texture
		{
			get { return("SGAmod/NPCs/TPD");}
		}

				public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if ((projectile.penetrate<1 || projectile.penetrate > 1) && projectile.damage>(GetType()==typeof(CobaltWraith) ? 20 : 30))
			{
				damage = (int)((float)damage * 0.25f);

			}
			else
			{
				//if (GetType() == typeof(CopperWraith))
				//{
					if (GetType() == typeof(CopperWraith))
					damage = (int)(damage*1.5);
					if (projectile.penetrate < 2 && projectile.penetrate > -1)
					crit = true;
				//}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
		potionType=ItemID.LesserHealingPotion;
		}

		public override void NPCLoot()
		{
			List<int> types=new List<int>();

			/*WorldGen.CopperTierOre = 7;
			WorldGen.IronTierOre = 6;
			WorldGen.SilverTierOre = 9;
			WorldGen.GoldTierOre = 8;*/

			int shardtype = mod.ItemType("WraithFragment");
			if (SGAWorld.WorldIsTin)
			{
				shardtype = mod.ItemType("WraithFragment2");
				//npc.GivenName = "Tin Wraith";
			}

			types.Insert(types.Count, shardtype);
			types.Insert(types.Count,SGAmod.WorldOres[0,WorldGen.CopperTierOre==TileID.Copper ? 1 : 0]); types.Insert(types.Count,SGAmod.WorldOres[0,WorldGen.CopperTierOre== TileID.Copper ? 1 : 0]);
			types.Insert(types.Count,SGAmod.WorldOres[1,WorldGen.IronTierOre== TileID.Iron ? 1 : 0]); types.Insert(types.Count,SGAmod.WorldOres[1,WorldGen.IronTierOre== TileID.Iron ? 1 : 0]);
			types.Insert(types.Count,SGAmod.WorldOres[2,WorldGen.SilverTierOre== TileID.Silver ? 1 : 0]); types.Insert(types.Count,SGAmod.WorldOres[2,WorldGen.SilverTierOre== TileID.Silver ? 1 : 0]);
			types.Insert(types.Count,SGAmod.WorldOres[3,WorldGen.GoldTierOre== TileID.Gold ? 1 : 0]);


		if (shardtype>0){
		for (int f = 0; f < (Main.expertMode ? 40 : 25); f=f+1){
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, shardtype);
		}}

		for (int f = 0; f < (Main.expertMode ? 200 : 100); f=f+1){
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, types[Main.rand.Next(0,types.Count)]);
		}

		Achivements.SGAAchivements.UnlockAchivement("Copper Wraith", Main.LocalPlayer);
		if (SGAWorld.downedWraiths<1){SGAWorld.downedWraiths=1;
		Idglib.Chat("You have regained the knowledge to craft a furnace!",150, 150, 70);
		}
        }

        public virtual void SpawnMoreExpert()
        {
		int newguy=NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("CopperArmorSword")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[0]=300f; newguy2.ai[1]=-64f; newguy2.ai[2]=48f; newguy2.lifeMax=(int)npc.lifeMax*1; newguy2.life=(int)(npc.lifeMax*(1)); newguy2.knockBackResist=0.9f; newguy2.netUpdate = true;
		newguy =NPC.NewNPC((int)npc.position.X-400, (int)npc.position.Y, mod.NPCType("CopperArmorSword")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[1]=64f; newguy2.ai[2]=48f; newguy2.lifeMax=(int)npc.lifeMax*1; newguy2.life=(int)(npc.lifeMax*(1)); newguy2.knockBackResist=0.9f; newguy2.netUpdate = true;
			newguy =NPC.NewNPC((int)npc.position.X-400, (int)npc.position.Y, mod.NPCType("CopperArmorBow")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[0]=450f; newguy2.ai[1]=16f; newguy2.ai[2]=-64f; newguy2.lifeMax=(int)npc.lifeMax*1; newguy2.life=(int)(npc.lifeMax*(1)); newguy2.knockBackResist=0.2f; newguy2.netUpdate = true;
			newguy =NPC.NewNPC((int)npc.position.X+400, (int)npc.position.Y, mod.NPCType("CopperArmorBow")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[1]=-16f; newguy2.ai[2]=-64f; newguy2.lifeMax=(int)npc.lifeMax*1; newguy2.life=(int)(npc.lifeMax*(1)); newguy2.knockBackResist=0.2f; newguy2.netUpdate = true;
			newguy =NPC.NewNPC((int)npc.position.X+400, (int)npc.position.Y, mod.NPCType(level>0 ? "CobaltArmorChainmail" : "CopperArmorChainmail")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.lifeMax=(int)(npc.lifeMax*1.5f); newguy2.life=(int)(npc.lifeMax*(1.5f)); newguy2.knockBackResist=1f; newguy2.netUpdate = true;
		}
		public override void AI()
		{

			float speedmulti = 0.75f;
			if (GetType() == typeof(CopperWraith))
			{
				if (!Main.expertMode)
					speedmulti = 0.5f;
				if (SGAWorld.NightmareHardcore > 0)
					speedmulti = 1f;

			}

			if (GetType() == typeof(CobaltWraith))
			{
				speedmulti = 1.25f;
				if (!Main.expertMode)
					speedmulti = 1f;
				if (SGAWorld.NightmareHardcore > 0)
					speedmulti = 1.4f;

			}

			//npc.netUpdate = true;
			Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
				if (!P.active || P.dead)
				{
					npc.active=false;
					Main.npc[(int)npc.ai[1]].active=false;
				}
				}else{

		int expert=0;
		if(Main.expertMode){
		expert=1;
		}
		npc.ai[0]+=1;
		if (npc.type==mod.NPCType("CobaltWraith")){level=1;}
		npc.defense=(int)(((NPC.CountNPCS(mod.NPCType("CopperArmorChainmail"))*6)+(NPC.CountNPCS(mod.NPCType("CopperArmorGreaves")))*3+(NPC.CountNPCS(mod.NPCType("CopperArmorHelmet"))*4))*((expert+1)*0.4f));
		if (NPC.CountNPCS(mod.NPCType("CopperArmorChainmail"))+NPC.CountNPCS(mod.NPCType("CobaltArmorChainmail"))<1){ if (npc.ai[0]>50){
		npc.ai[0]=-500;
		}}
		if (npc.life<npc.lifeMax*0.5f){
		if (expert>0){
		if (npc.ai[0]>-1500 && npc.ai[1]==0){ npc.ai[0]=-2000; npc.ai[1]=1;}
		if (npc.ai[0]==-1850){
		List<Projectile> itz=Idglib.Shattershots(npc.position,npc.position+new Vector2(0,200),new Vector2(0,0),ProjectileID.DD2BetsyArrow,10,5,360,20,true,0,true,150);
		for (int f = 0; f < 20; f=f+1){
		itz[f].aiStyle=0;
		itz[f].rotation = -((float)Math.Atan2((double)itz[f].velocity.Y, (double)itz[f].velocity.X));
		}
		SpawnMoreExpert();
		}if (npc.ai[0]==-1800){
		npc.ai[0]=0;
		}
		}
		}
		if ((npc.ai[0]==1 || npc.ai[0]==-1) && npc.ai[1]<1){
			float mul=(npc.ai[0]<0 ? 0.10f : 0.45f);
		if (NPC.CountNPCS(mod.NPCType("CopperArmorChainmail"))<1){ int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType(level>0 ? "CobaltArmorChainmail" : "CopperArmorChainmail")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.lifeMax=(int)npc.lifeMax*2; newguy2.life=(int)(npc.lifeMax*(2*mul)); newguy2.knockBackResist=0.85f; newguy2.netUpdate = true; }
		if (NPC.CountNPCS(mod.NPCType("CopperArmorSword"))<1){ int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType(level>0 ? "CobaltArmorSword" : "CopperArmorSword")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[1]=-32f; newguy2.ai[2]=-16f; newguy2.lifeMax=(int)(npc.lifeMax*1f); newguy2.life=(int)(npc.lifeMax*(1f)); newguy2.knockBackResist=0.75f; newguy2.netUpdate = true; }
		if (NPC.CountNPCS(mod.NPCType("CopperArmorHelmet"))<1){ int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType(level>0 ? "CobaltArmorHelmet" : "CopperArmorHelmet")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[2]=-12f; newguy2.lifeMax=(int)(npc.lifeMax*1.5f); newguy2.life=(int)(npc.lifeMax*(1.5f*mul)); newguy2.knockBackResist=0.8f; newguy2.netUpdate = true; }
		if (NPC.CountNPCS(mod.NPCType("CopperArmorGreaves"))<1){ int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType(level>0 ? "CobaltArmorGreaves" : "CopperArmorGreaves")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[2]=12f; newguy2.lifeMax=(int)(npc.lifeMax*1.5f); newguy2.life=(int)(npc.lifeMax*(1.5f*mul)); newguy2.knockBackResist=0.8f; newguy2.netUpdate = true; }
		if (NPC.CountNPCS(mod.NPCType("CopperArmorBow"))<1){ int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType(level>0 ? "CobaltArmorSword" : "CopperArmorBow")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.ai[1]=32f; newguy2.ai[2]=-16f; newguy2.lifeMax=(int)(npc.lifeMax*(1f)); newguy2.life=(int)(npc.lifeMax*(1f)); newguy2.knockBackResist=0.75f; newguy2.netUpdate = true; }
		}if (npc.ai[0]>1){

		if (npc.ai[0]%600<250){
		Vector2 itt=((P.Center+OffsetPoints)-npc.position); itt.Normalize();
		npc.velocity=npc.velocity+(itt*(speed* speedmulti));
		}
		npc.velocity=npc.velocity*0.98f;

		}if (npc.ai[0]<0 && npc.ai[0]>-2000){
		if (npc.ai[0]%110<-95){
		npc.velocity=new Vector2(Main.rand.Next(-20,20),0);
		if (npc.ai[0]%10==0){
		Idglib.Shattershots(npc.position,P.position,new Vector2(P.width,P.height),100,10,8,0,1,true,0,true,300);
		}
		}else{
		Vector2 itt=((P.Center+OffsetPoints+new Vector2(0,-250))-npc.position); itt.Normalize();
		float speedz=(float)level+0.45f;
		npc.velocity=npc.velocity+((itt*speedz)* speedmulti);
		}
		float fric=0.96f+((float)level*0.01f);
		npc.velocity=npc.velocity*fric;
		}


		}
		}




		public override bool CanHitPlayer(Player ply, ref int cooldownSlot){
			return true;
		}
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			if (CanBeHitByPlayer(player)){return false;
			}else{
			return base.CanBeHitByItem(player,item);}
		}
		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			//if (Main.npc[projectile.owner]!=null){return false;}else{
			//return CanBeHitByPlayer(Main.player[projectile.owner]);
			if (CanBeHitByPlayer(Main.player[projectile.owner])){return false;
			}else{
			return base.CanBeHitByProjectile(projectile);}
		}
		private bool CanBeHitByPlayer(Player P){
		//int npctype=mod.NPCType("CopperArmorChainmail");
		return npc.ai[0]<-700 ? true : false;
	}


public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
{
int npctype=mod.NPCType("CopperArmorChainmail");
Vector2 drawPos = npc.Center - Main.screenPosition;
Color glowingcolors1 = Main.hslToRgb((float)lightColor.R*0.08f,(float)lightColor.G*0.08f,(float)lightColor.B*0.08f);
Texture2D texture = mod.GetTexture("NPCs/TPD");
spriteBatch.Draw(texture, drawPos, null, glowingcolors1, npc.spriteDirection+(npc.ai[0]*0.4f), new Vector2(16,16),new Vector2(Main.rand.Next(1,20)/17f,Main.rand.Next(1,20)/17f), SpriteEffects.None, 0f);
if (npc.ai[0]>0){return false;}else{
//Vector2 drawPos = npc.Center-Main.screenPosition;
for (int a = 0; a < 30; a=a+1){
spriteBatch.Draw(texture, drawPos, null, glowingcolors1, npc.spriteDirection+(npc.ai[0]*(1-(a%2)*2))*0.4f, new Vector2(16,16),new Vector2(Main.rand.Next(1,100)/17f,Main.rand.Next(1,20)/17f), SpriteEffects.None, 0f);
}
return true;
}}


	}

}

