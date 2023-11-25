namespace Game.ActorModel.Messages
{
    public sealed class LoggingMessage
    {
        public string Message { get; }

        public LoggingMessage(string message)
        {
            Message = message;
        }
    }
}
