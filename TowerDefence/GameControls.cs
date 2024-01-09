using Microsoft.Xna.Framework;
using MonoGame.UI.Forms.Effects;
using MonoGame.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class GameControls : ControlManager
    {
        public GameControls(Game game) : base(game)
        {

        }

        //much of this is from the example project
        //https://github.com/csharpskolan/MonoGame.UI.Forms/blob/master/FormsTest/MyControls.cs
        //Vector2.One is new Vector2(1,1), so its top left cornder 20 pixels down right
        public Button EverythingButton = new Button() { Text = "Start", Location = Vector2.One * 16, Size = Vector2.One * 72, BackgroundColor = Color.Gray };


        public Form UpgradeForm = new Form()
        {
            Title = "Upgrades",
            Location = new Vector2(32, 350),
            Size = new Vector2(380, 110),
            IsVisible = false,
        };

        public Button LeftButton = new Button() { Text = "Left", Location = Vector2.One * 28, Size = new Vector2(148, 64), BackgroundColor = Color.White, TextColor = Color.Black };
        public Button RightButton = new Button() { Text = "Right", Location = Vector2.One * 28 + Vector2.UnitX * 178, Size = new Vector2(148, 64), BackgroundColor = Color.White, TextColor = Color.Black };

        public Form BuyTower = new Form()
        {
            Title = "Buildings",
            Location = new Vector2(660, 32),
            Size = new Vector2(130, 420),
            IsVisible = true,
        };

        public TextArea BuildingDescription = new TextArea() { Location = new Vector2(20, 300), Size = new Vector2(340, 300), Text = "" };

        public Label BasicBuildingText = new Label() { Location = new Vector2(35, 80), Text = "  Basic\nCannon", TextColor = Color.Black };
        public Button BuyBasicBuilding = new Button() { Text = "200 Coins", Size = new Vector2(90), Location = new Vector2(20, 60), TextAlign = ContentAlignment.BottomCenter, BackgroundColor = Color.White, TextColor = Color.Black };

        public Label SplashBuildingText = new Label() { Location = new Vector2(35, 80 + 90 + 20), Text = " Splash\nCannon", TextColor = Color.Black };
        public Button BuySplashBuilding = new Button() { Text = "400 Coins", Size = new Vector2(90), Location = new Vector2(20, 60 + 90 + 20), TextAlign = ContentAlignment.BottomCenter, BackgroundColor = Color.White, TextColor = Color.Black };

        public override void InitializeComponent()
        {
            UpgradeForm.Controls.Add(LeftButton);
            UpgradeForm.Controls.Add(RightButton);

            BuyTower.Controls.Add(BuildingDescription);

            BuyTower.Controls.Add(BuyBasicBuilding);
            BuyTower.Controls.Add(BasicBuildingText);

            BuyTower.Controls.Add(BuySplashBuilding);
            BuyTower.Controls.Add(SplashBuildingText);

            Controls.Add(EverythingButton);
            Controls.Add(UpgradeForm);
            Controls.Add(BuyTower);

            BuyBasicBuilding.Clicked += BuyBasicBuilding_Clicked;
            BuySplashBuilding.Clicked += BuySplashBuilding_Clicked;
        }

        private void BuySplashBuilding_Clicked(object sender, EventArgs e)
        {
            BuildingDescription.Text = "Hits multiple, \nenemies at \na time, with\nless damage";
        }

        private void BuyBasicBuilding_Clicked(object sender, EventArgs e)
        {
            BuildingDescription.Text = "Simple and \ncheap, not to \nbe \nunderestimated";
        }
    }
}
