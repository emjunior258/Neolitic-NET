using System;
using Neolitic;

namespace NeoliticTest
{
	public class ErrorResolver : IErrorMessageResolver
	{
		
		public string Resolve (string errorCode)
		{
			return "Error {error} on command {seqno}";
		}
		
	}
}

