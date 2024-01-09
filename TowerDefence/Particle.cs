using Apos.Shapes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal abstract class Particle
    {
        public int FramesLeft;

        public Particle(int framesLeft)
        {
            FramesLeft = framesLeft;
        }

        public virtual void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            FramesLeft--;
        }
    }
}
