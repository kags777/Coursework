using System.Linq;
using System.Windows;

namespace Coursework
{
    public partial class MainWindow : Window
    {
        private DataStore store = new DataStore();

        public MainWindow()
        {
            InitializeComponent();
            store.Load();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var editor = new OrderEditor(store, this);
            RightPanel.Content = editor;
        }

        private void ManageCars_Click(object sender, RoutedEventArgs e)
        {
            var editor = new CarEditor(store, this);
            RightPanel.Content = editor;
        }

        private void ManageDrivers_Click(object sender, RoutedEventArgs e)
        {
            var editor = new DriverEditor(store, this);
            RightPanel.Content = editor;
        }

        private void CreatedOrders_Click(object sender, RoutedEventArgs e)
        {
            var editor = new CreatedOrdersEditor(store, this);
            RightPanel.Content = editor;
        }

        private void ActiveOrders_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new ActiveOrdersEditor(store, this);
        }


        private void CompletedOrders_Click(object sender, RoutedEventArgs e)
        {
            var editor = new CompletedOrdersEditor(store);
            RightPanel.Content = editor;
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Очистить весь JSON?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                store.Orders.Clear();
                store.Cars.Clear();
                store.Drivers.Clear();
                store.Save();
                MessageBox.Show("JSON очищен!");
                RightPanel.Content = null;
            }
        }
        public void RefreshCreatedOrders()
        {
            RightPanel.Content = new CreatedOrdersEditor(store, this);
        }

        public void RefreshActiveOrders()
        {
            RightPanel.Content = new ActiveOrdersEditor(store, this);
        }

        public void RefreshCompletedOrders()
        {
            RightPanel.Content = new CompletedOrdersEditor(store);
        }
        public void RefreshCars()
        {
            if (RightPanel.Content is CarEditor ce)
                ce.RefreshCarList();
        }
    }
}
