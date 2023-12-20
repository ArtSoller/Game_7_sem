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
public partial class Page1 : Room
{
    protected DispatcherTimer? gameTimer;

    protected bool _isUpKeyPressed, _isDownKeyPressed, _isLeftKeyPressed, _isRightKeyPressed;

    protected const float _friction = 0.88F, _speed = 0.8F;

    protected float _speedX, _speedY;

    protected bool _isPossibleUpwardMovement, _isPossibleDownwardMovement,
                 _isPossibleLeftwardMovement, _isPossibleRightwardMovement;

    protected bool _isPlayerMovingUpward, _isPlayerMovingLeftward,
                 _isPlayerMovingRightward, _isPlayerMovingDownward;

    protected Rect pacmanHitBox;

    public Page1()
    {
        gameTimer = new();
        InitializeComponent();

        // Перенести в OpeningWindow
        _companion = new("Buga guga", Role.Assistant);

        _me.X = Canvas.GetLeft(Player1);
        _me.Y = Canvas.GetTop(Player1);

        _companion.X = Canvas.GetLeft(Player2);
        _companion.Y = Canvas.GetTop(Player2);
        GameSetUp();
    }

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

        if (e.Key == Key.Escape)
            GameOver("Dead");
    }

    private void CanvasKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.W) _isUpKeyPressed = false;

        if (e.Key == Key.A) _isLeftKeyPressed = false;

        if (e.Key == Key.D) _isRightKeyPressed = false;
        
        if (e.Key == Key.S) _isDownKeyPressed = false;
    }

    private void SetMovementPossibility()
    {
        _isPossibleUpwardMovement = Canvas.GetTop(Player1) > 70;
        _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > 19;
        _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + 30 < Application.Current.MainWindow.ActualWidth - 39;
        _isPossibleDownwardMovement = Canvas.GetTop(Player1) + 30 < Application.Current.MainWindow.ActualHeight - 70;

        // asssign the pac man hit box to the pac man rectangle
        pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

        foreach (var obj in MyCanvas.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && pacmanHitBox.IntersectsWith(hitBox))
            {
                _me.TeleportateTo(Location.Location2);
                NavigationService.Navigate(new Page2());
            }

            // check if we are colliding with the wall while moving up if true then stop the pac man movement
            if (pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingUpward)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + 15);
                _isPossibleUpwardMovement = false;
                _speedY = 0;
                _isPlayerMovingUpward = false;
            }

            // check if we are colliding with the wall while moving left if true then stop the pac man movement
            if (pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingLeftward)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + 20);
                _isPossibleLeftwardMovement = false;
                _speedX = 0;
                _isPlayerMovingLeftward = false;
            }

            // check if we are colliding with the wall while moving right if true then stop the pac man movement
            if (pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingRightward)
            {
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - 20);
                _isPossibleRightwardMovement = false;
                _speedX = 0;
                _isPlayerMovingRightward = false;
            }

            // check if we are colliding with the wall while moving down if true then stop the pac man movement
            if (pacmanHitBox.IntersectsWith(hitBox) && _isPlayerMovingDownward)
            {
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - 15);
                _isPossibleDownwardMovement = false;
                _speedY = 0;
                _isPlayerMovingDownward = false;
            }
        }

    }

    private void SetMovementsStatus()
    {
        if (Math.Abs(_speedX) > 1e-1F)
        {
            if (_speedX > 0)
            {
                _isPlayerMovingRightward = true;
                _isPlayerMovingLeftward = false;
            }
            else if (_speedX < 0)
            {
                _isPlayerMovingLeftward = true;
                _isPlayerMovingRightward = false;
            }
        }
        else
        {
            _isPlayerMovingLeftward = false;
            _isPlayerMovingRightward = false;
        }

        if (Math.Abs(_speedY) > 1e-1F)
        {
            if (_speedY < 0)
            {
                _isPlayerMovingDownward = true;
                _isPlayerMovingUpward = false;
            }
            else if (_speedY > 0)
            {
                _isPlayerMovingUpward = true;
                _isPlayerMovingDownward = false;
            }
        }
        else
        {
            _isPlayerMovingUpward = false;
            _isPlayerMovingDownward = false;
        }
    }

    private void GameLoop(object sender, EventArgs e)
    {
        SetMovementsStatus();

        SetMovementPossibility();

        if (_isUpKeyPressed && _isPossibleUpwardMovement) _speedY += _speed;
        else if (!_isPossibleUpwardMovement && _isPlayerMovingUpward) _speedY = 0;

        if (_isLeftKeyPressed && _isPossibleLeftwardMovement) _speedX -= _speed;
        else if (!_isPossibleLeftwardMovement && _isPlayerMovingLeftward) _speedX = 0;

        if (_isRightKeyPressed && _isPossibleRightwardMovement) _speedX += _speed;
        else if (!_isPossibleRightwardMovement && _isPlayerMovingRightward) _speedX = 0;

        if (_isDownKeyPressed && _isPossibleDownwardMovement) _speedY -= _speed;
        else if (!_isPossibleDownwardMovement && _isPlayerMovingDownward) _speedY = 0;


        _speedX *= _friction;
        _speedY *= _friction;

        Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + _speedX);
        Canvas.SetTop(Player1, Canvas.GetTop(Player1) - _speedY);

        _me.X += _speedX;
        _me.Y -= _speedY;

        Tb1.Text = _isPlayerMovingUpward.ToString();
        Tb2.Text = _isPlayerMovingLeftward.ToString();
        Tb3.Text = _isPlayerMovingRightward.ToString();
        Tb4.Text = _isPlayerMovingDownward.ToString();
        Tb5.Text = _speedX.ToString();
        Tb6.Text = _speedY.ToString();
        Tb7.Text = Canvas.GetLeft(Player1).ToString();
        Tb8.Text = Canvas.GetTop(Player1).ToString();
        Tb9.Text = _me.X.ToString();
        Tb10.Text = _me.Y.ToString();

        

    }
    #endregion

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

    private void Rectangle1_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        var left = Canvas.GetLeft(Player1);
        if (e.Key == Key.F && left > 0)
        {
            // Открываем страницу для прямоугольника 1
            // NavigationService.Navigate(new Page2());
            // Page2 page = new Page2();
            // this.Content = page;
            NavigationService.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }
    }

    private void GameOver(string message)
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");
        // inside the game over function we passing in a string to show the final message to the game
        gameTimer.Stop(); // stop the game timer
        MessageBox.Show(message, "The Pac Man Game WPF MOO ICT"); // show a mesage box with the message that is passed in this function
        // when the player clicks ok on the message box
        // restart the application
        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        Application.Current.Shutdown();
    }

    private void But1_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new Page2());
    }
}
