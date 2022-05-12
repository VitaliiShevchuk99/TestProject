using System.Windows;
using Client.Controllers;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserController userController = new UserController();

        }
    }
}
