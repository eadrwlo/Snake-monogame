using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    class GameScene : DrawableGameComponent
    {
        Game game;
        bool isStart = true;
        int blockSize;

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
            //spriteBatch.Draw(playerBodyTexture, playerBodyRectangleList[0], Color.Red);
            spriteBatch.End();
        }

        public void Update()
        {
            calculateItemsSize();
        }

        protected void calculateItemsSize()
        {
            backgroundRectangle.Width = GraphicsDevice.Viewport.Width;
            backgroundRectangle.Height = GraphicsDevice.Viewport.Height;

        }
    }
}
