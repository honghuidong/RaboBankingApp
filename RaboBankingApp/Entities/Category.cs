#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RaboBankingApp.Entities
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Keywords { get; set; }

        public List<Account> Accounts { get; set; }

        //public IList<CategoryAccount> CategoryAccounts { get; set; }

    }
}
