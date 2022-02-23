using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace Shadow_Casting
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PenumbraComponent _penumbra;

        public static Texture2D defaultTexture;

        private Player player;

        private List<Box> boxList = new List<Box>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _penumbra = new PenumbraComponent(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _penumbra.Initialize();

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

            foreach (Box item in boxList)
            {
                _penumbra.Hulls.Add(item.hull);
            }

            _penumbra.Lights.Add(player.light);
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
            _penumbra.BeginDraw();

            GraphicsDevice.Clear(Color.White);


            _spriteBatch.Begin();

            _penumbra.Draw(gameTime);

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
