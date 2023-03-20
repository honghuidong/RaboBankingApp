using System.ComponentModel.DataAnnotations.Schema;

namespace RaboBankingApp.Entities.NotUsed;

public class CategoryAccount
{
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; }
    public int AccountId { get; set; }
}