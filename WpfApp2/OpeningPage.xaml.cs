using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WpfApp2;
/// <summary>
/// Логика взаимодействия для OpeningPage.xaml
/// </summary>
public partial class OpeningPage : Room
{
    private bool _isConnectedWithCompanion;

    private Brush? _brush;

    public OpeningPage()
    {
        InitializeComponent();
        var Host = Dns.GetHostName();
        var IP = Dns.GetHostAddresses(Host);
        var ipAddress = IP.Length == 5 ? $"{IP[4]}:7106" : $"{IP[1]}:7106";
        TextBoxInfoIP.Text = "Ваш IP адрес: " + ipAddress;

        _brush = new SolidColorBrush(Color.FromRgb(190, 190, 190));
        ButtStart.Foreground = _brush;
    }

    private void StartGame(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new Page1());
    }

    private async void WaitingForCompanion(object sender, RoutedEventArgs e)
    {
        /* Здесь будет логика подключения между игроками */
        
    }

    private void ConnectToCompanion(object sender, RoutedEventArgs e)
    {
        
        ButtStart.Visibility = Visibility.Collapsed;
        ButtConnect1.Visibility = Visibility.Collapsed;

    }

    private void TryToConnect(object sender, RoutedEventArgs e)
    {

    }

    private void BackToMain(object sender, RoutedEventArgs e)
    {
        //UnrealCyberRoyak.Content = "Нереально красивое название для игры будет здесь";

        ButtStart.Visibility = Visibility.Visible;
        ButtConnect1.Visibility = Visibility.Visible;

    }
}
