using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SGAmod
{
	public class ProgramSky : CustomSky
	{
		private Random _random = new Random();
		private bool _isActive;
		private float[] xoffset = new float[200];
		private Color acolor = Color.White;


		public override void OnLoad()
		{
		}

		public override void Update(GameTime gameTime)
		{
			acolor = Main.hslToRgb((Main.GlobalTime / 10f) % 1, 0.81f, 0.5f);
			SGAmod.ProgramSkyAlpha = MathHelper.Clamp(NPC.CountNPCS((SGAmod.Instance).NPCType("SPinky")) > 0 ? SGAmod.ProgramSkyAlpha + 0.005f : SGAmod.ProgramSkyAlpha - 0.005f, 0f, 1f);
		}

		private float GetIntensity()
		{
			return SGAmod.ProgramSkyAlpha;
		}

		public override Color OnTileColor(Color inColor)
		{
			return acolor;
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			double thevalue = Main.GlobalTime * 2.0;
			double movespeed = Main.GlobalTime * 0.2;

			//NPC theboss=Main.npc[NPC.FindFirstNPC((SGAmod.Instance).NPCType("Asterism"))];
			//float valie=((float)theboss.life/(float)theboss.lifeMax);
			//Main.spriteBatch.End();
			//Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			//var deathShader = GameShaders.Misc["WaterProcessor"];
			//GameShaders.Misc["WaterProcessor"].Apply(new DrawData?(new DrawData(this._distortionTarget, Vector2.Zero, Color.White)));
			//deathShader.UseOpacity(0.5f);
			//deathShader.Apply(null);
			if (maxDepth >= 0 && minDepth < 0)
			{
				Texture2D texa = ModContent.GetTexture("SGAmod/noise");

				int sizechunk = texa.Width;
				for (int y = 0; y < Main.screenHeight + sizechunk; y += sizechunk)
				{
					for (int x = -sizechunk*2; x < Main.screenWidth + sizechunk*4; x += sizechunk)
					{
						//thevalue += (y * 0.15) + ((x) / 610);
						float thecoloralpha = 0.5f + (float)Math.Sin(thevalue) * 0.05f;


						Main.spriteBatch.End();
						Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
						ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye); shader.Apply(null);
						shader = GameShaders.Armor.GetShaderFromItemId(ItemID.MidnightRainbowDye);
						shader.Apply(null);
						spriteBatch.Draw(texa, new Rectangle(x-((int)(Main.GlobalTime*30) % sizechunk*3), y, sizechunk, sizechunk), (acolor * 1f) * (thecoloralpha * SGAmod.ProgramSkyAlpha));
					} }
			}


			if (maxDepth >= -0 && minDepth < -0)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
				ArmorShaderData shader2 = GameShaders.Armor.GetShaderFromItemId(ItemID.StardustDye); shader2.Apply(null);
				Texture2D sun = ModContent.GetTexture("Terraria/Sun");
				spriteBatch.Draw(sun, new Vector2(Main.screenWidth / 2, Main.screenHeight / 8), null, Color.DeepPink * SGAmod.ProgramSkyAlpha, 0, new Vector2(sun.Width / 2f, sun.Height / 2f), new Vector2(5f, 5f) * SGAmod.ProgramSkyAlpha, SpriteEffects.None, 0f);
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

			//Main.spriteBatch.End();
			//Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);

		}

		public override float GetCloudAlpha()
		{
			return 1f-(0.75f*SGAmod.ProgramSkyAlpha);
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			this._isActive = false;
		}

		public override void Reset()
		{
			this._isActive = false;
		}

		public override bool IsActive()
		{
			return this._isActive;
		}
	}
}
