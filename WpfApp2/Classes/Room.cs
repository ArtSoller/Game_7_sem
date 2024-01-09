using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2;

public abstract class Room : Page
{
    protected DispatcherTimer? gameTimer;

    protected bool _isUpKeyPressed = false, _isDownKeyPressed = false,
                   _isLeftKeyPressed = false, _isRightKeyPressed = false,
                   _isForceButtonClicked = false;

    protected bool _toDisplay = true;

    public bool IsTeleportActive = false;

    public double _friction = 0.88F, _speed = 1.5F;

    protected bool _isPossibleUpwardMovement = false, _isPossibleDownwardMovement = false,
                   _isPossibleLeftwardMovement = false, _isPossibleRightwardMovement = false;
  
    protected Rect pacmanHitBox;

    protected ImageBrush MyImage, MyImagE;
    protected string pathtemplate = System.IO.Path.GetFullPath("../../../img/");
    public static string[] spritePaths1 = { "sptirte_1_1.png", "sptirte_1_2.png", "sptirte_1_3.png", "sptirte_1_4.png", "sptirte_1_5.png", "sptirte_1_6.png", "sptirte_1_7.png", "sptirte_1_8.png" };
    public static string[] spritePaths2 = { "sptirte_2_1.png", "sptirte_2_2.png", "sptirte_2_3.png", "sptirte_2_4.png", "sptirte_2_5.png", "sptirte_2_6.png", "sptirte_2_7.png", "sptirte_2_8.png" };
    protected int currentSpriteIndex_1 = 0, currentSpriteIndex_2 = 0;

    protected Room(Player pl1, Player pl2)
    {
        gameTimer = new();
        _toDisplay = true;

        //Game.Me = pl1;
        //Game.Companion = pl2;

        //MyImage = new()
        //{
        //    ImageSource = new BitmapImage(new Uri("D:\\CodeRepos\\CS\\NewGame\\Game_7_sem\\WpfApp2\\img\\pacman.png"))
        //};

        //mediaPlayer = new();
        //mediaPlayer.MediaFailed += FailedMusic;
    }

    protected Room()
    {
        gameTimer = new();
        _toDisplay = true;

        //Game.Me = pl1;
        //Game.Companion = pl2;

        //MyImage = new()
        //{
        //    ImageSource = new BitmapImage(new Uri("D:\\CodeRepos\\CS\\NewGame\\Game_7_sem\\WpfApp2\\img\\pacman.png"))
        //};

        //mediaPlayer = new();
        //mediaPlayer.MediaFailed += FailedMusic;
    }

    protected void FailedMusic(object obj, EventArgs arg)
    {
        throw new Exception("Music has been dead");
    }

    protected virtual void GameOver(string message)
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

    protected virtual void GameSetUp()
    {        
        gameTimer.Interval = TimeSpan.FromMilliseconds(10);
        gameTimer.Tick += GameLoop;
        gameTimer.Start();
    }

