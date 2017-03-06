using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;


namespace NeoliticTest
{
	public class EDMCredelec : BaseContextualized
	{

		[Service("EDM")]
		public void BuyVoucher(){

			String meter = Command.Get<String> ("meter");
			decimal amount = Command.Get<Decimal> ("amount");

			if (amount < 1)
				Command.Fail ("222");//Invalid recharge amount


			//The voucher bought by the client
			Command.Set ("voucher", "1103200238902145");

		}



		[Service("EDMA")]
		public void GetLastVoucherAmount(){

			String meter = Command.GetOptional<String> ("meter");
			if (meter != null) {

				//Get last voucher for specific meter
				Command.Set ("amount", "2500");

			} else {

				//Get the last voucher
				Command.Set ("amount", "4500");

			}

		}


	}



}

