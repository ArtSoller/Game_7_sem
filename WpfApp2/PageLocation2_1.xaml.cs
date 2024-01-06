using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class PageLocation2_1 : Room
    {
        public PageLocation2_1(Player pl1, Player pl2)
        {
            gameTimer = new();
            InitializeComponent();

            Floor.Height = SystemParameters.VirtualScreenHeight;
            Floor.Width = SystemParameters.VirtualScreenWidth;

            _me = pl1;
            _companion = pl2;

            CanvasSetObjects();
            GameSetUp();
        }

        private void CanvasSetObjects()
        {
            // Ставим игроков.
            Canvas.SetLeft(Player1, _me.X);
            Canvas.SetTop(Player1, _me.Y);

            // Переходы на карты.
            Canvas.SetTop(TeleportToLocaltion3_1, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion3_1.Height));
            Canvas.SetLeft(TeleportToLocaltion3_1, SystemParameters.VirtualScreenWidth - TeleportToLocaltion3_1.Width - 10);

            Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
            Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

            // Ставим книги
            Canvas.SetTop(picture1, 0.5 * (SystemParameters.VirtualScreenHeight - picture1.Height));
            Canvas.SetLeft(picture1, 0.5 * (SystemParameters.VirtualScreenWidth - picture1.Width));

            // Ставим область вокруг книг
            Canvas.SetTop(AreaEasel1, 0.5 * (SystemParameters.VirtualScreenHeight - AreaEasel1.Height) - 15);
            Canvas.SetLeft(AreaEasel1, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel1.Width) - 12);

            // Ставим стены.
            Canvas.SetTop(wallTop, 0);
            Canvas.SetLeft(wallTop, 0);
            wallTop.Height = 80;
            wallTop.Width = SystemParameters.VirtualScreenWidth;

            Canvas.SetTop(wallLeft, 0);
            Canvas.SetLeft(wallLeft, 0);
            wallLeft.Height = SystemParameters.VirtualScreenHeight;
            wallLeft.Width = 25;

            Canvas.SetRight(wallRight, 0);
            Canvas.SetBottom(wallRight, 0);
            wallRight.Height = SystemParameters.VirtualScreenHeight;
            wallRight.Width = 25;

            Canvas.SetRight(wallBottom, 0);
            Canvas.SetBottom(wallBottom, 0);
            wallBottom.Height = 75;
            wallBottom.Width = SystemParameters.VirtualScreenHeight;
        }

        private void GameSetUp()
        {
            if (gameTimer is null) throw new Exception("gameTimer is null");

            Location2_1.Focus();

            gameTimer.Interval = TimeSpan.FromMilliseconds(10);

            gameTimer.Tick += GameLoop;

            gameTimer.Start();

            ImageBrush MyImage = new()
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/pacman.png"))
            };
            Player1.Fill = MyImage;
        }
        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                _isUpKeyPressed = true;
                _me.IsMovingUpward = true;
            }

            if (e.Key == Key.A)
            {
                _isLeftKeyPressed = true;
                _me.IsMovingLeftward = true;
                Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
            }

            if (e.Key == Key.D)
            {
                _isRightKeyPressed = true;
                _me.IsMovingRightward = true;
                Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);
            }

            if (e.Key == Key.S)
            {
                _isDownKeyPressed = true;
                _me.IsMovingDownward = true;
            }

            if (e.Key == Key.F)
            {
                _isForceButtonClicked = true;
            }

            if (e.Key == Key.Escape)
                GameOver("Dead");
        }
        private void GameLoop(object sender, EventArgs e)
        {
            SetMovementsStatus();

            //SetMovementPossibility();

            if (_isUpKeyPressed && _isPossibleUpwardMovement) _me.SpeedY += _speed;
            else if (!_isPossibleUpwardMovement && _me.IsMovingUpward) _me.SpeedY = 0;

            if (_isLeftKeyPressed && _isPossibleLeftwardMovement) _me.SpeedX -= _speed;
            else if (!_isPossibleLeftwardMovement && _me.IsMovingLeftward) _me.SpeedX = 0;

            if (_isRightKeyPressed && _isPossibleRightwardMovement) _me.SpeedX += _speed;
            else if (!_isPossibleRightwardMovement && _me.IsMovingRightward) _me.SpeedX = 0;

            if (_isDownKeyPressed && _isPossibleDownwardMovement) _me.SpeedY -= _speed;
            else if (!_isPossibleDownwardMovement && _me.IsMovingDownward) _me.SpeedY = 0;

            _me.SpeedX *= _friction;
            _me.SpeedY *= _friction;

            // Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + _speedX);
            // Canvas.SetTop(Player1, Canvas.GetTop(Player1) - _speedY);

            Canvas.SetLeft(Player1, _me.X + _me.SpeedX);
            Canvas.SetTop(Player1, _me.Y - _me.SpeedY);

            _me.X += _me.SpeedX;
            _me.Y -= _me.SpeedY;
        }

    }
}

