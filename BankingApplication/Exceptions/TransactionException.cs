using System;
namespace BankingApplication.Exceptions
{
	public class TransactionException : Exception
	{
		public TransactionException(String message)
			: base(message)
		{
		}
	}
}

