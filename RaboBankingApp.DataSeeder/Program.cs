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
        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RaboBankingDb")));
        var app = builder.Build();

        var dataContext = app.Services.GetRequiredService<DataContext>();
        var dataSeeder = new DataSeeder(dataContext);
        dataSeeder.SeedCategories();

    }


}