using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Service : Named
    {
        public Service(string name) : base(name)
        {}
    }
}
