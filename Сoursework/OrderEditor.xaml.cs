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

            // Создаём новый заказ с пустым клиентом и списком грузов
            order = new Order
            {
                Loads = new System.Collections.Generic.List<Cargo>(),
                ClientSender = new Client()
            };

            RefreshCargo();
        }

        // Добавление груза
        private void AddCargo_Click(object sender, RoutedEventArgs e)
        {
            CargoWindow win = new CargoWindow();
            if (win.ShowDialog() == true)
            {
                Cargo cargo = win.Cargo;
                order.Loads.Add(cargo);
                RefreshCargo();
            }
        }

        // Обновление списка грузов
        private void RefreshCargo()
        {
            CargoList.ItemsSource = null;
            CargoList.ItemsSource = order.Loads;
        }

        // Сохранение заказа
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Заполняем информацию о клиенте
            Client client = new Client();
            if (ClientType.SelectedIndex == 0) // Физическое лицо
            {
                client.ClientType = "Физическое";
                client.NameClient = NameClient.Text;
                client.PhoneClient = PhoneClient.Text;
                client.Passport = PassportOrTIN.Text;
            }
            else // Юридическое лицо
            {
                client.ClientType = "Юридическое";
                client.NameLegalEntity = NameClient.Text;
                client.LeaderName = LeaderOrBank.Text;
                client.LegalAddress = AddressOrAccount.Text;
                client.LegalPhoneNumber = PhoneClient.Text;
                client.Bank = LeaderOrBank.Text;
                client.BankAccountNumber = AddressOrAccount.Text;
                client.TIN = PassportOrTIN.Text;
            }

            order.ClientSender = client;

            // Адреса и маршрут
            order.LoadingAddress = LoadingAddress.Text;
            order.UnloadingAddress = UnloadingAddress.Text;
            order.RouteLength = float.TryParse(RouteLength.Text, out var len) ? len : 0;

            // Выбор водителя и машины
            AssignDriverCarWindow assignWin = new AssignDriverCarWindow(store);
            if (assignWin.ShowDialog() == true)
            {
                order.AssignedDriver = assignWin.SelectedDriver;
                order.AssignedCar = assignWin.SelectedCar;
            }

            store.AddOrder(order); // Сохраняем в DataStore
            MessageBox.Show("Заказ создан!");
            main.RefreshActiveOrders();
        }
    }
}
