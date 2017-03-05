using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class Named : Attribute
    {
        private String _name = null;

        public Named(String name)
        {
            this._name = name;
        }

        public String Name
        {
            get { return _name; }
        }
    }
}
