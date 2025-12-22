namespace Coursework
{
    public interface IClientServ
    {
        string ClientType { get; set; } // "Физическое" или "Юридическое"

        // Физическое лицо
        string NameClient { get; set; }
        string PhoneClient { get; set; }
        string Passport { get; set; }

        // Юридическое лицо
        string NameLegalEntity { get; set; }
        string LeaderName { get; set; }
        string LegalAddress { get; set; }
        string LegalPhoneNumber { get; set; }
        string Bank { get; set; }
        string BankAccountNumber { get; set; }
        string TIN { get; set; }
    }
}
