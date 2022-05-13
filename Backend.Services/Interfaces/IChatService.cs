using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto;

namespace Backend.Services.Interfaces
{
    public interface IChatService
    {
        Task AddMessageAsync(ChatMessageDto chatMessage);
        Task<IEnumerable<ChatMessageDto>> GetAllMessages(string name);
    }
}
