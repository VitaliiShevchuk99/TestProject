using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Dto;

namespace Client.Hubs
{
    public class ClientChatHub
    {
        private readonly HubConnection _connection;
        public event Action<ChatMessageDto> MessageReceived;
        public event Action<IEnumerable<ChatMessageDto>> GetAllMessages;

        public ClientChatHub(HubConnection connection)
        {
            _connection = connection;
            _connection.On<ChatMessageDto>("ReceiveUpdateAsync", (messageReceived) => MessageReceived?.Invoke(messageReceived));
            _connection.On<IEnumerable<ChatMessageDto>>("ReceiveMessagesAsync", (messageReceived) => GetAllMessages?.Invoke(messageReceived));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendMessageAsync(ChatMessageDto message)
        {
            await _connection.SendAsync("SendMessageAsync", message);
        }

        public async Task GetAllMessagesAsync()
        {
            await _connection.SendAsync("GetAllMessagesAsync");
        }
    }
}
