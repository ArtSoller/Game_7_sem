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

/// <summary>
/// Логика взаимодействия для Page1.xaml
/// </summary>
public partial class PageLocation2_2 : Room
{
    public PageLocation2_2(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();

        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;

        CanvasSetObjects();
        GameSetUp();
    }

    private void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player2, _me.X);
        Canvas.SetTop(Player2, _me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion3_2, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion3_2.Height));
        Canvas.SetLeft(TeleportToLocaltion3_2, SystemParameters.VirtualScreenWidth - TeleportToLocaltion3_2.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

        // Ставим мольберты.
        Canvas.SetTop(picture1, 0.5 * (SystemParameters.VirtualScreenHeight - picture1.Height));
        Canvas.SetLeft(picture1, 0.5 * (SystemParameters.VirtualScreenWidth - picture1.Width));

        // Ставим область вокруг мольбертов.
        Canvas.SetTop(AreaEasel1, 0.5 * (SystemParameters.VirtualScreenHeight - AreaEasel1.Height) - 15);
        Canvas.SetLeft(AreaEasel1, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel1.Width) - 12);

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

    protected override void GameSetUp()
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");

        Location2_2.Focus();
        base.GameSetUp();
        Player2.Fill = MyImage;
    }

    private void SetMovementPossibility()
    {
        if (_toDisplay)
        {
            if (_me is null) throw new ArgumentException("_me is null");
            if (_companion is null) throw new ArgumentException("_companion is null");

            _isPossibleUpwardMovement = Canvas.GetTop(Player2) > wallTop.Height;
            _isPossibleLeftwardMovement = Canvas.GetLeft(Player2) > wallLeft.Width;
            _isPossibleRightwardMovement = Canvas.GetLeft(Player2) + Player2.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
            _isPossibleDownwardMovement = Canvas.GetTop(Player2) + Player2.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

            pacmanHitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);

            foreach (var obj in Location2_2.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
            {
                Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                if ((string)obj.Tag == "teleport" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
                {
                    _toDisplay = false;
                    NavigationService?.Navigate(TeleportTo(Location.Location3_2));
                }


                if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                {
                    //mediaPlayer.Play();
                    NavigationService?.Navigate(new Page8(_me, _companion));
                }

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
    }

    protected override void GameLoop(object sender, EventArgs e)
    {
        SetMovementsStatus();

        SetMovementPossibility();

        base.GameLoop(sender, e);

        Canvas.SetLeft(Player2, _me.X + _me.SpeedX);
        Canvas.SetTop(Player2, _me.Y - _me.SpeedY);
    }
}

