/********************************
 * Projet : Shadow Casting
 * Description : Proof of concept of 13th Haunted Street 
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


namespace Shadow_Casting
{
    public class Button
    {
        // Attributs
        public SpriteFont font;
        public Texture2D texture;
        public Vector2 position;
        public string text;

        public Color color;

        public bool isHovering = false;
        public event EventHandler Click;
        public bool clicked;
        public MouseState msState;
        public MouseState previusMsState;

        public Rectangle Size
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X + 10, (int)font.MeasureString(text).Y + 10);
            }
        }

        // Ctor
        public Button(Texture2D texture, SpriteFont font, Vector2 position, string text)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.text = text;
        }

        // Methods
        public void Update(GameTime gameTime)
        {
            previusMsState = msState;
            msState = Mouse.GetState();
            var msPosition = new Rectangle(msState.X, msState.Y, 1, 1);

            // If the mouse is in the button
            if (msPosition.Intersects(Size))
            {
                this.isHovering = true;

                // Create the click event
                if (msState.LeftButton == ButtonState.Pressed && previusMsState.LeftButton == ButtonState.Released)
                {
                    this.Click?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                this.isHovering = false;
            }

            // Change the button color when the mouse hovering the button
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
            // Draw the rectangle
            spriteBatch.Draw(this.texture, this.Size, this.color);

            // Draw the text on the rectangle
            spriteBatch.DrawString(this.font, this.text, new Vector2(this.position.X + 5, this.position.Y + 5), Color.White);
        }

    }
}
