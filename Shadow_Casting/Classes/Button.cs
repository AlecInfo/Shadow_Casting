using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Shadow_Casting
{
    public class Button
    {
        public SpriteFont font;
        public Texture2D texture;
        public Vector2 position;
        public string text;

        public Color color;

        public bool isHovering = false;
        public event EventHandler Click;
        public bool clicked;

        public Rectangle Size
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X + 10, (int)font.MeasureString(text).Y + 10);
            }
        }

        public Button(Texture2D texture, SpriteFont font, Vector2 position, string text)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.text = text;
        }

        public void Update(GameTime gameTime)
        {
            MouseState msState = Mouse.GetState();
            var msPosition = new Rectangle(msState.X, msState.Y, 1, 1);

            if (msPosition.Intersects(Size))
            {
                this.isHovering = true;

                if (msState.LeftButton == ButtonState.Pressed)
                {
                    this.Click?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                this.isHovering = false;
            }

            if (this.isHovering)
            {
                this.color = Color.LightGray;
            }
            else
            {
                this.color = Color.Gray;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Size, this.color);

            spriteBatch.DrawString(this.font, this.text, new Vector2(this.position.X + 5, this.position.Y + 5), Color.White);
        }

    }
}
