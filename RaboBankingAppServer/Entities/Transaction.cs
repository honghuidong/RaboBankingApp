using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaboBankingAppServer.Entities;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    //public string ToAccountName { get; set; }
    //public string ToAccountIban { get; set; }
    //public string FromAccountName { get; set; }
    //public string FromAccountIban { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string? Description { get; set; }
    public double Amount { get; set; }
    public double BalanceAfterBooking { get; set; }
    public int CategoryId { get; set; }
    public double CarbonFootPrint { get; set; }
    public bool IncomingTransaction { get; set; }

    [ForeignKey(nameof(FromAccountId))]
    public virtual Account FromAccount { get; set; }
    public int? FromAccountId { get; set; }

    [ForeignKey(nameof(ToAccountId))]
    public virtual Account ToAccount { get; set; }
    public int? ToAccountId { get; set; }
}