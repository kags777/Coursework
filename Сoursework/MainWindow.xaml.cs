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
            var editor = new OrderEditor(store);
            RightPanel.Content = editor;
        }

        private void ManageCars_Click(object sender, RoutedEventArgs e)
        {
            var editor = new CarEditor(store);
            RightPanel.Content = editor;
        }

        private void ManageDrivers_Click(object sender, RoutedEventArgs e)
        {
            var editor = new DriverEditor(store);
            RightPanel.Content = editor;
        }

        private void ActiveOrders_Click(object sender, RoutedEventArgs e)
        {
            var editor = new ActiveOrdersEditor(store);
            RightPanel.Content = editor;
        }
    }
}
