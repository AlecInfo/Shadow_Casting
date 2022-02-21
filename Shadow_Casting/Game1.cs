using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shadow_Casting
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch; Effect effect;
        SpriteFont font;
        string message = "";
        Rectangle boundryRectangle;
        public static Texture2D dotTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load up your stuff
            font = Content.Load<SpriteFont>("File");

            // load up your effect;
            effect = Content.Load<Effect>("effect");
            effect.CurrentTechnique = effect.Techniques["ConeTechnique"];

            // make a message
            message =
            " Ok so i had a bit of trouble figuring this out it turns out that it was working after all. " +
            "\n I was wrong when spritebatch passes the pos to the pixel shader. " +
            "\n It passes the untransformed coordinates so it does a bit of magic under the hood. " +
            "\n However its own vertex shader is still lining them up properly even if i pass matrix identity." +
            "\n So there we go if you intend however to pass a projection matrix ill bet youll need to do the transform on the boundry" +
            "\n +\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+\n+"
            ;

            dotTexture = TextureDotCreate(GraphicsDevice);

            boundryRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        public Texture2D TextureDotCreate(GraphicsDevice device)
        {
            Color[] data = new Color[1];
            data[0] = new Color(255, 255, 255, 255);
            Texture2D tex = new Texture2D(device, 1, 1);
            tex.SetData<Color>(data);
            return tex;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mouse = Mouse.GetState();
            playerLookAtTargetPosition = mouse.Position.ToVector2();

            base.Update(gameTime);
        }

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 playerLookAtTargetPosition = new Vector2(200, 20);
        float rangeInDegrees = 35f;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Matrix m = Matrix.Identity;
            effect.CurrentTechnique = effect.Techniques["ConeTechnique"];
            effect.Parameters["visibleBounds"].SetValue(GetVector4Rectangle(boundryRectangle));
            effect.Parameters["playerPosition"].SetValue(playerPosition);
            effect.Parameters["playerLookAtTarget"].SetValue(playerLookAtTargetPosition);
            effect.Parameters["rangeInDegrees"].SetValue(rangeInDegrees);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, effect, m);
            effect.Parameters["TextureA"].SetValue(font.Texture);
            _spriteBatch.Draw(dotTexture, boundryRectangle, Color.White);
            _spriteBatch.Draw(font.Texture, new Vector2(150, 320), Color.LightGray);
            _spriteBatch.DrawString(font, message, new Vector2(100f, 100f), Color.MonoGameOrange);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public Vector4 GetVector4Rectangle(Rectangle r)
        {
            int h = _graphics.GraphicsDevice.Viewport.Height;

            // Gl you gotta flip the y
            //return new Vector4(r.Left, (h - r.Top), r.Right, (h - r.Bottom));

            // Dx dont flip the y.
            return new Vector4(r.Left, r.Top, r.Right, r.Bottom);
        }
        public Vector4 GetVector4GpuRectangle(Rectangle r, Matrix vp)
        {
            var lt = Vector2.Transform(new Vector2(r.Left, r.Top), vp);
            var rb = Vector2.Transform(new Vector2(r.Right, r.Bottom), vp);
            return new Vector4(lt.X, lt.Y, rb.X, rb.Y);
        }
    }
}