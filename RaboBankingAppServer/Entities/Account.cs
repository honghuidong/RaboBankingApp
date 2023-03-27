using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaboBankingAppServer.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        [Column(TypeName = "decimal(15,2)")]
        public double Balance { get; set; }

        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }

        //[ForeignKey(nameof(TransactionTypeId))]
        //public virtual AccountType AccountType { get; set; }
        //public int TransactionTypeId { get; set; }

        //public virtual ICollection<Transaction> ToTransactions { get; set; }
        //public virtual ICollection<Transaction> FromTransactions { get; set; }

        //public bool MultipleCategories { get; set; }
        //public IList<CategoryAccount> CategoryAccounts { get; set; }

    }
}
