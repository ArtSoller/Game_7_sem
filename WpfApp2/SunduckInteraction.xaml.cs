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
/// Логика взаимодействия для Page6.xaml
/// </summary>
public partial class Page8
{
    public string InputText { get; set; }

    public Page8(Player pl1, Player pl2)
    {
        InitializeComponent();
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
        NavigationService.Navigate(new PageLocation0(_me, _companion));
    }
    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        var inputValue1 = txtInput1.Text; // Получаем значение из текстового поля

        if (inputValue1 == Game.randomString)
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            Game.parts_code += Game.randomString[0];
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


    private void GameOver(string message)
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

}