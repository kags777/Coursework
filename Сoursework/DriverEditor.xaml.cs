using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class DriverEditor : UserControl
    {
        private DataStore store;

        public DriverEditor(DataStore store)
        {
            InitializeComponent();
            this.store = store;
            store.Load();
            DriverListBox.ItemsSource = store.Drivers;
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            var driver = new Driver { NameDriver = "Новый водитель", Age = 30, Experience = 5 };
            store.Drivers.Add(driver);
            DriverListBox.Items.Refresh();
            store.Save();
        }

        private void RemoveDriver_Click(object sender, RoutedEventArgs e)
        {
            if (DriverListBox.SelectedItem is Driver driver)
            {
                store.Drivers.Remove(driver);
                DriverListBox.Items.Refresh();
                store.Save();
            }
        }
    }
}
