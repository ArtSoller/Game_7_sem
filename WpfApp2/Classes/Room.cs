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


    
    protected MediaPlayer? mediaPlayer;

    protected bool _isUpKeyPressed = false, _isDownKeyPressed = false,
                   _isLeftKeyPressed = false, _isRightKeyPressed = false,
                   _isForceButtonClicked = false;

    protected bool _toDisplay = true;

    public static bool IsTeleportActive = true;

    public double _friction = 0.88F, _speed = 1.5F;

    protected bool _isPossibleUpwardMovement = false, _isPossibleDownwardMovement = false,
                   _isPossibleLeftwardMovement = false, _isPossibleRightwardMovement = false;
    /*
    protected bool _isPlayerMovingUpward = false, _isPlayerMovingLeftward = false,
                   _isPlayerMovingRightward = false, _isPlayerMovingDownward = false,
                   _isForceButtonClicked = false;
    */
    protected Rect pacmanHitBox;

    protected Player? _me;

    protected void FailedMusic(object obj, EventArgs arg)
    {
        throw new Exception("Music has been dead");
    }

    protected Player? _companion;

    protected void GameOver(string message)
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
    //protected void CheckKey()
    //{
    //    if (_isUpKeyPressed)
    //    {
    //        CanvasKeyDown();
    //    }

    //    if (e.Key == Key.A) _isLeftKeyPressed = false;

    //    if (e.Key == Key.D) _isRightKeyPressed = false;

    //    if (e.Key == Key.S) _isDownKeyPressed = false;

    //    if (e.Key == Key.F) _isForceButtonClicked = false;

    //}


    protected Page TeleportTo(Location location)
    {
        if (_me is null) throw new ArgumentNullException("_me is null");

        if (_companion is null) throw new ArgumentNullException("_companion is null");

        _me.TeleportateTo(location);

        _isDownKeyPressed = false;
        _isUpKeyPressed = false;
        _isLeftKeyPressed = false;
        _isRightKeyPressed = false;
        
        return location switch
        {
            Location.Location0   => new PageLocation0(_me, _companion),
            Location.Location1_1 => new PageLocation1_1(_me, _companion),
            Location.Location1_2 => new PageLocation1_2(_me, _companion),
            Location.Location2_1 => new PageLocation2_1(_me, _companion),
            Location.Location2_2 => new PageLocation2_2(_me, _companion),
            Location.Location3_1 => new PageLocation3_1(_me, _companion),
            Location.Location3_2 => new PageLocation3_2(_me, _companion),
            Location.Location4_1 => new PageLocation4_1(_me, _companion),
            Location.Location4_2 => new PageLocation4_2(_me, _companion),
            _ => throw new NotSupportedException("Kuda zalez, pridurok?")
        };
    }

    protected void SetMovementsStatus()
    {
        if (Math.Abs(_me.SpeedX) > 1e-1F)
        {
            if (_me.SpeedX > 0)
            {
                _me.IsMovingRightward= true;
                _me.IsMovingLeftward = false;
            }
            else if (_me.SpeedX < 0)
            {
                _me.IsMovingLeftward = true;
                _me.IsMovingRightward = false;
                
            }
        }
        else
        {
            _me.IsMovingLeftward = false;
            _me.IsMovingRightward = false;
        }

        if (Math.Abs(_me.SpeedY) > 1e-1F)
        {
            if (_me.SpeedY < 0)
            {
                _me.IsMovingDownward = true;
                _me.IsMovingUpward = false;
            }
            else if (_me.SpeedY > 0)
            {
                _me.IsMovingUpward = true;
                _me.IsMovingDownward = false;
            }
        }
        else
        {
            _me.IsMovingUpward = false;
            _me.IsMovingDownward = false;
        }
    }
}
 