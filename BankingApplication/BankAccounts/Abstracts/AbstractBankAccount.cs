using System;
using System.Collections.Generic;
using System.Transactions;
using BankingApplication.Constants;
using BankingApplication.TransactionPOCO;
namespace BankingApplication.BankAccounts.Abstracts
{
     public abstract class AbstractBankAccount
    {

        protected int AccountNumber;

        private string Owner;

        protected double balance;

        protected List<BankTransaction> transactions;

        public AbstractBankAccount(int AccountNum, string Owner)
        {
            this.AccountNumber = AccountNum;
            this.Owner = Owner;
            transactions = new List<BankTransaction>();
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
            Console.WriteLine(String.Format("Transactions for {0} owned by {1} \n[",AccountNumber,Owner));
            foreach (BankTransaction temp in transactions) {
                String transactionStr = "Transaction Type: {0}, Amount: {1}";
                Console.WriteLine(String.Format(transactionStr, temp.Type, temp.Amount));
            }
            Console.WriteLine(String.Format("]\nCurrent Balance: {0}",balance));
            return transactions;

        }

        private void addTransferTransaction(double Amount)
        {
            transactions.Add(new BankTransaction(TransactionTypeEnum.TRANSFER_TRANS, Amount));
        }


    }
}
