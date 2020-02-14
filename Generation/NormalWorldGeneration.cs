using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
//using Terraria.GameContent.Generation;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace SGAmod.Generation
{
    public class NormalWorldGeneration
    {

public static void TempleChambers(){

int [] templecord = {1000000,1000000,-1000000,-1000000};
bool firstone=false;

for (int x = 0; x < Main.maxTilesX; x++)
{
for (int y = 0; y < Main.maxTilesY; y++)
{
Tile tile = Framing.GetTileSafely(x,y);
if (Main.tile[x,y].type==TileID.LihzahrdBrick){
templecord[0]=System.Math.Min(templecord[0],x);
templecord[1]=System.Math.Min(templecord[1],y);
templecord[2]=System.Math.Max(templecord[2],x);
templecord[3]=System.Math.Max(templecord[3],y);

}}}

Tile tile1 = Framing.GetTileSafely(templecord[0],templecord[1]); tile1.type=TileID.RainCloud; tile1.active(true);
Tile tile2 = Framing.GetTileSafely(templecord[2],templecord[1]); tile2.type=TileID.RainCloud; tile2.active(true);
Tile tile3 = Framing.GetTileSafely(templecord[0],templecord[3]); tile3.type=TileID.RainCloud; tile3.active(true);
Tile tile4 = Framing.GetTileSafely(templecord[2],templecord[3]); tile4.type=TileID.RainCloud; tile4.active(true);


for (int rooms = 0; rooms < 3+WorldGen.genRand.Next(4); rooms++)
{
bool thisplacegood=false;
int buffersizex=6;
int buffersizey=4;
int [] theplace={0,0};
for (int tries = 0; tries < 500; tries++)
{
buffersizex=10+WorldGen.genRand.Next(7);
buffersizey=8+WorldGen.genRand.Next(3);
thisplacegood=true;
int x=templecord[0]+WorldGen.genRand.Next(templecord[2]-templecord[0]);
int y=templecord[1]+WorldGen.genRand.Next(templecord[3]-templecord[1]);
int xbuffer = -buffersizex;
int ybuffer = -buffersizey;
for (xbuffer = -buffersizex; xbuffer < buffersizex; xbuffer++)
{
for (ybuffer = -buffersizey; ybuffer < buffersizey; ybuffer++)
{
if (Main.tile[x+xbuffer,y+ybuffer].type!=TileID.LihzahrdBrick || !Main.tile[x+xbuffer,y+ybuffer].active()){
thisplacegood=false;
break;
}}}

if (thisplacegood==true){theplace[0]=x; theplace[1]=y; 
break;}
}




if (thisplacegood==true){
for (int xfiller = -buffersizex+3; xfiller < buffersizex-3; xfiller++)
{
for (int yfiller = -buffersizey+3; yfiller < buffersizey-3; yfiller++)
{
Tile tileroomout = Framing.GetTileSafely(theplace[0]+xfiller,theplace[1]+yfiller); tileroomout.active(false);

}}
if (firstone==false){
WorldGen.PlaceObject(theplace[0], theplace[1]+buffersizey-4, SGAmod.Instance.TileType("PrismalStation"), true, 0);
firstone=true;
}else{
int thechest=WorldGen.PlaceChest(theplace[0], theplace[1]+buffersizey-4, 21, false, 16);
if (thechest>0){
                            List<int> loot = new List<int> { 2344, 2345, 2346, 2347, 2348, 2349, 2350, 2351, 2352, 2353, 2354, 2355, 2356, 2359, 301, 302, 303, 304, 305, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 226, 188, 189, 110, 28 };
                            
List<int> lootmain = new List<int> {ItemID.SuperManaPotion, ItemID.SuperHealingPotion,ItemID.HellstoneBar,ItemID.GoldCoin };
List<int> lootextrabonus = new List<int> { ItemID.Arkhalis, ItemID.LizardEgg,ItemID.PlatinumCoin,ItemID.ReindeerBells,ItemID.SuperAbsorbantSponge,ItemID.BottomlessBucket,ItemID.TheAxe };
                            int e = 0;
                            for (int kk = 0; kk < 2 + (Main.expertMode ? 1 : 0); kk += 1)
                            {
                                //for (int i = 0; i < WorldGen.genRand.Next(15, Main.expertMode ? 25 : 30); i += 1)
                                //{
                                    int index = WorldGen.genRand.Next(0, lootmain.Count);
                                    Main.chest[thechest].item[e].SetDefaults(lootmain[index]);
                                    Main.chest[thechest].item[e].stack = WorldGen.genRand.Next(15, Main.expertMode ? 25 : 30);

                                //}
                                e += 1;
                            }
                                    int index2 = WorldGen.genRand.Next(0, lootextrabonus.Count);
                                    Main.chest[thechest].item[e].SetDefaults(lootextrabonus[index2]);

                            //Tile tiletest = Framing.GetTileSafely(theplace[0],theplace[1]); tiletest.type=TileID.RainCloud; tiletest.active(true);
                        }

                    }

}

}














}



	}
}