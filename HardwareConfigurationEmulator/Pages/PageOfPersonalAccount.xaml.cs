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
    /// Логика взаимодействия для PageOfPersonalAccount.xaml
    /// </summary>
    public partial class PageOfPersonalAccount : Page
    {
        public List<string> Hardware { get; set; } = new List<string>();
        public PageOfPersonalAccount()
        {
            Hardware.Add("Коммутатор");
            Hardware.Add("Маршрутизатор");
            Hardware.Add("Сетевой адаптер");
            InitializeComponent();
            Mail.DataContext = Current.user;
            HardwareListView.ItemsSource = Hardware;
        }

        private void HardwareListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new PageOfHardware(HardwareListView.SelectedItem as string));
        }
    }
}
