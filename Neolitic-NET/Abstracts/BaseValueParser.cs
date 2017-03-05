using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Abstracts
{
    public abstract class BaseValueParser : BaseContextualized, IValueParser
    {
        public abstract object Parse(string value);
    }
}
