using System;
using System.Windows;
using Shared.Dto;

namespace Client
{
    public static class CurrentSession
    {
        public static LoginResponceDto LoginedUser { get; set; }
        public static event Action UserLogined;
        public static void LoginUser(LoginResponceDto user)
        {
            LoginedUser = user;
            UserLogined?.Invoke();
        }

        public static void LoginError()
        {
            MessageBox.Show("Pishov nahui");
        }
    }
}
