namespace AccountingSystemWebAPI.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}
