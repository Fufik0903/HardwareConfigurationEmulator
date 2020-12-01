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
    /// Логика взаимодействия для PageOfRegistration.xaml
    /// </summary>
    public partial class PageOfRegistration : Page
    {
        public PageOfRegistration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || Mail.Text == "" || Password.Password == "" || RepeatPassword.Password == "")
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }
            else if (Password.Password != RepeatPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            try
            {
                User newUser = new User();
                newUser.Email = Mail.Text;
                newUser.Login = Login.Text;
                newUser.Password = Password.Password;
                newUser.NicId = 1;
                newUser.RouterId = 1;
                newUser.SwitchId = 1;
                Database.Connection.User.Add(newUser);
                Database.Connection.SaveChanges();
                MessageBox.Show("Успешно");
                NavigationService.Navigate(new PageOfAuthorization());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
