using System.Windows;
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
    }

    private void But2_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new Page1());
    }
}
