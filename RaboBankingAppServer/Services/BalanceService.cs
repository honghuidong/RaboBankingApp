using Microsoft.EntityFrameworkCore;
using RaboBankingAppServer.Entities;

namespace RaboBankingAppServer.Services;

public class BalanceService
{
    private readonly DataContext _dataContext;
    public BalanceService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public void UpdateBalance()
    {
        List<Transaction> orderedTransactions = _dataContext.Transactions
            .Include(t => t.FromAccount)
            .Include(t => t.ToAccount)
            .OrderBy(t => t.Date)
            .ToList();
        foreach (Transaction transaction in orderedTransactions)
        {
            transaction.BalanceAfterBooking = transaction.ToAccount.Balance + transaction.Amount;
            transaction.FromAccount.Balance = transaction.Amount > 0 ? transaction.FromAccount.Balance - transaction.Amount : transaction.FromAccount.Balance + transaction.Amount;
            transaction.ToAccount.Balance = transaction.ToAccount.Balance + transaction.Amount;
        }
        _dataContext.SaveChanges();
    }
}
