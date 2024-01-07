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

public class Game
{
    public Player Me;

    public Player Companion;

    public Room MyPage;

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

    public static int AttemptsNumber = 5;

    public static bool Quest0_5 = false, Quest1 = false, Quest2 = false, Quest3 = false, Quest4 = false;
    
    public static string CodeParts = "Найденные части кода: ";
    
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
            Role = role1, 
            CurrentLocation = Location.Location0
        };
        
        Companion = new Player() 
        {
            X = 0.05 * SystemParameters.VirtualScreenWidth,
            Y = 0.66 * SystemParameters.VirtualScreenHeight,
            Role = role2, 
            CurrentLocation = Location.Location0
        };
        Connection.StartServer();
        // MyPage = new PageLocation0(Me, Companion);
    }
}
