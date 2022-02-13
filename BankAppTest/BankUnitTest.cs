using NUnit.Framework;
using BankingApplication.Banks.Abstracts;
using BankingApplication.Banks.Implementation;
using BankingApplication.Constants;
using System;
using BankingApplication.BankAccounts.Abstracts;

namespace BankAppTest
{
    public class Tests
    {

        private AbstractBankAccount account;

        [SetUp]
        public void Setup()
        {
            AbstractBank WellsForgo = new Bank("WellsForgo");

            int AccNum = WellsForgo.addBankAccount(BankAccountTypeEnum.CHECKING_ACCOUNT, "Tod");

            account = WellsForgo.getBankAccount(AccNum);
            account.Deposit(1000);
        }

        [Test]
        public void CheckingDepositTest()
        {
           

            Assert.AreEqual(account.getBalance(),1000);
        }

        [Test]
        public void CheckingWithdrawTest()
        {
            account.Withdraw(800);

            Assert.AreEqual(account.getBalance(), 200);
        }

        [Test]
        public void IndividualDepositTest()
        {
            AbstractBank WellsForgo = new Bank("WellsForgo");

            int AccNum = WellsForgo.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Mo");

            var account = WellsForgo.getBankAccount(AccNum);
            account.Deposit(1000);

            Assert.AreEqual(account.getBalance(), 1000);
        }

        [Test]
        public void IndividualWithdrawTest()
        {

            AbstractBank WellsForgo = new Bank("WellsForgo");

            int AccNum = WellsForgo.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Tod");
            var account = WellsForgo.getBankAccount(AccNum);
            account.Deposit(1000);

            Assert.Throws<Exception>(()=> account.Withdraw(800));

        }

        [Test]
        public void BankTransferTest()
        {

            AbstractBank WellsForgo = new Bank("WellsForgo");

            AbstractBank AllE = new Bank("AllE");

            int WellsAccNum = WellsForgo.addBankAccount(BankAccountTypeEnum.CHECKING_ACCOUNT, "Tod");

            int AllEAccNum = AllE.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Woop");

            var Wellsaccount = WellsForgo.getBankAccount(WellsAccNum);
            var AllEaccount = AllE.getBankAccount(AllEAccNum);

            Wellsaccount.Deposit(1000);

            AllEaccount.Deposit(1200);

            AllEaccount.Transfer(WellsAccNum, WellsForgo.getRoutingNumber(), 300);
            Assert.AreEqual(Wellsaccount.getBalance(), 1300);
            Assert.AreEqual(AllEaccount.getBalance(), 900);


        }
    }
}
