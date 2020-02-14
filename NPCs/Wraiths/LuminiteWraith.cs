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

		/*public int armortype=ItemID.CopperChainmail;
		public int attachedID=0;
		public int CoreID=0;
		public float friction=0.75f;
		public float speed=0.3f;
		public string attachedType="CopperWraith";*/

	public class LuminiteWraithTarget: ModNPC
	{

		public bool spawnedbosses = false;
		public float delay=0f;

		public override void SetDefaults()
		{
			npc.width = 2;
			npc.height = 2;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 1000000;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = -1;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.dontTakeDamage=true;
			npc.immortal=true;
		}

		public override bool CheckActive()
		{
NPC master=Main.npc[(int)npc.ai[1]];
return (!master.active);


		}

		public override string Texture
		{
			get { return("SGAmod/NPCs/TPD");}
		}

public override void AI(){

NPC master=Main.npc[(int)npc.ai[1]];
if (master.active==false){
npc.active=false;
}else{
npc.timeLeft=99999;
Player P = Main.player[npc.target];
if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
{
npc.TargetClosest(false);
P = Main.player[npc.target];
}
				NPC realmaster = Main.npc[(int)npc.ai[2]];


				LuminiteWraithArmor myrealmaster=master.modNPC as LuminiteWraithArmor;
Vector2 theloc=myrealmaster.mytargetis;
myrealmaster.whereisthetarget=npc.Center;

					//int dust2 = Dust.NewDust(npc.Center+new Vector2(-16,-16), 32,32, 247, 0f, 0f, 100, default(Color), 5f);
					//int dust2 = Dust.NewDust(npc.Center+new Vector2(-16,-16), 32,32, 278, 0f, 0f, 100, default(Color), 5f);
					int dust2 = Dust.NewDust(npc.Center+new Vector2(-16,-16), 32,32, 185, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dust2].noGravity = true;
					Main.dust[dust2].color=Main.hslToRgb((float)(Main.GlobalTime/5)%1, 0.9f, 1f);
					Main.dust[dust2].color.A=10;
					Main.dust[dust2].velocity.X = npc.velocity.X/3 + (Main.rand.Next(-50, 51) * 0.005f);
					Main.dust[dust2].velocity.Y = npc.velocity.Y/3 + (Main.rand.Next(-50, 51) * 0.005f);
					Main.dust[dust2].alpha = 100;;

Vector2 newdist=(master.Center+theloc)-npc.Center;
if (npc.ai[0]>-1 && master.dontTakeDamage==false){
if (npc.ai[0]%1800>1200){

				int dustType = 43;//Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.Center+new Vector2(-8,-8), 16,16, dustType);//,0,5,0,new Color=Main.hslToRgb((float)(npc.ai[0]/300)%1, 1f, 0.9f),1f);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.005f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.005f;
				dust.scale = 40f + (Main.rand.Next(-30, 31) * 0.01f);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.color=Main.hslToRgb((float)(npc.ai[0]/300)%1, 1f, 1f);

if (npc.ai[0]%1800>1350 && npc.ai[0]%6==0 && npc.ai[0]%1800<1700){
List<Projectile> itz=Idglib.Shattershots(npc.Center,npc.Center,new Vector2(0f,8f),ProjectileID.PhantasmalBolt,18,(float)Main.rand.Next(200,240)*0.1f,30,1,true,npc.ai[0]/300f,false,250);
}
}else{


						if (npc.ai[0]%1300>1000 || master.ai[3]<0)
newdist=(P.Center+theloc)-npc.Center;
//if (master.ai[3]<0f)
//npc.Center+=((P.Center+theloc)-npc.Center)/1.5f;
Vector2 orgin=newdist;
newdist.Normalize();
npc.velocity+=newdist/4f;
if (npc.ai[0]%600>450)
npc.velocity+=orgin/800f;

//if (realmaster.life<(int)(realmaster.lifeMax*0.75) && delay==10f)
//{
//npc.ai[0]=-1000;
//}


if (npc.ai[0]%300>200){
npc.velocity/=2f;
if (npc.ai[0]%300>260 && npc.ai[0]%10==0){
List<Projectile> itz=Idglib.Shattershots(npc.Center,P.position,new Vector2(P.width,P.height),ProjectileID.PhantasmalBolt,18,(float)Main.rand.Next(160,200)*0.1f,30,1,true,(float)Main.rand.Next(-200,200)/2000f,false,250);
}}
}






}

if (npc.ai[0]>-800 && npc.ai[0]<-600){
npc.ai[0] = -200;

int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType("BossCopperWraith"));
NPC newguy2=Main.npc[newguy];
newguy2.life*=15;
newguy2.lifeMax*=15;
CopperWraith created=newguy2.modNPC as CopperWraith;
created.OffsetPoints=theloc;
created.speed=1f;
//if (delay>11f){
//created.coreonlymode=true;
//}
newguy2.damage=50;



}



npc.velocity/=1.025f;
npc.rotation+=0.2f;

npc.ai[0]+=1f;
}

}


