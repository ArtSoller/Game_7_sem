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
using System.Windows.Threading;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page1.xaml
/// </summary>
public partial class PageLocation4_1
{
    private MediaPlayer mediaPlayer = new();

    public PageLocation4_1(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();

        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;



            mediaPlayer = new();
            mediaPlayer.MediaFailed += FailedMusic;

            code.IsReadOnly = true;
            code.Text = Game.parts_code;
            first_part_code.Text = Game.first_part_code;
            second_part_code.Text = Game.second_part_code;
            third_part_code.Text = Game.third_part_code;
            fourth_part_code.Text = Game.fourth_part_code;
            if (IsTeleportActive == true)
                TeleportToLocaltion0.Fill = Game.redBrush;
            CanvasSetObjects();
            GameSetUp();
        }

    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player1, Game.Me.X);
        Canvas.SetTop(Player1, Game.Me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion0, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion0.Height));
        Canvas.SetLeft(TeleportToLocaltion0, SystemParameters.VirtualScreenWidth - TeleportToLocaltion0.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

            // Ставим книгу и бумагу
            Canvas.SetTop(Book, 0.1 * (SystemParameters.VirtualScreenHeight - Book.Height));
            Canvas.SetLeft(Book, 0.5 * (SystemParameters.VirtualScreenWidth - Book.Width));

            Canvas.SetTop(Papirus, 0.9 * (SystemParameters.VirtualScreenHeight - Papirus.Height));
            Canvas.SetLeft(Papirus, 0.5 * (SystemParameters.VirtualScreenWidth - Papirus.Width));

            // Ставим область вокруг книги и бумаги
            Canvas.SetTop(AreaBook, 0.1 * (SystemParameters.VirtualScreenHeight - AreaBook.Height));
            Canvas.SetLeft(AreaBook, 0.5 * (SystemParameters.VirtualScreenWidth - AreaBook.Width));

            Canvas.SetTop(AreaPapirus, 0.9 * (SystemParameters.VirtualScreenHeight - AreaPapirus.Height));
            Canvas.SetLeft(AreaPapirus, 0.5 * (SystemParameters.VirtualScreenWidth - AreaPapirus.Width) );

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

        Location4_1.Focus();
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

        foreach (var obj in Location4_1.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "easel" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "easel_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                    if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion0" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        _toDisplay = false;
                        NavigationService?.Navigate(TeleportTo(Location.Location0));
                        Game.isGameDone = true;
                    }

            if ((string)obj.Tag == "book_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new PageQuest4_1_code(Game.Me, Game.Companion));

            if ((string)obj.Tag == "papirus_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new PageQuest4_1_terminal(Game.Me, Game.Companion));

            if (((string)obj.Tag == "book" || (string)obj.Tag == "papirus") && pacmanHitBox.IntersectsWith(hitBox))
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
            Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
        else if (Game.Me.IsMovingRightward)
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);


        base.GameLoop(sender, e);

        ImageBrush MyImage1 = new()
        {
            ImageSource = new BitmapImage(new Uri(pathtemplate + spritePaths1[currentSpriteIndex_1], UriKind.Relative))
        };
        if (Game.Me.IsMovingRightward && Game.Me.Role == Role.Performer)
        {
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        }
        if (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Performer)
        {
            Player1.RenderTransform = new ScaleTransform(-1, 1);
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        }
        if (Game.Me.IsMovingUpward && Game.Me.Role == Role.Performer)
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        if (Game.Me.IsMovingDownward && Game.Me.Role == Role.Performer)
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        Player1.Fill = MyImage1;

        Canvas.SetLeft(Player1, Game.Me.X + Game.Me.SpeedX);
        Canvas.SetTop(Player1, Game.Me.Y - Game.Me.SpeedY);
    }
}

