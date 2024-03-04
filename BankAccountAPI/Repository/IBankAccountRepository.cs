using BankAccountAPI.Entities;

namespace BankAccountAPI.Interfaces
{
    public interface IBankAccountRepository
    {
        BankAccount GetById(int id);
        void Save(BankAccount account);
    }
}
