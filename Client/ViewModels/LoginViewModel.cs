using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Client.Controllers;
using Client.Views;
using GalaSoft.MvvmLight.Command;
using Shared.Dto;

namespace Client.ViewModels
{
    public class LoginViewModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        private readonly UserController _userController;
        public ICommand LoginCommand => new RelayCommand(Login);// { get; private set; }

        public LoginViewModel(UserController userController)
        {
            _userController = userController;
        }

        private void Login()
        {
            _userController.LoginUser(new UserDto {Login = LoginName, Password = Password});
        }
    }
}
