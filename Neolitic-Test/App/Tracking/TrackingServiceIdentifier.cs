using System;
using System.Collections;
using System.Collections.Generic;
using Neolitic;
using Neolitic.Abstracts;

namespace NeoliticTest
{
	public class TrackingServiceIdentifier : IServiceIdentifier
	{
		

		private IDictionary<String,ServiceInfo> _serviceInfos = new Dictionary<String,ServiceInfo>();

		public TrackingServiceIdentifier(){


			String commonArgsMapping = "{vehicle} {@authToken}";
		

			//Last position service
			ServiceInfo lastPosition = new ServiceInfo("LAST_POS");
			lastPosition.Description = "Last Vehicle position service";
			lastPosition.SuccessMessage = "Vehicle {vehicle} last position is {latitude}x{longitude}";
			lastPosition.ArgumentsMapping = commonArgsMapping;
			EnableService (lastPosition);

			//Current speed service
			ServiceInfo currentSpeed = new ServiceInfo("CUR_SPEED");
			currentSpeed.Description = "Current Vehicle speed service";
			currentSpeed.SuccessMessage = "Vehicle {vehicle} current speed is {speed}";
			currentSpeed.ArgumentsMapping = commonArgsMapping;
			EnableService (currentSpeed);

			//Disable engine service
			ServiceInfo disableEngine = new ServiceInfo("DISABLE_ENGINE");
			disableEngine.Description = "Disable Vehicle's Engine service";
			disableEngine.SuccessMessage = "Engine was successfully disabled for vehicle {vehicle}";
			disableEngine.AddStatusMessage (TrackingService.STATUS_ENGINE_ALREADY_DISABLED, "Engine of vehicle {vehicle} is already disabled");
			disableEngine.ArgumentsMapping = "{vehicle} {duration?} {@authToken}";
			EnableService(disableEngine);


			//Activate alarm service
			ServiceInfo activateAlarm = new ServiceInfo("ACTIVATE_ALARM");
			activateAlarm.Description = "Activates vehicle's alarm";
			activateAlarm.SuccessMessage = "Alarm successfully activated for vehicle {vehicle}";
			activateAlarm.AddStatusMessage (TrackingService.STATUS_ALARM_ALREADY_ACTIVE, "Alarm of vehicle {vehcile} is already active");
			disableEngine.ArgumentsMapping = "{vehicle} {noiseLevel?} {@authToken}";
			EnableService (activateAlarm);


		}

		private void EnableService(ServiceInfo info){

			_serviceInfos.Add(info.Id,info);

		}

		public IServiceInfo IdentifyService (string command, out string arguments)
		{
			arguments = null;
			String[] pieces = command.Split (' ');
			if (pieces.Length < 2) 
				throw new UnrecognizedServiceException (command);
					

			String serviceId = pieces [0];
			ServiceInfo serviceInfo = null;
			_serviceInfos.TryGetValue (serviceId, out serviceInfo);
			if(serviceInfo==null)
				throw new UnrecognizedServiceException (command);


			return serviceInfo;


		}
		
		
	}
}