    protected void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            _isUpKeyPressed = true;
            Game.Me.IsMovingUpward = true;
        }

        if (e.Key == Key.A)
        {
            _isLeftKeyPressed = true;
            Game.Me.IsMovingLeftward = true;
        }

        if (e.Key == Key.D)
        {
            _isRightKeyPressed = true;
            Game.Me.IsMovingRightward = true;
        }

        if (e.Key == Key.S)
        {
            _isDownKeyPressed = true;
            Game.Me.IsMovingDownward = true;
        }

        if (e.Key == Key.F)
            _isForceButtonClicked = true;

        if (e.Key == Key.Escape)
            GameOver("Dead");
    }

    protected void CanvasKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            _isUpKeyPressed = false;
            //CheckKey();
        }        

        if (e.Key == Key.A) _isLeftKeyPressed = false;

        if (e.Key == Key.D) _isRightKeyPressed = false;

        if (e.Key == Key.S) _isDownKeyPressed = false;

        if (e.Key == Key.F) _isForceButtonClicked = false;

    }


    protected Page TeleportTo(Location location)
    {
        if (Game.Me is null) throw new ArgumentNullException("Game.Me is null");

        if (Game.Companion is null) throw new ArgumentNullException("Game.Companion is null");

        Game.Me.TeleportateTo(location);

        _isDownKeyPressed = false;
        _isUpKeyPressed = false;
        _isLeftKeyPressed = false;
        _isRightKeyPressed = false;
        
        return location switch
        {
            Location.Location0   => new PageLocation0(Game.Me, Game.Companion),
            Location.Location1_1 => new PageLocation1_1(Game.Me, Game.Companion),
            Location.Location1_2 => new PageLocation1_2(Game.Me, Game.Companion),
            Location.Location2_1 => new PageLocation2_1(Game.Me, Game.Companion),
            Location.Location2_2 => new PageLocation2_2(Game.Me, Game.Companion),
            Location.Location3_1 => new PageLocation3_1(Game.Me, Game.Companion),
            Location.Location3_2 => new PageLocation3_2(Game.Me, Game.Companion),

            _ => throw new NotSupportedException("Kuda zalez, pridurok?")
        };
    }

    protected virtual void GameLoop(object sender, EventArgs e)
    {
        if (!_toDisplay)
            return;

        if (_isUpKeyPressed && Game.Me._isPossibleUpwardMovement) Game.Me.SpeedY += _speed;
        else if (!Game.Me._isPossibleUpwardMovement && Game.Me.IsMovingUpward) Game.Me.SpeedY = 0;

        if (_isLeftKeyPressed && Game.Me._isPossibleLeftwardMovement) Game.Me.SpeedX -= _speed;
        else if (!Game.Me._isPossibleLeftwardMovement && Game.Me.IsMovingLeftward) Game.Me.SpeedX = 0;

        if (_isRightKeyPressed && Game.Me._isPossibleRightwardMovement) Game.Me.SpeedX += _speed;
        else if (!Game.Me._isPossibleRightwardMovement && Game.Me.IsMovingRightward) Game.Me.SpeedX = 0;

        if (_isDownKeyPressed && Game.Me._isPossibleDownwardMovement) Game.Me.SpeedY -= _speed;
        else if (!Game.Me._isPossibleDownwardMovement && Game.Me.IsMovingDownward) Game.Me.SpeedY = 0;

        Game.Me.SpeedX *= _friction;
        Game.Me.SpeedY *= _friction;

        Game.Me.X += Game.Me.SpeedX;
        Game.Me.Y -= Game.Me.SpeedY;

        MyImage = new()
        {
            ImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../img/sptirte_1_1.png")))
        };

        MyImagE = new()
        {
            ImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../../img/sptirte_2_1.png")))
        };

        Connection.SendCoordinates(Game.Me.Name, Game.Me.X, Game.Me.Y, Game.Me.IsMovingLeftward, Game.Me.IsMovingRightward, Game.Me.IsMovingUpward, Game.Me.IsMovingDownward, Game.first_part_code, Game.second_part_code, Game.third_part_code);
    }

    protected abstract void SetMovementPossibility();

    protected abstract void CanvasSetObjects();

    protected void SetMovementsStatus()
    {
        if (Math.Abs(Game.Me.SpeedX) > 1e-1F)
        {
            if (Game.Me.SpeedX > 0)
            {
                Game.Me.IsMovingRightward= true;
                Game.Me.IsMovingLeftward = false;
            }
            else if (Game.Me.SpeedX < 0)
            {
                Game.Me.IsMovingLeftward = true;
                Game.Me.IsMovingRightward = false;
            }
        }
        else
        {
            Game.Me.IsMovingLeftward = false;
            Game.Me.IsMovingRightward = false;
        }

        if (Math.Abs(Game.Me.SpeedY) > 1e-1F)
        {
            if (Game.Me.SpeedY < 0)
            {
                Game.Me.IsMovingDownward = true;
                Game.Me.IsMovingUpward = false;
            }
            else if (Game.Me.SpeedY > 0)
            {
                Game.Me.IsMovingUpward = true;
                Game.Me.IsMovingDownward = false;
            }
        }
        else
        {
            Game.Me.IsMovingUpward = false;
            Game.Me.IsMovingDownward = false;
        }
    }
}
 