using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class ActiveOrdersEditor : UserControl
    {
        private DataStore store;

        public ActiveOrdersEditor(DataStore ds)
        {
            InitializeComponent();
            store = ds;
            Refresh();
        }

        public void Refresh()
        {
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == "Активен")
                .ToList();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedItem is Order order)
            {
                order.Status = "Завершён";
                store.Save();
                Refresh();
            }
        }
    }
}
