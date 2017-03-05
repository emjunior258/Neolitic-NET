using System;
using Neolitic;

namespace NeoliticTest
{
	public class ServiceIdentifier : IServiceIdentifier
	{
		
		public ServiceInfo IdentifyService (string command, out string arguments)
		{
			if(!command.StartsWith("BIM EDM")){

				arguments = null;
				return null;

			}
				
			int edmIndex =	command.IndexOf ("EDM");
			arguments = command.Substring (edmIndex + 4);

			ServiceInfo info = new ServiceInfo ("EDM");
			info.SuccessMessage = "Your voucher code is {voucher}";
			return info;
			
		}

	}
}

