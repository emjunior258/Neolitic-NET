using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class ContainerException : CommandExecutionException
    {

        private String _command = null;
        public String Command { get { return _command; } }

        public ContainerException(String message, Exception ex, String cmd)
            :base(message,ex)
        {
            this._command = cmd;
        }

		public ContainerException(String message, String cmd)
			:base(message)
		{
			this._command = cmd;
		}
    }
}
