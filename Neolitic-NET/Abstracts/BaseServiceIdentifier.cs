using System;
using Neolitic;
using Neolitic.Abstracts;

namespace Neolitic
{
	public abstract class BaseServiceIdentifier : IServiceIdentifier
	{

		public BaseServiceIdentifier ()
		{}


		public IServiceInfo IdentifyService (string command, out string arguments)
		{
			arguments = null;
			IServiceInfo serviceInfo = null;

			String[] tokens = command.Split (' ');
			if (tokens.Length < 2)//Every command will have at least two tokens
				throw new UnrecognizedServiceException (command); 

			String serviceName = tokens [0]; //TRANSFER or whatever service
			serviceInfo = GetServiceInfo(serviceName);
			if (serviceInfo == null)
				throw new UnrecognizedServiceException (command);

			//Arguments must be separated by space
			arguments = command.Substring (command.IndexOf (serviceName) + serviceName.Length+1);
			return serviceInfo;
		}

		protected abstract IServiceInfo GetServiceInfo(String serviceId);

	
	}
}

