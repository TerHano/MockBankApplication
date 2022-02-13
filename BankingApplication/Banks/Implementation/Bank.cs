using System;
using BankingApplication.Banks.Abstracts;

namespace BankingApplication.Banks.Implementation
{
    public class Bank : AbstractBank
    {
        public Bank(String BankName): base(BankName)
        {
        }
    }
}
