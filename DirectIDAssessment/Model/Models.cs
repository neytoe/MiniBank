namespace DirectIDAssessment.Model
{
    public class Models
    {
    }
    public class Account
    {
        public string accountId { get; set; }
        public string currencyCode { get; set; }
        public string displayName { get; set; }
        public string accountHolderNames { get; set; }
        public string accountType { get; set; }
        public string accountSubType { get; set; }
        public Identifiers identifiers { get; set; }
        public List<Party> parties { get; set; }
        public Balances balances { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class Available
    {
        public double amount { get; set; }
        public string creditDebitIndicator { get; set; }
        public List<object> creditLines { get; set; }
    }

    public class Balances
    {
        public Current current { get; set; }
        public Available available { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public double confidence { get; set; }
    }

    public class Class
    {
        public int id { get; set; }
        public string name { get; set; }
        public double confidence { get; set; }
    }

    public class Current
    {
        public double amount { get; set; }
        public string creditDebitIndicator { get; set; }
        public List<object> creditLines { get; set; }
    }

    public class EnrichedData
    {
        public Category category { get; set; }
        public Class @class { get; set; }
        public string predictedMerchantName { get; set; }
    }

    public class Identifiers
    {
        public string accountNumber { get; set; }
        public string bankCode { get; set; }
        public object iban { get; set; }
        public object secondaryIdentification { get; set; }
    }

    public class MerchantDetails
    {
        public string merchantName { get; set; }
        public object merchantCategoryCode { get; set; }
    }

    public class Party
    {
        public string partyId { get; set; }
        public string fullName { get; set; }
        public List<object> addresses { get; set; }
        public object partyType { get; set; }
        public object isIndividual { get; set; }
        public object isAuthorizingParty { get; set; }
        public object emailAddress { get; set; }
        public List<object> phoneNumbers { get; set; }
    }

    public class Customer
    {
        public string providerName { get; set; }
        public string countryCode { get; set; }
        public List<Account> accounts { get; set; }
    }

    public class Transaction
    {
        public string transactionId { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public string creditDebitIndicator { get; set; }
        public string status { get; set; }
        public TransactionCode transactionCode { get; set; }
        public object proprietaryTransactionCode { get; set; }
        public DateTime bookingDate { get; set; }
        public MerchantDetails merchantDetails { get; set; }
        public EnrichedData enrichedData { get; set; }
    }

    public class TransactionCode
    {
        public string code { get; set; }
        public string subCode { get; set; }
    }
}
