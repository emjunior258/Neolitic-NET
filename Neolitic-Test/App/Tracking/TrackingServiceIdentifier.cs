using System;
using System.Collections;
using Neolitic;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	public class TrackingServiceIdentifier : IServiceIdentifier
	{
		//Every service receives atLeast two-tokens: authToken and vehicleId 

		private String[] services = {"CUR_POS","LAST_POS","CUR_SPEED","DISABLE_ENGINE","ACTIVATE_ALARM"};
		private IDictionary<String,ServiceInfo> serviceInfos = new Dictionary<String,ServiceInfo>();

		public TrackingServiceIdentifier(){

			//Current Position service
			ServiceInfo curPos = new ServiceInfo("CUR_POS");
			curPos.Description = "Current Vehicle position service";
			curPos.SuccessMessage = "Vehicle {vehicle} is located at {latitude}x{longitude}";

			//Last position service
			ServiceInfo lastPos = new ServiceInfo("LAST_POS");
			lastPos.Description = "Last Vehicle position service";
			lastPos.SuccessMessage = "Vehicle {vehicle} last position is {latitude}x{longitude}";

			//Current speed service
			ServiceInfo curSpeedPos = new ServiceInfo("CUR_SPEED");
			curSpeedPos.Description = "Current Vehicle speed service";
			curSpeedPos.SuccessMessage = "Vehicle {vehicle} current speed is {speed}";

			//Disable engine service
			ServiceInfo disableEngine = new ServiceInfo("DISABLE_ENGINE");
			disableEngine.Description = "Disable Vehicle's Engine service";
			disableEngine.SuccessMessage = "Engine was successfully disabled for vehicle {vehicle}";

			//Activate alarm service
			ServiceInfo activateAlarm = new ServiceInfo("ACTIVATE_ALARM");
			activateAlarm.Description = "Activates vehicle's alarm";
			activateAlarm.SuccessMessage = "Alarm successfully activated for vehicle {vehicle}";



		}

		public ServiceInfo IdentifyService (string command, out string arguments)
		{
			arguments = null;
			String[] pieces = command.Split (' ');
			if (pieces.Length < 3) 
				throw new UnrecognizedServiceException (command);

			

			String serviceId = pieces [0];
			int serviceIndex = Array.IndexOf (services, serviceId);
			if (serviceIndex == -1) 
				throw new UnrecognizedServiceException (command);


			String 


		}
		
		
	}
}

