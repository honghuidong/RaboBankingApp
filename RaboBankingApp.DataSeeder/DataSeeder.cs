using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaboBankingAppServer;
using RaboBankingAppServer.Entities;

namespace RaboBankingApp.DataSeeder
{
    internal class DataSeeder
    {
        private readonly DataContext _dataContext;
        //transportation
        Account ns = new Account() { Name = "Ns", Iban = "NL67INGB0761606463" };
        Account shell = new Account() { Name = "Shell", Iban = "NL67INGB0751606463" };

        // food
        Account ah = new Account() { Name = "AH", Iban = "NL46INGB0702493368" };
        Account hema = new Account() { Name = "Hema", Iban = "NL67INGB0651607663" };
        Account jumbo = new Account() { Name = "Jumbo", Iban = "NL67INGB0651607664" };

        // health/sport
        Account pilates = new Account() { Name = "PLTS", Iban = "NL46INGB0704493768" };
        Account tennis = new Account() { Name = "ZTV", Iban = "NL46INGB07044963368" };


        // housing
        Account mortgage = new Account() { Name = "Mortgage", Iban = "NL67RABOB0641607464" };
        Account utilities = new Account() { Name = "Utilities", Iban = "NL67RABOB064162464" };



        // shopping
        Account bijenkorf = new Account() { Name = "Bijenkorf", Iban = "NL67INGB0651607464" };
        Account zara = new Account() { Name = "Zara", Iban = "NL67INGB0651607464" };

        //income
        Account rabobank = new Account() { Name = "Income", Iban = "NL67RABOB0651607464" };


        public DataSeeder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Account> CreateAccounts()
        {
            var dbAccounts = new List<Account>();
            var accounts = new List<Account>()
            {
                ns, shell,
                ah, hema, jumbo,
                pilates, tennis, 
                mortgage, utilities, 
                bijenkorf, zara,
                rabobank

            };

            accounts.ForEach(f =>
            {
                _dataContext.Add(f);
                _dataContext.SaveChanges();
                dbAccounts.Add(f);
            });

            return dbAccounts;
        }

        

        public void SeedCategories()
        {

            var doTransactionExist = _dataContext.Transactions.Any();

            if (!doTransactionExist)
            {
                // seeding logic here...
            }

            var transportation = new Category() {Name = "Transportation", Keywords = "Shell, BP, NS"};
            var food = new Category() { Name = "Food", Keywords = "AH, Jumbo, Hema"};
            var sport = new Category() { Name = "Sport", Keywords = "Tennis, Pilates"};
            var housing = new Category() { Name = "Housing", Keywords = "Mortgage, Utilities"};
            var shopping = new Category() { Name = "Shopping", Keywords = "Zara, Bijenkorf" };
            //var saving = new Category() { Name = "Saving"};
            var income = new Category() {Name = "Income", Keywords = "Rabobank"};
            var other = new Category() { Name = "Other"};

            var categories = new List<Category>()
            {
                transportation,
                food,
                sport,
                housing,
                shopping,
                other,
                income
            };

            _dataContext.Categories.AddRange(categories);
            _dataContext.SaveChanges();
        }

        public void SeedAccounts()
        {
            var accounts = new List<Account>()
            {
                ns, shell,
                ah, hema, jumbo,
                pilates, tennis,
                mortgage, utilities,
                bijenkorf, zara,
                rabobank
            };
            _dataContext.Accounts.AddRange(accounts);
            _dataContext.SaveChanges();
        }

        public void SeedTransactions()
        {
            var accounts = CreateAccounts();

            var standardTransactions = new List<Transaction>
            {
                new Transaction()
                {
                    ToAccount = accounts[0],
                    ToAccountId = accounts[0].Id
                }
            };
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
