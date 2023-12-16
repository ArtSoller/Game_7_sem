using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2;

public class Room : Page
{
    protected DispatcherTimer gameTimer; // create a new instance of the dispatcher timer called game timer
    protected bool goLeft, goRight, goDown, goUp; // 4 boolean created to move player in 4 direction
    protected bool noLeft, noRight, noDown, noUp; // 4 more boolean created to stop player moving in that direction
    protected float speed = 6.2f, speedX, speedY, Friction = 0.53f; // player speed
    protected Rect pacmanHitBox; // player hit box, this will be used to check for collision between player to walls, ghost and coints
    protected int ghostSpeed = 10; // ghost image speed
    protected int ghostMoveStep = 160; // ghost step limits
    protected int currentGhostStep; // current movement limit for the ghosts
    protected string score; // score keeping integer
    protected Pacman myPacman;


    protected void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        // this is the key down event
        if (e.Key == Key.Left && noLeft == false)
        {
            // if the left key is down and the boolean noLeft is set to false
            goRight = goUp = goDown = false; // set rest of the direction booleans to false
            noRight = noUp = noDown = false; // set rest of the restriction boolean to false
            goLeft = true; // set go left true
            myPacman.RenderTransform = new RotateTransform(-180, myPacman.Position.Width / 2, myPacman.Position.Height / 2); // rotate the pac man image to face left
        }
        if (e.Key == Key.Right && noRight == false)
        {
            // if the right key pressed and no right boolean is false
            noLeft = noUp = noDown = false; // set rest of the direction boolean to false
            goLeft = goUp = goDown = false; // set rest of the restriction boolean to false
            goRight = true; // set go right to true
            myPacman.RenderTransform = new RotateTransform(0, myPacman.Position.Width / 2, myPacman.Position.Height / 2); // rotate the pac man image to face right
        }
        if (e.Key == Key.Up && noUp == false)
        {
            // if the up key is pressed and no up is set to false
            noRight = noDown = noLeft = false; // set rest of the direction boolean to false
            goRight = goDown = goLeft = false; // set rest of the restriction boolean to false
            goUp = true; // set go up to true
            myPacman.RenderTransform = new RotateTransform(-90, myPacman.Position.Width / 2, myPacman.Position.Height / 2); // rotate the pac man character to face up
        }
        if (e.Key == Key.Down && noDown == false)
        {
            // if the down key is press and the no down boolean is false
            noUp = noLeft = noRight = false; // set rest of the direction boolean to false
            goUp = goLeft = goRight = false; // set rest of the restriction boolean to false
            goDown = true; // set go down to true
            myPacman.RenderTransform = new RotateTransform(90, myPacman.Position.Width / 2, myPacman.Position.Height / 2); // rotate the pac man character to face down
        }
        var left = Canvas.GetLeft(myPacman);
        var top = Canvas.GetTop(myPacman);
        if (e.Key == Key.F && top > 422 && top < 601 && left > 107 && left < 286)
        {
            // Открываем страницу для прямоугольника 1
            NavigationService.Navigate(new Page3());
        }
    }

    protected void CanvasKeyUp(object sender, KeyEventArgs e)
    {
        // this is the key down event
        if (e.Key == Key.Left && noLeft == false)
        {
            // if the left key is down and the boolean noLeft is set to false
            goLeft = false;
        }
        if (e.Key == Key.Right && noRight == false)
        {
            // if the right key pressed and no right boolean is false
            goRight = false; // set go right to true
        }
        if (e.Key == Key.Up && noUp == false)
        {
            // if the up key is pressed and no up is set to false
            goUp = false; // set go up to true
        }
        if (e.Key == Key.Down && noDown == false)
        {
            // if the down key is press and the no down boolean is false
            // set go down to true
            goDown = false;
        }
    }
}
