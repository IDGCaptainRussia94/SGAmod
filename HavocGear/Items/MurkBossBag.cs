using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.HavocGear.Items
{
	public class MurkBossBag : ModItem
	{
		public override void SetDefaults()
		{

			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;

			item.rare = 9;
			item.expert = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right click to open");
		}

		public override int BossBagNPC
		{
			get
			{
			return mod.NPCType("Murk");
			}
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            for (int f = 0; f < (Main.rand.Next(40,60)); f=f+1){
            player.QuickSpawnItem(mod.ItemType("MurkyGel"));
            }

            int random = Main.rand.Next(4);
			if (random == 3)
			{
				player.QuickSpawnItem(mod.ItemType("Mudmore"));
			}
			if (random == 2)
            {
				player.QuickSpawnItem(mod.ItemType("MurkFlail"));
            }
            if (random == 1)
			{
				player.QuickSpawnItem(mod.ItemType("Mossthorn"));
            }
            if (random == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Landslide"));
            }
            player.QuickSpawnItem(mod.ItemType("MudAbsorber"));
            player.QuickSpawnItem(mod.ItemType("MurkyGel"), 10);
		}
	}
}
