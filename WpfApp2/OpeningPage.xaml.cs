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
public partial class OpeningPage : Page
{
    private bool _isConnectedWithCompanion;

    public OpeningPage()
    {
        InitializeComponent();
        var Host = Dns.GetHostName();
        var IP = Dns.GetHostAddresses(Host);
        var ipAddress = IP.Length == 5 ? $"{IP[4]}:7106" : $"{IP[1]}:7106";
        TextBoxInfoIP.Text = "Ваш IP адрес: " + ipAddress;
        ButtConnect2.Visibility = Visibility.Collapsed;
        GoBack.Visibility = Visibility.Collapsed;
    }

    private void StartGame(object sender, RoutedEventArgs e)
    {
        UnrealCyberRoyak.Content = "Ожидаем подключения";

        ButtStart.Visibility = Visibility.Collapsed;
        ButtConnect1.Visibility = Visibility.Collapsed;

        ButtConnect2.Visibility = Visibility.Collapsed;
        GoBack.Visibility = Visibility.Collapsed;

        /*Подключение должно быть асинхронным*/
        UnrealCyberRoyak.Visibility = Visibility.Collapsed;
        _isConnectedWithCompanion = true;
        if (_isConnectedWithCompanion)
            NavigationService.Navigate(new Page1());
    }

    private void ConnectToCompanion(object sender, RoutedEventArgs e)
    {
        UnrealCyberRoyak.Content = "Lobby zone";

        ButtStart.Visibility = Visibility.Collapsed;
        ButtConnect1.Visibility = Visibility.Collapsed;

        ButtConnect2.Visibility = Visibility.Visible;
        GoBack.Visibility = Visibility.Visible;
    }

    private void TryToConnect(object sender, RoutedEventArgs e)
    {

    }

    private void BackToMain(object sender, RoutedEventArgs e)
    {
        UnrealCyberRoyak.Content = "Нереально красивое название для игры будет здесь";

        ButtStart.Visibility = Visibility.Visible;
        ButtConnect1.Visibility = Visibility.Visible;

        ButtConnect2.Visibility = Visibility.Collapsed;
        GoBack.Visibility = Visibility.Collapsed;
    }
}
