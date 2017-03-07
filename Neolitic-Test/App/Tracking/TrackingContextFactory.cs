using System;
using Neolitic;

namespace NeoliticTest
{
	public class TrackingContextFactory : IContextFactory
	{

		public IExecutionContext CreateContext (string command, IServiceInfo serviceInfo, string arguments)
		{
			IExecutionContext context = new ExecutionContext ();
			return context;
		}

	}
}

