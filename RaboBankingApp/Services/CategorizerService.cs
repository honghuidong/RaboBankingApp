using System;
using System.Collections.Generic;
using System.Linq;
using RaboBankingApp.Entities;
using Transaction = RaboBankingApp.Entities.Transaction;

namespace RaboBankingApp.Services;

public class CategorizerService
{

    private readonly DataContext _dataContext;

    public CategorizerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IList<Category> CategorizeMultiple(IList<Transaction> transactions)
    {
        var categories = transactions.Select(t => Categorize(t)).ToList();
        return categories;
    }

    public Category Categorize(Transaction transaction)
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
        foreach (var category in _dataContext.Categories)
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

        if (!foundCategory)
        {
            // If no category was found, set it as "Other"
            transaction.CategoryId = _dataContext.Categories.First(category => (category.Name == "Other")).Id;
        }

        return new Category();
    }

    public string[] ConvertStringToArrayOfStrings(string stringList)
    {
        string[] arrayOfStrings = stringList.Split(',').ToArray();
        return arrayOfStrings;
    }
    

}
