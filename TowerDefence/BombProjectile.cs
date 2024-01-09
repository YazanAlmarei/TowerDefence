using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class BombProjectile : Projectile
    {
        Texture2D bombTexture;
        InGame game;
        int range;
        public BombProjectile(Vector2 Location, Enemy enemy, ContentManager content, InGame game, int range) : base(Location, Vector2.Normalize(enemy.GetFutureLocation(4, Location) - Location) * 4, enemy, 0)
        {
            bombTexture = content.LoadLocalized<Texture2D>("bomb");
            this.game = game;
            this.range = range;
        }

        public override void Draw(SpriteBatch sb, ShapeBatch sp)
        {
            sb.Draw(bombTexture, MathFunc.RectangleFromCenterSize(Location.ToPoint(), new Point(32,32)), Color.White);
        }

        public override void OnArrival()
        {
            foreach(Enemy enemy in game.enemies)
            {
                if(Vector2.Distance(enemy.Location, Location) < range)
                {
                    enemy.Health -= 30;
                }
            }
        }

        public override void GenerateParticles()
        {
            for (int i = 0; i < 12; i++)
            {
                Vector2 vel = new Vector2(Random.Shared.NextSingle() - 0.5f, Random.Shared.NextSingle() - 0.5f) * 8;
                ParticleSystem.Instance.particles.Add(new ExplosionParticle(Location, vel));
            }
        }
    }
}
