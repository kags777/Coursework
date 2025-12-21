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

        // Перезагружает список активных заказов
        public void Refresh()
        {
            OrdersList.ItemsSource = null;
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == OrderStatus.Active)
                .ToList();
        }

        // Обработчик кнопки "Завершить заказ"
        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!(OrdersList.SelectedItem is Order order))
            {
                MessageBox.Show("Выберите заказ");
                return;
            }

            // освобождаем водителя
            order.AssignedDriver?.FreePeriod(order.StartDate, order.EndDate);

            // освобождаем машину и увеличиваем пробег
            if (order.AssignedCar != null)
            {
                order.AssignedCar.FreePeriod(order.StartDate, order.EndDate);
                order.AssignedCar.MileageCalc((int)order.RouteLength);
            }

            // меняем статус
            order.Status = OrderStatus.Completed;

            store.Save();

            // обновляем UI
            main.RefreshActiveOrders();
            main.RefreshCompletedOrders();
            main.RefreshCars(); // обновляем список машин, чтобы показать новый пробег

            MessageBox.Show(
                $"Заказ выполнен!\n" +
                $"Пробег машины увеличен на {order.RouteLength} км");
        }
    }
}
