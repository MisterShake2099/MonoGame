﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ScoreTrackingTest
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public static Random Random;

		public static int ScreenWidth;
		public static int ScreenHeight;

		private List<Sprite> _sprites;
		private SpriteFont _font;
		private float _timer;
		private Texture2D _appleTexture;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			Random = new Random();

			ScreenWidth = graphics.PreferredBackBufferWidth;
			ScreenHeight = graphics.PreferredBackBufferHeight;
		}

		protected override void Initialize()
		{
			var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
			form.Location = new System.Drawing.Point(450, 250);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Texture2D playerTexture = Content.Load<Texture2D>("EvilPacman");

			_sprites = new List<Sprite>()
			{
				new Player(playerTexture)
				{
					Input = new Input()
					{
						Left = Keys.A,
						Right = Keys.D,
						Up = Keys.W,
						Down = Keys.S
					},
					Position = new Vector2(100, 100),
					Color = Color.Blue,
					Speed = 5f
				},
				new Player(playerTexture)
				{
					Input = new Input()
					{
						Left = Keys.Left,
						Right = Keys.Right,
						Up = Keys.Up,
						Down = Keys.Down
					},
					Position = new Vector2(ScreenWidth - 100 - playerTexture.Width, 100),
					Color = Color.Green,
					Speed = 5f
				}
			};

			_font = Content.Load<SpriteFont>("Font");
			_appleTexture = Content.Load<Texture2D>("apple");
		}

		protected override void UnloadContent(){}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

			foreach (Sprite sprite in _sprites)
			{
				sprite.Update(gameTime, _sprites);
			}

			PostUpdate();
			Spawn();
			base.Update(gameTime);
		}

		private void Spawn()
		{
			if (_timer > 1)
			{
				_timer = 0;
				var xPos = Random.Next(0, ScreenWidth - _appleTexture.Width);
				var yPos = Random.Next(0, ScreenHeight - _appleTexture.Height);

				_sprites.Add(new Sprite(_appleTexture)
				{
					Position = new Vector2(xPos, yPos)
				});
			}
		}

		private void PostUpdate()
		{
			for (int i = 0; i < _sprites.Count; i++)
			{
				if (_sprites[i].IsRemoved)
				{
					_sprites.RemoveAt(i);
					i--;
				}
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			foreach (Sprite sprite in _sprites)
			{
				sprite.Draw(spriteBatch);
			}

			int fontY = 10;
			int i = 0;
			foreach (Sprite sprite in _sprites)
			{
				if (sprite is Player)
				{
					spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ++i, ((Player)sprite).Score), new Vector2(10, fontY += 20), Color.Black);
				}
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
