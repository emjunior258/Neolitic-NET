using System;
using Neolitic;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	public class ErrorResolver : BaseErrorMessageResolver
	{

		public override string Resolve (string errorCode)
		{
			return "Error {error} on command {seqno}";
		}
		
		
	}
}