public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
{
	float Scale=0f;

Color glowingcolors1 = Main.hslToRgb((float)(npc.ai[0]/300)%1, 1f, 0.9f);
Color glowingcolors2 = Main.hslToRgb((float)(-npc.ai[0]/300)%1, 1f, 0.9f-(float)Scale/30);
Color glowingcolors3 = Main.hslToRgb((float)(npc.ai[0]/80)%1, 1f, 0.9f-(float)Scale/90);
Texture2D texture = mod.GetTexture("NPCs/TPD");
Texture2D texture2 = mod.GetTexture("NPCs/TPD1");
Texture2D texture3 = mod.GetTexture("NPCs/TPD2");
//Vector2 drawPos = npc.Center-Main.screenPosition;
Vector2 drawPos = npc.Center - Main.screenPosition;
spriteBatch.Draw(texture2, drawPos, null, glowingcolors1, npc.rotation, new Vector2(16,16),new Vector2(2,2), SpriteEffects.None, 0f);
spriteBatch.Draw(texture3, drawPos, null, glowingcolors2, -npc.rotation, new Vector2(16,16),new Vector2(1,1)*(1), SpriteEffects.None, 0f);
return false;
}

}
	
	public class BossCopperWraith : CopperWraith
	{

		public override void SetDefaults()
		{
			npc.width = 16;
			npc.height = 16;
			npc.damage = 10;
			npc.defense = 20;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.knockBackResist = 0.05f;
			npc.aiStyle = -1;
			npc.boss=false;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.value = 0f;
		}

		public override bool CheckActive()
		{
			return NPC.CountNPCS(mod.NPCType("LuminiteWraith"))<1;
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Copper Wraiths MK2");
		}

		public override void AI()
		{
			npc.localAI[3] += 1;
			if (npc.localAI[3] % 10 == 0 && npc.localAI[3] % 500 < 180)
			{
					Vector2 Centerhere = (Main.player[npc.target].Center - npc.Center);
					List<Projectile> itz = Idglib.Shattershots(npc.Center, Main.player[npc.target].Center, new Vector2(0, 0), ProjectileID.SaucerMissile, 25, -15, (npc.localAI[3] % 500) / 2f, 2, false, 0, false, 800);
					itz[0].localAI[1] = -(npc.localAI[3] % 500);
					itz[1].localAI[1] = -(npc.localAI[3] % 500);


			}
			
			base.AI();

		}

		public override void NPCLoot()
		{
		//null
		}

	}



















	public class LuminiteWraith : ModNPC
	{
		public int level=0;
		public Vector2 dodge=new Vector2(0f,0f);
		public int warninglevel=0;
		public int warninglevel2=0;
		public int quitermode=0;
		public int fighttversion=0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Wraith");
			Main.npcFrameCount[npc.type] = 1;
			NPCID.Sets.NeedsExpertScaling[npc.type] = true;
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
        potionType=ItemID.GreaterHealingPotion;
        }

		public override void NPCLoot()
		{
			List<int> types=new List<int>();
			types.Insert(types.Count,ItemID.LunarOre); types.Insert(types.Count,ItemID.LunarOre); types.Insert(types.Count,ItemID.LunarOre); types.Insert(types.Count,ItemID.LunarOre);
			types.Insert(types.Count,ItemID.LunarBar);
		for (int b = 0; b < (3); b=b+1){
		if (SGAWorld.downedWraiths > 2)
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LuminiteWraithNotch"));
		}
		for (int f = 0; f < (Main.expertMode ? 100 : 50); f=f+1){
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, types[Main.rand.Next(0,types.Count)]);
		}
		if (Main.expertMode)
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CosmicFragment"));
		if (SGAWorld.downedWraiths<4){SGAWorld.downedWraiths=4;
		Idglib.Chat("You have regained your knowledge to make Luminite Bars!",150, 150, 70);
		}
        }

		public override void SetDefaults()
		{
			npc.width = 16;
			npc.height = 16;
			npc.damage = 100;
			npc.defense = 0;
			npc.lifeMax = 20000;
			npc.defense = 20;
			if (SGAWorld.downedWraiths<3){
			npc.lifeMax = 15000;
			npc.defense = 0;
			}
			else{
			fighttversion=1;
			}
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			npc.boss=true;
			music=MusicID.TheTowers;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.value = Item.buyPrice(0, 75, 0, 0);
		}
				public override string Texture
		{
			get { return("SGAmod/NPCs/TPD");}
		}

		public override bool CheckDead()
		{
		if (warninglevel<70)
		Idglib.Chat("I don't appericate being 1-shotted. Piss off.",25, 25, 80);
		return (warninglevel>69);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}

		public override void AI()
		{

					//npc.netUpdate = true;
						Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
				if (!P.active || P.dead){Idglib.Chat("Pathetic...",25, 25, 80); npc.active=false;}
				}else{

		npc.ai[0]+=1;

		if (npc.ai[0]%20==0)
		dodge=new Vector2((float)Main.rand.Next(-200,200),(float)Main.rand.Next(-100,100));
		if (npc.ai[0]==1){

		int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType("LuminiteWraithHead"));
		NPC newguy2=Main.npc[newguy];
		CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece;
		LuminiteWraithArmor newguy4 = newguy2.modNPC as LuminiteWraithArmor; newguy2.lifeMax=(int)(npc.life/2); newguy2.life=(int)(npc.lifeMax/2);
		if (fighttversion==1)
		newguy2.lifeMax=(int)(npc.life/3); newguy2.life=(int)(npc.lifeMax/3);

		newguy4.mytargetis=new Vector2(0,-100);
		newguy3.attachedID=npc.whoAmI;
		newguy2.knockBackResist=0.85f;
		newguy4.delay=20f;

		newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType("LuminiteWraithChestPlate"));
		newguy2=Main.npc[newguy];
		newguy3 = newguy2.modNPC as CopperArmorPiece;
		newguy4 = newguy2.modNPC as LuminiteWraithArmor; newguy2.lifeMax=(int)(npc.lifeMax/2); newguy2.life=(int)(npc.lifeMax/2);
		if (fighttversion==1)
		newguy2.lifeMax=(int)(npc.life/3); newguy2.life=(int)(npc.lifeMax/3);

		newguy4.mytargetis=new Vector2(160,100);
		newguy3.attachedID=npc.whoAmI;
		newguy2.knockBackResist=0.85f;
		newguy4.delay=20f;

		newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType("LuminiteWraithPants"));
		newguy2=Main.npc[newguy];
		newguy3 = newguy2.modNPC as CopperArmorPiece;
		newguy4 = newguy2.modNPC as LuminiteWraithArmor; newguy2.lifeMax=(int)(npc.lifeMax/2); newguy2.life=(int)(npc.lifeMax/2);
		if (fighttversion==1)
		newguy2.lifeMax=(int)(npc.life/3); newguy2.life=(int)(npc.lifeMax / 3);

		newguy4.mytargetis=new Vector2(-160,100);
		newguy3.attachedID=npc.whoAmI;
		newguy2.knockBackResist=0.85f;
		newguy4.delay=30f;


		}

		DoAIStuff(P);


		if (Main.netMode!=1){
		if (warninglevel2==2){
		warninglevel2+=8;
		Idglib.Chat("So your the one messing with our master's plans...",25, 25, 80);
		}
		if (warninglevel2==12){
		warninglevel2+=10;
		Idglib.Chat("The one who destroyed his creations...",25, 25, 80);
		}
		if (warninglevel2==24){
		warninglevel2+=10;
		Idglib.Chat("He was right to fear you...",25, 25, 80);
		}

		if (warninglevel<20 && npc.life<(int)(npc.lifeMax*0.2)){
		warninglevel+=5;
		npc.life=npc.lifeMax;
		npc.netUpdate = true;

		Idglib.Chat(warninglevel<10 ? "Oh? Think we're done just yet?" : "You think We'd die that easily?",25, 25, 80);
		}else{
		if (warninglevel<30 && npc.life<(int)(npc.lifeMax*0.2)){
		warninglevel+=5;
		npc.life=npc.lifeMax;
		npc.netUpdate=true;
		Idglib.Chat("Well now! Arn't you quite the fighter...",25, 25, 80);
		}else{
		if (warninglevel<50 && npc.life<(int)(npc.lifeMax*0.2)){
		warninglevel+=5;
		npc.life=npc.lifeMax;
		npc.netUpdate=true;
		if (warninglevel<40)
		Idglib.Chat("Think again!",25, 25, 80);
		else
		Idglib.Chat("We still got plenty left!",25, 25, 80);
		}else{
		if (warninglevel<70 && npc.life<(int)(npc.lifeMax*0.2)){
		warninglevel+=5;
		npc.life=npc.lifeMax;
		npc.netUpdate=true;
		Idglib.Chat(warninglevel<55 ? "This is proving more than We expected..." : (warninglevel<65 ? "How is he still alive?!" : "Failure is an option we can't affourd!"),25, 25, 80);
		}}}}

		if (warninglevel>69 && warninglevel<1000 && npc.life<(int)(npc.lifeMax*0.75)){warninglevel=1000;
		Idglib.Chat("No you can't beat us!",25, 25, 80);

						/*if (Main.expertMode)
						{
							for (int gg = 0; gg < Main.maxNPCs; gg += 1)
							{
								if (Main.npc[gg].active && Main.npc[gg].type == ModContent.NPCType<LuminiteWraithTarget>())
								{
									Main.npc[gg].ai[0] = -900;
									Main.npc[gg].netUpdate = true;
								}
							}

						}*/
		npc.netUpdate = true;
		}
		if (warninglevel>69 && warninglevel<2000 && npc.life<(int)(npc.lifeMax*0.2)){warninglevel=2000;
		Idglib.Chat("Master, hear my call!",25, 25, 80);

						/*if (Main.expertMode)
						{
							for (int gg = 0; gg < Main.maxNPCs; gg += 1)
							{
								if (Main.npc[gg].active && Main.npc[gg].type == ModContent.NPCType<LuminiteWraithTarget>())
								{
									Main.npc[gg].ai[0] = -900;
								}
							}
						}*/

					npc.netUpdate=true;
		}


		}


		}

	}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1f;
			position=position+new Vector2(0,64f);
			return true;
		}


