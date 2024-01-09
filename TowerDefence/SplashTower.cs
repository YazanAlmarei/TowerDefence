using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.UI.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefence
{
    internal class SplashTower : Tower
    {
        //Path 1
        private static readonly UpgradeInfo[] ReloadSpeedTable = new UpgradeInfo[] { new(10, 10, "Trained Soldiers"), new(30, 10, "Magazines"), new(50, 10, "Robotic Replacement") };
        //Path 2
        private static readonly UpgradeInfo[] SplashRadiusTable = new UpgradeInfo[] { new(60, 30, "Tactical Bombs"), new(120, 30, "Bigger Bombs"), new(180, 30, "Nuclear Arsenal") };

        InGame game;
        int splashRadius = 70;
        ContentManager content;
        public SplashTower(ContentManager content, Vector2 Loc, InGame game) : base(content.Load<Texture2D>("basicBase"), content.Load<Texture2D>("SplashTop"), Loc, 200, 60)
        {
            this.game = game;
            this.content = content;
        }

        public override void GuiOpened()
        {
            UpdateButton(ReloadSpeedTable, game.GameControls.LeftButton, Path1Level, "Reload Speed");
            UpdateButton(SplashRadiusTable, game.GameControls.RightButton, Path2Level, "Splash Radius");
        }

        public override void UpgradePath2(object sender, EventArgs e)
        {
            //splash radius
            if (Path2Level < SplashRadiusTable.Length && game.Coins - SplashRadiusTable[Path2Level].CoinCost >= 0)
            {
                game.Coins -= SplashRadiusTable[Path2Level].CoinCost;
                splashRadius += SplashRadiusTable[Path2Level].UpgradeAmount;

                Path2Level++;
                GuiOpened();
            }
        }

        public override void UpgradePath1(object sender, EventArgs e)
        {
            //reload speed
            if (Path1Level < ReloadSpeedTable.Length && game.Coins - ReloadSpeedTable[Path1Level].CoinCost >= 0)
            {
                game.Coins -= ReloadSpeedTable[Path1Level].CoinCost;
                reloadTime -= ReloadSpeedTable[Path1Level].UpgradeAmount;//subtracting here because decreasing timer

                Path1Level++;
                GuiOpened();
            }
        }

        protected override void FireProjectile(List<Projectile> projectiles, Enemy enemy)
        {
            projectiles.Add(new BombProjectile(Location, enemy, content, game, splashRadius));
        }
    }
}
