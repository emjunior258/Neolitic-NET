using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class ServiceInfo
    {

		private String _name = null;

        public ServiceInfo(String code)
        {
            this._name  = code;
        }

        public String Name { get { return _name; } }

        public String SuccessMessage { get; set; }

        public String Description { get; set; }
    }
}
