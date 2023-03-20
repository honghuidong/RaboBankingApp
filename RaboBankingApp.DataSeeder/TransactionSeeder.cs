using RaboBankingAppServer;
using RaboBankingAppServer.Entities;
using System.Numerics;
using System.Security.Cryptography;

namespace RaboBankingApp.DataSeeder
{
    internal class TransactionSeeder
    {
        private readonly DataContext _dataContext;
        private AccountSeeder _accountSeeder;

        public TransactionSeeder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedTransactions()
        {
            //var accounts =_accountSeeder.CreateAccounts();
            var transactions = new List<Transaction>();

            // ns: once a month
            transactions.AddRange(CreateTransactionsPerAccount("Train expenses", "Ns", 1, 15, false, new DateTime(2021, 3, 28, 12, 0, 0)));

            // shell: twice a month
            transactions.AddRange(CreateTransactionsPerAccount("Gasoline", "Shell", 2, 50, false, new DateTime(2021, 3, 1, 12, 0, 0)));

            // ah: once a week, 4x a month
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Ah", 4, 40, false, new DateTime(2021, 3, 1, 12, 0, 0)));

            // jumbo: twice a week, 8x a month
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Jumbo", 8, 10, false, new DateTime(2021, 3, 2, 12, 0, 0)));

            // pilates: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Pilates", "PLTS", 1, 50, true, new DateTime(2021, 3, 26, 12, 0, 0)));


            // tennis: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Tennis", "ZTV", 1, 20, true, new DateTime(2021, 3, 26, 12, 0, 0)));

            // mortgage: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Mortgage Villa Bella Capri", "RabobankMortgage", 1,  1000, true, new DateTime(2021, 3, 26, 12, 0, 0)));

            // utlities: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Gas, water", "Utilities", 1, 50, false, new DateTime(2021, 3, 26, 12, 0, 0)));

            // bijenkorf: once a month
            transactions.AddRange(CreateTransactionsPerAccount("Shopping", "Farfetch", 1, 200, false, new DateTime(2021, 3, 26, 12, 0, 0)));

            // zara: twice a month
            transactions.AddRange(CreateTransactionsPerAccount("Shopping", "Cos", 2, 100, false, new DateTime(2021, 3, 26, 12, 0, 0)));

            // rabobank: once a month
            transactions.AddRange(CreateTransactionsPerAccount("Income", "Rabobank", 1, 10000, true, new DateTime(2021, 3, 26, 12, 0, 0)));


            _dataContext.Transactions.AddRange(transactions);
            _dataContext.SaveChanges();

        }

        public List<Transaction> CreateTransactionsPerAccount(string description, string accountName ,int transactionsPerMonth, int factor, bool constantPayment, DateTime startDate) 
        {
            int totalMonths = 24;
            var transactions = new List<Transaction>();
            int monthCounter = 1;
            int addDays = 0;
            var date = startDate;

            // create 24*transactionsPerMonth worth of transactions
            for (int i = 0; i<totalMonths*transactionsPerMonth; i++)
            {
                date.AddMonths(i);
                var transaction = new Transaction()
                {
                    Description = description,
                    IncomingTransaction = true,
                    FromAccount = _dataContext.Accounts.Where(t => t.Name == accountName).First(),
                    FromAccountId = _dataContext.Accounts.Where(t => t.Name == accountName).Select(t => t.Id).First(),
                    Amount = constantPayment == true ? factor : ((new Random().NextDouble() + 1) * factor),
                    Date = date.AddDays(addDays)
                };
                if (transactionsPerMonth > 1)
                {
                
                    if(date.Month == 2)
                    {
                        addDays = 28 / transactionsPerMonth;
                    }
                    else if(date.Month == 4 || date.Month == 6 || date.Month == 9 | date.Month == 11)
                    {
                        addDays = 30 / transactionsPerMonth;
                    }
                    else
                    {
                        addDays = 31 / transactionsPerMonth;
                    }

                    if (transactionsPerMonth > monthCounter)
                    {
                        monthCounter++;
                        i--;
                    }
                }
                transactions.Add(transaction);
                _dataContext.Transactions.Add(transaction);
                _dataContext.SaveChanges();
            }            
            return transactions;
        }

        public Category CreateFakeCategory()
        {
            var randomArray = new[] {"Food", "Groceries", "Utilities", "Housing", "Entertainment", "Sports"};

            return new Category()
            {
                Name = randomArray[new Random().Next(0, randomArray.Length-1)],
            };
        }
    }
}
