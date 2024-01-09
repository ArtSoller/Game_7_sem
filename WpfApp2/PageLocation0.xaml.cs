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
using System.Xml.Linq;

namespace WpfApp2;

public partial class PageLocation0 : Room
{
    //public static string[] spritePathsUp = { "sptirte_1_1.png", "sptirte_1_2.png", "sptirte_1_3.png", "sptirte_1_4.png", "sptirte_1_5.png", "sptirte_1_6.png", "sptirte_1_7.png", "sptirte_1_8.png" };
    public static string[] spritePathsUp = { "sptirte_2_1.png", "sptirte_2_2.png", "sptirte_2_3.png", "sptirte_2_4.png", "sptirte_2_5.png", "sptirte_2_6.png", "sptirte_2_7.png", "sptirte_2_8.png" };

    public static int currentSpriteIndex = 0;
    public PageLocation0(Player pl1, Player pl2)
    {
        InitializeComponent();
        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;
        if (Game.isGameDone == false)
        {
            TeleportToLocaltion1_ForPlayer1.Fill = Game.redBrush;
            TeleportToLocaltion1_ForPlayer2.Fill = Game.blueBrush;
        }
        else
        {
            TeleportToLocaltion1_ForPlayer1.Fill = Game.defaultBrush;
            TeleportToLocaltion1_ForPlayer2.Fill = Game.defaultBrush;
        }

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\ChestOpened.mp3"));
        code.Text = Game.parts_code;
        first_part_code.Text = Game.first_part_code;
        second_part_code.Text = Game.second_part_code;
        third_part_code.Text = Game.third_part_code;
        fourth_part_code.Text = Game.fourth_part_code;



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
        Canvas.SetLeft(ChestArea, 0.6 * (SystemParameters.VirtualScreenWidth - ChestArea.Width));

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion1_ForPlayer1, SystemParameters.VirtualScreenHeight * 0.2);
        Canvas.SetLeft(TeleportToLocaltion1_ForPlayer1, SystemParameters.VirtualScreenWidth - TeleportToLocaltion1_ForPlayer1.Width - 10);

        Canvas.SetTop(TeleportToLocaltion1_ForPlayer2, SystemParameters.VirtualScreenHeight * 0.65);
        Canvas.SetLeft(TeleportToLocaltion1_ForPlayer2, SystemParameters.VirtualScreenWidth - TeleportToLocaltion1_ForPlayer2.Width - 10);

        Canvas.SetTop(TeleportToLocaltionBack_1, SystemParameters.VirtualScreenHeight * 0.2);

        Canvas.SetTop(TeleportToLocaltionBack_2, SystemParameters.VirtualScreenHeight * 0.65);

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

        Location0.Focus();
        base.GameSetUp();

        ImageBrush MyImage = new()
        {
            ImageSource = new BitmapImage(new Uri(spritePathsUp[currentSpriteIndex], UriKind.Relative))
        };
        currentSpriteIndex = (currentSpriteIndex + 1) % spritePathsUp.Length;

