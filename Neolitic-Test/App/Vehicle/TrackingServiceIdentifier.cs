using System;
using Neolitic;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	public class CarServiceIdentifier : IServiceIdentifier
	{
		//Available services: CUR_POS, LAST_POS, CUR_SPEED, DISABLE_ENGINE, ACTIVATE_ALARM

		public ServiceInfo IdentifyService (string command, out string arguments)
		{
			
		}
		
		
	}
}

