using Microsoft.EntityFrameworkCore;
using RaboBankingAppServer;
using RaboBankingAppServer.Entities;
using RaboBankingAppServer.Services;
using System.Numerics;
using System.Security.Cryptography;

namespace RaboBankingApp.DataSeeder
{
    internal class TransactionSeeder
    {
        private readonly DataContext _dataContext;


        public TransactionSeeder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void SeedTransactions()
        {
            //var accounts =_accountSeeder.CreateAccounts();
            var transactions = new List<Transaction>();

            // ns: once a month
            transactions.AddRange(CreateTransactionsPerAccount("Train expenses", "Ns", -15, false, new DateTime(2021, 3, 28, 12, 0, 0)));

            // shell: twice a month
            transactions.AddRange(CreateTransactionsPerAccount("Gasoline", "Shell", -50, false, new DateTime(2021, 3, 1, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Gasoline", "Shell", -50, false, new DateTime(2021, 3, 15, 12, 0, 0)));

            // ah: once a week, 4x a month
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Ah", -40, false, new DateTime(2021, 3, 1, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Ah", -40, false, new DateTime(2021, 3, 8, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Ah", -40, false, new DateTime(2021, 3, 16, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Ah", -40, false, new DateTime(2021, 3, 25, 12, 0, 0)));

            // jumbo: once a week, 4x a month
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Jumbo", -10, false, new DateTime(2021, 3, 5, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Jumbo", -10, false, new DateTime(2021, 3, 12, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Jumbo", -10, false, new DateTime(2021, 3, 24, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Groceries", "Jumbo", -10, false, new DateTime(2021, 3, 28, 12, 0, 0)));

            // pilates: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Pilates", "PLTS", -50, true, new DateTime(2021, 3, 21, 12, 0, 0)));

            // tennis: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Tennis", "ZTV", -20, true, new DateTime(2021, 3, 21, 12, 0, 0)));

            // mortgage: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Mortgage Villa Bella Capri", "RabobankMortgage",  -1000, true, new DateTime(2021, 3, 26, 12, 0, 0)));

            // utlities: monthly
            transactions.AddRange(CreateTransactionsPerAccount("Gas, water", "Nuon", -50, false, new DateTime(2021, 3, 26, 12, 0, 0)));

            // Farfetch: once a month
            transactions.AddRange(CreateTransactionsPerAccount("Shopping", "Farfetch", -200, false, new DateTime(2021, 3, 15, 12, 0, 0)));

            // Cos: twice a month
            transactions.AddRange(CreateTransactionsPerAccount("Shopping", "Cos", -100, false, new DateTime(2021, 3, 3, 12, 0, 0)));
            transactions.AddRange(CreateTransactionsPerAccount("Shopping", "Cos", -100, false, new DateTime(2021, 3, 26, 12, 0, 0)));

            // rabobank: once a month
            transactions.AddRange(CreateTransactionsPerAccount("Income", "RabobankSalary", 10000, true, new DateTime(2021, 3, 26, 12, 0, 0)));

        }
        public List<Transaction> CreateTransactionsPerAccount(string description, string accountName, int factor, bool constantPayment, DateTime startDate)
        {
            int totalMonths = 1;
            var transactions = new List<Transaction>();
            int monthCounter = 1;
            int addDays = 0;
            var date = startDate;

            Account fromAccount = _dataContext.Accounts.Where(t => t.Name == accountName).First();
            Account toAccount = _dataContext.Accounts.Where(t => t.Name == "Hong Hui Dong").First();
            // create 24*transactionsPerMonth worth of transactions
            for (int i = 0; i < totalMonths; i++)
            {
                var transaction = new Transaction()
                {
                    Description = description,
                    IncomingTransaction = true,
                    FromAccount = fromAccount,
                    FromAccountId = fromAccount.Id,
                    ToAccount = toAccount,
                    ToAccountId = toAccount.Id,
                    Amount = constantPayment == true ? factor : factor, // ((new Random().NextDouble() + 1) * factor),
                    Date = date.AddMonths(i),
                };
                //transaction.BalanceAfterBooking = toAccount.Balance + transaction.Amount;
                
                transactions.Add(transaction);
                _dataContext.Transactions.Add(transaction);

                // call CategorizerServer


            }
            _dataContext.SaveChanges();

            return transactions;
        }

        internal void UpdateBalance()
        {
            var balanceService = new BalanceService(_dataContext);
            balanceService.UpdateBalance();

        }

        internal void UpdateCategory()
        {
            var categorizerService = new CategorizerService(_dataContext);
            categorizerService.CategorizeAll();
        }




        //public List<Transaction> CreateTransactionsPerAccount(string description, string accountName ,int transactionsPerMonth, int factor, bool constantPayment, DateTime startDate) 
        //{
        //    int totalMonths = 24;
        //    var transactions = new List<Transaction>();
        //    int monthCounter = 1;
        //    int addDays = 0;
        //    var date = startDate;

        //    // create 24*transactionsPerMonth worth of transactions
        //    for (int i = 0; i<totalMonths*transactionsPerMonth; i++)
        //    {
        //        var transaction = new Transaction()
        //        {
        //            Description = description,
        //            IncomingTransaction = true,
        //            FromAccount = _dataContext.Accounts.Where(t => t.Name == accountName).First(),
        //            FromAccountId = _dataContext.Accounts.Where(t => t.Name == accountName).Select(t => t.Id).First(),
        //            ToAccount = _dataContext.Accounts.Where(t => t.Name == "Hong Hui Dong").First(),
        //            ToAccountId = _dataContext.Accounts.Where(t => t.Name == "Hong Hui Dong").Select(t => t.Id).First(),
        //            Amount = constantPayment == true ? factor : ((new Random().NextDouble() + 1) * factor),
        //            Date = transactionsPerMonth == 1 ? date.AddMonths(i) : date.AddMonths(i).AddDays(addDays)
        //        };
        //        if (transactionsPerMonth > 1)
        //        {

        //            if(date.Month == 2)
        //            {
        //                addDays = 28 / transactionsPerMonth;
        //            }
        //            else if(date.Month == 4 || date.Month == 6 || date.Month == 9 | date.Month == 11)
        //            {
        //                addDays = 30 / transactionsPerMonth;
        //            }
        //            else
        //            {
        //                addDays = 31 / transactionsPerMonth;
        //            }

        //            if (transactionsPerMonth > monthCounter)
        //            {
        //                monthCounter++;
        //                i--;
        //            }
        //        }
        //        transactions.Add(transaction);
        //        _dataContext.Transactions.Add(transaction);
        //        _dataContext.SaveChanges();

        //        // update amount for each account
        //    }            
        //    return transactions;
        //}

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
