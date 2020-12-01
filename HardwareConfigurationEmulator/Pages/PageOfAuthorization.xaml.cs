using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HardwareConfigurationEmulator.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class PageOfAuthorization : Page
    {
        public PageOfAuthorization()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Login.Text) || String.IsNullOrEmpty(Password.Password))
            {
                MessageBox.Show("Заполните поля");
                return;
            }
            List<User> UserList = Database.Connection.User.ToList();
            foreach (var user in UserList)
            {
                if (user.Login == Login.Text && user.Password == Password.Password)
                {
                    Current.user = user;
                    NavigationService.Navigate(new PageOfPersonalAccount());
                    return;
                }
            }
            MessageBox.Show("Пользователь не найден");
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageOfRegistration());
        }
    }
}
