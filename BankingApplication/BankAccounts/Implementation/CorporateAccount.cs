using System;
using System.Collections.Generic;
using System.Transactions;
using BankingApplication.BankAccounts.Abstracts;
using BankingApplication.Constants;
using BankingApplication.TransactionPOCO;
namespace BankingApplication.BankAccounts.Implementation
{
	public class CorporateAccount : AbstractInvestmentBankAccount
	{
		public CorporateAccount(int AccountNum, string Owner, double initialDeposit) : base(AccountNum, Owner, initialDeposit)
		{
		}
	}
}

