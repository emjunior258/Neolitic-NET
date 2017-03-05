using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Service : Named
    {

		private String _commandMapping = null;

		public Service(string name,String mapping) : base(name)
        {
			this._commandMapping = mapping;
		}

		public String CommandMapping {get { return _commandMapping; }}

    }
}
