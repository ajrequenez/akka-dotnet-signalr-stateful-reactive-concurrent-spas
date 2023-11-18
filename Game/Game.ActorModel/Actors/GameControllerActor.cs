using Akka.Actor;
using Akka.Event;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public GameControllerActor()
        {
            _players = new Dictionary<string, IActorRef>();
            _log.Info("GameControllerActor created...");

            Receive<JoinGameMessage>(onJoinGame);

            Receive<AttackPlayerMessage>(onAttackPlayer);
        }

        private void onAttackPlayer(AttackPlayerMessage message) 
        {
            _log.Info($"Attacking Player {0}", message.PlayerName);
            _players[message.PlayerName].Forward(message);
        }

        private void onJoinGame(JoinGameMessage message)
        {
            var playerName = message.PlayerName;

            _log.Info($"New player joining game...{0}", playerName);

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
