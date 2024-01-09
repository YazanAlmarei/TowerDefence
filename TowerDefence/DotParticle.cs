using Apos.Shapes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class DotParticle : Particle
    {
        Color color;
        Vector2 Velocity;
        Vector2 Location;
        public DotParticle(Color color, Vector2 location, Vector2 Velocity) : base(20)
        {
            this.color = color;
            Location = location;
            this.Velocity = Velocity;
        }

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            Velocity += new Vector2(0, 0.1f);//simulate gravty
            Location += Velocity;

            shapeBatch.FillCircle(Location, 2, color);

            //this just decreases the frames left
            base.Draw(spriteBatch, shapeBatch);
        }
    }
}
