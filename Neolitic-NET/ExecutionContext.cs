using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
    public class ExecutionContext : IExecutionContext
    {
        private ICommandKeywords _keyword = new CommandKeywords();
        private bool _executionFailed = false;
        private String _errorCode = null;

        public bool ExecutionFailed
        {
            get
            {
                return _executionFailed;
            }
        }

        public ICommandKeywords Keywords
        {
            get {

                return _keyword;
            }
        }

        public MessageTransport ReplyVia { get; set; }

        public ServiceInfo Service { get; set; }

        public string ErrorCode
        {
            get  {

                return _errorCode;
            }
        }

        public String Arguments { get; set; }

        public void Fail(string errorCode)
        {
            throw new CommandExecutionFailException(String.Format("Failing execution of {0} service with error {1}",
                Service.Code, errorCode), errorCode);
        }


        //TODO: Think about optional tokens : consider the definition of a NULL token, so that verytime the null token if found the token presence is ignored.
        public void MapParams(string mapping)
        {           
            //TODO: Scan Keyword tokens and store them in the context
            
            throw new NotImplementedException();
        }

        public void Notify(string message, string[] recipients)
        {
            throw new NotImplementedException();
        }

        public void Notify(string message, string[] recipients, MessageTransport transport)
        {
            throw new NotImplementedException();
        }

        public void OnExecutionEnd()
        {
            //Must be overriden
        }

        public bool OnExecutionStart()
        {
            //Must be overriden
            return false;
        }

        public void SetKeywords(IDictionary<string, object> keywords)
        {
            throw new NotImplementedException();
        }

        public bool SupportsTransport(MessageTransport transport)
        {          
            return true;
        }
    }
}
