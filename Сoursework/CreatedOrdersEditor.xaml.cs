using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class CreatedOrdersEditor : UserControl
    {
        private DataStore store;
        private MainWindow main;

        public CreatedOrdersEditor(DataStore ds, MainWindow mw)
        {
            InitializeComponent();
            store = ds;
            main = mw;
            Refresh();
        }

        private void Refresh()
        {
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == "Created")
                .ToList();
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order order)
            {
                order.Status = "Active";
                store.Save();
                Refresh();
                main.RefreshActiveOrders();
            }
        }
    }
}
