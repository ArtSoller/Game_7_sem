using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2;

public enum Location
{
    Location1,
    Location2,
    Location3,
    Location4,
    Location5,
    Location6,
    Location7
}

public enum Role
{
    Performer,
    Assistant
}

public class Game
{
    public Player Me;

    public Player Companion;

    public Room MyPage;

    private bool _isConnected = false;


    private static Random random = new Random();

    // Генерируем случайные цифры
    //private static int digit1 = random.Next(0, 10);
    //private static int digit2 = random.Next(0, 10);
    //private static int digit3 = random.Next(0, 10);
    //private static int digit4 = random.Next(0, 10);

    private static int digit1 = 1;
    private static int digit2 = 2;
    private static int digit3 = 3;
    private static int digit4 = 4;

    //Brush[] colors = new Brush[]
    //{
    //    Brushes.Orange, // оранжевый
    //    Brushes.Red,    // красный
    //    Brushes.Blue,   // синий
    //    Brushes.Green   // зеленый
    //};

    // Собираем строку из сгенерированных цифр $"{digit1}";

    public static string randomString => $"{digit1}{digit2}{digit3}{digit4}";

    public static int count_try = 5;

    public static bool Quest0_5 = false, Quest1 = false, Quest2 = false, Quest3 = false, Quest4 = false;
    public static string parts_code = "Найденные части кода: ";
    

    public Game()
    {
        Random rnd = new();

        var value = rnd.Next(0, 2);

        Role role1 = value == 0 ? Role.Performer : Role.Assistant;
        Role role2 = value == 0 ? Role.Assistant : Role.Performer;
        
        Me = new Player() 
        { 
            X = 0.05 * SystemParameters.VirtualScreenWidth, 
            Y = 0.33 * SystemParameters.VirtualScreenHeight, 
            _role = role1, 
            CurrentLocation = Location.Location1
        };
        
        Companion = new Player() 
        {
            X = 0.05 * SystemParameters.VirtualScreenWidth,
            Y = 0.66 * SystemParameters.VirtualScreenHeight,
            _role = role2, 
            CurrentLocation = Location.Location1 
        };
        
        MyPage = new Page1(Me, Companion);
    }

    public bool ConnectPlayers()
    {
        /* Логика подключения */
        return _isConnected;
    }

    public void StartGame()
    {
        if (_isConnected)
            MyPage = new Page1(Me, Companion);
    }
}
