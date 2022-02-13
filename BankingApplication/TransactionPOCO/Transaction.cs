using System;
namespace BankingApplication.TransactionPOCO
{
    public class BankTransaction
    {

        public string Type { get; set; }

        public double Amount { get; set; }

        public BankTransaction(string Type, double Amount)
        {
            this.Type = Type;
            this.Amount = Amount;
        }


    }
}
