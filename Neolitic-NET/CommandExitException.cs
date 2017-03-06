
using System;

namespace Neolitic
{
	public class CommandExitException : Exception
	{
		private String _status = null;

		public CommandExitException (String status):base(String.Format("Exiting command execution with status {0}",status))
		{
			this._status = status;
		}

		public String Status { get { return _status; }}

	}


}

