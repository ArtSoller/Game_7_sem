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
/// Логика взаимодействия для Page2.xaml
/// </summary>
public partial class Page2
{
    public Page2(Player pl1, Player pl2)
    {
        gameTimer = new();
        InitializeComponent();

        _me = pl1;
        _companion = pl2;

        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth / 2;

        CanvasSetObjects();
        GameSetUp();
    }

    public void CanvasSetObjects()
    {
        // Переходы на карты.
        Canvas.SetLeft(TeleportToLocation1, 30);
        Canvas.SetTop(TeleportToLocation1, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocation1.Height));

        Canvas.SetLeft(TeleportToLocation3, SystemParameters.VirtualScreenWidth / 2 - TeleportToLocation3.Width - 30);
        Canvas.SetTop(TeleportToLocation3, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocation3.Height));


        // Ставим мольберты.
        Canvas.SetTop(picture2, 0.2 * (SystemParameters.VirtualScreenHeight - picture2.Height));
        Canvas.SetLeft(picture2, 0.33 * (SystemParameters.VirtualScreenWidth / 2 - picture2.Width));

        Canvas.SetTop(picture3, 0.2 * (SystemParameters.VirtualScreenHeight - picture3.Height));
        Canvas.SetLeft(picture3, 0.66 * (SystemParameters.VirtualScreenWidth / 2 - picture3.Width));

        Canvas.SetTop(picture4, 0.8 * (SystemParameters.VirtualScreenHeight - picture4.Height));
        Canvas.SetLeft(picture4, 0.2 * (SystemParameters.VirtualScreenWidth / 2 - picture4.Width));

        Canvas.SetTop(picture5, 0.8 * (SystemParameters.VirtualScreenHeight - picture5.Height));
        Canvas.SetLeft(picture5, 0.5 * (SystemParameters.VirtualScreenWidth / 2 - picture5.Width));

        Canvas.SetTop(picture6, 0.8 * (SystemParameters.VirtualScreenHeight - picture6.Height));
        Canvas.SetLeft(picture6, 0.8 * (SystemParameters.VirtualScreenWidth / 2 - picture6.Width));
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
    }

    #region Механика игры
    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            _isUpKeyPressed = true;
            // _isPlayerMovingUpward = true;
        }

        if (e.Key == Key.A)
        {
            _isLeftKeyPressed = true;
            // _isPlayerMovingLeftward = true;
            Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
        }

        if (e.Key == Key.D)
        {
            _isRightKeyPressed = true;
            // _isPlayerMovingRightward = true;
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);
        }

        if (e.Key == Key.S)
        {
            _isDownKeyPressed = true;
            // _isPlayerMovingDownward = true;
        }

        if (e.Key == Key.F)
            _isForceButtonClicked = true;

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

        pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

        foreach (var obj in MyCanvas.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && pacmanHitBox.IntersectsWith(hitBox))
            {
                if (obj.Name == "TeleportToLocation1")
                    NavigationService?.Navigate(TeleportTo(Location.Location1));
                
                if (obj.Name == "TeleportToLocation3")
                {
                    _me.TeleportateTo(Location.Location3);
                    NavigationService?.Navigate(new Page3(_me, _companion));
                }
            }
            if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                NavigationService?.Navigate(new Page3(_me, _companion));
            }

            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingUpward)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + 15);
                _isPossibleUpwardMovement = false;
                _me.SpeedY = 0;
                _me.IsMovingUpward = false;
            }

            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingLeftward)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + 20);
                _isPossibleLeftwardMovement = false;
                _me.SpeedX = 0;
                _me.IsMovingLeftward = false;
            }

            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingRightward)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - 20);
                _isPossibleRightwardMovement = false;
                _me.SpeedX = 0;
                _me.IsMovingRightward = false;
            }

            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingDownward)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - 15);
                _isPossibleDownwardMovement = false;
                _me.SpeedY = 0;
                _me.IsMovingDownward = false;
            }
        }

    }

    private void GameLoop(object sender, EventArgs e)
    {
        if (_me is null) throw new ArgumentNullException("_me is null!");

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
    #endregion

    private void But2_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        _me.TeleportateTo(Location.Location1);
        NavigationService.Navigate(new Page1(_me, _companion));
    }
}