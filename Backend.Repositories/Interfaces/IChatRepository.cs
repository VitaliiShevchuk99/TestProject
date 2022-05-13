using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto;

namespace Backend.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task AddMessage(ChatMessageDto chatMessage);
        Task<IEnumerable<ChatMessageDto>> GetAllMessages(string name);
    }
}
