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

    public Page3(Player pl1, Player pl2) : base(pl1, pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;
        
        foreach(Image img in MainContainer.Children)
            img.MouseDown += DoubleMouseDown;

        CanvasSetObjects();
    }

    protected override void CanvasSetObjects()
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
        if (Game.Me is null) throw new ArgumentException("Game.Me is null");
        if (Game.Companion is null) throw new ArgumentException("Game.Companion is null");

        NavigationService.Navigate(new PageLocation3_1(Game.Me, Game.Companion));
    }

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

    protected override void SetMovementPossibility()
    {
        throw new NotImplementedException();
    }
}
