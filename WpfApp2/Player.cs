using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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

public class Player
{
    private string _imgPath = "path to load player image";

    private readonly string _name;

    public string Name {  get => _name; }

    private double _x;

    private double _y;

    public double X { get => _x; set => _x = value; }

    public double Y { get => _y; set => _y = value; }

    private Location _currentLocation; 

    public Role _role;

    private string _ip;

    public string IP { get => _ip; }

    public Player()
    {
        _name = "Default";
        _currentLocation = Location.Location1;
        _role = Role.Performer;
        var Host = Dns.GetHostName();
        var _IP = Dns.GetHostAddresses(Host);
        _ip = _IP.Length == 5 ? $"{_IP[4]}:7106" : $"{_IP[1]}:7106";
    }

    public Player(string name, Role role)
    {
        _name = name;
        _currentLocation = Location.Location1;
        _role = role;
        var Host = Dns.GetHostName();
        var _IP = Dns.GetHostAddresses(Host);
        _ip = _IP.Length == 5 ? $"{_IP[4]}:7106" : $"{_IP[1]}:7106";
    }

    public void TeleportateTo(Location location)
    {
        _currentLocation = location;
        switch (_currentLocation)
        {
            case Location.Location1:
            break;
            case Location.Location2:
                X = 0.0D;
                Y = 0.0D;
            break;
            case Location.Location3:
                break;
            case Location.Location4:
                break;
            case Location.Location5:
                break;
            case Location.Location6:
                break;
            case Location.Location7:
                break;
        }
    }
}
