using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace SGAmod
{
	public class SGAHUD : Overlay
	{
		public SGAHUD() : base(EffectPriority.Medium, RenderLayers.All) { }

		public override void Update(GameTime gameTime)
		{
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu || SGAmod.Instance == null && Main.netMode!=1)
				return;
			Player locply = Main.LocalPlayer;
				if (locply != null && locply.whoAmI == Main.myPlayer)
			{
				if (locply.HeldItem.type == SGAmod.Instance.ItemType("CaliburnCompess"))
				{

					for (int i = 0; i < SGAWorld.CaliburnAlterCoordsX.Length; i += 1)
					{
						string[] texs = { "SGAmod/Items/Weapons/Caliburn/CaliburnTypeA", "SGAmod/Items/Weapons/Caliburn/CaliburnTypeB", "SGAmod/Items/Weapons/Caliburn/CaliburnTypeC" };
						Texture2D tex = ModContent.GetTexture(texs[i]);

						Vector2 drawOrigin = new Vector2(tex.Width, tex.Height) / 2f;

						Vector2 drawPos = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;

						Vector2 Vecd = (new Vector2(SGAWorld.CaliburnAlterCoordsX[i], SGAWorld.CaliburnAlterCoordsY[i]+96) - (drawPos + Main.screenPosition));
						float pointthere = Vecd.ToRotation();
						bool flip = Vecd.X > 0;

						spriteBatch.Draw(tex, drawPos + (pointthere.ToRotationVector2() * 64f)+(pointthere.ToRotationVector2() * (float)Math.Pow(Vecd.Length(),0.9)/50), null, Color.White, pointthere + MathHelper.ToRadians(45)+(flip ? MathHelper.ToRadians(-90)*3f : 0), drawOrigin, 1, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
						//spriteBatch.Draw(tex, new Vector2(150, 150), null, Color.White, Main.GlobalTime, drawOrigin, 1, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

					}
				}

				if (SGAmod.UsesPlasma.ContainsKey(locply.HeldItem.type))
				{
					if (!locply.dead)
					{
						SGAmod mod = SGAmod.Instance;
						SGAPlayer modply = locply.GetModPlayer<SGAPlayer>();
						int maxclip;
						bool check = SGAmod.UsesPlasma.TryGetValue(locply.HeldItem.type, out maxclip);

						if (check)
						{
							Color color = Lighting.GetColor((int)locply.Center.X / 16, (int)locply.Center.Y / 16);
							Texture2D texture = mod.GetTexture("Items/PlasmaCell");
							int drawX = (int)((locply.position.X + (locply.width / 2)) - Main.screenPosition.X);
							int drawY = (int)((locply.position.Y + (locply.gravDir == -1 ? locply.height + 20 : -20)) - Main.screenPosition.Y);//gravDir 

							float percent = ((float)modply.plasmaLeftInClip / (float)modply.plasmaLeftInClipMax);

							//DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Color.Lerp(Color.Black, Color.DarkGray, 0.25f), (float)Math.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
							//DrawData data2 = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, (int)((float)texture.Height * percent)), Color.White, (float)Math.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);

							spriteBatch.Draw(texture, new Vector2(drawX, drawY), null, Color.Lerp(Color.Black, Color.DarkGray, 0.25f), (float)Math.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
							spriteBatch.Draw(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, (int)((float)texture.Height * percent)), Color.White, (float)Math.PI, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);

						}
					}
				}

				if (SGAmod.UsesClips.ContainsKey(locply.HeldItem.type))
				{
					if (!locply.dead)
					{
						SGAmod mod = SGAmod.Instance;
						SGAPlayer modply = locply.GetModPlayer<SGAPlayer>();
						int maxclip;
						bool check = SGAmod.UsesClips.TryGetValue(locply.HeldItem.type, out maxclip);

						if (check)
						{
							Color color = Lighting.GetColor((int)locply.Center.X / 16, (int)locply.Center.Y / 16);
							Texture2D texture = mod.GetTexture("AmmoHud");
							int drawX = (int)((locply.position.X + (locply.width / 2)) - Main.screenPosition.X);
							int drawY = (int)((locply.position.Y + (locply.gravDir == -1 ? locply.height + 10 : -10)) - Main.screenPosition.Y);//gravDir 
							for (int q = 0; q < modply.ammoLeftInClip; q++)
							{
								//DrawData data = new DrawData(texture, new Vector2((drawX - (q * texture.Width)) + (int)((maxclip * texture.Width) / 2), drawY), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);
								spriteBatch.Draw(texture, new Vector2((drawX - (q * texture.Width)) + (int)((maxclip * texture.Width) / 2), drawY), null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0);

							}
						}
					}
				}




			}

		}

		public override void Activate(Vector2 position, params object[] args) { }

		public override void Deactivate(params object[] args) { }

		public override bool IsVisible()
		{
			bool draw=false;
			if (!Main.gameMenu && Main.LocalPlayer != null && SGAmod.Instance != null)
			{
				//if (Main.LocalPlayer.HeldItem.type == SGAmod.Instance.ItemType("Expertise"))
				draw = true;
			}


			return draw;
		}
	}
}
