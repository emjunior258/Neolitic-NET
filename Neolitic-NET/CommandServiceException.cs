using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    /// <summary>
    /// Is correspondent to invoking the <see cref="IExecutionContext.Fail(string)"/> method.
    /// </summary>
    public class CommandServiceException : CommandExecutionFailException
    {
        public CommandServiceException(String message, String errorCode)
            :base(message, errorCode){
        }

        public CommandServiceException(String message, String errorCode, Exception ex)
            :base(message, errorCode, ex){
        }

    }
}
