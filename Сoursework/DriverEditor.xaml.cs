using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class DriverEditor : UserControl
    {
        private DataStore store;

        public DriverEditor(DataStore ds)
        {
            InitializeComponent();
            store = ds;
            RefreshDrivers();
        }

        private void RefreshDrivers()
        {
            DriverList.ItemsSource = null;
            DriverList.ItemsSource = store.Drivers;
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            DriverWindow win = new DriverWindow();
            if (win.ShowDialog() == true)
            {
                store.AddDriver(win.Driver);
                RefreshDrivers();
            }
        }

        private void EditDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriverList.SelectedItem is Driver driver)
            {
                DriverWindow win = new DriverWindow(driver);
                if (win.ShowDialog() == true)
                {
                    RefreshDrivers();
                }
            }
        }

        private void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriverList.SelectedItem is Driver driver)
            {
                store.Drivers.Remove(driver);
                store.Save();
                RefreshDrivers();
            }
        }
    }
}
