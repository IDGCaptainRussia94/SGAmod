using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;
using SGAmod.Tiles;
using Idglibrary;

namespace SGAmod
{
    public class SGAWorld : ModWorld
    {
        //Setting up variables for invasion
        public static bool customInvasionUp = false;
        public static bool downedCustomInvasion = false;
        public static bool downedSPinky = false;
        public static bool downedTPD = false;
        public static bool downedHarbinger = false;
        public static bool downedSpiderQueen = false;
        public static bool downedCratrosity = false;
        public static bool downedSharkvern = false;
        public static bool downedCirno = false;
        public static int downedMurk = 0;
        public static int downedCaliburnGuardians = 0;
        public static int downedCaliburnGuardiansPoints = 0;
        public static int[] CaliburnAlterCoordsX = {0,0,0};
        public static int[] CaliburnAlterCoordsY = { 0, 0, 0 };
        public static bool downedMurklegacy = false;
        public static bool tf2cratedrops = false;
        public static int downedWraiths = 0;
        public static int overalldamagedone = 0;
        public static int MoistStonecount = 0;
        public static int tf2quest = 0;
        public static int bossprgressor = 0;
        public static int tf2questcounter = 0;
        public static int[] questvars = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static bool WorldIsTin = false;

        public static int harbingercounter = 0;
        public static int golemchecker = 0;
        public static int stolecrafting = 0;
        public static int modtimer = 0;
        public static bool GennedVirulent=false;
        public static int NightmareHardcore=0;
        public static int[] oretypesprehardmode = { TileID.Copper, TileID.Iron, TileID.Silver, TileID.Gold };

        //Initialize all variables to their default values
        public override void Initialize()
        {
            Main.invasionSize = 0;
            customInvasionUp = false;
            downedCustomInvasion = false;
            downedSPinky = false;
            downedTPD = false;
            downedSpiderQueen = false;
            downedHarbinger = false;
            downedWraiths = 0;
            downedMurk = 0;
            downedMurklegacy = false;
            downedCaliburnGuardians = 0;
            downedCaliburnGuardiansPoints = 0;
            downedCirno = false;
            downedSharkvern = false;
            overalldamagedone = 0;
            downedCratrosity = false;
            tf2cratedrops = false;
            tf2quest = 0;
            bossprgressor = 0;
            modtimer = 0;
            for (int f = 0; f < CaliburnAlterCoordsX.Length; f++)
            {
                CaliburnAlterCoordsX[f] = 0;
                CaliburnAlterCoordsY[f] = 0;
            }
        WorldIsTin = WorldGen.oreTier1 != TileID.Copper;
            int x = 0;
            for (x = 0; x < questvars.Length; x++) {
                questvars[x] = 0;
            }
        }


        public static void QuestCheck(int questtype, Player ply)
        {
            ply.GetModPlayer<SGAPlayer>().upgradetf2();
            if (tf2questcounter < 1) {
                if (questtype == 0) {
                    if (tf2quest < 1 && GetInstance<SGAConfig>().Contracker) {
                        tf2quest = 1;
                        SgaLib.Chat("<Contracker> The TF2 questline has began!", 255, 255, 255);
                    }
                    if (tf2quest == 2) {
                        if (questvars[0] < 1000) { SgaLib.Chat("<Contracker> You have killed " + questvars[0] + "/1000 enemies", 255, 255, 255);
                        } else {
                            questvars[0] = 0;
                            tf2quest = 3;
                        }
                    }
                    if (tf2quest == 4) {
                        int goldcount = ply.CountItem(ItemID.GoldBar);
                        if (goldcount < 100) {
                            SgaLib.Chat("<Contracker> You have " + goldcount + "/100 Gold Bars", 255, 255, 255);
                        } else {
                            for (int x = 0; x < 100; x++) { ply.ConsumeItem(ItemID.GoldBar); }
                            tf2quest = 5;
                            SgaLib.Chat("Your world has been blessed with Australium!", 255, 255, 102);
                            GenAustralium();
                        }
                    }
                }



            }
        }


