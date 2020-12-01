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
    /// Логика взаимодействия для PageOfHardware.xaml
    /// </summary>
    public partial class PageOfHardware : Page
    {
        public PageOfHardware(string hardwareTitle)
        {
            InitializeComponent();
            DataContext = this;
            TitleCategory.Content = hardwareTitle;
            Mail.DataContext = Current.user;

            switch (hardwareTitle)
            {
                case "Коммутатор":
                    List<Switch> SwitchList = Database.Connection.Switch.ToList();
                    FillDatagridSwitch(MainDataGrid, SwitchList);
                    MainDataGrid.ItemsSource = SwitchList;
                    break;
                case "Маршрутизатор":
                    List<Router> RouterList = Database.Connection.Router.ToList();
                    FillDatagridRouter(MainDataGrid, RouterList);
                    MainDataGrid.ItemsSource = RouterList;
                    break;
                case "Сетевой адаптер":
                    List<NetworkInterfaceController> NICList = Database.Connection.NetworkInterfaceController.ToList();
                    FillDatagridNIC(MainDataGrid, NICList);
                    MainDataGrid.ItemsSource = NICList;
                    break;
            }
        }

        private void FillDatagridSwitch(DataGrid dataGrid, List<Switch> HardwareList)
        {
            DataGridTextColumn Color = new DataGridTextColumn();
            Color.Header = "Цвет";
            Color.Binding = new Binding("Color");

            DataGridTextColumn TypeOfBuffering = new DataGridTextColumn();
            TypeOfBuffering.Header = "Тип буферизации";
            TypeOfBuffering.Binding = new Binding("TypeOfBuffering.Title");

            DataGridTextColumn BasicTransmissionSpeed = new DataGridTextColumn();
            BasicTransmissionSpeed.Header = "Базовая скорость передачи";
            BasicTransmissionSpeed.Binding = new Binding("BasicTransmissionSpeed");

            DataGridTextColumn CountOfSwitchPorts = new DataGridTextColumn();
            CountOfSwitchPorts.Header = "Количество портов";
            CountOfSwitchPorts.Binding = new Binding("CountOfSwitchPorts");

            dataGrid.Columns.Add(Color);
            dataGrid.Columns.Add(TypeOfBuffering);
            dataGrid.Columns.Add(BasicTransmissionSpeed);
            dataGrid.Columns.Add(CountOfSwitchPorts);
        }

        private void FillDatagridRouter(DataGrid dataGrid, List<Router> HardwareList)
        {
            DataGridTextColumn TypeOfRoutingTable = new DataGridTextColumn();
            TypeOfRoutingTable.Header = "Тип таблицы маршрутизации";
            TypeOfRoutingTable.Binding = new Binding("TypeOfRoutingTable.Title");


            DataGridTextColumn SpeedThroughCable = new DataGridTextColumn();
            SpeedThroughCable.Header = "Скорость через кабель";
            SpeedThroughCable.Binding = new Binding("SpeedThroughCable");

            DataGridTextColumn CanSupportDHCP = new DataGridTextColumn();
            CanSupportDHCP.Header = "Поддержка DHCP";
            CanSupportDHCP.Binding = new Binding("CanSupportDHCP");

            dataGrid.Columns.Add(TypeOfRoutingTable);
            dataGrid.Columns.Add(SpeedThroughCable);
            dataGrid.Columns.Add(CanSupportDHCP);
        }

        private void FillDatagridNIC(DataGrid dataGrid, List<NetworkInterfaceController> HardwareList)
        {
            DataGridTextColumn Generation = new DataGridTextColumn();
            Generation.Header = "Поколение";
            Generation.Binding = new Binding("Generation.Title");

            DataGridTextColumn Model = new DataGridTextColumn();
            Model.Header = "Модель";
            Model.Binding = new Binding("Model");

            DataGridTextColumn TypeOfAdapter = new DataGridTextColumn();
            TypeOfAdapter.Header = "Тип адаптера";
            TypeOfAdapter.Binding = new Binding("TypeOfAdapter");

            DataGridTextColumn TransmissionSpeed = new DataGridTextColumn();
            TransmissionSpeed.Header = "Скорость передачи";
            TransmissionSpeed.Binding = new Binding("TransmissionSpeed");

            dataGrid.Columns.Add(Generation);
            dataGrid.Columns.Add(Model);
            dataGrid.Columns.Add(TypeOfAdapter);
            dataGrid.Columns.Add(TransmissionSpeed);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitleCategory.Content.ToString() == "Коммутатор")
            {
                NavigationService.Navigate(new FormForAddOrEditSwitch(new Switch()));
            }
            else if (TitleCategory.Content.ToString() == "Маршрутизатор")
            {
                NavigationService.Navigate(new FormForAddOrEditRouter(new Router()));
            }
            else if (TitleCategory.Content.ToString() == "Сетевой адаптер")
            {
                NavigationService.Navigate(new FormForAddOrNIC(new NetworkInterfaceController()));
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать элемент в таблице");
                return;
            }
            if (TitleCategory.Content.ToString() == "Коммутатор")
            {
                NavigationService.Navigate(new FormForAddOrEditSwitch(MainDataGrid.SelectedItem as Switch));
            }
            else if (TitleCategory.Content.ToString() == "Маршрутизатор")
            {
                NavigationService.Navigate(new FormForAddOrEditRouter(MainDataGrid.SelectedItem as Router));
            }
            else if (TitleCategory.Content.ToString() == "Сетевой адаптер")
            {
                NavigationService.Navigate(new FormForAddOrNIC(MainDataGrid.SelectedItem as NetworkInterfaceController));
            }
        }

        private void RemoveButtom_Click(object sender, RoutedEventArgs e)
        {
            if (MainDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать элемент в таблице");
                return;
            }
            MessageBoxResult boxResult = MessageBox.Show("Вы уверены, что хотите удалить данный элемент?", "Опасное действие", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (boxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (TitleCategory.Content.ToString() == "Коммутатор")
                    {
                        Database.Connection.Switch.Remove(MainDataGrid.SelectedItem as Switch);
                    }
                    else if (TitleCategory.Content.ToString() == "Маршрутизатор")
                    {
                        Database.Connection.Router.Remove(MainDataGrid.SelectedItem as Router);
                    }
                    else if (TitleCategory.Content.ToString() == "Сетевой адаптер")
                    {
                        Database.Connection.NetworkInterfaceController.Remove(MainDataGrid.SelectedItem as NetworkInterfaceController);
                    }
                    Database.Connection.SaveChanges();
                    NavigationService.Navigate(new PageOfHardware(TitleCategory.Content.ToString()));
                    MessageBox.Show("Успешное удаление");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
