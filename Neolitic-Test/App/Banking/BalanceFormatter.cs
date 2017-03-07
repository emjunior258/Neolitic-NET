using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace NeoliticTest
{
	[Named("fbalance")]
	public class BalanceFormatter : BaseValueFormatter
	{

		public override string Format (object value)
		{

			//Multiple accounts balance
			if (value is ICollection) {

				ICollection<BalanceInfo> balances = (ICollection<BalanceInfo>)value;
				String balanceText = "";

				foreach (BalanceInfo balanceInfo in balances) 
					balanceText += " | "+ balanceInfoToString (balanceInfo);

				return balanceText;

			}


			//Single account balance value
			return Decimal.Parse (value.ToString()).ToString ("C");

		}

		private String balanceInfoToString(BalanceInfo balanceInfo){

			return String.Format ("{0} - {1}", balanceInfo.Account, 
				balanceInfo.Balance.ToString ("C"));

		}

	}
}

