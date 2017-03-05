using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Attributes
{
    public class Interprets : Attribute
    {
        private String[] _values = null;

        public String[] Values
        {
            get
            {
                return _values;
            }
        }

        public Interprets(String[] valueNames):base()
        {
            this._values = valueNames;
        }
    }
}
