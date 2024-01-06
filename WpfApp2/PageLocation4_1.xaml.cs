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
    public partial class PageLocation4_1 : Room
    {
        public PageLocation4_1(Player pl1, Player pl2)
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
            Canvas.SetTop(TeleportToLocaltion0, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion0.Height));
            Canvas.SetLeft(TeleportToLocaltion0, SystemParameters.VirtualScreenWidth - TeleportToLocaltion0.Width - 10);

            Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
            Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

            // Ставим книгу и бумагу
            Canvas.SetTop(picture1, 0.1 * (SystemParameters.VirtualScreenHeight - picture1.Height));
            Canvas.SetLeft(picture1, 0.5 * (SystemParameters.VirtualScreenWidth - picture1.Width));

            Canvas.SetTop(picture2, 0.9 * (SystemParameters.VirtualScreenHeight - picture2.Height));
            Canvas.SetLeft(picture2, 0.5 * (SystemParameters.VirtualScreenWidth - picture2.Width));

            // Ставим область вокруг книги и бумаги
            Canvas.SetTop(AreaEasel1, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel1.Height));
            Canvas.SetLeft(AreaEasel1, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel1.Width));

            Canvas.SetTop(AreaEasel2, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel2.Height));
            Canvas.SetLeft(AreaEasel2, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel2.Width) );

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

            Location4_1.Focus();

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

        private void SetMovementPossibility()
        {
            if (_toDisplay)
            {
                if (_me is null) throw new ArgumentException("_me is null");
                if (_companion is null) throw new ArgumentException("_companion is null");

                _isPossibleUpwardMovement = Canvas.GetTop(Player1) > wallTop.Height;
                _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > wallLeft.Width;
                _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + Player1.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
                _isPossibleDownwardMovement = Canvas.GetTop(Player1) + Player1.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

                pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

                foreach (var obj in Location4_1.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
                {
                    Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                    if ((string)obj.Tag == "teleport" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        _toDisplay = false;
                        NavigationService?.Navigate(TeleportTo(Location.Location0));
                    }


                    if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        //mediaPlayer.Play();
                        NavigationService?.Navigate(new Page8(_me, _companion));
                    }

                    if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingUpward)
                    {
                        _isPossibleUpwardMovement = false;
                        _me.SpeedY = 0;
                        _me.Y = Canvas.GetTop(obj) + obj.Height + 30;
                        _me.IsMovingUpward = false;
                    }

                    if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingLeftward)
                    {
                        _isPossibleLeftwardMovement = false;
                        _me.SpeedX = 0;
                        _me.X = Canvas.GetLeft(obj) + obj.Width + 30;
                        _me.IsMovingLeftward = false;
                    }

                    if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingRightward)
                    {
                        _isPossibleRightwardMovement = false;
                        _me.SpeedX = 0;
                        _me.X = Canvas.GetLeft(obj) - 0.5 * obj.Width;
                        _me.IsMovingRightward = false;
                    }

                    if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingDownward)
                    {
                        _me.Y = Canvas.GetTop(obj) - 0.5 * obj.Height - 30;
                        _isPossibleDownwardMovement = false;
                        _me.SpeedY = 0;
                        _me.IsMovingDownward = false;
                    }
                }
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            SetMovementsStatus();

            SetMovementPossibility();

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