        public override void PostUpdate()
        {
            WorldIsTin = (WorldGen.CopperTierOre == 7 ? false : true) ;
            SGAWorld.modtimer +=1;
            if (Main.dayTime == true) {
                harbingercounter = 0;
            }
            if (NPC.CountNPCS(NPCID.Golem) > 0 && SGAConfig.Instance.GolemImprovement) {
                golemchecker = 1;
                if (NPC.CountNPCS(mod.NPCType("SGAGolemBoss")) < 1) {
                    NPC myowner = Main.npc[NPC.FindFirstNPC(NPCID.Golem)];
                    NPC.NewNPC((int)myowner.position.X, (int)myowner.position.Y, mod.NPCType("SGAGolemBoss"));
                    //Main.NewText("Test: modded golem npc spawned", 25, 25, 80);
                }
            } else {
                golemchecker = 0;
            }

            harbingercounter += 1;
            stolecrafting += 1;
            if (Main.netMode < 1) {
                if (harbingercounter == 5) {
                    if (Main.rand.Next(0, 10) < 5 && bossprgressor == 1 && downedHarbinger == false && DD2Event.DownedInvasionT3 && NPC.downedMartians) {
                        harbingercounter = -600;
                        Idglib.Chat("You feel a darker presence watching over you...", 0, 0, 75);
                    } }
                if (harbingercounter == -5) {
                    harbingercounter = 6;
                    SGAmod.CalamityNoRevengenceNoDeathNoU();
                    NPC.SpawnOnPlayer(Main.rand.Next(0, Main.PlayerList.Count), mod.NPCType("Harbinger"));
                } }

            if (stolecrafting == -400)
                Idglib.Chat("Bet you were expecting him to drop an Ancient Manipulator huh?", 25, 25, 80);
            if (stolecrafting == -200)
                Idglib.Chat("Welp, we stole that from him, come fight us if you want it", 25, 25, 80);
            if (stolecrafting == -50)
                Idglib.Chat("We want our wraith core fragments back you son of a bitch...", 25, 25, 80);

            if (tf2quest == 1) {
                tf2questcounter = tf2questcounter + 1;
                if (tf2questcounter == 60) { SgaLib.Chat("<Administrator> Greeting mercenary", 150, 150, 150); }
                if (tf2questcounter == 150) { SgaLib.Chat("<Administrator> You have just agreed to our contract terms", 150, 150, 150); }
                if (tf2questcounter == 280) { SgaLib.Chat("<Administrator> To fight for our new division, Terraria Co", 150, 150, 150); }
                if (tf2questcounter == 510) { SgaLib.Chat("<Administrator> I'll spare you the details, other than your first job", 150, 150, 150); }
                if (tf2questcounter == 640) { SgaLib.Chat("<Administrator> Kill a total of 1000 enemies; prove your even worth looking at", 150, 150, 150); }
                if (tf2questcounter == 770) { SgaLib.Chat("<Administrator> When your done, check your contracter. DO NOT Disapointment me...", 150, 150, 150); }
                if (tf2questcounter == 850) { tf2quest = 2; tf2questcounter = 0; }
            }
            if (tf2quest == 3) {
                tf2questcounter = tf2questcounter + 1;
                if (tf2questcounter == 60) { SgaLib.Chat("<Administrator> Very good", 150, 150, 150); }
                if (tf2questcounter == 150) { SgaLib.Chat("<Administrator> You've proven your, somewhat confident in your work", 150, 150, 150); }
                if (tf2questcounter == 280) { SgaLib.Chat("<Administrator> Now then, if I am to take back Mann Co I need resources", 150, 150, 150); }
                if (tf2questcounter == 510) { SgaLib.Chat("<Administrator> Most effectively... Australium...", 150, 150, 150); }
                if (tf2questcounter == 640) { SgaLib.Chat("<Administrator> I have added functions to your contracker, it will reveal veins of Australium in this world when used", 150, 150, 150); }
                if (tf2questcounter == 770) { SgaLib.Chat("<Administrator> You need to have 100 Gold Ingots in your inventory for it to work; it will not take Platinum!", 150, 150, 150); }
                if (tf2questcounter == 900) { SgaLib.Chat("<Administrator> However accessing this function may attract unwanted attention too early, I'm sure it's nothing you can't handle", 150, 150, 150); }
                if (tf2questcounter == 1030) { SgaLib.Chat("<Administrator> When your done, check your contracter. Try not to be a Disapointment.", 150, 150, 150); }
                if (tf2questcounter == 1050) { tf2quest = 4; tf2questcounter = 0; }
            }
            if (tf2quest == 5) {
                tf2questcounter = tf2questcounter + 1;
                if (tf2questcounter == 60) { SgaLib.Chat("<Administrator> Australium... A bountiful ammount of 100 veins", 150, 150, 150); }
                if (tf2questcounter == 150) { SgaLib.Chat("<Administrator> My plan is going accordingly", 150, 150, 150); }
                if (tf2questcounter == 280) { SgaLib.Chat("<Administrator> I think your due a promotion, to Commando", 150, 150, 150); }
                if (tf2questcounter == 510) { SgaLib.Chat("<Administrator> You have been granted knowledge of how to make higher tier items, including more TF2 emblems", 150, 150, 150); }
                if (tf2questcounter == 640) { SgaLib.Chat("<Administrator> Now for the next phase. I need 50 Australium bars", 150, 150, 150); }
                if (tf2questcounter == 770) { SgaLib.Chat("<Administrator> Minedown and smelt the ores you find, then report back right away", 150, 150, 150); }
                if (tf2questcounter == 900) { SgaLib.Chat("<Administrator> However accessing this function may attract unwanted attention too early, I'm sure it's nothing you can't handle", 150, 150, 150); }
                if (tf2questcounter == 1030) { SgaLib.Chat("<Administrator> When your done, check your contracter. Try not to be a Disapointment.", 150, 150, 150); }
                if (tf2questcounter == 1050) { tf2quest = 6; tf2questcounter = 0; }
            }


        }

