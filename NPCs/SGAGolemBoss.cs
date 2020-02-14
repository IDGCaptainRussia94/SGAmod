using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.NPCs
{
	public class SGAGolemBoss : ModNPC
	{
		int oldtype=0;
		int phase=0;
		int minlife=9999;
		int anticheese=0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Simply better Golem");
			Main.npcFrameCount[npc.type] = 1;
		}
		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 24;
			npc.damage = 0;
			npc.defense = 0;
			npc.lifeMax = 3000000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.2f;
			npc.aiStyle = -1;
			npc.boss=false;
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.value = 1440000f;
			npc.dontTakeDamage=true;
			npc.immortal=true;
		}
				public override string Texture
		{
			get { return("SGAmod/NPCs/TPD");}
		}


		public override void NPCLoot()
		{


        }

		public override void AI()
		{
			npc.timeLeft=30;
			if (oldtype<1){
			oldtype=npc.type;
			}
			npc.netUpdate = true;
						Player P = Main.player[npc.target];
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active || NPC.CountNPCS(NPCID.Golem)<1)
			{
				npc.TargetClosest(false);
				P = Main.player[npc.target];
				if (NPC.CountNPCS(NPCID.Golem)<1)
				{
					npc.active=false;
				}
				}else{
					
			npc.ai[0]+=1;
		int npctype=NPCID.Golem;
		if (NPC.CountNPCS(npctype)>0){
		NPC myowner=Main.npc[NPC.FindFirstNPC(NPCID.Golem)];
		npc.position=myowner.position;
		if (anticheese%500<400){
		if (!Collision.CanHitLine(new Vector2(myowner.Center.X, myowner.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1))
		anticheese+=1;
		}else{
		
		}
		if (NPC.CountNPCS(NPCID.GolemHeadFree)>0){
		NPC myhead=Main.npc[NPC.FindFirstNPC(NPCID.GolemHeadFree)];
		if (phase==0){
		phase=1;
		myhead.lifeMax=myowner.lifeMax;
		myhead.life=myowner.lifeMax;
		myhead.dontTakeDamage=false;
		}}
		if (NPC.CountNPCS(NPCID.GolemHeadFree)<1 && phase==1)
		phase=2;
		if (phase==0)
		minlife=myowner.life;
		if (phase==1){
		myowner.life = minlife;
		if (Main.expertMode)
		minlife+=1;
		}

		if (phase==1 && npc.ai[0]%100==0){
		List<Projectile> itz=SgaLib.Shattershots(myowner.Center,P.position,new Vector2(P.width,P.height),ProjectileID.CultistBossFireBall,40,8,30,2,true,0,false,220);
		//itz[0].aiStyle=5;
		}
		if (phase>0 && npc.ai[0]%350==0){
		for (int i = 0; i < 3; i++)
		{
		List<Projectile> itz=SgaLib.Shattershots(myowner.Center,myowner.Center,new Vector2(0,0),mod.ProjectileType("Ringproj"),60,14,0,1,true,Main.rand.Next(-360,360),false,420);
		itz[0].timeLeft=200;
		itz[0].tileCollide=true;
		//itz[0].aiStyle=5;
		}}
		if ((phase>0 && (npc.ai[0]*(phase==2 ? 1.5f : 1f))%600>(phase==2 ? 450 : 500) && myowner.velocity.Y<0) || anticheese%500>399){
		if (myowner.velocity.Y<0){
		myowner.noTileCollide = true;
		myowner.velocity=myowner.velocity+(new Vector2(P.Center.X>myowner.Center.X ? 1 : -1,-0.25f));
		if (anticheese%500>399)
		anticheese+=1;
		//itz[0].aiStyle=5;
		}}


		if (phase==2){
		if (System.Math.Abs(myowner.velocity.Y)<1){
		if ((npc.ai[0])%600>400 && (npc.ai[0])%10==0){
		List<Projectile> itz=SgaLib.Shattershots(myowner.Center,P.position+new Vector2((float)Main.rand.Next(-150,150),-700f),new Vector2(P.width,P.height),ProjectileID.DD2BetsyFireball,70,30,0,1,true,0,false,220);
		itz[0].timeLeft=600;
		itz[0].tileCollide=false;
		SGAprojectile modeproj=itz[0].GetGlobalProjectile<SGAprojectile>();
		modeproj.raindown=true;
		modeproj.splithere=P.Center+new Vector2(0,200);
		}}}
		if (myowner.velocity.Y>0 && (myowner.Center.Y>P.Center.Y-100))
		myowner.noTileCollide = false;
		else
		myowner.noTileCollide = true;

		if (myowner.velocity.Y<-1)
		myowner.noTileCollide = true;

		}



		}

		}
		




		public override bool CanHitPlayer(Player ply, ref int cooldownSlot){
			return false;
		}
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return CanBeHitByPlayer(player);
		}
		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return CanBeHitByPlayer(Main.player[projectile.owner]);
		}
		private bool? CanBeHitByPlayer(Player P){
		return false;
		}


public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
{
return false;
}


	}
}

