using NUnit.Framework;
using System;
using Neolitic;

namespace NeoliticTest
{
	[TestFixture ()]
	public class CredelecServicesTest
	{
		[Test ()]
		public void BuyVoucherMustSucceed ()
		{
			Container container = new Container ();
			container.AddParser (new MoneyParser ());
			container.AddCapturer (new PinCapturer ());
			container.MapServices (new EDMCredelec ());
			container.Start (new ContextFactory (), new ServiceIdentifier (), new ErrorResolver ());

			ExecutionResult result = container.ExecuteCommand ("BIM EDM 100027024 450 1234");
			Assert.False (result.Context.ExecutionFailed);
			Assert.True (result.Message.Equals ("Your voucher code is 1103200238902145"));

		}


		[Test ()]
		public void LastVoucherMustSucceed ()
		{
			Container container = new Container ();
			container.AddFormatter (new MoneyFormatter ());
			container.AddCapturer (new PinCapturer ());
			container.MapServices (new EDMCredelec ());
			container.Start (new ContextFactory (), new ServiceIdentifier (), new ErrorResolver ());

			ExecutionResult result = container.ExecuteCommand ("BIM EDMA <NULL> 1234");
			Assert.False (result.Context.ExecutionFailed);
			Assert.True (result.Message.Equals ("The Last recharge amount is 4500 MZN"));

			result = container.ExecuteCommand ("BIM EDMA 1000123 1234");
			Assert.False (result.Context.ExecutionFailed);
			Assert.True (result.Message.Equals ("The Last recharge amount is 2500 MZN"));


		}
	}
}

