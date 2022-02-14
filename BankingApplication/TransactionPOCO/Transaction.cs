using System;
namespace BankingApplication.TransactionPOCO
{
    public class BankTransaction
    {

        public string Type { get; set; }

        public double Amount { get; set; }

        public double CurrentBalance { get; set; }

        public BankTransaction(string Type, double Amount, double CurrentBalance)
        {
            this.Type = Type;
            this.Amount = Amount;
            this.CurrentBalance = CurrentBalance;
        }


    }
}
