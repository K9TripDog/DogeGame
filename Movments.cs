using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.UI;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace DogeGame_3._0
{
    internal class  Movments
    {
        public static bool GoingUp { get; private set; }
        public static bool GoingDown { get; private set; }
        public static bool GoingLeft { get; private set; }
        public static bool GoingRight { get; private set; }

        public static void OnKeyPress(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Up:
                    GoingUp = true;
                    break;
                case VirtualKey.Down:
                    GoingDown = true;
                    break;
                case VirtualKey.Left:
                    GoingLeft = true;
                    break;
                case VirtualKey.Right:
                    GoingRight = true;
                    break;
                default:
                    break;
            }
        }
        public static void OnKeyRelease(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Up:
                    GoingUp = false;
                    break;
                case VirtualKey.Down:
                    GoingDown = false;
                    break;
                case VirtualKey.Left:
                    GoingLeft = false;
                    break;
                case VirtualKey.Right:
                    GoingRight = false;
                    break;
                default:
                    break;
            }
        }
    }
}

