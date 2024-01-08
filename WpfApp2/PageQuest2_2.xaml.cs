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
        Canvas.SetTop(Back, 0.7 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.4 * (SystemParameters.VirtualScreenWidth - Back.Width));

        Canvas.SetTop(Reset, 0.7 * (SystemParameters.VirtualScreenHeight - Reset.Height));
        Canvas.SetLeft(Reset, 0.5 * (SystemParameters.VirtualScreenWidth - Reset.Width));

        Canvas.SetTop(Enter, 0.7 * (SystemParameters.VirtualScreenHeight - Enter.Height));
        Canvas.SetLeft(Enter, 0.6 * (SystemParameters.VirtualScreenWidth - Enter.Width) - 40);

        Canvas.SetTop(txtScore, 0.6 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(txtScore, 0.46 * SystemParameters.VirtualScreenWidth);

        Canvas.SetTop(txtInput1, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput1.Height));
        Canvas.SetLeft(txtInput1, 0.405 * (SystemParameters.VirtualScreenWidth - txtInput1.Width));

        Canvas.SetTop(txtInput2, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput2.Height));
        Canvas.SetLeft(txtInput2, 0.43 * (SystemParameters.VirtualScreenWidth - txtInput2.Width));

        Canvas.SetTop(txtInput3, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput3.Height));
        Canvas.SetLeft(txtInput3, 0.455 * (SystemParameters.VirtualScreenWidth - txtInput3.Width));

        Canvas.SetTop(txtInput4, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput4.Height));
        Canvas.SetLeft(txtInput4, 0.480 * (SystemParameters.VirtualScreenWidth - txtInput4.Width));

        Canvas.SetTop(txtInput5, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput5.Height));
        Canvas.SetLeft(txtInput5, 0.505 * (SystemParameters.VirtualScreenWidth - txtInput5.Width));

        Canvas.SetTop(txtInput6, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput6.Height));
        Canvas.SetLeft(txtInput6, 0.530 * (SystemParameters.VirtualScreenWidth - txtInput6.Width));

        Canvas.SetTop(txtInput7, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput7.Height));
        Canvas.SetLeft(txtInput7, 0.555 * (SystemParameters.VirtualScreenWidth - txtInput7.Width));

        Canvas.SetTop(txtInput8, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput8.Height));
        Canvas.SetLeft(txtInput8, 0.585 * (SystemParameters.VirtualScreenWidth - txtInput8.Width));
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
            Game.isQuestDone = true;
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