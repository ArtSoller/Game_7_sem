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

    protected bool _isUpKeyPressed, _isDownKeyPressed, _isLeftKeyPressed, _isRightKeyPressed;

    protected const float _friction = 0.88F, _speed = 1.5F;

    protected float _speedX, _speedY;

    protected bool _isPossibleUpwardMovement, _isPossibleDownwardMovement,
                   _isPossibleLeftwardMovement, _isPossibleRightwardMovement;

    protected bool _isPlayerMovingUpward, _isPlayerMovingLeftward,
                   _isPlayerMovingRightward, _isPlayerMovingDownward,
                   _isforceButtonClicked;

    protected Rect pacmanHitBox;

    protected Player? _me;// = new("Kaka puka", Role.Performer) { X = 0.1 * SystemParameters.VirtualScreenWidth, Y = 0.33 * SystemParameters.VirtualScreenHeight };

    protected Player? _companion;// = new("Buga guga", Role.Assistant) { X = 0.1 * SystemParameters.VirtualScreenWidth, Y = 0.66 * SystemParameters.VirtualScreenHeight };

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

        if (e.Key == Key.F) _isforceButtonClicked = false;
    }

    protected void SetMovementsStatus()
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
}
