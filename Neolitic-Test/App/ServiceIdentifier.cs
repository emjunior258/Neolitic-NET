using System;
using Neolitic;

namespace NeoliticTest
{
	public class ServiceIdentifier : IServiceIdentifier
	{
		
		public ServiceInfo IdentifyService (string command, out string arguments)
		{
			if(command.StartsWith("BIM EDM ")){

				int edmIndex =	command.IndexOf ("EDM");
				arguments = command.Substring (edmIndex + 4);

				ServiceInfo info = new ServiceInfo ("EDM");
				info.SuccessMessage = "Your voucher code is {voucher}";
				return info;

			}else if(command.StartsWith("BIM EDMA ")){

				int edmIndex =	command.IndexOf ("EDMA");
				arguments = command.Substring (edmIndex + 5);

				ServiceInfo info = new ServiceInfo ("EDMA");
				info.SuccessMessage = "The Last recharge amount is {amount|money}";
				return info;

			}
				
			arguments = null;
			return null;
			
		}

	}
}

