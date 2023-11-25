using Akka.Actor;
using Akka.Event;
using Game.ActorModel.Messages;
using System.Configuration;

namespace Game.ActorModel.Actors
{
    public class EchoActor : ReceiveActor
    {
        private ILoggingAdapter _logger = Context.GetLogger();

        public EchoActor() {

            Receive<LoggingMessage>(m =>
            {
                _logger.Info(m.Message);
                //Sender.Tell(m);
            });
        }

        protected override void PreStart()
        {
            _logger.Info("Echo Starting...");
            base.PreStart();
        }

        public static Props Props() => Akka.Actor.Props.Create(() =>  new EchoActor());
    }
}
