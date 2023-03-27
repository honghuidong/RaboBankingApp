using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RaboBankingAppServer.Entities;
using Transaction = RaboBankingAppServer.Entities.Transaction;

namespace RaboBankingAppServer.Services;

public class CategorizerService
{

    private readonly DataContext _dataContext;

    public CategorizerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void CategorizeAll()
    {
        var transactions = _dataContext.Transactions;
        //var categorizesTransactions = transactions.Select(t => Categorize(t)).ToList();
        var categorizesTransactions = transactions.Include(t => t.FromAccount)
            .Include(t => t.ToAccount).ToList();
        foreach (var transaction in categorizesTransactions)
        {
            Categorize(transaction);
        }
        _dataContext.SaveChanges();
    }

    public Transaction Categorize(Transaction transaction)
    {
        bool foundCategory = false;
        string ibanToCheck;
        if (transaction.IncomingTransaction == true)
        {
            ibanToCheck = transaction.FromAccount.Iban;
        }
        else
        {
            ibanToCheck = transaction.ToAccount.Iban;
        }

        // Loop through each accountnumber for all categories to see if the transaction matches
        foreach (var category in _dataContext.Categories.Include(c=>c.Accounts))
        {
            //Check if either the incoming or outgoing account number matches one of the account numbers for this category
            if (Array.IndexOf(category.Accounts.Select(x => x.Iban).ToArray(), ibanToCheck) >= 0)
            {
                transaction.CategoryId = category.Id;
                foundCategory = true;
            }

            if (foundCategory)
            {
                break;
            }
        }

        // check the name of the transaction

        if (!foundCategory)
        {
            foreach (var category in _dataContext.Categories)
            {
                if(category.Keywords != null)
                {
                    //Check whether the description match the keywords for this category
                    string[] keywords = ConvertStringToArrayOfStrings(category.Keywords);
                    foreach (var keyword in keywords)
                    {
                        if (ConvertStringToArrayOfStrings(transaction.Description.ToLower()).Contains(keyword.ToLower()))
                        {
                            transaction.CategoryId = category.Id;
                            foundCategory = true;
                            break;
                        }
                    }
                }
                
            }
        }

        if (!foundCategory)
        {
            // If no category was found, set it as "Other"
            transaction.CategoryId = _dataContext.Categories.First(category => category.Name == "Other").Id;
        }

        return transaction;
    }

    public string[] ConvertStringToArrayOfStrings(string stringList)
    {
        string[] arrayOfStrings = stringList.Split(',').ToArray();
        return arrayOfStrings;
    }


}
