using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class ServiceInfo
    {

        private String _code = null;

        public ServiceInfo(String code)
        {
            this._code  = code;
        }

        public String Code { get { return _code; } }

        public String SuccessMessage { get; set; }

        public String Description { get; set; }
    }
}
