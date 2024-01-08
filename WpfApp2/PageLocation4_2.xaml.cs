﻿using System;
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
public partial class PageLocation4_2 : Room
{
    public PageLocation4_2(Player pl1, Player pl2) : base(pl1, pl2)
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
        Canvas.SetLeft(Player2, Game.Me.X);
        Canvas.SetTop(Player2, Game.Me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion0, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion0.Height));
        Canvas.SetLeft(TeleportToLocaltion0, SystemParameters.VirtualScreenWidth - TeleportToLocaltion0.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

        // Ставим книгу и бумагу
        Canvas.SetTop(picture1, 0.1 * (SystemParameters.VirtualScreenHeight - picture1.Height));
        Canvas.SetLeft(picture1, 0.5 * (SystemParameters.VirtualScreenWidth - picture1.Width));

        Canvas.SetTop(picture2, 0.9 * (SystemParameters.VirtualScreenHeight - picture2.Height));
        Canvas.SetLeft(picture2, 0.5 * (SystemParameters.VirtualScreenWidth - picture2.Width));

        // Ставим область вокруг книги и бумаги
        Canvas.SetTop(AreaEasel1, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel1.Height));
        Canvas.SetLeft(AreaEasel1, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel1.Width));

        Canvas.SetTop(AreaEasel2, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel2.Height));
        Canvas.SetLeft(AreaEasel2, 0.5 * (SystemParameters.VirtualScreenWidth - AreaEasel2.Width));

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

        Location4_2.Focus();
        base.GameSetUp();
        Player2.Fill = MyImage;
    }

    protected override void SetMovementPossibility()
    {
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");

        _isPossibleUpwardMovement = Canvas.GetTop(Player2) > wallTop.Height;
        _isPossibleLeftwardMovement = Canvas.GetLeft(Player2) > wallLeft.Width;
        _isPossibleRightwardMovement = Canvas.GetLeft(Player2) + Player2.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
        _isPossibleDownwardMovement = Canvas.GetTop(Player2) + Player2.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

        pacmanHitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);

        foreach (var obj in Location4_2.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
            {
                _toDisplay = false;
                NavigationService?.Navigate(TeleportTo(Location.Location0));
            }

            if ((string)obj.Tag == "easel_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new Page8(Game.Me, Game.Companion));

            if ((string)obj.Tag == "easel" && pacmanHitBox.IntersectsWith(hitBox) && Game.Me.IsMovingUpward)
            {
                Game.Me.X -= Game.Me.SpeedX;
                Game.Me.Y += Game.Me.SpeedY;
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
            Player2.RenderTransform = new RotateTransform(180, Player2.Width / 2, Player2.Height / 2);
        else if (Game.Me.IsMovingRightward)
            Player2.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2);

        base.GameLoop(sender, e);

        Canvas.SetLeft(Player2, Game.Me.X + Game.Me.SpeedX);
        Canvas.SetTop(Player2, Game.Me.Y - Game.Me.SpeedY);
    }
}