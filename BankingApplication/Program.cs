using System;
using BankingApplication;
using BankingApplication.BankAccounts.Abstracts;
using BankingApplication.BankAccounts.Implementation;
using BankingApplication.Banks.Abstracts;
using BankingApplication.Banks.Implementation;
using BankingApplication.Constants;

namespace BankingApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            AbstractBank WellsForgo = new Bank("Wells Fargo");
            AbstractBank AllyBank = new Bank("Ally Bank");

            int AccNum = WellsForgo.addBankAccount(BankAccountTypeEnum.CHECKING_ACCOUNT,"Tod");

            int InAccNum = AllyBank.addBankAccount(BankAccountTypeEnum.INDIVIDUAL_ACCOUNT, "Mod",200);


            var WellAcc = WellsForgo.getBankAccount(AccNum);


            var AllyAcc = AllyBank.getBankAccount(InAccNum);

            WellAcc.Deposit(200);
            WellAcc.Deposit(400);
            WellAcc.Deposit(550.4);


            AllyAcc.Deposit(900);
            AllyAcc.Deposit(100);
            AllyAcc.Deposit(350.4);

            WellAcc.Transfer(AllyAcc.getAccountNumber(), AllyBank.getRoutingNumber(), 400);

            WellAcc.getTransactions();

            AllyAcc.getTransactions();


        }
    }
}
