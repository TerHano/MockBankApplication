using System;
using System.Collections.Generic;
using BankingApplication.Constants;
using BankingApplication.TransactionPOCO;
using BankingApplication.BankAccounts.Abstracts;

namespace BankingApplication.BankAccounts.Implementation
{
    public class CheckingAccount : AbstractBankAccount
    {
        public CheckingAccount(int AccountNum, string Owner) : base(AccountNum, Owner)
        {
        }
    }
}
