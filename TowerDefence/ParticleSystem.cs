using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class ParticleSystem
    {
        private static ParticleSystem instance;
        public List<Particle> particles = new List<Particle>();

        private ParticleSystem() { }

        //this is a singleton
        //more info here https://csharpindepth.com/articles/singleton
        public static ParticleSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ParticleSystem();
                }
                return instance;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            for(int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Draw(spriteBatch, shapeBatch);

                if(particles[i].FramesLeft <= 0)
                {
                    particles.RemoveAt(i);
                }
            }
        }
    }
}
