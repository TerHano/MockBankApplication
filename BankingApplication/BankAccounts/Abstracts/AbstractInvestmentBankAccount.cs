using System;
namespace BankingApplication.BankAccounts.Abstracts
{
    public class AbstractInvestmentBankAccount : AbstractBankAccount
    {
        public AbstractInvestmentBankAccount(int AccountNum, string Owner, double initialDeposit) : base(AccountNum, Owner, initialDeposit)
        {
        }
    }
}
