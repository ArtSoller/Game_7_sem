using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;

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

    private double _speedX;

    private double _speedY;

    public double SpeedX { get => _speedX; set => _speedX = value; }

    public double SpeedY { get => _speedY; set => _speedY = value; }

    public double Speed { get => Math.Sqrt(_speedX * _speedX + _speedY * _speedY); }

    public bool IsMovingUpward { get; set; }

    public bool IsMovingDownward { get; set; }

    public bool IsMovingLeftward { get; set; }

    public bool IsMovingRightward { get; set; }

    public bool IsForcing { get; set; }

    internal Location CurrentLocation; 

    public Role Role;

    private readonly string _ip;

    public bool _isPossibleUpwardMovement = false, _isPossibleLeftwardMovement = false, _isPossibleRightwardMovement = false, _isPossibleDownwardMovement = false;


    public Player()
    {
        _name = "Default";
        Role = Role.Performer;
        var Host = Dns.GetHostName();
        var _IP = Dns.GetHostAddresses(Host);
        _ip = _IP.Length == 5 ? $"{_IP[4]}:7106" : $"{_IP[1]}:7106";
    }

    public Player(string name, Role role)
    {
        _name = name;
        Role = role;
        var Host = Dns.GetHostName();
        var _IP = Dns.GetHostAddresses(Host);
        _ip = _IP.Length == 5 ? $"{_IP[4]}:7106" : $"{_IP[1]}:7106";
    }

    public void TeleportateTo(Location location)
    {
        CurrentLocation = location;

        if (CurrentLocation == Location.Location0)
        { 
            if (Game.Me.Role == Role.Performer)
            {
                Game.Me.X = 100;
                Game.Me.Y = SystemParameters.VirtualScreenHeight * 0.33;
            }
            else
            {
                Game.Me.X = 100;
                Game.Me.Y = SystemParameters.VirtualScreenHeight * 0.66;
            }
        }
        else
        {
            Game.Me.X = 80;
            Game.Me.Y = 0.5 * (SystemParameters.VirtualScreenHeight - 50);
        }
    }
}