using System.Windows;

namespace Coursework
{
    public partial class MainWindow : Window
    {
        private DataStore store;

        public MainWindow()
        {
            InitializeComponent();
            store = new DataStore();
            store.Load();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new OrderEditor(store, this);
        }

        private void CreatedOrders_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new CreatedOrdersEditor(store, this);
        }

        private void ActiveOrders_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new ActiveOrdersEditor(store);
        }

        // 🔥 МАШИНЫ
        private void ManageCars_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new CarEditor(store);
        }

        // 🔥 ВОДИТЕЛИ
        private void ManageDrivers_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new DriverEditor(store);
        }

        // 🔥 ОЧИСТКА JSON
        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Очистить ВСЕ данные?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                store.ClearAll();
                RightPanel.Content = null;
            }
        }

        // 🔥 ОБНОВЛЕНИЕ АКТИВНЫХ ЗАКАЗОВ
        public void RefreshActiveOrders()
        {
            if (RightPanel.Content is ActiveOrdersEditor editor)
            {
                editor.Refresh();
            }
        }
    }
}
