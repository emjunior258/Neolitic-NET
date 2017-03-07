using System;
using NUnit;
using NUnit.Framework;
using Neolitic;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	[TestFixture()]
	public class BankingServicesTest
	{

		private const String CUSTOMER_ID = "0000021";

		private IContainer GetContainer(){

			IContainer container = new Container ();
			container.AddFormatter (new BankingMoneyFormatter ());
			container.AddFormatter (new BalanceFormatter());
			container.AddCapturer (new BankingPinCapturer ());
			container.MapServices (new BankingServices ());
			container.Start (new BankingContextFactory (), new BankingServiceIdentifier (), 
				new BankingErrorMessageResolver ());

			return container;


		}

		[Test()]
		public void TransferMustEndSuccessfully(){

			IContainer container = GetContainer ();
			ExecutionResult result = container.ExecuteCommand("TRANSFER 100024 12000 100023 1234",CUSTOMER_ID);
			Assert.False (result.Context.ExecutionFailed);
			Assert.True (result.Message.Equals ("You have successfully transfered $12,000.00 to 100023"));

		}
			

		[Test()]
		public void MustExitWithInsufficientFundsError(){

			IContainer container = GetContainer ();
			ExecutionResult result = container.ExecuteCommand("TRANSFER 100021 35000 100023 1234",CUSTOMER_ID);
			Assert.True (result.Context.ExecutionFailed);
			Assert.True (result.Context.ErrorCode.Equals("INSUFFICIENT_FUNDS"));
			Assert.True(result.Message.Equals("Your funds are not sufficient to complete the transaction"));

		}


		[Test()]
		public void MustExitWithInvalidPinError(){

			IContainer container = GetContainer ();
			ExecutionResult result = container.ExecuteCommand("TRANSFER 100021 35000 100023 1235",CUSTOMER_ID);
			Assert.True (result.Context.ExecutionFailed);
			Assert.True (result.Context.ErrorCode.Equals("INVALID_PIN"));
			Assert.True(result.Message.Equals("The supplied pin is not valid. Try again"));
		}

		[Test()]
		public void MustSucceedGettingBalanceOfOneAccount(){

			IContainer container = GetContainer ();
			ExecutionResult result = container.ExecuteCommand("BALANCE 100021 1234",CUSTOMER_ID);
			Assert.False (result.Context.ExecutionFailed);
			Assert.True(result.Message.Equals("Your balance is $1,000.00"));

		}


		[Test()]
		public void MustSucceedGettingBalanceOfAllAccounts(){

			IContainer container = GetContainer ();
			ExecutionResult result = container.ExecuteCommand("BALANCE <NULL> 1234",CUSTOMER_ID);
			Assert.False (result.Context.ExecutionFailed);
			Assert.True (result.Message.Equals ("Your balance is  | 100021 - $1,000.00 | 100023 - $0.00"));

		}

	}
}

