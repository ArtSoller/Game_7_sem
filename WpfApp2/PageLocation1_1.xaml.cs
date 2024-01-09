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


            mediaPlayer = new();
            mediaPlayer.MediaFailed += FailedMusic;
            mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\PictureOpened.mp3"));

            code.IsReadOnly = true;
            code.Text = Game.parts_code;
            first_part_code.Text = Game.first_part_code;
            second_part_code.Text = Game.second_part_code;
            third_part_code.Text = Game.third_part_code;
            fourth_part_code.Text = Game.fourth_part_code;

            CanvasSetObjects();
            GameSetUp();
        }

    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player1, Game.Me.X);
        Canvas.SetTop(Player1, Game.Me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion2_1, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion2_1.Height));
        Canvas.SetLeft(TeleportToLocaltion2_1, SystemParameters.VirtualScreenWidth - TeleportToLocaltion2_1.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

            // Ставим карты
            Canvas.SetTop(picture1, 0.1 * (SystemParameters.VirtualScreenHeight - picture1.Height));
            Canvas.SetLeft(picture1, 0.2 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(picture2, 0.1 * (SystemParameters.VirtualScreenHeight - picture2.Height));
            Canvas.SetLeft(picture2, 0.5 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(picture3, 0.1 * (SystemParameters.VirtualScreenHeight - picture3.Height));
            Canvas.SetLeft(picture3, 0.8 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(picture4, 0.9 * (SystemParameters.VirtualScreenHeight - picture4.Height));
            Canvas.SetLeft(picture4, 0.2 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(picture5, 0.9 * (SystemParameters.VirtualScreenHeight - picture5.Height));
            Canvas.SetLeft(picture5, 0.5 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(picture6, 0.9 * (SystemParameters.VirtualScreenHeight - picture6.Height));
            Canvas.SetLeft(picture6, 0.8 * SystemParameters.VirtualScreenWidth);

            // Ставим область вокруг мольбертов.
            Canvas.SetTop(AreaEasel1, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel1.Height));
            Canvas.SetLeft(AreaEasel1, 0.15 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(AreaEasel2, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel2.Height));
            Canvas.SetLeft(AreaEasel2, 0.45 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(AreaEasel3, 0.1 * (SystemParameters.VirtualScreenHeight - AreaEasel3.Height));
            Canvas.SetLeft(AreaEasel3, 0.75 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(AreaEasel4, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel4.Height));
            Canvas.SetLeft(AreaEasel4, 0.16 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(AreaEasel5, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel5.Height));
            Canvas.SetLeft(AreaEasel5, 0.46 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(AreaEasel6, 0.9 * (SystemParameters.VirtualScreenHeight - AreaEasel6.Height));
            Canvas.SetLeft(AreaEasel6, 0.76 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(code, 0.96 * SystemParameters.VirtualScreenHeight);
            Canvas.SetLeft(code, 0.8 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(first_part_code, 0.96 * SystemParameters.VirtualScreenHeight);
            Canvas.SetLeft(first_part_code, 0.9 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(second_part_code, 0.96 * SystemParameters.VirtualScreenHeight);
            Canvas.SetLeft(second_part_code, 0.91 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(third_part_code, 0.96 * SystemParameters.VirtualScreenHeight);
            Canvas.SetLeft(third_part_code, 0.92 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(fourth_part_code, 0.96 * SystemParameters.VirtualScreenHeight);
            Canvas.SetLeft(fourth_part_code, 0.93 * SystemParameters.VirtualScreenWidth);

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
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");

        Game.Me._isPossibleUpwardMovement = Game.Me.Y > wallTop.Height;
        Game.Me._isPossibleLeftwardMovement = Game.Me.X > wallLeft.Width;
        Game.Me._isPossibleRightwardMovement = Game.Me.X + 50 < SystemParameters.VirtualScreenWidth - wallRight.Width;
        Game.Me._isPossibleDownwardMovement = Game.Me.Y + 50 < SystemParameters.VirtualScreenHeight - wallBottom.Height;

        pacmanHitBox = new Rect(Game.Me.X, Game.Me.Y, 50, 50);

        foreach (var obj in Location1_1.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                    if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion2_1" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        _toDisplay = false;
                        NavigationService?.Navigate(TeleportTo(Location.Location2_1));
                    }


                    if ((string)obj.Name == "AreaEasel1" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        mediaPlayer.Play();
                        NavigationService?.Navigate(new PageQuest1_1_1_picture(_me, _companion));
                    }

                    if ((string)obj.Name == "AreaEasel2" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        mediaPlayer.Play();
                        NavigationService?.Navigate(new PageQuest1_1_2_picture(_me, _companion));
                    }

                    if ((string)obj.Name == "AreaEasel3" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        mediaPlayer.Play();
                        NavigationService?.Navigate(new PageQuest1_1_3_picture(_me, _companion));
                    }

                    if ((string)obj.Name == "AreaEasel4" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        mediaPlayer.Play();
                        NavigationService?.Navigate(new PageQuest1_1_4_picture(_me, _companion));
                    }

                    if ((string)obj.Name == "AreaEasel5" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        mediaPlayer.Play();
                        NavigationService?.Navigate(new PageQuest1_1_5_picture(_me, _companion));
                    }

                    if ((string)obj.Name == "AreaEasel6" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                    {
                        mediaPlayer.Play();
                        NavigationService?.Navigate(new PageQuest1_1_6_picture(_me, _companion));
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

        Canvas.SetLeft(Player1, Game.Me.X + Game.Me.SpeedX);
        Canvas.SetTop(Player1, Game.Me.Y - Game.Me.SpeedY);
    }
}

