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
public partial class PageQuest1_2
{
    public PageQuest1_2(Player pl1, Player pl2)
    {
        InitializeComponent();
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;
        
        foreach(Image img in MainContainer.Children)
            img.MouseDown += DoubleMouseDown;

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\PictureClosed.mp3"));

        _me = pl1;
        _companion = pl2;
        txtScore.Visibility = Visibility.Hidden;


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
        Canvas.SetLeft(Target1, 0.43 * (SystemParameters.VirtualScreenWidth - AnswerContainer1.Width));

        Canvas.SetTop(AnswerContainer1, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer1.Height));
        Canvas.SetLeft(AnswerContainer1, 0.43 * (SystemParameters.VirtualScreenWidth - AnswerContainer1.Width));


        Canvas.SetTop(Target2, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer2.Height));
        Canvas.SetLeft(Target2, 0.5 * (SystemParameters.VirtualScreenWidth - AnswerContainer2.Width));

        Canvas.SetTop(AnswerContainer2, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer2.Height));
        Canvas.SetLeft(AnswerContainer2, 0.5 * (SystemParameters.VirtualScreenWidth - AnswerContainer2.Width));


        Canvas.SetTop(Target3, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer3.Height));
        Canvas.SetLeft(Target3, 0.57 * (SystemParameters.VirtualScreenWidth - AnswerContainer3.Width));

        Canvas.SetTop(AnswerContainer3, 0.4 * (SystemParameters.VirtualScreenHeight - AnswerContainer3.Height));
        Canvas.SetLeft(AnswerContainer3, 0.57 * (SystemParameters.VirtualScreenWidth - AnswerContainer3.Width));


        Canvas.SetTop(Back, 0.95 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.5 * (SystemParameters.VirtualScreenWidth - Back.Width));

        Canvas.SetTop(txtScore, 0.5 * SystemParameters.VirtualScreenHeight);
        Canvas.SetLeft(txtScore, 0.48 * SystemParameters.VirtualScreenWidth);

        Canvas.SetTop(Check, 0.85 * (SystemParameters.VirtualScreenHeight - Check.Height));
        Canvas.SetLeft(Check, 0.5 * (SystemParameters.VirtualScreenWidth - Check.Width));
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Play();
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");

        NavigationService.Navigate(new PageLocation1_2(_me, _companion));
    }

    private void Check_Click(object sender, RoutedEventArgs e)
    {
        foreach (Image child in AnswerContainer1.Children)
        {
            if (child.Name != "Picture1")
                return;
        }
        foreach (Image child in AnswerContainer2.Children)
        {
            if (child.Name != "Picture6")
                return;
        }
        foreach (Image child in AnswerContainer3.Children)
        {
            if (child.Name != "Picture8")
                return;
        }
        Game.isQuestDone = true;
        txtScore.Text = "Готово!";
        txtScore.Visibility = Visibility.Visible;
        Game.first_part_code += Game.randomString[0];
        IsTeleportActive = true;
        Check.Visibility = Visibility.Collapsed;
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
                        AnswerContainer1.HorizontalAlignment = HorizontalAlignment.Center;
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
