using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RaboBankingAppServer;
using RaboBankingAppServer.Services;

namespace RaboBankingApp.DataSeeder;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RaboBankingAppDb")));
        //builder.Services.AddScoped<CategorizerService>();

        var app = builder.Build();

        var dataContext = app.Services.GetRequiredService<DataContext>();
        //var categorizerService = app.Services.GetRequiredService<CategorizerService>();

        var dataSeeder = new SeederService(dataContext);
        //dataSeeder.SeedAllData();

        var transactionSeeder = new TransactionSeeder(dataContext);

        //transactionSeeder.UpdateBalance();
        transactionSeeder.UpdateCategory();




        //var dataContext2 = app.Services.GetRequiredService<DataContext>();
        //var trx = dataContext2.Transactions
        //    .Include(t => t.FromAccount)
        //    .Include(t => t.ToAccount)
        //    .ToList();
        //Console.WriteLine(trx.Count);
    }
}