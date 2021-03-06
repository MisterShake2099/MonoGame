﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SimpleTwoPlayerTest
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public static Random Random;
		public static int ScreenWidth;
		public static int ScreenHeight;

		private Score _score;
		private List<Sprite> _sprites;



		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}


		protected override void Initialize()
		{
			var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
			form.Location = new System.Drawing.Point(450, 250);

			ScreenWidth = graphics.PreferredBackBufferWidth;
			ScreenHeight = graphics.PreferredBackBufferHeight;
			Random = new Random();

			base.Initialize();
		}

		
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Texture2D batTexture = Content.Load<Texture2D>("Bat");
			Texture2D ballTexture = Content.Load<Texture2D>("Ball");

			_score = new Score(Content.Load<SpriteFont>("Font"));

			_sprites = new List<Sprite>()
			{
				new Sprite(Content.Load<Texture2D>("Background")),
				new Bat(batTexture)
				{
					Position = new Vector2(20, (ScreenHeight / 2) - (batTexture.Height / 2)),
					Input = new Input()
					{
						Up = Keys.W,
						Down = Keys.S
					}
				},
				new Bat(batTexture)
				{
					Position = new Vector2(ScreenWidth - 20 - batTexture.Width, (ScreenHeight / 2) - (batTexture.Height / 2)),
					Input = new Input()
					{
						Up = Keys.Up,
						Down = Keys.Down
					}
				},
				new Ball(ballTexture)
				{
					Position = new Vector2((ScreenWidth / 2) - (ballTexture.Width / 2), (ScreenHeight / 2) - (ballTexture.Height / 2)),
					Score = _score
				}
			};
		}


		protected override void UnloadContent(){}


		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			foreach (Sprite sprite in _sprites)
			{
				sprite.Update(gameTime, _sprites);
			}

			base.Update(gameTime);
		}


		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			foreach (Sprite sprite in _sprites)
			{
				sprite.Draw(spriteBatch);
			}

			_score.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}


	}
}