        Player1.Fill = MyImage;
    }
    #endregion

    //void StopPlayerAnimation()
    //{
    //    if (gameTimer != null)
    //    {
    //        gameTimer.Stop();
    //        gameTimer = null;
    //        currentSpriteIndex = 0;
    //        playerSprite.Source = new BitmapImage(new Uri("pacman.png", UriKind.Relative)); // Устанавливаем начальный спрайт
    //    }
    //}

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            _isUpKeyPressed = true;
            _me.IsMovingUpward = true;
            ImageBrush MyImage = new()
            {
                ImageSource = new BitmapImage(new Uri(spritePathsUp[currentSpriteIndex], UriKind.Relative))
            };
            currentSpriteIndex = (currentSpriteIndex + 1) % spritePathsUp.Length;
            Player1.Fill = MyImage;
        }

        if (e.Key == Key.A)
        {
            _isLeftKeyPressed = true;
            _me.IsMovingLeftward = true;
            Player1.RenderTransform = new ScaleTransform(-1, 1);
            ImageBrush MyImage = new()
            {
                ImageSource = new BitmapImage(new Uri(spritePathsUp[currentSpriteIndex], UriKind.Relative))
            };
            currentSpriteIndex = (currentSpriteIndex + 1) % spritePathsUp.Length;
            Player1.Fill = MyImage;
        }

        if (e.Key == Key.D)
        {
            _isRightKeyPressed = true;
            _me.IsMovingRightward = true;
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);
            ImageBrush MyImage = new()
            {
                ImageSource = new BitmapImage(new Uri(spritePathsUp[currentSpriteIndex], UriKind.Relative))
            };
            currentSpriteIndex = (currentSpriteIndex + 1) % spritePathsUp.Length;
            Player1.Fill = MyImage;
        }

        if (e.Key == Key.S)
        {
            _isDownKeyPressed = true;
            _me.IsMovingDownward = true;
            ImageBrush MyImage = new()
            {
                ImageSource = new BitmapImage(new Uri(spritePathsUp[currentSpriteIndex], UriKind.Relative))
            };
            currentSpriteIndex = (currentSpriteIndex + 1) % spritePathsUp.Length;
            Player1.Fill = MyImage;
        }

        if (e.Key == Key.F)
        {
            _isForceButtonClicked = true;
        }

        if (e.Key == Key.Escape)
            GameOver("Dead");
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

        foreach (var obj in Location0.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "chest" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "chestArea")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion1_ForPlayer1" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox) && _me.Role == Role.Performer)
                {
                    Game.isQuestDone = false;
                    NavigationService?.Navigate(TeleportTo(Location.Location1_1));
                    _toDisplay = false;
                    Game.isQuestDone = false;
                }

                if ((string)obj.Tag == "teleport" && obj.Name == "TeleportToLocaltion1_ForPlayer2" && IsTeleportActive && pacmanHitBox.IntersectsWith(hitBox) && _me.Role == Role.Assistant)
                {
                    Game.isQuestDone = false;
                    NavigationService?.Navigate(TeleportTo(Location.Location1_2));
                    _toDisplay = false;
                }

                if ((string)obj.Tag == "chestArea" && pacmanHitBox.IntersectsWith(hitBox) && _isForceButtonClicked)

                {
                    mediaPlayer.Play();
                    NavigationService?.Navigate(new Page8(Game.Me, Game.Companion));
                }
                
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

        if (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Performer)
            Player1.RenderTransform = new RotateTransform(180, Player1.Width / 2, Player1.Height / 2);
        else if (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Assistant)
            Player2.RenderTransform = new RotateTransform(180, Player2.Width / 2, Player2.Height / 2);
        else if (Game.Me.IsMovingRightward && Game.Me.Role == Role.Performer)
            Player1.RenderTransform = new RotateTransform(0, Player1.Width / 2, Player1.Height / 2);
        else if (Game.Me.IsMovingRightward && Game.Me.Role == Role.Assistant)
            Player2.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2);

        base.GameLoop(sender, e);

        Canvas.SetLeft(Player1, Game.Me.Role == Role.Performer ? Game.Me.X : Game.Companion.X);
        Canvas.SetTop(Player1, Game.Me.Role == Role.Performer ? Game.Me.Y : Game.Companion.Y);

        Canvas.SetLeft(Player2, Game.Me.Role == Role.Assistant ? Game.Me.X : Game.Companion.X);
        Canvas.SetTop(Player2, Game.Me.Role == Role.Assistant ? Game.Me.Y : Game.Companion.Y);

        //if (Game.Me.Role == Role.Performer)
        //{
        //    Canvas.SetLeft(Player1, Game.Me.X);
        //}
        //if (Game.Me.Role == Role.Assistant)
        //{
        //    Canvas.SetLeft(Player2, Game.Me.X);
        //}

        Tb1.Text = Game.Me.Role.ToString();
        Tb3.Text = Game.Me.X.ToString();
        Tb4.Text = Game.Me.Y.ToString();
        Tb7.Text = Game.Companion.X.ToString();
        Tb8.Text = Game.Companion.Y.ToString();
    }
}
