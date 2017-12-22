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
        public GameScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
        }

        protected override void LoadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public void Update()
        {

        }
    }
}
