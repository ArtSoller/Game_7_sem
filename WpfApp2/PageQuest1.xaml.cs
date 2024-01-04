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
/// Логика взаимодействия для Page3.xaml
/// </summary>
public partial class Page3
{
    private bool isDragging = false;
    private Point startPoint;

    public Page3(Player pl1, Player pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;
        
        _me = pl1;
        _companion = pl2;
        
        CanvasSetUp();
    }

    private void CanvasSetUp()
    {
        Canvas.SetTop(Target, 0.5 * (SystemParameters.VirtualScreenHeight - Target.Height));
        Canvas.SetLeft(Target, 0.5 * (SystemParameters.VirtualScreenWidth - Target.Width));

        Canvas.SetTop(ImageContainer, 0.5 * (SystemParameters.VirtualScreenHeight - ImageContainer.Height));
        Canvas.SetLeft(ImageContainer, 0.5 * (SystemParameters.VirtualScreenWidth - ImageContainer.Width));

        Canvas.SetTop(DropTargets, 0.5 * (SystemParameters.VirtualScreenHeight - DropTargets.Height));
        Canvas.SetLeft(DropTargets, 0.5 * (SystemParameters.VirtualScreenWidth - DropTargets.Width));
    }

    private void But2_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        NavigationService.Navigate(new Page1(_me, _companion));
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            startPoint = e.GetPosition(null);
            isDragging = true;
            ((Image)sender).CaptureMouse();
        }
    }

    private void Image_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            Point position = e.GetPosition(null);
            var tt = new TranslateTransform
            {
                X = position.X - startPoint.X,
                Y = position.Y - startPoint.Y
            };
            ((Image)sender).RenderTransform = tt;
        }
    }

    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (isDragging)
        {
            isDragging = false;
            ((Image)sender).ReleaseMouseCapture();
        }
    }

    private void Target_DragEnter(object sender, DragEventArgs e)
    {
        e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
    }

    private void Target_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {

                // Создать новый Image элемент и добавить его в вашу страницу
                var newImage = new Image
                {
                    Source = new BitmapImage(new Uri(file))
                };

                // Добавить новый Image элемент в вашу разметку
                // Например, добавить его в StackPanel или другой контейнер
                ImageContainer.Children.Add(newImage);

                var dropTarget = new Border
                {
                    Background = Brushes.White,
                    AllowDrop = true
                };
                dropTarget.DragEnter += Target_DragEnter;
                dropTarget.Drop += Target_Drop;
                DropTargets.Children.Add(dropTarget);
            }
        }
    }
}
