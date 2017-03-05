using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class ExecutionResult
    {
        private IExecutionContext _context;
        private String _message;

        public ExecutionResult(IExecutionContext context, String message)
        {
            this._context = context;
            this._message = message;
        }

        public IExecutionContext Context { get { return _context; } }

        public String Message { get { return _message; } }

    }
}
