using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace GrpcService1.Services;

internal record ChatClient
{
    public IServerStreamWriter<Content>? StreamWriter { get; set; }
    public string? UserName { get; set; }
}


public class GreeterService : Greeter.GreeterBase
{
    private static ConcurrentDictionary<string, IServerStreamWriter<Content>> clients = new();

    public override async Task ActionStream(IAsyncStreamReader<Content> requestStream, IServerStreamWriter<Content> responseStream, ServerCallContext context)
    {
        while (true)
        {
            var clientMessage = await ReadMessageWithTimeoutAsync(requestStream, Timeout.InfiniteTimeSpan);
            await AddClientAsync(new ChatClient
            {
                StreamWriter = responseStream,
                UserName = clientMessage.Name
            });

            //Console.WriteLine($"{clientMessage.Name} {clientMessage.X} {clientMessage.Y}");
            await SendBroadcastMessageAsync($"{clientMessage.Name} {clientMessage.X} {clientMessage.Y} {clientMessage.CompanionsName} {clientMessage.IsMovingLeftward} {clientMessage.IsMovingRightward} {clientMessage.IsMovingUpward} {clientMessage.IsMovingDownward} {clientMessage.Combination.First} {clientMessage.Combination.Second} {clientMessage.Combination.Third} {clientMessage.Combination.Fourth}");
        }
    }

    private static async Task SendBroadcastMessageAsync(string messageBody)
    {
        var message = new Content
        {
            Name = messageBody.Split()[0],
            X = Convert.ToDouble(messageBody.Split()[1]),
            Y = Convert.ToDouble(messageBody.Split()[2]),
            CompanionsName = messageBody.Split()[3],
            IsMovingLeftward = Convert.ToBoolean(messageBody.Split()[4]),
            IsMovingRightward = Convert.ToBoolean(messageBody.Split()[5]),
            IsMovingUpward = Convert.ToBoolean(messageBody.Split()[6]),
            IsMovingDownward = Convert.ToBoolean(messageBody.Split()[7]),
            Combination = new Code
            {
                First = messageBody.Split()[8],
                Second = messageBody.Split()[9],
                Third = messageBody.Split()[10],
                Fourth = messageBody.Split()[11],
            }
        };

        var tasks = new List<Task>() { };
        foreach (KeyValuePair<string, IServerStreamWriter<Content>> client in clients)
        {
            if (client.Key != message.Name)
                tasks.Add(client.Value.WriteAsync(message));
        }

        await Task.WhenAll(tasks);
    }

    private static async void SendRoleMessageAsync()
    {
        var assistantFree = true;
        foreach (KeyValuePair<string, IServerStreamWriter<Content>> client in clients)
        {
            if (assistantFree)
            {
                var message = new Content
                {
                    Role = assistantFree
                };

                client.Value.WriteAsync(message);
                assistantFree = false;
                await SendBroadcastMessageAsync($"{client.Key} {0.0} {0.0} {client.Key} {false} {false} {false} {false} {""} {""} {""} {""}");
            }
            else
            {
                var message = new Content
                {
                    Role = assistantFree
                };

                client.Value.WriteAsync(message);
                await SendBroadcastMessageAsync($"{client.Key} {0.0} {0.0} {client.Key} {false} {false} {false} {false} {""} {""} {""} {""}");
            }
        }
    }

    private static async Task AddClientAsync(ChatClient chatClient)
    {
        var existingUser = clients.FirstOrDefault(c => c.Key == chatClient.UserName);
        if (existingUser.Key == null)
        {
            clients.TryAdd(chatClient.UserName ?? "Unexpected user", chatClient.StreamWriter);
            if (clients.Count == 2)
            {
                SendRoleMessageAsync();
            }
        }

        await Task.CompletedTask;
    }


    // ������ ��������.
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

    public async Task<Content> ReadMessageWithTimeoutAsync(IAsyncStreamReader<Content> requestStream, TimeSpan timeout)
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
            Console.WriteLine("��� ���?");
            throw new TimeoutException();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"����� ��� ����: {ex}");
            throw new Exception();
        }
    }
}