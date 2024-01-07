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
public partial class PageQuest1_1_1_picture
{
    private Brush? _brush;

    public PageQuest1_1_1_picture(Player pl1, Player pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        _me = pl1;
        _companion = pl2;

        CanvasSetObjects();
    }


    private void CanvasSetObjects()
    {



        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));

    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");
        NavigationService.Navigate(new PageLocation1_1(_me, _companion));
    }

}
