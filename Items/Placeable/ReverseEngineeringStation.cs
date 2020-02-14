using Terraria.ModLoader;
using Terraria.ID;

namespace SGAmod.Items.Placeable
{
	public class ReverseEngineeringStation : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reverse Engineering Station");
			Tooltip.SetDefault("Allows weaponization of unusual tidbits, breaking down some weapons into raw components, and crafting of advanced machinery\nSome formally uncraftable items may be crafted here\nDoubles as a Tinkerer's Workbench");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.width = 26;
			item.height = 14;
			item.value = 1;
			item.rare = 1;
			item.alpha = 0;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("ReverseEngineeringStation");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinkerersWorkshop, 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 15);
			recipe.AddIngredient(mod.ItemType("VialofAcid"), 15);
			recipe.AddRecipeGroup("SGAmod:PressurePlates", 2);
			recipe.AddIngredient(ItemID.CopperBar, 8);
			recipe.AddRecipeGroup("SGAmod:TechStuff", 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}