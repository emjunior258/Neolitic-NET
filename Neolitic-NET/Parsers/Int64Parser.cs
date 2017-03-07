using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace Neolitic
{
	[Named("pint64")]
	public class Int64Parser : IValueParser
	{

		public object Parse (string value)
		{
			return Int64.Parse (value);
		}
				

	}
}

