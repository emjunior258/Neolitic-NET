using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace Neolitic
{
	[Named("double")]
	public class DoubleParser : IValueParser
	{
		
		public object Parse (string value)
		{
			return Double.Parse (value);
		}

		
	}
}

