using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public Game()
    {
        Random rnd = new();

        var value = rnd.Next(0, 2);

        Role role1 = value == 0 ? Role.Performer : Role.Assistant;
        Role role2 = value == 0 ? Role.Assistant : Role.Performer;

        Me = new Player() 
        { 
            X = 100, 
            Y = 260, 
            _role = role1, 
            CurrentLocation = Location.Location1
        };
        
        Companion = new Player() 
        { 
            X = 100, 
            Y = 460, 
            _role = role2, 
            CurrentLocation = Location.Location1 
        };
        
        MyPage = new Page1();
    }
}
