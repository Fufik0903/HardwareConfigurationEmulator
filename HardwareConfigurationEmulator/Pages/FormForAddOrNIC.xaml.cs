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
    /// Логика взаимодействия для FormForAddOrNIC.xaml
    /// </summary>
    public partial class FormForAddOrNIC : Page
    {
        public NetworkInterfaceController MyNIC { get; set; }
        public List<string> GenerationList { get; set; }
        public FormForAddOrNIC(NetworkInterfaceController myNIC)
        {
            MyNIC = myNIC;
            GenerationList = Database.Connection.Generation.Select(g => g.Title).ToList();
            InitializeComponent();

            Mail.DataContext = Current.user;

            if (String.IsNullOrEmpty(myNIC.Title))
            {
                SaveButton.Content = "Добавить";
            }
            if (myNIC.Path == null)
            {
                HardwareImage.Visibility = Visibility.Hidden;
            }
            else
            {
                HardwareImage.Source = new BitmapImage(new Uri(myNIC.Path));
            }
            DataContext = this;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text == "" || ModelTextBox.Text == "" ||
                TransmissionSpeedTextBox.Text == "" || TypeOfAdapterTextBox.Text == "")
            {
                MessageBox.Show("Должны быть заполнены все поля");
                return;
            }

            try
            {
                NetworkInterfaceController newNIC = new NetworkInterfaceController();
                newNIC.Title = TitleTextBox.Text;
                newNIC.Model = ModelTextBox.Text;
                newNIC.TypeOfAdapter = TypeOfAdapterTextBox.Text;
                newNIC.TransmissionSpeed = Convert.ToInt32(TransmissionSpeedTextBox.Text);
                newNIC.GenerationId = Database.Connection.Generation.First(g => g.Title == GenerationComboBox.SelectedItem.ToString()).Id;

                if (SaveButton.Content.ToString() == "Добавить")
                {
                    Database.Connection.NetworkInterfaceController.Add(newNIC);
                }
                Database.Connection.SaveChanges();
            MessageBox.Show("Успешное сохранение");
            NavigationService.Navigate(new PageOfHardware("Сетевой адаптер"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
