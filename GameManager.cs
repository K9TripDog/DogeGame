using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Gaming.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DogeGame_3._0
{
    class GameManager
    {
        public BoardManager Board { get; set; }
        public GameManager(double boardwidth, double boardheight)
        {
            Board = new BoardManager(boardwidth, boardheight);
        }

        public void UpdateGame()
        {
            Board.MoveEntities();
            Board.IfTouch();
            Board.GameBoardLimits();
            Board.WinGame();
            Board.LoseGame();
        }

        public void StartGame()
        {
            Board.EnemyRemoved = 0;
            Board.MakeEntity();
        }


    }
}









