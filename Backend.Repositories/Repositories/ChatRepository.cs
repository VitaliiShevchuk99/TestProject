using System.Collections.Generic;
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
            var messageDb = new Message
            {
                ReceiverName = chatMessage.Name,
                SenderName = chatMessage.SenderName,
                MessageTime = chatMessage.MessageTime,
                SendedMessage = chatMessage.Message
            };
            await _dbContext.Messages.AddAsync(messageDb);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessageDto>> GetAllMessages()
        {
            var chatMessageList = new List<ChatMessageDto>();
            var result = await _dbContext.Messages.ToListAsync();
            foreach (var message in result)
                chatMessageList.Add(new ChatMessageDto
                {
                    Name = message.ReceiverName,
                    SenderName = message.SenderName,
                    MessageTime = message.MessageTime,
                    Message = message.SendedMessage
                });
            return chatMessageList;
        }
    }
}