public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
{
Vector2 drawPos = npc.Center - Main.screenPosition;
Color glowingcolors1 = Main.hslToRgb((float)lightColor.R*0.08f,(float)lightColor.G*0.08f,(float)lightColor.B*0.08f);
Texture2D texture = mod.GetTexture("NPCs/TPD");
spriteBatch.Draw(texture, drawPos, null, glowingcolors1, npc.spriteDirection+(npc.ai[0]*0.4f), new Vector2(16,16),new Vector2(Main.rand.Next(5,35)/17f,Main.rand.Next(5,35)/17f), SpriteEffects.None, 0f);


drawPos = npc.Center - Main.screenPosition;
for (int i = 0; i < 70; i=i+1){
float inrc=npc.ai[0]/50f;
if (warninglevel<i){
glowingcolors1 = Main.hslToRgb(inrc*0.05f,0.8f,1f);
		float angle = 2f * (float)Math.PI / 70f * i;
		float dist=100f;
Vector2 thisloc=new Vector2((float)(Math.Cos(angle) * dist),(float)(Math.Sin(angle) * dist));
spriteBatch.Draw(texture, drawPos+thisloc, null, glowingcolors1, npc.spriteDirection+(npc.ai[0]*0.4f), new Vector2(16,16),new Vector2(Main.rand.Next(1,20)/17f,Main.rand.Next(1,20)/17f), SpriteEffects.None, 0f);
}}


/*Texture2D texture = mod.GetTexture("NPCs/TPD");
Vector2 drawPos = npc.Center - Main.screenPosition;
for (int i = 0; i < 80; i=i+1){
float inrc=npc.ai[0]/60;
Color glowingcolors1 = Main.hslToRgb(inrc*1.71f,0.5f,0.8f);
double angle=(inrc*(1f+(float)i/30f))+ 2.0* Math.PI * (i / ((double)20f));
float dist=(float)(i*1f);
Vector2 thisloc=new Vector2((float)(Math.Cos(angle) * dist),(float)(Math.Sin(angle) * dist));
spriteBatch.Draw(texture, drawPos+thisloc, null, glowingcolors1, npc.spriteDirection+(npc.ai[0]*0.4f), new Vector2(16,16),new Vector2(Main.rand.Next(1,20)/17f,Main.rand.Next(1,20)/17f), SpriteEffects.None, 0f);
}*/
return false;
}

