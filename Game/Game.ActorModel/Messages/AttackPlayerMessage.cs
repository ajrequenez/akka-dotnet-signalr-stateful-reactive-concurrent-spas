namespace Game.ActorModel.Messages
{
    public sealed class AttackPlayerMessage
    {
        public string PlayerName { get; }

        public AttackPlayerMessage(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
