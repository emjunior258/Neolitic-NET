using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace NeoliticTest
{
	public class TrackingService : BaseContextualized
	{

		public const String STATUS_ENGINE_ALREADY_DISABLED = "STATUS_ENGINE_ALREADY_DISABLED";
		public const String STATUS_ALARM_ALREADY_ACTIVE = "STATUS_ALARM_ALREADY_ACTIVE";



		[Service("LAST_POS")]
		public void LastPosition(){




		}


		[Service("CUR_SPEED")]
		public void CurrentSpeed(){


		}

		[Service("DISABLE_ENGINE")]
		public void DisableEngine(){

			//Optional duration
			String duration = Command.GetOptional<String>("duration");
			String vehicle = Command.Get<String> ("vehicle");

			if (vehicle == "MIA-46-25")
				Command.Exit (STATUS_ENGINE_ALREADY_DISABLED);



			//TODO: Disable engine here

		}

		[Service("ACTIVATE_ALARM")]
		public void ActivateAlarm(){


			//Optional noiseLevel
			

		}
	}
}

