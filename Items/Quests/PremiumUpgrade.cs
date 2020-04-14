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
			Tooltip.SetDefault("Use to activate\nActivates the TF2 Contract quest line\nActivating this quest will grant it's owner the TF2 emblem and allow Crate Drops\nUse this item after activated to check your progress\nThe Quest DOES NOT work online and is a WIP, your save progress will not be saved!\nThe crates will drop per world on activation, however only new characters will receive the TF2 Emblem");

		}

		public override string Texture
		{
			get { return("SGAmod/Items/TerrariacoCrateBase");}
		}

		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.consumable = false;
			item.width = 40;
			item.height = 40;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 4;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.value = Item.buyPrice(1, 0, 0, 0);
		}
		//player.CountItem(mod.ItemType("ModItem"))

		public override bool UseItem(Player ply)
		{
			SGAWorld.QuestCheck(0,ply);
			return true;
		}
	}
}