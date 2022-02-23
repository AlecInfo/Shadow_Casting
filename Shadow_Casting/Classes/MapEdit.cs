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
        static MapEdit instance;

        public bool isMouseClicked = false;
        public bool boxAsBeenAdded = false;
        public bool lightAsBeenAdded = false;
        public Vector2 mousePosition;
        public bool mouseIsInButton = false;

        public enum Edit
        {
            None,
            AddWall,
            AddLight,
            Move
        }
        private Edit _option;

        public List<Box> boxList = new List<Box>();
        public List<Light> lightList = new List<Light>();
        public List<Button> buttonList = new List<Button>();

        protected MapEdit()
        {

        }

        public static MapEdit Instance()
        {
            if (instance == null)
            {
                instance = new MapEdit();
            }
            return instance;
        }

        public void Update(GameTime gameTime)
        {
            MouseState msState = Mouse.GetState();
            this.mousePosition = msState.Position.ToVector2();
            var mouseInButton = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

            // if mouse is pressed
            if (msState.LeftButton == ButtonState.Pressed)
                this.isMouseClicked = true;
            else
                this.isMouseClicked = false;

            foreach (var item in buttonList)
            {
                if (mouseInButton.Intersects(item.Size))
                {
                    this.mouseIsInButton = true;
                }
            }

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
                        break;
                }
            }
            else
            {
                this.mouseIsInButton = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        void AddWall()
        {
            if (this.isMouseClicked)
            {
                if (!this.boxAsBeenAdded)
                {
                    this.boxList.Add(new Box(this.mousePosition.X, this.mousePosition.Y, 0, 0));
                    this.boxAsBeenAdded = true;
                }
                else
                {
                    this.boxList[this.boxList.Count - 1].size = this.mousePosition - this.boxList[this.boxList.Count - 1].position;
                    this.boxList[this.boxList.Count - 1].hull.Scale = this.boxList[this.boxList.Count - 1].size;
                }    
            }
            else
            {
                this.boxAsBeenAdded = false;
            }
        }

        void AddLight()
        {
            if (this.isMouseClicked)
            {
                if (!this.lightAsBeenAdded)
                {
                    this.lightList.Add(new PointLight
                    {
                        Scale = new Vector2(400),
                        Position = this.mousePosition,
                        ShadowType = ShadowType.Occluded,
                        Radius = 0,
                        Intensity = 0.5f
                    });
                    this.lightAsBeenAdded = true;
                }
            }
            else
            {
                this.lightAsBeenAdded = false;
            }
        }

        public void BtnAddLight_Click(object sender, EventArgs e)
        {
             _option = Edit.AddLight;
        }

        public void BtnAddWall_Click(object sender, EventArgs e)
        {
            _option = Edit.AddWall;
        }

        public void BtnMove_Click(object sender, EventArgs e)
        {
            _option = Edit.Move;
        }
    }
}
