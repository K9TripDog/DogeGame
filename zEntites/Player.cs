using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace DogeGame_3._0
{
    class Player : Entity
    {
        public const int PlayerSize = 40;
        public const int PlayerSpeed = 3;
        public Player(double Playerposition_x, double Playerposition_y) : base(Playerposition_x, Playerposition_y, PlayerSize, PlayerSpeed) 
        {

        }
        public override void EntityMovment() 
        {
            // Y = from top down. X = left to right
            if (Movments.GoingUp)
                PositionY -= Speed;

            if (Movments.GoingDown)
                PositionY += Speed;

            if (Movments.GoingRight)
                PositionX += Speed;

            if (Movments.GoingLeft)
                PositionX -= Speed;
        }


    }

}
