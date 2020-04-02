using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Idglibrary;

namespace SGAmod.NPCs.Murk
{
    [AutoloadBossHead]
    public class Murk : ModNPC
	{
        int counter = 0;
        public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 6000;
            if (Main.hardMode)
            npc.lifeMax = 10000;
            npc.damage = 100;
			npc.defense = 14;
            npc.knockBackResist = 0.0f;
            npc.dontTakeDamage = false;
			npc.npcSlots = 50f;
            npc.width = 126;
            npc.height = 134;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = false;
			npc.noTileCollide = false;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.HitSound = SoundID.NPCHit1;
            npc.alpha = 0;
            //aiType = NPCID.KingSlime;
            animationType = NPCID.KingSlime;			
			NPCID.Sets.MustAlwaysDraw[npc.type] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Murk");
            bossBag = mod.ItemType("MurkBossBag");
            npc.value = Item.buyPrice(0, 7, 50, 0);
            npc.buffImmune[mod.BuffType("AcidBurn")] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Murk");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.KingSlime];
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
        potionType=ItemID.RestorationPotion;
        }

        public void CustomBehavior(ref float ai)
		{
			Player player = Main.player[npc.target];
        }

        public override void NPCLoot()
        {
            for (int i = 0; i < (Main.hardMode ? 2 : 1); i += 1)
            {
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
                else
                {

                    for (int f = 0; f < (Main.rand.Next(30, 45)); f = f + 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MurkyGel"));
                    }

                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MurkFlail"));
                    else if (choice == 1)
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mossthorn"));
                    else if (choice == 2)
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Landslide"));
                    else if (choice == 3)
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mudmore"));
                }
            }
            Achivements.SGAAchivements.UnlockAchivement("Murk", Main.LocalPlayer);
            if (SGAWorld.downedMurk==0)
                Idglib.Chat("The Moist Stone around Dank Shrines has weakened and can be broken.", 75, 225, 75);
            SGAWorld.downedMurk = Main.hardMode ? 2 : 1;
            SGAWorld.GenVirulent();
    }


