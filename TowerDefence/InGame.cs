﻿using Apos.Shapes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoGame.UI.Forms;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace TowerDefence
{
    internal class InGame
    {
        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;

        MouseState mouseState;
        MouseState prevMouseState;

        int CurrentWave = 1;
        int enemiesSpawned = 0;
        int TimeSinceStart = 0;

        GamePart gamePart = GamePart.SpawnWave;

        public Vector2[] SplineVerts;

        public List<Enemy> enemies = new List<Enemy>();
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Tower> towers = new List<Tower>();

        bool paused = true;

        public int Coins = 400;
        public int Health = 20;

        ContentManager contentManager;

        Tower SelectedTower = null;
        TowerBuy TowerToBeBought = TowerBuy.None;
        bool IsValidPlacement = false;

        public GameControls GameControls;

        const string Map = "[\"0, 200\",\"1.9778726, 200.29668\",\"3.9648418, 200.52461\",\"5.951811, 200.75255\",\"7.9387803, 200.98048\",\"9.92575, 201.20842\",\"11.912719, 201.43636\",\"13.902246, 201.64076\",\"15.897695, 201.77562\",\"17.895765, 201.86345\",\"19.895426, 201.8266\",\"21.892178, 201.71266\",\"23.886606, 201.56348\",\"25.877892, 201.37698\",\"27.867645, 201.1748\",\"29.853308, 200.93576\",\"31.837149, 200.68204\",\"33.815582, 200.38911\",\"35.79146, 200.07942\",\"37.764645, 199.75304\",\"39.731457, 199.39021\",\"41.680485, 198.94157\",\"43.624958, 198.47357\",\"45.56092, 197.97153\",\"47.441116, 197.28972\",\"49.321312, 196.60791\",\"51.12263, 195.73886\",\"52.92395, 194.86981\",\"54.72527, 194.00076\",\"56.503826, 193.08603\",\"58.23626, 192.08669\",\"59.957397, 191.06802\",\"61.631943, 189.97446\",\"63.306488, 188.88089\",\"64.931984, 187.71565\",\"66.55748, 186.55042\",\"68.157745, 185.35077\",\"69.7306, 184.1154\",\"71.2949, 182.86922\",\"72.84887, 181.61018\",\"74.38545, 180.32999\",\"75.92203, 179.04977\",\"77.44963, 177.75888\",\"78.97584, 176.46634\",\"80.483765, 175.15251\",\"81.972275, 173.81673\",\"83.450745, 172.46983\",\"84.910805, 171.103\",\"86.34289, 169.70688\",\"87.756645, 168.2922\",\"89.16129, 166.86848\",\"90.538445, 165.41815\",\"91.89798, 163.9513\",\"93.2403, 162.46867\",\"94.55579, 160.96219\",\"95.844635, 159.43285\",\"97.124596, 157.89606\",\"98.3887, 156.3462\",\"99.65281, 154.79636\",\"100.910034, 153.24092\",\"102.15827, 151.67827\",\"103.39745, 150.10841\",\"104.62616, 148.53035\",\"105.854866, 146.95229\",\"107.08357, 145.37422\",\"108.30845, 143.79318\",\"109.52704, 142.20729\",\"110.7483, 140.62346\",\"111.96332, 139.03484\",\"113.17221, 137.44154\",\"114.38392, 135.85039\",\"115.592354, 134.25674\",\"116.80359, 132.66524\",\"118.0176, 131.07584\",\"119.27466, 129.52026\",\"120.531715, 127.96469\",\"121.78877, 126.40912\",\"123.056885, 124.86254\",\"124.32698, 123.3176\",\"125.60818, 121.781845\",\"126.891205, 120.24762\",\"128.19781, 118.73342\",\"129.50443, 117.21922\",\"130.82335, 115.71575\",\"132.16373, 114.23138\",\"133.5156, 112.757454\",\"134.87865, 111.29386\",\"136.26253, 109.849945\",\"137.64641, 108.40603\",\"139.04071, 106.972176\",\"140.4548, 105.55784\",\"141.87854, 104.15321\",\"143.31166, 102.758156\",\"144.75388, 101.37251\",\"146.20494, 99.99614\",\"147.67424, 98.63925\",\"149.14279, 97.281555\",\"150.6204, 95.93373\",\"152.10707, 94.59589\",\"153.60165, 93.266884\",\"155.10391, 91.94657\",\"156.62349, 90.646225\",\"158.1516, 89.35593\",\"159.69653, 88.085815\",\"161.24788, 86.82354\",\"162.80544, 85.56894\",\"164.3789, 84.33435\",\"165.96555, 83.116745\",\"167.55219, 81.89914\",\"169.15404, 80.701614\",\"170.76337, 79.51415\",\"172.3727, 78.32669\",\"174.03151, 77.209404\",\"175.69032, 76.09212\",\"177.34914, 74.97483\",\"179.01755, 73.87193\",\"180.68596, 72.769035\",\"182.39032, 71.72255\",\"184.10744, 70.697105\",\"185.82455, 69.67166\",\"187.54703, 68.65524\",\"189.28, 67.65684\",\"191.0419, 66.71042\",\"192.8328, 65.82009\",\"194.64175, 64.96706\",\"196.46451, 64.1439\",\"198.29045, 63.327843\",\"200.13528, 62.555447\",\"202.01244, 61.86528\",\"203.90698, 61.224445\",\"205.8165, 60.629696\",\"207.73227, 60.055397\",\"209.65541, 59.50629\",\"211.59404, 59.014633\",\"213.53267, 58.522976\",\"215.4713, 58.03132\",\"217.40993, 57.53966\",\"219.34856, 57.048004\",\"221.28719, 56.556347\",\"223.22581, 56.06469\",\"225.16444, 55.573032\",\"227.14394, 55.287354\",\"229.12343, 55.001675\",\"231.10292, 54.715996\",\"233.0878, 54.4705\",\"235.0791, 54.284195\",\"237.07056, 54.099552\",\"239.06516, 53.95259\",\"241.0611, 53.825203\",\"243.05914, 53.736706\",\"245.05792, 53.666866\",\"247.05768, 53.635696\",\"249.05765, 53.623695\",\"251.05748, 53.64992\",\"253.05489, 53.75175\",\"255.04861, 53.909958\",\"257.0371, 54.12416\",\"259.02368, 54.35554\",\"261.00568, 54.62336\",\"262.98196, 54.930336\",\"264.9523, 55.273468\",\"266.91223, 55.671852\",\"268.85928, 56.129047\",\"270.80276, 56.601204\",\"272.7362, 57.112865\",\"274.6587, 57.664227\",\"276.57535, 58.235603\",\"278.48154, 58.84097\",\"280.3768, 59.47972\",\"282.2652, 60.138477\",\"284.13907, 60.837467\",\"286.00055, 61.56883\",\"287.8459, 62.339985\",\"289.68637, 63.12265\",\"291.51822, 63.925335\",\"293.3271, 64.77854\",\"295.117, 65.67082\",\"296.89267, 66.59113\",\"298.65393, 67.53872\",\"300.4106, 68.49477\",\"302.14685, 69.4875\",\"303.8786, 70.48801\",\"305.5953, 71.514145\",\"307.30768, 72.54748\",\"309.00482, 73.60564\",\"310.69073, 74.68159\",\"312.36856, 75.770134\",\"314.03104, 76.88195\",\"315.68973, 77.999435\",\"317.34464, 79.12251\",\"318.99576, 80.251114\",\"320.62787, 81.40708\",\"322.24475, 82.58423\",\"323.8553, 83.770065\",\"325.4713, 84.9484\",\"327.0754, 86.14291\",\"328.66443, 87.35739\",\"330.2506, 88.5756\",\"331.8312, 89.80103\",\"333.4091, 91.02997\",\"334.98697, 92.2589\",\"336.57462, 93.47518\",\"338.14398, 94.71496\",\"339.6982, 95.97367\",\"341.24774, 97.23818\",\"342.79495, 98.5055\",\"344.3399, 99.7756\",\"345.86993, 101.06363\",\"347.396, 102.35636\",\"348.89502, 103.680336\",\"350.3898, 105.00912\",\"351.871, 106.353\",\"353.36206, 107.68594\",\"354.83966, 109.033806\",\"356.31512, 110.38398\",\"357.78857, 111.73636\",\"359.2719, 113.07789\",\"360.75412, 114.42068\",\"362.2352, 115.76472\",\"363.73804, 117.08436\",\"365.2702, 118.36984\",\"366.81027, 119.645836\",\"368.36966, 120.89818\",\"369.95667, 122.11532\",\"371.56964, 123.29784\",\"373.207, 124.44636\",\"374.8522, 125.58361\",\"376.52725, 126.67641\",\"378.20938, 127.758255\",\"379.9226, 128.79018\",\"381.6603, 129.78033\",\"383.42975, 130.71257\",\"385.21857, 131.60704\",\"387.02585, 132.46365\",\"388.8545, 133.2736\",\"390.71408, 134.00975\",\"392.59787, 134.6816\",\"394.50305, 135.29008\",\"396.44153, 135.78232\",\"398.39398, 136.2158\",\"400.3531, 136.6181\",\"402.31903, 136.98555\",\"404.28967, 137.3269\",\"406.2691, 137.61292\",\"408.25555, 137.84546\",\"410.24716, 138.02846\",\"412.24274, 138.16122\",\"414.24094, 138.24574\",\"416.24057, 138.28397\",\"418.24057, 138.27785\",\"420.2404, 138.25\",\"422.2392, 138.18144\",\"424.2362, 138.07225\",\"426.23212, 137.94435\",\"428.22678, 137.79819\",\"430.2199, 137.63245\",\"432.2113, 137.44716\",\"434.20123, 137.24672\",\"436.187, 137.00862\",\"438.17117, 136.75732\",\"440.15088, 136.47311\",\"442.12845, 136.1745\",\"444.10168, 135.8484\",\"446.076, 135.5288\",\"448.0513, 135.21555\",\"450.0271, 134.90546\",\"452.00388, 134.60161\",\"453.98248, 134.30977\",\"455.96533, 134.04851\",\"457.95178, 133.81622\",\"459.94476, 133.64896\",\"461.94086, 133.52382\",\"463.93912, 133.44017\",\"465.93893, 133.41376\",\"467.9389, 133.42441\",\"469.9387, 133.45277\",\"471.93814, 133.49905\",\"473.93646, 133.58078\",\"475.93307, 133.69724\",\"477.9273, 133.84904\",\"479.9187, 134.03438\",\"481.90472, 134.27045\",\"483.88675, 134.53806\",\"485.8551, 134.8924\",\"487.8072, 135.32756\",\"489.7347, 135.86113\",\"491.61914, 136.53114\",\"493.43857, 137.3616\",\"495.215, 138.28049\",\"496.95358, 139.26907\",\"498.64633, 140.33426\",\"500.2876, 141.47714\",\"501.89142, 142.67203\",\"503.47247, 143.89687\",\"505.03052, 145.15085\",\"506.56537, 146.43314\",\"508.0846, 147.73389\",\"509.58023, 149.0617\",\"511.05215, 150.41576\",\"512.4847, 151.81143\",\"513.88586, 153.23854\",\"515.256, 154.6955\",\"516.58777, 156.18758\",\"517.88934, 157.70609\",\"519.16113, 159.24962\",\"520.4109, 160.81108\",\"521.646, 162.38416\",\"522.86646, 163.9686\",\"524.0723, 165.56418\",\"525.2493, 167.18118\",\"526.40497, 168.81349\",\"527.50903, 170.48116\",\"528.5863, 172.16623\",\"529.6439, 173.86372\",\"530.6546, 175.58955\",\"531.63434, 177.33315\",\"532.56256, 179.10472\",\"533.45685, 180.89363\",\"534.32336, 182.69618\",\"535.1457, 184.5193\",\"535.93604, 186.3565\",\"536.7, 188.20485\",\"537.41425, 190.07297\",\"538.0823, 191.9581\",\"538.7176, 193.8545\",\"539.3202, 195.76157\",\"539.8863, 197.67978\",\"540.43604, 199.60275\",\"540.95294, 201.53479\",\"541.44037, 203.47449\",\"541.9081, 205.41904\",\"542.3593, 207.36748\",\"542.77747, 209.32327\",\"543.1791, 211.28253\",\"543.5338, 213.25082\",\"543.8117, 215.23143\",\"544.05756, 217.21626\",\"544.25604, 219.20639\",\"544.4247, 221.19926\",\"544.5633, 223.19446\",\"544.6719, 225.19151\",\"544.7503, 227.18997\",\"544.81287, 229.189\",\"544.8305, 231.18892\",\"544.7882, 233.18848\",\"544.7167, 235.1872\",\"544.5857, 237.1829\",\"544.3526, 239.16928\",\"544.0474, 241.14586\",\"543.67145, 243.1102\",\"543.2118, 245.05667\",\"542.6951, 246.98878\",\"542.0985, 248.89772\",\"541.4118, 250.77614\",\"540.6649, 252.63144\",\"539.83014, 254.4489\",\"538.9409, 256.24036\",\"538.00507, 258.00787\",\"537.0127, 259.7443\",\"535.966, 261.44855\",\"534.8775, 263.12637\",\"533.7584, 264.784\",\"532.59973, 266.41418\",\"531.4029, 268.01654\",\"530.16925, 269.59076\",\"528.90027, 271.13663\",\"527.5892, 272.64694\",\"526.246, 274.12878\",\"524.865, 275.57547\",\"523.4406, 276.97946\",\"521.9819, 278.3477\",\"520.47186, 279.65915\",\"518.9373, 280.94183\",\"517.3736, 282.18875\",\"515.8099, 283.43567\",\"514.22845, 284.66003\",\"512.61456, 285.8413\",\"510.9575, 286.96115\",\"509.27466, 288.0419\",\"507.56577, 289.081\",\"505.8243, 290.06454\",\"504.05252, 290.99234\",\"502.2608, 291.881\",\"500.44244, 292.71384\",\"498.60178, 293.4961\",\"496.7367, 294.21823\",\"494.84717, 294.87372\",\"492.93985, 295.47552\",\"491.0203, 296.03708\",\"489.08307, 296.53427\",\"487.13358, 296.98087\",\"485.17343, 297.37808\",\"483.20193, 297.71445\",\"481.22495, 298.0169\",\"479.24344, 298.28827\",\"477.2548, 298.50098\",\"475.26334, 298.68576\",\"473.27057, 298.85556\",\"471.27658, 299.01053\",\"469.28064, 299.13812\",\"467.28384, 299.25134\",\"465.28568, 299.33707\",\"463.28622, 299.38333\",\"461.28632, 299.40375\",\"459.28632, 299.39865\",\"457.28677, 299.356\",\"455.28833, 299.27673\",\"453.2931, 299.13895\",\"451.30164, 298.9543\",\"449.31232, 298.74777\",\"447.32538, 298.51968\",\"445.3439, 298.24814\",\"443.36655, 297.948\",\"441.3958, 297.60715\",\"439.4264, 297.25882\",\"437.46036, 296.89175\",\"435.49744, 296.50848\",\"433.53812, 296.1072\",\"431.58014, 295.69934\",\"429.62592, 295.27393\",\"427.67307, 294.84225\",\"425.72406, 294.39343\",\"423.7784, 293.9304\",\"421.84076, 293.43484\",\"419.90576, 292.9291\",\"417.9761, 292.40332\",\"416.04697, 291.8757\",\"414.1211, 291.3362\",\"412.19647, 290.79233\",\"410.27307, 290.24408\",\"408.3509, 289.69156\",\"406.43198, 289.1278\",\"404.51138, 288.56985\",\"402.59198, 288.00775\",\"400.6738, 287.4416\",\"398.75595, 286.8743\",\"396.8384, 286.3059\",\"394.9212, 285.73645\",\"393.00064, 285.17834\",\"391.08322, 284.60953\",\"389.1661, 284.03967\",\"387.24655, 283.47818\",\"385.32733, 282.91556\",\"383.40762, 282.35452\",\"381.4856, 281.80157\",\"379.56204, 281.25394\",\"377.63626, 280.71414\",\"375.70905, 280.17953\",\"373.77835, 279.6576\",\"371.8481, 279.1339\",\"369.9159, 278.6176\",\"367.98175, 278.10855\",\"366.04517, 277.60895\",\"364.1074, 277.11398\",\"362.16507, 276.6371\",\"360.21802, 276.17996\",\"358.267, 275.74\",\"356.3108, 275.32376\",\"354.35193, 274.92023\",\"352.3905, 274.52933\",\"350.42627, 274.15274\",\"348.4594, 273.79022\",\"346.4906, 273.4384\",\"344.51785, 273.10938\",\"342.5429, 272.79388\",\"340.56607, 272.4903\",\"338.5888, 272.18948\",\"336.6085, 271.90958\",\"334.628, 271.63095\",\"332.64606, 271.36282\",\"330.66272, 271.1052\",\"328.6779, 270.85922\",\"326.69168, 270.62485\",\"324.70413, 270.40198\",\"322.71448, 270.1989\",\"320.7237, 270.00717\",\"318.73102, 269.8361\",\"316.7367, 269.68552\",\"314.74164, 269.54517\",\"312.74585, 269.41565\",\"310.74945, 269.29575\",\"308.75247, 269.1861\",\"306.75497, 269.08624\",\"304.75656, 269.00653\",\"302.75745, 268.94702\",\"300.75766, 268.91766\",\"298.7577, 268.90848\",\"296.7578, 268.92953\",\"294.75848, 268.9808\",\"292.76013, 269.06226\",\"290.76328, 269.1744\",\"288.77, 269.33804\",\"286.77948, 269.53278\",\"284.7923, 269.75876\",\"282.80893, 270.01614\",\"280.8284, 270.29462\",\"278.85098, 270.59427\",\"276.87897, 270.92767\",\"274.91266, 271.29327\",\"272.95132, 271.6847\",\"270.99683, 272.10886\",\"269.04776, 272.5574\",\"267.10522, 273.0334\",\"265.17276, 273.54877\",\"263.25476, 274.1156\",\"261.34467, 274.70853\",\"259.44672, 275.3392\",\"257.5647, 276.01596\",\"255.69267, 276.71988\",\"253.833, 277.4558\",\"251.99846, 278.25235\",\"250.1919, 279.11044\",\"248.40755, 280.01382\",\"246.64426, 280.95767\",\"244.90027, 281.9367\",\"243.18344, 282.96262\",\"241.52838, 284.08545\",\"239.91905, 285.27292\",\"238.33698, 286.49643\",\"236.78358, 287.75616\",\"235.26035, 289.05222\",\"233.8095, 290.4288\",\"232.40193, 291.84964\",\"231.02112, 293.29648\",\"229.66843, 294.76965\",\"228.31573, 296.24283\",\"226.9733, 297.72534\",\"225.63086, 299.20786\",\"224.31866, 300.7172\",\"223.02763, 302.2447\",\"221.74849, 303.78214\",\"220.49187, 305.33807\",\"219.27165, 306.9227\",\"218.11429, 308.5538\",\"216.99744, 310.21292\",\"215.93942, 311.91016\",\"214.92635, 313.63458\",\"214.02718, 315.42105\",\"213.28873, 317.27972\",\"212.64757, 319.17416\",\"212.11557, 321.1021\",\"211.6147, 323.0384\",\"211.19615, 324.9941\",\"210.92319, 326.9754\",\"210.70746, 328.96375\",\"210.57881, 330.9596\",\"210.59035, 332.95956\",\"210.68361, 334.9574\",\"210.85829, 336.94977\",\"211.11003, 338.93387\",\"211.41719, 340.91013\",\"211.77058, 342.87866\",\"212.22156, 344.82715\",\"212.76413, 346.75214\",\"213.3922, 348.65097\",\"214.11394, 350.5162\",\"214.91666, 352.34805\",\"215.80615, 354.13937\",\"216.7159, 355.92047\",\"217.67474, 357.67563\",\"218.68036, 359.40442\",\"219.69383, 361.12863\",\"220.7336, 362.8371\",\"221.79884, 364.52982\",\"222.89484, 366.2028\",\"224.03094, 367.8488\",\"225.16705, 369.49478\",\"226.3465, 371.11\",\"227.53032, 372.722\",\"228.75012, 374.30695\",\"229.97353, 375.88913\",\"231.24821, 377.43027\",\"232.55342, 378.94568\",\"233.90149, 380.4231\",\"235.26402, 381.88718\",\"236.67406, 383.30554\",\"238.11536, 384.69214\",\"239.57529, 386.0591\",\"241.05415, 387.40558\",\"242.5321, 388.75305\",\"244.01945, 390.09012\",\"245.5068, 391.4272\",\"246.99416, 392.76425\",\"248.47934, 394.10373\",\"249.97427, 395.4323\",\"251.48552, 396.7423\",\"253.01497, 398.031\",\"254.54988, 399.3132\",\"256.10638, 400.56912\",\"257.667, 401.81992\",\"259.23346, 403.06335\",\"260.7979, 404.30936\",\"262.36813, 405.54807\",\"263.93427, 406.79193\",\"265.49643, 408.04077\",\"267.05292, 409.29672\",\"268.61517, 410.54547\",\"270.1701, 411.80334\",\"271.7035, 413.08737\",\"273.24142, 414.36597\",\"274.77637, 415.6481\",\"276.30844, 416.9337\",\"277.8377, 418.2226\",\"279.35687, 419.5234\",\"280.88223, 420.81696\",\"282.39755, 422.12225\",\"283.91287, 423.42755\",\"285.42575, 424.7357\",\"286.92078, 426.06418\",\"288.42377, 427.38367\",\"289.91763, 428.71347\",\"291.40225, 430.0536\",\"292.86752, 431.41483\",\"294.33215, 432.77676\",\"295.77808, 434.15854\",\"297.20514, 435.55978\",\"298.6227, 436.97064\",\"300.0116, 438.40973\",\"301.38135, 439.86703\",\"302.7319, 441.34216\",\"304.062, 442.83575\",\"305.3613, 444.35623\",\"306.6507, 445.8851\",\"307.89584, 447.45023\",\"309.08173, 449.0607\",\"310.22952, 450.69855\",\"311.35245, 452.35355\",\"312.43423, 454.03574\",\"313.47946, 455.74088\",\"314.49774, 457.46225\",\"315.47067, 459.20966\",\"316.40524, 460.97787\",\"317.2916, 462.77075\",\"318.1482, 464.57803\",\"318.95337, 466.40878\",\"319.67416, 468.27438\",\"320.2538, 470.18854\",\"320.66574, 472.14566\",\"320.88416, 474.1337\",\"320.99393, 476.13068\",\"321.02045, 478.1305\",\"321.0199, 480.1305\",\"321.01935, 482.1305\",\"320.99017, 484.13028\",\"320.96143, 486.13007\",\"320.87292, 488.1281\",\"320.78577, 490.12622\",\"320.6986, 492.12433\",\"320.61145, 494.12244\",\"320.5243, 496.12054\",\"320.43713, 498.11865\",\"320.35, 500.11676\",\"320.26285, 502.11487\"]\r\n";
        public InGame(ContentManager content, GameControls controls)
        {

            GameControls = controls;

            contentManager = content;

            SplineVerts = JsonConvert.DeserializeObject<Vector2[]>(Map);

            //we are forced to use events here due to Monogame.UI
            //events are like functions that we can add to somthing
            //in this case, when the start game button is clicked, StartGameButton_Clicked is called
            //we can igore sender, e, (noone ever really uses them)
            //in this case, we are "adding" this function to the list of functions that the button does when clicked.
            //that is why "+=" is used
            //we can acsess the button from here because it is public static
            GameControls.EverythingButton.Clicked += EverythingButton_Clicked;

            GameControls.BuyBasicBuilding.Clicked += BuyBasicBuilding_Clicked;
            GameControls.BuySplashBuilding.Clicked += BuySplashBuilding_Clicked;
        }

        private void BuySplashBuilding_Clicked(object sender, EventArgs e)
        {
            TowerToBeBought = TowerBuy.Splash;
        }

        private void BuyBasicBuilding_Clicked(object sender, EventArgs e)
        {
            TowerToBeBought = TowerBuy.Basic;
        }

        private void EverythingButton_Clicked(object sender, EventArgs e)
        {
            if (paused)
                GameControls.EverythingButton.Text = "Pause";
            else
                GameControls.EverythingButton.Text = "Resume";

            paused = !paused;
        }

        public bool Update(Texture2D gameMap)
        {
            if(Health <= 0)
            {
                return true;
            }

            UpdateInputs();

            //see if there is rising edge
            if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != mouseState.LeftButton)
            {
                bool hasSelected = false;
                foreach(Tower tower in towers)
                {
                    if(MathFunc.RectangleFromCenterSize(tower.Location.ToPoint(), new Point(64)).Contains(mouseState.Position))
                    {//clicked on this tower
                        SelectedTower = tower;
                        //open upgrade menu

                        GameControls.UpgradeForm.IsVisible = true;

                        //tell the tower to edit the gui as it sees fits
                        tower.GuiOpened();

                        //make it upgrade
                        GameControls.LeftButton.Clicked += SelectedTower.UpgradePath1;
                        GameControls.RightButton.Clicked += SelectedTower.UpgradePath2;

                        hasSelected = true;
                        break;
                    }
                }

                //see if user actually clicked on somthing
                //also make sure user is not clicking on gui
                if(!hasSelected && !GameControls.UpgradeForm.HitBox.Contains(mouseState.Position))
                {//hide it
                    GameControls.UpgradeForm.IsVisible = false;
                    //remove the previous events so that only one tower is upgraded
                    if (SelectedTower != null)
                    {
                        GameControls.LeftButton.Clicked -= SelectedTower.UpgradePath1;
                        GameControls.RightButton.Clicked -= SelectedTower.UpgradePath2;
                    }

                    SelectedTower = null;
                }

                //also allow for deselecting other menu
                if(!GameControls.UpgradeForm.Contains(mouseState.Position))
                {
                    GameControls.BuildingDescription.Text = "";
                }
            }

            if(TowerToBeBought != TowerBuy.None)
            {
                Point MouseLocTopLeft = mouseState.Position - new Point(32);

                Color[] Colors = new Color[gameMap.Width * gameMap.Height];
                gameMap.GetData(Colors);


                IsValidPlacement = true;

                for(int i = MouseLocTopLeft.X; i < MouseLocTopLeft.X + 64; i++)
                {
                    for (int j = MouseLocTopLeft.Y; j < MouseLocTopLeft.Y + 64; j++)
                    {
                        //make sure it is in range
                        if(i < 0 || j < 0 || i >= Game1.WindowSize.X || j >= Game1.WindowSize.Y)
                        {
                            continue;
                        }

                        if (Colors[TwoDToFlat(i, j, gameMap.Width)] != Color.White)
                        {
                            IsValidPlacement = false;
                            break;
                        }
                    }
                }

                int TwoDToFlat(int x, int y, int width)
                {
                    return x + y * width;
                }

                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != mouseState.LeftButton && !GameControls.BuyBasicBuilding.Contains(mouseState.Position))
                {
                    if(IsValidPlacement && TowerToBeBought == TowerBuy.Basic && Coins - 200 >= 0)
                    {
                        Coins -= 200;
                        towers.Add(new BasicTower(mouseState.Position.ToVector2(), contentManager, this));
                    }
                    else if (IsValidPlacement && TowerToBeBought == TowerBuy.Splash && Coins - 400 >= 0)
                    {
                        Coins -= 400;
                        towers.Add(new SplashTower(contentManager, mouseState.Position.ToVector2(), this));
                    }
                    TowerToBeBought = TowerBuy.None;
                }
            }


            if (paused)
            {
                //if paused, dont update anything for the game
                return false;
            }

            switch (gamePart)
            {
                case GamePart.SpawnWave:
                    SpawnWave();
                    break;
                case GamePart.DuringWave:
                    DuringWave();
                    break;
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                //if it ran out of time remove it
                if (projectiles[i].TicksLeft <= 0)
                    projectiles.RemoveAt(i);
            }

            //go through the list backwards to make removal easier
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update();

                if (enemies[i].ToBeRemoved)
                    enemies.RemoveAt(i);
            }

            //before updating towers, sort enemies so the towers shoot the ones that are the farthest
            enemies.Sort((a, b) => b.CurrentIndex.CompareTo(a.CurrentIndex));
            foreach(Tower tower in towers)
            {
                tower.Update(enemies, projectiles);
            }

            return false;
        }

        private void SpawnWave()
        {
            TimeSinceStart++;

            //since monogame runs at 60fps, this occurs every 0.5f seconds
            if (TimeSinceStart % 30 == 0)
            {
                SpawnEnemyFromLevel(Random.Shared.Next(CurrentWave));

                enemiesSpawned++;
            }

            //each wave, 12 more enimies are spawned
            if (enemiesSpawned > CurrentWave * 12)
            {
                TimeSinceStart = 0;
                enemiesSpawned = 0;
                gamePart = GamePart.DuringWave;
            }
        }

        void SpawnEnemyFromLevel(int level)
        {
            switch(level)
            {
                case 0:
                    enemies.Add(new WaveOneEnemy(this));
                    break;
                case 1:
                    enemies.Add(new WaveTwoEnemy(contentManager, this));
                    break;
                case 2:
                    enemies.Add(new WaveThreeEnemy(contentManager, this));
                    break;
            }
        }

        private void DuringWave()
        {
            if(enemies.Count == 0)
            {
                CurrentWave++;
                gamePart = GamePart.SpawnWave;
                projectiles.Clear();
            }
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            //draw path
            for (int i = 1; i < SplineVerts.Length; i++)
            {
                shapeBatch.FillLine(SplineVerts[i], SplineVerts[i - 1], 16, Color.Tan);
            }

            //draw the range of the selected tower
            if(SelectedTower != null)
            {
                //multiplying reduces all color values, including alpha which makes it more transparent
                shapeBatch.DrawCircle(SelectedTower.Location, SelectedTower.range, Color.DarkGray * 0.5f, Color.GhostWhite * 0.5f, 3);
            }

            //draw enemies on path
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch, shapeBatch);
            }

            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch, shapeBatch);
            }

            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch, shapeBatch);
            }


            if(TowerToBeBought == TowerBuy.Basic)
            {
                shapeBatch.DrawCircle(mouseState.Position.ToVector2(), 100, Color.DarkGray * 0.5f, Color.GhostWhite * 0.5f, 3);

                spriteBatch.Draw(contentManager.Load<Texture2D>("basicWhole"), MathFunc.RectangleFromCenterSize(mouseState.Position, new Point(64)), 
                    IsValidPlacement ? Color.White : Color.Red);//this is a ternary operator
                //replace with normal "if" if you want
                ///https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
            }

            if (TowerToBeBought == TowerBuy.Splash)
            {
                shapeBatch.DrawCircle(mouseState.Position.ToVector2(), 200, Color.DarkGray * 0.5f, Color.GhostWhite * 0.5f, 3);

                spriteBatch.Draw(contentManager.Load<Texture2D>("SplashAll"), MathFunc.RectangleFromCenterSize(mouseState.Position, new Point(64)),
                    IsValidPlacement ? Color.White : Color.Red);//this is a ternary operator
                //replace with normal "if" if you want
                ///https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
            }

            //draw coin count, health
            //we can afford to load in the font every frame because contentmanager has a cache of loaded assets
            SpriteFont spriteFont = contentManager.Load<SpriteFont>("largeFont");


            spriteBatch.Draw(contentManager.Load<Texture2D>("heart"), new Rectangle(100 + 200, 10, 64, 64), Color.White);
            spriteBatch.DrawString(spriteFont, Health.ToString(), new Vector2(170 + 200, 17), Color.White);

            spriteBatch.Draw(contentManager.Load<Texture2D>("coin"), new Rectangle(100, 10, 64, 64), Color.White);
            spriteBatch.DrawString(spriteFont, Coins.ToString(), new Vector2(160, 17), Color.White);

            spriteBatch.DrawString(spriteFont, $"Wave: {CurrentWave}", new Vector2(450, 17), Color.White);
        }

        public void DrawValidPlacement(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            for (int i = 1; i < SplineVerts.Length; i++)
            {
                shapeBatch.FillLine(SplineVerts[i], SplineVerts[i - 1], 16, Color.Black);
            }

            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch, shapeBatch);
            }
        }

        private void UpdateInputs()
        {
            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
        }

        internal enum GamePart
        {
            SpawnWave,
            DuringWave,
        }

        internal enum TowerBuy
        {
            Splash,
            None,
            Basic,
        }
    }
}