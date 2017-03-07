using System;
using Neolitic;
using Neolitic.Attributes;

namespace NeoliticTest
{
	public class BankingContextFactory : IContextFactory
	{
		
		public IExecutionContext CreateContext (IServiceInfo serviceInfo, Object contextCreationParam)
		{
			if (contextCreationParam == null)
				throw new Exception ("The costumer Id must be passed as the contextCreationParam");

			IExecutionContext context = new BankingContext(contextCreationParam.ToString());
			context.NullToken = "<NULL>";
			return context;

		}
		
	}
}

