using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Apos.Shapes;
using System.Runtime.CompilerServices;

namespace TowerDefence
{
    internal class Enemy
    {
        public int Health;
        public int MaxHealth;
        public Texture2D Texture;
        public InGame InGame;
        public float CurrentIndex;
        public float MovementSpeed;
        public Vector2 Location;
        public int Coins;
        public bool ToBeRemoved = false;

        public Enemy(int health, float movementSpeed, Texture2D texture, InGame inGame, int coins)
        {
            Health = health;
            MaxHealth = health;
            Texture = texture;
            InGame = inGame;
            MovementSpeed = movementSpeed;
            Coins = coins;
        }

        public virtual void Update()
        {
            CurrentIndex += MovementSpeed;
            int LocationIndex = (int)CurrentIndex;
            if (LocationIndex >= InGame.SplineVerts.Length)
            {//it reached the end of the path
                InGame.Health -= 1;
                ToBeRemoved = true;
            }
            else
            {
                Location = InGame.SplineVerts[LocationIndex];
            }

            if(Health <= 0)
            {
                InGame.Coins += Coins;
                ToBeRemoved = true;
                OnDeath();
            }
        }

        Point Size = new Point(48,48);

        public virtual void Draw(SpriteBatch sp, ShapeBatch sb)
        {
            if(Texture != null)
            {
                sp.Draw(Texture, MathFunc.RectangleFromCenterSize(Location.ToPoint(), Size), Color.White);
            }

            //const is like copying and pasting this number in every time its used
            //so const is compile time
            const int BarWidth = 20;

            Vector2 Loc = new(Location.X, Location.Y + 16);//16 is amount moved down
            float left = Loc.X - BarWidth * 0.5f;

            sb.FillLine(
                new Vector2(left, Loc.Y),
                new Vector2(left + (float)Health / MaxHealth * BarWidth, Loc.Y),//right location. Health / MaxHealth is in the range of 0 and 1
                2, Color.OrangeRed);//thickness, color
        }

        public Vector2 GetFutureLocation(float speed, Vector2 yourLocation)
        {
            return InGame.SplineVerts[Math.Min((int)(CurrentIndex + (Vector2.Distance(yourLocation, Location) / speed) * MovementSpeed), InGame.SplineVerts.Length - 1)];
        }

        public virtual void OnDeath()
        {

        }
    }
}
