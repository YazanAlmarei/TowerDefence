using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;

namespace PathGenerator
{
    public class Game1 : Game
    {
        
        private GraphicsDeviceManager _graphics;
        private ShapeBatch sb;

        List<Vector2> verts = new List<Vector2>();

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            verts.Add(new Vector2(0, 200));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new ShapeBatch(GraphicsDevice, Content);

            // TODO: use this.Content to load your game content here
        }
        int ticks = 0;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ticks++;

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Vector2 mouseLocation = Mouse.GetState().Position.ToVector2();
                Vector2 prevVert = verts[verts.Count - 1];
                verts.Add(prevVert + Vector2.Normalize(mouseLocation - prevVert) * 2);
            }

            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                string json = JsonConvert.SerializeObject(verts);
                System.Diagnostics.Debug.WriteLine(json);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            sb.Begin();
            for (int i = 1; i < verts.Count; i++)
            {
                sb.FillLine(verts[i], verts[i - 1], 16, Color.Tan);
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
