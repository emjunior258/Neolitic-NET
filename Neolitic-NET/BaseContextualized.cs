using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Neolitic
{
    public abstract class BaseContextualized
    {
        private static ThreadLocal<IExecutionContext> _contextLocal = new ThreadLocal<IExecutionContext>();

        public IExecutionContext Context {
            protected set { _contextLocal.Value = value;  }
            get { return _contextLocal.Value; }
        }
        
    }
}
