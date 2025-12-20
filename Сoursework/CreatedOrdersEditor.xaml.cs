using System.Linq;
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
    }
}