        //Save downed data
        public override TagCompound Save()
        {
            //var downed = new List<string>();
            // if (downedCustomInvasion) downed.Add("customInvasion");
            //if (downedSPinky) downed.Add("downedSPinky");
            //if (downedTPD) downed.Add("downedTPD");
            TagCompound tag = new TagCompound();
            tag["downedCustomInvasion"] = downedCustomInvasion;
            tag["downedSPinky"] = downedSPinky;
            tag["downedTPD"] = downedTPD;
            tag["downedCirno"] = downedCirno;
            tag["downedSharkvern"] = downedSharkvern;
            tag["overalldamagedone"] = overalldamagedone;
            tag["downedCratrosity"] = downedCratrosity;
            tag["downedHarbinger"] = downedHarbinger;
            tag["downedMurk"] = downedMurklegacy;
            tag["downedMurk2"] = downedMurk;
            tag["downedWraiths"] = downedWraiths;
            tag["tf2quest"] = tf2quest;
            tag["bossprgressor"] = bossprgressor;
            tag["GennedVirulent"] = GennedVirulent; 
            tag["downedSpiderQueen"] = downedSpiderQueen; 
            tag["downedCaliburnGuardians"] = downedCaliburnGuardians;
            tag["downedCaliburnGuardiansPoints"] = downedCaliburnGuardiansPoints;
            int x = 0;
            for (x = 0; x < questvars.Length; x++)
            {
                string tagname = "questvars" + ((string)x.ToString());
                tag[tagname] = questvars[x];
            }
            tag["tf2cratedrops"] = tf2cratedrops;
            for (x = 0; x < CaliburnAlterCoordsX.Length; x++)
            {
                string tagname = "CaliburnAlterCoordsX_" + ((string)x.ToString());
                tag[tagname] = CaliburnAlterCoordsX[x];
                string tagname2 = "CaliburnAlterCoordsY_" + ((string)x.ToString());
                tag[tagname2] = CaliburnAlterCoordsY[x];
            }
            for (x = 0; x < oretypesprehardmode.Length; x++)
            {
                string tagname = "oretypesprehardmode" + ((string)x.ToString());
                tag[tagname] = oretypesprehardmode[x];
            }           
            return tag;
            //return new TagCompound {
            //    {"downed", downed}
            //};
        }

