using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaboBankingAppServer;

namespace RaboBankingApp.DataSeeder;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RaboBankingAppDb")));
        var app = builder.Build();

        var dataContext = app.Services.GetRequiredService<DataContext>();
        var accountSeeder = new AccountSeeder(dataContext);
        var transactionSeeder = new TransactionSeeder(dataContext);
        var categorySeeder = new CategorySeeder(dataContext);

        //var dataSeeder = new SeederService(dataContext);
        //dataSeeder.SeedAllData();

        accountSeeder.SeedAccounts();
        transactionSeeder.SeedTransactions(); // accounts need to be created before transactions are seeded
        categorySeeder.SeedCategories();

        // categorize the transactions
    }
}