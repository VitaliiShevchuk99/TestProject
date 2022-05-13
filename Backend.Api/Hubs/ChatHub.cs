using System.Collections.Concurrent;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Shared.Dto;

namespace Backend.Api.Hubs
{
    public class ChatHub:Hub
    {
        public static ConcurrentDictionary<string, string> Connections = new ConcurrentDictionary<string, string>();
        private readonly IChatService _chatService;
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessageAsync(ChatMessageDto message)
        {
            await Clients.Client(Connections[message.SenderName]).SendAsync("ReceiveUpdateAsync", message);

            if (Connections.ContainsKey(message.Name))
                await Clients.Client(Connections[message.Name]).SendAsync("ReceiveUpdateAsync", message);
            await _chatService.AddMessageAsync(message);
        }

        public async Task GetAllMessagesAsync()
        {
            var messages = await _chatService.GetAllMessages(Context.User?.Identity.Name);
            await Clients.Client(Context.User?.Identity.Name).SendAsync("ReceiveMessagesAsync", messages);
        }

        public override Task OnConnectedAsync()
        {
            if (!Connections.ContainsKey(Context.ConnectionId))
            {
                Connections.TryAdd(Context.User?.Identity?.Name, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }
    }
}
