using System;
using System.Collections.Generic;
using BankingApplication.BankAccounts.Abstracts;
using BankingApplication.BankAccounts.Implementation;
using BankingApplication.Constants;

namespace BankingApplication.Banks.Abstracts
{
    public abstract class AbstractBank
    {
        private int BankingAccountNumberScheme = 325084426;
        private string BankName;
        private int RoutingNumber;
        List<AbstractBankAccount> bankAccounts;


        public AbstractBank(string BankName, int BankScheme) : this()
        {
            this.BankName = BankName;
            this.BankingAccountNumberScheme = BankScheme;
        }

        public AbstractBank(string BankName) : this()
        {
            this.BankName = BankName;
        }

        private AbstractBank()
        {
            bankAccounts = new List<AbstractBankAccount>();
            this.RoutingNumber = BankRoutingMap.routingNumberScheme++;
            BankRoutingMap.BankDictionary.Add(this.RoutingNumber,this);
        }

        public string getBankName()
        {
            return BankName;
        }

        public int getRoutingNumber()
        {
            return RoutingNumber;
        }

        public AbstractBankAccount getBankAccount(int BankAccountNum)
        {
            AbstractBankAccount bankAccount = bankAccounts.Find(x => x.getAccountNumber() == BankAccountNum);
            if(bankAccount == null)
            {
                throw new Exception("BankAccount doesn't exist");
            }
            return bankAccount;
        }

        public int addBankAccount(string AccountType, string Owner)
        {
            AbstractBankAccount newBankAccount = null;
            int newAccountNum = BankingAccountNumberScheme++;
            switch (AccountType)
            {
                case BankAccountTypeEnum.CHECKING_ACCOUNT:
                    newBankAccount = new CheckingAccount(newAccountNum, Owner);
                    break;
                case BankAccountTypeEnum.INDIVIDUAL_ACCOUNT:
                    newBankAccount = new IndividualAccount(newAccountNum, Owner);
                    break;
            }

            if(newBankAccount == null)
            {
                throw new Exception("Oops");
            }
            bankAccounts.Add(newBankAccount);

            return newAccountNum;
        }
        
    }
}
