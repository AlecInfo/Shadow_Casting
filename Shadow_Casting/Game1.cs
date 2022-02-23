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
        public static SpriteFont font;

        private Player player;

        private MapEdit mapEdit = MapEdit.Instance();


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

            font = Content.Load<SpriteFont>("font");

            player = new Player(450, 250, 15, 15);
            mapEdit.lightList.Add(player.light);

            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(5, 5), "add light"));
            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(78, 5), "add wall"));
            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(149, 5), "move"));

            mapEdit.buttonList[0].Click += mapEdit.BtnAddLight_Click;
            mapEdit.buttonList[1].Click += mapEdit.BtnAddWall_Click;
            mapEdit.buttonList[2].Click += mapEdit.BtnMove_Click;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            mapEdit.Update(gameTime);
            _penumbra.Hulls.Clear();
            foreach (Box item in mapEdit.boxList)
            {
                _penumbra.Hulls.Add(item.hull);
            }

            _penumbra.Lights.Clear();
            foreach (Light item in mapEdit.lightList)
            {
                _penumbra.Lights.Add(item);
            }

            foreach (var item in mapEdit.buttonList)
            {
                item.Update(gameTime);
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _penumbra.BeginDraw();

            GraphicsDevice.Clear(Color.White);


            _spriteBatch.Begin();

            _penumbra.Draw(gameTime);

            player.Draw(_spriteBatch, defaultTexture);
            
            foreach(Box box in mapEdit.boxList)
            {
                box.Draw(_spriteBatch, defaultTexture);
            }

            foreach (var item in mapEdit.buttonList)
            {
                item.Draw(_spriteBatch);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
