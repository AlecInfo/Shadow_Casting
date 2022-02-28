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
    class Box
    {
        // Attributs
        private Vector2 _position;
        private Vector2 _size;

        public Hull hull;

        public Vector2 Position { 
            get => this._position;
            set {
                this._position = value;
                this.hull.Position = value;
            } 
        }

        public Vector2 Size {
            get => this._size;
            set {
                this._size = value;
                this.hull.Scale = value;
            }
        }


        // Ctor
        public Box(float posX, float posY, float sizeX, float sizeY)
        {
            this._position = new Vector2(posX, posY);
            this._size = new Vector2(sizeX, sizeY);

            // Create the wall
            this.hull = new Hull(new Vector2(0.49f), new Vector2(-0.49f, 0.49f), new Vector2(-0.49f), new Vector2(0.49f, -0.49f))
            {
                Position = this._position,
                Scale = this._size,
                Origin = new Vector2(-0.5f)
            };
        }


        // Methods
        public void Draw(SpriteBatch spriteBatch, Texture2D defaultTexture)
        {
            // Draw the wall
            spriteBatch.Draw(defaultTexture,
                new Rectangle(
                    (int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y), null, new Color(79, 104, 102), 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
