using System;
using System.Collections.Generic;
using System.Drawing;
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
public partial class OpeningPage
{
    private bool _isConnectedWithCompanion;

    private Brush? _brush;

    public Game Game;

    public OpeningPage(Game game)
    {
        InitializeComponent();
        Font.Height = SystemParameters.VirtualScreenHeight;
        Font.Width = SystemParameters.VirtualScreenWidth / 2;

        Game = game;

        CanvasSetObjects();
        IpConnect();
    }

    private void CanvasSetObjects()
    {
        Canvas.SetLeft(Sign, 0.5 * (SystemParameters.VirtualScreenWidth / 2 - Sign.Width));

        Canvas.SetTop(TextBoxInfoIP, TextBoxInfoIP.Height);
        Canvas.SetLeft(TextBoxInfoIP, SystemParameters.VirtualScreenWidth / 2 - TextBoxInfoIP.Width);

        Canvas.SetTop(ButtStart, 0.6 * (SystemParameters.VirtualScreenHeight - ButtStart.Height));
        Canvas.SetLeft(ButtStart, 0.5 * (SystemParameters.VirtualScreenWidth / 2 - ButtStart.Width));

        Canvas.SetTop(ButtConnect1, Canvas.GetTop(ButtStart) + 90);
        Canvas.SetLeft(ButtConnect1, 0.5 * (SystemParameters.VirtualScreenWidth / 2 - ButtStart.Width));

        Canvas.SetTop(QuitBox, Canvas.GetTop(ButtConnect1) + 90);
        Canvas.SetLeft(QuitBox, 0.5 * (SystemParameters.VirtualScreenWidth / 2 - ButtConnect1.Width));

        Canvas.SetTop(MyNameTextBox, 0.35 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(MyNameTextBox, 0.25 * (SystemParameters.VirtualScreenWidth / 2 - MyNameTextBox.Width));

        Canvas.SetTop(CompanionNameTextBox, 0.45 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(CompanionNameTextBox, 0.25 * (SystemParameters.VirtualScreenWidth / 2 - CompanionNameTextBox.Width));

        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));
        ButtStart.Foreground = _brush;

    }

    private void IpConnect()
    {
        var Host = Dns.GetHostName();
        var IP = Dns.GetHostAddresses(Host);
        var ipAddress = IP.Length == 5 ? $"{IP[4]}:7106" : $"{IP[1]}:7106";
        TextBoxInfoIP.Text = "Ваш IP адрес: " + ipAddress;
    }

    private void StartGame(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new Page1(Game.Me, Game.Companion));
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
        ButtStart.Visibility = Visibility.Visible;
        ButtConnect1.Visibility = Visibility.Visible;
    }

    private void QuitGame(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
