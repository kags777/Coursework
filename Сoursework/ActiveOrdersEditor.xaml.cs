using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class ActiveOrdersEditor : UserControl
    {
        private DataStore store;
        private MainWindow main;

        public ActiveOrdersEditor(DataStore ds, MainWindow mw)
        {
            InitializeComponent();
            store = ds;
            main = mw;
            Refresh();
        }

        public void Refresh()
        {
            OrdersList.ItemsSource = null;
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == "Активен")
                .ToList();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersList.SelectedItem as Order;
            if (order == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            // освобождаем водителя
            if (order.AssignedDriver != null)
            {
                order.AssignedDriver.FreePeriod(order.StartDate, order.EndDate);
            }

            // освобождаем машину
            if (order.AssignedCar != null)
            {
                order.AssignedCar.FreePeriod(order.StartDate, order.EndDate);
            }

            // меняем статус
            order.Status = "Выполнен";

            store.Save();

            main.RefreshActiveOrders();
            main.RefreshCompletedOrders();

            MessageBox.Show("Заказ выполнен");
        }
    }
}
