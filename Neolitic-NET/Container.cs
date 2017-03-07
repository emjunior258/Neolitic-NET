using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Neolitic.Attributes;

namespace Neolitic
{
	public class Container : IContainer
    {
		private class MappedService{

			public Object Source {get;set;}
			public MethodInfo MethodInfo { get; set;}

		}


		private class CapturerInfo
		{
			public String Capturable { get; set;}
			public ICapturer Capturer { get; set;}
			public void DoCapturation(Object value){

				Capturer.Captured (value);
			
			}


			public void AddTo(IDictionary<String,IList<CapturerInfo>> target){

				IList<CapturerInfo> infos = null;
				if (!target.ContainsKey (Capturable))
					infos = new List<CapturerInfo> ();
				else
					target.TryGetValue (Capturable, out infos);

				infos.Add (this);
				target.Add (Capturable, infos);

			}

		}


		public Container(){

			this.AddParser (new BooleanParser ());
			this.AddParser (new DecimalParser ());
			this.AddParser (new DoubleParser ());
			this.AddParser (new Int16Parser ());
			this.AddParser (new Int32Parser ());
			this.AddParser (new Int64Parser ());

		}


		public IValueFormatter GetFormatter (string name)
		{
			IValueFormatter formatter = null;
			_formatters.TryGetValue (name, out formatter);

			return formatter;
				
		}

		public IValueParser GetParser (string name)
		{

			IValueParser parser = null;
			_parsers.TryGetValue (name, out parser);
			return parser;

		}

        private IContextFactory _contextFactory;
        private IServiceIdentifier _serviceIdentifier;
        private IErrorMessageResolver _errMessageResolver;
		private IDictionary<String, MappedService> _mappedServices = new Dictionary<String, MappedService>();
        private IDictionary<String,IValueFormatter> _formatters = new Dictionary<string, IValueFormatter>();
        private IDictionary<String,IValueParser> _parsers = new Dictionary<String,IValueParser>();
		private IDictionary<String,IList<CapturerInfo>> _capturers = new Dictionary<String, IList<CapturerInfo>>();

        public void MapServices(Object servicesHolder)
        {
			MethodInfo[] methods =  servicesHolder.GetType().GetMethods ();
			foreach (MethodInfo method in methods) {

				if (!method.IsPublic)
					continue;

						
				Object[] attributes = method.GetCustomAttributes(false);
				foreach (Object attribute in attributes) {

					if(attribute is Service){

						Service service = (Service)attribute;

						MappedService mapped = new MappedService();
						mapped.Source = servicesHolder;
						mapped.MethodInfo = method;
						_mappedServices.Add (service.Name, mapped);

						break;

					}

					//TODO: Thow exception:  duplicated service mapping

				}


			}
        }


		private String getName(Object target){

			Object[] attributes = target.GetType ().GetCustomAttributes (false);
			foreach (Object attribute in attributes) {
				
				if (attribute is Named)
					return ((Named)attribute).Name;
				
			}	

			//TODO: Throw exception: Named attribute not found
			return null;


		}


        public void AddFormatter(IValueFormatter formatter)
        {
			String name = getName (formatter);
            _formatters.Add(name,formatter);
        }

        public void AddParser(IValueParser parser)
        {
			String name = getName (parser);
            _parsers.Add(name,parser);
        }

        public void AddCapturer(ICapturer capturer)
        {

			object[] attributes = capturer.GetType ().GetCustomAttributes (false);
			foreach (Object attribute in attributes) {

				if (attribute is Captures) {

					Captures cap = (Captures)attribute;
					CapturerInfo capturableInfo = new CapturerInfo ();
					capturableInfo.Capturer = capturer;
					capturableInfo.Capturable = cap.Name;
					capturableInfo.AddTo (_capturers);
					break;

				}

			}


			//TODO: Throw exception: [Captures] attribute is missing
        }


		private bool HandleCapturables(IList<KeyValueToken> tokens, IExecutionContext context,out String exitMessage){

			exitMessage = null;

			try{

				foreach (KeyValueToken token in tokens) {

					if (!token.Capturable)
						continue;

					Object value = token.GetValue (context.KeyValues);
					FireCapturers (token.Name, value);

				}

				return true;

			}catch(Exception ex){

				HandleException(ex,context, out exitMessage);
				return false;
					
			}

		}


		private void FireCapturers(String capturable, Object value){

			if (!_capturers.ContainsKey (capturable))
				return;
			
			IList<CapturerInfo> capturers = null;
			_capturers.TryGetValue (capturable, out capturers);
			foreach (CapturerInfo capturer in capturers) 
				capturer.DoCapturation (value);

		}


		private MethodInfo GetServiceMethodInfo(String name, out Object target)
        {
			MappedService service = null;

			if (_mappedServices.ContainsKey(name))
				_mappedServices.TryGetValue(name, out service);

			if(service==null){
				//TODO: Throw NonMappedServiceException if method is null
				target = null;
				return null;
			}
           

			target = service.Source;
			return service.MethodInfo;
        }

