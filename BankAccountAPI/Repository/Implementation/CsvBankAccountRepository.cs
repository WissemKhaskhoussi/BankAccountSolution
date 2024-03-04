using BankAccountAPI.Entities;
using BankAccountAPI.Interfaces;
using System.Globalization;

namespace BankAccountAPI.Repository.Implementation
{
    public class CsvBankAccountRepository : IBankAccountRepository
    {
        private readonly string _csvFilePath;

        public CsvBankAccountRepository(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
            if (!File.Exists(_csvFilePath))
            {
                File.Create(_csvFilePath).Close();
            }
        }

        public BankAccount GetById(int id)
        {
            var lines = File.ReadAllLines(_csvFilePath);
            foreach (var line in lines.Skip(1))
            {
                var fields = line.Split(';');
                if (int.Parse(fields[0]) == id)
                {
                    var account = new BankAccount
                    {
                        Id = int.Parse(fields[0]),
                        Balance = decimal.Parse(fields[1]),
                        Transactions = new List<Transaction>()
                    };

                    // Loop through all transaction fields (starting from index 2)
                    for (int i = 2; i < fields.Length; i++)
                    {
                        var transactionsData = fields[i].Split(';');
                        foreach (var transactionData in transactionsData)
                        {
                            account.Transactions.AddRange(DeserializeTransactions(transactionData));
                        }
                    }

                    return account;
                }
            }
            return null;
        }

        public void Save(BankAccount account)
        {
            var lines = File.ReadAllLines(_csvFilePath).ToList();

            // Check if the file is empty (no lines at all)
            if (lines.Count == 0)
            {
                // If the file is empty, add the header line
                lines.Add("Id;Balance;Transactions");
            }
            else
            {
                // If the file is not empty, skip the header line
                lines = lines.Skip(1).ToList();
            }

            var accountIndex = lines.FindIndex(line => int.Parse(line.Split(';')[0]) == account.Id);
            if (accountIndex != -1)
            {
                lines[accountIndex] = SerializeAccount(account);
            }
            else
            {
                lines.Add(SerializeAccount(account));
            }

            // Write the lines back to the file, including the header line
            File.WriteAllLines(_csvFilePath, new[] { "Id;Balance;Transactions" }.Concat(lines));
        }

        private string SerializeAccount(BankAccount account)
        {
            return $"{account.Id};{account.Balance.ToString("0,00").Replace(",", ".")};{SerializeTransactions(account.Transactions)}";
        }

        private string SerializeTransactions(List<Transaction> transactions)
        {
            return string.Join(";", transactions.Select(t =>
                  $"{t.Id}*{t.Amount.ToString("0,00").Replace(",", ".")}*{t.Date:yyyy-MM-dd}"));
        }

        private List<Transaction> DeserializeTransactions(string serializedTransactions)
        {
            return serializedTransactions.Split(';').Select(transactionString =>
            {
                var fields = transactionString.Split('*');
                return new Transaction
                {
                    Id = fields[0],
                    Amount = decimal.Parse(fields[1]),
                    Date = DateTime.Parse(fields[2])
                };
            }).ToList();
        }
    }
}