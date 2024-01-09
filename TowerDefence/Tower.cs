using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.UI.Forms;

namespace TowerDefence
{
    //
    internal abstract class Tower
    {
        Texture2D StillTexture;
        Texture2D RotateTexture;
        public Vector2 Location;
        public float range;

        int framesSinceLastFire = 0;
        public int reloadTime;
        float rotation;//rotation of this thing in radians

        public int Path1Level = 1;
        public int Path2Level = 1;


        public Tower(Texture2D stillTexture, Texture2D rotateTexture, Vector2 Location, float range, int reloadtime)
        {
            StillTexture = stillTexture;
            this.RotateTexture = rotateTexture;
            this.Location = Location;
            this.range = range;
            reloadTime = reloadtime;
        }

        public void Update(List<Enemy> enemies, List<Projectile> projectiles)
        {
            if(framesSinceLastFire >= reloadTime)
            {//we can fire again
                framesSinceLastFire = 0;

                for(int i = 0; i < enemies.Count; i++)
                {
                    Enemy enemy = enemies[i];
                    if (Vector2.Distance(enemy.Location, Location) < range)
                    {//I can shoot at this enemy!

                        //point towards it
                        rotation = MathFunc.VectorPointAngle(Location, enemy.Location);

                        //we give it a refrence to the list of projectiles so the building can add to it
                        FireProjectile(projectiles, enemy);
                        break; //leave the foreach loop so we only shoot once
                    }
                }
            }
            framesSinceLastFire++;
        }
        
        public void Draw(SpriteBatch sb, ShapeBatch sp)
        {
            if(StillTexture != null)
                sb.Draw(StillTexture, MathFunc.RectangleFromCenterSize(Location.ToPoint(), new Point(64)), Color.White);

            //null for source rectangle as we are not using it, nosprite effects, and layer depth can really be any number since we are not using it
            if(RotateTexture != null)//origin is 190,190 or the center of the png which is 380x380
                sb.Draw(RotateTexture, new Rectangle(Location.ToPoint(), new Point(64)), null, Color.White, rotation, Vector2.One * 190, SpriteEffects.None, 0);
        }

        //we leave it up to the classes that derive from this to implement
        protected abstract void FireProjectile(List<Projectile> projectiles, Enemy enemy);

        public abstract void UpgradePath1(object sender, EventArgs e);

        public abstract void UpgradePath2(object sender, EventArgs e);

        public abstract void GuiOpened();

        public void UpdateButton(UpgradeInfo[] table, Button button, int currentLevel, string statDescriptor)
        {
            if (currentLevel == table.Length)
            {
                button.Text = "MAX LEVEL";
            }
            else
            {
                button.Text = $"{table[currentLevel].FlavorText}\n+{table[currentLevel].UpgradeAmount} {statDescriptor}\n{table[currentLevel].CoinCost} Coins";
            }
        }
    }
}
