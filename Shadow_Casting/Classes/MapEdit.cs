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
using Penumbra;


namespace Shadow_Casting
{
    class MapEdit
    {
        // Attributs
        static MapEdit instance;

        public bool isMouseClicked = false;
        public Vector2 mousePosition;
        Rectangle mouseRectangle;

        public bool boxAsBeenAdded = false;
        public bool lightAsBeenAdded = false;

        public bool mouseIsInButton = false;

        public int lightSelect;
        public int boxSelect;
        public bool isLightSelected = false;
        public bool isBoxSelected = false;

        private Texture2D _bulbTexture;
        public enum Edit
        {
            None,
            AddWall,
            AddLight,
            Delete,
            Move
        }
        private Edit _option;

        public List<Box> boxList = new List<Box>();
        public List<Light> lightList = new List<Light>();
        public List<Button> buttonList = new List<Button>();


        // Ctor
        protected MapEdit(Texture2D bulbTexture)
        {
            this._bulbTexture = bulbTexture;
        }

        public static MapEdit Instance(Texture2D bulbTexture)
        {
            // Create a Singleton to avoid duplicates
            if (instance == null)
            {
                instance = new MapEdit(bulbTexture);
            }
            return instance;
        }

        // Methods
        public void Update(GameTime gameTime)
        {
            // Get Mouse State 
            MouseState msState = Mouse.GetState();
            this.mousePosition = msState.Position.ToVector2();
            this.mouseRectangle = new Rectangle((int)this.mousePosition.X, (int)this.mousePosition.Y, 0, 0);

            // If mouse is pressed
            if (msState.LeftButton == ButtonState.Pressed)
                this.isMouseClicked = true;
            else
                this.isMouseClicked = false;

            // If the mouse is in one of the buttons
            foreach (Button item in buttonList)
            {
                if (mouseRectangle.Intersects(item.Size))
                {
                    this.mouseIsInButton = true;
                }
            }

            // Choose the option according to the clicked button
            if (!this.mouseIsInButton)
            {
                switch (this._option)
                { 
                    case Edit.AddLight:
                        AddLight();
                        break;

                    case Edit.AddWall:
                        AddWall();
                        break;

                    case Edit.Move:
                        Move();
                        break;

                    case Edit.Delete:
                        Delete();
                        break;
                }
            }
            else
            {
                this.mouseIsInButton = false;
            }
        }

        /// <summary>
        /// Add a wall on the first click and change the size until the mouse is released
        /// </summary>
        void AddWall()
        {
            if (this.isMouseClicked)
            {
                if (!this.boxAsBeenAdded)
                {
                    // Create wall
                    this.boxList.Add(new Box(this.mousePosition.X, this.mousePosition.Y, 0, 0));
                    this.boxAsBeenAdded = true;
                }
                else
                {
                    // Change the size
                    this.boxList[this.boxList.Count - 1].Size = this.mousePosition - this.boxList[this.boxList.Count - 1].Position;
                }    
            }
            else
            {
                this.boxAsBeenAdded = false;
            }
        }

        /// <summary>
        /// Add a light at the mouse position
        /// </summary>
        void AddLight()
        {
            if (this.isMouseClicked)
            {
                if (!this.lightAsBeenAdded)
                {
                    // Add light
                    this.lightList.Add(new PointLight
                    {
                        Scale = new Vector2(400),
                        Position = this.mousePosition,
                        ShadowType = ShadowType.Occluded,
                        Radius = 0,
                        Intensity = 0.5f,
                    });
                    this.lightAsBeenAdded = true;
                }
            }
            else
            {
                this.lightAsBeenAdded = false;
            }
        }


        /// <summary>
        /// Checks if the mouse is in an object, and when a click has been made,
        /// a change of position can be done as long as the click has not been released
        /// </summary>
        void Move()
        {
            // Walls list
            for (int i = 0; i < boxList.Count; i++)
            {
                Rectangle itemRectangle = new Rectangle((int)boxList[i].Position.X, (int)boxList[i].Position.Y, (int)boxList[i].Size.X, (int)boxList[i].Size.Y);

                // Verification 
                if (this.isMouseClicked && !isBoxSelected)
                {
                    if (mouseRectangle.Intersects(itemRectangle))
                    {
                        boxSelect = i;
                        isBoxSelected = true;
                    }
                }
                if ((!this.isMouseClicked && this.isBoxSelected) || (this.isMouseClicked && this.isLightSelected))
                {
                    isBoxSelected = false;
                }

                // Change position
                if (isBoxSelected)
                {
                    boxList[boxSelect].Position = new Vector2(this.mousePosition.X - boxList[boxSelect].Size.X / 2, this.mousePosition.Y - boxList[boxSelect].Size.Y / 2);
                }
            }

            // Lights list
            for (int i = 1; i < lightList.Count; i++)
            {
                Rectangle bulbRectangle = new Rectangle((int)(lightList[i].Position.X - _bulbTexture.Height / 2 * 0.135f), (int)(lightList[i].Position.Y - _bulbTexture.Height / 2 * 0.135f), (int)(_bulbTexture.Width * 0.135f), (int)(_bulbTexture.Height * 0.135f));

                // Verification
                if (this.isMouseClicked && !isLightSelected)
                {
                    if (mouseRectangle.Intersects(bulbRectangle))
                    {
                        lightSelect = i;
                        isLightSelected = true;
                    }
                }
                if ((!this.isMouseClicked && this.isLightSelected) || (this.isMouseClicked && this.isBoxSelected))
                {
                    isLightSelected = false;
                }

                // Change position
                if (isLightSelected)
                {   
                    lightList[lightSelect].Position = new Vector2(this.mousePosition.X, this.mousePosition.Y);    
                } 
            }
        }

        /// <summary>
        /// Checks if the mouse is in an object, and when a click has been made,
        /// delete this object.
        /// </summary>
        void Delete()
        {
            // Lights list
            for (int i = lightList.Count - 1; 0 < i; i--)
            {
                Rectangle bulbRectangle = new Rectangle((int)(lightList[i].Position.X - _bulbTexture.Height / 2 * 0.135f), (int)(lightList[i].Position.Y - _bulbTexture.Height / 2 * 0.135f), (int)(_bulbTexture.Width * 0.135f), (int)(_bulbTexture.Height * 0.135f));

                // Check
                if (this.isMouseClicked && !isLightSelected)
                {
                    if (mouseRectangle.Intersects(bulbRectangle))
                    {
                        // Delete
                        lightList.Remove(lightList[i]);
                    }
                }
            }

            // Walls list
            for (int i = boxList.Count - 1; 0 <= i; i--)
            {
                Rectangle itemRectangle = new Rectangle((int)boxList[i].Position.X, (int)boxList[i].Position.Y, (int)boxList[i].Size.X, (int)boxList[i].Size.Y);

                // Check
                if (this.isMouseClicked && !isBoxSelected)
                {
                    if (mouseRectangle.Intersects(itemRectangle))
                    {
                        // Delete
                        boxList.Remove(boxList[i]);
                    }
                }
            }
        }

        // Create button click event 
        public void BtnAddLight_Click(object sender, EventArgs e)
        {
            this._option = Edit.AddLight;
        }

        public void BtnAddWall_Click(object sender, EventArgs e)
        {
            this._option = Edit.AddWall;
        }

        public void BtnMove_Click(object sender, EventArgs e)
        {
            this._option = Edit.Move;
        }

        public void BtnDelete_Click(object sender, EventArgs e)
        {
            this._option = Edit.Delete;
        }
    }
}
