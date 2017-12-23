using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class MenuScene : DrawableGameComponent
    {
        Game game;
        Texture2D playButtonTexture;
        Texture2D exitButtonTexture;
        Texture2D logoTexture;

        Rectangle playButtonRectangle;
        Rectangle exitButtonRectangle;
        Rectangle logoRectangle;

        Color playButtonColor = Color.White;
        Color exitButtonColor = Color.White;

        MouseState mouseState;
        Rectangle cursorRectangle;

        public MenuScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            playButtonTexture = game.Content.Load<Texture2D>("PlayButton-300x110");
            exitButtonTexture = game.Content.Load<Texture2D>("ExitButton-300x110");
            logoTexture = game.Content.Load<Texture2D>("Logo-300x116");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(logoTexture, logoRectangle, Color.White);
            spriteBatch.Draw(playButtonTexture, playButtonRectangle, playButtonColor);
            spriteBatch.Draw(exitButtonTexture, exitButtonRectangle, exitButtonColor);
            spriteBatch.End();
        }

        public void Update()
        {
            CalculateItemsSize();
            CalculateItemsPositions();
            updateCursorPosition();
            buttonsEvents();

        }

        private void CalculateItemsSize()
        {
            int height = GraphicsDevice.Viewport.Height / 7;
            int width = GraphicsDevice.Viewport.Height / 3;

            playButtonRectangle.Height = height;
            playButtonRectangle.Width = width;

            exitButtonRectangle.Height = height;
            exitButtonRectangle.Width = width;

            logoRectangle.Height = GraphicsDevice.Viewport.Height / 4;
            logoRectangle.Width = GraphicsDevice.Viewport.Width / 2;
        }
        private void CalculateItemsPositions()
        {
            logoRectangle.X = GraphicsDevice.Viewport.Width / 2 - logoRectangle.Width / 2;

            int positionX = GraphicsDevice.Viewport.Width / 2 - playButtonRectangle.Width / 2;

            playButtonRectangle.X = positionX;
            playButtonRectangle.Y = logoRectangle.Height + playButtonRectangle.Height /3;

            exitButtonRectangle.X = positionX;
            //exitButtonRectangle.Y = logoRectangle.Height + playButtonRectangle.Height + exitButtonRectangle.Height;
            exitButtonRectangle.Y = playButtonRectangle.Y + 12 * playButtonRectangle.Height / 10;

        }
        private void updateCursorPosition()
        {
            mouseState = Mouse.GetState();
            cursorRectangle.X = mouseState.X;
            cursorRectangle.Y = mouseState.Y;
        }
        private void buttonsEvents()
        {
            if (playButtonRectangle.Intersects(cursorRectangle))
            {
                playButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    playButtonColor = Color.Red;
                    MenuState.IsShowGameScene = true;
                }
            }
            else
            {
                playButtonColor = Color.White;
            }

            if (exitButtonRectangle.Intersects(cursorRectangle))
            {
                exitButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    exitButtonColor = Color.Red;
                    game.Exit();
                }
            }
            else
            {
                exitButtonColor = Color.White;
            }
        }

    }
}
