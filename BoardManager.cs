using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace DogeGame_3._0
{
    internal class BoardManager
    {

        public double Board_Width { get; set; }
        public double Board_Height { get; set; }
        public Entity[] Entities { get; set; }

        public Random rnd = new Random();
        public int EnemyRemoved = 0;
        public int EntitiesCount = 11;
        public BoardManager(double boardwidth, double boardheight)
        {
            Board_Width = boardwidth;
            Board_Height = boardheight;
            Entities = new Entity[EntitiesCount];
        }
        public void MakeEntity()
        {
            Entities[0] = new Player(Board_Width / 2, Board_Height / 1.5);

            // starts from one because the first one in the arry is Player
            for (int i = 1; i < Entities.Length; i++)
            {   //                                                    for not deploying outside the board
                Entities[i] = new Enemy(Entities[0], rnd.NextDouble() * (Board_Width - Enemy.EnemySize), rnd.NextDouble() * (Board_Height / 1.5 - Enemy.EnemySize));
            }
        }
        public void MoveEntities()
        {
            for (int i = 0; i < Entities.Length; i++)
                if (Entities[i] != null)
                {
                    Entities[i].EntityMovment();
                }
        }
        public void IfTouch()
        {
            for (int i = 1; i < Entities.Length; i++)
            {
                for (int j = 1; j < Entities.Length; j++)
                {
                    if (Entities[i] != null && Entities[j] != null && i != j)
                    {
                        double MidEnemySize = Entities[i].Size / 2;
                        double MidPlayerSize = Entities[0].Size / 2;

                        double enemy1_X = Entities[i].PositionX + MidEnemySize;
                        double enemy1_Y = Entities[i].PositionY + MidEnemySize;

                        double enemy2_X = Entities[j].PositionX + MidEnemySize;
                        double enemy2_Y = Entities[j].PositionY + MidEnemySize;

                        double player_X = Entities[0].PositionX + MidPlayerSize;
                        double player_Y = Entities[0].PositionY + MidPlayerSize;

                        //caculate the square root                   number -   power
                        double EnemiesDistance = Math.Sqrt(Math.Pow(enemy2_X - enemy1_X, 2) + Math.Pow(enemy2_Y - enemy1_Y, 2));
                        double DistanceWithPlayer = Math.Sqrt(Math.Pow(player_X - enemy1_X, 2) + Math.Pow(player_Y - enemy1_Y, 2));

                        if (EnemiesDistance < Entities[i].Size - 1)
                        {
                            EnemyRemoved += 1;
                            Entities[i] = null;
                        }
                        if (DistanceWithPlayer < MidPlayerSize + MidEnemySize - 1)
                        {
                            Entities[0] = null;
                            return;
                        }

                    }
                }
            }

        }
        public void GameBoardLimits()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] != null)
                {
                    if (Entities[i].PositionX + Entities[i].Size > Board_Width || Entities[i].PositionY + Entities[i].Size > Board_Height || Entities[i].PositionX < 0 || Entities[i].PositionY < 0)
                    {
                        Entities[i] = null;
                        if (Entities[0] == null)
                        {
                            return;
                        }
                    }
                }
            }

        }
        public bool WinGame()
        {
            if (EnemyRemoved >= 9)
            {
                return true;
            }

            return false;
        }
        public bool LoseGame()
        {
            if (Entities[0] == null)
            {
                return true;
            }

            return false;
        }
    }
}
