using NUnit.Framework;
using BankingApplication.Banks.Abstracts;
using BankingApplication.Banks.Implementation;
using BankingApplication.Constants;
using System;
using BankingApplication.BankAccounts.Abstracts;
using System.Transactions;

namespace BankAppTest
{
    public class Tests
    {

        private AbstractBankAccount account;

        [SetUp]
        public void Setup()
        {
            AbstractBank WellsFargo = new Bank("WellsFargo");

            int AccNum = WellsFargo.addBankAccount(BankAccountTypeEnum.CHECKING_ACCOUNT, "Tod");

            account = WellsFargo.getBankAccount(AccNum);
            account.Deposit(1000);
        }

        [Test]
        public void CheckingDepositTest()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(account.getBalance(), 1000);
                Assert.Throws<TransactionException>(() => account.Deposit(-200));
                Assert.AreEqual(account.getTransactions()[0].Amount, 1000);
                Assert.AreEqual(account.getTransactions()[0].Type, TransactionTypeEnum.DEPOSIT_TRANS);
            });
        }

        [Test]
        public void CheckingWithdrawTest()
        {
            account.Withdraw(800);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(account.getBalance(), 200);
                Assert.Throws<TransactionException>(() => account.Withdraw(8000));
            });

        }

        [Test]
        public void IndividualDepositTest()
        {
            AbstractBank WellsFargo = new Bank("WellsFargo");

            int AccNum = WellsFargo.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Mo");

            var account = WellsFargo.getBankAccount(AccNum);
            account.Deposit(1000);

            Assert.AreEqual(account.getBalance(), 1000);
        }

        [Test]
        public void IndividualWithdrawTest()
        {

            AbstractBank WellsFargo = new Bank("WellsFargo");

            int AccNum = WellsFargo.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Tod");
            var account = WellsFargo.getBankAccount(AccNum);
            account.Deposit(1000);

            Assert.Throws<TransactionException>(()=> account.Withdraw(800));

        }

        [Test]
        public void BankTransferTest()
        {

            AbstractBank WellsFargo = new Bank("WellsFargo");

            AbstractBank Ally = new Bank("Ally");

            int WellsAccNum = WellsFargo.addBankAccount(BankAccountTypeEnum.CHECKING_ACCOUNT, "Tod");

            int AllyAccNum = Ally.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Woop");

            var Wellsaccount = WellsFargo.getBankAccount(WellsAccNum);
            var Allyaccount = Ally.getBankAccount(AllyAccNum);

            Wellsaccount.Deposit(1000);

            Allyaccount.Deposit(1200);

            Allyaccount.Transfer(WellsAccNum, WellsFargo.getRoutingNumber(), 300);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Wellsaccount.getBalance(), 1300);
                Assert.AreEqual(Allyaccount.getBalance(), 900);
            });

        }
    }
}
