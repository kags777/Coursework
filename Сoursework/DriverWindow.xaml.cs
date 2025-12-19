using System.Windows;

namespace Coursework
{
    public partial class DriverWindow : Window
    {
        public Driver Driver { get; private set; }

        public DriverWindow(Driver driver = null)
        {
            InitializeComponent();
            if (driver != null)
            {
                Driver = driver;
                PersNumber.Text = driver.PersNumber.ToString();
                NameDriver.Text = driver.NameDriver;
                Age.Text = driver.Age.ToString();
                Experience.Text = driver.Experience.ToString();
                Category.Text = driver.Category;
                Classic.Text = driver.Classic;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Driver == null) Driver = new Driver();
            Driver.PersNumber = int.TryParse(PersNumber.Text, out var pn) ? pn : 0;
            Driver.NameDriver = NameDriver.Text;
            Driver.Age = int.TryParse(Age.Text, out var age) ? age : 0;
            Driver.Experience = int.TryParse(Experience.Text, out var exp) ? exp : 0;
            Driver.Category = Category.Text;
            Driver.Classic = Classic.Text;

            DialogResult = true;
            Close();
        }
    }
}
