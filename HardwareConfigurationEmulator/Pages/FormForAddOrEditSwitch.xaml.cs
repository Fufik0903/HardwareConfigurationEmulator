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
    /// Логика взаимодействия для FormForAddOrEdit.xaml
    /// </summary>
    public partial class FormForAddOrEditSwitch : Page
    {
        public Switch MySwitch { get; set; }
        public List<string> TypeOfBufferingList { get; set; }
        public FormForAddOrEditSwitch(Switch mySwitch)
        {
            MySwitch = mySwitch;
            TypeOfBufferingList = Database.Connection.TypeOfBuffering.Select(t => t.Title).ToList();
            InitializeComponent();

            Mail.DataContext = Current.user;


            if (String.IsNullOrEmpty(mySwitch.Title))
            {
                SaveButton.Content = "Добавить";
            }
            if (mySwitch.Path == null)
            {
                HardwareImage.Visibility = Visibility.Hidden;
            }
            else
            {
                HardwareImage.Source = new BitmapImage(new Uri(mySwitch.Path));
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
            if (TitleTextBox.Text == "" || ColorTextBox.Text == "" ||
                BasicTransmissionSpeedTextBox.Text == "" || CountOfSwitchPortsTextBox.Text == "")
            {
                MessageBox.Show("Должны быть заполнены все поля");
                return;
            }

            try
            {
                Switch newSwitch = new Switch();
                newSwitch.Title = TitleTextBox.Text;
                newSwitch.Color = ColorTextBox.Text;
                newSwitch.IdTypeOfBuffering = Database.Connection.TypeOfBuffering.First(t => t.Title == TypeOfBufferingComboBox.SelectedItem.ToString()).Id;
                newSwitch.BasicTransmissionSpeed = Convert.ToInt32(BasicTransmissionSpeedTextBox.Text);
                newSwitch.CountOfSwitchPorts = Convert.ToInt32(CountOfSwitchPortsTextBox.Text);
                newSwitch.Path = MySwitch.Path;

                if (SaveButton.Content.ToString() == "Добавить")
                {
                    Database.Connection.Switch.Add(newSwitch);
                }
                Database.Connection.SaveChanges();
                MessageBox.Show("Успешное сохранение");
                NavigationService.Navigate(new PageOfHardware("Коммутатор"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
