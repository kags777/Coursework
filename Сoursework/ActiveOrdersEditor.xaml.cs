using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class ActiveOrdersEditor : UserControl
    {
        private readonly DataStore store;
        private readonly MainWindow main;

        public ActiveOrdersEditor(DataStore ds, MainWindow mw)
        {
            InitializeComponent();
            store = ds;
            main = mw;
            Refresh();
        }

        // обновляем список активных заказов
        public void Refresh()
        {
            OrdersList.ItemsSource = null;
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == OrderStatus.Active)
                .ToList();
        }

        // ===== ПРОСМОТР ЗАКАЗА =====
        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersList.SelectedItem as Order;
            if (order == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            Window win = new Window
            {
                Title = "Просмотр активного заказа",
                Content = new OrderViewer(order),
                Width = 500,
                Height = 600
            };

            win.ShowDialog();
        }

        // ===== ЗАВЕРШЕНИЕ ЗАКАЗА =====
        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersList.SelectedItem as Order;
            if (order == null)
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            // освобождаем водителя
            if (order.AssignedDriver != null)
                order.AssignedDriver.FreePeriod(order.StartDate, order.EndDate);

            // освобождаем машину и увеличиваем пробег
            if (order.AssignedCar != null)
            {
                order.AssignedCar.FreePeriod(order.StartDate, order.EndDate);
                order.AssignedCar.MileageCalc((int)order.RouteLength);
            }

            // меняем статус
            order.Status = OrderStatus.Completed;

            store.Save();

            // обновляем интерфейс
            main.RefreshActiveOrders();
            main.RefreshCompletedOrders();
            main.RefreshCars();

            MessageBox.Show(
                "Заказ выполнен!\n" +
                "Пробег машины увеличен на " + order.RouteLength + " км");
        }
    }
}
