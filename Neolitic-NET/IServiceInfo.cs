using System;

namespace Neolitic
{
	public interface IServiceInfo
	{

		String Id { get; }

		String SuccessMessage { get; }

		String Description { get; }

		String GetStatusMessage(String status);
		
	}
}

