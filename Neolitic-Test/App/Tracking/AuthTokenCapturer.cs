using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace NeoliticTest
{
	public class AuthTokenCapturer : BaseCapturer
	{
		
		public override void Captured (object value)
		{
			
			//The authToken must always be equal to a12b3c
			String authToken = value.ToString ();
			if (authToken != "a12b3c")
				this.Command.Fail ("1120");
			
			
		}

	}
}

