using RaboBankingAppServer;

namespace RaboBankingApp.DataSeeder;

public class SeederService
{
    private readonly DataContext _dataContext;

    public SeederService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void SeedAllData()
    {
        var _accountSeeder = new AccountSeeder(_dataContext);
        var _transactionSeeder = new TransactionSeeder(_dataContext);
        var _categorySeeder = new CategorySeeder(_dataContext);

        _categorySeeder.SeedCategories();
        _accountSeeder.SeedAccounts();
        _transactionSeeder.SeedTransactions(); // accounts need to be created before transactions are seeded
    }
}