		private void ExecuteService(IExecutionContext context, 
			MethodInfo serviceMethodInfo, Object invocationTarget, out String resultMessage){
			bool stopExecution = false;
			resultMessage = null;

			try
			{
				//Command execution start hook
				stopExecution = context.OnExecutionStart(); 
				context.ExecutionFailed = false;
			}
			catch (Exception ex)
			{
				HandleException(ex, context, out resultMessage);
			}

			//There is no wish to stop the command execution and the execution didn't fail earlier
			if ((!stopExecution)&&(!context.ExecutionFailed))
			{                  
				try
				{
					//Invoke the mapped service method
					serviceMethodInfo.Invoke(invocationTarget, new Object[] { });

				}catch(Exception ex)
				{
					HandleException(ex.InnerException, context, out resultMessage);
				}
			}

			try
			{
				//Command execution end hook
				context.OnExecutionEnd();         

			}catch(Exception ex)
			{
				HandleException(ex, context, out resultMessage);
			}
		}

		public ExecutionResult ExecuteCommand(string command){
			return ExecuteCommand (command, null);
		}

		public ExecutionResult ExecuteCommand(string command, Object contextCreationParam)
        {
            try
            {
                String arguments = null;
				IServiceInfo serviceInfo = _serviceIdentifier.IdentifyService(command, out arguments);
				IExecutionContext context = _contextFactory.CreateContext(serviceInfo, contextCreationParam);
                context.Service = serviceInfo;
                context.Arguments = arguments;
				context.Container = this;

                MethodInfo serviceMethodInfo = null;
				Object invocationTarget = null;
				serviceMethodInfo = GetServiceMethodInfo(serviceInfo.Id, out invocationTarget);

				//TODO: Throw MissingServiceMethodException

				IList<KeyValueToken> tokens = KeyValueToken.Scan (serviceInfo.ArgumentsMapping);

				//Initialize context for current Thread
				BaseContextualized.Initialize(context);

				foreach(KeyValueToken token in tokens){
					token.Container = this;
					context.KeyValues.Set(token);
				}

				//Put the argument values in the context
				context.KeyValues.InitializeTokens(context);

				//This variable will if the service method invokes IExecutionContext.Exit
				String exitMessage = null;

				//Call Capturers
				if(HandleCapturables(tokens,context,out exitMessage)){

					ExecuteService(context,serviceMethodInfo,invocationTarget, out exitMessage);

				}
			
				return GetResult(serviceInfo,context, exitMessage);

			}catch(ContainerException ex){

				throw ex;

			}catch (Exception ex)
            {
                throw new ContainerException("An Unexpected Exception was thrown during the command execution", ex,command);
			
			}finally{

				BaseContextualized.Clean();

			}
        }

		private ExecutionResult GetResult(IServiceInfo info, IExecutionContext context, String exitMessage){

			String message = "";

			if (exitMessage == null) {

				if (context.ExecutionFailed) {
					message = _errMessageResolver.Resolve (context.ErrorCode);
					context.KeyValues.Set ("error", context.ErrorCode);
				} else
					message = info.SuccessMessage;

			} else
				message = exitMessage;


			message = context.KeyValues.Apply(message);
			ExecutionResult result = new ExecutionResult(context,message);
			return result;

		}


        public void Start(IContextFactory contextFactory, IServiceIdentifier serviceIdentifier, IErrorMessageResolver errMessageResolver)
        {
			//TODO: Throw exception: Container already started
			this._contextFactory = contextFactory;
			this._serviceIdentifier = serviceIdentifier;
			this._errMessageResolver = errMessageResolver;
        }


		private void HandleException(Exception e, IExecutionContext context, out String statusMessage)
        {
			statusMessage = null;

			if (e is CommandExecutionFailException) {
				CommandExecutionFailException ex = (CommandExecutionFailException)e;

				//Fail method was explicitly invoked: use the error code set on exception object
				context.ErrorCode = ex.ErrorCode;
				context.ExecutionFailed = true;


			}else if(e is CommandExitException){

				CommandExitException exitException = (CommandExitException)e;
				context.ExitStatus = exitException.Status;
				AutoCommandExecutionFailException autoFail = null;

				try{

					statusMessage =  context.Service.GetStatusMessage (exitException.Status);

				}catch(Exception ex){

					autoFail = new FailedToFetchMappedMessageException (exitException.Status,ex);

				}

				//If no message is mapped, Exit will be interpreted as a Fail and the failure cause will be set to MessageNotMappedException
				if (statusMessage == null) {

					if(autoFail==null)
						autoFail = new MessageNotMappedException (exitException.Status);
					
					context.ExecutionFailed = true;
					context.FailureCause = autoFail;
					context.ErrorCode = autoFail.ErrorCode;

				}

				//Mappend message was found
				return;

            }else
            {

				if (e is TargetInvocationException)
					e = e.InnerException;

                //Implicit invocation of Fail method: use the default error code
                context.ErrorCode = CommandExecutionException.DEFAULT_ERROR_CODE;
                context.ExecutionFailed = true;

            }  

			context.FailureCause = e;


        }

    }
}
