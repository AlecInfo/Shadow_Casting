/********************************
 * Projet : Shadow Casting
 * Description : Prooft of concept of 13th Haunted Street 
 *  to experiment penumbra https://github.com/AlecInfo/13th_Haunted_Street
 * 
 * Date : 28/02/2022
 * Version : 1.0
 * Auteur : Rodrigues Marques Marco, Piette Alec, Arcidiacono Jérémie, Viera Luis David
*******************************/

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
        // Attributs
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PenumbraComponent _penumbra;

        public static Texture2D defaultTexture;
        public static SpriteFont font;
        public static Texture2D bulb;

        private Player player;

        private MapEdit mapEdit;

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

            // Load Textures and Font
            defaultTexture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            defaultTexture.SetData(new Color[] { Color.White });

            font = Content.Load<SpriteFont>("font");
            bulb = Content.Load<Texture2D>("bulb");

            // Load player
            player = new Player(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 15, 15);

            // Load editing map
            mapEdit = MapEdit.Instance(bulb);

            // Load buttons and light for editing map
            mapEdit.lightList.Add(player.light);
            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(5, 5), "add light"));
            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(78, 5), "add wall"));
            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(149, 5), "move"));
            mapEdit.buttonList.Add(new Button(defaultTexture, font, new Vector2(201, 5), "delete"));

            // Load Click event for the buttons
            mapEdit.buttonList[0].Click += mapEdit.BtnAddLight_Click;
            mapEdit.buttonList[1].Click += mapEdit.BtnAddWall_Click;
            mapEdit.buttonList[2].Click += mapEdit.BtnMove_Click;
            mapEdit.buttonList[3].Click += mapEdit.BtnDelete_Click;

        }

        protected override void Update(GameTime gameTime)
        {
            // Quit the game 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update player
            player.Update(gameTime);
            
            // Update walls (Clear and refresh)
            mapEdit.Update(gameTime);
            _penumbra.Hulls.Clear();
            foreach (Box item in mapEdit.boxList)
            {
                _penumbra.Hulls.Add(item.hull);
            }

            // Update lights (Clear and refresh)
            _penumbra.Lights.Clear();
            foreach (Light item in mapEdit.lightList)
            {
                _penumbra.Lights.Add(item);
            }

            // Uptade buttons
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

            // Draw player
            player.Draw(_spriteBatch, defaultTexture);
            
            // Draw Walls
            foreach(Box box in mapEdit.boxList)
            {
                box.Draw(_spriteBatch, defaultTexture);
            }

            // Draw Buttons
            foreach (Button item in mapEdit.buttonList)
            {
                item.Draw(_spriteBatch);
            }

            // Draw Lights
            for (int i = 1; i < mapEdit.lightList.Count; i++)
            {
                _spriteBatch.Draw(bulb, mapEdit.lightList[i].Position, null, Color.White, 0f, bulb.Bounds.Center.ToVector2(), 0.135f, SpriteEffects.None, 0f);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
