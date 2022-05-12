using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Controllers;
using Client.Hubs;
using Shared.Dto;

namespace Client.ViewModels
{
    public class ChatViewModel
    {
        private string _currentName;
        private static ClientChatHub _chatHub;
        public ObservableCollection<ChatMessageDto> Messages { get; }
        public ObservableCollection<ChatMessageDto> MessagesForName { get; }
        public ObservableCollection<string> Names { get; }
        public ObservableCollection<string> NewConversationNames { get; }
        public ChatViewModel(ClientChatHub chatHub)
        {
            _chatHub = chatHub;
            NewConversationNames = new ObservableCollection<string>(); 
            Messages = new ObservableCollection<ChatMessageDto>();
            MessagesForName = new ObservableCollection<ChatMessageDto>();
            Names = new ObservableCollection<string>();
            GetAllMessages(new ChatController().GetAllMessages());
            _chatHub.MessageReceived += MessageReceived;
            _chatHub.GetAllMessages += GetAllMessages;
        }

        public async void SendMessage(ChatMessageDto chatMessage)
        {
            _currentName = chatMessage.Name;
            await _chatHub.SendMessageAsync(chatMessage);
        }

        private void GetAllMessages(IEnumerable<ChatMessageDto> messages)
        {
            foreach (var message in messages)
            {
                MessageReceived(message);
            }
        }
        private void MessageReceived(ChatMessageDto messageDto)
        {
            var name = messageDto.SenderName == CurrentSession.LoginedUser.Login
                ? messageDto.Name
                : messageDto.SenderName;

            if (!Names.Contains(name))
                    Names.Add(name);
            Messages.Add(messageDto);
            if (_currentName == name)
                GetMessagesForUser(_currentName);
        }

        public void GetMessagesForUser(string name)
        {
            MessagesForName.Clear();
            foreach (var message in Messages.Where(t =>
                         (t.Name == name || t.SenderName == name) && (t.Name == CurrentSession.LoginedUser.Login ||
                                                                      t.SenderName == CurrentSession.LoginedUser
                                                                          .Login)))
            {
                MessagesForName.Add(message);
            }
        }

        public static ChatViewModel CreatedConnectedViewModel(ClientChatHub chatService)
        {
            ChatViewModel viewModel = new ChatViewModel(chatService);
           
            return viewModel;
        }

        public void SelectChat()
        {
            NewConversationNames.Clear();
            var users = new UserController().GetAllUsers();
            foreach (var user in users)
            {
                NewConversationNames.Add(user.Login);
            }
        }
    }
}
