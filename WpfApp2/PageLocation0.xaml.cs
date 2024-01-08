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

namespace WpfApp2;

public partial class PageLocation0 : Room
{
    public PageLocation0(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;
        CanvasSetObjects();
        GameSetUp();
    }

    #region Настройка объектов
    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player1, Game.Me.X);
        Canvas.SetTop(Player1, Game.Me.Y);

        Canvas.SetLeft(Player2, Game.Companion.X);
        Canvas.SetTop(Player2, Game.Companion.Y);

        Canvas.SetTop(chest, 0.5 * (SystemParameters.VirtualScreenHeight - chest.Height));
        Canvas.SetLeft(chest, 0.5 * (SystemParameters.VirtualScreenWidth - chest.Width));

        Canvas.SetTop(ChestArea, 0.5 * (SystemParameters.VirtualScreenHeight - ChestArea.Height));
        Canvas.SetLeft(ChestArea, 0.5 * (SystemParameters.VirtualScreenWidth - ChestArea.Width));

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion1_ForPlayer1, SystemParameters.VirtualScreenHeight * 0.2);
        Canvas.SetLeft(TeleportToLocaltion1_ForPlayer1, SystemParameters.VirtualScreenWidth - TeleportToLocaltion1_ForPlayer1.Width - 10);

        Canvas.SetTop(TeleportToLocaltion1_ForPlayer2, SystemParameters.VirtualScreenHeight * 0.65);
        Canvas.SetLeft(TeleportToLocaltion1_ForPlayer2, SystemParameters.VirtualScreenWidth - TeleportToLocaltion1_ForPlayer2.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack_1, SystemParameters.VirtualScreenHeight * 0.2);
        Canvas.SetLeft(TeleportToLocaltionBack_1, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack_1.Width - 1875);

        Canvas.SetTop(TeleportToLocaltionBack_2, SystemParameters.VirtualScreenHeight * 0.65);
        Canvas.SetLeft(TeleportToLocaltionBack_2, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack_2.Width - 1875);
    }

    protected override void GameSetUp()
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");

        Location0.Focus();
        base.GameSetUp();

        Player1.Fill = MyImage;
    }
    #endregion

    protected override void SetMovementPossibility()
    {
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");

        _isPossibleUpwardMovement = Canvas.GetTop(Player1) > wallTop.Height;
        _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > wallLeft.Width;
        _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + Player1.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
        _isPossibleDownwardMovement = Canvas.GetTop(Player1) + Player1.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

        pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

        foreach (var obj in Location0.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "chest" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "chestArea")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion1_ForPlayer1" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox) && Game.Me.Role == Role.Performer)
            {
                NavigationService?.Navigate(TeleportTo(Location.Location1_1));
                _toDisplay = false;
            }

            if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion1_ForPlayer2" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox) && Game.Me.Role == Role.Assistant)
            {
                NavigationService?.Navigate(TeleportTo(Location.Location1_2));
                _toDisplay = false;
            }

            if ((string)obj.Tag == "chestArea" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new Page8(Game.Me, Game.Companion));
                
            if ((string)obj.Tag == "chest" && pacmanHitBox.IntersectsWith(hitBox))
            {
                Game.Me.X -= 1.1 * Game.Me.SpeedX;
                Game.Me.Y += 1.1 * Game.Me.SpeedY;
            }
        }
    }

    protected override void GameLoop(object sender, EventArgs e)
    {
        if (!_toDisplay)
            return;

        SetMovementsStatus();

        SetMovementPossibility();

        if (Game.Me.IsMovingLeftward)
            Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
        else if (Game.Me.IsMovingRightward)
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);

        base.GameLoop(sender, e);

        Canvas.SetLeft(Player1, Game.Me.Role == Role.Performer ? Game.Me.X : Game.Companion.X);
        Canvas.SetTop(Player1, Game.Me.Role == Role.Performer ? Game.Me.Y : Game.Companion.Y);

        Canvas.SetLeft(Player2, Game.Me.Role == Role.Assistant ? Game.Me.X : Game.Companion.X);
        Canvas.SetTop(Player2, Game.Me.Role == Role.Assistant ? Game.Me.Y : Game.Companion.Y);

        Tb1.Text = Game.Me.Role.ToString();
        Tb3.Text = Game.Me.X.ToString();
        Tb4.Text = Game.Me.Y.ToString();
        Tb7.Text = Game.Companion.X.ToString();
        Tb8.Text = Game.Companion.Y.ToString();
    }
}
