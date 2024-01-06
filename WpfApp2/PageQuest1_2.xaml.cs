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
        
        foreach(Image img in MainContainer.Children)
            img.MouseDown += DoubleMouseDown;

        _me = pl1;
        _companion = pl2;
        
        CanvasSetUp();
    }

    private void CanvasSetUp()
    {
        Canvas.SetTop(Quest, 0.01 * (SystemParameters.VirtualScreenHeight));
        Canvas.SetLeft(Quest, 0.5 * (SystemParameters.VirtualScreenWidth - Quest.Width));

        Canvas.SetTop(TargetMain, 0.7 * (SystemParameters.VirtualScreenHeight - MainContainer.Height));
        Canvas.SetLeft(TargetMain, 0.5 * (SystemParameters.VirtualScreenWidth - MainContainer.Width));

        Canvas.SetTop(MainContainer, 0.7 * (SystemParameters.VirtualScreenHeight - MainContainer.Height));
        Canvas.SetLeft(MainContainer, 0.5 * (SystemParameters.VirtualScreenWidth - MainContainer.Width));


        Canvas.SetTop(Target1, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer1.Height));
        Canvas.SetLeft(Target1, 0.25 * (SystemParameters.VirtualScreenWidth - AnswerContainer1.Width));

        Canvas.SetTop(AnswerContainer1, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer1.Height));
        Canvas.SetLeft(AnswerContainer1, 0.25 * (SystemParameters.VirtualScreenWidth - AnswerContainer1.Width));


        Canvas.SetTop(Target2, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer2.Height));
        Canvas.SetLeft(Target2, 0.5 * (SystemParameters.VirtualScreenWidth - AnswerContainer2.Width));

        Canvas.SetTop(AnswerContainer2, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer2.Height));
        Canvas.SetLeft(AnswerContainer2, 0.5 * (SystemParameters.VirtualScreenWidth - AnswerContainer2.Width));


        Canvas.SetTop(Target3, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer3.Height));
        Canvas.SetLeft(Target3, 0.75 * (SystemParameters.VirtualScreenWidth - AnswerContainer3.Width));

        Canvas.SetTop(AnswerContainer3, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer3.Height));
        Canvas.SetLeft(AnswerContainer3, 0.75 * (SystemParameters.VirtualScreenWidth - AnswerContainer3.Width));


        Canvas.SetTop(But3, 0.85 * (SystemParameters.VirtualScreenHeight - But3.Height));
        Canvas.SetLeft(But3, 0.5 * (SystemParameters.VirtualScreenWidth - But3.Width));

        //UIElement obj = MainContainer.FindName("Picture1") as Image;

        //MainContainer.Children.Remove(obj);
        //AnswerContainer1.Children.Add(obj);
    }

    private void But2_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        NavigationService.Navigate(new PageLocation3_1(_me, _companion));
    }



    #region Возможный мусор
    //private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    //{
    //    if (e.ChangedButton == MouseButton.Left)
    //    {
    //        startPoint = e.GetPosition(null);
    //        isDragging = true;
    //        ((Image)sender).CaptureMouse();
    //    }
    //}

    //private void Image_MouseMove(object sender, MouseEventArgs e)
    //{
    //    if (isDragging)
    //    {
    //        Point position = e.GetPosition(null);
    //        var tt = new TranslateTransform
    //        {
    //            X = position.X - startPoint.X,
    //            Y = position.Y - startPoint.Y
    //        };
    //        ((Image)sender).RenderTransform = tt;
    //    }
    //}

    //private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    //{
    //    if (isDragging)
    //    {
    //        isDragging = false;
    //        ((Image)sender).ReleaseMouseCapture();
    //    }
    //}

    //private void Target_DragEnter(object sender, DragEventArgs e)
    //{
    //    e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
    //}

    //private void Target_Drop(object sender, DragEventArgs e)
    //{
    //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
    //    {
    //        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
    //        foreach (var file in files)
    //        {

    //            // Создать новый Image элемент и добавить его в вашу страницу
    //            var newImage = new Image
    //            {
    //                Source = new BitmapImage(new Uri(file))
    //            };

    //            // Добавить новый Image элемент в вашу разметку
    //            // Например, добавить его в StackPanel или другой контейнер
    //            MainContainer.Children.Add(newImage);

    //            var dropTarget = new Border
    //            {
    //                Background = Brushes.White,
    //                AllowDrop = true
    //            };
    //            dropTarget.DragEnter += Target_DragEnter;
    //            dropTarget.Drop += Target_Drop;
    //        }
    //    }
    //}
    #endregion

    private void DoubleMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            Image obj = sender as Image;

            if (obj.Parent == MainContainer)
            {
                if (AnswerContainer1.Children.Count * AnswerContainer2.Children.Count * AnswerContainer3.Children.Count == 0)
                    MainContainer.Children.Remove(obj);

                switch (AnswerContainer1.Children.Count, AnswerContainer2.Children.Count, AnswerContainer3.Children.Count)
                {
                    case (0, int, int):
                        AnswerContainer1.Children.Add(obj);
                        break;
                    case (1, 0, int):
                        AnswerContainer2.Children.Add(obj);
                        break;
                    case (1, 1, 0):
                        AnswerContainer3.Children.Add(obj);
                        break;
                    case (1, 1, 1):
                        break;
                    default: throw new ArgumentException("Slomalos' vse");
                }
            }
            else if (obj.Parent == AnswerContainer1)
            {
                AnswerContainer1.Children.Remove(obj);
                MainContainer.Children.Add(obj);
            }
            else if (obj.Parent == AnswerContainer2)
            {
                AnswerContainer2.Children.Remove(obj);
                MainContainer.Children.Add(obj);
            }
            else if (obj.Parent == AnswerContainer3)
            {
                AnswerContainer3.Children.Remove(obj);
                MainContainer.Children.Add(obj);
            }
            else
                throw new ArgumentException("Ty chto nadelal???");
        }
    }
}
