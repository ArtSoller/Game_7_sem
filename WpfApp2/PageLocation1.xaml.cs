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

        MyCanvas.Focus();

        gameTimer.Interval = TimeSpan.FromMilliseconds(10);

        gameTimer.Tick += GameLoop;

        gameTimer.Start();

        ImageBrush MyImage = new()
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/pacman.png"))
        };
        Player1.Fill = MyImage;
    }
    #endregion

    // TODO: Улучшить взаимодействие с мольбертом.
    #region Механика игры

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
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        _isPossibleUpwardMovement = Canvas.GetTop(Player1) > Canvas.GetTop(wallTop) + wallTop.Height;
        _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > Canvas.GetLeft(wallLeft) + wallLeft.Width;
        _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + Player1.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
        _isPossibleDownwardMovement = Canvas.GetTop(Player1) + Player1.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

        pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

        foreach (var obj in MyCanvas.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && pacmanHitBox.IntersectsWith(hitBox))
                NavigationService?.Navigate(TeleportTo(Location.Location2));
            
            if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new Page3(_me, _companion));
            
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

        Tb1.Text = _me.IsMovingUpward.ToString();
        Tb2.Text = _me.IsMovingLeftward.ToString();
        Tb3.Text = _me.IsMovingRightward.ToString();
        Tb4.Text = _me.IsMovingDownward.ToString();

        Tb5.Text = _isUpKeyPressed.ToString();
        Tb6.Text = _isLeftKeyPressed.ToString();
        Tb11.Text = _isRightKeyPressed.ToString();
        Tb12.Text = _isDownKeyPressed.ToString();

        Tb7.Text = _isPossibleUpwardMovement.ToString();
        Tb8.Text = _isPossibleLeftwardMovement.ToString();
        Tb9.Text = _isPossibleRightwardMovement.ToString();
        Tb10.Text = _isPossibleDownwardMovement.ToString();

        //Tb1.Text = Canvas.GetRight(wallRight).ToString();
        //Tb2.Text = _isPlayerMovingLeftward.ToString();
        //Tb3.Text = _isPlayerMovingRightward.ToString();
        //Tb4.Text = _isPlayerMovingDownward.ToString();
        //Tb5.Text = _me.SpeedX.ToString();
        //Tb6.Text = _me.SpeedY.ToString();
        //Tb7.Text = Canvas.GetLeft(Player1).ToString();
        //Tb8.Text = Canvas.GetTop(Player1).ToString();
        //Tb9.Text = _me.X.ToString();
        //Tb10.Text = _me.Y.ToString();
    }
    #endregion
}