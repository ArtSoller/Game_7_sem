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
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp2;

/// <summary>
/// Логика взаимодействия для Page5.xaml
/// </summary>
public partial class Page5
{
    public string InputText { get; set; }

    public Page5()
    {
        InitializeComponent();
        txtScore.Visibility = Visibility.Hidden;
    }

    private void But4_Click(object sender, RoutedEventArgs e)
    {
        if (_me is null) throw new ArgumentException("_me is null");
        if (_companion is null) throw new ArgumentException("_companion is null");
        NavigationService.Navigate(new PageLocation3_1(_me, _companion));
    }
    private void But5_Click(object sender, RoutedEventArgs e)
    {
        string inputValue = txtInput.Text; // Получаем значение из текстового поля
        if (inputValue == "8903130627")
        {
            txtScore.Text = "Готово!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
        }
        else
        {
            txtScore.Text = "Неверный пароль!";
            txtScore.Visibility = Visibility.Visible;
            txtInput.IsReadOnly = true;
        }
    }
    private void But6_Click(object sender, RoutedEventArgs e)
    {
        txtInput.Text = ""; // Получаем значение из текстового поля
        txtInput.IsReadOnly = false;
        txtScore.Visibility = Visibility.Hidden;
    }

}