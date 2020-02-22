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
	public class InfinityWarStormbreaker : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("IWS");
			Description.SetDefault("Players arn't meant to have this debuff!");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			//player.statDefense /= 2;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<SGAnpcs>().InfinityWarStormbreaker = true;
		}
	}

	public class SunderedDefense : ModBuff
	{

		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "SGAmod/Buffs/AcidBurn";
			return true;
		}
		public override void SetDefaults()
		{
			DisplayName.SetDefault("IWS");
			Description.SetDefault("Players arn't meant to have this debuff!");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			//player.statDefense /= 2;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<SGAnpcs>().SunderedDefense = true;
		}
	}

	public class DankSlow : ModBuff
	{

		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "SGAmod/Buffs/AcidBurn";
			return true;
		}
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dank Slow");
			Description.SetDefault("Players arn't meant to have this debuff!");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override bool ReApply(NPC npc, int time, int buffIndex)
		{
			npc.buffTime[buffIndex] = (int)Math.Pow(npc.buffTime[buffIndex]+(int)time, 0.98);
			return true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			//player.statDefense /= 2;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<SGAnpcs>().TimeSlow += (npc.buffTime[buffIndex]/(60f*5f));
			npc.GetGlobalNPC<SGAnpcs>().DankSlow = true;
		}
	}

}