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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page6.xaml
/// </summary>
public partial class PageQuest2_2
{
    public string InputText { get; set; }
    private Brush? _brush;
    public PageQuest2_2(Player pl1, Player pl2)
    {
        InitializeComponent();
        txtScore.Visibility = Visibility.Hidden;
        txtInput2.IsReadOnly = true;
        txtInput4.IsReadOnly = true;
        txtInput6.IsReadOnly = true;
        txtInput8.IsReadOnly = true;
        txtInput2.Text = "+(";
        txtInput4.Text = "-";
        txtInput6.Text = ")*";
        txtInput8.Text = "=10";
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        _me = pl1;
        _companion = pl2;

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\BookClosed.mp3"));

        CanvasSetObjects();
    }

    private void CanvasSetObjects()
    {
        Canvas.SetTop(Back, 0.6 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.5 * (SystemParameters.VirtualScreenWidth - Back.Width));


        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));
        Back.Foreground = _brush;

    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Play();
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");
        NavigationService.Navigate(new PageLocation2_2(_me, _companion));
    }
    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        string inputValue1 = txtInput1.Text; // Получаем значение из текстового поля
        string inputValue2 = txtInput3.Text; // Получаем значение из текстового поля
        string inputValue3 = txtInput5.Text; // Получаем значение из текстового поля
        string inputValue4 = txtInput7.Text; // Получаем значение из текстового поля


        if (inputValue1 == "4" && inputValue2 == "4" && inputValue3 == "2" && inputValue4 == "3")
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            txtInput3.IsReadOnly = true;
            txtInput5.IsReadOnly = true;
            txtInput7.IsReadOnly = true;
            Game.second_part_code += Game.randomString[1];
            IsTeleportActive = true;
            Enter.Visibility = Visibility.Collapsed;
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            txtInput3.IsReadOnly = true;
            txtInput5.IsReadOnly = true;
            txtInput7.IsReadOnly = true;
        }
    }
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        txtInput1.Text = ""; // Получаем значение из текстового поля
        txtInput3.Text = ""; // Получаем значение из текстового поля
        txtInput5.Text = ""; // Получаем значение из текстового поля
        txtInput7.Text = ""; // Получаем значение из текстового поля
        txtInput1.IsReadOnly = false;
        txtInput3.IsReadOnly = false;
        txtInput5.IsReadOnly = false;
        txtInput7.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

}