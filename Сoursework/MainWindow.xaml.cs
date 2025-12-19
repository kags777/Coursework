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
            RightPanel.Content = new OrderEditor(store);
        }

        private void ManageCars_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new CarEditor(store);
        }

        private void ManageDrivers_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new DriverEditor(store);
        }

        private void ActiveOrders_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.Content = new ActiveOrdersEditor(store);
        }

        public void RefreshActiveOrders()
        {
            if (RightPanel.Content is ActiveOrdersEditor activeEditor)
                activeEditor.Refresh();
        }
    }
}
