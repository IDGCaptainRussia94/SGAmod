using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SGAmod.NPCs;
using Idglibrary;

namespace SGAmod.Buffs
{
	public class DragonsMight: ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dragon's Might");
			Description.SetDefault("50% increase to all damage types except Summon damage");
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.magicDamage += 0.5f;
			player.thrownDamage += 0.5f;
			player.meleeDamage += 0.5f;
			player.rangedDamage += 0.5f;
			if (player.buffTime[buffIndex] < 10){
			bool tempimmune = player.buffImmune[BuffID.Weak];
			player.buffImmune[BuffID.Weak] = false;
			player.AddBuff(BuffID.Weak,60*20);
			tempimmune = player.buffImmune[BuffID.Weak] = tempimmune;
			}
		}

		//public override void Update(NPC npc, ref int buffIndex)
		//{
		//	npc.GetGlobalNPC<SGAWorld>(mod).eFlames = true;
		//}
	}
}
