using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class ActiveOrdersEditor : UserControl
    {
        private DataStore store;

        public ActiveOrdersEditor(DataStore store)
        {
            InitializeComponent();
            this.store = store;
            store.Load();
            OrdersListBox.ItemsSource = store.Orders.Where(o => o.DateUnloading >= DateTime.Now).ToList();
        }

        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListBox.SelectedItem is Order order)
            {
                // Освобождаем машину и водителя
                if (order.AssignedCar != null)
                    order.AssignedCar.BusyPeriods.RemoveAll(p => p.Item1 == order.DateLoading && p.Item2 == order.DateUnloading);
                if (order.AssignedDriver != null)
                    order.AssignedDriver.BusyPeriods.RemoveAll(p => p.Item1 == order.DateLoading && p.Item2 == order.DateUnloading);

                store.Orders.Remove(order);
                store.Save();
                OrdersListBox.ItemsSource = store.Orders.Where(o => o.DateUnloading >= DateTime.Now).ToList();
                OrdersListBox.Items.Refresh();
                MessageBox.Show("Заказ завершён");
            }
        }
    }
}
