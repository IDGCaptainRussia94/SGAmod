using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Buffs
{
	public class Matrix : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Matrix");
			Description.SetDefault("You are coated in a highly flammable substance");
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<SGAPlayer>().Matrix = true;
		}
	}
}