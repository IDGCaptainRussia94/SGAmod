using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SGAmod.Tiles
{
    public class UnmanedOreTile: ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 800;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 200;
            TileID.Sets.Ore[Type] = true;
            soundType = 21;
            soundStyle = 1;
            dustType = 0;
            drop = mod.ItemType("UnmanedOre");
            minPick = 55;
            mineResist = 1.25f;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Novus Ore");
            AddMapEntry(new Color(70, 0, 40), name);
        }

        /*public override void RandomUpdate(int i, int j)
        {
            for (int x = -5; x <= 5; x++)
            {
                for (int y = -5; y <= 5; y++)
                {
                    WorldGen.Convert(i + x, j + y, 0, 0);
                    Tile tile = Main.tile[i + x, j + y];
                    if (tile.active() && (tile.type == TileID.Demonite || tile.type == TileID.Crimtane) && Main.rand.Next(3) == 0)
                    {
                        tile.type = (ushort)mod.TileType("VirulentOre");
                        NetMessage.SendTileRange(Main.myPlayer, i + x, j + y, 1, 1);
                    }
                }
            }
        }*/

        public override bool CanExplode(int i, int j)
        {
            return SGAWorld.downedWraiths>0;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)  
        {
            r = 0.5f;
            g = 0f;
            b = 0.2f;
        }
    }
}