using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Apos.Shapes;

namespace TowerDefence
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatch ShapeBatch;

        private InGame gameWorld;

        RenderTarget2D GameMap;//a render target is a texture2D you can draw on

        //this is static, meaning you can get this variable ANYWHERE! > Game1.WindowSize
        public static Point WindowSize;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //there are two graphics profiles
            //they define what can and cannot be done by the game
            //reach supports more platforms, but HiDef is better
            //we need HiDef for Apos.Shapes;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        GameControls gui;

        protected override void Initialize()
        {
            //get the window size
            WindowSize = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            GameMap = new RenderTarget2D(_graphics.GraphicsDevice, WindowSize.X, WindowSize.Y);


            //this is also from example project
            //https://github.com/csharpskolan/MonoGame.UI.Forms/blob/master/FormsTest/Game1.cs
            gui = new GameControls(this);

            this.Components.Add(gui);

            gameWorld = new InGame(Content, gui);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ShapeBatch = new ShapeBatch(GraphicsDevice, Content);

            Content.Load<Texture2D>("basicBase");
            Content.Load<Texture2D>("basicTop");
            Content.Load<Texture2D>("basicWhole");
            Content.Load<Texture2D>("bomb");
            Content.Load<Texture2D>("coin");
            Content.Load<Texture2D>("heart");
            Content.Load<Texture2D>("level2enemy");
            Content.Load<Texture2D>("level3enemy");
            Content.Load<Texture2D>("SplashAll");
            Content.Load<Texture2D>("SplashTop");

            Content.Load<SpriteFont>("defaultFont");
            Content.Load<SpriteFont>("largeFont");
        }

        bool GameInLimbo = false;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            GameInLimbo = gameWorld.Update(GameMap);

            if(GameInLimbo && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                gameWorld = new InGame(Content, gui);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(GameMap);
            GraphicsDevice.Clear(Color.White);

            Start();

            gameWorld.DrawValidPlacement(_spriteBatch, ShapeBatch);

            Stop();

            GraphicsDevice.SetRenderTarget(null);
            Start();
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            gameWorld.Draw(_spriteBatch, ShapeBatch);
            ParticleSystem.Instance.Draw(_spriteBatch, ShapeBatch);

            if(GameInLimbo)
                _spriteBatch.DrawString(Content.LoadLocalized<SpriteFont>("largeFont"), "   You Died.\nPress R to retry", new Vector2(200, 200), Color.White);

            Stop();

            base.Draw(gameTime);

        }

        private void Start()
        {
            _spriteBatch.Begin();
            ShapeBatch.Begin();
        }

        private void Stop()
        {
            ShapeBatch.End();
            _spriteBatch.End();
        }
    }
}
