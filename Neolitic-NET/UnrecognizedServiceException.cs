using System;
using Neolitic;

namespace Neolitic
{
	public class UnrecognizedServiceException : ContainerException
	{
		private String _tip;

		public String Tip{ get { return _tip; }}

		public UnrecognizedServiceException (String command):base(String.Format("Service identification failed for the submited command : {0}",command),command)
		{}

		public UnrecognizedServiceException (String command, String tip):base(String.Format("Service identification failed for the submited command : {0}",command),command)
		{
			this._tip = tip;
		}
	}
}

