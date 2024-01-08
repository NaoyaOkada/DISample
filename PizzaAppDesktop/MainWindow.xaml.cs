using Microsoft.Extensions.DependencyInjection;
using PizzaAppDBAccessLib.DataServices;
using PizzaAppDBAccessLib.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaAppDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataService _dataService;


        public MainWindow(IDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            LoadPizzasToDeliver();
            
        }

        private void LoadPizzasToDeliver()
        {
            List<FullOrder> pizzas = _dataService.GetAllOrdersToDeliver();
            PizzaListView.ItemsSource = pizzas;
        }

        private void DeliveryIsButton_Click(object sender, RoutedEventArgs e)
        {
            var fullOrder = (FullOrder)((Button)e.Source).DataContext;
            if(null != fullOrder)
            {
                var deliveryWindow = App.ServiceProvider()?.GetService<DeliveryDone>();
                deliveryWindow?.PopulateDeliveryInfo(fullOrder);
                deliveryWindow?.Show();
            }
        }
    }
}