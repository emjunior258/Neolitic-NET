using System;
using Neolitic;
using Neolitic.Abstracts;


namespace NeoliticTest
{
	public class BankingErrorMessageResolver : IErrorMessageResolver
	{
		
		public string Resolve (string errorCode)
		{
			if (errorCode == "INVALID_PIN")
				return "The supplied pin is not valid. Try again";
			
			else if (errorCode == "INSUFFICIENT_FUNDS")
				return "Your funds are not sufficient to complete the transaction";
			
			else if (errorCode == "INEXISTENT_ACCOUNT")
				return "The account {account} does not exist";
			
			else 
				return "We couldnt process your request successfully";
		}

		
	}
}

