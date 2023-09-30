namespace Game.ActorModel.Messages
{
    public sealed class JoinGameMessage
    {
        public string PlayerName { get; }
        public JoinGameMessage(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
