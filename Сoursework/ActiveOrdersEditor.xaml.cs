using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class ActiveOrdersEditor : UserControl
    {
        private DataStore store;

        public ActiveOrdersEditor(DataStore dataStore)
        {
            InitializeComponent();
            store = dataStore;
            Refresh();
        }

        public void Refresh()
        {
            OrdersListBox.ItemsSource = null;
            OrdersListBox.ItemsSource = store.Orders
                .Where(o => o.DateUnloading >= System.DateTime.Now)
                .ToList();
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListBox.SelectedItem is Order order)
            {
                store.Orders.Remove(order);

                if (order.AssignedCar != null)
                    order.AssignedCar.BusyPeriods.RemoveAll(p =>
                        p.Item1 == order.DateLoading && p.Item2 == order.DateUnloading);

                if (order.AssignedDriver != null)
                    order.AssignedDriver.BusyPeriods.RemoveAll(p =>
                        p.Item1 == order.DateLoading && p.Item2 == order.DateUnloading);

                store.Save();
                Refresh();
            }
        }
    }
}
