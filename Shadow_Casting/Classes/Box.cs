using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shadow_Casting
{
    class Box
    {
        // Attributs
        public Vector2 position;
        public Vector2 size;


        // Ctor
        public Box(float posX, float posY, float sizeX, float sizeY)
        {
            this.position = new Vector2(posX, posY);
            this.size = new Vector2(sizeX, sizeY);
        }


        // Methods
        public void Draw(SpriteBatch spriteBatch, Texture2D defaultTexture)
        {
            spriteBatch.Draw(defaultTexture,
                new Rectangle((int)position.X,
                (int)position.Y, (int)size.X, (int)size.Y), null, new Color(79, 104, 102), 0f, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0f);
        }
    }
}
