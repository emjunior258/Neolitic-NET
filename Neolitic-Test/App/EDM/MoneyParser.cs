using System;
using Neolitic.Attributes;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	[Named("money")]
	public class MoneyParser : BaseValueParser
	{
		public override object Parse (string value)
		{
			return Decimal.Parse (value);
			
		}
		
	}
}

