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
    private static AsyncDuplexStreamingCall<Coordinates, Coordinates>? call;

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

    public static async void StartServer()
    {

    }

    public static void StopServer()
    {
        /* Остановка сервера. */
    }

    public static void Send(double _x, double _y)
    {
        if (call is null) throw new ArgumentNullException("call is null");
        call.RequestStream.WriteAsync(new Coordinates() { XPosition = _x, YPosition = _y });
    }

    public static async void Receive()
    {
        if (call is null) throw new ArgumentNullException("call is null");

        while (IsConnected)
            await foreach (var res in call.ResponseStream.ReadAllAsync())
            { 
                Game.Companion.X = res.XPosition;
                Game.Companion.Y = res.YPosition;
            }
    }
}
