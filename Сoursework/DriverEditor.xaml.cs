using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class DriverEditor : UserControl
    {
        private DataStore store;

        public DriverEditor(DataStore dataStore)
        {
            InitializeComponent();
            store = dataStore;
            Refresh();
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            var window = new DriverWindow();
            if (window.ShowDialog() == true)
            {
                store.AddDriver(window.Driver);
                Refresh();
            }
        }

        public void Refresh()
        {
            DriverList.ItemsSource = null;
            DriverList.ItemsSource = store.Drivers;
            DriverList.DisplayMemberPath = "NameDriver";
        }
    }
}
