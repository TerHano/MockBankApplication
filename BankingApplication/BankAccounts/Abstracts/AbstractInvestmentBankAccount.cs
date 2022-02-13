using System;
namespace BankingApplication.BankAccounts.Abstracts
{
    public class AbstractInvestmentBankAccount : AbstractBankAccount
    {
        public AbstractInvestmentBankAccount(int AccountNum, string Owner) : base(AccountNum, Owner)
        {
        }
    }
}
