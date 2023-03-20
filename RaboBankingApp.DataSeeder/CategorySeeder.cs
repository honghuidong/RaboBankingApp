using RaboBankingAppServer;
using RaboBankingAppServer.Entities;

namespace RaboBankingApp.DataSeeder;

internal class CategorySeeder
{
    private readonly DataContext _dataContext;

    public CategorySeeder(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void SeedCategories()
    {
        //var doTransactionExist = _dataContext.Transactions.Any();

        //if (!doTransactionExist)
        //{
        //    // seeding logic here...
        //}

        var transportation = new Category() { Name = "Transportation", Keywords = "Shell, BP, NS, train, Gasoline" };
        var food = new Category() { Name = "Food", Keywords = "AH, Jumbo, Hema" };
        var sport = new Category() { Name = "Sport", Keywords = "Tennis, Pilates" };
        var housing = new Category() { Name = "Housing", Keywords = "Mortgage, Utilities, Nuon, gas, water" };
        var shopping = new Category() { Name = "Shopping", Keywords = "Cos, Farfetch, garments, shopping" };
        //var saving = new Category() { Name = "Saving"};
        var income = new Category() { Name = "Income", Keywords = "Rabobank" };
        var other = new Category() { Name = "Other" };

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
}