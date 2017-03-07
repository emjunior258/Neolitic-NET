using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace Neolitic
{
	[Named("decimal")]
	public class DecimalParser : IValueParser
	{
		
		public object Parse (string value)
		{
			return Decimal.Parse (value);
		}

		
	}
}

