using Akka.Actor;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class PlayerActor : ReceiveActor
    {
        const int DefaultHealth = 100;
        private string _playerName;
        private int _health;

        public PlayerActor(string playerName)
        {
            _playerName = playerName;
            _health = DefaultHealth;

            Receive<RequestPlayerStatusMessage>(onRequestPlayerStatus);

            Receive<AttackPlayerMessage>(onAttackPlayer);
        }

        private void onAttackPlayer(AttackPlayerMessage message) 
        {
            if(message.PlayerName.ToUpperInvariant() != _playerName.ToUpperInvariant()) { return;  }

            _health -= 20;
            Sender.Tell(new PlayerHealthChangedMessage(_playerName, _health));
        }

        private void onRequestPlayerStatus(RequestPlayerStatusMessage message)
        {
            Sender.Tell(new PlayerStatusMessage(_playerName, _health));
        }

        public static Props Props(string playerName) => Akka.Actor.Props.Create(() => new PlayerActor(playerName));
    }
}
