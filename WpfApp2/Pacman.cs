using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp2;

public class Pacman : UIElement
{
    public Rectangle Position { get; set; }

    public Pacman()
    {
        var mySolidColorBrush = new SolidColorBrush
        {
            Color = Color.FromArgb(255, 255, 255, 0)
        };

        Position = new Rectangle
        {
            Name = "pacman",
            Width = 30,
            Height = 30,
            Fill = mySolidColorBrush
        };
    }
    //<Rectangle Name = "pacman" Width="30" Height="30" Fill="Yellow" Canvas.Left="100" Canvas.Top="260" />
}