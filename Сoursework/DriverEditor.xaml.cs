using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class DriverEditor : UserControl
    {
        private DataStore store;
        private MainWindow main;

        public DriverEditor(DataStore ds, MainWindow mw)
        {
            InitializeComponent();
            store = ds;
            main = mw;
            RefreshDriverList();
        }

        private void RefreshDriverList()
        {
            DriverListBox.ItemsSource = null;
            DriverListBox.ItemsSource = store.Drivers;
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            DriverWindow win = new DriverWindow();
            if (win.ShowDialog() == true)
            {
                store.Drivers.Add(win.Driver);
                store.Save();
                RefreshDriverList();
            }
        }

        private void EditDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriverListBox.SelectedItem is Driver selected)
            {
                DriverWindow win = new DriverWindow(selected);
                if (win.ShowDialog() == true)
                {
                    store.Save();
                    RefreshDriverList();
                }
            }
        }

        private void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriverListBox.SelectedItem is Driver selected)
            {
                if (MessageBox.Show($"Удалить водителя {selected.NameDriver}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    store.Drivers.Remove(selected);
                    store.Save();
                    RefreshDriverList();
                }
            }
        }
    }
}
