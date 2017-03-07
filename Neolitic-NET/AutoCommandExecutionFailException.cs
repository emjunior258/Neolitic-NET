using System;
using Neolitic;

namespace Neolitic
{
	public class AutoCommandExecutionFailException : CommandExecutionFailException
	{
		public AutoCommandExecutionFailException (String message):base(message,CommandExecutionFailException.DEFAULT_ERROR_CODE)
		{
			
		}

		public AutoCommandExecutionFailException (String message, Exception cause):base(message,CommandExecutionFailException.DEFAULT_ERROR_CODE,cause)
		{

		}
	}
}

