using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.UI;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace DogeGame_3._0
{
    class Enemy : Entity
    {
        public const int EnemySize = 35;
        public const int EnemySpeed = 3;
        public Entity Target;
        public Enemy(Entity target, double Enemyposition_x, double Enemyposition_y) : base(Enemyposition_x, Enemyposition_y, EnemySize, EnemySpeed)
        {
            Target = target;
        }
        public override void EntityMovment()
        {
            // Calculate the angle to the player                   enemy                                   ememy
            double angle = Math.Atan2(Target.PositionY - PositionY, Target.PositionX - PositionX);

            Speed += 0.001; // making the enemy faster every time tick,(i add this just for fun)

            // Update the enemy position
            PositionX += Math.Cos(angle) * Speed; //caculate the radius
            PositionY += Math.Sin(angle) * Speed;
        }
    }

}
