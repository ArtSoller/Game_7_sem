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
        channel = GrpcChannel.ForAddress($"http://{address}");
        client = new Greeter.GreeterClient(channel);
        call = client.ActionStream();
        while (channel.State == ConnectivityState.Connecting)
            continue;
        IsConnected = true;
    }

    public static async void StartStream(double _x, double _y)
    {
        await Send(_x, _y);
        await Receive();
    }

    public static async void StartServer()
    {

    }

    public static void StopServer()
    {
        /* Остановка сервера. */
    }

    private static async Task Send(double _x, double _y)
    {
        if (call is null) throw new ArgumentNullException("call is null");
        var isDisposed = false;
        while (!isDisposed)
            await call.RequestStream.WriteAsync(new Coordinates() { XPosition = _x, YPosition = _y });
    }

    public static async Task Receive()
    {
        if (call is null) throw new ArgumentNullException("call is null");
        double _x;
        double _y;
        while (IsConnected)
            await foreach (var res in call.ResponseStream.ReadAllAsync())
            { 
                _x = res.XPosition;
                _y = res.YPosition;
            }
    }

    //public static async void ConnectTo(string address)
    //{
    //    while (_isConnected)
    //    {
    //        if (channel.State == ConnectivityState.Ready)
    //        {
    //            var name = "NUBAS";
    //            await call.RequestStream.WriteAsync(new ActionRequest() { Name = name });

    //            var recieve = Task.Run(async () =>
    //            {
    //                while (_isConnected)
    //                    await foreach (var res in call.ResponseStream.ReadAllAsync())
    //                        Console.WriteLine(res.Answer);
    //            });

    //            var send = Task.Run(Send());
    //            await send;
    //            //if (isConnected) 
    //            await recieve;
    //        }
    //        else if (channel.State == ConnectivityState.TransientFailure)
    //        {
    //            Console.WriteLine("Error during connection");
    //            Console.WriteLine("Type localhost or required ip");
    //            address = Console.ReadLine();
    //        }
    //    }
    //    Console.WriteLine("Type any key to quit");
    //    Console.ReadKey();
}
