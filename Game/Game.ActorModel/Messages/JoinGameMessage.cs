namespace Game.ActorModel.Messages
{
    internal sealed class JoinGameMessage
    {
        public string PlayerName { get; }
        public JoinGameMessage(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
