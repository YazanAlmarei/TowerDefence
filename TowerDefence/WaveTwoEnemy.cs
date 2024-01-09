using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class WaveTwoEnemy : Enemy
    {
        public WaveTwoEnemy(ContentManager content, InGame inGame) : base(180, 1f, content.Load<Texture2D>("level2enemy"), inGame, 120)
        {

        }

        public override void OnDeath()
        {
            Enemy enemy = new WaveOneEnemy(InGame);
            enemy.CurrentIndex = CurrentIndex;
            InGame.enemies.Add(enemy);
        }
    }
}
