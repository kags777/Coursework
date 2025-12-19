using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class OrderEditor : UserControl
    {
        private DataStore store;
        public ObservableCollection<Cargo> CargoList { get; set; } = new ObservableCollection<Cargo>();

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

        private void SetClient_Click(object sender, RoutedEventArgs e)
        {
            var window = new ClientWindow();
            if (window.ShowDialog() == true)
            {
                var order = DataContext as Order;
                order.Client = window.Client;
            }
        }

        private void AddCargo_Click(object sender, RoutedEventArgs e)
        {
            var window = new CargoWindow();
            if (window.ShowDialog() == true)
            {
                CargoList.Add(window.Cargo);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var order = DataContext as Order;
            if (order != null)
            {
                order.Loads = CargoList.ToList();
                order.AssignedCar = CarComboBox.SelectedItem as Car;
                order.AssignedDriver = DriverComboBox.SelectedItem as Driver;

                if (order.AssignedCar != null)
                    order.AssignedCar.BusyPeriods.Add(Tuple.Create(order.DateLoading, order.DateUnloading));
                if (order.AssignedDriver != null)
                    order.AssignedDriver.BusyPeriods.Add(Tuple.Create(order.DateLoading, order.DateUnloading));

                store.AddOrder(order);
                store.Save();

                if (Application.Current.MainWindow is MainWindow main)
                    main.RefreshActiveOrders();

                MessageBox.Show("Заказ сохранён!");
            }
        }
    }
}
