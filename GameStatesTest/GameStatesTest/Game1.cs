﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStatesTest
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private State _currentState;
		private State _nextState;

		public void ChangeState(State state)
		{
			_nextState = state;
		}


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}


		protected override void Initialize()
		{
			IsMouseVisible = true;
			base.Initialize();
		}


		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			_currentState = new MenuState(this, graphics.GraphicsDevice, Content);
		}


		protected override void UnloadContent(){}


		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			if (_nextState != null)
			{
				_currentState = _nextState;
				_nextState = null;
			}

			_currentState.Update(gameTime);
			_currentState.PostUpdate(gameTime);

			base.Update(gameTime);
		}


		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_currentState.Draw(gameTime, spriteBatch);

			base.Draw(gameTime);
		}


	}
}
