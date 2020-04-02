using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Idglibrary;

namespace SGAmod.Items.Accessories
{
	public class LostNotes : ModItem
	{
		int notetype = 0;
		static int totallength=6;
		public virtual string[,] NoteWords => new string[,] { { ":Date 62 AC:", "This is it, I've finally made it to the forgotten lands in hopes of finding wealth and fortune!","It wouldn't be long now! I could already see the coast, eagerly awaiting it!", "With my rucksack and tools in hand, surely nothing can go wrong, right?" },
		{":Date 62 AC:","I met up with the settlment on the isles, looking for a resupply before heading out, and given my desire for some booze went to the tavern","Pretty lighthearted place, a little on the shady side however, but you never know in these times, I took a seat on the first stool at the bar I could find","When the tavernkeep came over, I ordered up some wine,rented a room, and asked what jobs there are for some quick cash." },
		{":Date 62 AC:","Monster hunting, mining, and exploring where none had gone before, pretty standard jobs they had","He also said something about Etheria, but at the same time where were people pointing out he let kids stay in a clearly adult tavern, something about defenders, I laughed it off, and went up to my room","So? This is it? The adventure I've dreamed of? Well, I guess I can consider my actions after I take a wee nap." },
		{":Date 68 AC:","I had first run into something odd... A little girl, in the middle of the dark. Being the parent back home that I am I approuched her.","This however proved to be my folly, as her sweet and innocent apperence quickly turned twisted and... lusty","Needless to say I ran" },
		{":Date 74 AC:","With one final blow, the monsterious wall of meat came crashing down, for one moment I felt like an utter god!","But it seems said gods had something else in mind when I had unwillingly released","but non the less, I had high hopes!"},
		{":Date 74 AC:","Things didn't quite go as well as I had hoped, when I got back to the settlement everyone was dead, impaled through the chest but a sharp horn-like object","Furthermore... the entire area was a pastel rainbow color, and it wasn't too long before I found out the cause...","(something was wrote here, but it is too badly written to read)"}
		};
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lost Notes");
			//Tooltip.SetDefault("25% increased mining/hammering/chopping speed\nCan only wear 1 Charm at a time");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			for(int i=0;i< 4; i+=1)
			{
				tooltips.Add(new TooltipLine(mod, "NoteWords", NoteWords[notetype, i]));
			}
		}

		public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();
			tag["notetype"] = notetype;
			return tag;
		}
		public override void Load(TagCompound tag)
		{
			notetype = tag.GetInt("notetype");
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(notetype);
		}

		public override void NetRecieve(BinaryReader reader)
		{
			notetype = reader.ReadInt16();
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 10, 0);
			item.rare = 0;
			//item.accessory = true;
		}

	}

}