using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Items.Quests
{
	public class PremiumUpgrade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Contracker");
			//Tooltip.SetDefault("<right> for goodies!");
			Tooltip.SetDefault("Press <right> to activate\nActivates the TF2 Contract quest line\nActivating this quest will grant it's owner the TF2 emblem and allow Crate Drops\nUse this item after activated to check your progress\nThis DOES NOT work online and is a WIP, your save progress will not be saved!\nThe crates will drop per world on activation, however only new characters will receive the TF2 Emblem");

		}

		public override string Texture
		{
			get { return("SGAmod/Items/TerrariacoCrateBase");}
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.rare = 1;
			item.maxStack = 1;
			item.consumable=false;
			item.value = Item.buyPrice(1, 0, 0, 0);;
		}
		//player.CountItem(mod.ItemType("ModItem"))

		public override bool CanRightClick()
		{
		return true;
		}

		public override void RightClick(Player ply)
		{
			SGAWorld.QuestCheck(0,ply);
			ply.QuickSpawnItem(item.type,1);
		}
	}
}