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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page6.xaml
/// </summary>
public partial class Page8
{
    public string? InputText { get; set; }

    private MediaPlayer _mediaPlayer;

    public Page8(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();

        _mediaPlayer = new();
        _mediaPlayer.MediaFailed += FailedMusic;

        _mediaPlayer.Open(new Uri("D:\\CodeRepos\\CS\\NewGame\\Game_7_sem\\WpfApp2\\snd\\ChestOpened.mp3"));
        _mediaPlayer.Play();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;
        gameTimer = new();
        _me = pl1;
        _companion = pl2;
        txtScore.Visibility = Visibility.Hidden;
        txtInput2.IsReadOnly = true;
        count.IsReadOnly = true;
        txtInput2.Text = Game.AttemptsNumber.ToString();       
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        _mediaPlayer.Open(new Uri("D:\\CodeRepos\\CS\\NewGame\\Game_7_sem\\WpfApp2\\snd\\ChestClosed.mp3"));
        _mediaPlayer.Play();

        NavigationService.Navigate(new PageLocation0(_me, _companion));
    }

    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        var inputValue1 = txtInput1.Text; // Получаем значение из текстового поля

        if (inputValue1 == Game.QuestKeyString)
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            Game.CodeParts += Game.QuestKeyString[0];
            IsTeleportActive = true;
            GameOver("Won");
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            Game.AttemptsNumber -= 1;
            txtInput2.Text = Game.AttemptsNumber.ToString();
            if (Game.AttemptsNumber == 0)
                GameOver("Dead");
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
        txtInput1.Text = ""; // Получаем значение из текстового поля
        txtInput1.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }

    protected override void CanvasSetObjects()
    {
        throw new NotImplementedException();
    }
}