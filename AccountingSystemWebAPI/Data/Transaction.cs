namespace AccountingSystemWebAPI.Data
{
    public class Transaction
    { 
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int ClientId { get; internal set; }
        public byte[] RowVersion { get; set; } // Adding a concurrency control field
    }
}
