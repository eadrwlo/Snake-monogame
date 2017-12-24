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
    class GameOverScene : DrawableGameComponent
    {
        String writing = "Nacisnije spacje aby rozpoczac ponownie !";
        Game game;
        SpriteFont label;
        public GameOverScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {
            label = game.Content.Load<SpriteFont>("Arial");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);
            spriteBatch.Begin();
            spriteBatch.DrawString(label, writing, new Vector2(GraphicsDevice.Viewport.Width / 2 - label.MeasureString(writing).X / 2, GraphicsDevice.Viewport.Height / 2), Color.Red);
            spriteBatch.End();
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                MenuState.IsShowMainMenuScene = true;
            }
        }
    }
}
