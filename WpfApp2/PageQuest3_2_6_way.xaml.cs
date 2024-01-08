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
public partial class Page14
{
    private Brush? _brush;

    public Page14(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        CanvasSetObjects();
    }

    protected override void CanvasSetObjects()
    {
        Canvas.SetLeft(quest_3, 0.5 * (SystemParameters.VirtualScreenWidth - quest_3.Width));

        Canvas.SetTop(But3, 0.6 * (SystemParameters.VirtualScreenHeight - But3.Height));
        Canvas.SetLeft(But3, 0.5 * (SystemParameters.VirtualScreenWidth - But3.Width));


        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));
        But3.Foreground = _brush;

    }

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }

    private void But3_Click(object sender, RoutedEventArgs e)
    {
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");
        NavigationService.Navigate(new PageLocation3_2(Game.Me, Game.Companion));
    }
}
