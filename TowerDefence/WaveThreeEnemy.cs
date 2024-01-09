using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class WaveThreeEnemy : Enemy
    {
        private ContentManager contentManager;
        public WaveThreeEnemy(ContentManager content, InGame inGame) : base(250, 2f, content.Load<Texture2D>("level3enemy"), inGame, 150)
        {
            contentManager = content;
        }

        public override void OnDeath()
        {
            Enemy enemy = new WaveTwoEnemy(contentManager, InGame);
            enemy.CurrentIndex = CurrentIndex;
            InGame.enemies.Add(enemy);

            Enemy enemy2 = new WaveTwoEnemy(contentManager, InGame);
            enemy2.CurrentIndex = CurrentIndex - 50;
            if(enemy2.CurrentIndex < 0)
            {
                enemy2.CurrentIndex = 0;
            }
            InGame.enemies.Add(enemy2);
        }
    }
}
