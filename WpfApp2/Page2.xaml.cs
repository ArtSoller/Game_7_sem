using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page2.xaml
/// </summary>
public partial class Page2
{
    public Page2()
    {
        InitializeComponent();
        Canvas.SetLeft(Player1, _me.X);
        Canvas.SetTop(Player1, _me.Y);
    }

    private void But2_Click(object sender, RoutedEventArgs e)
    {
        _me.TeleportateTo(Location.Location1);
        NavigationService.Navigate(new Page1());
    }
}
