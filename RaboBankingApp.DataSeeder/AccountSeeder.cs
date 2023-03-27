using RaboBankingAppServer;
using RaboBankingAppServer.Entities;
using System.Collections.Generic;

namespace RaboBankingApp.DataSeeder;

internal class AccountSeeder
{
    private readonly DataContext _dataContext;

    //yourself
    Account hh = new Account() { Name = "Hong Hui Dong", Iban = "NL68ABNA0431463484", Balance = 10000000 };

    //transportation
    Account ns = new Account() { Name = "Ns", Iban = "NL67INGB0761606463", Balance = 10000000, CategoryId = 1 };
    Account shell = new Account() { Name = "Shell", Iban = "NL67INGB0751606463", Balance = 10000000, CategoryId = 1 };

    // food
    Account ah = new Account() { Name = "AH", Iban = "NL46INGB0702493368", Balance = 10000000, CategoryId = 2 };
    Account hema = new Account() { Name = "Hema", Iban = "NL67INGB0651607663", Balance = 10000000, CategoryId = 2 };
    Account jumbo = new Account() { Name = "Jumbo", Iban = "NL67INGB0651607664", Balance = 10000000, CategoryId = 2 };

    // health/sport
    Account pilates = new Account() { Name = "PLTS", Iban = "NL46INGB0704493768", Balance = 10000000, CategoryId = 3 };
    Account tennis = new Account() { Name = "ZTV", Iban = "NL46INGB07044963368", Balance = 10000000, CategoryId = 3 };


    // housing
    Account mortgage = new Account() { Name = "RabobankMortgage", Iban = "NL67RABOB0641607464", Balance = 10000000, CategoryId = 4 };
    Account utilities = new Account() { Name = "Nuon", Iban = "NL67RABOB064162464", Balance = 10000000, CategoryId = 4 };


    // shopping
    Account farfetch = new Account() { Name = "Farfetch", Iban = "NL67INGB0651607464", Balance = 10000000, CategoryId = 5 };
    Account cos = new Account() { Name = "Cos", Iban = "NL67INGB0651607464", Balance = 10000000, CategoryId = 5 };

    //income
    Account rabobank = new Account() { Name = "RabobankSalary", Iban = "NL67RABOB0651607464", Balance = 1000000000, CategoryId = 7 };


    public AccountSeeder(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    //public List<Account> CreateAccounts()
    //{
    //    var dbAccounts = new List<Account>();
    //    var accounts = new List<Account>()
    //        {
    //            hh,
    //            ns, shell,
    //            ah, hema, jumbo,
    //            pilates, tennis,
    //            mortgage, utilities,
    //            farfetch, cos,
    //            rabobank

    //        };

    //    accounts.ForEach(f =>
    //    {
    //        _dataContext.Add(f);
    //        _dataContext.SaveChanges();
    //        dbAccounts.Add(f);
    //    });

    //    return dbAccounts;
    //}

    public void SeedAccounts()
    {
        //iList<Category> categories = _dataContext.Categories.ToList();


        var doAccountsExist = _dataContext.Accounts.Any();

        if (!doAccountsExist)
        {
            var accounts = new List<Account>()
            {
                hh,
                ns, shell,
                ah, hema, jumbo,
                pilates, tennis,
                mortgage, utilities,
                farfetch, cos,
                rabobank
            };
            _dataContext.Accounts.AddRange(accounts);
            _dataContext.SaveChanges();
        }
        
    }
}
