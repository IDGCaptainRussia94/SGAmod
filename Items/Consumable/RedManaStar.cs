using System;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace SGAmod.Items.Consumable
{
	public class RedManaStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Mana Star");
			Tooltip.SetDefault("" +
				"'Less Stable than its counterpart; it emanates heat but is thankfully only warm to the touch'" +
				"\nIncreases Max Mana by 20 each time it is used, up to 3 times depending on progression.");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
			item.maxStack = 30;
			item.rare = 8;
			item.value = 1000;
			item.useStyle = 2;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useTurn = true;
			item.UseSound = SoundID.Item9;
			item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			SGAPlayer sgaplayer = player.GetModPlayer<SGAPlayer>();
			if (sgaplayer.Redmanastar < 1)
			return true;
			else if (sgaplayer.Redmanastar == 1 && SGAWorld.downedSharkvern)
			return true;
			else if (sgaplayer.Redmanastar == 2 && SGAWorld.downedWraiths>3)
			return true;
			return false;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			SGAPlayer sgaplayer = Main.LocalPlayer.GetModPlayer<SGAPlayer>();
			if (sgaplayer.Redmanastar < 1)
			{
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "Your magic attacks have a small chance to inflict 'On-Fire!'"));
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "Usable right away"));
			}
			if (sgaplayer.Redmanastar == 1)
			{
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "You instead inflict 'Thermal Blaze' instead of 'On Fire!'"));
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "Usable after Sharkvern is defeated."));
			}
			if (sgaplayer.Redmanastar == 2)
			{
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "You instead inflict 'Daybroken' instead of 'Thermal Blaze'"));
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "Usable after Luminite Wraith is defeated."));
			}
			if (sgaplayer.Redmanastar > 2)
			{
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "Its power is at its max, and can no longer help you gain strength"));
			}
			else
			{
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "Rare chance to stripe enemy immunity to the above debuff on magic hit."));
				tooltips.Add(new TooltipLine(mod, "RedStarLine", "-Permanent Upgrade-"));

			}
		}
		public override bool UseItem(Player player)
		{
			SGAPlayer sgaplayer = player.GetModPlayer<SGAPlayer>();
			sgaplayer.Redmanastar += 1;
			return true;
		}
	}

	public class Debug1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Debug-reset SGA Player Save Data");
		}

		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
			item.maxStack = 30;
			item.rare = 8;
			item.value = 1000;
			item.useStyle = 2;
			item.useAnimation = 17;
			item.useTime = 17;
			item.useTurn = true;
			item.UseSound = SoundID.Item9;
			item.consumable = true;
		}
		public override bool UseItem(Player player)
		{
			SGAPlayer sgaplayer = player.GetModPlayer<SGAPlayer>();
			sgaplayer.ExpertiseCollected = 0;
			sgaplayer.ExpertiseCollectedTotal = 0;
			sgaplayer.Redmanastar = 0;
			sgaplayer.GenerateNewBossList();
			return true;
		}
		public override string Texture
		{
			get { return "Terraria/Item_" + ItemID.RedSolution; }
		}

	}


}