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
    /// Логика взаимодействия для FormForAddOrEditRouter.xaml
    /// </summary>
    public partial class FormForAddOrEditRouter : Page
    {
        public Router OldRouter { get; set; }
        public Router MyRouter { get; set; }
        public List<string> TypeOfRoutingTableList { get; set; }

        public FormForAddOrEditRouter(Router myRouter)
        {
            InitializeComponent();
            Mail.DataContext = Current.user;

            CanSupportDHCPComboBox.Items.Add("true");
            CanSupportDHCPComboBox.Items.Add("false");
            MyRouter = myRouter;
            OldRouter = myRouter;
            TypeOfRoutingTableList = Database.Connection.TypeOfRoutingTable.Select(t => t.Title).ToList();


            if (String.IsNullOrEmpty(myRouter.Title))
            {
                SaveButton.Content = "Добавить";
            }
            if (myRouter.Path == null)
            {
                HardwareImage.Visibility = Visibility.Hidden;
            }
            else
            {
                HardwareImage.Source = new BitmapImage(new Uri(myRouter.Path));
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
            if (TitleTextBox.Text == "" || SpeedThroughCableTextBox.Text == "")
            {
                MessageBox.Show("Должны быть заполнены все поля");
                return;
            }

            try
            {
                Router newRouter = new Router();
                newRouter.Title = TitleTextBox.Text;
                newRouter.SpeedThroughCable = Convert.ToInt32(SpeedThroughCableTextBox.Text);
                newRouter.IdTypeOfRoutingTable = Database.Connection.TypeOfRoutingTable.First(t => t.Title == TypeOfRoutingTableComboBox.SelectedItem.ToString()).Id;
                newRouter.CanSupportDHCP = CanSupportDHCPComboBox.SelectedItem.ToString() == "true" ? true : false;

                if (SaveButton.Content.ToString() == "Добавить")
                {
                    Database.Connection.Router.Add(newRouter);
                }
                Database.Connection.SaveChanges();
                MessageBox.Show("Успешное сохранение");
                NavigationService.Navigate(new PageOfHardware("Маршрутизатор"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
