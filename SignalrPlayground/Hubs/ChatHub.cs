using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalrPlayground.Hubs
{
    public class ChatHub : Hub
    {
        public void NewChatMessage(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public override Task OnConnected()
        {
            NewChatMessage(Context.ConnectionId, "Welcome to the chat!");
            return base.OnConnected();
        }
    }
}
