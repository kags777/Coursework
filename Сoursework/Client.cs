namespace Coursework
{
    public class Client
    {
        public string ClientType { get; set; } // "Физическое" или "Юридическое"
                                               // Физическое лицо
        public string NameClient { get; set; }
        public string PhoneClient { get; set; }
        public string Passport { get; set; }
        // Юридическое лицо
        public string NameLegalEntity { get; set; }
        public string LeaderName { get; set; }
        public string LegalAddress { get; set; }
        public string LegalPhoneNumber { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public string TIN { get; set; }
    }
}
