using Google.Protobuf.WellKnownTypes;
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

    public OpeningPage()
    {
        InitializeComponent();
        Font.Height = SystemParameters.VirtualScreenHeight;
        Font.Width = SystemParameters.VirtualScreenWidth;

        CanvasSetObjects();
        IpConnect();
    }

    private void CanvasSetObjects()
    {
        Canvas.SetLeft(Sign, 0.5 * (SystemParameters.VirtualScreenWidth - Sign.Width));

        Canvas.SetTop(TextBoxInfoIP, TextBoxInfoIP.Height);
        Canvas.SetLeft(TextBoxInfoIP, SystemParameters.VirtualScreenWidth - TextBoxInfoIP.Width);

        Canvas.SetTop(ButtStart, 0.6 * (SystemParameters.VirtualScreenHeight - ButtStart.Height));
        Canvas.SetLeft(ButtStart, 0.5 * (SystemParameters.VirtualScreenWidth - ButtStart.Width));
        ButtStart.IsEnabled = false;

        Canvas.SetTop(ButtConnect1, Canvas.GetTop(ButtStart) + 90);
        Canvas.SetLeft(ButtConnect1, 0.5 * (SystemParameters.VirtualScreenWidth - ButtStart.Width));
        
        Canvas.SetTop(QuitBox, Canvas.GetTop(ButtConnect1) + 90);
        Canvas.SetLeft(QuitBox, 0.5 * (SystemParameters.VirtualScreenWidth - ButtConnect1.Width));
        
        Canvas.SetTop(BackBox, Canvas.GetTop(ButtConnect1) + 90);
        Canvas.SetLeft(BackBox, 0.5 * (SystemParameters.VirtualScreenWidth - ButtConnect1.Width));
        BackBox.Visibility = Visibility.Collapsed;

        Canvas.SetTop(MyNameTextBox, 0.35 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(MyNameTextBox, 0.25 * (SystemParameters.VirtualScreenWidth - MyNameTextBox.Width));

        Canvas.SetTop(IPEnterBox, 0.35 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(IPEnterBox, Canvas.GetLeft(MyNameTextBox) + MyNameTextBox.Width + 100);

        Canvas.SetTop(CompanionNameTextBox, 0.45 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(CompanionNameTextBox, 0.25 * (SystemParameters.VirtualScreenWidth - CompanionNameTextBox.Width));
    }

    private void IpConnect()
    {
        var Host = Dns.GetHostName();
        var IP = Dns.GetHostAddresses(Host);
        var externalIP = new WebClient().DownloadString("https://api.ipify.org");
        TextBoxInfoIP.Text = "Ваш внешний IP адрес: " + externalIP + "\nВаш локальный IP адрес: " + IP[0];
    }

    private void StartGame(object sender, RoutedEventArgs e)
    {
        Connection.ReceiveRole();
        Connection.ReceiveCoordinates().Start();
        NavigationService.Navigate(new PageLocation0(Game.Me, Game.Companion));
    }

    private async void WaitingForCompanion(object sender, RoutedEventArgs e)
    {
        /* Здесь будет логика подключения между игроками */
    }

    private void ConnectToCompanion(object sender, RoutedEventArgs e)
    {
        IPEnterBox.Visibility = Visibility.Visible;
        Connection.SetConnection(IPEnterBox.Text);
        if (Connection.IsConnected)
        {
            _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            ButtStart.Foreground = _brush;
            ButtStart.IsEnabled = true;
            CompanionNameTextBox.Text = Game.Companion.Name;
        }
    }

    private void TryToConnect(object sender, RoutedEventArgs e)
    {

    }

    private void BackToMain(object sender, RoutedEventArgs e)
    {
        BackBox.Visibility = Visibility.Collapsed;
        QuitBox.Visibility = Visibility.Visible;
        ButtStart.Visibility = Visibility.Visible;
        ButtConnect1.Visibility = Visibility.Visible;
        IPEnterBox.Visibility = Visibility.Collapsed;
        ButtStart.IsEnabled = false;
    }

    private void QuitGame(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
