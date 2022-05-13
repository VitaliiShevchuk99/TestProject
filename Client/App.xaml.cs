using System.Threading.Tasks;
using System.Windows;
using Client.Controllers;
using Client.Hubs;
using Client.ViewModels;
using Client.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public LoginWindow loginWindow;
        protected override void OnStartup(StartupEventArgs e)
        {
            var userController = new UserController();
            CurrentSession.UserLogined += StartChat;
            loginWindow = new LoginWindow(userController);
            loginWindow.Show();
        }

        protected async void StartChat()
        {
            loginWindow.Close();
            var connection = new HubConnectionBuilder().WithUrl("https://localhost:5000/chathub",
                    options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(CurrentSession.LoginedUser.Token);
                    })
                .Build();
            var clientChatHub = new ClientChatHub(connection);

            await clientChatHub.Connect();

            
            var chatViewModel = ChatViewModel.CreatedConnectedViewModel(clientChatHub);
            var chatWindow = new ChatWindow(chatViewModel);
                        await clientChatHub.GetAllMessagesAsync();
            chatWindow.Show();

        }


    }
}
