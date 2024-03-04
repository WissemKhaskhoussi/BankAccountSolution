using BankAccountAPI.Entities;

namespace BankAccountAPI.Services
{
    public interface IBankAccountService
    {
        void Deposit(int accountId, decimal amount);
        void Withdraw(int accountId, decimal amount);
        decimal GetBalance(int accountId);
        IEnumerable<Transaction> GetTransactions(int accountId);
    }
}
