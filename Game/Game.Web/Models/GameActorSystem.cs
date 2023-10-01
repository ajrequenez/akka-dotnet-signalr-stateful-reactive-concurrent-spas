using Akka.Actor;
using Akka.Dispatch.SysMsg;
using Game.ActorModel.Actors;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem? GameSystem;

        public static void Create()
        {
            GameSystem = Akka.Actor.ActorSystem.Create("GameSystem");
            
            ActorReferences.GameController = GameSystem.ActorOf<GameControllerActor>();
        }

        public static void Shutdown()
        {
            GameSystem?.Terminate();
        }

        public static class ActorReferences
        {
            public static IActorRef? GameController { get; set; } 
        }
    }
}
