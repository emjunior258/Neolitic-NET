using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace Neolitic
{
	[Named("pbool")]
	public class BooleanParser : IValueParser
	{
		
		public object Parse (string value)
		{
			return Boolean.Parse (value);			
		}
	}
}

