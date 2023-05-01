using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace DogeGame_3._0
{
    internal class Entity
    {
        public double PositionX { get; set; }
        public double PositionY { get; set; } 
        public double Size { get; set; }
        public double Speed { get; set; }

        public Entity(double position_x, double position_y, double size, double speed)
        {
            Speed = speed;
            Size = size; //Size is both width and height (the same)
            PositionX = position_x;
            PositionY = position_y;
        }
       
        public virtual void EntityMovment()
        {

        }

    }

}
