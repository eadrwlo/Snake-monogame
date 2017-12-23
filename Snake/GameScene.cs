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
    class GameScene : DrawableGameComponent
    {
        Game game;
        bool isStart = true;
        int blockSize = 10;

        Texture2D playerBodyTexture;
        Texture2D itemTexture;
        Texture2D backgroundTexture;

        Rectangle itemRectangle;
        Rectangle backgroundRectangle;

        SpriteFont scoreFont;

        // Snake
        int bodyIndex = 0;
        List<Rectangle> playerBodyRectangleList = new List<Rectangle>();
        bool isMoveDown;
        bool isMoveUp;
        bool isMoveLeft;
        bool isMoveRight;


        public GameScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            playerBodyTexture = game.Content.Load<Texture2D>("PlayerBody");
            backgroundTexture = game.Content.Load<Texture2D>("background");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(playerBodyTexture, playerBodyRectangleList[0], Color.Red);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (isStart)
            {
                addBodyBlock();
                calculateItemsSize();
                isStart = false;
            }
            else
            {
                handleKeysPressing();
                moveSnake(gameTime);
            }            
        }

        protected void calculateItemsSize()
        {
            backgroundRectangle.Width = GraphicsDevice.Viewport.Width;
            backgroundRectangle.Height = GraphicsDevice.Viewport.Height;
        }

        private void addBodyBlock()
        {
            if (isStart)
            {
                playerBodyRectangleList.Add(new Rectangle(100, 100, blockSize, blockSize));
            }
        }

        private void handleKeysPressing()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                isMoveDown = true;
                isMoveUp = false;
                isMoveLeft = false;
                isMoveRight = false;
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                isMoveUp = true;
                isMoveDown = false;
                isMoveLeft = false;
                isMoveRight = false;
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                isMoveLeft = true;
                isMoveUp = false;
                isMoveDown = false;
                isMoveRight = false;

            }
            else if (state.IsKeyDown(Keys.Right))
            {
                isMoveRight = true;
                isMoveUp = false;
                isMoveLeft = false;
                isMoveDown = false;

            }
        }

        private void moveSnake(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % 2 == 0)
            {
                if (isMoveDown)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X, playerBodyRectangleList[0].Y + playerBodyRectangleList[0].Height / 10, blockSize, blockSize);
                }
                if (isMoveUp)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X, playerBodyRectangleList[0].Y - playerBodyRectangleList[0].Height / 10, blockSize, blockSize);
                }
                if (isMoveLeft)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X - playerBodyRectangleList[0].Width / 10, playerBodyRectangleList[0].Y, blockSize, blockSize);
                }
                if (isMoveRight)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X + playerBodyRectangleList[0].Width / 10, playerBodyRectangleList[0].Y, blockSize, blockSize);
                }
            }
           
        }
    }
}
