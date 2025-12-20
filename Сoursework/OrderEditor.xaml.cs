using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class OrderEditor : UserControl
    {
        private DataStore store;
        private MainWindow main;
        private Order order;

        public OrderEditor(DataStore ds, MainWindow mw, Order existingOrder = null)
        {
            InitializeComponent();
            store = ds;
            main = mw;

            if (existingOrder != null)
            {
                order = existingOrder;
                FillFields();
            }
            else
            {
                order = new Order
                {
                    Loads = new System.Collections.Generic.List<Cargo>(),
                    ClientSender = new Client()
                };
            }

            RefreshCargo();
        }

        private void FillFields()
        {
            // Заполняем поля клиента
            if (order.ClientSender.ClientType == "Физическое")
            {
                ClientTypeComboBox.SelectedIndex = 0;
                NameClientBox.Text = order.ClientSender.NameClient;
                PhoneClientBox.Text = order.ClientSender.PhoneClient;
                PassportBox.Text = order.ClientSender.Passport;
            }
            else
            {
                ClientTypeComboBox.SelectedIndex = 1;
                LegalNameBox.Text = order.ClientSender.NameLegalEntity;
                LeaderNameBox.Text = order.ClientSender.LeaderName;
                LegalAddressBox.Text = order.ClientSender.LegalAddress;
                LegalPhoneBox.Text = order.ClientSender.LegalPhoneNumber;
                BankBox.Text = order.ClientSender.Bank;
                BankAccountBox.Text = order.ClientSender.BankAccountNumber;
                TINBox.Text = order.ClientSender.TIN;
            }

            LoadingAddressBox.Text = order.LoadingAddress;
            UnloadingAddressBox.Text = order.UnloadingAddress;
            RouteLengthBox.Text = order.RouteLength.ToString();
            RefreshCargo();
        }

        private void AddCargo_Click(object sender, RoutedEventArgs e)
        {
            CargoWindow win = new CargoWindow();
            if (win.ShowDialog() == true)
            {
                order.Loads.Add(win.Cargo);
                RefreshCargo();
            }
        }

        private void RefreshCargo()
        {
            CargoListBox.ItemsSource = null;
            CargoListBox.ItemsSource = order.Loads;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            if (ClientTypeComboBox.SelectedIndex == 0)
            {
                client.ClientType = "Физическое";
                client.NameClient = NameClientBox.Text;
                client.PhoneClient = PhoneClientBox.Text;
                client.Passport = PassportBox.Text;
            }
            else
            {
                client.ClientType = "Юридическое";
                client.NameLegalEntity = LegalNameBox.Text;
                client.LeaderName = LeaderNameBox.Text;
                client.LegalAddress = LegalAddressBox.Text;
                client.LegalPhoneNumber = LegalPhoneBox.Text;
                client.Bank = BankBox.Text;
                client.BankAccountNumber = BankAccountBox.Text;
                client.TIN = TINBox.Text;
            }

            order.ClientSender = client;
            order.LoadingAddress = LoadingAddressBox.Text;
            order.UnloadingAddress = UnloadingAddressBox.Text;
            order.RouteLength = float.TryParse(RouteLengthBox.Text, out var len) ? len : 0;

            // Выбор водителя и машины
            AssignDriverCarWindow assignWin = new AssignDriverCarWindow(store);
            if (assignWin.ShowDialog() == true)
            {
                order.AssignedDriver = assignWin.SelectedDriver;
                order.AssignedCar = assignWin.SelectedCar;
            }

            // Если это новый заказ, добавляем в DataStore
            if (!store.Orders.Contains(order))
            {
                order.Status = "Создан";
                store.AddOrder(order);
            }
            else
            {
                store.Save(); // если редактируем существующий заказ
            }

            main.RefreshCreatedOrders();
            MessageBox.Show("Заказ сохранён!");
        }
    }
}
