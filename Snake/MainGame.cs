using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MenuScene menuScene;
        GameScene gameScene;
        GameOverScene gameOverScene;
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            // -- Creating new objects
            menuScene = new MenuScene(this);
            gameScene = new GameScene(this);
            gameOverScene = new GameOverScene(this);

            // -- Settings
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            // --
            MenuState.IsShowMainMenuScene = true;
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           if (MenuState.IsShowMainMenuScene)
            {
                menuScene.Update();
            }
           if (MenuState.IsShowGameScene)
            {
                gameScene.Update(gameTime);
            }
           if (MenuState.IsShowGameOverScene)
            {
                gameOverScene.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (MenuState.IsShowMainMenuScene)
            {
                menuScene.Draw(spriteBatch, gameTime);
            }
            if (MenuState.IsShowGameScene)
            {
                gameScene.Draw(spriteBatch, gameTime);
            }
            if (MenuState.IsShowGameOverScene)
            {
                gameOverScene.Draw(spriteBatch, gameTime);
            }

            base.Update(gameTime);


            base.Draw(gameTime);
        }
    }
}
