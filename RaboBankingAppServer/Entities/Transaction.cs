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
    [Column(TypeName = "decimal(15,2)")]
    public double Amount { get; set; }
    [Column(TypeName = "decimal(15,2)")]
    public double BalanceAfterBooking { get; set; }
    public int CategoryId { get; set; }
    public double CarbonFootPrint { get; set; }
    public bool IncomingTransaction { get; set; }

    public Account FromAccount { get; set; }
    [Required]
    public int? FromAccountId { get; set; }
    public virtual Account ToAccount { get; set; }
    [Required]
    public int? ToAccountId { get; set; }

    //private Account HongHui = new Account() { Name = "Hong Hui Dong", Iban = "NL68ABNA0431463484", Balance = 10000000 };
}

