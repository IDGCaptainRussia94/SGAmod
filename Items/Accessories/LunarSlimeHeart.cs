using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SGAmod.Items.Accessories
{
	public class LunarSlimeHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Slime Heart");
			Tooltip.SetDefault("Heart of the surpreme lunar princess" +
				"\nSummons an array of lunar gels that damage enemies and cancel out projectiles\nWhen a projectile is canceled out or hits an enemy 5 times, the gel explodes into a damaging debuffing nova and grants the player a random buff for 8 seconds\nWhen the gel explodes from canceling out projectiles remains inactive for 10 seconds, otherwise only 6 seconds\nBase damage is based on your defense times the sum of your damage multipliers: (melee+thrown+summon+magic+ranged)*defense\nEach buff the player has grants +1 defense\ndebuffs grant 4 defense");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = 1000000;
			item.rare = -12;
			item.expert = true;
			item.accessory = true;
			item.defense = 10;
			item.damage = 1;
			item.knockBack = 1f;
		}


		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
		flat=(float)(player.statDefense*(player.minionDamage+ player.rangedDamage+ player.meleeDamage+ player.thrownDamage+ player.magicDamage));
		base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
	}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
		player.GetModPlayer<SGAPlayer>().lunarSlimeHeart = true;
		}
	}
}