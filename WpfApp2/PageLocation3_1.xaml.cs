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
public partial class PageLocation3_1
{
    private MediaPlayer mediaPlayer = new();

    public PageLocation3_1(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();

        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;



            mediaPlayer = new();
            mediaPlayer.MediaFailed += FailedMusic;
            mediaPlayer.Open(new Uri(System.IO.Path.GetFullPath("../../../snd/BookOpened.mp3")));

            code.IsReadOnly = true;
            code.Text = Game.parts_code;
            first_part_code.Text = Game.first_part_code;
            second_part_code.Text = Game.second_part_code;
            third_part_code.Text = Game.third_part_code;
            fourth_part_code.Text = Game.fourth_part_code;
            IsTeleportActive = true;

            if (IsTeleportActive == true)
                TeleportToLocaltion4_1.Fill = Game.redBrush;
            CanvasSetObjects();
            GameSetUp();
        }

    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Performer, Game.Me.X);
        Canvas.SetTop(Performer, Game.Me.Y);

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion4_1, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltion4_1.Height));
        Canvas.SetLeft(TeleportToLocaltion4_1, SystemParameters.VirtualScreenWidth - TeleportToLocaltion4_1.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack, 0.5 * (SystemParameters.VirtualScreenHeight - TeleportToLocaltionBack.Height));
        Canvas.SetLeft(TeleportToLocaltionBack, SystemParameters.VirtualScreenWidth - TeleportToLocaltionBack.Width - 1880);

        // Ставим мольберты.
        Canvas.SetTop(QuestField, 0.5 * (SystemParameters.VirtualScreenHeight - QuestField.Height));
        Canvas.SetLeft(QuestField, 0.5 * (SystemParameters.VirtualScreenWidth - QuestField.Width));

            // Ставим область вокруг мольбертов.
            //Canvas.SetTop(QuestArea1, 0.5 * (SystemParameters.VirtualScreenHeight - QuestArea1.Height) - 15);
            //Canvas.SetLeft(QuestArea1, 0.5 * (SystemParameters.VirtualScreenWidth - QuestArea1.Width) - 12);

            Canvas.SetTop(Book, 0.2 * (SystemParameters.VirtualScreenHeight - Book.Height));
            Canvas.SetLeft(Book, 0.2 * SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(BookArea, 0.22 * (SystemParameters.VirtualScreenHeight - BookArea.Height));
            Canvas.SetLeft(BookArea, 0.27 *SystemParameters.VirtualScreenWidth);

            Canvas.SetTop(QuestField, 0.07 * (SystemParameters.VirtualScreenHeight - Book.Height));
            Canvas.SetLeft(QuestField, 0.2 * SystemParameters.VirtualScreenWidth);
            QuestField.Height = 0.87 * SystemParameters.VirtualScreenHeight;
            QuestField.Width = 0.5 * SystemParameters.VirtualScreenWidth;

            //Canvas.SetTop(QuestArea, 0.07 * (SystemParameters.VirtualScreenHeight - Book.Height));
            //Canvas.SetLeft(QuestArea, 0.2 * SystemParameters.VirtualScreenWidth);
            //QuestArea.Height = 0.87 * SystemParameters.VirtualScreenHeight;
            //QuestArea.Width = 0.5 * SystemParameters.VirtualScreenWidth;


            Canvas.SetTop(Plate, 50);
            Canvas.SetLeft(Plate, SystemParameters.VirtualScreenWidth - Plate.Width - 12);


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

        Location3_1.Focus();
        base.GameSetUp();
        Performer.Fill = MyImage;
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

        foreach (var obj in Location3_1.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "papirus" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "papirus_area")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

                    if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion4_1" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        _toDisplay = false;
                        NavigationService?.Navigate(TeleportTo(Location.Location4_1));
                        IsTeleportActive = false;
                    }


            if ((string)obj.Tag == "papirus_area" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)
                NavigationService?.Navigate(new PageQuest3_1(Game.Me, Game.Companion));
            
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
            Performer.RenderTransform = new RotateTransform(180, Performer.Width / 2, Performer.Height / 2);
        else if (Game.Me.IsMovingRightward)
            Performer.RenderTransform = new RotateTransform(0, Performer.Width / 2, Performer.Height / 2);

        base.GameLoop(sender, e);

        ImageBrush MyImage1 = new()
        {
            ImageSource = new BitmapImage(new Uri(pathtemplate + spritePaths1[currentSpriteIndex_1], UriKind.Relative))
        };
        if (Game.Me.IsMovingRightward && Game.Me.Role == Role.Performer)
        {
            Performer.RenderTransform = new RotateTransform(0, Performer.Width / 2, Performer.Height / 2);
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        }
        if (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Performer)
        {
            Performer.RenderTransform = new ScaleTransform(-1, 1);
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        }
        if (Game.Me.IsMovingUpward && Game.Me.Role == Role.Performer)
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        if (Game.Me.IsMovingDownward && Game.Me.Role == Role.Performer)
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        Performer.Fill = MyImage1;

        Canvas.SetLeft(Performer, Game.Me.X + Game.Me.SpeedX);
        Canvas.SetTop(Performer, Game.Me.Y - Game.Me.SpeedY);
    }
}

