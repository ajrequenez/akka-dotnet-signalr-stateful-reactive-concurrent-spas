using Akka.Actor;
using Akka.Remote;
using Akka.Hosting;
using Game.ActorModel.Actors;
using Game.ActorModel.Messages;
using Microsoft.Extensions.Hosting;

namespace Game.State
{
    internal class Program
    {
        //private static ActorSystem? ActorSystemInstance;
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Starting Actor System...");

            //ActorSystemInstance = ActorSystem.Create("GameStateSystem");
            //var gameController = ActorSystemInstance.ActorOf<GameControllerActor>("GameController");
            //gameController.Tell(new RequestPlayerStatusMessage());

            //Console.ReadLine();


            var hostBuilder = new HostBuilder();

            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddAkka("GameStateSystem", (builder, sp) =>
                {
                    builder
                        .WithActors((system, registry, resolver) =>
                        {
                            var gameController = system.ActorOf(Props.Create(() => new GameControllerActor()), "game-controller");
                            registry.Register<GameControllerActor>(gameController);
                        });
                });
            });

            var host = hostBuilder.Build();
            await host.RunAsync();
        }
    }
}