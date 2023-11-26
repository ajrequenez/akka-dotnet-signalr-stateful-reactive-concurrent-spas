using Akka.Actor;
using Akka.Hosting;
using Game.ActorModel.Actors;
using Game.ActorModel.Messages;
using Microsoft.AspNetCore.SignalR;

namespace Game.Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly IActorRef _signalRActor;

        public GameHub(IRequiredActor<SignalRActor> signalRActor)
        {
            _signalRActor = signalRActor.ActorRef;
        }

        public void JoinGame(string PlayerName)
        {
            _signalRActor.Tell(new JoinGameMessage(PlayerName));
        }

        public void AttackPlayer(string PlayerName) 
        {

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
