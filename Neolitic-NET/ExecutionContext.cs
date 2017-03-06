using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neolitic
{
	public class ExecutionContext : IExecutionContext
    {


		public ExecutionContext(){

			this._keywords = new CommandKeywords ();

		}

        private ICommandKeywords _keywords = new CommandKeywords();

		public bool ExecutionFailed { get; set; }

        public ICommandKeywords Keywords
        {
            get {

                return _keywords;
            }
        }

        public ServiceInfo Service { get; set; }

		public string ErrorCode { get; set; }

        public String Arguments { get; set; }

        public void Fail(string errorCode)
        {
            throw new CommandExecutionFailException(String.Format("Failing execution of {0} service with error {1}",
                Service.Id, errorCode), errorCode);
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
			
		public Exception FailureCause { get; set;}

		public IContainer Container { get; set; }

		public string NullToken { get; set;}

		public Object Get(String name){

			return Keywords.Get (name);

		}

		public T Get<T> (string name)
		{
			return (T) Get(name);
		}
		public object GetOptional (string name)
		{
			if (!Keywords.Contains (name))
				return null;

			return Get (name);


		}
		public T GetOptional<T> (string name)
		{
			if (!Keywords.Contains (name))
				return default(T);

			return (T)	Get (name);

		}

		public void Set (String name, Object val){

			Keywords.Set (name, val);

		}


		public void Exit (string status)
		{
			throw new CommandExitException (status);
		}

		public string ExitStatus { get; set; }
    }
}
