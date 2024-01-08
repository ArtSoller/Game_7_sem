using System.Windows;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        Game.Init();
        InitializeComponent();
        MainFrame.Content = new OpeningPage();
        WindowState = WindowState.Maximized;
        WindowStyle = WindowStyle.None;
    }
}
