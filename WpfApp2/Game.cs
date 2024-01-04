using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
