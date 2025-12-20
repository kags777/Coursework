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

        public void Refresh()
        {
            OrdersList.ItemsSource = null;
            OrdersList.ItemsSource = store.Orders.Where(o => o.Status == "Создан").ToList();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order selected)
            {
                var editor = new OrderEditor(store, main, selected);
                main.RightPanel.Content = editor;
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order selected)
            {
                if (MessageBox.Show($"Удалить заказ {selected.DisplayName}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    store.Orders.Remove(selected);
                    store.Save();
                    Refresh();
                }
            }
        }

        private void StartOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order selected)
            {
                selected.Status = "Активен";
                store.Save();
                Refresh();
                main.RefreshActiveOrders(); // если в MainWindow есть метод для обновления активных заказов
            }
        }
    }
}
