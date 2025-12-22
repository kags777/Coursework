using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class CompletedOrdersEditor : UserControl
    {
        private DataStore store;

        public CompletedOrdersEditor(DataStore ds)
        {
            InitializeComponent();
            store = ds;
            Refresh();
        }

        private void Refresh()
        {
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == OrderStatus.Completed)
                .ToList();
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order order)
            {
                Window win = new Window
                {
                    Title = "Выполненный заказ",
                    Content = new OrderViewer(order),
                    Width = 500,
                    Height = 600
                };
                win.ShowDialog();
            }
        }
    }
}
