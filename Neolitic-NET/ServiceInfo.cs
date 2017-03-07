using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class ServiceInfo : IServiceInfo
    {

		private IDictionary<String,String> _statusMessages = new Dictionary<String,String> ();
		private String _id = null;

        public ServiceInfo(String serviceId)
        {
            this._id  = serviceId;
        }

        public String Id { get { return _id; } }

        public String SuccessMessage { get; set; }

        public String Description { get; set; }

		public void SetStatusMessage(String status, String message){

			_statusMessages.Add (status, message);

		}

		public String GetStatusMessage(String status){

			String statusMessage = null;
			_statusMessages.TryGetValue(status, out statusMessage);
			return statusMessage;

		}

		public String ArgumentsMapping { get; set; }

    }
}
