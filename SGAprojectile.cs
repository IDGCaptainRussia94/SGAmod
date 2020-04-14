using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SGAmod
{
    public class SGAprojectile : GlobalProjectile
    {
	public Player myplayer=null;

		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
	public bool inttime=false;
	public bool enhancedbees=false;
	public bool splittingcoins=false;
	public bool raindown=false;
		public bool onehit = false;
	public Vector2 splithere=new Vector2(0,0);
		public int shortlightning = 0;

		/*private List<int> debuffs=new List<int>();
		private List<int> debufftime=new List<int>();

			public void AddDebuff(int debuff,int time) 
			{
			debuffs.Insert(debuffs.Count+1,debuff);
			debufftime.Insert(debufftime.Count+1,time);
			}

			public override void ModifyHitNPC(Projectile prog,NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
				applydebuffs(target,null);
			}
			public override void ModifyHitPlayer(Projectile prog,Player target, ref int damage, ref bool crit){
				applydebuffs(null,target);
			}

			private void applydebuffs(NPC target,Player ply){
			for (int i = 0; i < debuffs.Count;i++ )
			 {
			 if (target!=null){target.AddBuff(debuffs[i], debufftime[i], true);}
			 if (ply!=null){ply.AddBuff(debuffs[i], debufftime[i], true);}
		}
		}*/
		//for (x = 0; x < questvars.Length; x++)
		//{


		public override bool? CanHitNPC(Projectile projectile, NPC target)
		{
			if (projectile.type == ProjectileID.FlamethrowerTrap && projectile.owner > -1)
				return false;
			return base.CanHitNPC(projectile, target);
		}
		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if (onehit)
				projectile.Kill();
		}
		public override void PostAI(Projectile projectile)
		{
			if (shortlightning > 0)
			{

				for (int i = 0; i < Math.Min(shortlightning, projectile.oldPos.Length); i++)
				{
					projectile.oldPos[i].X = projectile.position.X;
					projectile.oldPos[i].Y = projectile.position.Y;
				}

			}
			if (projectile.modProjectile != null)
			{
				Player projowner = Main.player[projectile.owner];
				if (projectile.modProjectile.mod==SGAmod.Instance && projowner.active && projowner.heldProj==projectile.whoAmI)
				projectile.Opacity = MathHelper.Clamp(projowner.stealth, 0.1f, 1f);
			}
		}
		public override bool PreAI(Projectile projectile)
		{
		SGAprojectile modeproj=projectile.GetGlobalProjectile<SGAprojectile>();

	if (projectile.modProjectile!=null){

		/*if ((projectile.modProjectile).GetType().Name=="JackpotRocket"){
		projectile.velocity.Y+=0.1f;
		projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f; 
		}*/

	}

			/*if (Main.player[projectile.owner]!=null);
			ply=Main.player[projectile.owner];
			if (Main.npc[projectile.owner]!=null);
			npc=Main.npc[projectile.owner];
			if (npc!=null && modeproj.inttime==true){
			if (modeproj.raindown==true && projectile.position.Y>modeproj.splithere.Y && projectile.velocity.Y>0){
			projectile.timeLeft=-1;

			}
			if (modeproj.splittingcoins==true){
			if ((projectile.position.Y>modeproj.splithere.Y && projectile.velocity.Y>0) || (projectile.position.Y<modeproj.splithere.Y && projectile.velocity.Y<0)){
			SgaLib.Shattershots(projectile.position,projectile.position+new Vector2(-100,0),new Vector2(0,0),projectile.type,projectile.damage,projectile.velocity.Length(),0,1,true,0,true,projectile.timeLeft);
			SgaLib.Shattershots(projectile.position,projectile.position+new Vector2(100,0),new Vector2(0,0),projectile.type,projectile.damage,projectile.velocity.Length(),0,1,true,0,true,projectile.timeLeft);
			//projectile.position=new Vector2(0,-900);
			projectile.timeLeft=-1;
		}}

			}*/
			if (Main.player[projectile.owner] != null)
			{
				Player ply = Main.player[projectile.owner];
				if (ply != null)
				{
					SGAPlayer modplayer = ply.GetModPlayer<SGAPlayer>();
					if (ply != null)
					{
						if (modplayer.beefield > 0)
						{
							//modeproj.enhancedbees == true
							if ((projectile.type == 181 || projectile.type==ProjectileID.GiantBee) && modplayer.beefieldtoggle > 0)
							{
								if (projectile.velocity.Length() > 20)
								{
									projectile.velocity.Normalize();
									projectile.velocity = projectile.velocity * 0.98f;
								}
								else
								{
									projectile.velocity = projectile.velocity * 1.15f;
								}
							}
						}
					}
				}
			}
		modeproj.inttime=true;
		return true;
		}




	}

}