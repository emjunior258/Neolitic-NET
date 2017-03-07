using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;



namespace NeoliticTest
{
	[Named("money")]
	public class MoneyFormatter : BaseValueFormatter
	{

		public override string Format (string value)
		{
			return String.Format ("{0} MZN", value);
		}


		
	}
}

