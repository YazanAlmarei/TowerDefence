using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class UpgradeInfo
    {
        public int CoinCost;
        public int UpgradeAmount;
        public string FlavorText;

        public UpgradeInfo(int coinCost, int upgradeAmount, string flavorText)
        {
            CoinCost = coinCost;
            UpgradeAmount = upgradeAmount;
            FlavorText = flavorText;
        }
    }
}
