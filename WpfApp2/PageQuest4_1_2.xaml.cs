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
/// Логика взаимодействия для Page5.xaml
/// </summary>
public partial class Page5
{
    public string InputText { get; set; }

    private Brush? _brush;

    public Page5(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        txtScore.Visibility = Visibility.Hidden;
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        CanvasSetObjects();

        Image image = new Image();
        BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/img/reset.png")); // Указываем путь к вашей картинке
        image.Source = bitmap;

        // Устанавливаем изображение в качестве содержимого кнопки
        But6.Content = image;

    }

    protected override void CanvasSetObjects()
    {
        Canvas.SetTop(But4, 0.6 * (SystemParameters.VirtualScreenHeight - But4.Height));
        Canvas.SetLeft(But4, 0.5 * (SystemParameters.VirtualScreenWidth - But4.Width));


        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));
        But4.Foreground = _brush;

    }

    private void But4_Click(object sender, RoutedEventArgs e)
    {
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");
        NavigationService.Navigate(new PageLocation3_1(Game.Me, Game.Companion));
    }
    
    private void But5_Click(object sender, RoutedEventArgs e)
    {
        string inputValue = txtInput.Text; // Получаем значение из текстового поля
        if (inputValue == "8903130627")
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
        }
    }
    
    private void But6_Click(object sender, RoutedEventArgs e)
    {
        txtInput.Text = ""; // Получаем значение из текстового поля
        txtInput.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }
}