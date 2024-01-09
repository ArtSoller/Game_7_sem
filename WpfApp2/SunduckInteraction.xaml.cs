using System;
using System.Collections.Generic;
using System.IO;
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
public partial class SunduckInteraction
{
    public string? InputText { get; set; }
    private MediaPlayer mediaPlayer = new();

    public SunduckInteraction(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;

        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\ChestOpened.mp3"));
        mediaPlayer.Play();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;
        gameTimer = new();


        txtScore.Visibility = Visibility.Hidden;
        txtInput5.IsReadOnly = true;
        count.IsReadOnly = true;
        txtInput5.Text = Game.AttemptsNumber.ToString();
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

        Canvas.SetTop(txtInput1, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput1.Height));
        Canvas.SetLeft(txtInput1, 0.465 * (SystemParameters.VirtualScreenWidth - txtInput1.Width));

        Canvas.SetTop(txtInput2, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput2.Height));
        Canvas.SetLeft(txtInput2, 0.49 * (SystemParameters.VirtualScreenWidth - txtInput2.Width));

        Canvas.SetTop(txtInput3, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput3.Height));
        Canvas.SetLeft(txtInput3, 0.515 * (SystemParameters.VirtualScreenWidth - txtInput3.Width));

        Canvas.SetTop(txtInput4, 0.3 * (SystemParameters.VirtualScreenHeight - txtInput4.Height));
        Canvas.SetLeft(txtInput4, 0.54 * (SystemParameters.VirtualScreenWidth - txtInput4.Width));

        Canvas.SetTop(count, 0.37 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(count, 0.42 * SystemParameters.VirtualScreenWidth);

        Canvas.SetTop(txtInput5, 0.37 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(txtInput5, 0.565 * SystemParameters.VirtualScreenWidth);
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\ChestClosed.mp3"));
        mediaPlayer.Play();
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");
        NavigationService.Navigate(new PageLocation0(Game.Me, Game.Companion));
    }

    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        var inputValue1 = txtInput1.Text; // Получаем значение из текстового поля
        var inputValue2 = txtInput2.Text; // Получаем значение из текстового поля
        var inputValue3 = txtInput3.Text; // Получаем значение из текстового поля
        var inputValue4 = txtInput4.Text; // Получаем значение из текстового поля


        if (inputValue1 == Game.QuestKeyString[0].ToString() && inputValue2 == Game.QuestKeyString[1].ToString() && inputValue3 == Game.QuestKeyString[2].ToString() && inputValue4 == Game.QuestKeyString[3].ToString())
        {
            mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\GameWon.mp3"));
            mediaPlayer.Play();
            Enter.Visibility = Visibility.Collapsed;
            GameOver("Won");
        }
        else
        {
            Game.AttemptsNumber -= 1;
            txtInput5.Text = Game.AttemptsNumber.ToString();
            if (Game.AttemptsNumber == 0)
                GameOver("Dead");
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
        }
    }

    protected override void GameOver(string message)
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");
        gameTimer.Stop();

        switch (message)
        {
            case "Dead":
                MessageBox.Show("You're " + message, "GAME OVER"); 
                //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                break;
            case "Won":
                MessageBox.Show("You've " + message, "GAME OVER");
                break;
        }
        Application.Current.Shutdown();
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        txtInput1.Text = "";
        txtInput2.Text = "";
        txtInput3.Text = "";
        txtInput4.Text = "";
        txtInput1.IsReadOnly = false;
        txtInput2.IsReadOnly = false;
        txtInput3.IsReadOnly = false;
        txtInput4.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }

}