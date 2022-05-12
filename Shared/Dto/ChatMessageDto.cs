using System;

namespace Shared.Dto
{
    public class ChatMessageDto
    {
        public string Name { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
