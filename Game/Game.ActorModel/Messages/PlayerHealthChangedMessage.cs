namespace Game.ActorModel.Messages
{
    internal sealed class PlayerHealthChangedMessage
    {
        public string PlayerName { get; }
        public int Health { get; }

        public PlayerHealthChangedMessage(string playerName, int health)
        {
            PlayerName = playerName;
            Health = health;
        }
    }
}
