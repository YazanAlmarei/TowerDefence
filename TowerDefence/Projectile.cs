using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal abstract class Projectile
    {
        public Vector2 Location;
        public Vector2 Velocity;
        public int TicksLeft;
        Enemy enemy;
        public float damage;
        public Projectile(Vector2 Location, Vector2 Velocity, Enemy enemy, float damage)
        {
            this.enemy = enemy;
            this.damage = damage;
            this.Location = Location;
            this.Velocity = Velocity;

            //get the float of the distance from projectile to the enemy
            //get how fast one is
            //see how long it will take to get there
            TicksLeft = (int)(Vector2.Distance(Location, enemy.Location) / Velocity.Length());
        }

        public void Update()
        {
            TicksLeft--;
            Location += Velocity;

            if(TicksLeft == 0)
            {//we have arrived at the enemy
                if(!enemy.ToBeRemoved)//double check it has not already been killed
                {
                    enemy.Health -= (int)damage;
                    OnArrival();
                    GenerateParticles();
                }
            }
        }

        public virtual void OnArrival()
        {

        }

        public virtual void GenerateParticles()
        {
            for (int i = 0; i < 8; i++)
            {
                //next single is a float between 0 and 1
                //-0.5 makes it between -0.5, 0.5, then finnally to -4, 4
                Vector2 vel = new Vector2(Random.Shared.NextSingle() - 0.5f, Random.Shared.NextSingle() - 0.5f) * 8;
                ParticleSystem.Instance.particles.Add(new DotParticle(Color.DarkGray, Location, vel));
            }
        }

        public abstract void Draw(SpriteBatch sb, ShapeBatch sp);
    }
}
