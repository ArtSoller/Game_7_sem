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
public partial class PageQuest4_2_code
{
    private MediaPlayer mediaPlayer = new();

    public PageQuest4_2_code(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\BookOpened.mp3"));
        mediaPlayer.Play();

        CanvasSetObjects();
    }


    protected override void CanvasSetObjects()
    {
        Canvas.SetTop(quest_4_2, 0.1 * (SystemParameters.VirtualScreenWidth - quest_4_2.Width));
        Canvas.SetLeft(quest_4_2, 0.5 * (SystemParameters.VirtualScreenWidth - quest_4_2.Width));

        Canvas.SetTop(Back, 0.7 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.5 * (SystemParameters.VirtualScreenWidth - Back.Width));

    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\BookClosed.mp3"));
        mediaPlayer.Play();
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");
        NavigationService.Navigate(new PageLocation4_2(Game.Me, Game.Companion));
    }

}
