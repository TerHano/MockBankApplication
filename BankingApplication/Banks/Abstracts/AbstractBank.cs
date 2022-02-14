using System;
using System.Collections.Generic;
using System.Transactions;
using BankingApplication.BankAccounts.Abstracts;
using BankingApplication.BankAccounts.Implementation;
using BankingApplication.Constants;
using BankingApplication.Exceptions;

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
                throw new BankAccountException("Bank Account doesn't exist");
            }
            return bankAccount;
        }

        public int addBankAccount(BankAccountTypeEnum AccountType, string Owner, double intialDeposit = 0)
        {
            AbstractBankAccount newBankAccount = null;
            int newAccountNum = BankingAccountNumberScheme++;
            switch (AccountType)
            {
                case BankAccountTypeEnum.CHECKING_ACCOUNT:
                    newBankAccount = new CheckingAccount(newAccountNum, Owner,intialDeposit);
                    break;
                case BankAccountTypeEnum.INDIVIDUAL_ACCOUNT:
                    newBankAccount = new IndividualAccount(newAccountNum, Owner,intialDeposit);
                    break;
                case BankAccountTypeEnum.CORPORATE_ACCOUNT:
                    newBankAccount = new CorporateAccount(newAccountNum, Owner,intialDeposit);
                    break;
            }

            if(newBankAccount == null)
            {
                throw new BankAccountException("Error creating new bank account");
            }
            bankAccounts.Add(newBankAccount);

            return newAccountNum;
        }
        
    }
}
