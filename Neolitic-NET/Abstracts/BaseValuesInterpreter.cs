using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Abstracts
{
    public abstract class BaseValuesInterpreter : BaseContextualized, IValuesInterpreter
    {
        public abstract void InterpretValues(IDictionary<string, object> values);
    }
}
