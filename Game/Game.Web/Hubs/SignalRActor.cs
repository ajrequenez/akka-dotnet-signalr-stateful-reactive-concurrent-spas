using Akka.Actor;
using Akka.Event;
using Game.ActorModel.Messages;
using Game.Web.Hubs;

namespace Game.Web
{
    public class SignalRActor : ReceiveActor
    {
        private readonly IActorRef _gameController;
        private IServiceScope _scope;
        private readonly GameHubHelper _hubHelper;

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public SignalRActor(IActorRef gameController, IServiceProvider sp) {

            _scope = sp.CreateScope();
            _hubHelper = _scope.ServiceProvider.GetRequiredService<GameHubHelper>();
            _gameController = gameController;

            hubAvailable();
        }

        private void hubAvailable()
        {
            Receive<AttackPlayerMessage>(onAttackPlayerMessage);
            Receive<JoinGameMessage>(onJoinGameMessage);
            Receive<PlayerHealthChangedMessage>(onPlayerHealthChangedMessage);
            Receive<PlayerStatusMessage>(onPlayerStatusMessage);
        }

        private void onAttackPlayerMessage(AttackPlayerMessage message)
        {
            _gameController.Tell(message);
        }

        private void onJoinGameMessage(JoinGameMessage message)
        {
            _log.Info("signalR message received");
            _log.Info(message.PlayerName);
            _gameController.Tell(message);
        }

        private void onPlayerHealthChangedMessage(PlayerHealthChangedMessage message)
        {
            //_gameEventsPusher.UpdatePlayerHealth(message.PlayerName, message.Health);
        }

        private void onPlayerStatusMessage(PlayerStatusMessage message)
        {
            _log.Info("player statu message received");
            _log.Info($"{message.PlayerName}:{message.Health}");

            _hubHelper.PlayerJoined(message.PlayerName, message.Health);
        }

    }
}
