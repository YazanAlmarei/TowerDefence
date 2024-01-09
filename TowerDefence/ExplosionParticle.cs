using Apos.Shapes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class ExplosionParticle : Particle
    {
        Vector2 Location;
        Vector2 Velocity;
        float rotation;
        public ExplosionParticle(Vector2 location, Vector2 velocity) : base(20)
        {
            Location = location;
            Velocity = velocity;
        }

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            Location += Velocity;
            rotation += 0.2f;
            shapeBatch.DrawEquilateralTriangle(Location, 4, Color.OrangeRed * 0.4f, Color.Red * 0.4f, 2, rotation: rotation);

            base.Draw(spriteBatch, shapeBatch);
        }
    }
}
