using Microsoft.AspNetCore.SignalR;

namespace Game.Web.Hubs
{
    public class GameHubHelper
    {
        private readonly IHubContext<GameHub> _hub;

        public GameHubHelper(IHubContext<GameHub> hub)
        {
            _hub = hub;
        }

        public void PlayerJoined(string playerName, int playerHealth)
        {
            Console.WriteLine($"hubhelper: {playerName}:{playerHealth}");
            _hub.Clients.All.SendAsync("PlayerJoined", playerName, playerHealth);
        }

        internal void WriteMessage(string message)
        {
            _hub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
