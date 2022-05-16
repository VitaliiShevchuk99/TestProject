using System.Windows;
using Client.Controllers;
using Client.ViewModels;
using Shared.Dto;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _loginViewModel;
        public LoginWindow(LoginViewModel loginViewModel)
        { 
            DataContext = loginViewModel;
            InitializeComponent();
        }
    }
}
