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
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page5.xaml
/// </summary>
public partial class PageQuest4_2_terminal
{
    private MediaPlayer mediaPlayer = new();

    public string InputText { get; set; }

    private Brush? _brush;

    public PageQuest4_2_terminal(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        txtScore.Visibility = Visibility.Hidden;
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;


        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\PapirusOpened.mp3"));
        mediaPlayer.Play();

        CanvasSetObjects();

    }

    protected override void CanvasSetObjects()
    {
        Canvas.SetTop(Back, 0.7 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.4 * (SystemParameters.VirtualScreenWidth - Back.Width));

        Canvas.SetTop(Reset, 0.7 * (SystemParameters.VirtualScreenHeight - Reset.Height));
        Canvas.SetLeft(Reset, 0.5 * (SystemParameters.VirtualScreenWidth - Reset.Width));

        Canvas.SetTop(Enter, 0.7 * (SystemParameters.VirtualScreenHeight - Enter.Height));
        Canvas.SetLeft(Enter, 0.6 * (SystemParameters.VirtualScreenWidth - Enter.Width) - 40);

        Canvas.SetTop(txtScore, 0.6 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(txtScore, 0.46 * SystemParameters.VirtualScreenWidth);

        Canvas.SetTop(txtInput, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput.Height));
        Canvas.SetLeft(txtInput, 0.5 * (SystemParameters.VirtualScreenWidth - txtInput.Width));

    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\PapirusOpened.mp3"));
        mediaPlayer.Play();
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");
        NavigationService.Navigate(new PageLocation3_1(Game.Me, Game.Companion));
    }
    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        string inputValue = txtInput.Text; // Получаем значение из текстового поля
        if (inputValue == "8903130627")
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
            Game.third_part_code += Game.QuestKeyString[3];
            IsTeleportActive = true;
            Enter.Visibility = Visibility.Collapsed;
            Game.isQuestDone = true;
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
        }
    }
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        txtInput.Text = ""; // Получаем значение из текстового поля
        txtInput.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }
}