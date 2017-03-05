using System;
using Neolitic;

namespace NeoliticTest
{
	public class ContextFactory : IContextFactory
	{
		
		public IExecutionContext CreateContext (string command, ServiceInfo serviceInfo, string arguments)
		{
		    IExecutionContext context = new ExecutionContext ();
			context.Keywords.Set ("seqno", 1300012);
			return context;
			
		}
			
	}
}

