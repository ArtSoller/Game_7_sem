using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page3.xaml
/// </summary>
public partial class Page3
{
    private bool isDragging = false;
    private Point startPoint;

    public Page3()
    {
        InitializeComponent();
    }

    private void CanvasKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F)
        {
            // Открываем страницу для прямоугольника 1
            NavigationService.Navigate(new Page1());
        }
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
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
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
