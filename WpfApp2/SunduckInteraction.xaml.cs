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
        txtInput2.Text = Game.count_try.ToString();        
    }



    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");
        NavigationService.Navigate(new Page1(_me, _companion));
    }
    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        string inputValue1 = txtInput1.Text; // Получаем значение из текстового поля

        if (inputValue1 == Game.randomString)
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            Game.parts_code += Game.randomString[0];
            Room.IsTeleportActive = true;
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtScore.Visibility = Visibility.Visible;
            txtInput1.IsReadOnly = true;
            Game.count_try -= 1;
            txtInput2.Text = Game.count_try.ToString();
            if (Game.count_try == 0)
            {
                GameOver("Dead");
            }
        }
    }

    protected void GameOver(string message)
    {
        if (gameTimer is null) throw new Exception("gameTimer is null");
        // inside the game over function we passing in a string to show the final message to the game
        gameTimer.Stop(); // stop the game timer
        MessageBox.Show(message, "The Pac Man Game WPF MOO ICT"); // show a mesage box with the message that is passed in this function
        // when the player clicks ok on the message box
        // restart the application
        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        Application.Current.Shutdown();
    }
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        txtInput1.Text = ""; // Получаем значение из текстового поля
        txtInput1.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

}