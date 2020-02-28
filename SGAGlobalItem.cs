using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent.UI;
using System.Reflection;
using SGAmod.Items.Weapons;
using SGAmod.Items.Accessories;
using Idglibrary;

namespace SGAmod
{
    public class SGAGlobalItem : GlobalItem
    {
        public static string pboostertextbase2 = "While you have wing time, hold DOWN while flying to boost in a direction\nHold LEFT or RIGHT to cap your vertical speed and greatly increase horizontal fly speeds\nHold only DOWN to quickly fly upwards, else rapidly fall downwards with no wingtime left";
        public static string pboostertext = "";
        public static string pboostertextboost = "";

        public override bool CanUseItem(Item item, Player player)
        {
            if ((player.ownedProjectileCounts[mod.ProjectileType("TheJacobReloading")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("DragonRevolverReloading")] > 0) && item.damage > 0) {
                return false;
            } else {
                return base.CanUseItem(item, player);
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.modItem != null) {
                if (item.owner > -1 && Main.netMode != 1) {
                    SGAPlayer sgaply = (Main.player[item.owner].GetModPlayer<SGAPlayer>());
                    pboostertextboost = "\nCurrent boost: " + sgaply.SpaceDiverWings;
                    pboostertext = pboostertextbase2 + pboostertextboost;
                }
                var myType = (item.modItem).GetType();
                var n = myType.Namespace;
                string asastring = (string)n;
                //int ishavocitem = (asastring.Split('.').Length - 1);
                int ishavocitem = asastring.Length - asastring.Replace("HavocGear.", "").Length;
                if (ishavocitem > 0) {
                    Color c = Main.hslToRgb(0.9f, 0.5f, 0.35f);
                    tooltips.Add(new TooltipLine(mod, "Havoc Item", Idglib.ColorText(c, "Former Havoc mod item")));

                }
                if (SGAmod.UsesPlasma.ContainsKey(item.type))
                {
                    Color c = Main.hslToRgb(0.7f, 0.15f, 0.7f);
                    tooltips.Add(new TooltipLine(mod, "Plasma Item", Idglib.ColorText(c, "This weapon uses plasma cells for recharging")));
                }

                if (SGAmod.UsesClips.ContainsKey(item.type)) { Color c = Main.hslToRgb(0.7f, 0.15f, 0.7f);
                    tooltips.Add(new TooltipLine(mod, "Clip Item", Idglib.ColorText(c, "This weapon has a clip and requires manual reloading")));
                }
            }
        }

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == mod.ItemType("DankWoodHelm") && body.type == mod.ItemType("DankWoodChest") && legs.type == mod.ItemType("DankLegs"))
            {
                return "Dank";
            }
            if (head.type == mod.ItemType("UnmanedHood") && body.type == mod.ItemType("UnmanedBreastplate") && legs.type == mod.ItemType("UnmanedLeggings"))
            {
                return "Novus";
            }
            if (head.type == mod.ItemType("BlazewyrmHelm") && body.type == mod.ItemType("BlazewyrmBreastplate") && legs.type == mod.ItemType("BlazewyrmLeggings"))
            {
                return "Blazewyrm";
            }
            if (head.type == mod.ItemType("SpaceDiverHelmet") && body.type == mod.ItemType("SpaceDiverChestplate") && legs.type == mod.ItemType("SpaceDiverLeggings"))
            {
                return "SpaceDiver";
            }
            if (!head.vanity && !body.vanity && !legs.vanity) {
                if (head.type == mod.ItemType("MisterCreeperHead") && body.type == mod.ItemType("MisterCreeperBody") && legs.type == mod.ItemType("MisterCreeperLegs"))
                {
                    return "MisterCreeper";
                } }
            return "";
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
            if (set == "Dank")
            {
                player.setBonus = "10% of the sum of all damage types is added to your current weapon's attack\nyou regen life faster while on the surface during the day";
                sgaplayer.Dankset = 3;
            }
            if (set == "Novus")
            {
                player.setBonus = "Novus items emit more light when used and deal 20% more damage";
                sgaplayer.Novusset = 3;
            }
            if (set == "Blazewyrm")
            {
                player.setBonus = "When you Crit with a non-projectile melee hit you create a very powerful explosion equal to triple the damage dealt; hurting everything nearby\nthis however gives you the action cooldown debuff for 10 seconds which this ability will not activate" +
                        "\nImmune to fireblocks as well as immunity to On Fire! and Thermal Blaze";
                player.fireWalk = true;
                player.buffImmune[BuffID.OnFire] = true;
                player.buffImmune[mod.BuffType("ThermalBlaze")] = true;
                sgaplayer.Blazewyrmset = true;
            }
            if (set == "SpaceDiver")
            {
                string text1 = Idglib.ColorText(Color.Red, "90% reduced breath meter regen");
                string text2 = Idglib.ColorText(Color.Red, "You've adapted to pressurized air, removing the armor set will greatly harm you");
                player.setBonus = "Receive Endurance and Defense based on breath left (40% Endurance and 100 Defense at full breath)\nTaking damage will drain your breath meter based on the faction of life lost\nReceive no damage when damaged with a full breath meter\n" + SGAGlobalItem.pboostertext + text1 + " \n" + text2;
                sgaplayer.SpaceDiverset = true;
                sgaplayer.SpaceDiverWings += 0.5f;
            }
            if (set == "MisterCreeper")
            {
                player.setBonus = "Any sword that doesn't shoot a projectile is swung 50% faster and deals crits when you are falling downwards\nWhen you take damage, you launch a damaging high velocity grenade at what hit you\nThese grenades are launched even during immunity frames if your touching an enemy\nDrinking a healing potion launches a ton of bouncy grendes in all directions" +
                    "\nTaking lethal damage will cause you to light your fuse, killing you IF you fail to kill anyone with your ending explosion in a few seconds!\nThis gives you Action cooldown for 60 seconds, which prevents reactivation";
                sgaplayer.MisterCreeperset = true;
            }
        }

        [System.Obsolete]
        public override void GetWeaponDamage(Item item, Player player, ref int damage) {
            SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;

            if (item.modItem != null) {
                var myType = (item.modItem).GetType();
                var n = myType.Namespace;
                string asastring = (string)n;
                int ishavocitem = asastring.Length - asastring.Replace("HavocGear.", "").Length;
                if (ishavocitem > 0 && sgaplayer.Havoc > 0) {
                    damage = (int)(damage * 1.25);
                } }

            if (sgaplayer.Dankset>0)
            {
                damage = (int)(damage+(damage * ((player.magicDamage + player.minionDamage + player.rangedDamage + player.meleeDamage + player.thrownDamage) - 4f) * 0.10f));


            }

        }

        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            if (item.type == ItemID.NebulaPickup1 || item.type == ItemID.NebulaPickup2 || item.type == ItemID.NebulaPickup3)
            {
                if (player.GetModPlayer<SGAPlayer>().BoosterMagnet)
                {
                    grabRange = 400;
                }
            }
        }

        public override bool GrabStyle(Item item,Player player)
        {
            if (item.type == ItemID.NebulaPickup1 || item.type == ItemID.NebulaPickup2 || item.type == ItemID.NebulaPickup3)
            {
                if (player.GetModPlayer<SGAPlayer>().BoosterMagnet)
                {
                    Vector2 vectorItemToPlayer = player.Center - item.Center;
                    Vector2 movement = vectorItemToPlayer.SafeNormalize(default(Vector2)) * 0.1f;
                    item.velocity = item.velocity + movement;
                    item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);

                }
            }


            return false;
        }

        public override bool UseItem(Item item, Player player)
        {
            if (item.healLife > 0)
            {
                if (player.GetModPlayer<SGAPlayer>().MisterCreeperset) {

                    for (int gg = 0; gg < 30; gg += 1)
                    {
                        Vector2 myspeed = MathHelper.ToRadians(Main.rand.NextFloat(0f, 360f)).ToRotationVector2();
                        myspeed *= Main.rand.NextFloat(14f, 22f);
                        int prog = Projectile.NewProjectile(player.Center.X, player.Center.Y, myspeed.X, myspeed.Y, ProjectileID.BouncyGrenade, 1000, 10f, player.whoAmI);
                        Main.projectile[prog].thrown = true; Main.projectile[prog].ranged = false; Main.projectile[prog].netUpdate = true;
                        IdgProjectile.Sync(prog);
                    }

                }
            }
            return base.UseItem(item, player);
        }

        public override bool OnPickup(Item item, Player player)
        {
            SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
            if (sgaplayer.MidasIdol > 0)
            {
                /*int[] count = {player.CountItem(ItemID.CopperCoin), player.CountItem(ItemID.SilverCoin), player.CountItem(ItemID.GoldCoin), player.CountItem(ItemID.PlatinumCoin) };

                 int totalcount = 0;
                 foreach(int coincount in  count){
                 totalcount += coincount;
                 }
                 if (totalcount>0)*/


                if (item.type == ItemID.CopperCoin)
                {
                    Main.PlaySound(38, (int)player.position.X, (int)player.position.Y, 0, 1f, -0.6f);
                    if (sgaplayer.MidasIdol == 2)
                        player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.Campfire : BuffID.Swiftness, 60 * 5);
                    else
                        player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.MagicPower : BuffID.Titan, 60 * 3);
                    return false;
                }
                if (item.type == ItemID.SilverCoin)
                {
                    Main.PlaySound(38, (int)player.position.X, (int)player.position.Y, 0, 1f, -0.6f);
                    if (sgaplayer.MidasIdol == 2)
                        player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.Endurance : BuffID.Honey, 60 * 5);
                    else
                        player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.Wrath : BuffID.Rage, 60 * 5);
                    return false;
                }
                if (item.type == ItemID.GoldCoin)
                {
                    Main.PlaySound(38, (int)player.position.X, (int)player.position.Y, 0, 1f, -0.6f);
                    player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.DryadsWard : BuffID.Ironskin, 60 * 10);
                    player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.Wrath : BuffID.Rage, 60 * 20);
                    return false;
                }
                if (item.type == ItemID.PlatinumCoin)
                {
                    Main.PlaySound(38, (int)player.position.X, (int)player.position.Y, 0, 1f, -0.6f);
                    player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.RapidHealing : BuffID.ShadowDodge, 60 * 30);
                    player.AddBuff(Main.rand.Next(0, 2) == 0 ? BuffID.SolarShield2 : BuffID.IceBarrier, 60 * 30);
                    return false;
                }

            }
            return base.OnPickup(item, player);
        }

        public override float UseTimeMultiplier(Item item, Player player)
        {

            if (item.damage < 1)
                return 1f;
            float usetimetemp = 1f;
            SGAPlayer sgaplayer = player.GetModPlayer(mod, typeof(SGAPlayer).Name) as SGAPlayer;
            if (item.pick + item.hammer + item.axe > 0) {
                usetimetemp *= sgaplayer.UseTimeMulPickaxe;
            }
            if (item.thrown) {
                usetimetemp *= sgaplayer.ThrowingSpeed;
            }
            /*ModItem mitem = item.modItem;
            bool hasshoot = false;
            if (mitem != null)
            {
                hasshoot = mitem.GetType().GetMethod("Shoot").GetMethodBody().ToString().Length > 5;
                Main.NewText(mitem.GetType().GetMethod("Shoot").GetMethodBody().);
            }*/
            if (sgaplayer.MisterCreeperset)
            {
                if (item.shoot < 1 && item.melee && item.pick + item.axe + item.hammer < 1)
                {
                    usetimetemp *= 1.5f;
                }
            }

            return (usetimetemp * sgaplayer.UseTimeMul);
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag")
            {
                if (Main.rand.Next(100) <= (Main.hardMode ? 2 : 1))
                {
                    player.QuickSpawnItem(mod.ItemType("MisterCreeperHead"), 1);
                    player.QuickSpawnItem(mod.ItemType("MisterCreeperBody"), 1);
                    player.QuickSpawnItem(mod.ItemType("MisterCreeperLegs"), 1);
                }
                if (arg == ItemID.GolemBossBag && Main.rand.Next(100) <= 20)
                    player.QuickSpawnItem(mod.ItemType("Upheaval"), 1);
                if (arg == ItemID.MoonLordBossBag)
                    player.QuickSpawnItem(mod.ItemType("EldritchTentacle"), Main.rand.Next(20, 40));
                if (arg == ItemID.BossBagBetsy)
                    player.QuickSpawnItem(mod.ItemType("OmegaSigil"), 1);
                if (arg == ItemID.WallOfFleshBossBag && Main.rand.Next(100) <= 10)
                    player.QuickSpawnItem(mod.ItemType("Powerjack"), 1);
            }
        }

        public override bool WingUpdate(int wings, Player player, bool inUse)
        {
            SGAPlayer modply = player.GetModPlayer<SGAPlayer>();
            return modply.SpaceDiverset;
        }

    }













        public class ClipWeaponReloading : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("You should not see this");
        }

        public override string Texture
        {
            get { return("SGAmod/Items/Weapons/TheJacob");}
        }

        public override void SetDefaults()
        {
            //projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
            projectile.width = 24;
            projectile.height = 24;
            projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            projectile.hostile=false;
            projectile.friendly=true;
            projectile.tileCollide = false;
            projectile.thrown = true;
            projectile.timeLeft=100;
            projectile.penetrate=10;
            aiType = 0;
            drawOriginOffsetX = 8;
            drawOriginOffsetY = 8;
            drawHeldProjInFrontOfHeldItemAndArms=false;
        }

    public override bool? CanHitNPC(NPC target){
    return false;
    }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
        return (Main.player[projectile.owner].itemAnimation<1);
        }

        public override bool PreAI()
        {
        Player owner = Main.player[projectile.owner];
        if (owner==null)
        projectile.Kill();
        return true;
        }

        public override void AI()
        {
        Vector2 positiondust = Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 8f;
            Player owner = Main.player[projectile.owner];
            if (owner==null)
            projectile.Kill();
            if (owner.dead)
            projectile.Kill();

            if (owner.itemAnimation>0){
            projectile.timeLeft+=1;
            }else{
            Vector2 direction=(Main.MouseWorld-owner.Center);
            projectile.spriteDirection=(owner.direction>0).ToDirectionInt();
            owner.heldProj=projectile.whoAmI;
            projectile.ai[0]+=1;
            projectile.velocity=new Vector2(0f,0f);
            //projectile.rotation = projectile.rotation.AngleLerp((float)(Math.PI/-(4.0*(double)projectile.spriteDirection)),0.15f);
            owner.bodyFrame.Y = owner.bodyFrame.Height * 3;

            if (projectile.timeLeft==18){
            SGAPlayer sgaplayer = owner.GetModPlayer(mod,typeof(SGAPlayer).Name) as SGAPlayer;
            sgaplayer.ammoLeftInClip=sgaplayer.ammoLeftInClipMax;
            Main.PlaySound(SoundID.Item65,owner.Center);
            }

            /*if (owner.velocity.X<0)
            owner.direction=-1;
            projectile.spriteDirection=owner.direction;*/
            }

            //projectile.velocity=new Vector2(projectile.velocity.X,0f);
            projectile.Center=owner.Center+new Vector2(owner.direction<0 ? -projectile.width*2 : 0,-4f);


            projectile.Opacity=MathHelper.Clamp((float)projectile.timeLeft/10f,0f,1f);

        }
    }


    public class TrapPrefixAccessory : TrapPrefix
    {

        public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

        public TrapPrefixAccessory()
        {
        }
        public TrapPrefixAccessory(float armorbreak, float damage)
        {
            this.armorbreak = armorbreak;
            this.damage = damage;
        }

    }

    public class ThrowerPrefix : TrapPrefix
    {

        public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }

        public ThrowerPrefix()
        {
        }
        public ThrowerPrefix(float armorbreak, float damage, float thrownvelocity, float throweruserate)
        {
            this.thrownvelocity = thrownvelocity;
            this.throweruserate = throweruserate;
            this.armorbreak = armorbreak;
            this.damage = damage;
        }
        public override bool CanRoll(Item item)
        {
            return item.thrown;
        }
    }

    public class ThrowerPrefixAccessory : TrapPrefix
    {

        public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

        public ThrowerPrefixAccessory()
        {
        }
        public ThrowerPrefixAccessory(float armorbreak, float damage, float thrownvelocity, float throweruserate, float throwersavingchance)
        {
            this.thrownvelocity = thrownvelocity;
            this.throweruserate = throweruserate;
            this.armorbreak = armorbreak;
            this.damage = damage;
            this.throwersavingchance = throwersavingchance;
        }
        public override bool CanRoll(Item item)
        {
            return item.accessory;
        }


    }

    public class TrapPrefix : ModPrefix
    {
        public float armorbreak = 0f;
        public float damage = 0f;
        public float thrownvelocity = 0f;
        public float throweruserate = 0f;
        public float throwersavingchance = 0f;

        public override PrefixCategory Category { get { return PrefixCategory.AnyWeapon; } }
        public TrapPrefix()
        {
        }
        public TrapPrefix(float armorbreak, float damage)
        {
            this.armorbreak = armorbreak;
            this.damage = damage;
        }
        public override bool Autoload(ref string name)
        {
            if (base.Autoload(ref name))
            {
                if (GetType() == typeof(TrapPrefix))
                {
                    mod.AddPrefix("Edged", new TrapPrefix(0.05f, 0.05f));
                    mod.AddPrefix("Sundering", new TrapPrefix(0.08f, 0.10f));
                    mod.AddPrefix("Undercut", new TrapPrefix(0.12f, 0.10f));
                    mod.AddPrefix("Razor Sharp", new TrapPrefix(0.2f, 0.15f));
                }
                if (GetType() == typeof(TrapPrefixAccessory))
                {
                    mod.AddPrefix("Tinkering", new TrapPrefixAccessory(0f, 0.05f));
                    mod.AddPrefix("Knowledgable", new TrapPrefixAccessory(0.08f, 0f));
                    mod.AddPrefix("Dungeoneer's", new TrapPrefixAccessory(0.05f, 0.10f));
                    mod.AddPrefix("Goblin Tinker's Own", new TrapPrefixAccessory(0.075f, 0.15f));
                }
                if (GetType() == typeof(ThrowerPrefix))
                {
                    mod.AddPrefix("Tossable", new ThrowerPrefix(0f, 0f, 0.1f, 0.15f));
                    mod.AddPrefix("Impacting", new ThrowerPrefix(0f, 0f, 0.15f, 0.2f));
                    mod.AddPrefix("Olympian", new ThrowerPrefix(0f, 0f, 0.25f, 0.4f));
                }
                if (GetType() == typeof(ThrowerPrefixAccessory))
                {
                    mod.AddPrefix("Lightweight", new ThrowerPrefixAccessory(0f, 0f, 0.025f, 0.025f,0f));
                    mod.AddPrefix("Slinger's", new ThrowerPrefixAccessory(0f, 0f, 0.04f, 0.02f,0.01f));
                    mod.AddPrefix("Pocketed", new ThrowerPrefixAccessory(0f, 0f, 0.02f, 0.03f, 0.015f));
                    mod.AddPrefix("Conservative", new ThrowerPrefixAccessory(0f, 0f, 0.0f, 0.0f,0.05f));
                    mod.AddPrefix("Rougish", new ThrowerPrefixAccessory(0f, 0f, 0.06f, 0.05f,0.02f));
                }
            }
            return false;
        }

        public override bool CanRoll(Item item)
        {
            if (item.modItem != null)
            {
                Type myclass = item.modItem.GetType();
                if (myclass.BaseType == typeof(TrapWeapon) || myclass.IsSubclassOf(typeof(TrapWeapon)))
                    return true;
            }

            return false;
        }

        public override float RollChance(Item item)
        {
            return 3f;
        }

        public override void Apply(Item item)
        {
            TrapDamageItems myitem = item.GetGlobalItem<TrapDamageItems>();
            myitem.armorbreak = armorbreak;
            myitem.damage2 = damage;
            myitem.throweruserate = throweruserate;
            myitem.thrownvelocity = thrownvelocity;
            myitem.thrownsavingchance = throwersavingchance;
            /*if (item.damage > 0)
              {
                  item.damage = (int)(item.damage * (1f + damage));
              }*/
            //myitem.damage = damage;
        }
    }

    public class TrapDamageItems : GlobalItem
    {
        public float armorbreak = 0f;
        public float damage2 = 0f;
        public float throweruserate = 0f;
        public float thrownvelocity = 0f;
        public float thrownsavingchance = 0f;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            TrapDamageItems myClone = (TrapDamageItems)base.Clone(item, itemClone);
            myClone.armorbreak = armorbreak;
            myClone.damage2 = damage2;
            myClone.throweruserate = throweruserate;
            myClone.thrownvelocity = thrownvelocity;
            myClone.thrownsavingchance = thrownsavingchance;
            return myClone;
        }


        public override bool NewPreReforge(Item item)
        {
            damage2 = 0f;
            armorbreak = 0f;
            throweruserate = 0f;
            thrownvelocity = 0f;
            thrownsavingchance = 0f;
            return base.NewPreReforge(item);
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.thrown)
                return Main.rand.Next(100) > (int)(player.GetModPlayer<SGAPlayer>().Thrownsavingchance * 100f);
            else
                return true;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.GetModPlayer<SGAPlayer>().TrapDamageAP += armorbreak;
            player.GetModPlayer<SGAPlayer>().TrapDamageMul += damage2;
            player.GetModPlayer<SGAPlayer>().ThrowingSpeed += throweruserate;
            player.GetModPlayer<SGAPlayer>().Thrownsavingchance += thrownsavingchance;
            player.thrownVelocity += thrownvelocity;
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (player.HeldItem == item)
            {
                player.GetModPlayer<SGAPlayer>().TrapDamageAP += armorbreak;
                player.GetModPlayer<SGAPlayer>().TrapDamageMul += damage2;
                player.GetModPlayer<SGAPlayer>().ThrowingSpeed += throweruserate;
                player.thrownVelocity += thrownvelocity;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            if (damage2 > 0)
            {
                string line2 = "% extra trap damage (while held)";
                if (item.accessory)
                    line2 = "% extra trap damage";
                TooltipLine line = new TooltipLine(mod, "Trapline1", "+" + ((damage2) * 100f) + line2);
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (armorbreak > 0)
            {
                string line2 = "%";
                if (item.accessory)
                    line2 = "% trap damage";
                TooltipLine line = new TooltipLine(mod, "Trapline2", "+" + ((armorbreak) * 100f) + line2 + " armor piercing");
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (throweruserate > 0)
            {
                string line2 = "% throwing rate";
                TooltipLine line = new TooltipLine(mod, "Trapline3", "+" + ((throweruserate) * 100f) + line2);
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (thrownvelocity > 0)
            {
                string line2 = "% throwing velocity";
                TooltipLine line = new TooltipLine(mod, "Trapline4", "+" + ((thrownvelocity) * 100f) + line2);
                line.isModifier = true;
                tooltips.Add(line);
            }
            if (thrownsavingchance > 0)
            {
                string line2 = "% chance to not consume thrown item";
                TooltipLine line = new TooltipLine(mod, "Trapline5", "+" + ((thrownsavingchance) * 100f) + line2);
                line.isModifier = true;
                tooltips.Add(line);
            }
        }
        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write((int)damage2 * 1000);
            writer.Write((int)armorbreak * 1000);
            writer.Write((int)thrownvelocity * 1000);
            writer.Write((int)throweruserate * 1000);
            writer.Write((int)thrownsavingchance * 1000);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            damage2 = (float)(reader.ReadInt32() / 1000f);
            armorbreak = (float)(reader.ReadInt32() / 1000f);
            thrownvelocity = (float)(reader.ReadInt32() / 1000f);
            throweruserate = (float)(reader.ReadInt32() / 1000f);
            thrownsavingchance = (float)(reader.ReadInt32() / 1000f);
        }


    }

}
