using System;
using System.Collections.Generic;
using System.Transactions;
using BankingApplication.Constants;
using BankingApplication.TransactionPOCO;
namespace BankingApplication.BankAccounts.Abstracts
{
     public abstract class AbstractBankAccount
    {

        protected readonly int AccountNumber;

        private string Owner;

        protected double balance;

        protected List<BankTransaction> transactions;

        public AbstractBankAccount(int AccountNum, string Owner, double initialDeposit = 0)
        {
            this.AccountNumber = AccountNum;
            this.Owner = Owner;
            transactions = new List<BankTransaction>();
            if (initialDeposit == 0)
            {
                balance = 0;
            }
            else
            {
                if (initialDeposit < 0)
                {
                    throw new TransactionException("Amount is negative");
                }
                balance = initialDeposit;
                transactions.Add(new BankTransaction(TransactionTypeEnum.INITIAL_DEPOSIT_TRANS, initialDeposit));
            }
        }


        public int getAccountNumber()
        {
            return AccountNumber;
        }

        public string getOwner()
        {
            return Owner;
        }

        public void Deposit(double Amount)
        {
            if (Amount < 0)
            {
                throw new TransactionException("Amount is negative");
            }
            balance += Amount;
            transactions.Add(new BankTransaction(TransactionTypeEnum.DEPOSIT_TRANS, Amount));
        }

        private void TransferDeposit(double Amount)
        {
            balance += Amount;
            transactions.Add(new BankTransaction(TransactionTypeEnum.TRANSFER_TRANS, Amount));

        }
        public virtual void Withdraw(double Amount)
        {
            if (Amount > balance)
            {
                throw new TransactionException("Not enough funds");
            }

            balance -= Amount;
            transactions.Add(new BankTransaction(TransactionTypeEnum.WITHDRAW_TRANS, Amount*-1));

        }
        private void TransferWithdraw(double Amount)
        {
            if (Amount > balance)
            {
                throw new TransactionException("Not enough funds");
            }
            transactions.Add(new BankTransaction(TransactionTypeEnum.TRANSFER_TRANS, Amount*-1));
            balance -= Amount;

        }
        public virtual double getBalance()
        {
            return balance;
        }

        public virtual void Transfer(int targetAccountNum,int targetRoutingNumber, double amount)
        {
            var targetBankAccount = BankRoutingMap.BankDictionary[targetRoutingNumber]?.getBankAccount(targetAccountNum);
            if(targetBankAccount == null)
            {
                throw new TransactionException("Bank account doesnt exist");
            }
            TransferWithdraw(amount);
            targetBankAccount.TransferDeposit(amount);
        }

        public List<BankTransaction> getTransactions()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(String.Format("Transactions for Account {0} Owned by {1} \n[",AccountNumber,Owner));
            foreach (BankTransaction temp in transactions) {
                String transactionStr = "Transaction Type: {0}, Amount: {2}${1}";
                if (temp.Amount < 0)
                {
                    Console.WriteLine(String.Format(transactionStr, temp.Type, Math.Abs(temp.Amount).ToString("N2"),"-"));
                }
                else
                {
                    Console.WriteLine(String.Format(transactionStr, temp.Type, temp.Amount.ToString("N2"),""));
                }
            }
            Console.WriteLine(String.Format("]\nCurrent Balance: ${0}",balance.ToString("N2")));
            Console.WriteLine("-------------------------------------------");
            return transactions;

        }

        private void addTransferTransaction(double Amount)
        {
            transactions.Add(new BankTransaction(TransactionTypeEnum.TRANSFER_TRANS, Amount));
        }


    }
}