public void DoAIStuff(Player P){

		//float moveval=(float)(Math.Sin(npc.ai[0]/600f)*40f);
		Vector2 loc=new Vector2(0f,-250f)+dodge;
			npc.dontTakeDamage = false;
			npc.GivenName="The Luminite Wraith";
			if (fighttversion==0 && warninglevel>30){
		quitermode+=1;
		npc.dontTakeDamage=true;
		music=MusicID.Title;
		if (quitermode==100)
		Idglib.Chat("You know what...?",25, 25, 80);
		if (quitermode==200)
		Idglib.Chat("Your not worth fighting",25, 25, 80);
		if (quitermode==300)
		Idglib.Chat("Not yet",25, 25, 80);
		if (quitermode==400)
		Idglib.Chat("Soon, my master will challenge you",25, 25, 80);
		if (quitermode==500)
		Idglib.Chat("And crush you like the little worm you are",25, 25, 80);

		if (quitermode==600){
		Idglib.Chat("Goodbye",25, 25, 80);
		if (SGAWorld.downedWraiths<3){SGAWorld.downedWraiths=3;
		Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LunarCraftingStation);
		Idglib.Chat("The cultist can now drop an Ancient Manipulator",150, 150, 70);
		}
		npc.active=false;
		}
		}

		if (fighttversion==1 && warninglevel>30 && quitermode>-1){
		quitermode+=1;
		npc.dontTakeDamage=true;
		music=MusicID.Title;
		if (Main.netMode!=1){
		if (quitermode==100)
		Idglib.Chat("You INSECT!",25, 25, 80);
		if (quitermode==200)
		Idglib.Chat("I'm done playing around",25, 25, 80);
		if (quitermode==300)
		Idglib.Chat("It's time, you know your PLACE!",25, 25, 80);
		if (quitermode==400){
		int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WraithSolarFlareAxe")); NPC newguy2=Main.npc[newguy]; CopperArmorPiece newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.localAI[0]=0f; newguy2.ai[1]=128f; newguy2.ai[2]=-128f; newguy2.knockBackResist=0.9f; newguy2.netUpdate=true;
		newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WraithPhantasm")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.localAI[0]=(float)Math.PI/2f; newguy2.ai[1]=128f; newguy2.knockBackResist=0.9f; newguy2.netUpdate=true;
		newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WraithSolarFlareAxe")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.localAI[0]=(float)Math.PI; newguy2.ai[1]=128f; newguy2.knockBackResist=0.9f; newguy2.netUpdate=true;
		newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WraithPhantasm")); newguy2=Main.npc[newguy]; newguy3 = newguy2.modNPC as CopperArmorPiece; newguy3.attachedID=npc.whoAmI; newguy2.localAI[0]=(float)Math.PI*1.5f; newguy2.ai[1]=128f; newguy2.knockBackResist=0.9f; newguy2.netUpdate=true;
		}
		if (quitermode==500)
		Idglib.Chat("Now...",25, 25, 80);
		if (quitermode==570)
		Idglib.Chat("Die!",25, 25, 80);
		}

		if (quitermode==600){
		quitermode=-1;
		npc.dontTakeDamage=false;
		music=MusicID.LunarBoss;
		}
		}

			if (npc.dontTakeDamage == false)
			if (NPC.CountNPCS(mod.NPCType("BossCopperWraith")) > 0)
				npc.dontTakeDamage = true;

			if (music == MusicID.LunarBoss)
			{
				for (int i = 0; i < Main.maxPlayers; i += 1)
				{
					if (Main.player[i].active && !Main.player[i].dead)
						Main.player[i].AddBuff(ModLoader.GetMod("IDGLibrary").GetBuff("BossFightPurity").Type, 1);
				}
			}


			npc.velocity/=1.05f;
		if (quitermode<1){

				Vector2 itzx = P.velocity + new Vector2(0,-0.25f);
				float addtocircle = (warninglevel<30 ? 0.2f : 0.4f);

				if (npc.ai[0] % 2400 > 2000 && fighttversion > 0 && Main.expertMode)
				{
					addtocircle = 0.75f;
					double angle = (npc.ai[0] / 20f) + 2.0 * Math.PI * 1.0;
					loc = new Vector2((float)((Math.Cos(angle) * (700f + itzx.Length() * 80f))), (float)((Math.Sin(angle) * (700f + itzx.Length() * 80f))));
				}
				else
				{

					if (npc.ai[0] % 3100 > 2400 && fighttversion > 0 && Main.expertMode)
					{
						Vector2 itzy = itzx;
						itzy.Normalize(); itzy *= 600f;
						loc = itzy + (P.velocity * 8f);
					}

				}

		Vector2 dir=(P.Center+loc-npc.Center);
		Vector2 dist=dir;

		if (npc.ai[0]%600>200){

		Vector2 distnormal= dist;
		if (dist.Length() > 320 && dist.Length() < 2500 && addtocircle < 0.7f)
		{
		distnormal.Normalize();
		distnormal *= 320;
		}

		dir.Normalize();
		npc.velocity+=(dir*addtocircle) +(distnormal * 0.001f);

}else{

		if (npc.ai[0]%70>60 && npc.ai[0]%600>40){
		dir=(P.Center-npc.Center);
		dir.Normalize();
		npc.velocity+=(dir*5);
		}

	}

}












}




}












	public class LuminiteWraithArms : CobaltArmorChainmail
	{


		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{

		CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
		int npctype = mod.NPCType(myself.attachedType);
		NPC myowner = Main.npc[myself.attachedID];

			int width = 64;
		int height = Math.Max(160, (int)(((myowner.Center - npc.Center).Length())/5f));

		Texture2D beam = new Texture2D(Main.graphics.GraphicsDevice, width, height);
		var dataColors = new Color[width * height];


			///


			for (int y = 0; y<height; y++)
			{
				for (float x = 0f; x < 5f; x += 0.25f)
				{
					int output = 32;

					int inta = Main.rand.Next((int)x * -5, (int)x*5);
					output += inta;

					dataColors[output + y * width] = Main.hslToRgb(Main.rand.NextFloat(),0.75f,0.5f);
				}

			}

			beam.SetData(dataColors);

			Idglib.coloroverride = Color.White;

			Idglib.DrawSkeletronLikeArms(spriteBatch, beam, npc.Center, myowner.Center,-8f,-8f, MathHelper.Clamp((myowner.Center.X- npc.Center.X)*0.02f,-1,1));
			Vector2 drawPos = ((Idglib.skeletronarmjointpos - Main.screenPosition)) + new Vector2(0f, 0f);
			Texture2D texture = mod.GetTexture("NPCs/TPD");
			spriteBatch.Draw(texture, drawPos, null, Color.White, npc.spriteDirection + (npc.ai[0] * 0.4f), new Vector2(16, 16), new Vector2(Main.rand.Next(15, 35) / 17f, Main.rand.Next(15, 35) / 17f), SpriteEffects.None, 0f);

			Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width/2, Main.npcTexture[npc.type].Height / 2);
			//spriteBatch.Draw(Main.npcTexture[npc.type], npc.position-Main.screenPosition, null, drawColor, npc.rotation, drawOrigin, npc.scale, npc.spriteDirection > 0 ? (SpriteEffects.FlipVertically) : SpriteEffects.None, 0f);
			//spriteBatch.Draw(ModContent.GetTexture("Terraria/Glow_197"), npc.position - Main.screenPosition, null, drawColor, npc.rotation, drawOrigin, npc.scale, npc.spriteDirection < 1 ? (SpriteEffects.FlipVertically) : SpriteEffects.None, 0f);


			return true;
		}

		public override void NPCLoot()
		{
			//nothing
		}

	}






	public class WraithSolarFlareAxe : LuminiteWraithArms
	{
		public new int armortype = ItemID.SolarFlareAxe;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Axe");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + armortype; }
		}
		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.damage = 15;
			npc.defDamage = 15;
			npc.defense = 0;
			npc.lifeMax = 100000;
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
			CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
			int npctype = mod.NPCType(myself.attachedType);
			NPC myowner = Main.npc[myself.attachedID];
			if (myowner.active == false)
			{
				myself.ArmorMalfunction();
			}
			else
			{
				npc.dontTakeDamage = myowner.dontTakeDamage;
				npc.localAI[0] += 0.02f;
				double angle = ((double)(npc.localAI[0])) + 2.0 * Math.PI;
				Player P = Main.player[myowner.target];
				npc.ai[0] += 1;
				npc.spriteDirection = npc.velocity.X > 0 ? -1 : 1;
				Vector2 itt = (myowner.Center - npc.Center + new Vector2((float)Math.Cos((float)angle) * npc.ai[1], (float)Math.Sin((float)angle) * npc.ai[1]));
				float locspeed = 0.25f;
				if (npc.ai[0] % 900 > 650)
				{
					npc.damage = (int)npc.defDamage * 5;
					itt = (P.Center - npc.Center + new Vector2((float)Math.Cos((float)angle) * npc.ai[1], (float)Math.Sin((float)angle) * npc.ai[1]));
					npc.rotation = npc.rotation + (0.65f * npc.spriteDirection);
					if (npc.ai[0] % 30 == 0)
					{
						Vector2 itt2 = itt; itt2.Normalize();
						npc.velocity = (itt2 * 40f);
					}
					npc.velocity = npc.velocity * 0.95f;

					int dust = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("HotDust"));
					Main.dust[dust].scale = 0.5f;
					Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();
					Main.dust[dust].velocity = (randomcircle / 2f) + (npc.velocity / 1.5f);
					Main.dust[dust].noGravity = true;

				}
				else
				{
					npc.damage = 0;
					if (npc.ai[0] % 300 < 60)
					{
						locspeed = 0.50f;
						npc.velocity = npc.velocity * 0.92f;
					}
					npc.rotation = (float)npc.velocity.X * 0.09f;
					locspeed = 2.0f;
				}
				npc.velocity = npc.velocity * 0.96f;
				itt.Normalize();
				npc.velocity = npc.velocity + (itt * locspeed);
				npc.timeLeft = 999;
			}


		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode || Main.rand.Next(2) == 0)
			{
				player.AddBuff(BuffID.Burning, 60 * 4, true);
			}
		}

	}


		public class WraithPhantasm: LuminiteWraithArms
	{
		public new int armortype=ItemID.Phantasm;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasm");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + armortype; }
		}
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 24;
			npc.height = 24;
			npc.damage = 0;
			npc.defense = 5;
			npc.lifeMax = 100000;
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
		CopperArmorPiece myself = npc.modNPC as CopperArmorPiece;
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		if (myowner.active==false){
		myself.ArmorMalfunction();
		}else{
		npc.dontTakeDamage=myowner.dontTakeDamage;
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
		npc.localAI[0]+=0.02f;
		double angle=((double)(npc.localAI[0]))+ 2.0* Math.PI;
		if (!myowner.dontTakeDamage)
		npc.ai[0]+=1;
		Vector2 itt=(myowner.Center-npc.Center+new Vector2((float)Math.Cos((float)angle)*npc.ai[1],(float)Math.Sin((float)angle)*npc.ai[1]));
		if (npc.ai[0]%1800>1250){
		itt=(P.Center-npc.Center+new Vector2((float)Math.Cos((float)angle)*npc.ai[1],(float)Math.Sin((float)angle)*npc.ai[1]));;
		}
		float locspeed=0.25f;
		if (npc.ai[0]%1400>950){
		Vector2 cas=new Vector2(npc.position.X-P.position.X,npc.position.Y-P.position.Y);
		double dist=cas.Length();
		float rotation = (float)Math.Atan2(npc.position.Y - (P.position.Y - (new Vector2(0,(float)(dist*0.04f))).Y + (P.height * 0.5f)), npc.position.X - (P.position.X + (P.width * 0.5f)));
		npc.rotation=rotation;//npc.rotation+((rotation-npc.rotation)*0.1f);
		npc.velocity=npc.velocity*0.92f;
		if (npc.ai[0]%10==0 && npc.ai[0]%1400>1150){
		Idglib.Shattershots(npc.Center,npc.Center+new Vector2(-15*npc.spriteDirection,0),new Vector2(0,0),ProjectileID.MoonlordArrowTrail,35,20,0,1,true,(Main.rand.Next(-100,100)*0.000f)-npc.rotation,true,300);
		}
		npc.spriteDirection=1;
		}else{
		if (Math.Abs(npc.velocity.X)>2){ npc.spriteDirection=npc.velocity.X>0 ? -1 : 1; }
		npc.rotation=(float)npc.velocity.X*0.09f;
		locspeed=2.5f;
		}
		npc.velocity=npc.velocity*0.96f;
		itt.Normalize();
		npc.velocity=npc.velocity+(itt*locspeed);
		npc.timeLeft=999;
		}

		}
	}

}












	public class LuminiteWraithArmor: CopperArmorPiece
	{
			public int tempappeance=0;
			public CopperArmorPiece myself;
			public List<int> armors=new List<int>();
			public int xpos;
			public int ypos;
			public Vector2 mytargetis;
			public Vector2 whereisthetarget;
			public float delay=0f;
			public bool hitstate=false;
		public int timeswentdown = 0;
			public bool droploot=false;

		public override void ArmorMalfunction()
        { 
        int theitem=0;
        if (!Main.player[npc.target].dead && droploot){
        if (tempappeance==ItemID.VortexHelmet || tempappeance==ItemID.VortexBreastplate || tempappeance==ItemID.VortexLeggings)
        theitem = ItemID.FragmentVortex;
        if (tempappeance==ItemID.SolarFlareHelmet || tempappeance==ItemID.SolarFlareBreastplate || tempappeance==ItemID.SolarFlareLeggings)
        theitem = ItemID.FragmentSolar;
        if (tempappeance==ItemID.StardustHelmet || tempappeance==ItemID.StardustBreastplate || tempappeance==ItemID.StardustLeggings)
        theitem = ItemID.FragmentStardust;
        if (tempappeance==ItemID.NebulaHelmet || tempappeance==ItemID.NebulaBreastplate || tempappeance==ItemID.NebulaLeggings)
        theitem = ItemID.FragmentNebula;
        if (theitem>0){
        for (int f = 0; f < (Main.expertMode ? 6 : 4); f=f+1){
        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, theitem);
        }}
        }
        base.ArmorMalfunction();
    }

		public override void UpdateLifeRegen(ref int damage)
		{
			myself = npc.modNPC as CopperArmorPiece;
			NPC myowner = Main.npc[myself.attachedID];
			if (myowner.active == true)
			{
				myowner.lifeRegen -= npc.lifeRegen;
			}
		}



		public override void NPCLoot()
		{
					//nothing
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
		if (projectile.width>24 && projectile.height>24)
		damage=(int)((double)damage/50.0);
		}

		public void DoAIStuff()
		{

			Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
				}else{
				npc.ai[0]+=1;

				if (npc.ai[0]==1){
				int newguy=NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y-10, mod.NPCType("LuminiteWraithTarget"));
				NPC newguy2=Main.npc[newguy];
				newguy2.ai[0]=Main.rand.Next(0,10000);
				newguy2.ai[1]=npc.whoAmI;
				newguy2.ai[2]=myself.attachedID;
				(newguy2.modNPC as LuminiteWraithTarget).delay=delay;
				npc.ai[1]=newguy;
				}
				NPC myowner=Main.npc[myself.attachedID];
				if (npc.dontTakeDamage==false){

//public static List<Projectile> Shattershots(Vector2 here,Vector2 there,Vector2 widthheight,int type,int damage,float Speed,float spread,int count,bool centershot,float globalangularoffset,bool tilecollidez,int timeleft){
			if (npc.ai[3]==ItemID.SpectreRobe){

			if (npc.ai[0]%400<100 && npc.ai[0]%8==0){
			List<Projectile> itz=Idglib.Shattershots(npc.Center,P.position,new Vector2(P.width,P.height),ProjectileID.DiamondBolt,25,(float)(Main.rand.Next(180,220))*0.1f,30,1,true,0,false,400);
			//itz[0].aiStyle=0;
			}
			if (npc.ai[0]%500==0){
			List<Projectile> itz=Idglib.Shattershots(npc.position,npc.position+new Vector2(0,200),new Vector2(0,0),ProjectileID.DiamondBolt,50,10,360,20,true,0,true,400);
			//itz[0].aiStyle=0;
			}

			}

			if (npc.ai[3]==ItemID.TurtleScaleMail){

			if (npc.ai[0]%400<150 && npc.ai[0]%8==0){
			List<Projectile> itz=Idglib.Shattershots(npc.Center,P.position,new Vector2(P.width,P.height),ProjectileID.SporeGas,25,15f,10,1,true,((float)Main.rand.Next(-500,500)/2000f),false,500);
			//itz[0].aiStyle=0;
			}

			}


			if (npc.ai[3]==ItemID.TikiShirt){

			if (npc.ai[0]%600<100 && npc.ai[0]%4==0){
			List<Projectile> itz=Idglib.Shattershots(npc.Center,npc.Center,new Vector2(0,16),ProjectileID.HornetStinger,30,5f,180,2,false,npc.ai[0]/15f,false,500);
			}

			}


			if (npc.ai[3]==ItemID.ShroomiteBreastplate){

			if (npc.ai[0]%300<240 && npc.ai[0]%5==0){
			List<Projectile> itz=Idglib.Shattershots(npc.Center,new Vector2(Main.rand.Next(-500,500)+npc.Center.X,npc.Center.Y+Main.rand.Next(-200,200)),new Vector2(0,0),ProjectileID.Mushroom,30,30f,30,1,true,0f,false,200);
			//itz[0].aiStyle=0;
			}

			}


			if (npc.ai[3]==ItemID.VortexBreastplate){

			if (npc.ai[0]%800>600 && npc.ai[0]%4==0){
				for (int a = 0; a < 20; a++){
				Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
				Vector2 vecr=randomcircle*512;
				vecr*=(1f-(800f/(npc.ai[0]%800)));

					int num622 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y)+vecr, 0, 0, 185, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num622].velocity *= 1f;

					Main.dust[num622].noGravity = true;
					Main.dust[num622].color=Main.hslToRgb((float)(Main.GlobalTime/5)%1, 0.9f, 1f);
					Main.dust[num622].color.A=10;
					Main.dust[num622].velocity.X = npc.velocity.X/3 + (Main.rand.Next(-50, 51) * 0.005f);
					Main.dust[num622].velocity.Y = npc.velocity.Y/3 + (Main.rand.Next(-50, 51) * 0.005f);
					Main.dust[num622].alpha = 100;;
				}
			}
			if (npc.ai[0]%800==799){
			for (int i = 0; i < Main.maxPlayers; i++)
			{
			Player ply=Main.player[i];
								if (ply != null && ply.active && !ply.dead)
								{
									ply.AddBuff(BuffID.VortexDebuff, 60 * 5);

									for (int a = 0; a < 60; a++)
									{
										Vector2 randomcircle = new Vector2(Main.rand.Next(-8000, 8000), Main.rand.Next(-8000, 8000)); randomcircle.Normalize();

										int num622 = Dust.NewDust(new Vector2(ply.Center.X, ply.Center.Y), 0, 0, 226, randomcircle.X * 8f, randomcircle.Y * 8f, 100, default(Color), 2f);
										Main.dust[num622].velocity *= 1f;

										Main.dust[num622].scale = 1.5f;
										Main.dust[num622].noGravity = true;
										Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
									}
								}

			}

			}


			}

			if (npc.ai[3]==ItemID.SolarFlareBreastplate){

			if (npc.ai[0]%800>600 && npc.ai[0]%4==0){
				for (int a = 0; a < 20; a++){
				Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();
				Vector2 vecr=randomcircle*512;
				vecr*=(1f-(800f/(npc.ai[0]%800)));

					int num622 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y)+vecr, 0, 0, 6, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num622].velocity *= 1f;

					Main.dust[num622].noGravity = true;
				}
			}

			if (npc.ai[0]%800<60 && npc.ai[0]%4==0 && npc.ai[0]>400){
			List<Projectile> itz=Idglib.Shattershots(new Vector2(npc.Center.X,npc.Center.Y),P.position,new Vector2(P.width,P.height),mod.ProjectileType("HeatWave"),30,20f,30,1,true,0,false,120);
			itz[0].friendly=false; itz[0].hostile=true;
			itz[0].damage=80;
			itz[0].alpha = 50;
			itz[0].tileCollide = false;
			//itz[0].aiStyle=0;
			}

			if (npc.ai[0]%800==799){

				for (int a = 0; a < 60; a++){
				Vector2 randomcircle=new Vector2(Main.rand.Next(-8000,8000),Main.rand.Next(-8000,8000)); randomcircle.Normalize();

					int num622 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), 0, 0, mod.DustType("HotDust"), randomcircle.X*8f, randomcircle.Y*8f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 1f;

						Main.dust[num622].scale = 1.5f;
						Main.dust[num622].noGravity=true;
				}

			}


			}
					if (npc.ai[3] == ItemID.NebulaBreastplate)
					{

						if (NPC.CountNPCS(NPCID.NebulaBrain)<1 && npc.ai[0] % 40 == 0)
						{
							int newguy = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.NebulaBrain);
							NPC newguy2 = Main.npc[newguy];
							newguy2.knockBackResist = 0f;
							newguy2.noTileCollide = true;
							//itz[0].aiStyle=0;
						}

					}

					if (npc.ai[3] == ItemID.StardustBreastplate)
					{

						if (NPC.CountNPCS(NPCID.StardustJellyfishBig) < 1 && npc.ai[0] % 40 == 0)
						{
							int newguy = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.StardustJellyfishBig);
							NPC newguy2 = Main.npc[newguy];
							newguy2.knockBackResist = 0f;
							newguy2.noTileCollide = true;
							//itz[0].aiStyle=0;
						}

					}


					if (npc.ai[3]==ItemID.SpectrePants){

			if (npc.ai[0]%500<200 && npc.ai[0]%5==0){
			List<Projectile> itz=Idglib.Shattershots(npc.Center,P.position,new Vector2(P.width,P.height),ProjectileID.DiamondBolt,25,10f,30,1,true,0,false,200);
			itz[0].damage=50;
			float ogspeed=itz[0].velocity.Length();
			itz[0].velocity.Normalize();
			itz[0].velocity.X/=2f;
			itz[0].velocity.Y+=itz[0].velocity.Y>0 ? 2f : -2;
			itz[0].velocity*=ogspeed;
			//itz[0].aiStyle=0;
			}

			}

			if (npc.ai[3]==ItemID.TikiPants){

			if (npc.ai[0]%500<150 && npc.ai[0]%15==0){
			List<Projectile> itz=Idglib.Shattershots(npc.Center,P.position,new Vector2(P.width,P.height),ProjectileID.PygmySpear,30,2f,30,1,true,0,false,400);
			itz[0].damage=30;
			float ogspeed=itz[0].velocity.Length();
			itz[0].velocity.Normalize();
			itz[0].velocity.X/=2f;
			itz[0].velocity.Y-=3f;
			itz[0].velocity*=ogspeed;
			//itz[0].aiStyle=0;
			}

			}


			if (npc.ai[3]==ItemID.SolarFlareLeggings){

			if (npc.ai[0]%600<200 && npc.ai[0]%10==0){
			List<Projectile> itz=Idglib.Shattershots(new Vector2(Main.rand.Next(-15,15)+npc.Center.X,npc.Center.Y+Main.rand.Next(0,30)),P.position,new Vector2(P.width,P.height+400),ProjectileID.GeyserTrap,30,10f,30,1,true,0,false,400);
			itz[0].damage=30;
			//itz[0].aiStyle=0;
			}

			}


					if (npc.ai[3] == ItemID.NebulaLeggings)
					{

						if (npc.ai[0] % 500 < 150 && npc.ai[0] % 15 == 0)
						{
							List<Projectile> itz = Idglib.Shattershots(npc.Center, P.position, new Vector2(P.width, P.height), ProjectileID.NebulaBolt, 30, 6f, 30, 1, true, 0, false, 400);
							itz[0].damage = 30;
							//itz[0].aiStyle=0;
						}

					}

					if (npc.ai[3] == ItemID.StardustLeggings)
					{

						if (npc.ai[0] % 200 < 32 && npc.ai[0] % 8 == 0)
						{
							List<Projectile> itz = Idglib.Shattershots(npc.Center, P.position, new Vector2(P.width, P.height), ProjectileID.StardustJellyfishSmall, 30, 6f, 30, 1, true, 0, false, 400);
							itz[0].damage = 30;
							//itz[0].aiStyle=0;
						}

					}

					if (npc.ai[3] == ItemID.VortexLeggings)
					{

						if (npc.ai[0] % 300 < 80 && npc.ai[0] % 3 == 0)
						{
							List<Projectile> itz = Idglib.Shattershots(npc.Center, P.position, new Vector2(P.width, P.height), ProjectileID.VortexAcid, 20, 6f, 30, 1, true, 0, false, 200);
							itz[0].velocity.X /= 3f;
							itz[0].velocity.Y -= Main.rand.NextFloat(2f,6f);
							//itz[0].aiStyle=0;
						}

					}


					if (npc.ai[3]==ItemID.TikiMask){

			if (npc.ai[0]%600<100 && npc.ai[0]%15==0){
			List<Projectile> itz=Idglib.Shattershots(new Vector2(Main.rand.Next(-15,15)+npc.Center.X,npc.Center.Y+Main.rand.Next(0,30)),P.position,new Vector2(P.width,P.height-300),ProjectileID.BeeHive,20,Main.rand.Next(120,250)/10,30,1,true,0,false,80);
			itz[0].damage=50;
			//int newguy=NPC.NewNPC((int)npc.Center.X+Main.rand.Next(-12,12), (int)npc.Center.Y-10, NPCID.Bee);
			//itz[0].aiStyle=0;
			}

			}


			if (npc.ai[3]==ItemID.StardustHelmet){

			if (npc.ai[0]%600<100 && npc.ai[0]%40==0){
		int newguy=NPC.NewNPC((int)npc.Center.X+Main.rand.Next(-1000,1000), (int)npc.Center.Y-800, NPCID.StardustCellSmall);
		NPC newguy2=Main.npc[newguy];
		newguy2.knockBackResist=0f;
		newguy2.noTileCollide=true;
			//itz[0].aiStyle=0;
			}

			}

			if (npc.ai[3]==ItemID.VortexHelmet){

			if (npc.ai[0]%400<200 && npc.ai[0]%20==0){
			List<Projectile> itz=Idglib.Shattershots(new Vector2(Main.rand.Next(-500,500)+npc.Center.X,npc.Center.Y+Main.rand.Next(-250,-100)),P.position,new Vector2(P.width,P.height),ProjectileID.VortexVortexLightning,50,0f,30,1,true,0,false,400);
			itz[0].damage=150;
			//itz[0].aiStyle=0;
			}

			}

			if (npc.ai[3]==ItemID.SolarFlareHelmet){

			if (npc.ai[0]%400<150 && npc.ai[0]%15==0){
			Vector2 velocity7 = -Vector2.UnitY.RotatedByRandom(0.78539818525314331) * (7f + Main.rand.NextFloat() * 5f);
			int num1392 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 519, npc.whoAmI, 0f, 0f, 0f, 0f, 255);
			Main.npc[num1392].velocity = velocity7;
			Main.npc[num1392].noTileCollide = true;
			Main.npc[num1392].netUpdate = true;
			//itz[0].aiStyle=0;
			}

			}

					if (npc.ai[3] == ItemID.NebulaHelmet)
					{

						if (npc.ai[0] % 400 < 150 && npc.ai[0] % 40 == 0)
						{
							int newguy = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.NebulaHeadcrab);
							NPC newguy2 = Main.npc[newguy];
							newguy2.knockBackResist = 0f;
							newguy2.noTileCollide = true;
							//itz[0].aiStyle=0;
						}

					}


				}

			}
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.damage = 0;
			npc.defense = 10;
			npc.lifeMax = 5;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 0f;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			aiType = -1;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			attachedType="LuminiteWraith";
			friction=0.65f;
			speed=0.8f;
		}

		private void RelayDamage(Player player, int damage, float knockback, bool crit)
		{
		myself = npc.modNPC as CopperArmorPiece;
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		if (myowner.active==true){
		myowner.StrikeNPC(damage,0f,0,crit,true,true);
		}
	}

		public override void OnHitByItem (Player player,Item item, int damage, float knockback, bool crit)
		{
		RelayDamage(player,damage,knockback,crit);
		base.OnHitByItem(player,item,damage,knockback,crit);
		}

		public override void OnHitByProjectile (Projectile projectile, int damage, float knockback, bool crit)
		{
		double damagemul=1.0;
		if (projectile.penetrate>0)
		damagemul=0.5;
		if (projectile.penetrate>3 || projectile.penetrate < 0)
		damagemul=0.15;
		RelayDamage(Main.player[projectile.owner],(int)(damage*damagemul),knockback,crit);
		base.OnHitByProjectile(projectile,(int)(damage*damagemul),knockback,crit);
		}

		public override bool CheckActive()
		{
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		return (!myowner.active);
		}

		public override bool CheckDead()
		{
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];

		if (!myowner.active)
		return true;

		if (armors.Count>0 && npc.life<1){
				if (myowner.active)
				{
					if (timeswentdown < 4)
					npc.ai[3] = (float)Main.rand.Next(-1200, -600);
					else
					npc.ai[3] = (float)Main.rand.Next(-700, -550);
				}
				else
				{
					npc.ai[3] = -499;
				}
		npc.defDefense += Main.expertMode ? 5 : 2;
		npc.defense=npc.defDefense;
		npc.life=npc.lifeMax;
		npc.ai[0]=2;
		timeswentdown += 1;
		npc.netUpdate=true;
		(myowner.modNPC as LuminiteWraith).warninglevel+=1;
		(myowner.modNPC as LuminiteWraith).warninglevel2+=1;
		return false;
		}else{




		return false;
	}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Body Plate");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override string Texture
		{
			get { return "Terraria/Item_" + 0; }
		}

		public override void AI()
		{
		if (myself==null)
		myself = npc.modNPC as CopperArmorPiece;
		int npctype=mod.NPCType(myself.attachedType);
		NPC myowner=Main.npc[myself.attachedID];
		if (myowner.active==false){
		myself.ArmorMalfunction();
		}else{
		if ((myowner.modNPC as LuminiteWraith).warninglevel<1)
		droploot=true;
		if (npc.ai[3]<1 && npc.ai[3]>-500){
		npc.ai[3]=(float)armors[0];
		//Main.NewText("got this item: [i:"+armors[0]+"]");
		tempappeance=armors[0];
		armors.RemoveAt(0);
		hitstate=false;
		if (npc.ai[0]>10)
		npc.ai[0]=2;
		npc.netUpdate=true;
		}else{if (npc.ai[3]<1){npc.ai[3]+=1; hitstate=true;}}

	if (npc.ai[3]>0){
	DoAIStuff();
	npc.alpha=(int)MathHelper.Clamp((float)npc.alpha+6,1,255);
	}

	npc.dontTakeDamage=hitstate;
	if (myowner.dontTakeDamage==true)
	npc.dontTakeDamage=true;

		npc.velocity=npc.velocity+(myowner.Center+new Vector2((float)xpos,(float)ypos)-npc.Center)*(myself.speed);
		npc.velocity=npc.velocity*myself.friction;
		npc.rotation=(float)npc.velocity.X*0.1f;
		//npc.position=myowner.position;
		npc.timeLeft=999;
		}

		if (npc.ai[3]<-400){

   	for (int i = 0; i < 3; ++i){
  	double devider=(i / ((double)3f));
  	double angle=(npc.ai[3]/15)+ 2.0* Math.PI * devider;
  	Vector2 thecenter=new Vector2((float)((Math.Cos(angle) * 1)), (float)((Math.Sin(angle) * 1)));
    thecenter = thecenter.RotatedByRandom(MathHelper.ToRadians(10));
    int DustID2 = Dust.NewDust(npc.Center+(thecenter*8f), 0, 0, 43);
    Main.dust[DustID2].noGravity = true;
    Main.dust[DustID2].velocity = new Vector2(thecenter.X*0.2f, thecenter.Y*0.2f)*-1f;
    }

	npc.alpha=(int)MathHelper.Clamp((float)npc.alpha-5,1,255);

		}


	}

