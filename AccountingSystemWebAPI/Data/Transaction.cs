namespace AccountingSystemWebAPI.Data
{
    public class Transaction
    { 
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}
