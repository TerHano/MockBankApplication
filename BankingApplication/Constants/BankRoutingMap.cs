using System;
using System.Collections.Generic;
using BankingApplication.Banks.Abstracts;

namespace BankingApplication.Constants
{
    public static class BankRoutingMap

    {
        public static int routingNumberScheme = 2023431321;
        public static Dictionary<int,AbstractBank> BankDictionary = new Dictionary<int, AbstractBank>();
    }
}
