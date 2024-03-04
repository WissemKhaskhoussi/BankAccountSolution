namespace BankAccountAPI.Entities
{
    public class Transaction
    {
        private static int lastId = 0;
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Transaction()
        {
            // Auto-increment the Id
            Id = ++lastId;
        }
    }
}
