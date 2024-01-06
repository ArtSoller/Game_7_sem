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

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page4.xaml
/// </summary>
public partial class Page20
{
    private Brush? _brush;

    public Page20(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        CanvasSetObjects();
    }

    private void CanvasSetObjects()
    {
        Canvas.SetLeft(quest_3, 0.5 * (SystemParameters.VirtualScreenWidth - quest_3.Width));

        Canvas.SetTop(But3, 0.6 * (SystemParameters.VirtualScreenHeight - But3.Height));
        Canvas.SetLeft(But3, 0.5 * (SystemParameters.VirtualScreenWidth - But3.Width));


        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));
        But3.Foreground = _brush;

    }

    private void But3_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");
        NavigationService.Navigate(new PageLocation1_1(_me, _companion));
    }
}