        //Load downed data
        public override void Load(TagCompound tag)
        {
            WorldIsTin = WorldGen.oreTier1 == TileID.Tin;
            //var downed = tag.GetList<string>("downed");
            downedCustomInvasion = tag.GetBool("customInvasion");
            downedSPinky = tag.GetBool("downedSPinky");
            downedTPD = tag.GetBool("downedTPD");
            downedCirno = tag.GetBool("downedCirno");
            downedSharkvern = tag.GetBool("downedSharkvern");
            if (tag.ContainsKey("overalldamagedone")) { overalldamagedone = tag.GetInt("overalldamagedone"); }
            downedCratrosity = tag.GetBool("downedCratrosity");
            downedHarbinger = tag.GetBool("downedHarbinger");
            downedMurklegacy = tag.GetBool("downedMurk");
            downedMurk = tag.GetInt("downedMurk2");
            downedSpiderQueen = tag.GetBool("downedSpiderQueen");
            downedCaliburnGuardians = tag.GetInt("downedCaliburnGuardians");
            downedCaliburnGuardiansPoints = tag.GetInt("downedCaliburnGuardiansPoints");
            if (tag.ContainsKey("downedWraiths")) { downedWraiths = tag.GetInt("downedWraiths"); }
            if (tag.ContainsKey("tf2quest")) { tf2quest = 0; }//tag.GetInt("tf2quest");}
            if (tag.ContainsKey("bossprgressor")) { bossprgressor = tag.GetInt("bossprgressor"); }
            if (tag.ContainsKey("GennedVirulent")) { GennedVirulent = tag.GetBool("GennedVirulent"); }
            int x = 0;
            for (x = 0; x < questvars.Length; x++)
            {
                string tagname = "questvars" + ((string)x.ToString());
                if (tag.ContainsKey(tagname)) { questvars[x] = tag.GetInt(tagname); }
            }
            tf2cratedrops = tag.GetBool("tf2cratedrops");
            for (x = 0; x < CaliburnAlterCoordsX.Length; x++)
            {
                string tagname = "CaliburnAlterCoordsX_" + ((string)x.ToString());
                if (tag.ContainsKey(tagname)) { CaliburnAlterCoordsX[x] = tag.GetInt(tagname); }
                tagname = "CaliburnAlterCoordsY_" + ((string)x.ToString());
                if (tag.ContainsKey(tagname)) { CaliburnAlterCoordsY[x] = tag.GetInt(tagname); }
            }
            for (x = 0; x < oretypesprehardmode.Length; x++)
            {
                string tagname = "oretypesprehardmode" + ((string)x.ToString());
                if (tag.ContainsKey(tagname)) { oretypesprehardmode[x] = tag.GetInt(tagname); }
            }

        }

        //Sync downed data
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte(); flags[0] = downedCustomInvasion; flags[1] = downedSPinky; flags[2] = downedTPD; flags[3] = downedCratrosity; flags[4] = downedCirno; flags[5] = downedSharkvern; flags[6] = downedHarbinger; writer.Write(flags);
            flags[7] = GennedVirulent; writer.Write(flags);
            writer.Write(overalldamagedone);
            writer.Write(downedWraiths);
            writer.Write(downedMurk);
            writer.Write(downedCaliburnGuardians);
            writer.Write(downedCaliburnGuardiansPoints);
            writer.Write(tf2quest);
            writer.Write(bossprgressor);
            writer.Write(tf2cratedrops);
            writer.Write(modtimer);
             BitsByte flags2 = new BitsByte(); flags[0] = downedSpiderQueen;
            writer.Write(flags2);
            int x = 0;

            for (x = 0; x < questvars.Length; x++)
            {
                writer.Write(questvars[x]);
            }
            for (x = 0; x < CaliburnAlterCoordsX.Length; x++)
            {
                writer.Write(CaliburnAlterCoordsX[x]);
                writer.Write(CaliburnAlterCoordsY[x]);
            }
            for (x = 0; x < oretypesprehardmode.Length; x++)
            {
                writer.Write(oretypesprehardmode[x]);
            }
        }

