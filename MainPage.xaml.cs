using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Power;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.UI;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DogeGame_3._0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //if not wirting public/private so this is private by Default.
        private GameManager Game;
        private bool GameStart = false;
        private bool Timer_isRunning = false;
        Ellipse[] Entities_Graphic;
        TextBlock[] Information_Text;
        ImageBrush Brush_Image;
        DispatcherTimer Timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyPress;  // for the player movement Press
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyRelease;  // for the player movement Release
            Window.Current.CoreWindow.KeyDown += KeyShourtCuts; // for the keyboard shortcuts (Start/stop ...)

            Timer.Interval = TimeSpan.FromMilliseconds(10);
            Timer.Tick += EveryTimerTick;

            Game = new GameManager(GameBoard.Width, GameBoard.Height);
            CreateInformation_Text();
          
        }

        // in game
        public void EveryTimerTick(object sender, object e)
        {
            Game.UpdateGame();
            UpdateGraphicPosition();
            RemoveGraphic();
            IfGameEnd();
        }
        public void UpdateGraphicPosition()
        {
            for (int i = 0; i < Game.Board.Entities.Length; i++)
            {
                if (Game.Board.Entities[i] != null)
                {
                    Canvas.SetLeft(Entities_Graphic[i], Game.Board.Entities[i].PositionX);
                    Canvas.SetTop(Entities_Graphic[i], Game.Board.Entities[i].PositionY);
                }
            }
        }
        public void RemoveGraphic()
        {
            for (int i = 0; i < Game.Board.Entities.Length; i++)
            {
                if (Game.Board.Entities[i] == null)
                {
                    GameBoard.Children.Remove(Entities_Graphic[i]);
                }
            }
        }
        public void IfGameEnd()
        {
            if (Game.Board.WinGame() == true || Game.Board.LoseGame() == true)
            {
                Timer.Stop();
                AllComandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
                GameBoard.Children.Add(Information_Text[0]);
                GameBoard.Children.Add(Information_Text[1]);

                if (Game.Board.LoseGame() == true)
                {
                    Information_Text[1].Text = "You Lost!";
                }
                else
                {
                    Information_Text[1].Text = "You Won!";
                }
            }
        }

        //for starting game
        public void CreateEntityGraphic()
        {
            Entities_Graphic = new Ellipse[Game.Board.EntitiesCount];
            Entities_Graphic[0] = new Ellipse();
            Brush_Image = new ImageBrush();
            Brush_Image.ImageSource = new BitmapImage(new Uri("ms-appx:///Photos/Earth.gif"));

            Entities_Graphic[0].Fill = Brush_Image;
            Entities_Graphic[0].Width = Player.PlayerSize;
            Entities_Graphic[0].Height = Player.PlayerSize;//Game.Board.Entities[0].Size;//Player.PlayerSize;

            GameBoard.Children.Add(Entities_Graphic[0]);

            for (int i = 1; i < Game.Board.Entities.Length; i++)
            {
                Entities_Graphic[i] = new Ellipse();
                Brush_Image = new ImageBrush();
                Brush_Image.ImageSource = new BitmapImage(new Uri("ms-appx:///Photos/sun.gif"));

                Entities_Graphic[i].Fill = Brush_Image;
                Entities_Graphic[i].Width = Enemy.EnemySize;
                Entities_Graphic[i].Height = Enemy.EnemySize;

                GameBoard.Children.Add(Entities_Graphic[i]);
            }
            UpdateGraphicPosition();
        }
        public void CreateInformation_Text()
        {
            Information_Text = new TextBlock[4];
            for (int i = 0; i < Information_Text.Length; i++)
            { // Default settings
                Information_Text[i] = new TextBlock();
                Information_Text[i].Width = GameBoard.Width;
                Information_Text[i].Height = GameBoard.Height;
                Information_Text[i].TextAlignment = TextAlignment.Center;
                Information_Text[i].FontWeight = FontWeights.Bold;
            }

            Brush_Image = new ImageBrush();
            Brush_Image.ImageSource = new BitmapImage(new Uri("ms-appx:///Photos/Retro.gif"));

            //0 = Start Text Information.
            Information_Text[0].FontSize = 30;
            Information_Text[0].Foreground = Brush_Image;
            Information_Text[0].Text = "  Press Enter\n to start a new game";
            Canvas.SetLeft(Information_Text[0], 0);
            Canvas.SetTop(Information_Text[0], 400);

            //2 = ShourCuts Explain
            Information_Text[2].FontSize = 15;
            Information_Text[2].Foreground = new SolidColorBrush(Colors.DarkRed);
            Information_Text[2].Text = "Enter = Start New Game \n Space = Stop/Resume \n S = Save \n L = Load";
            Canvas.SetLeft(Information_Text[2], 0);
            Canvas.SetTop(Information_Text[2], 550);

            //1 = Win Lose & name Text Information.
            Brush_Image = new ImageBrush();
            Brush_Image.ImageSource = new BitmapImage(new Uri("ms-appx:///Photos/sun.gif"));

            Information_Text[1].FontSize = 40;
            Information_Text[1].Foreground = Brush_Image;
            Canvas.SetLeft(Information_Text[1], 0);
            Canvas.SetTop(Information_Text[1], 350);
            Information_Text[1].Text = "Please Save Our Earth";

            //3 = doesnt Exist file
            Information_Text[3].FontSize = 25;
            Information_Text[3].Foreground = new SolidColorBrush(Colors.Red);
            Information_Text[3].Text = "File Doesn't Exist";
            Canvas.SetLeft(Information_Text[3], 0);
            Canvas.SetTop(Information_Text[3], 500);

            GameBoard.Children.Add(Information_Text[0]); // Start Text Information.
            GameBoard.Children.Add(Information_Text[1]); // Game Name Text.
            GameBoard.Children.Add(Information_Text[2]); // ShourCuts Explain Text.
        }

        //Command Bar Methods
        public void StartGame()
        {
            GameStart = true;
            GameBoard.Children.Clear();
            AllComandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Hidden;
            Game.StartGame();
            CreateEntityGraphic();
            Timer.Start();
            Timer_isRunning = true;
        }
        public void StopGame()
        {
            if (Timer_isRunning == true)
            {
                Timer.Stop();
                Timer_isRunning = false;
                AllComandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
                GameBoard.Children.Add(Information_Text[2]); //shourtcuts
            }
        }
        public void Exit()
        {
            Application.Current.Exit();
        }
        public void ResumeGame()
        {
            // if game doesnt even started &  if not contains - win or lose Text
            if (GameStart == true && !GameBoard.Children.Contains(Information_Text[1]))
            {
                Timer.Start();
                Timer_isRunning = true;
                AllComandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Hidden;
                GameBoard.Children.Remove(Information_Text[2]); //shourtcuts text
            }
        }
        public async void Save()
        {
            // if game doesnt even started &  if not contains - win or lose Text
            if (GameStart == true && !GameBoard.Children.Contains(Information_Text[1]))
            {
                string PositionData = "";
                string SplitEntities = "|";

                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile File = await localFolder.CreateFileAsync("Entities_position.txt", CreationCollisionOption.ReplaceExisting);

                for (int i = 0; i < Entities_Graphic.Length; i++)
                {
                    if (Game.Board.Entities[i] != null)
                    {
                        double positionX = Canvas.GetLeft(Entities_Graphic[i]);
                        double positionY = Canvas.GetTop(Entities_Graphic[i]);

                        PositionData += $"{positionX},{positionY}{SplitEntities}";
                    }
                }
                await FileIO.WriteTextAsync(File, PositionData);
            }

        }
        public async void Load()
        {
            GameBoard.Children.Clear();
            try
            {
                Game.Board.EnemyRemoved = 0;

                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile saveFile = await localFolder.GetFileAsync("Entities_position.txt");

                string RowPositions = await FileIO.ReadTextAsync(saveFile);

                //split Between every Entity
                string[] EntitiesLoadCount = RowPositions.Split('|');

                //for every entity
                double[] Position_x = new double[EntitiesLoadCount.Length - 1];
                double[] Position_y = new double[EntitiesLoadCount.Length - 1];

                CreateEntityGraphic();
                Game.Board.MakeEntity();
                for (int i = 0; i < EntitiesLoadCount.Length - 1; i++)
                {
                    //Split between X and Y
                    string[] LoadEntity_XY = EntitiesLoadCount[i].Split(',');

                    Position_x[i] = double.Parse(LoadEntity_XY[0]);
                    Position_y[i] = double.Parse(LoadEntity_XY[1]);

                    Game.Board.Entities[i].PositionX = Position_x[i];
                    Game.Board.Entities[i].PositionY = Position_y[i];

                    Canvas.SetLeft(Entities_Graphic[i], Position_x[i]);
                    Canvas.SetTop(Entities_Graphic[i], Position_y[i]);

                }
                for (int i = 0; i < Entities_Graphic.Length; i++)
                {
                    if (EntitiesLoadCount.Length - 1 <= i)
                    {
                        Game.Board.Entities[i] = null;
                        GameBoard.Children.Remove(Entities_Graphic[i]);
                        Game.Board.EnemyRemoved += 1;
                    }
                }
                GameStart = true;
                StopGame();
            }
            catch (FileNotFoundException)
            {
                // if the file doesn't exist
                Timer.Stop();
                GameBoard.Children.Add(Information_Text[2]); //shourcuts Text
                GameBoard.Children.Add(Information_Text[3]); //not exist file Text
                return;
            }
        }



        //Game Clicks (Command Bar)
        private void StartBar_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        private void StopBar_Click(object sender, RoutedEventArgs e)
        {
            StopGame();
        }
        private void ResumeBar_Click(object sender, RoutedEventArgs e)
        {
            ResumeGame();
        }
        private void SaveBar_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        private void LoadBar_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void ExitBar_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }
       
        //Movments
        private void CoreWindow_KeyPress(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            Movments.OnKeyPress(args.VirtualKey);
        }
        private void CoreWindow_KeyRelease(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            Movments.OnKeyRelease(args.VirtualKey);
        }
        private void KeyShourtCuts(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Enter)
            {
                StartGame();
            }
            if (args.VirtualKey == VirtualKey.Space)
                if (Timer_isRunning == true)
                {
                    StopGame();
                }
                else
                {
                    ResumeGame();
                    return;
                }
            if (args.VirtualKey == VirtualKey.L)
            {
                Load();

            }
            if (args.VirtualKey == VirtualKey.S)
            {
                Save();

            }
            if (args.VirtualKey == VirtualKey.Escape)
            {
                Exit();
            }
        } 
    }
}


