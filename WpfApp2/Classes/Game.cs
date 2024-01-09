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
    Location0,
    Location1_1,
    Location1_2,
    Location2_1,
    Location2_2,
    Location3_1,
    Location3_2,
    Location4_1,
    Location4_2
}

public enum Role
{
    Performer,
    Assistant
}

public static class Game
{
    public static Player Me;

    public static Player Companion;

    public static Room MyPage;

    private static Random random = new();

    // Генерируем случайные цифры
    //private static int digit1 = random.Next(0, 10);
    //private static int digit2 = random.Next(0, 10);
    //private static int digit3 = random.Next(0, 10);
    //private static int digit4 = random.Next(0, 10);

    private static readonly int digit1 = 1;
    private static readonly int digit2 = 2;
    private static readonly int digit3 = 3;
    private static readonly int digit4 = 4;

    // Собираем строку из сгенерированных цифр

    public static string QuestKeyString => $"{digit1}{digit2}{digit3}{digit4}";

    public static int AttemptsNumber = 3;

    public static SolidColorBrush blueBrush = new SolidColorBrush(Colors.Blue);
    public static SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
    public static SolidColorBrush defaultBrush = new SolidColorBrush(Colors.LightGray);


    public static string parts_code = "Найденные части кода: ";
    public static string first_part_code = "";
    public static string second_part_code = "";
    public static string third_part_code = "";
    public static string fourth_part_code = "";

    public static bool isGameDone = false;


    public static void Init()
    {
        
        Me = new Player() 
        {   
            CurrentLocation = Location.Location0
        };
        
        Companion = new Player() 
        {
            CurrentLocation = Location.Location0
        };
    }
}
