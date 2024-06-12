using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Started test...");
            // Create a connection to the GameHub
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/game")
                .Build();

            // Start the connection
            await connection.StartAsync();
            Console.WriteLine("Connected to GameHub");

            // Example method call to request a new game
            await connection.InvokeAsync("RequestNewGame", "player-guid");
            Console.WriteLine("New game requested");

            // Listen for messages from the hub
            connection.On<string, string>("NewGame", (gameGuid, playerSymbol) =>
            {
                Console.WriteLine($"New game started: {gameGuid}, Player Symbol: {playerSymbol}");
            });

            // Prevent the console from closing
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            // Stop the connection when done
            await connection.StopAsync();
        }
    }
}

