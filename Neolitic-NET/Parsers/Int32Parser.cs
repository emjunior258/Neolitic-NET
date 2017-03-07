using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace Neolitic
{
	[Named("int32")]
	public class Int32Parser : IValueParser
	{

		public object Parse (string value)
		{
			return Int32.Parse (value);
		}

		
	}
}

