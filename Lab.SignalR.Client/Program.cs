using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Lab.SignalR.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.Command("sendMessage", cmd => {
                cmd.OnExecute(() => SendMessage());
            });

            app.Execute(args);
        }

        private static async Task<int> SendMessage()
        {
            Console.ReadLine();

            var baseUrl = "http://localhost:5000/hubs/machines" ;

            Console.WriteLine("Connecting to {0}", baseUrl);
            var connection = new HubConnectionBuilder()
                .WithUrl(baseUrl + "?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0cmljaGxpbmdAZ214LmRlIiwianRpIjoiZTUzYjZmOWUtYzk0Ny00ZDY1LWJlZDAtNWViYmExNDEwMDM5IiwibmJmIjoxNTI3MTUyNzg1LCJleHAiOjE1MzIzMzY3ODUsImlzcyI6Ik1lIiwiYXVkIjoiRXZlcnlib2R5In0.WobxWlk-2crbBQSUBcY-eeAI2g1PhcxeEtrc9pfqJO8", opt => {
                    opt.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0cmljaGxpbmdAZ214LmRlIiwianRpIjoiZTUzYjZmOWUtYzk0Ny00ZDY1LWJlZDAtNWViYmExNDEwMDM5IiwibmJmIjoxNTI3MTUyNzg1LCJleHAiOjE1MzIzMzY3ODUsImlzcyI6Ik1lIiwiYXVkIjoiRXZlcnlib2R5In0.WobxWlk-2crbBQSUBcY-eeAI2g1PhcxeEtrc9pfqJO8");
                })
                .ConfigureLogging(logging => logging.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .Build();

            await connection.StartAsync();
            
            var closeTcs  = new CancellationTokenSource();
            Console.CancelKeyPress += async (sender, a) =>
            {
                a.Cancel = true;
                Console.WriteLine("Stopping loops...");
                await connection.DisposeAsync();
            };

            while (!closeTcs.IsCancellationRequested)
            {
                var message = Console.ReadLine();
                if (message == "QUIT")
                {
                    closeTcs.Cancel();
                    continue;
                }
                await connection.InvokeAsync("NotifyAll", message);
            }

            Console.ReadLine();

            return 0;
        }

        private static async Task ConnectAsync(HubConnection connection)
        {
            // Keep trying to until we can start
            while (true)
            {
                try
                {
                    await connection.StartAsync();
                    return;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
