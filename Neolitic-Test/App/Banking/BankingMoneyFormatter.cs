using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace NeoliticTest
{
	[Named("money")]
	public class BankingMoneyFormatter : IValueFormatter
	{

		public string Format (Object value)
		{

			return Decimal.Parse (value.ToString()).ToString ("C");

		}

	}
}

