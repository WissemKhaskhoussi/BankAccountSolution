using System.Transactions;

namespace BankAccountAPI.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void Deposit(decimal amount)
        {
            Balance += amount;
            Transactions.Add(new Transaction { Amount = amount, Date = DateTime.Now });
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            Balance -= amount;
            Transactions.Add(new Transaction {  Amount = -amount, Date = DateTime.Now }); 
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return Transactions;
        }
    }
}
