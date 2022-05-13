using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Context;
using Backend.Data.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace Backend.Repositories.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DataBaseContext _dbContext;

        public ChatRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddMessage(ChatMessageDto chatMessage)
        {
            var userss = new List<UserModel>();
            userss.Add(await _dbContext.UserModels.FirstOrDefaultAsync(t => t.Login == chatMessage.Name));
            userss.Add(await _dbContext.UserModels.FirstOrDefaultAsync(t => t.Login == chatMessage.SenderName));

            var messageDb = new Message
            {
                Users = userss,
                MessageTime = chatMessage.MessageTime,
                SendedMessage = chatMessage.Message
            };
            await _dbContext.Messages.AddAsync(messageDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessageDto>> GetAllMessages(string name)
        {
            var chatMessageList = new List<ChatMessageDto>();
            var result = await _dbContext.Messages.Include(t => t.Users).ToListAsync();
            foreach (var message in result)
                chatMessageList.Add(new ChatMessageDto
                {
                    Name = message.Users[0].Login,
                    SenderName = message.Users[1].Login,
                    MessageTime = message.MessageTime,
                    Message = message.SendedMessage
                });
            return chatMessageList.Where(t=>t.SenderName==name || t.Name == name);
        }
    }
}