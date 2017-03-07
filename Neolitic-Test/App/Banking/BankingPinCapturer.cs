using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace NeoliticTest
{
	[Captures("pin")]
	public class BankingPinCapturer : BaseCapturer
	{


		public override void Captured (object value)
		{

			BankingContext bankingContext = GetContext<BankingContext> ();

			//Here we will validate the pin code, which must be equal to 1234
			//We only recognize the client with Id 0000021
			if (bankingContext.CustomerID=="0000021" && value.ToString () != "1234")
				this.Context.Fail ("INVALID_PIN"); //Fail Execution with INVALID_PIN error code
			
		}

		
		
	}
}

