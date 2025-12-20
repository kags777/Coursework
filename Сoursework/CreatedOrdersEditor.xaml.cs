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
                OrderEditor editor = new OrderEditor(store, main, selected);
                Window win = new Window
                {
                    Title = "Редактирование заказа",
                    Content = editor,
                    Height = 800,
                    Width = 400
                };
                if (win.ShowDialog() == true)
                {
                    store.Save(); // сохраняем изменения после редактирования
                    Refresh();
                }
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order selected)
            {
                if (MessageBox.Show("Удалить заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    store.Orders.Remove(selected);
                    store.Save();
                    Refresh();
                }
            }
        }

        private void ActivateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order selected)
            {
                selected.Status = "Active";
                store.Save();
                Refresh();
                MessageBox.Show("Заказ отправлен на выполнение!");
            }
        }
    }
}
