using System;
using System.Windows;
using System.Windows.Controls;
using Client.ViewModels;
using Shared.Dto;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private static ChatViewModel _chatViewModel;
        private static bool _newConversation;
        private string _userReceiverName;

        public ChatWindow(ChatViewModel chatViewModel)
        {
            DataContext = chatViewModel;
            _chatViewModel = chatViewModel;
            InitializeComponent();
            ActiveChats.Visibility = Visibility.Visible;
            SelectUser.Visibility = Visibility.Hidden;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (_userReceiverName != null)
            {
                var message = new ChatMessageDto()
                {
                    Message = MessageBox.Text,
                    SenderName = CurrentSession.LoginedUser.Login,
                    Name = _userReceiverName,
                    MessageTime = DateTime.Now
                };
                _chatViewModel.SendMessage(message);
                MessageBox.Clear();
            }
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            _userReceiverName = button.Content.ToString();
            _chatViewModel.GetMessagesForUser(button.Content.ToString());
            ActiveChats.Visibility = Visibility.Visible;
            SelectUser.Visibility = Visibility.Hidden;
        }

        private void NewConversation(object sender, RoutedEventArgs e)
        {
            _newConversation = !_newConversation;
            if (_newConversation)
            {
                SelectUser.Visibility = Visibility.Visible;
                ActiveChats.Visibility = Visibility.Hidden;
                _chatViewModel.SelectChat();
            }
            else
            {
                ActiveChats.Visibility = Visibility.Visible;
                SelectUser.Visibility = Visibility.Hidden;
            }
        }
    }
}