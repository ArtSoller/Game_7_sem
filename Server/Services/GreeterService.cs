using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using System.Collections.Concurrent;
using System.IO;

namespace GrpcService1.Services;

internal record ChatClient
{
    public IServerStreamWriter<Coordinates>? StreamWriter { get; set; }
    public string? UserName { get; set; }
}


public class GreeterService : Greeter.GreeterBase
{
    private static ConcurrentDictionary<string, IServerStreamWriter<Coordinates>> clients = new();

    public override async Task ActionStream(IAsyncStreamReader<Coordinates> requestStream, IServerStreamWriter<Coordinates> responseStream, ServerCallContext context)
    {
        while (true)
        {
            var clientMessage = await ReadMessageWithTimeoutAsync(requestStream, Timeout.InfiniteTimeSpan);
            await SendBroadcastMessageAsync($"{clientMessage.XPosition} {clientMessage.YPosition}");
        }
    }

    private static async Task SendBroadcastMessageAsync(string messageBody)
    {
        var message = new Coordinates { XPosition = Convert.ToDouble(messageBody.Split()[0]),
                                        YPosition = Convert.ToDouble(messageBody.Split()[1])};
        Console.WriteLine(messageBody);
        var tasks = new List<Task>() { };
        foreach (KeyValuePair<string, IServerStreamWriter<Coordinates>> client in clients)
            tasks.Add(client.Value.WriteAsync(message));
        await Task.WhenAll(tasks);
    }

    private static async Task AddClientAsync(ChatClient chatClient)
    {
        var existingUser = clients.FirstOrDefault(c => c.Key == chatClient.UserName);
        if (existingUser.Key == null)
            clients.TryAdd(chatClient.UserName ?? "Unexpected user", chatClient.StreamWriter);
        await Task.CompletedTask;
    }


    // Должно работать.
    private static async Task RemoveClientAsync(ChatClient chatClient)
    {
        var existingUser = clients.FirstOrDefault(c => c.Key == chatClient.UserName);
        if (existingUser.Key == null)
            Console.WriteLine("No such user");
        else
        {
            Console.WriteLine($"{existingUser.Key} left us.");
            clients.TryRemove(existingUser);
        }
        await Task.CompletedTask;
    }

    public async Task<Coordinates> ReadMessageWithTimeoutAsync(IAsyncStreamReader<Coordinates> requestStream, TimeSpan timeout)
    {
        CancellationTokenSource cancellationTokenSource = new();

        try
        {
            cancellationTokenSource.CancelAfter(timeout);

            bool moveNext = await requestStream.MoveNext(cancellationTokenSource.Token);

            if (moveNext == false)
            {
                throw new Exception("connection dropped exception");
            }

            return requestStream.Current;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            Console.WriteLine("Что это?");
            throw new TimeoutException();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Вылет где надо: {ex}");
            throw new Exception();
        }
    }
}