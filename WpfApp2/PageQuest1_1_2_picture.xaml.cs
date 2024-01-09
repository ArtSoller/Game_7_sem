using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
/// Логика взаимодействия для Page4.xaml
/// </summary>
public partial class PageQuest1_1_2_picture
{
    private MediaPlayer mediaPlayer = new();
    public PageQuest1_1_2_picture(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;


        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("D:\\CodeRepos\\CS\\NewGame\\Game_7_sem\\WpfApp2\\snd\\PictureClosed.mp3"));

        CanvasSetObjects();
    }

    protected override void CanvasSetObjects()
    {
        Canvas.SetTop(quest_1, 0.1 * (SystemParameters.VirtualScreenWidth - quest_1.Width));
        Canvas.SetLeft(quest_1, 0.5 * (SystemParameters.VirtualScreenWidth - quest_1.Width));

        Canvas.SetTop(Back, 0.7 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.5 * (SystemParameters.VirtualScreenWidth - Back.Width));

    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Play();

        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");
        NavigationService.Navigate(new PageLocation1_1(Game.Me, Game.Companion));
    }
}
