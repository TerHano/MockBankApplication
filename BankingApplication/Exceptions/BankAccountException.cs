using System;
namespace BankingApplication.Exceptions
{
	public class BankAccountException : Exception
	{
		public BankAccountException(String message)
			: base(message)
		{
		}
	}
}

