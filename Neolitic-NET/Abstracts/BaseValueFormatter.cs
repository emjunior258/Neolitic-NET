using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Abstracts
{
    public abstract class BaseValueFormatter : BaseContextualized, IValueFormatter
    {
        public abstract string Format(Object value);
    }
}
