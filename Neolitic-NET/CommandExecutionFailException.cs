using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class CommandExecutionFailException : CommandExecutionException
    {
        private String _errorCode;
        private IDictionary<String, Object> _keywords = new Dictionary<String, Object>();

        public IDictionary<String, Object> Keywords { get { return _keywords; } }

        public String ErrorCode { get { return _errorCode; } }

        public CommandExecutionFailException(String message, String errorCode)
            :base(message){
            this._errorCode = errorCode;
        }

        public CommandExecutionFailException(String message, String errorCode, Exception ex)
            :base(message,ex){
            this._errorCode = errorCode;
        }
    }
}
