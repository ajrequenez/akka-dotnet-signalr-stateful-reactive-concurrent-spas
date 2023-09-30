using Akka.Actor;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;

        public GameControllerActor()
        {
            _players = new Dictionary<string, IActorRef>();

            Receive<JoinGameMessage>(onJoinGame);

            Receive<AttackPlayerMessage>(onAttackPlayer);
        }

        private void onAttackPlayer(AttackPlayerMessage message) 
        {
            _players[message.PlayerName].Forward(message);
        }

        private void onJoinGame(JoinGameMessage message)
        {
            var playerName = message.PlayerName;

            if(!_players.ContainsKey(playerName))
            {
                IActorRef newPlayer = Context.ActorOf(PlayerActor.Props(playerName));
                _players[playerName] = newPlayer;

                foreach(var player in _players.Values)
                {
                    player.Tell(new RequestPlayerStatusMessage(), Sender);
                }
            }
        }

        public static Props Props() => Akka.Actor.Props.Create(()=> new GameControllerActor());
    }
}