        //Sync downed data
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte(); downedCustomInvasion = flags[0]; downedSPinky = flags[1]; downedTPD = flags[2]; downedCratrosity = flags[3]; downedCirno = flags[4]; downedSharkvern = flags[5]; downedHarbinger = flags[6];
            GennedVirulent = flags[7];
            overalldamagedone = reader.ReadInt32();
            downedWraiths = reader.ReadInt32();
            downedMurk = reader.ReadInt32();
            downedCaliburnGuardians = reader.ReadInt32();
            downedCaliburnGuardiansPoints = reader.ReadInt32();
            tf2quest = reader.ReadInt32();
            bossprgressor = reader.ReadInt32();
            tf2cratedrops = reader.ReadBoolean();
            modtimer = reader.ReadInt32();
            BitsByte flags2 = reader.ReadByte(); downedSpiderQueen = flags2[0];
            int x = 0;
            for (x = 0; x < questvars.Length; x++)
            {
                tf2quest = reader.ReadInt32();
            }
            for (x = 0; x < CaliburnAlterCoordsX.Length; x++)
            {
                CaliburnAlterCoordsX[x] = reader.ReadInt32();
                CaliburnAlterCoordsY[x] = reader.ReadInt32();
            }
            for (x = 0; x < oretypesprehardmode.Length; x++)
            {
                oretypesprehardmode[x] = reader.ReadInt32();
            }
        }


        public static void GenAustralium()
        {
            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }
            for (double k = 0; k < (100); k += 1.0)
            {
                WorldGen.OreRunner(WorldGen.genRand.Next(100, Main.maxTilesX - 100), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 150), (double)WorldGen.genRand.Next(3, 3), WorldGen.genRand.Next(3, 6), (ushort)SGAmod.Instance.TileType("AustraliumOre"));
            }
            if (Main.netMode == 2)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
        }

        //this and the next one were based off BlushieMagic's code, god bless!
        public static void GenVirulent()
        {

            if (Main.netMode == 1 || WorldGen.noTileActions || WorldGen.gen || !Main.hardMode || GennedVirulent)
            {
                return;
            }

            //WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), TileType<ExampleOre>(), false, 0f, 0f, false, true);
            for (double k = 0; k < (Main.maxTilesX - 200) * (Main.maxTilesY - 150 - (int)Main.rockLayer) / 25.0 / 1.0; k += 1.0)
            {
                int genx = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int geny = WorldGen.genRand.Next((int)Main.rockLayer, (int)Main.maxTilesY - 150);
                Tile tile = Framing.GetTileSafely(genx, geny);
                int chance = 0;
                if (tile.active() && (tile.type == TileID.Mud))
                {
                    chance = 3;
                }

                if (Main.rand.Next(0, 100) < chance)
                    WorldGen.TileRunner(genx, geny, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(5, 16), TileType<VirulentOre>(), false, 0f, 0f, false, true);
            }
            if (Main.netMode == 2)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
            GennedVirulent = true;
            Idglib.Chat("The raw power of the empowered Murk has seeped into the jungle underground!", 75, 225, 75);
        }

        public static void GenNovus()
        {

            //WorldGen.TileRunner(x, y, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), TileType<ExampleOre>(), false, 0f, 0f, false, true);
            for (double k = 0; k < (Main.maxTilesX - 200) * (Main.maxTilesY - 150 - (int)Main.rockLayer) / 25.0 / 1.0; k += 1.0)
            {
                int genx = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                int geny = WorldGen.genRand.Next((int)0, (int)Main.rockLayer + 150);
                 Tile tile = Framing.GetTileSafely(genx, geny);
                int chance = 0;
                int tiletype = TileType<UnmanedOreTile>();
                if (tile.active() && (tile.type == TileID.Dirt || tile.type == TileID.Stone))
                {
                    chance = 2;
                        if (tile.active() && (geny < WorldGen.worldSurfaceLow || (WorldGen.genRand.Next(0, 1000) < 2)))
                        {
                            chance = 100;
                            tiletype=TileType<Biomass>();
                    }
                }

                if (WorldGen.genRand.Next(0,100)<chance)
                WorldGen.TileRunner(genx, geny, (double)WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(5, 16), tiletype, false, 0f, 0f, false, true);
            }
        }

        public static int RaycastDownWater(int x, int y, int check)
        {
            while (Main.tile[x, y].liquid < check)
            {
                y++;
            }
            return y;
        }


        public override void ResetNearbyTileEffects()
        {
            MoistStonecount = 0;
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            SGAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<SGAPlayer>();
            MoistStonecount = tileCounts[mod.TileType("MoistStone")];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            if (Main.worldName == "Mannhattan") {
                Generation.Mannhattan.GenMannhattan(tasks);
            } else {


                int Shinies = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
                tasks.Insert(Shinies + 1, new PassLegacy("Novus Ore", delegate (GenerationProgress progress)
                {
                    progress.Message = "Planting Novus Ore";
                    GenNovus();
                }));

                int SecretChambers = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Temple"));
                tasks.Insert(SecretChambers + 1, new PassLegacy("Secret Chambers", delegate (GenerationProgress progress)
                  {
                      progress.Message = "Hiding Secret Chambers";
                      Generation.NormalWorldGeneration.TempleChambers();
                  }));
                int CaliburnShrines = tasks.FindIndex(genpass => genpass.Name.Equals("Pots"));
                tasks.Add(new PassLegacy("Caliburn Shrines", delegate (GenerationProgress progress)
                  {
                      progress.Message = "Hiding Caliburn's Gifts";
                      Generation.NormalWorldGeneration.GenAllCaliburnShrine();
                  }));       
            
            }

        }

        public override void PostWorldGen()
        {
            oretypesprehardmode[0]= WorldGen.CopperTierOre;
            oretypesprehardmode[1] = WorldGen.IronTierOre;
            oretypesprehardmode[2] = WorldGen.SilverTierOre;
            oretypesprehardmode[3] = WorldGen.GoldTierOre;

            for (int ii = 0; ii < Main.rand.Next(1, 2); ii++)
            {
                int[] itemsToPlaceInOvergrownChestsSecond = new int[] { mod.ItemType("ForagersBlade"), mod.ItemType("FiberglassRifle") };
                int itemsToPlaceInOvergrownChestsChoiceSecond = 0;
                for (int chestIndexx = 0; chestIndexx < 1000; chestIndexx++)
                {
                    Chest chest = Main.chest[chestIndexx];
                    if (chest != null && (WorldGen.genRand.Next(0, 100) < 20 || Main.tile[chest.x, chest.y - 1].wall == mod.TileType("SwampWall")))
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                itemsToPlaceInOvergrownChestsChoiceSecond = Main.rand.Next(itemsToPlaceInOvergrownChestsSecond.Length);
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInOvergrownChestsSecond[itemsToPlaceInOvergrownChestsChoiceSecond]);
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = 1; i < 3; i++)
            {
                int[] itemsToPlaceInOvergrownChestsSecond = new int[] {mod.ItemType("DragonsMightPotion"), mod.ItemType("DecayedMoss"), mod.ItemType("DecayedMoss") };
                int itemsToPlaceInOvergrownChestsChoiceSecond = 0;
                for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
                {
                    Chest chest = Main.chest[chestIndex];
                    if (chest != null && (WorldGen.genRand.Next(0,100)<10 || Main.tile[chest.x, chest.y-1].wall == mod.TileType("SwampWall")))
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                itemsToPlaceInOvergrownChestsChoiceSecond = Main.rand.Next(itemsToPlaceInOvergrownChestsSecond.Length);
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInOvergrownChestsSecond[itemsToPlaceInOvergrownChestsChoiceSecond]);
                                chest.item[inventoryIndex].stack = Main.rand.Next(3, 4);
                                break;
                            }
                        }
                    }
                }
            }

            /*for (int i = 1; i < Main.rand.Next(4, 6); i++)
            {
                int[,] itemsToPlaceInOvergrownChestsSecond = new int[,] { { mod.ItemType("ForagersBlade"),0 }, { mod.ItemType("FiberglassRifle"),0 }, {mod.ItemType("DragonsMightPotion"),3 }, {mod.ItemType("DecayedMoss"),3} };
                int itemsToPlaceInOvergrownChestsChoiceSecond = 0;
                for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
                {
                    Chest chest = Main.chest[chestIndex];
                    if (chest != null && WorldGen.genRand.Next(0,100)< 40)//Main.tile[chest.x, chest.y].type == mod.TileType("OvergrownChest"))
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                itemsToPlaceInOvergrownChestsChoiceSecond = Main.rand.Next(itemsToPlaceInOvergrownChestsSecond.Length);
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInOvergrownChestsSecond[itemsToPlaceInOvergrownChestsChoiceSecond,0]);
                                chest.item[inventoryIndex].stack = Main.rand.Next(1, 1+itemsToPlaceInOvergrownChestsSecond[itemsToPlaceInOvergrownChestsChoiceSecond,1]);
                                break;
                            }
                        }
                    }
                }
            }*/

        }





        }
}