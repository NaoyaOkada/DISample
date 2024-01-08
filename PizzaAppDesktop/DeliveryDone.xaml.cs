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
using System.Windows.Shapes;
using PizzaAppDBAccessLib.DataServices;
using PizzaAppDBAccessLib.Models;

namespace PizzaAppDesktop
{
    /// <summary>
    /// Interaction logic for DeliveryDone.xaml
    /// </summary>
    /// 


    public partial class DeliveryDone : Window
    {
        private readonly IDataService _dataService;
        private FullOrder? _fullOrder = null;

        public DeliveryDone(IDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
        }

        public void PopulateDeliveryInfo(FullOrder fullOrder)
        {
            DataContext = fullOrder;
        }

        private void deliveryIsDone_Click(object sender, RoutedEventArgs e)
        {
            var fullOrder = DataContext as FullOrder;
            _dataService.DeliveryDone(fullOrder!.Id);
            this.Close();
        }
    }
}
