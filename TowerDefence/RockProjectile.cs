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
    internal class RockProjectile : Projectile
    {
        public RockProjectile(Enemy enemy, Vector2 Location, float damage) : base(Location, Vector2.Normalize(enemy.GetFutureLocation(8, Location) - Location) * 8,enemy, damage)
        {

        }

        public override void Draw(SpriteBatch sb, ShapeBatch sp)
        {
            sp.DrawCircle(Location, 4, Color.DarkGray, Color.Gray);
        }
    }
}
