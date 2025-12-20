using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class OrderEditor : UserControl
    {
        private DataStore store;
        private MainWindow main;
        private Order order;

        public OrderEditor(DataStore ds, MainWindow mw)
        {
            InitializeComponent();
            store = ds;
            main = mw;

            order = new Order();
            DataContext = order;

            RefreshCargo();
        }

        // 🔥 ВОТ ЕГО НЕ ХВАТАЛО
        private void AddCargo_Click(object sender, RoutedEventArgs e)
        {
            // пока простой тестовый груз
            Cargo cargo = new Cargo
            {
                Nomination = "Тестовый груз",
                Quantity = 1,
                Weight = 100,
                Cost = 10000,
                FragileCargo = false
            };

            order.Loads.Add(cargo);
            RefreshCargo();
        }

        private void RefreshCargo()
        {
            CargoList.ItemsSource = null;
            CargoList.ItemsSource = order.Loads;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            store.AddOrder(order);
            MessageBox.Show("Заказ создан");

            main.RefreshActiveOrders();
        }
    }
}
