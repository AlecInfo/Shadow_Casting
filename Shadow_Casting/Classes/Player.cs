/********************************
 * Projet : Shadow Casting
 * Description : Proof of concept of 13th Haunted Street 
 *  to experiment penumbra https://github.com/AlecInfo/13th_Haunted_Street
 * 
 * Date : 28/02/2022
 * Version : 1.0
 * Auteur : Rodrigues Marques Marco, Piette Alec, Arcidiacono Jérémie, Viera Luis David
*******************************/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;


namespace Shadow_Casting
{
    class Player
    {
        // Attributs
        public Vector2 position;
        public Vector2 size;

        private float speed = 0.25f;

        public Light light;


        // Ctor
        public Player(float posX, float posY, float sizeX, float sizeY)
        {
            this.position = new Vector2(posX, posY);
            this.size = new Vector2(sizeX, sizeY);

            // Create light who to follow the player
            this.light = new PointLight
            {
                Scale = new Vector2(600),
                Position = this.position,
                ShadowType = ShadowType.Occluded,
                Radius = 0,
                Intensity = 1
            };
        }


        // Methods
        public void Update(GameTime gameTime)
        {
            KeyboardState kbdState = Keyboard.GetState();

            // Mouvements
            if (kbdState.IsKeyDown(Keys.W))
            {
                this.position.Y -= this.speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (kbdState.IsKeyDown(Keys.A))
            {
                this.position.X -= this.speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (kbdState.IsKeyDown(Keys.S))
            {
                this.position.Y += this.speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (kbdState.IsKeyDown(Keys.D))
            {
                this.position.X += this.speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            light.Position = this.position;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D defaultTexture)
        {
            // Draw player
            spriteBatch.Draw(defaultTexture, 
                new Rectangle((int)position.X,
                (int)position.Y, (int)size.X, (int)size.Y), null, Color.LightSlateGray, 0f, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
        }
    }
}
