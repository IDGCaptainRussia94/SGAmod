using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SGAmod.Items
{
	public class SalvagedCrate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Salavaged Supply Crate");
			//Tooltip.SetDefault("<right> for goodies!");
			Tooltip.SetDefault("A strange forgotten crate, it looks like an old Terraria Co Crate.\nIt has a keyhole that looks familar to the only locked door I opened in this world...");

		}

		public override string Texture
		{
			get { return("SGAmod/Items/TerrariacoCrateBase");}
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.rare = 0;
			item.maxStack = 1;
		}

		public virtual void CrateLoot(Player ply){
				int chances=Main.rand.Next(0,1);
				string [] dropitems={"SwordathousandTruths1","SwordathousandTruths1"};
				ply.QuickSpawnItem(mod.ItemType(dropitems[chances]),1);
		}

		public override bool CanRightClick()
		{
		Main.NewText("Nope.avi... This boss isn't done yet, coming in a future update",250, 250, 250);
		return false;
		//Player ply=Main.LocalPlayer;
		//return (ply.CountItem(ItemID.TempleKey)>0 && !Main.dayTime && !NPC.AnyNPCs(mod.NPCType("Cratrogeddon")));
		}

		public override void RightClick(Player ply)
		{
		bool usedwrongkey=true;
		bool usedrightkey=false;

		if (usedrightkey==true){
		usedwrongkey=false;
		CrateLoot(ply);
		}

		if (usedwrongkey==true){
		ply.QuickSpawnItem(item.type, 1);
		if (!NPC.AnyNPCs(mod.NPCType("Cratrosity"))){
		Main.PlaySound(15, (int)ply.position.X, (int)ply.position.Y, 0);

						if (Main.netMode == 1)
						{
							ModPacket packet = mod.GetPacket();
							packet.Write((byte)MessageType.CratrosityNetSpawn);
							packet.Write(mod.NPCType("CratrosityPML"));
							packet.Write((int)(ply.Center.X-800));
							packet.Write(1600);
							packet.Write(ply.whoAmI);
							packet.Send();
							//packet.Send(-1, ply.whoAmI);
						}else{
				ply.ConsumeItem(ItemID.TempleKey);
				NPC.SpawnOnPlayer(ply.whoAmI, mod.NPCType("CratrosityPML"));
					for (int num172 = 0; num172 < 100; num172 = num172+1){
					Player ply2=Main.player[num172];
					//if (ply2.dead==false){
					ply2.GetModPlayer<SGAPlayer>().Locked=new Vector2(ply.Center.X-800,1600);
					//}
				}

		}
		}
		}

		}
	}

		enum MessageType : byte
	{
	CratrosityNetSpawn
	}

}