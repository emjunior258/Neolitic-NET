using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;


namespace NeoliticTest
{
	public class EDMCredelec : BaseContextualized
	{

		[Service("EDM","{meter} {amount:money} {@pin}")]
		public void BuyRecharge(){

			String meter = (String) Command.Get ("meter");
			decimal amount = (decimal)Command.Get ("amount");

			if (amount < 1)
				Command.Fail ("222");//Invalid recharge amount


			//The voucher bought by the client
			Command.Keywords.Set ("voucher", "1103200238902145");

		}


	}



}

