using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shadow_Casting
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Texture2D defaultTexture;

        private Player player;

        private List<Box> boxList = new List<Box>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            defaultTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            defaultTexture.SetData(new Color[] { Color.White });

            player = new Player(450, 250, 15, 15);

            boxList.Add(new Box(100, 100, 125, 35));
            boxList.Add(new Box(650, 150, 35, 110));
            boxList.Add(new Box(250, 250, 50, 50));
            boxList.Add(new Box(500, 325, 75, 25));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            _spriteBatch.Begin();

            player.Draw(_spriteBatch, defaultTexture);

            foreach(Box box in boxList)
            {
                box.Draw(_spriteBatch, defaultTexture);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
