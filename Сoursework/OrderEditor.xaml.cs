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
                    ClientReceiver = new Client(),  // ← Создаем объект получателя!
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
            if (order == null) return;

            // ===== ЗАГРУЗКА ОТПРАВИТЕЛЯ =====
            if (order.ClientSender != null)
            {
                if (order.ClientSender.ClientType == "Юридическое")
                {
                    ClientTypeComboBox.SelectedIndex = 1;
                    LegalNameBox.Text = order.ClientSender.NameLegalEntity ?? "";
                    LeaderNameBox.Text = order.ClientSender.LeaderName ?? "";
                    LegalAddressBox.Text = order.ClientSender.LegalAddress ?? "";
                    LegalPhoneBox.Text = order.ClientSender.LegalPhoneNumber ?? "";
                    BankBox.Text = order.ClientSender.Bank ?? "";
                    BankAccountBox.Text = order.ClientSender.BankAccountNumber ?? "";
                    TINBox.Text = order.ClientSender.TIN ?? "";
                }
                else // Физическое лицо
                {
                    ClientTypeComboBox.SelectedIndex = 0;
                    NameClientBox.Text = order.ClientSender.NameClient ?? "";
                    PhoneClientBox.Text = order.ClientSender.PhoneClient ?? "";
                    PassportBox.Text = order.ClientSender.Passport ?? "";
                }
            }
            else
            {
                // Если отправителя нет - по умолчанию физическое лицо
                ClientTypeComboBox.SelectedIndex = 0;
            }

            // ===== ЗАГРУЗКА ПОЛУЧАТЕЛЯ =====
            if (order.ClientReceiver != null)
            {
                if (order.ClientReceiver.ClientType == "Юридическое")
                {
                    ReceiverTypeComboBox.SelectedIndex = 1;
                    ReceiverLegalNameBox.Text = order.ClientReceiver.NameLegalEntity ?? "";
                    ReceiverTINBox.Text = order.ClientReceiver.TIN ?? "";
                }
                else // Физическое лицо
                {
                    ReceiverTypeComboBox.SelectedIndex = 0;
                    ReceiverNameBox.Text = order.ClientReceiver.NameClient ?? "";
                    ReceiverPhoneBox.Text = order.ClientReceiver.PhoneClient ?? "";
                }
            }
            else
            {
                ReceiverTypeComboBox.SelectedIndex = 0;
            }

            // ===== ЗАГРУЗКА ОСТАЛЬНЫХ ДАННЫХ =====
            LoadingAddressBox.Text = order.LoadingAddress ?? "";
            UnloadingAddressBox.Text = order.UnloadingAddress ?? "";
            RouteLengthBox.Text = order.RouteLength.ToString();

            // Даты
            if (order.StartDate != default)
                StartDatePicker.SelectedDate = order.StartDate;
            else
                StartDatePicker.SelectedDate = DateTime.Now;

            if (order.EndDate != default)
                EndDatePicker.SelectedDate = order.EndDate;
            else
                EndDatePicker.SelectedDate = DateTime.Now.AddDays(1);
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

            // ===== Клиент-отправитель =====
            Client senderClient = new Client();

            if (ClientTypeComboBox.SelectedIndex == 0)
            {
                senderClient.ClientType = "Физическое";
                senderClient.NameClient = NameClientBox.Text;
                senderClient.PhoneClient = PhoneClientBox.Text;
                senderClient.Passport = PassportBox.Text;
            }
            else
            {
                senderClient.ClientType = "Юридическое";
                senderClient.NameLegalEntity = LegalNameBox.Text;
                senderClient.LeaderName = LeaderNameBox.Text;
                senderClient.LegalAddress = LegalAddressBox.Text;
                senderClient.LegalPhoneNumber = LegalPhoneBox.Text;
                senderClient.Bank = BankBox.Text;
                senderClient.BankAccountNumber = BankAccountBox.Text;
                senderClient.TIN = TINBox.Text;
            }

            // ===== Клиент-получатель =====
            Client receiverClient = new Client();

            if (ReceiverTypeComboBox.SelectedIndex == 0)
            {
                receiverClient.ClientType = "Физическое";
                receiverClient.NameClient = ReceiverNameBox.Text;
                receiverClient.PhoneClient = ReceiverPhoneBox.Text;
                // Для получателя паспорт обычно не нужен
            }
            else
            {
                receiverClient.ClientType = "Юридическое";
                receiverClient.NameLegalEntity = ReceiverLegalNameBox.Text;
                receiverClient.TIN = ReceiverTINBox.Text;
                // Если нужно больше полей для юрлица-получателя, добавьте их
            }

            // ===== Сохраняем оба клиента в заказ =====
            order.ClientSender = senderClient;
            order.ClientReceiver = receiverClient;  // ← Вот здесь сохраняем получателя!

            order.LoadingAddress = LoadingAddressBox.Text;
            order.UnloadingAddress = UnloadingAddressBox.Text;
            order.RouteLength = float.TryParse(RouteLengthBox.Text, out var len) ? len : 0;
            order.StartDate = start;
            order.EndDate = end;

            // ===== Назначение водителя и машины =====
            AssignDriverCarWindow assignWin =
                new AssignDriverCarWindow(
                    store,
                    order,
                    order.StartDate,
                    order.EndDate
                );

            if (assignWin.ShowDialog() == true)
            {
                order.AssignedDriver = assignWin.SelectedDriver;
                order.AssignedCar = assignWin.SelectedCar;
            }
            else
            {
                // если пользователь закрыл окно — не сохраняем заказ
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

        private void ReceiverTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReceiverPhysicalPanel.Visibility =
                ReceiverTypeComboBox.SelectedIndex == 0
                    ? Visibility.Visible
                    : Visibility.Collapsed;

            ReceiverLegalPanel.Visibility =
                ReceiverTypeComboBox.SelectedIndex == 1
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }
    }
}
