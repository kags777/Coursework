using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Coursework
{
    public partial class OrderEditor : UserControl
    {
        public ObservableCollection<Cargo> CargoList { get; set; } = new ObservableCollection<Cargo>();
        private DataStore store;

        public OrderEditor(DataStore store)
        {
            InitializeComponent();
            this.store = store;
            DataContext = new Order();
            CarComboBox.ItemsSource = store.Cars;
            CarComboBox.DisplayMemberPath = "Number";
            DriverComboBox.ItemsSource = store.Drivers;
            DriverComboBox.DisplayMemberPath = "NameDriver";
        }

        private void AddCargo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CargoList.Add(new Cargo { Nomination = "Новый груз" });
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var order = DataContext as Order;
            if (order != null)
            {
                order.Loads = CargoList.ToList();
                order.AssignedCar = CarComboBox.SelectedItem as Car;
                order.AssignedDriver = DriverComboBox.SelectedItem as Driver;

                // Блокируем машину и водителя на даты заказа
                if (order.AssignedCar != null)
                    order.AssignedCar.BusyPeriods.Add(Tuple.Create(order.DateLoading, order.DateUnloading));
                if (order.AssignedDriver != null)
                    order.AssignedDriver.BusyPeriods.Add(Tuple.Create(order.DateLoading, order.DateUnloading));

                store.AddOrder(order);
                store.Save();
                System.Windows.MessageBox.Show("Заказ сохранён!");
            }
        }
    }
}