        public override void AI()
        {
            float num644 = 1f;
            bool flag65 = false;
            bool flag66 = false;
            bool touchingground=false;
            npc.aiAction = 0;
            npc.localAI[0]+=npc.localAI[0]>-1 ? 1 : -1;
            int dustype=mod.DustType("MangroveDust");
            if (npc.localAI[0]>0)
            dustype=184;

            if (npc.ai[3] == 0f && npc.life > 0)
            {
                npc.ai[3] = (float)npc.lifeMax;

                    if (Main.netMode!=1){

                            int num664 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Murk2"), 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[num664].ai[1] = npc.whoAmI;
                            Main.npc[num664].netUpdate=true;
                            if (Main.netMode == 2 && num664 < 200)
                            {
                                NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                            }

                    }

            }

            if (npc.localAI[3] == 0f && Main.netMode != 1)
            {
                npc.ai[0] = -100f;
                npc.localAI[3] = 1f;
                npc.TargetClosest(true);
                npc.netUpdate = true;
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.timeLeft = 0;
                    if (Main.player[npc.target].Center.X < npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                }
            }

            if (!Main.player[npc.target].dead && npc.ai[2] >= 300f && npc.ai[1] < 5f && npc.velocity.Y == 0f)
            {
                npc.ai[2] = 0f;
                npc.ai[0] = 0f;
                npc.ai[1] = 5f;
                if (Main.netMode != 1)
                {
                    npc.TargetClosest(false);
                    Point point5 = npc.Center.ToTileCoordinates();
                    Point point6 = Main.player[npc.target].Center.ToTileCoordinates();
                    Vector2 vector65 = Main.player[npc.target].Center - npc.Center;


                    int num645 = 10;
                    int num646 = 0;
                    int num647 = 7;
                    int num648 = 0;
                    bool flag67 = false;
                    if (vector65.Length() > 2000f)
                    {
                        flag67 = true;
                        num648 = 100;
                    }
                    while (!flag67 && num648 < 100)
                    {
                        num648++;
                        int num649 = Main.rand.Next(point6.X - num645, point6.X + num645 + 1);
                        int num650 = Main.rand.Next(point6.Y - num645, point6.Y + 1);
                        if ((num650 < point6.Y - num647 || num650 > point6.Y + num647 || num649 < point6.X - num647 || num649 > point6.X + num647) && (num650 < point5.Y - num646 || num650 > point5.Y + num646 || num649 < point5.X - num646 || num649 > point5.X + num646) && !Main.tile[num649, num650].nactive())
                        {
                            int num651 = num650;
                            int num652 = 0;
                            bool flag68 = Main.tile[num649, num651].nactive() && Main.tileSolid[(int)Main.tile[num649, num651].type] && !Main.tileSolidTop[(int)Main.tile[num649, num651].type];
                            if (flag68)
                            {
                                num652 = 1;
                            }
                            else
                            {
                                while (num652 < 150 && num651 + num652 < Main.maxTilesY)
                                {
                                    int num653 = num651 + num652;
                                    bool flag69 = Main.tile[num649, num653].nactive() && Main.tileSolid[(int)Main.tile[num649, num653].type] && !Main.tileSolidTop[(int)Main.tile[num649, num653].type];
                                    if (flag69)
                                    {
                                        num652--;
                                        break;
                                    }
                                    num652++;
                                }
                            }
                            num650 += num652;
                            bool flag70 = true;
                            if (flag70 && Main.tile[num649, num650].lava())
                            {
                                flag70 = false;
                            }
                            if (flag70 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                            {
                                flag70 = false;
                            }
                            if (flag70)
                            {
                                npc.localAI[1] = (float)(num649 * 16 + 8);
                                npc.localAI[2] = (float)(num650 * 16 + 16);
                                break;
                            }
                        }
                    }
                    if (num648 >= 100)
                    {
                        Vector2 bottom = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
                        npc.localAI[1] = bottom.X;
                        npc.localAI[2] = bottom.Y;
                    }
                }
            }
            if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
            {
                npc.ai[2] += 1f;
            }
            if (Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 320f)
            {
                npc.ai[2] += 1f;
            }

            if (Main.netMode != 1)
            {
                bool flag80 = false;
                int num70 = 0;
                num70 = num70 + 1;
                Vector2 vector65 = Main.player[npc.target].Center - npc.Center;
                bool flag71 = false;
                bool flag81 = true;

                if (npc.life < npc.lifeMax / 2 && Main.expertMode == true)
                {
                    npc.damage = 200;
                    npc.defense = 30;
                    npc.knockBackResist = 0.0f;
                    if (npc.life <= 1000)
                    {
                        flag80 = true;
                    }
                }
                else if (npc.life < npc.lifeMax / 2 && Main.expertMode == false)
                {
                    npc.damage = 150;
                    npc.defense = 20;
                }

                if (npc.life <= npc.lifeMax/5)
                {
                    if (flag81 == true)
                    {
                        flag71 = true;
                    }
                }

                if (flag71 == true)
                {
                    npc.defense = 10;
                    flag81 = false;
                    flag71 = false;
                    if (counter++ == 10)
                    {

                    }
                }

                /*if (!Main.player[npc.target].ZoneJungle)
                {
                    npc.defense += 75;
                }*/

                if (Main.expertMode == true && npc.life <= npc.lifeMax / 10)
                {
                    if (num70 >= 75)
                    {
                        npc.life += 5;
                        num70 = 0;
                    }
                }
                //if (!Main.player[npc.target].ZoneJungle)
                //npc.defense=100;
            }

            if (npc.ai[1] == 5f)
            {
                flag65 = true;
                npc.aiAction = 1;
                npc.ai[0] += 1f;
                num644 = MathHelper.Clamp((60f - npc.ai[0]) / 60f, 0f, 1f);
                num644 = 0.5f + num644 * 0.5f;
                if (npc.ai[0] >= 60f)
                {
                    flag66 = true;
                }
                /* if (npc.ai[0] == 60f)
                {
                    Gore.NewGore(npc.Center + new Vector2(-40f, (float)(-(float)npc.height / 2)), npc.velocity, 734, 1f);
                } */
                if (npc.ai[0] >= 60f && Main.netMode != 1)
                {
                    npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                }
                if (Main.netMode == 1 && npc.ai[0] >= 120f)
                {
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                }
                if (!flag66)
                {
                    for (int num654 = 0; num654 < 10; num654++)
                    {
                        int num655 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, dustype, npc.velocity.X, npc.velocity.Y, 100, new Color(30, 30, 30, 20), 2f);
                        Main.dust[num655].noGravity = true;
                        Main.dust[num655].velocity *= 0.5f;
                    }
                }
            }
            else if (npc.ai[1] == 6f)
            {
                flag65 = true;
                npc.aiAction = 0;
                npc.ai[0] += 1f;
                num644 = MathHelper.Clamp(npc.ai[0] / 30f, 0f, 1f);
                num644 = 0.5f + num644 * 0.5f;
                if (npc.ai[0] >= 30f && Main.netMode != 1)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
                if (Main.netMode == 1 && npc.ai[0] >= 60f)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.TargetClosest(true);
                }
                for (int num656 = 0; num656 < 10; num656++)
                {
                    int num657 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, dustype, npc.velocity.X, npc.velocity.Y, 100, new Color(30, 30, 30, 20), 2f);
                    Main.dust[num657].noGravity = true;
                    Main.dust[num657].velocity *= 2f;
                }
            }

            npc.dontTakeDamage = ((npc.hide = flag66) || (NPC.CountNPCS(mod.NPCType("BossFlyMiniboss1"))>0 && Main.hardMode));
            if (Main.hardMode)
            npc.GivenName = "Murk: Lord of the flies";

            if (npc.velocity.Y == 0f)
            {
            touchingground=true;
                npc.velocity.X = npc.velocity.X * 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (!flag65)
                {
                    if (npc.life<(npc.lifeMax*0.75) && npc.localAI[0]>-600)
                    {
                    npc.ai[0] = -100f;
                    npc.ai[2] = 0f;
                    if (npc.localAI[0]>0)
                    npc.localAI[0]=-1;
                    //npc.dontTakeDamage=true;
                    npc.defense*=2;

                    if ((-npc.localAI[0])%15==0)
                    {
                    npc.TargetClosest(true);
                    npc.netUpdate=true;
                    Player target=Main.player[npc.target];
                    List<Projectile> itz2=Idglib.Shattershots(npc.Center,target.position,new Vector2(target.width/2,target.height/2),ProjectileID.HornetStinger,15,15f,0,1,true,0f,false,300);
                    Main.PlaySound(SoundID.Item42,npc.Center);

                        for (int num656 = 0; num656 < 15; num656++)
                        {
                        int num657 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 184, itz2[0].velocity.X, itz2[0].velocity.Y, 100, new Color(80, 80, 80, 100), 1.5f);
                        Main.dust[num657].noGravity = true;

                        Main.dust[num657].velocity = (new Vector2(itz2[0].velocity.X, itz2[0].velocity.Y)*2)*((float)num656/15);
                        }
                    }

                        for (int xx = 0; xx < 5; xx++)
                        {
                        int num657 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, mod.DustType("MangroveDust"), npc.velocity.X, npc.velocity.Y, 100, new Color(30, 30, 30, 20), 1f);
                        Main.dust[num657].noGravity = true;
                        Main.dust[num657].velocity *= 2f;
                        }

                    }

                    npc.ai[0] += 2f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.8)
                    {
                        npc.ai[0] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.6)
                    {
                        npc.ai[0] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.4)
                    {
                        npc.ai[0] += 2f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.2)
                    {
                        npc.ai[0] += 3f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.1)
                    {
                        npc.ai[0] += 4f;
                    }
                    if (npc.ai[0] >= 0f)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest(true);
                        if (npc.ai[1] == 3f)
                        {
                            npc.velocity.Y = -13;
                            npc.velocity.X = npc.velocity.X + 3.5f * (float)npc.direction;
                            npc.ai[0] = -200f;
                            npc.ai[1] = 0f;
                        }
                        else if (npc.ai[1] == 2f)
                        {
                            npc.velocity.Y = -6f;
                            npc.velocity.X = npc.velocity.X + (npc.localAI[0]<0 ? 10f : 4.5f) * (float)npc.direction;
                            npc.ai[0] = -120f;
                            npc.ai[1] += 1f;
                        }
                        else
                        {
                            npc.velocity.Y = -8f;
                            float distz = Main.player[npc.target].Center.Y - npc.Center.Y;
                            if (distz < -96)
                            npc.velocity.Y += (distz-100) / 60f;
                            npc.velocity.X = npc.velocity.X + 4f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            npc.ai[1] += 1f;
                        }
                    }
                    else if (npc.ai[0] >= -30f)
                    {
                        npc.aiAction = 1;
                    }
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X * 0.93f;
                }
            }



            if (Math.Abs(npc.velocity.Y) < 8f && npc.ai[1]==0 && npc.localAI[0]%2==0 && !touchingground && npc.localAI[0]<0)
            {
            double angle=((double)(npc.localAI[0]/13f))+ 2.0* Math.PI;
            List<Projectile> itz=Idglib.Shattershots(npc.Center,npc.Center,new Vector2(0,16),ProjectileID.Stinger,15,5f,0,1,true,(float)angle,true,300);
            itz=Idglib.Shattershots(npc.Center,npc.Center,new Vector2(0,16),ProjectileID.Stinger,15,5f,0,1,true,(float)-angle,true,300);
            }
            if (npc.localAI[0]<-3000)
            npc.localAI[0]=5;

            //Vector2 vector3 = Collision.WaterCollision(npc.position, new Vector2(0,16), npc.width, npc.height, false, false, false);
            if (npc.wet){
            npc.life=(int)MathHelper.Clamp(npc.life+1,0,npc.lifeMax);
            }


            int num658 = Dust.NewDust(npc.position, npc.width, npc.height, dustype, npc.velocity.X, npc.velocity.Y, 100, new Color(30, 30, 30, 20), npc.scale * 1.2f);
            Main.dust[num658].noGravity = true;
            Main.dust[num658].velocity *= 0.5f;
            if (npc.life > 0)
            {
                float num659 = (float)npc.life / (float)npc.lifeMax;
                num659 = num659 * 0.5f + 0.75f;
                num659 *= num644;
                if (num659 != npc.scale)
                {
                    npc.position.X = npc.position.X + (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y + (float)npc.height;
                    npc.scale = num659;
                    npc.width = (int)(98f * npc.scale);
                    npc.height = (int)(92f * npc.scale);
                    npc.position.X = npc.position.X - (float)(npc.width / 2);
                    npc.position.Y = npc.position.Y - (float)npc.height;
                }
                if (Main.netMode != 1)
                {
                    int num660 = (int)((double)npc.lifeMax * 0.04);
                    if ((float)(npc.life + num660) < npc.ai[3])
                    {
                        npc.ai[3] = (float)npc.life;
                        int num661 = Main.rand.Next(1, 4);
                        for (int num662 = 0; num662 < num661; num662++)
                        {
                            int x = (int)(npc.position.X + (float)Main.rand.Next(npc.width - 32));
                            int y = (int)(npc.position.Y + (float)Main.rand.Next(npc.height - 32));
                            int num663 = NPCID.JungleSlime;
                            if (Main.hardMode)
                                num663 = NPCID.SpikedJungleSlime;
                            if (Main.rand.NextBool())
                                num663 = mod.NPCType("SwampSlime");
                            if (npc.localAI[0] < 0 && Main.expertMode)
                            {
                                if (Main.rand.Next(0, 100) < 20)
                                num663 = NPCID.SpikedJungleSlime;
                                if (num663 == NPCID.JungleSlime || num663 == mod.NPCType("SwampSlime"))
                                    num663 = mod.NPCType("BossFly3");
                            }

                            int num664 = NPC.NewNPC(x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[num664].SetDefaults(num663, -1f);
                            Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
                            Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
                            Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
                            Main.npc[num664].ai[1] = 0f;
                            if (num663==mod.NPCType("BossFly3"))
                            Main.npc[num664].ai[1] = npc.whoAmI;

                            Main.npc[num664].netUpdate=true;
                            if (Main.netMode == 2 && num664 < 200)
                            {
                                NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                            }
                        }
                        return;
                    }
                }
            }
        }

        public override bool CheckDead()
        {
            return true;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Main.netMode != 1)
            {
                if (NPC.CountNPCS(mod.NPCType("Fly")) > 30)
                return;
            int x = (int)(npc.position.X + (float)Main.rand.Next(npc.width - 32));
            int y = (int)(npc.position.Y + (float)Main.rand.Next(npc.height - 32));
            int num663 = mod.NPCType("Fly");

            int num664 = NPC.NewNPC(x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
                            if (Main.netMode == 2 && num664 < 200)
                            {
                                NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                            }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
             if (Main.netMode != 1)
                {
                if (NPC.CountNPCS(mod.NPCType("Fly")) > 30)
                return;
                int x = (int)(npc.position.X + (float)Main.rand.Next(npc.width - 32));
            int y = (int)(npc.position.Y + (float)Main.rand.Next(npc.height - 32));
            int num663 = mod.NPCType("Fly");

            int num664 = NPC.NewNPC(x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
                if (npc.life < npc.lifeMax * 0.35 && Main.hardMode)
                {
                    Main.npc[num664].aiStyle = 86;
                    Main.npc[num664].netUpdate = true;
                }

                    if (Main.netMode == 2 && num664 < 200)
                            {
                                NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                            }
        }
        }
    }

    public class Fly : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BeeSmall);
            npc.width = 10;
            npc.height = 10;
            npc.damage = 12;
            npc.defense = 0;
            npc.lifeMax = 15;
            npc.value = 0f;
            npc.noGravity = true;
            npc.aiStyle = 5;
            aiType = NPCID.BeeSmall;
            animationType = NPCID.BeeSmall;
        }

        public override bool PreNPCLoot()
        {
            NPCLoader.blockLoot.Add(ItemID.Heart);
            NPCLoader.blockLoot.Add(ItemID.Star);
            return false;
        }

        public override void AI()
        {
            npc.ai[3] += 1;
            if (npc.ai[3]>60*(Main.expertMode ? 10 : 25))
            {
                Player target = Main.player[npc.target];
                List<Projectile> itz2 = Idglib.Shattershots(npc.Center, target.position, new Vector2(target.width / 2, target.height / 2), ProjectileID.HornetStinger, (int)((float)npc.damage / (Main.hardMode ? 6f : 2f)), 16f, 0, 1, true, 0f, false, 300);
                Main.PlaySound(SoundID.Item42, npc.Center);
                npc.active = false;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fly");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BeeSmall];
        }

    }

    public class Murk2 : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BeeSmall);
            npc.width = 10;
            npc.height = 10;
            npc.damage = 5;
            npc.defense = 0;
            npc.lifeMax = 500000;
            npc.value = 0f;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.dontTakeDamage=true;
            npc.immortal=true;
        }

        public override string Texture
        {
            get { return("SGAmod/NPCs/Murk/Fly");}
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Murk Spawns");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BeeSmall];
        }

        public override bool CheckActive()
        {
        NPC master=Main.npc[(int)npc.ai[1]];
        return (!master.active || npc.ai[1]<1);
        }

        public override void AI()
        {

        npc.ai[0]+=1;
        double angle=((double)(npc.ai[0]/13f))+ 2.0* Math.PI;
        NPC Master = Main.npc[(int)npc.ai[1]];
        if (!Master.active || npc.ai[1]<1)
        npc.active=false;
        npc.Center=Master.Center;

        if (Master.life < Master.lifeMax * 0.35 && npc.ai[2]==0)
            {
                if (Main.netMode != 1)
                {
                    Idglib.Chat("Murk calls for backup with a killer fly swarm!", 103, 128, 79);
                    int x = (int)(Master.position.X + (float)Main.rand.Next(Master.width - 32));
                    int y = (int)(Master.position.Y + (float)Main.rand.Next(Master.height - 32));
                    int num663 = mod.NPCType("BossFlyMiniboss1");

                    int num664 = NPC.NewNPC(x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num664].ai[1] = Master.whoAmI;
                    Main.npc[num664].life = (int)((double)(Master.lifeMax* (Main.hardMode ? 1.5 : 0.5)));
                    Main.npc[num664].lifeMax = Main.npc[num664].life;
                    Main.npc[num664].damage = Main.hardMode ? 80 : 30;
                    Main.npc[num664].netUpdate = true;
                    if (!Main.hardMode)
                    Main.npc[num664].dontTakeDamage = true;
                    if (Main.netMode == 2 && num664 < 200)
                    {
                        NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                    }

                    if (Main.hardMode)
                    {
                        x = (int)(Master.position.X + (float)Main.rand.Next(Master.width - 32));
                        y = (int)(Master.position.Y + (float)Main.rand.Next(Master.height - 32));
                        num663 = mod.NPCType("BossFly1");

                        if (Main.expertMode)
                        {
                            for (int i = 0; i < 10; i += 1)
                            {

                                num664 = NPC.NewNPC(x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
                                Main.npc[num664].ai[0] = Main.rand.NextFloat(50, 450);
                                Main.npc[num664].ai[2] = 10 + (i * 12);
                                Main.npc[num664].ai[1] = Master.whoAmI;
                                Main.npc[num664].damage = 50;
                                Main.npc[num664].netUpdate = true;
                                Main.npc[num664].dontTakeDamage = true;
                                if (Main.netMode == 2 && num664 < 200)
                                {
                                    NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }

                        }


                    }

                    npc.ai[2] = 1;
                    npc.netUpdate = true;
                }
            }



            if (Main.netMode != 1 && npc.ai[0]%300==0 && NPC.CountNPCS(mod.NPCType("BossFly2"))<(Main.expertMode ? 2 : 1) && Master.localAI[0]<0)
            {
            int x = (int)(Master.position.X + (float)Main.rand.Next(Master.width - 32));
            int y = (int)(Master.position.Y + (float)Main.rand.Next(Master.height - 32));
            int num663 = mod.NPCType("BossFly2");

            int num664 = NPC.NewNPC(x, y, num663, 0, 0f, 0f, 0f, 0f, 255);
            Main.npc[num664].ai[1]=Master.whoAmI;
                Main.npc[num664].netUpdate = true;
                if (Main.netMode == 2 && num664 < 200)
                            {
                                NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                            }
            }


        }

    }

    public class BossFly1 : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BeeSmall);
            npc.width = 10;
            npc.height = 10;
            npc.damage = 5;
            npc.defense = 2;
            npc.lifeMax = 50;
            npc.value = 0f;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            aiType = NPCID.BeeSmall;
            animationType = NPCID.BeeSmall;
        }

        public override bool PreNPCLoot()
        {
            NPCLoader.blockLoot.Add(ItemID.Heart);
            NPCLoader.blockLoot.Add(ItemID.Star);
            return false;
        }

        public override string Texture
        {
            get { return ("SGAmod/NPCs/Murk/Fly"); }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fly Swarm");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BeeSmall];
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = ((int)(npc.ai[0] / 3));
            npc.frame.Y %= 4;
            npc.frame.Y *= frameHeight;
        }

        public override void AI()
        {

            npc.ai[0] += 1;
            npc.ai[2] += 1;
            NPC Master = Main.npc[(int)npc.ai[1]];
            if (!Master.active || npc.ai[1]+1< 1)
                npc.active = false;

            Vector2 masterloc = Master.Center - new Vector2(0, 0);
            bool flyaway = false;

            if (npc.ai[0] % 1100 > 700)
            {
                npc.TargetClosest();
                masterloc = Main.player[npc.target].Center - new Vector2(0, 0);
                if (Main.player[npc.target].dead)
                {
                    flyaway = true;
                }

            }
            else
            {

                if (this.GetType() == typeof(BossFlyMiniboss1) && npc.ai[2]%300==0 && npc.ai[0]>700 && Main.expertMode)
                {

                    Player target = Main.player[npc.target];
                    List<Projectile> itz2 = Idglib.Shattershots(npc.Center, target.position, new Vector2(target.width / 2, target.height / 2), ProjectileID.Stinger, (int)((float)npc.damage/4f), 8f, 0, 1, true, 0f, false, 300);
                    Main.PlaySound(SoundID.Item42, npc.Center);
                }

            }
            if (flyaway)
            masterloc = -(Master.Center - npc.Center);

            Vector2 masterdist=(masterloc-npc.Center);
        Vector2 masternormal=masterdist; masternormal.Normalize();

        npc.velocity+=masternormal*0.25f;
        npc.direction=(npc.velocity.X>0f).ToDirectionInt();
        if (npc.velocity.Length()>8f){npc.velocity.Normalize(); npc.velocity*=8f;}

        }

    }

    public class BossFly2 : ModNPC
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BeeSmall);
            npc.width = 10;
            npc.height = 10;
            npc.damage = 0;
            npc.defense = 5;
            npc.lifeMax = 300;
            npc.value = 0f;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            aiType = NPCID.BeeSmall;
            animationType = NPCID.BeeSmall;
        }

        public override string Texture
        {
            get { return("SGAmod/NPCs/Murk/Fly");}
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomber Fly");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BeeSmall];
        }

        public override void FindFrame(int frameHeight)
        {
        npc.frame.Y=((int)(npc.ai[0]/3));
        npc.frame.Y%=4;
        npc.frame.Y*=frameHeight;
        }

        private void ResetBomb()
        {
        npc.ai[0]=Main.rand.Next(50,200);
        npc.ai[3] = Projectile.NewProjectile(npc.Center.X+Main.rand.Next(-8,8), npc.Center.Y-40f, 0f,0f, ProjectileID.DD2OgreSpit, 1, 0f,0);
        Main.projectile[(int)npc.ai[3]].damage=15;
        npc.velocity.Y/=3f;
        npc.netUpdate=true;
        }

        public override void AI()
        {

        npc.ai[0]+=1;
        NPC Master = Main.npc[(int)npc.ai[1]];
        if (!Master.active || npc.ai[1]+1<1)
        npc.active=false;

        npc.TargetClosest(true);

        Vector2 masterloc=(new Vector2(Master.position.X+(Master.width/2),Master.position.Y+(Master.height/2)));
        Vector2 distomaster=masterloc-npc.Center;

        if (npc.ai[3]<1 && (npc.ai[0]==2 || (distomaster.Length()<64f && npc.ai[0]>500))){ResetBomb();}
        Projectile carryproj=Main.projectile[(int)npc.ai[3]];
        Player target=Main.player[npc.target];

        if (carryproj!=null && npc.ai[3]>0){
        if (carryproj.type==ProjectileID.DD2OgreSpit && carryproj.active){
        carryproj.timeLeft=200;
        carryproj.position=new Vector2(npc.position.X,npc.position.Y+(npc.height));
        carryproj.velocity.X=npc.velocity.X/2f;
        carryproj.velocity.Y=npc.velocity.Y/1.5f;
        masterloc=(Main.player[npc.target].position)+new Vector2(target.width/2,-260);
        npc.velocity.Y-=0.05f;
        if (Math.Abs(masterloc.X-npc.Center.X)<64 && distomaster.Y>100){
        npc.ai[3]=0;
        npc.netUpdate=true;
        }
        }else{npc.ai[3]=0; npc.netUpdate=true;}
        }else{npc.ai[3]=0; npc.netUpdate=true;}


        Vector2 masterdist=(masterloc-npc.Center);
        Vector2 masternormal=masterdist; masternormal.Normalize();

        npc.velocity+=masternormal*0.25f;
        npc.direction=(npc.velocity.X>0f).ToDirectionInt();
        if (npc.velocity.Length()>8f){npc.velocity.Normalize(); npc.velocity*=8f;}

        }

    }

    public class BossFly3 : BossFly2
    {
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BeeSmall);
            npc.width = 10;
            npc.height = 10;
            npc.damage = 0;
            npc.defense = 5;
            npc.lifeMax = 300;
            npc.value = 0f;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            aiType = NPCID.BeeSmall;
            animationType = NPCID.BeeSmall;
        }

        public override string Texture
        {
            get { return ("SGAmod/NPCs/Murk/Fly"); }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Airlifting Fly");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BeeSmall];
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = ((int)(npc.ai[0] / 3));
            npc.frame.Y %= 4;
            npc.frame.Y *= frameHeight;
        }

        private void ResetBomb()
        {

            if (npc.ai[0] > 20)
            {
                npc.active = false;
                return;
            }
            npc.ai[0] = Main.rand.Next(50, 200);

            int num663 = NPCID.JungleSlime;
            if (Main.rand.Next(0,100)<20)
            num663 = mod.NPCType("SwampSlime");

            int num664 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, num663, 0, 0f, 0f, 0f, 0f, 255);
            Main.npc[num664].SetDefaults(num663, -1f);
            Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
            Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
            Main.npc[num664].ai[0] = (float)(-1000 * Main.rand.Next(3));
            Main.npc[num664].ai[1] = 0f;
            npc.ai[3] = num664;

            Main.npc[num664].netUpdate = true;
            if (Main.netMode == 2 && num664 < 200)
            {
                NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
            }


            npc.velocity.Y /= 3f;
            npc.netUpdate = true;
        }

        public override void AI()
        {

            npc.ai[0] += 1;
            NPC Master = Main.npc[(int)npc.ai[1]];
            if (!Master.active || npc.ai[1]+1 < 1)
                npc.active = false;

            npc.TargetClosest(true);

            Vector2 masterloc = (new Vector2(Master.position.X + (Master.width / 2), Master.position.Y + (Master.height / 2)));
            Vector2 distomaster = masterloc - npc.Center;

            if (npc.ai[3] < 1 && (npc.ai[0] == 2 || (distomaster.Length() < 64f && npc.ai[0] > 500))) { ResetBomb(); }
            NPC carryproj = Main.npc[(int)npc.ai[3]];
            Player target = Main.player[npc.target];

            if (carryproj != null && npc.ai[3] > 0)
            {
                if (carryproj.active)
                {
                    carryproj.position = new Vector2(npc.position.X, npc.position.Y + (npc.height));
                    carryproj.velocity.X = npc.velocity.X / 2f;
                    carryproj.velocity.Y = npc.velocity.Y / 1.5f;
                    masterloc = (Main.player[npc.target].position) + new Vector2(target.width / 2, -260);
                    npc.velocity.Y -= 0.05f;
                    if (Math.Abs(masterloc.X - npc.Center.X) < 64 && distomaster.Y > 100)
                    {
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                }
                else { npc.ai[3] = 0; npc.netUpdate = true; }
            }
            else { npc.ai[3] = 0; npc.netUpdate = true;
                if (carryproj != null && npc.ai[3] > 0)
                if (!carryproj.active)
                npc.active = false;
            }


            Vector2 masterdist = (masterloc - npc.Center);
            Vector2 masternormal = masterdist; masternormal.Normalize();

            npc.velocity += masternormal * 0.25f;
            npc.direction = (npc.velocity.X > 0f).ToDirectionInt();
            if (npc.velocity.Length() > 8f) { npc.velocity.Normalize(); npc.velocity *= 8f; }

        }

    }

    public class BossFlyMiniboss1 : BossFly1
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Killer Fly Swarm");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BeeSmall];
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.BeeSmall);
            npc.width = 10;
            npc.height = 10;
            npc.damage = 40;
            npc.defense = 15;
            npc.lifeMax = 5000;
            npc.value = 0f;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.noTileCollide = true;
            aiType = NPCID.BeeSmall;
            animationType = NPCID.BeeSmall;

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override bool PreNPCLoot()
        {
            NPCLoader.blockLoot.Add(ItemID.Heart);
            NPCLoader.blockLoot.Add(ItemID.Star);
            return true;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RoilingSludge"));
            if (SGAWorld.downedMurk<1)
            SGAWorld.downedMurk = 1;
        }

        public override string Texture
        {
            get { return ("SGAmod/NPCs/Murk/Fly"); }
        }

        public override void AI()
        {

            if (npc.ai[2]<5)
            {
                int prev=npc.whoAmI;
                int num664;
                float val = 10;
                for (int i = 0; i < ((NPC.CountNPCS(mod.NPCType("Murk"))>0 && !Main.hardMode) ? 10 : 20);i+=1)
                {

                    int num663 = mod.NPCType("BossFlyMiniboss1");

                    num664 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, num663, 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num664].SetDefaults(num663, -1f);
                    Main.npc[num664].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
                    Main.npc[num664].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
                    Main.npc[num664].ai[0] = Main.rand.NextFloat(50, 450);
                    Main.npc[num664].ai[1] = prev;
                    Main.npc[num664].ai[2] = val+(i*8);
                    Main.npc[num664].damage = npc.damage;
                    Main.npc[num664].dontTakeDamage = npc.dontTakeDamage;
                    Main.npc[num664].realLife = npc.whoAmI;
                    prev = num664;

                    Main.npc[num664].netUpdate = true;
                    if (Main.netMode == 2 && num664 < 200)
                    {
                        NetMessage.SendData(23, -1, -1, null, num664, 0f, 0f, 0f, 0, 0, 0);
                    }
                    npc.netUpdate = true;


                }
                if (NPC.CountNPCS(mod.NPCType("Murk")) < 1)
                npc.ai[1] = prev;
                if (npc.ai[2]<1)
                npc.ai[2] = val;
                npc.aiStyle = -2;
            }
            if (npc.target > -1 && !Main.player[npc.target].dead)
                npc.timeLeft = 60;

            base.AI();
        }


    }

    public class MurkyDepths : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Murky Depths");
            Description.SetDefault("You take 50% more damage from all sources");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        //public override void Update(Player player, ref int buffIndex)
        //{
        //	player.GetModPlayer<SGAPlayer>().MassiveBleeding = true;
        //}

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "SGAmod/NPCs/Murk/Murk_Head_Boss";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SGAPlayer>().MurkyDepths = true;
        }

        //public override void Update(NPC npc, ref int buffIndex)
        //{
        //	npc.GetGlobalNPC<SGAWorld>(mod).eFlames = true;
        //}
    }


}