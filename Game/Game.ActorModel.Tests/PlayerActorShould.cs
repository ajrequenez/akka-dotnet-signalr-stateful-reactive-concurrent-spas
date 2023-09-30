using Akka.Actor;
using Akka.TestKit.Xunit2;
using Game.ActorModel.Actors;
using Game.ActorModel.Messages;
using Xunit.Sdk;

namespace Game.ActorModel.Tests
{
    public class PlayerActorShould : TestKit
    {
        [Fact]
        public void InitializeMetaData()
        {
            var probe = CreateTestProbe();
            var player = Sys.ActorOf(PlayerActor.Props("John Doe"));

            player.Tell(new RequestPlayerStatusMessage(), probe.Ref);

            var received = probe.ExpectMsg<PlayerStatusMessage>();

            Assert.NotNull(received);
            Assert.Equal("John Doe", received.PlayerName);
            Assert.Equal(100, received.Health);

        }

        [Fact]
        public void TakeDefaultDamageWhenAttacked()
        {
            var probe = CreateTestProbe();
            var player = Sys.ActorOf(PlayerActor.Props("James Doe"));

            player.Tell(new AttackPlayerMessage("James Doe"), probe.Ref);

            var received = probe.ExpectMsg<PlayerHealthChangedMessage>();

            Assert.NotNull(received);
            Assert.Equal("James Doe", received.PlayerName);
            Assert.Equal(80, received.Health);
        }

        [Fact]
        public void TakeNoDamageWhenNotAttackedPlayer()
        {
            var probe = CreateTestProbe();
            var player = Sys.ActorOf(PlayerActor.Props("John Doe"));

            player.Tell(new AttackPlayerMessage("Don Joe"), probe.Ref);

            probe.ExpectNoMsg();
        }
    }
}
