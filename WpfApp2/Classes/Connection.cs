using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public static void SetConnection(string address)
    {
        if (address is null) return;
        channel = GrpcChannel.ForAddress($"http://{address}");
        client = new Greeter.GreeterClient(channel);
        call = client.ActionStream();
        while (channel.State == ConnectivityState.Connecting)
            continue;
        if (channel.State == ConnectivityState.Ready)
            IsConnected = true;
    }
    public static void SendCoordinates(string _name, double _x, double _y)
    {
        if (call is null) throw new ArgumentNullException("call is null");
        call.RequestStream.WriteAsync(new Content() {Name = _name, X = _x, Y = _y });
    }
   public static async Task ReceiveCoordinates()
    {
        if (call is null) throw new ArgumentNullException("call is null");

        while (IsConnected)
            await foreach (var res in call.ResponseStream.ReadAllAsync())
            {
                Game.Companion.X = res.X;
                Game.Companion.Y = res.Y;
            }
    }

    public static async void ReceiveRole()
    {
        if (call is null) throw new ArgumentNullException("call is null");
        await foreach (var res in call.ResponseStream.ReadAllAsync())
        {
            Game.Me.Role = res.Role ? Role.Performer : Role.Assistant;
            Game.Companion.Role = res.Role ? Role.Assistant : Role.Performer;
        }        
    }
}
