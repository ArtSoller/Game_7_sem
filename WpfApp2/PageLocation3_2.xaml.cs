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
public partial class PageLocation3_2
{
    private MediaPlayer mediaPlayer = new();

    public PageLocation3_2(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();

        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;



            mediaPlayer = new();
            mediaPlayer.MediaFailed += FailedMusic;
            mediaPlayer.Open(new Uri(System.IO.Path.GetFullPath("../../../snd/PapirusOpened.mp3")));

            code.IsReadOnly = true;
            code.Text = Game.parts_code;
            first_part_code.Text = Game.first_part_code;
            second_part_code.Text = Game.second_part_code;
            third_part_code.Text = Game.third_part_code;
            fourth_part_code.Text = Game.fourth_part_code;
            IsTeleportActive = true;
            if (IsTeleportActive == true)
                TeleportToLocaltion4_2.Fill = Game.blueBrush;

            CanvasSetObjects();
            GameSetUp();
        }

    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Assistant, Game.Me.X);
        Canvas.SetTop(Assistant, Game.Me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion4_2, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion4_2.Height));
        Canvas.SetLeft(TeleportToLocaltion4_2, SystemParameters.VirtualScreenWidth - TeleportToLocaltion4_2.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

            // Ставим карты
            Canvas.SetTop(papirus1, 0.1 * (SystemParameters.VirtualScreenHeight - papirus1.Height));
            Canvas.SetLeft(papirus1, 0.8 * (SystemParameters.VirtualScreenWidth - papirus1.Width));

            Canvas.SetTop(papirus2, 0.1 * (SystemParameters.VirtualScreenHeight - papirus2.Height));
            Canvas.SetLeft(papirus2, 0.5 * (SystemParameters.VirtualScreenWidth - papirus2.Width));

            Canvas.SetTop(papirus3, 0.1 * (SystemParameters.VirtualScreenHeight - papirus3.Height));
            Canvas.SetLeft(papirus3, 0.2 * (SystemParameters.VirtualScreenWidth - papirus3.Width));

            Canvas.SetTop(papirus4, 0.9 * (SystemParameters.VirtualScreenHeight - papirus4.Height));
            Canvas.SetLeft(papirus4, 0.8 * (SystemParameters.VirtualScreenWidth - papirus4.Width));

            Canvas.SetTop(papirus5, 0.9 * (SystemParameters.VirtualScreenHeight - papirus5.Height));
            Canvas.SetLeft(papirus5, 0.5 * (SystemParameters.VirtualScreenWidth - papirus5.Width));

            Canvas.SetTop(papirus6, 0.9 * (SystemParameters.VirtualScreenHeight - papirus6.Height));
            Canvas.SetLeft(papirus6, 0.2 * (SystemParameters.VirtualScreenWidth - papirus6.Width));

            Canvas.SetTop(Panel, 0.5 * (SystemParameters.VirtualScreenHeight - Panel.Height));
            Canvas.SetLeft(Panel, 0.5 * (SystemParameters.VirtualScreenWidth - Panel.Width));

            // Ставим область вокруг мольбертов.
            Canvas.SetTop(AreaPapirus1, 0.1 * (SystemParameters.VirtualScreenHeight - AreaPapirus1.Height));
            Canvas.SetLeft(AreaPapirus1, 0.2 * (SystemParameters.VirtualScreenWidth - AreaPapirus1.Width));

            Canvas.SetTop(AreaPapirus2, 0.1 * (SystemParameters.VirtualScreenHeight - AreaPapirus2.Height));
            Canvas.SetLeft(AreaPapirus2, 0.5 * (SystemParameters.VirtualScreenWidth - AreaPapirus2.Width));

            Canvas.SetTop(AreaPapirus3, 0.1 * (SystemParameters.VirtualScreenHeight - AreaPapirus3.Height));
            Canvas.SetLeft(AreaPapirus3, 0.8 * (SystemParameters.VirtualScreenWidth - AreaPapirus3.Width));

            Canvas.SetTop(AreaPapirus4, 0.9 * (SystemParameters.VirtualScreenHeight - AreaPapirus4.Height));
            Canvas.SetLeft(AreaPapirus4, 0.2 * (SystemParameters.VirtualScreenWidth - AreaPapirus4.Width));

            Canvas.SetTop(AreaPapirus5, 0.9 * (SystemParameters.VirtualScreenHeight - AreaPapirus5.Height));
            Canvas.SetLeft(AreaPapirus5, 0.5 * (SystemParameters.VirtualScreenWidth - AreaPapirus5.Width));

            Canvas.SetTop(AreaPapirus6, 0.9 * (SystemParameters.VirtualScreenHeight - AreaPapirus6.Height));
            Canvas.SetLeft(AreaPapirus6, 0.8 * (SystemParameters.VirtualScreenWidth - AreaPapirus6.Width));

            Canvas.SetTop(AreaPanel, 0.5 * SystemParameters.VirtualScreenHeight);
            Canvas.SetLeft(AreaPanel, 0.5 * SystemParameters.VirtualScreenWidth);

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
        }

    protected override void GameSetUp()
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");

        Location3_2.Focus();
        base.GameSetUp();
        Assistant.Fill = MyImagE;
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

        foreach (var obj in Location3_2.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "papirus" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "papirus_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                    if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion4_2" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        _toDisplay = false;
                        NavigationService?.Navigate(TeleportTo(Location.Location4_2));
                        IsTeleportActive = false;

                    }



            if ((string)obj.Name == "AreaPapirus1" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new PageQuest3_2_1_way(Game.Me, Game.Companion));
            }

            if ((string)obj.Name == "AreaPapirus2" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new PageQuest3_2_2_way(Game.Me, Game.Companion));
            }

            if ((string)obj.Name == "AreaPapirus3" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new PageQuest3_2_3_way(Game.Me, Game.Companion));
            }

            if ((string)obj.Name == "AreaPapirus4" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new PageQuest3_2_4_way(Game.Me, Game.Companion));
            }

            if ((string)obj.Name == "AreaPapirus5" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new PageQuest3_2_5_way(Game.Me, Game.Companion));
            }

            if ((string)obj.Name == "AreaPapirus6" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new PageQuest3_2_6_way(Game.Me, Game.Companion));
            }

            if ((string)obj.Tag == "papirus" && pacmanHitBox.IntersectsWith(hitBox))
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
            Assistant.RenderTransform = new RotateTransform(180, Assistant.Width / 2, Assistant.Height / 2);
        else if (Game.Me.IsMovingRightward)
            Assistant.RenderTransform = new RotateTransform(0, Assistant.Width / 2, Assistant.Height / 2);

        base.GameLoop(sender, e);


        ImageBrush MyImage2 = new()
        {
            ImageSource = new BitmapImage(new Uri(pathtemplate + spritePaths2[currentSpriteIndex_2], UriKind.Relative))
        };
        if ((Game.Companion.IsMovingRightward && Game.Companion.Role == Role.Assistant) || (Game.Me.IsMovingRightward && Game.Me.Role == Role.Assistant))
        {
            Assistant.RenderTransform = new RotateTransform(0, Assistant.Width / 2, Assistant.Height / 2);
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        }
        if ((Game.Companion.IsMovingLeftward && Game.Companion.Role == Role.Assistant) || (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Assistant))
        {
            Assistant.RenderTransform = new ScaleTransform(-1, 1);
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        }
        if ((Game.Me.IsMovingUpward && Game.Me.Role == Role.Assistant) || (Game.Companion.IsMovingUpward && Game.Companion.Role == Role.Assistant))
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        if ((Game.Me.IsMovingDownward && Game.Me.Role == Role.Assistant) || (Game.Companion.IsMovingDownward && Game.Companion.Role == Role.Assistant))
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        Assistant.Fill = MyImage2;


        Canvas.SetLeft(Assistant, Game.Me.X + Game.Me.SpeedX);
        Canvas.SetTop(Assistant, Game.Me.Y - Game.Me.SpeedY);
    }
}

