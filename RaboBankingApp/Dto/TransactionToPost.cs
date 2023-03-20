using System;

namespace RaboBankingApp.Dto
{
    public class TransactionToPost
    {
        public int TransactionId { get; set; }
        public string ToAccountName { get; set; }
        public string ToAccountIban { get; set; }
        public string FromAccountName { get; set; }
        public string FromAccountIban { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
    }
}
