using System;
using Neolitic.Attributes;
using Neolitic.Abstracts;

namespace NeoliticTest
{

	[Captures("pin")]
	public class PinCapturer : BaseCapturer
	{
		
		public override void Captured (object value)
		{

			if (!value.ToString ().Equals ("1234"))
				Command.Fail ("1824");//Invalid Pin

		}

	}
}

