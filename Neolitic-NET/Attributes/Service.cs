using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Service : Named
    {


		public Service(string id) : base(id)
        {
			
		}

    }
}
