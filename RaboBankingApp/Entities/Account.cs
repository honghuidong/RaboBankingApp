using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaboBankingApp.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public double? Balance { get; set; } = 1000000;

        //[ForeignKey(nameof(TransactionTypeId))]
        //public virtual AccountType AccountType { get; set; }
        //public int TransactionTypeId { get; set; }

        //public virtual ICollection<Transaction> ToTransactions { get; set; }
        //public virtual ICollection<Transaction> FromTransactions { get; set; }

        //public bool MultipleCategories { get; set; }
        //public IList<CategoryAccount> CategoryAccounts { get; set; }

    }
}