public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
{
Texture2D texture = Main.itemTexture[tempappeance];
Vector2 drawPos = npc.Center - Main.screenPosition;
Color lights=lightColor;
lights.A=(byte)(npc.alpha);
Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
spriteBatch.Draw(texture, drawPos, null, lights, npc.rotation, origin,new Vector2(1f,1f), SpriteEffects.None, 0f);
return false;
}

}




	public class LuminiteWraithChestPlate: LuminiteWraithArmor
	{

		public override void SetDefaults()
		{

			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.SpectreRobe);
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.TurtleScaleMail);
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.ShroomiteBreastplate);
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.TikiShirt);

			for (int i = 0; i < 100; i += 1)
			{
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.StardustBreastplate);
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.SolarFlareBreastplate);
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.VortexBreastplate);
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.NebulaBreastplate);
			}

			xpos=0;
			ypos=0;
			base.SetDefaults();
		}
}

	public class LuminiteWraithPants: LuminiteWraithArmor
	{

		public override void SetDefaults()
		{
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.SpectrePants);
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.TurtleLeggings);
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.ShroomiteLeggings);
			armors.Insert(Main.rand.Next(0,armors.Count),ItemID.TikiPants);

			for (int i = 0; i < 100; i += 1)
			{
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.StardustLeggings);
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.SolarFlareLeggings);
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.VortexLeggings);
				armors.Insert(Main.rand.Next(4, armors.Count), ItemID.NebulaLeggings);
			}

			xpos=0;
			ypos=16;
			base.SetDefaults();
		}
}

	public class LuminiteWraithHead: LuminiteWraithArmor
	{

		public override void SetDefaults()
		{
			armors.Insert(Main.rand.Next(0, armors.Count), ItemID.SpectreHood);
			armors.Insert(Main.rand.Next(0, armors.Count), ItemID.TurtleHelmet);
			armors.Insert(Main.rand.Next(0, armors.Count), ItemID.ShroomiteHeadgear);
			armors.Insert(Main.rand.Next(0, armors.Count), ItemID.TikiMask);

		for (int i = 0; i < 100; i += 1)
		{
			armors.Insert(Main.rand.Next(4, armors.Count), ItemID.StardustHelmet);
			armors.Insert(Main.rand.Next(4, armors.Count), ItemID.SolarFlareHelmet);
			armors.Insert(Main.rand.Next(4, armors.Count), ItemID.VortexHelmet);
			armors.Insert(Main.rand.Next(4, armors.Count), ItemID.NebulaHelmet);
		}

			xpos=0;
			ypos=-16;
			base.SetDefaults();
		}
}



}

