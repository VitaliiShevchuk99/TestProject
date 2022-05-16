using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client.Controllers;
using Client.Hubs;
using GalaSoft.MvvmLight.Command;
using Shared.Dto;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Client.Annotations;

namespace Client.ViewModels
{
    public class ChatViewModel:INotifyPropertyChanged
    {
        private string _currentName;
        private static bool _newConversation;
        private string _userReceiverName;
        private static ClientChatHub _chatHub;
        public Visibility SelectUserVisibility { get; set; }
        public Visibility ActiveChatsVisibility { get; set; }
        public string ChangeUserButton { get; set; }

        public string MessageToSend { get; set; }
        public string ChangeUserNameCommand { get; set; }
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

        public ICommand SendMessageCommand => new RelayCommand(SendMessage);
        public ICommand ChangeUserCommand => new RelayCommand<string>(ChangeUser);
        public ICommand NewConversationCommand => new RelayCommand(NewConversation);

        private void SendMessage()
        {
            if (_userReceiverName != null)
            {
                var message = new ChatMessageDto()
                {
                    Message = MessageToSend,
                    SenderName = CurrentSession.LoginedUser.Login,
                    Name = _userReceiverName,
                    MessageTime = DateTime.Now
                };
                SendMessage(message);
                MessageToSend= string.Empty;
            }
        }

        private void ChangeUser(string changeName)
        {
            _userReceiverName = changeName;
            GetMessagesForUser(changeName);
            ActiveChatsVisibility = Visibility.Visible;
            OnPropertyChanged("ActiveChatsVisibility");
            SelectUserVisibility = Visibility.Hidden;
            OnPropertyChanged("SelectUserVisibility");
        }

        private void NewConversation()
        {
            _newConversation = !_newConversation;
            if (_newConversation)
            {
                SelectUserVisibility = Visibility.Visible;
                OnPropertyChanged("SelectUserVisibility");
                ActiveChatsVisibility = Visibility.Hidden;
                OnPropertyChanged("ActiveChatsVisibility");
                SelectChat();
            }
            else
            {
                ActiveChatsVisibility = Visibility.Visible;
                OnPropertyChanged("ActiveChatsVisibility");
                SelectUserVisibility = Visibility.Hidden;
                OnPropertyChanged("SelectUserVisibility");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
