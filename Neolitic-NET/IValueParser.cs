using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public interface IValueParser 
    {
        Object Parse(String value);
    }
}
