namespace BankAccountAPI.Entities
{
    public class Transaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Transaction()
        {
            // Auto-increment the Id
            Id = Guid.NewGuid().ToString();
        }
    }
}
