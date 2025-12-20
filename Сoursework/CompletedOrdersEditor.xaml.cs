using System.Linq;
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

        public void Refresh()
        {
            OrdersList.ItemsSource = null;
            OrdersList.ItemsSource = store.Orders
                .Where(o => o.Status == OrderStatus.Completed)
                .ToList();
        }
    }
}
