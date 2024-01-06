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
public partial class Page4
{
    private bool isDragging = false;
    
    private Point startPoint;

    public Page4(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
    }

    private void But3_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        NavigationService.Navigate(new PageLocation1_1(_me, _companion));
    }
}
