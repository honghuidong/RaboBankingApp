using RaboBankingAppServer;

namespace RaboBankingApp.DataSeeder;

public class SeederService
{
    private readonly DataContext _dataContext;
    private TransactionSeeder _transactionSeeder;
    private AccountSeeder _accountSeeder;
    private CategorySeeder _categorySeeder;

    public SeederService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // how to use the datacontext in transactionseeder constructor
    public void SeedAllData()
    {
        _accountSeeder.SeedAccounts();
        _transactionSeeder.SeedTransactions(); // accounts need to be created before transactions are seeded
        _categorySeeder.SeedCategories();
    }
}