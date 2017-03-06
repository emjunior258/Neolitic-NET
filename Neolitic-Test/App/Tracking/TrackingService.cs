using System;
using Neolitic;
using Neolitic.Abstracts;
using Neolitic.Attributes;

namespace NeoliticTest
{
	public class TrackingService : BaseContextualized
	{


		[Service("LAST_POS")]
		public void LastPosition(){



		}


		[Service("CUR_SPEED")]
		public void CurrentSpeed(){


		}

		[Service("DISABLE_ENGINE")]
		public void DisableEngine(){


		}

		[Service("ACTIVATE_ALARM")]
		public void ActivateAlarm(){

			

		}
	}
}

