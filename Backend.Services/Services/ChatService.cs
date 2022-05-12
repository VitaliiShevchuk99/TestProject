using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Repositories.Interfaces;
using Backend.Repositories.Repositories;
using Backend.Services.Interfaces;
using Shared.Dto;

namespace Backend.Services.Services
{
    public class ChatService:IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task AddMessageAsync(ChatMessageDto chatMessage)
        {
            await _chatRepository.AddMessage(chatMessage);
        }

        public async Task<IEnumerable<ChatMessageDto>> GetAllMessages()
        {
            return await _chatRepository.GetAllMessages();
        }
    }
}
