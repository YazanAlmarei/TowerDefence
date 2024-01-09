using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefence
{
    internal class BasicTower : Tower
    {
        InGame game;

        //these are tuples
        //the int represents coin cost
        //the float represents how much is added to range
        //the string is flavor text
        private static readonly UpgradeInfo[] DamageCostTable = new UpgradeInfo[] { new(20, 80, "Better gunpowder"), new(80, 40, "Better cannon balls"), new(100, 60, "Spiked Balls") };

        //readonly is so it cannot be edited
        private static readonly UpgradeInfo[] RangeCostTable = new UpgradeInfo[] { new(40, 20, "Better glasses"), new(80, 20, "Telescope!"), new(120, 40, "Taller Tower"), new(200, 400, "Satellite System") };

        //PATH 1: Range
        //PATH 2: Damage
        public BasicTower(Vector2 Location, ContentManager content, InGame game) : base(content.Load<Texture2D>("basicBase"), content.Load<Texture2D>("basicTop"), Location, 100, 80)//100 pixels range, 80 frames or 1.333 seconds per fire
        {
            this.game = game;
        }

        float damage = 35;

        public override void GuiOpened()
        {
            UpdateButton(RangeCostTable, game.GameControls.LeftButton, Path1Level, "Range");
            UpdateButton(DamageCostTable, game.GameControls.RightButton, Path2Level, "Damage");
        }

        public override void UpgradePath2(object sender, EventArgs e)
        {
            //check if they have the coins and not max level
            if(Path2Level < DamageCostTable.Length && game.Coins - DamageCostTable[Path2Level].CoinCost >= 0)
            {
                game.Coins -= DamageCostTable[Path2Level].CoinCost;
                damage += DamageCostTable[Path2Level].UpgradeAmount;

                Path2Level++;

                GuiOpened();
            }
        }

        public override void UpgradePath1(object sender, EventArgs e)
        {
            //check if they have the coins and not max level
            if (Path1Level < RangeCostTable.Length && game.Coins - RangeCostTable[Path1Level].CoinCost >= 0)
            {
                game.Coins -= RangeCostTable[Path1Level].CoinCost;
                range += RangeCostTable[Path1Level].UpgradeAmount;

                Path1Level++;

                GuiOpened();
            }
        }

        protected override void FireProjectile(List<Projectile> projectiles, Enemy enemy)
        {
            RockProjectile rockProjectile = new RockProjectile(enemy, Location, damage);
            rockProjectile.damage = (int)(rockProjectile.damage * Path2Level);
            projectiles.Add(rockProjectile);
        }
    }
}
