using System.Windows;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Game myGame;

    public MainWindow()
    {
        myGame = new();
        InitializeComponent();
        MainFrame.Content = new OpeningPage(myGame);
        WindowState = WindowState.Maximized;
        WindowStyle = WindowStyle.None;
    }
}
