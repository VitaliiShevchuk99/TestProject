using System.Windows;
using Client.Controllers;
using Shared.Dto;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserController _userController;
        public LoginWindow(UserController userController)
        {
            _userController = userController;
            InitializeComponent();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            _userController.LoginUser(new UserDto()
            {
                Login = Login.Text,
                Password = Password.Text
            });
        }
    }
}
