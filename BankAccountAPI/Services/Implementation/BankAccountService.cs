using BankAccountAPI.Entities;
using BankAccountAPI.Interfaces;

namespace BankAccountAPI.Services.Implementation
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException(nameof(bankAccountRepository));
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = _bankAccountRepository.GetById(accountId);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            account.Deposit(amount);
            _bankAccountRepository.Save(account);
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _bankAccountRepository.GetById(accountId);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            account.Withdraw(amount);
            _bankAccountRepository.Save(account);
        }

        public decimal GetBalance(int accountId)
        {
            var account = _bankAccountRepository.GetById(accountId);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            return account.Balance;
        }

        public IEnumerable<Transaction> GetTransactions(int accountId)
        {
            var account = _bankAccountRepository.GetById(accountId);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            return account.GetTransactions();
        }
    }
}