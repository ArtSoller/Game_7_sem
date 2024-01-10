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

public partial class PageLocation0
{
     private MediaPlayer mediaPlayer = new();

    public PageLocation0(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Performer.Visibility = Visibility.Visible;
        Assistant.Visibility = Visibility.Visible;
        Floor.Height = SystemParameters.VirtualScreenHeight;
        Floor.Width = SystemParameters.VirtualScreenWidth;
        if (Game.first_part_code=="")
        {
            TeleportToLocaltion1_ForPerformer.Fill = Game.redBrush;
            TeleportToLocaltion1_ForAssistant.Fill = Game.blueBrush;
            IsTeleportActive = true;
        }

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri(System.IO.Path.GetFullPath("../../../snd/ChestOpened.mp3")));
        code.Text = Game.parts_code;
        first_part_code.Text = Game.first_part_code;
        second_part_code.Text = Game.second_part_code;
        third_part_code.Text = Game.third_part_code;



        CanvasSetObjects();
        GameSetUp();
    }

    #region Настройка объектов
    protected override void CanvasSetObjects()
    {
        // Ставим игроков.
        Canvas.SetLeft(Performer, Game.Me.X);
        Canvas.SetTop(Performer, Game.Me.Y);

        Canvas.SetLeft(Assistant, Game.Companion.X);
        Canvas.SetTop(Assistant, Game.Companion.Y);

        Canvas.SetTop(chest, 0.5 * (SystemParameters.VirtualScreenHeight - chest.Height));
        Canvas.SetLeft(chest, 0.5 * (SystemParameters.VirtualScreenWidth - chest.Width));

        Canvas.SetTop(ChestArea, 0.5 * (SystemParameters.VirtualScreenHeight - ChestArea.Height));
        Canvas.SetLeft(ChestArea, 0.6 * (SystemParameters.VirtualScreenWidth - ChestArea.Width));

        // Переходы на карты.
        Canvas.SetTop(TeleportToLocaltion1_ForPerformer, SystemParameters.VirtualScreenHeight * 0.2);
        Canvas.SetLeft(TeleportToLocaltion1_ForPerformer, SystemParameters.VirtualScreenWidth - TeleportToLocaltion1_ForPerformer.Width - 10);

        Canvas.SetTop(TeleportToLocaltion1_ForAssistant, SystemParameters.VirtualScreenHeight * 0.65);
        Canvas.SetLeft(TeleportToLocaltion1_ForAssistant, SystemParameters.VirtualScreenWidth - TeleportToLocaltion1_ForAssistant.Width - 10);

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


    }

    protected override void GameSetUp()
    {

        if (gameTimer is null) throw new Exception("gameTimer is null");

        Location0.Focus();
        base.GameSetUp();
        Performer.Fill = MyImage;
        Assistant.Fill = MyImagE;

    }
    #endregion

    protected override void SetMovementPossibility()
    {
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");

        Game.Me._isPossibleUpwardMovement = Game.Me.Y > wallTop.Height;
        Game.Me._isPossibleLeftwardMovement = Game.Me.X > wallLeft.Width;
        Game.Me._isPossibleRightwardMovement = Game.Me.X + 50 < SystemParameters.VirtualScreenWidth - wallRight.Width;
        Game.Me._isPossibleDownwardMovement = Game.Me.Y + 50 < SystemParameters.VirtualScreenHeight - wallBottom.Height;

        var PerformerHitBox = new Rect(Game.Me.Role == Role.Performer ? Game.Me.X : Game.Companion.X, Game.Me.Role == Role.Performer ? Game.Me.Y : Game.Companion.Y, 50, 50);
        var AssistantHitBox = new Rect(Game.Me.Role == Role.Assistant ? Game.Me.X : Game.Companion.X, Game.Me.Role == Role.Assistant ? Game.Me.Y : Game.Companion.Y, 50, 50);


        foreach (var obj in Location0.Children.OfType<Rectangle>().Where(_obj => ((string)_obj.Tag == "chest" || (string)_obj.Tag == "teleport" || (string)_obj.Tag == "chestArea")))
        {
            Rect hitBox = new(Canvas.GetLeft(obj), Canvas.GetTop(obj), obj.Width, obj.Height);

            if (obj.Name == "TeleportToLocaltion1_ForPerformer" && IsTeleportActive && PerformerHitBox.IntersectsWith(hitBox))
            {
                if (Game.Me.Role == Role.Performer)
                {
                    NavigationService?.Navigate(TeleportTo(Location.Location1_1));
                    _toDisplay = false;
                }
                else
                {
                    Performer.Visibility = Visibility.Collapsed;
                }
            }

            if (obj.Name == "TeleportToLocaltion1_ForAssistant" && IsTeleportActive && AssistantHitBox.IntersectsWith(hitBox))
            {
                if (Game.Me.Role == Role.Assistant)
                {
                    IsTeleportActive = false;
                    NavigationService?.Navigate(TeleportTo(Location.Location1_2));
                    _toDisplay = false;
                }
                else
                {
                    Assistant.Visibility = Visibility.Collapsed;
                }
            }

            if ((string)obj.Tag == "chestArea" && (PerformerHitBox.IntersectsWith(hitBox) || AssistantHitBox.IntersectsWith(hitBox)) && _isForceButtonClicked)
            {
                mediaPlayer.Play();
                NavigationService?.Navigate(new SunduckInteraction(Game.Me, Game.Companion));
            }

            if ((string)obj.Tag == "chest" && PerformerHitBox.IntersectsWith(hitBox))
            {
                PerformerHitBox.X -= 1.1 * (Game.Me.Role == Role.Performer ? Game.Me.SpeedX : Game.Companion.SpeedX);
                PerformerHitBox.Y += 1.1 * (Game.Me.Role == Role.Performer ? Game.Me.SpeedY : Game.Companion.SpeedY);
            }

            if ((string)obj.Tag == "chest" && AssistantHitBox.IntersectsWith(hitBox))
            {
                AssistantHitBox.X -= 1.1 * (Game.Me.Role == Role.Assistant ? Game.Me.SpeedX : Game.Companion.SpeedX);
                AssistantHitBox.Y += 1.1 * (Game.Me.Role == Role.Assistant ? Game.Me.SpeedY : Game.Companion.SpeedY);
            }

            if (Game.Me.CurrentLocation == Location.Location0)
            {
                Game.Me.X = Game.Me.Role == Role.Performer ? PerformerHitBox.X : AssistantHitBox.X;
                Game.Me.Y = Game.Me.Role == Role.Performer ? PerformerHitBox.Y : AssistantHitBox.Y;

            }
            
            Game.Companion.X = Game.Companion.Role == Role.Performer ? PerformerHitBox.X : AssistantHitBox.X;
            Game.Companion.Y = Game.Companion.Role == Role.Performer ? PerformerHitBox.Y : AssistantHitBox.Y;
        }
    }

    protected override void GameLoop(object sender, EventArgs e)
    {
        if (!_toDisplay)
            return;

        SetMovementsStatus();

        SetMovementPossibility();

        if (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Performer)
            Performer.RenderTransform = new RotateTransform(180, Performer.Width/2, Performer.Height/2);
        else if (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Assistant)
            Assistant.RenderTransform = new RotateTransform(180, Assistant.Width / 2, Assistant.Height/2);
        else if (Game.Me.IsMovingRightward && Game.Me.Role == Role.Performer)
            Performer.RenderTransform = new RotateTransform(0, Performer.Width / 2, Performer.Height/2);
        else if (Game.Me.IsMovingRightward && Game.Me.Role == Role.Assistant)
            Assistant.RenderTransform = new RotateTransform(0, Assistant.Width / 2, Assistant.Height / 2);

        base.GameLoop(sender, e);
        ImageBrush MyImage1 = new()
        {
            ImageSource = new BitmapImage(new Uri(pathtemplate+spritePaths1[currentSpriteIndex_1], UriKind.Relative))
        };
        ImageBrush MyImage2 = new()
        {
            ImageSource = new BitmapImage(new Uri(pathtemplate + spritePaths2[currentSpriteIndex_2], UriKind.Relative))
        };
        if ((Game.Me.IsMovingRightward && Game.Me.Role == Role.Performer) || (Game.Companion.IsMovingRightward && Game.Companion.Role == Role.Performer))
        {
            Performer.RenderTransform = new RotateTransform(0, Performer.Width/2, Performer.Height/2);
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        }
        if ((Game.Companion.IsMovingRightward && Game.Companion.Role == Role.Assistant) || (Game.Me.IsMovingRightward && Game.Me.Role == Role.Assistant))
        {
            Assistant.RenderTransform = new RotateTransform(0, Assistant.Width / 2, Assistant.Height / 2);
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        }
        if ((Game.Me.IsMovingLeftward && Game.Me.Role == Role.Performer) || (Game.Companion.IsMovingLeftward && Game.Companion.Role == Role.Performer))
        {
            Performer.RenderTransform = new ScaleTransform(-1, 1);
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        }
        if ((Game.Companion.IsMovingLeftward && Game.Companion.Role == Role.Assistant) || (Game.Me.IsMovingLeftward && Game.Me.Role == Role.Assistant))
        {
            Assistant.RenderTransform = new ScaleTransform(-1, 1);
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        }
        if ((Game.Me.IsMovingUpward && Game.Me.Role == Role.Performer) || (Game.Companion.IsMovingUpward && Game.Companion.Role == Role.Performer))
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        if ((Game.Me.IsMovingUpward && Game.Me.Role == Role.Assistant) || (Game.Companion.IsMovingUpward && Game.Companion.Role == Role.Assistant))
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        if ((Game.Me.IsMovingDownward && Game.Me.Role == Role.Performer) || (Game.Companion.IsMovingDownward && Game.Companion.Role == Role.Performer))
            currentSpriteIndex_1 = (currentSpriteIndex_1 + 1) % spritePaths1.Length;
        if ((Game.Me.IsMovingDownward && Game.Me.Role == Role.Assistant) || (Game.Companion.IsMovingDownward && Game.Companion.Role == Role.Assistant))
            currentSpriteIndex_2 = (currentSpriteIndex_2 + 1) % spritePaths2.Length;
        Performer.Fill = MyImage1;
        Assistant.Fill = MyImage2;

        Canvas.SetLeft(Performer, Game.Me.Role == Role.Performer ? Game.Me.X : Game.Companion.X);
        Canvas.SetTop(Performer, Game.Me.Role == Role.Performer ? Game.Me.Y : Game.Companion.Y);

        Canvas.SetLeft(Assistant, Game.Me.Role == Role.Assistant ? Game.Me.X : Game.Companion.X);
        Canvas.SetTop(Assistant, Game.Me.Role == Role.Assistant ? Game.Me.Y : Game.Companion.Y);

        Tb1.Text = Game.Me.Role.ToString();
    }
}
