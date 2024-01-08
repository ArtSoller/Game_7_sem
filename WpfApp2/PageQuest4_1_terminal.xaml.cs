﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page5.xaml
/// </summary>
public partial class PageQuest4_1_terminal
{
    public string InputText { get; set; }

    private Brush? _brush;

    public PageQuest4_1_terminal(Player pl1, Player pl2)
    {
        InitializeComponent();
        txtScore.Visibility = Visibility.Hidden;
        Background.Width = SystemParameters.VirtualScreenWidth;
        Background.Height = SystemParameters.VirtualScreenHeight;

        _me = pl1;
        _companion = pl2;

        mediaPlayer = new();
        mediaPlayer.MediaFailed += FailedMusic;
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\PapirusOpened.mp3"));
        mediaPlayer.Play();
        CanvasSetObjects();

    }

    private void CanvasSetObjects()
    {
        Canvas.SetTop(Back, 0.6 * (SystemParameters.VirtualScreenHeight - Back.Height));
        Canvas.SetLeft(Back, 0.5 * (SystemParameters.VirtualScreenWidth - Back.Width));


        _brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(190, 190, 190));
        Back.Foreground = _brush;

    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        mediaPlayer.Open(new Uri("A:\\NSTU\\4_course\\7_sem\\Elem_comp\\Игра\\Game_new\\Game_7_sem\\WpfApp2\\snd\\PapirusOpened.mp3"));
        mediaPlayer.Play();
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");
        NavigationService.Navigate(new PageLocation4_1(_me, _companion));
    }
    private void Enter_Click(object sender, RoutedEventArgs e)
    {
        string inputValue = txtInput.Text; // Получаем значение из текстового поля
        if (inputValue == "8903130627")
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
            Game.third_part_code += Game.randomString[3];
            IsTeleportActive = true;
            Enter.Visibility = Visibility.Collapsed;
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
        }
    }
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        txtInput.Text = ""; // Получаем значение из текстового поля
        txtInput.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

}