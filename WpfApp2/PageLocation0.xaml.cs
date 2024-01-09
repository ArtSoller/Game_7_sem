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
/// Логика взаимодействия для BeginingPage.xaml
/// </summary>
public partial class PageLocation0 : Room
{
    //public static string[] spritePathsUp = { "sptirte_1_1.png", "sptirte_1_2.png", "sptirte_1_3.png", "sptirte_1_4.png", "sptirte_1_5.png", "sptirte_1_6.png", "sptirte_1_7.png", "sptirte_1_8.png" };
    public static string[] spritePathsUp = { "sptirte_2_1.png", "sptirte_2_2.png", "sptirte_2_3.png", "sptirte_2_4.png", "sptirte_2_5.png", "sptirte_2_6.png", "sptirte_2_7.png", "sptirte_2_8.png" };

    public static int currentSpriteIndex = 0;
    public PageLocation0(Player pl1, Player pl2)
    {
        gameTimer = new();
        InitializeComponent();

        _toDisplay = true;
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
        _me = pl1;
        _companion = pl2;
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
    private void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Player1, _me.X);
        Canvas.SetTop(Player1, _me.Y);

        Canvas.SetLeft(Player2, _companion.X);
        Canvas.SetTop(Player2, _companion.Y);

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

    private void GameSetUp()
    {

        if (gameTimer is null) throw new Exception("gameTimer is null");

        Location0.Focus();
        gameTimer.Interval = TimeSpan.FromMilliseconds(10);
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

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

    private void SetMovementPossibility()
    {
        if (_toDisplay)
        {
            if (_me is null) throw new ArgumentException("_me is null");
            if (_companion is null) throw new ArgumentException("_companion is null");

            _isPossibleUpwardMovement = Canvas.GetTop(Player1) > wallTop.Height;
            _isPossibleLeftwardMovement = Canvas.GetLeft(Player1) > wallLeft.Width;
            _isPossibleRightwardMovement = Canvas.GetLeft(Player1) + Player1.Width < SystemParameters.VirtualScreenWidth - wallRight.Width;
            _isPossibleDownwardMovement = Canvas.GetTop(Player1) + Player1.Height < SystemParameters.VirtualScreenHeight - wallBottom.Height;

            pacmanHitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);

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
                    NavigationService?.Navigate(new SunduckInteraction(_me, _companion));
                }

                if ((string)obj.Tag == "chest" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingUpward)
                {
                    _isPossibleUpwardMovement = false;
                    _me.SpeedY = 0;
                    _me.Y = Canvas.GetTop(obj) + obj.Height + 30;
                    _me.IsMovingUpward = false;
                }

                if ((string)obj.Tag == "chest" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingLeftward)
                {
                    _isPossibleLeftwardMovement = false;
                    _me.SpeedX = 0;
                    _me.X = Canvas.GetLeft(obj) + obj.Width + 30;
                    _me.IsMovingLeftward = false;
                }

                if ((string)obj.Tag == "chest" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingRightward)
                {
                    _isPossibleRightwardMovement = false;
                    _me.SpeedX = 0;
                    _me.X = Canvas.GetLeft(obj) - 0.5 * obj.Width;
                    _me.IsMovingRightward = false;
                }

                if ((string)obj.Tag == "chest" && pacmanHitBox.IntersectsWith(hitBox) && _me.IsMovingDownward)
                {
                    _me.Y = Canvas.GetTop(obj) - 0.5 * obj.Height - 30;
                    _isPossibleDownwardMovement = false;
                    _me.SpeedY = 0;
                    _me.IsMovingDownward = false;
                }
            }
        }
    }

    private void GameLoop(object sender, EventArgs e)
    {
        SetMovementsStatus();

        SetMovementPossibility();

        if (_isUpKeyPressed && _isPossibleUpwardMovement) _me.SpeedY += _speed;
        else if (!_isPossibleUpwardMovement && _me.IsMovingUpward) _me.SpeedY = 0;

        if (_isLeftKeyPressed && _isPossibleLeftwardMovement) _me.SpeedX -= _speed;
        else if (!_isPossibleLeftwardMovement && _me.IsMovingLeftward) _me.SpeedX = 0;

        if (_isRightKeyPressed && _isPossibleRightwardMovement) _me.SpeedX += _speed;
        else if (!_isPossibleRightwardMovement && _me.IsMovingRightward) _me.SpeedX = 0;

        if (_isDownKeyPressed && _isPossibleDownwardMovement) _me.SpeedY -= _speed;
        else if (!_isPossibleDownwardMovement && _me.IsMovingDownward) _me.SpeedY = 0;


        _me.SpeedX *= _friction;
        _me.SpeedY *= _friction;

        // Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + _speedX);
        // Canvas.SetTop(Player1, Canvas.GetTop(Player1) - _speedY);

        Canvas.SetLeft(Player1, _me.X + _me.SpeedX);
        Canvas.SetTop(Player1, _me.Y - _me.SpeedY);

        Tb1.Text = _me.Role.ToString();

        _me.X += _me.SpeedX;
        _me.Y -= _me.SpeedY;
    }
}
