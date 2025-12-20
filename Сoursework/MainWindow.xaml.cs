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

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Очистить весь JSON?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                store.Orders.Clear();
                store.Cars.Clear();
                store.Drivers.Clear();
                store.Save();
                MessageBox.Show("JSON очищен!");

                // Очистка текущей панели и обновление вкладок
                RightPanel.Content = null;
                RefreshCreatedOrders();
            }
        }

        public void RefreshCreatedOrders()
        {
            if (RightPanel.Content is CreatedOrdersEditor editor)
            {
                editor.Refresh();
            }
        }
    }
}
