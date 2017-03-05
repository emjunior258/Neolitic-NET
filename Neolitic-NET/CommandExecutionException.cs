using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class CommandExecutionException : Exception
    {

        public static String DEFAULT_ERROR_CODE = "9999";


        public CommandExecutionException(String message)
            :base(message)
        {
            
        }

        public CommandExecutionException(String message, Exception ex)
            :base(message,ex)
        {
           
        }
    }
}
