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
                   _isLeftKeyPressed = false, _isRightKeyPressed = false;

    protected const float _friction = 0.88F, _speed = 1.5F;

    protected bool _isPossibleUpwardMovement, _isPossibleDownwardMovement,
                   _isPossibleLeftwardMovement, _isPossibleRightwardMovement;

    protected bool _isPlayerMovingUpward, _isPlayerMovingLeftward,
                   _isPlayerMovingRightward, _isPlayerMovingDownward,
                   _isForceButtonClicked;

    protected Rect pacmanHitBox;

    protected Player? _me;

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
        if (e.Key == Key.W) _isUpKeyPressed = false;

        if (e.Key == Key.A) _isLeftKeyPressed = false;

        if (e.Key == Key.D) _isRightKeyPressed = false;

        if (e.Key == Key.S) _isDownKeyPressed = false;

        if (e.Key == Key.F) _isForceButtonClicked = false;
    }

    protected Page TeleportTo(Location location)
    {
        if (_me is null) throw new ArgumentNullException("_me is null");

        if (_companion is null) throw new ArgumentNullException("_companion is null");

        _me.TeleportateTo(location);

        //_isDownKeyPressed = false;
        //_isUpKeyPressed = false;
        //_isLeftKeyPressed = false;
        //_isRightKeyPressed = false;

        return location switch
        {
            Location.Location1 => new Page1(_me, _companion),
            Location.Location2 => new Page2(_me, _companion),
            _ => throw new NotSupportedException("Kuda zalez, pridurok?")
        };
    }

    protected void SetMovementsStatus()
    {
        if (Math.Abs(_me.SpeedX) > 1e-1F)
        {
            if (_me.SpeedX > 0)
            {
                _isPlayerMovingRightward = true;
                _isPlayerMovingLeftward = false;
            }
            else if (_me.SpeedX < 0)
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

        if (Math.Abs(_me.SpeedY) > 1e-1F)
        {
            if (_me.SpeedY < 0)
            {
                _isPlayerMovingDownward = true;
                _isPlayerMovingUpward = false;
            }
            else if (_me.SpeedY > 0)
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
}
