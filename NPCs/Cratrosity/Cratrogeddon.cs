using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace SGAmod.NPCs.Cratrosity
{

	public class Cratrogeddon: Cratrosity
	{



		private int pmlphase=0;
		private int pmlphasetimer=0;
		private List<int> summons=new List<int>();

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cratrogeddon");
			Main.npcFrameCount[npc.type] = 1;
		}


		public override void OrderOfTheCrates(Player P)
		{

			if (pmlphase==2 && pmlphasetimer>-300){

nonaispin=nonaispin+0.6f;
for (int a = 0; a < phase; a=a+1){
Cratesperlayer[a]=4+(a*4);
for (int i = 0; i < Cratesperlayer[a]; i=i+1){

		float boost=250f+(a*50f);
		Vector2 gohere = ((boost*(new Vector2((float)Math.Cos(Cratesangle[a,i]), (float)Math.Sin(Cratesangle[a,i])))))+P.Center;
		Cratesangle[a,i] = (a%2==0 ? 1 : -1)*((nonaispin*0.01f)*(1f+a/3f)) + 2.0 * Math.PI * ((i / (double)Cratesperlayer[a]));
		Cratesdist[a,i]= compressvar*20f+((float)a*20f)*compressvar;

		if (a==1 && pmlphasetimer%200<150 && pmlphasetimer<2200 && pmlphasetimer>1200){

	}else{
		Cratesvector[a,i]+= (gohere-Cratesvector[a,i])/2f;
	}
		Vector2 it=new Vector2(P.Center.X-Cratesvector[a,i].X,P.Center.Y-Cratesvector[a,i].Y);
		it.Normalize();


		if (a==0 && pmlphasetimer%50==i*5 && pmlphasetimer<1100 && pmlphasetimer>0){
		SgaLib.Shattershots(Cratesvector[a,i],Cratesvector[a,i]+new Vector2(it.X*((i+1)%2),it.Y*(i%2))*50,new Vector2(0,0),ProjectileID.GoldCoin,45,(float)3,0,1,true,0,false,300);
		SgaLib.Shattershots(Cratesvector[a,i],Cratesvector[a,i]+new Vector2(it.X*((i)%2),it.Y*((i+1)%2))*50,new Vector2(0,0),ProjectileID.GoldCoin,45,(float)4,0,1,true,0,false,300);
		}

		if (a==1 && pmlphasetimer%200<40 && pmlphasetimer<2200 && pmlphasetimer>1200 && pmlphasetimer%20==i){
		SgaLib.Shattershots(Cratesvector[a,i],Cratesvector[a,i]+it*50,new Vector2(0,0),ProjectileID.SilverCoin,35,(float)8,0,1,true,0,false,150);
		}

		if (a==2 && pmlphasetimer%180==i*10 && pmlphasetimer>2200 && pmlphasetimer<2800){
		SgaLib.Shattershots(Cratesvector[a,i],Cratesvector[a,i]+it*50,new Vector2(0,0),ProjectileID.CopperCoin,25,(float)3,0,1,true,0,false,200);
		}

}}


			}else{
		base.OrderOfTheCrates(P);

	}
		}

		public override void CrateRelease(int phase)
		{
		base.CrateRelease(phase);
		}

		public override void FalseDeath(int phase)
		{
		pmlphase=pmlphase+1;
		pmlphasetimer=1100;
		if (pmlphase==2){pmlphasetimer=3000;}
		SgaLib.Chat("Impressive, but not good enough",144, 79, 16);
		int spawnedint=NPC.NewNPC((int)npc.Center.X,(int)npc.Center.Y, summons[0]); summons.RemoveAt(0);
		NPC him=Main.npc[spawnedint];
		him.life=(int)(npc.life*0.75f);
		him.lifeMax=(int)(npc.lifeMax*0.75f);
		}


		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.damage = 70;
			npc.defense = 40;
			npc.lifeMax = 15000;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Evoland 2 OST - Track 46 (Ceres Battle)");
			animationType = 0;
			npc.noTileCollide = true;
			npc.noGravity = true;
			summons.Insert(summons.Count,mod.NPCType("CratrosityCrateOfSlowing"));
			summons.Insert(summons.Count,mod.NPCType("CratrosityCrateOfSlowing"));
			summons.Insert(summons.Count,mod.NPCType("CratrosityCrateOfSlowing"));
		}

		public override void AI()
		{
		Player P = Main.player[npc.target];
		Cratrosity origin = npc.modNPC as Cratrosity;
		pmlphasetimer--;
		npc.dontTakeDamage=false;
		if (pmlphasetimer>0){


		//phase 1
		if (pmlphase==1){
		OrderOfTheCrates(P);
		origin.compressvargoal=4f;
		origin.themode=1;
		npc.rotation=SgaLib.LookAt(npc.Center,P.Center);
		npc.dontTakeDamage=true;
		npc.velocity=(npc.velocity*0.97f);
		if (pmlphasetimer<1000){
		Vector2 it=new Vector2(P.Center.X-npc.Center.X,P.Center.Y-npc.Center.Y);
		it.Normalize();
		if (pmlphasetimer%120==0){
		npc.velocity=(it*(30f-pmlphasetimer*0.02f));
		}
		if (pmlphasetimer%120<60 && pmlphasetimer%20==0){
		SgaLib.Shattershots(npc.Center,npc.Center+it*50,new Vector2(0,0),ProjectileID.NanoBullet,40,(float)6,80,3,true,0,false,600);
		SgaLib.PlaySound(13,npc.Center, 0);
		}
		}
		}
		//phase 2
		if (pmlphase==2){
		npc.dontTakeDamage=true;
		OrderOfTheCrates(P);
		npc.velocity=(npc.velocity*0.77f);
		Vector2 it=new Vector2(P.Center.X-npc.Center.X,P.Center.Y-npc.Center.Y);
		it.Normalize();
		npc.velocity+=it*0.3f;
		npc.Opacity+=(0.1f-npc.Opacity)/30f;
	}




		}else{
		npc.rotation=npc.rotation*0.85f;
		base.AI();
		}

		}
	

	}



}

