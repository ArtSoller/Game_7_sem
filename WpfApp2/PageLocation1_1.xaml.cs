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
public partial class PageLocation1_1 : Room
{
    public PageLocation1_1(Player pl1, Player pl2) : base(pl1, pl2) 
    {
        InitializeComponent();

        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;
        
        CanvasSetObjects();
        GameSetUp();
    }

    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player1, _me.X);
        Canvas.SetTop(Player1, _me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion2_1, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion2_1.Height));
        Canvas.SetLeft(TeleportToLocaltion2_1, SystemParameters.VirtualScreenWidth - TeleportToLocaltion2_1.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

        // Ставим карты
        Canvas.SetTop(picture1, 0.1 * (SystemParameters.VirtualScreenHeight - picture1.Height));
        Canvas.SetLeft(picture1, 0.8 * (SystemParameters.VirtualScreenWidth - picture1.Width));

        Canvas.SetTop(picture2, 0.1 * (SystemParameters.VirtualScreenHeight - picture2.Height));
        Canvas.SetLeft(picture2, 0.5 * (SystemParameters.VirtualScreenWidth - picture2.Width));

        Canvas.SetTop(picture3, 0.1 * (SystemParameters.VirtualScreenHeight - picture3.Height));
        Canvas.SetLeft(picture3, 0.2 * (SystemParameters.VirtualScreenWidth - picture3.Width));

        Canvas.SetTop(picture4, 0.9 * (SystemParameters.VirtualScreenHeight - picture4.Height));
        Canvas.SetLeft(picture4, 0.8 * (SystemParameters.VirtualScreenWidth - picture4.Width));

        Canvas.SetTop(picture5, 0.9 * (SystemParameters.VirtualScreenHeight - picture5.Height));
        Canvas.SetLeft(picture5, 0.5 * (SystemParameters.VirtualScreenWidth - picture5.Width));

        Canvas.SetTop(picture6, 0.9 * (SystemParameters.VirtualScreenHeight - picture6.Height));
        Canvas.SetLeft(picture6, 0.2 * (SystemParameters.VirtualScreenWidth - picture6.Width));

        // Ставим область вокруг мольбертов.
        Canvas.SetTop(AreaEasel1, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel1.Height));
        Canvas.SetLeft(AreaEasel1, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel1.Width));

        Canvas.SetTop(AreaEasel2, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel2.Height));
        Canvas.SetLeft(AreaEasel2, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel2.Width));

        Canvas.SetTop(AreaEasel3, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel3.Height));
        Canvas.SetLeft(AreaEasel3, 0.2 * (SystemParameters.VirtualScreenWidth - AreaEasel3.Width));

        Canvas.SetTop(AreaEasel4, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel4.Height));
        Canvas.SetLeft(AreaEasel4, 0.8 * (SystemParameters.VirtualScreenWidth - AreaEasel4.Width));

        Canvas.SetTop(AreaEasel5, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel5.Height));
        Canvas.SetLeft(AreaEasel5, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel5.Width));

        Canvas.SetTop(AreaEasel6, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height));
        Canvas.SetLeft(AreaEasel6, 0.2 * (SystemParameters.VirtualScreenWidth - AreaEasel6.Width));

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
        Location1_1.Focus();
        base.GameSetUp();
        Player1.Fill = MyImage;
    }

    protected override void SetMovementPossibility()
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        _isPossibleUpwardMovement = Canvas.GetTop(Player1) > wallTop.Height;
        _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > wallLeft.Width;
        _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + Player1.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
        _isPossibleDownwardMovement = Canvas.GetTop(Player1) + Player1.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

        pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

        foreach (var obj in Location1_1.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
            {
                _toDisplay = false;
                NavigationService?.Navigate(TeleportTo(Location.Location2_1));
            }

            if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new Page8(_me, _companion));
                
            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox))
            {
                _me.X -= 1.1 * _me.SpeedX;
                _me.Y += 1.1 * _me.SpeedY;
            }
        }

    }

    protected override void GameLoop(object sender, EventArgs e)
    {
        if (!_toDisplay)
            return;

        SetMovementsStatus();

        SetMovementPossibility();

        if (_me.IsMovingLeftward)
            Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
        else if (_me.IsMovingRightward)
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);

        base.GameLoop(sender, e);

        Canvas.SetLeft(Player1, _me.X + _me.SpeedX);
        Canvas.SetTop(Player1, _me.Y - _me.SpeedY);
    }
}

