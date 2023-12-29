using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WpfApp2;

public class Player
{
    private string _imgPath = "path to load player image";

    private string _name;

    public string Name { get => _name; set => _name = value; }

    private double _x;

    private double _y;

    public double X { get => _x; set => _x = value; }

    public double Y { get => _y; set => _y = value; }

    internal Location CurrentLocation; 

    public Role _role;

    private string _ip;

    public string IP { get => _ip; }

    public Player()
    {
        _name = "Default";
        _role = Role.Performer;
        var Host = Dns.GetHostName();
        var _IP = Dns.GetHostAddresses(Host);
        _ip = _IP.Length == 5 ? $"{_IP[4]}:7106" : $"{_IP[1]}:7106";
    }

    public Player(string name, Role role)
    {
        _name = name;
        _role = role;
        var Host = Dns.GetHostName();
        var _IP = Dns.GetHostAddresses(Host);
        _ip = _IP.Length == 5 ? $"{_IP[4]}:7106" : $"{_IP[1]}:7106";
    }

    public void TeleportateTo(Location location)
    {
        CurrentLocation = location;
        switch (CurrentLocation)
        {
            case Location.Location1:
                X = 896.0D;
                Y = 92.0D;
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
