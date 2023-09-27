namespace Game.ActorModel.Messages
{
    internal sealed class AttackPlayerMessage
    {
        public string PlayerName { get; }

        public AttackPlayerMessage(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
