using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class ClientWindow : Window
    {
        public Client Client { get; private set; }

        public ClientWindow()
        {
            InitializeComponent();
            ClientTypeComboBox.SelectionChanged += ClientTypeComboBox_SelectionChanged;
        }

        private void ClientTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((ClientTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString() == "Физическое")
            {
                PhysicalPanel.Visibility = Visibility.Visible;
                LegalPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                PhysicalPanel.Visibility = Visibility.Collapsed;
                LegalPanel.Visibility = Visibility.Visible;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Client = new Client();
            Client.ClientType = (ClientTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            if (Client.ClientType == "Физическое")
            {
                Client.NameClient = NameClient.Text;
                Client.PhoneClient = PhoneClient.Text;
                Client.Passport = Passport.Text;
            }
            else
            {
                Client.NameLegalEntity = NameLegalEntity.Text;
                Client.LeaderName = LeaderName.Text;
                Client.LegalAddress = LegalAddress.Text;
                Client.LegalPhoneNumber = LegalPhoneNumber.Text;
                Client.Bank = Bank.Text;
                Client.BankAccountNumber = BankAccountNumber.Text;
                Client.TIN = TIN.Text;
            }

            DialogResult = true;
            Close();
        }
    }
}
