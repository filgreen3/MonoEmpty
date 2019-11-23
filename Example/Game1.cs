using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoEmpty.EmptyComponent;
using MonoEmpty.UI;
using MonoEmpty.EmptyComponent.DebugHelp;

namespace Example
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Camera camera;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Vector2 inputOffset;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {


            // this.graphics.ToggleFullScreen();
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 640;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 480;   // set this value to the desired height of your window
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();

        }
        protected override void LoadContent()
        {
            new GameCenter();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprite.device = GraphicsDevice;
            UIManager.spriteBatchUI = spriteBatch;
            new Debug(Content.Load<SpriteFont>("defFont"), GraphicsDevice);
            new MovableHandle(Content.Load<Texture2D>("21"), Point.Zero, ScreenAnchor.UP_Right);



            var Jim = new GameObject(typeof(Sprite));
            Jim.AddComponent<Sprite>();
            Jim.GetComponent<Sprite>().texture = Content.Load<Texture2D>("2");
            Jim.AddComponent<RotationAtMouse>();
            Jim.GetComponent<Sprite>().Inicial();

            Jim = new GameObject(typeof(Sprite));
            Jim.AddComponent<Sprite>();
            Jim.GetComponent<Sprite>().texture = Content.Load<Texture2D>("turrel");
            Jim.GetComponent<Sprite>().Inicial();


        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            camera.UpdateCamera(GraphicsDevice.Viewport);
            inputOffset.X = camera.Transform.Translation.X;
            inputOffset.Y = camera.Transform.Translation.Y;

            Debug.Add(this, inputOffset);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameCenter.Update();
            UIManager.UpdateUI();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.SteelBlue);

            var viewMatrix = camera.Transform;

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: viewMatrix);
            GameCenter.Draw(spriteBatch);
            spriteBatch.End();
            UIManager.DrawUI();

            base.Draw(gameTime);
        }
    }
}
