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
    enum Direction
    {
        DOWN,
        UP,
        LEFT,
        RIGHT
    }
    class GameScene : DrawableGameComponent
    {

        Texture2D unCheckedCheckBoxTexture;
        Texture2D checkedCheckBoxTexture;
        Rectangle checkBoxRectangle;
        bool isCheckBoxChecked = false;
        SpriteFont checkBoxDescription;

        Game game;
        bool isStart = true;
        int blockSize = 20;
        bool allowSnakeTurnBack = false;
        Texture2D playerBodyTexture;
        Texture2D itemTexture;
        Texture2D backgroundTexture;
        MouseState previousMouseState;
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
            previousMouseState = Mouse.GetState();
        }

        protected override void LoadContent()
        {
            playerBodyTexture = game.Content.Load<Texture2D>("PlayerBody");
            backgroundTexture = game.Content.Load<Texture2D>("background");
            itemTexture = game.Content.Load<Texture2D>("Item");
            scoreFont = game.Content.Load<SpriteFont>("Arial");
            checkedCheckBoxTexture = game.Content.Load<Texture2D>("checked-checkbox");
            unCheckedCheckBoxTexture = game.Content.Load<Texture2D>("unchecked-checkbox");
            checkBoxDescription = scoreFont = game.Content.Load<SpriteFont>("Arial");

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(playerBodyTexture, playerBodyRectangleList[0], Color.Red);

            int i = 0;
            while (bodyIndex != i)
            {
                i++;
                if (i != 0)
                {
                    spriteBatch.Draw(playerBodyTexture, playerBodyRectangleList[i], Color.Black);
                }
            }
            spriteBatch.Draw(itemTexture, itemRectangle, Color.Green);
            spriteBatch.DrawString(scoreFont, "Score " + bodyIndex, new Vector2(0, 0), Color.DarkBlue);
            spriteBatch.DrawString(checkBoxDescription, "Waz moze zawracac: " , new Vector2(GraphicsDevice.Viewport.Width / 2, 10), Color.Blue);

            if (!isCheckBoxChecked)
                spriteBatch.Draw(unCheckedCheckBoxTexture, checkBoxRectangle, Color.Wheat);
            else
                spriteBatch.Draw(checkedCheckBoxTexture, checkBoxRectangle, Color.Wheat);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            if (isStart)
            {
                playerBodyRectangleList.Clear();
                addBodyBlock();
                calculateItemsSize();
                GenerateNewItem();
                isStart = false;
            }
            else
            {
                allowSnakeTurnBack = isCheckBoxChecked;
                handleKeysPressing(allowSnakeTurnBack);
                CheckCollision();
                moveSnake(gameTime);
            }
        }

        protected void calculateItemsSize()
        {
            backgroundRectangle.Width = GraphicsDevice.Viewport.Width;
            backgroundRectangle.Height = GraphicsDevice.Viewport.Height;
            itemRectangle.Width = blockSize;
            itemRectangle.Height = blockSize;
            checkBoxRectangle.Width = 34;
            checkBoxRectangle.Height = 34;
            checkBoxRectangle.X = GraphicsDevice.Viewport.Width - 40;
            checkBoxRectangle.Y = 10;


        }

        private void addBodyBlock()
        {
            if (isStart)
            {
                playerBodyRectangleList.Add(new Rectangle(100, 100, blockSize, blockSize));
            }
            else
            {
                bodyIndex++;
                if (isMoveDown)
                {
                    playerBodyRectangleList.Add(new Rectangle(playerBodyRectangleList[bodyIndex - 1].X, playerBodyRectangleList[bodyIndex - 1].Y - playerBodyRectangleList[bodyIndex - 1].Height, blockSize, blockSize));
                }
                if (isMoveUp)
                {
                    playerBodyRectangleList.Add(new Rectangle(playerBodyRectangleList[bodyIndex - 1].X, playerBodyRectangleList[bodyIndex - 1].Y + playerBodyRectangleList[bodyIndex - 1].Height, blockSize, blockSize));
                }
                if (isMoveLeft)
                {
                    playerBodyRectangleList.Add(new Rectangle(playerBodyRectangleList[bodyIndex - 1].X - playerBodyRectangleList[bodyIndex - 1].Width, playerBodyRectangleList[bodyIndex - 1].Y, blockSize, blockSize));
                }
                if (isMoveRight)
                {
                    playerBodyRectangleList.Add(new Rectangle(playerBodyRectangleList[bodyIndex - 1].X + playerBodyRectangleList[bodyIndex - 1].Width, playerBodyRectangleList[bodyIndex - 1].Y, blockSize, blockSize));
                }
                GenerateNewItem();
            }
        }

        private void handleKeysPressing(bool allowTurnBack)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                if (isMoveUp)
                {
                    if (allowTurnBack)
                    {
                        changeStatesOfDirectionFlags(Direction.DOWN);
                    }
                }
                else
                    changeStatesOfDirectionFlags(Direction.DOWN);
            }
            else if (state.IsKeyDown(Keys.Up))
            {
                if (isMoveDown)
                {
                    if (allowTurnBack)
                        changeStatesOfDirectionFlags(Direction.UP);
                }
                else
                    changeStatesOfDirectionFlags(Direction.UP);
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                if (isMoveRight)
                {
                    if (allowTurnBack)
                    {
                        changeStatesOfDirectionFlags(Direction.LEFT);
                    }
                }
                else
                    changeStatesOfDirectionFlags(Direction.LEFT);
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                if (isMoveLeft)
                {
                    if (allowTurnBack)
                    {
                        changeStatesOfDirectionFlags(Direction.RIGHT);
                    }
                }
                else
                    changeStatesOfDirectionFlags(Direction.RIGHT);
            }
        }

        private void moveSnake(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % 75 == 0)
            {
                if (bodyIndex > 0)
                {
                    int index = bodyIndex;
                    int i = bodyIndex * 2;
                    while (i != bodyIndex)
                    {
                        playerBodyRectangleList[index] = new Rectangle(playerBodyRectangleList[index - 1].X, playerBodyRectangleList[index - 1].Y, blockSize, blockSize);
                        index--;
                        i--;
                    }
                }
                if (isMoveDown)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X, playerBodyRectangleList[0].Y + playerBodyRectangleList[0].Height, blockSize, blockSize);
                }
                if (isMoveUp)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X, playerBodyRectangleList[0].Y - playerBodyRectangleList[0].Height, blockSize, blockSize);
                }
                if (isMoveLeft)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X - playerBodyRectangleList[0].Width , playerBodyRectangleList[0].Y, blockSize, blockSize);
                }
                if (isMoveRight)
                {
                    playerBodyRectangleList[0] = new Rectangle(playerBodyRectangleList[0].X + playerBodyRectangleList[0].Width, playerBodyRectangleList[0].Y, blockSize, blockSize);
                }
            }
        }
        private void GenerateNewItem()
        {
            Random r = new Random();
            itemRectangle.X = r.Next(GraphicsDevice.Viewport.Width - itemRectangle.Width);
            itemRectangle.Y = r.Next(GraphicsDevice.Viewport.Height - itemRectangle.Height);

        }

        private void CheckCollision()
        {
            if (playerBodyRectangleList[0].Intersects(itemRectangle))
            {
                addBodyBlock();
            }


            if (checkBoxRectangle.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
            {
                MouseState currentMouseState = Mouse.GetState();
                if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    isCheckBoxChecked = !isCheckBoxChecked;
                }
                previousMouseState = Mouse.GetState();
            }    
                
        }

        private void changeStatesOfDirectionFlags(Direction direction)
        {
            switch (direction)
            {
                case Direction.DOWN:
                    {
                        isMoveDown = true;
                        isMoveUp = false;
                        isMoveLeft = false;
                        isMoveRight = false;
                        break;
                    }
                case Direction.UP:
                    {
                        isMoveUp = true;
                        isMoveDown = false;
                        isMoveLeft = false;
                        isMoveRight = false;
                        break;
                    }
                case Direction.LEFT:
                    {
                        isMoveLeft = true;
                        isMoveUp = false;
                        isMoveDown = false;
                        isMoveRight = false;
                        break;
                    }
                case Direction.RIGHT:
                    {
                        isMoveRight = true;
                        isMoveUp = false;
                        isMoveLeft = false;
                        isMoveDown = false;
                        break;
                    }
            }
        }

    }
}
