using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Tiles
{
	public class AustraliumOre : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileShine[Type] = 200;
			Main.tileShine2[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileValue[Type] = 750;
			TileID.Sets.Ore[Type] = true;
			soundType = 21;
			soundStyle = 1;
			dustType = 128;
			drop = mod.ItemType("AustraliumOre");
			minPick = 199;
			mineResist = 5f;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Australium");

			AddMapEntry(new Color(255, 255, 128), name);
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}