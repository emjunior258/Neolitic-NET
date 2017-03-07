using System;
using Neolitic;

namespace Neolitic
{
	public class MessageNotMappedException : AutoCommandExecutionFailException
	{
		public MessageNotMappedException (String status):base(String.Format("No value is Mapped for Exit status {0}", status))
		{}
	}
}

