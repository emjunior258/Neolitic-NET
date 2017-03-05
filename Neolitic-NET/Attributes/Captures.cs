using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Attributes
{
    public class Captures : Attribute
    {
        private String[] _values = null;

        public String[] Values
        {
            get
            {
                return _values;
            }
        }

        public Captures(String[] valueNames):base()
        {
            this._values = valueNames;
        }
    }
}
