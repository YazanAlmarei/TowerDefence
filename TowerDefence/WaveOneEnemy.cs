using Apos.Shapes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence
{
    internal class WaveOneEnemy : Enemy
    {
        public WaveOneEnemy(InGame game) : base(100, 0.4f, null, game, 40)
        {

        }

        public override void Draw(SpriteBatch sp, ShapeBatch sb)
        {
            sb.DrawCircle(Location, 12, Color.Orange, Color.DarkOrange, 4);
            base.Draw(sp, sb);
        }
    }
}
