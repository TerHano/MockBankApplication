using System;
using System.Collections.Generic;
using System.Transactions;
using BankingApplication.BankAccounts.Abstracts;
using BankingApplication.Constants;
using BankingApplication.TransactionPOCO;

namespace BankingApplication.BankAccounts.Implementation
{
    public class IndividualAccount : AbstractInvestmentBankAccount
    {
        public IndividualAccount(int AccountNum, string Owner, double initialDeposit) : base(AccountNum, Owner, initialDeposit)
        {
        }

        public override void Withdraw(double Amount)
        {

            if (Amount > balance)
            {
                throw new TransactionException("Not enough funds");
            }

            if(Amount > 500)
            {
                throw new TransactionException("Over Individual Limit of 500");
            }

            balance -= Amount;
            transactions.Add(new BankTransaction(TransactionTypeEnum.WITHDRAW_TRANS, Amount * -1));
        }
    }
}
