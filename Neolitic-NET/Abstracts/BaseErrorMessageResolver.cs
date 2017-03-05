using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic.Abstracts
{
    public abstract class BaseErrorMessageResolver : BaseContextualized, IErrorMessageResolver
    {
        public abstract string Resolve(string errorCode);
    }
}
