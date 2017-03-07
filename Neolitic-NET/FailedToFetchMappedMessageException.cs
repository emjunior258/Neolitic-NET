using System;
using Neolitic;

namespace Neolitic
{
	public class FailedToFetchMappedMessageException: AutoCommandExecutionFailException
	{
		public FailedToFetchMappedMessageException (String status, Exception cause):base(String.Format("Failed to fetch mapped message for Exit status {0}",status),cause)
		{}
	}
}

