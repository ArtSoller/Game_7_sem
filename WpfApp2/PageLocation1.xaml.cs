using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page1.xaml
/// </summary>
public partial class Page1
{
    public Page1(Player pl1, Player pl2)
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

    #region Настройка полотна и игры.
    private void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player1, _me.X);
        Canvas.SetTop(Player1, _me.Y);

        Canvas.SetLeft(Player2, _companion.X);
        Canvas.SetTop(Player2, _companion.Y);


        // Переходы на карты.
        Canvas.SetLeft(TeleportToLocaltion2, SystemParameters.VirtualScreenWidth - TeleportToLocaltion2.Width - 30);


        // Ставим мольберты.
        Canvas.SetTop(picture2, 0.2 * (SystemParameters.VirtualScreenHeight - picture2.Height));
        Canvas.SetLeft(picture2, 0.33 * (SystemParameters.VirtualScreenWidth - picture2.Width));

        Canvas.SetTop(picture3, 0.2 * (SystemParameters.VirtualScreenHeight - picture3.Height));
        Canvas.SetLeft(picture3, 0.66 * (SystemParameters.VirtualScreenWidth - picture3.Width));

        Canvas.SetTop(picture4, 0.8 * (SystemParameters.VirtualScreenHeight - picture4.Height));
        Canvas.SetLeft(picture4, 0.2 * (SystemParameters.VirtualScreenWidth - picture4.Width));

        Canvas.SetTop(picture5, 0.8 * (SystemParameters.VirtualScreenHeight - picture5.Height));
        Canvas.SetLeft(picture5, 0.5 * (SystemParameters.VirtualScreenWidth - picture5.Width));

        Canvas.SetTop(picture6, 0.8 * (SystemParameters.VirtualScreenHeight - picture6.Height));
        Canvas.SetLeft(picture6, 0.8 * (SystemParameters.VirtualScreenWidth - picture6.Width));


        // Ставим область вокруг мольбертов.
        // Лютый подгон.
        Canvas.SetTop(AreaEasel2, 0.2 * (SystemParameters.VirtualScreenHeight - AreaEasel2.Height) - 15);
        Canvas.SetLeft(AreaEasel2, 0.33 * (SystemParameters.VirtualScreenWidth - AreaEasel2.Width) - 12);

        Canvas.SetTop(AreaEasel3, 0.2 * (SystemParameters.VirtualScreenHeight - AreaEasel3.Height) - 15);
        Canvas.SetLeft(AreaEasel3, 0.66 * (SystemParameters.VirtualScreenWidth - AreaEasel3.Width) + 12);

        Canvas.SetTop(AreaEasel4, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel4.Height) + 18);
        Canvas.SetLeft(AreaEasel4, 0.2 * (SystemParameters.VirtualScreenWidth - AreaEasel4.Width) - 18);

        Canvas.SetTop(AreaEasel5, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel5.Height) + 15);
        Canvas.SetLeft(AreaEasel5, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel5.Width));

        Canvas.SetTop(AreaEasel6, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height) + 18);
        Canvas.SetLeft(AreaEasel6, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel6.Width) + 18);

        // Ставим стены.
        //Canvas.SetTop(wallTop, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height) + 18);
        //Canvas.SetLeft(wallTop, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel6.Width) + 18);

        //Canvas.SetTop(wallLeft, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height) + 18);
        //Canvas.SetLeft(wallLeft, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel6.Width) + 18);

        //Canvas.SetTop(wallRight, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height) + 18);
        //Canvas.SetLeft(wallRight, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel6.Width) + 18);

        //Canvas.SetTop(wallBottom, 0.8 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height) + 18);
        //Canvas.SetLeft(wallBottom, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel6.Width) + 18);


    }

    private void GameSetUp()
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");

        MyCanvas.Focus();

        gameTimer.Interval = TimeSpan.FromMilliseconds(16);

        gameTimer.Tick += GameLoop;

        gameTimer.Start();

        ImageBrush MyImage = new()
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/pacman.png"))
        };
        Player1.Fill = MyImage;


        //ImageBrush CompanionImage = new()
        //{
        //    ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/pacman.png"))
        //};
        //Player2.Fill = CompanionImage;
    }
    #endregion

    // TODO: Улучшить взаимодействие с мольбертом.
    #region Механика игры

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            _isUpKeyPressed = true;
            _isPlayerMovingUpward = true;
        }

        if (e.Key == Key.A)
        {
            _isLeftKeyPressed = true;
            _isPlayerMovingLeftward = true;
            Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
        }

        if (e.Key == Key.D)
        {
            _isRightKeyPressed = true;
            _isPlayerMovingRightward = true;
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);
        }

        if (e.Key == Key.S)
        {
            _isDownKeyPressed = true;
            _isPlayerMovingDownward = true;
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
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        _isPossibleUpwardMovement = Canvas.GetTop(Player1) > 70;
        _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > 19;
        _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + 30 < Application.Current.MainWindow.ActualWidth - 39;
        _isPossibleDownwardMovement = Canvas.GetTop(Player1) + 30 < Application.Current.MainWindow.ActualHeight - 70;

        // asssign the pac man hit box to the pac man rectangle
        pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

        foreach (var obj in MyCanvas.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && pacmanHitBox.IntersectsWith(hitBox))
            {
                NavigationService?.Navigate(TeleportTo(Location.Location2));
            }

            if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                NavigationService?.Navigate(new Page3(_me, _companion));
            }

            // check if we are colliding with the wall while moving up if true then stop the pac man movement
            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingUpward)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + 15);
                _isPossibleUpwardMovement = false;
                _me.SpeedY = 0;
                _isPlayerMovingUpward = false;
            }

            // check if we are colliding with the wall while moving left if true then stop the pac man movement
            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingLeftward)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + 20);
                _isPossibleLeftwardMovement = false;
                _me.SpeedX = 0;
                _isPlayerMovingLeftward = false;
            }

            // check if we are colliding with the wall while moving right if true then stop the pac man movement
            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingRightward)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - 20);
                _isPossibleRightwardMovement = false;
                _me.SpeedX = 0;
                _isPlayerMovingRightward = false;
            }

            // check if we are colliding with the wall while moving down if true then stop the pac man movement
            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingDownward)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - 15);
                _isPossibleDownwardMovement = false;
                _me.SpeedY = 0;
                _isPlayerMovingDownward = false;
            }
        }

    }

    private void GameLoop(object sender, EventArgs e)
    {
        SetMovementsStatus();

        SetMovementPossibility();

        if (_isUpKeyPressed && _isPossibleUpwardMovement) _me.SpeedY += _speed;
        else if (!_isPossibleUpwardMovement && _isPlayerMovingUpward) _me.SpeedY = 0;

        if (_isLeftKeyPressed && _isPossibleLeftwardMovement) _me.SpeedX -= _speed;
        else if (!_isPossibleLeftwardMovement && _isPlayerMovingLeftward) _me.SpeedX = 0;

        if (_isRightKeyPressed && _isPossibleRightwardMovement) _me.SpeedX += _speed;
        else if (!_isPossibleRightwardMovement && _isPlayerMovingRightward) _me.SpeedX = 0;

        if (_isDownKeyPressed && _isPossibleDownwardMovement) _me.SpeedY -= _speed;
        else if (!_isPossibleDownwardMovement && _isPlayerMovingDownward) _me.SpeedY = 0;


        _me.SpeedX *= _friction;
        _me.SpeedY *= _friction;

        // Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + _speedX);
        // Canvas.SetTop(Player1, Canvas.GetTop(Player1) - _speedY);

        Canvas.SetLeft(Player1, _me.X + _me.SpeedX);
        Canvas.SetTop(Player1, _me.Y - _me.SpeedY);

        _me.X += _me.SpeedX;
        _me.Y -= _me.SpeedY;

        //Tb1.Text = _isPlayerMovingUpward.ToString();
        //Tb2.Text = _isPlayerMovingLeftward.ToString();
        //Tb3.Text = _isPlayerMovingRightward.ToString();
        //Tb4.Text = _isPlayerMovingDownward.ToString();

        //Tb5.Text = _isUpKeyPressed.ToString();
        //Tb6.Text = _isLeftKeyPressed.ToString();
        //Tb11.Text = _isRightKeyPressed.ToString();
        //Tb12.Text = _isDownKeyPressed.ToString();

        //Tb7.Text = _isPossibleUpwardMovement.ToString();
        //Tb8.Text = _isPossibleLeftwardMovement.ToString();
        //Tb9.Text = _isPossibleRightwardMovement.ToString();
        //Tb10.Text = _isPossibleDownwardMovement.ToString();

        Tb1.Text = _isPlayerMovingUpward.ToString();
        Tb2.Text = _isPlayerMovingLeftward.ToString();
        Tb3.Text = _isPlayerMovingRightward.ToString();
        Tb4.Text = _isPlayerMovingDownward.ToString();
        Tb5.Text = _me.SpeedX.ToString();
        Tb6.Text = _me.SpeedY.ToString();
        Tb7.Text = Canvas.GetLeft(Player1).ToString();
        Tb8.Text = Canvas.GetTop(Player1).ToString();
        Tb9.Text = _me.X.ToString();
        Tb10.Text = _me.Y.ToString();
    }
    #endregion
}