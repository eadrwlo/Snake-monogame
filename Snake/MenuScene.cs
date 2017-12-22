using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    class MenuScene : DrawableGameComponent
    {
        Game game;
        Texture2D playButtonTexture;
        Texture2D exitButtonTexture;
        Texture2D logoTexture;


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
            spriteBatch.Draw(logoTexture, new Rectangle(10, 10, 400, 100), Color.Wheat);
            spriteBatch.End();
        }

        public void Update()
        {

        }
    }
}
