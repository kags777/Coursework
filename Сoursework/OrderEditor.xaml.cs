using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class OrderEditor : UserControl
    {
        private readonly DataStore store;
        private readonly MainWindow main;
        private Order order;
        private bool isEditing;

        public OrderEditor(DataStore ds, MainWindow mw, Order existingOrder = null)
        {
            InitializeComponent();
            store = ds;
            main = mw;

            StartDatePicker.SelectedDate = DateTime.Now;
            EndDatePicker.SelectedDate = DateTime.Now.AddDays(1);

            if (existingOrder != null)
            {
                order = existingOrder;
                isEditing = true;
                LoadOrderToUI();
            }
            else
            {
                order = new Order
                {
                    ClientSender = new Client(),
                    Loads = new List<Cargo>(),
                    Status = OrderStatus.Created
                };
                ClientTypeComboBox.SelectedIndex = 0;
            }

            RefreshCargo();
        }

        private void ClientTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PhysicalPanel.Visibility =
                ClientTypeComboBox.SelectedIndex == 0 ? Visibility.Visible : Visibility.Collapsed;

            LegalPanel.Visibility =
                ClientTypeComboBox.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadOrderToUI()
        {
            if (order.ClientSender.ClientType == "Юридическое")
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
            else
            {
                ClientTypeComboBox.SelectedIndex = 0;
                NameClientBox.Text = order.ClientSender.NameClient;
                PhoneClientBox.Text = order.ClientSender.PhoneClient;
                PassportBox.Text = order.ClientSender.Passport;
            }

            LoadingAddressBox.Text = order.LoadingAddress;
            UnloadingAddressBox.Text = order.UnloadingAddress;
            RouteLengthBox.Text = order.RouteLength.ToString();

            StartDatePicker.SelectedDate =
                order.StartDate == default ? DateTime.Now : order.StartDate;

            EndDatePicker.SelectedDate =
                order.EndDate == default ? DateTime.Now.AddDays(1) : order.EndDate;
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
            DateTime start = StartDatePicker.SelectedDate ?? DateTime.Now;
            DateTime end = EndDatePicker.SelectedDate ?? DateTime.Now.AddDays(1);

            if (end <= start)
            {
                MessageBox.Show("Дата окончания должна быть позже даты начала");
                return;
            }

            // ===== Клиент =====
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
            order.StartDate = start;
            order.EndDate = end;

            // ===== Назначение водителя и машины =====
            AssignDriverCarWindow assignWin =
                new AssignDriverCarWindow(store, start, end);

            if (assignWin.ShowDialog() == true)
            {
                order.AssignedDriver = assignWin.SelectedDriver;
                order.AssignedCar = assignWin.SelectedCar;
            }

            if (order.AssignedDriver != null &&
                !order.AssignedDriver.IsAvailable(start, end))
            {
                MessageBox.Show("Выбранный водитель занят");
                return;
            }

            if (order.AssignedCar != null &&
                !order.AssignedCar.IsAvailable(start, end))
            {
                MessageBox.Show("Выбранная машина занята");
                return;
            }

            // ===== Блокировка =====
            if (order.AssignedDriver != null)
                order.AssignedDriver.BusyPeriods.Add(Tuple.Create(start, end));

            if (order.AssignedCar != null)
                order.AssignedCar.BusyPeriods.Add(Tuple.Create(start, end));

            // ===== СТРАХОВКА =====
            int totalInsurance = 0;
            bool fragileCargo = false;

            foreach (var cargo in order.Loads)
            {
                cargo.CalcInsurance();
                totalInsurance += cargo.InsuranceCost;

                if (cargo.FragileCargo)
                    fragileCargo = true;
            }

            // ===== СТОИМОСТЬ ЗАКАЗА =====
            order.CalcCost(
                totalInsurance,
                fragileCargo,
                (int)order.RouteLength
            );

            // ===== Сохранение =====
            if (!isEditing)
            {
                order.Status = OrderStatus.Created;
                store.AddOrder(order);
            }
            else
            {
                store.Save();
            }

            main.RefreshCreatedOrders();

            MessageBox.Show(
                $"Заказ сохранён!\n\n" +
                $"Страховка: {totalInsurance} ₽\n" +
                $"Стоимость заказа: {order.Cost} ₽",
                "Информация"
            );
        }
    }
}
