using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.CommandLineUtils;

namespace Dnp.SignalR.Client
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

            var baseUrl = "http://localhost:5000/chatHub?room=Test" ;

            Console.WriteLine("Connecting to {0}", baseUrl);
            var connection = new HubConnectionBuilder()
                .WithUrl(baseUrl)
                .WithConsoleLogger(LogLevel.Trace)
                .Build();

            await ConnectAsync(connection);
            
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
                await connection.SendAsync("Send", "Test", message);
                
            }

            await connection.SendAsync("Send", "Test", "Hallo From Console!");

            
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
