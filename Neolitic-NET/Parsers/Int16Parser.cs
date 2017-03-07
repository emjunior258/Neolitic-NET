using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace Neolitic
{

	[Named("int16")]
	public class Int16Parser : IValueParser
	{
		
		public object Parse (string value)
		{
			return Int16.Parse (value);
		}
		
	}
}

