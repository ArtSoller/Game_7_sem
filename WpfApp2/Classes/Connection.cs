using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;

namespace WpfApp2;

internal static class Connection
{
    private static AsyncDuplexStreamingCall<Content, Content>? call;

    private static GrpcChannel? channel;

    private static Greeter.GreeterClient? client;

    public static bool IsConnected;

    public static void SetConnection(string address, string name)
    {
        if (address is null) return;
        if (name is null) return;
        channel = GrpcChannel.ForAddress($"http://{address}");
        client = new Greeter.GreeterClient(channel);
        call = client.ActionStream();
        while (channel.State == ConnectivityState.Connecting)
            continue;
        if (channel.State == ConnectivityState.Ready)
            IsConnected = true;
        call.RequestStream.WriteAsync(new Content() { Name = name });
        Game.Me.Name = name;
    }
    public static void SendCoordinates(string _name, double _x, double _y, bool _isMovingLeftward, bool _isMovingRightward, bool _isMovingUpward, bool _isMovingDownward, string _first, string _second, string _third, string _fourth)
    {
        if (call is null) throw new ArgumentNullException("call is null");
        call.RequestStream.WriteAsync(new Content() {Name = _name, X = _x, Y = _y, IsMovingLeftward = _isMovingLeftward, IsMovingRightward = _isMovingRightward, IsMovingUpward = _isMovingUpward, IsMovingDownward = _isMovingDownward, Combination = new Code {First = _first, Second = _second, Third = _third, Fourth = _fourth }});
    }
    public static async Task ReceiveCoordinates()
    {
        if (call is null) throw new ArgumentNullException("call is null");

        while (true)
        {
            await foreach (var res in call.ResponseStream.ReadAllAsync())
            {
                if (Game.Companion.Name == res.Name)
                {
                    Game.Companion.X = res.X;
                    Game.Companion.Y = res.Y;
                    Game.Companion.IsMovingLeftward = res.IsMovingLeftward;
                    Game.Companion.IsMovingRightward = res.IsMovingRightward;
                    Game.Companion.IsMovingUpward = res.IsMovingUpward;
                    Game.Companion.IsMovingDownward = res.IsMovingDownward;
                    Game.first_part_code = res.Combination.First;
                    Game.second_part_code = res.Combination.Second;
                    Game.third_part_code = res.Combination.Third;
                    Game.fourth_part_code = res.Combination.Fourth;

                }
            }
        }
    }
public static async void ReceiveRole()
    {
        if (call is null) throw new ArgumentNullException("call is null");
        await foreach (var res in call.ResponseStream.ReadAllAsync())
        {
            if (res.CompanionsName == "")
            {
                Game.Me.Role = res.Role ? Role.Performer : Role.Assistant;
                Game.Companion.Role = res.Role ? Role.Assistant : Role.Performer;

                Game.Me.X = 0.05 * SystemParameters.VirtualScreenWidth;
                Game.Me.Y = Game.Me.Role == Role.Performer ? 0.33 * SystemParameters.VirtualScreenHeight : 0.66 * SystemParameters.VirtualScreenHeight;

                Game.Companion.X = 0.05 * SystemParameters.VirtualScreenWidth;
                Game.Companion.Y = Game.Companion.Role == Role.Assistant ? 0.66 * SystemParameters.VirtualScreenHeight : 0.33 * SystemParameters.VirtualScreenHeight;  
            }
            else
            {
                Game.Companion.Name = res.CompanionsName;
            }
            return;
        }
    }
}
