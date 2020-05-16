using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace SGAmod.HavocGear.Items
{
	public class SharkvernBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = 11;
			item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return mod.NPCType("SharkvernHead");
			}
		}

		public override void OpenBossBag(Player player)
		{

			    List<int> types=new List<int>();
                types.Insert(types.Count,ItemID.SharkFin); 
                types.Insert(types.Count,ItemID.Seashell); 
                types.Insert(types.Count,ItemID.Starfish); 
                types.Insert(types.Count,ItemID.SoulofFlight);  
                types.Insert(types.Count,ItemID.Coral); 

        		for (int f = 0; f < (Main.expertMode ? 150 : 75); f=f+1){
        		player.QuickSpawnItem(types[Main.rand.Next(0,types.Count)]);
        		}

			player.TryGettingDevArmor();
			int lLoot = (Main.rand.Next(0,4));
			player.QuickSpawnItem(mod.ItemType("SerratedTooth"));
            if (lLoot == 0)
            {
				player.QuickSpawnItem(mod.ItemType("SkytoothStorm"));
			}
			if (lLoot == 1)
			{
				player.QuickSpawnItem(mod.ItemType("Jaws"));
			}
			if (lLoot == 2)
			{
				player.QuickSpawnItem(mod.ItemType("SnappyShark"));
				player.QuickSpawnItem(mod.ItemType("SharkTooth"), 150);
			}
			if (lLoot == 3)
			{
				player.QuickSpawnItem(mod.ItemType("SharkBait"), Main.rand.Next(60, 150));
			}
			player.QuickSpawnItem(mod.ItemType("SharkTooth"), Main.rand.Next(100, 200));
		}
	}
}