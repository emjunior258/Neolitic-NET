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
		private IDictionary<String,IList<KeywordToken>> _serviceTokens = new Dictionary<String,IList<KeywordToken>> ();

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
						IList<KeywordToken> tokens = KeywordToken.Scan (service.CommandMapping);
						_serviceTokens.Add (service.Name, tokens);

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


		private void HandleCapturables(IList<KeywordToken> tokens, IExecutionContext context){

			foreach (KeywordToken token in tokens) {

				if (!token.Capturable)
					continue;

				Object value = token.GetValue (context.Keywords);
				FireCapturers (token.Name, value);

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
			MethodInfo serviceMethodInfo, Object invocationTarget){
			bool stopExecution = false;
			try
			{
				//Command execution start hook
				stopExecution = context.OnExecutionStart(); 
				context.ExecutionFailed = false;
			}
			catch (Exception ex)
			{
				HandleException(ex, context);
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
					context.FailureCause = ex;
					HandleException(ex, context);
				}
			}

			try
			{
				//Command execution end hook
				context.OnExecutionEnd();         

			}catch(Exception ex)
			{
				context.FailureCause = ex;
				HandleException(ex, context);
			}
		}

        public ExecutionResult ExecuteCommand(string command)
        {
            try
            {
                String arguments = null;
                ServiceInfo info = _serviceIdentifier.IdentifyService(command, out arguments);
                IExecutionContext context = _contextFactory.CreateContext(command, info, arguments);
                context.Service = info;
                context.Arguments = arguments;

                MethodInfo serviceMethodInfo = null;
				Object invocationTarget = null;
				serviceMethodInfo = GetServiceMethodInfo(info.Name, out invocationTarget);

				IList<KeywordToken> tokens = null;
				_serviceTokens.TryGetValue(info.Name, out tokens);

				//Initialize context for current Thread
				BaseContextualized.Initialize(context);

				foreach(KeywordToken token in tokens)
					context.Keywords.Set(token);

				//Put the argument values in the context
				context.Keywords.InitializeTokens(context);

				//Call capturers
				HandleCapturables(tokens,context);
			
				ExecuteService(context,serviceMethodInfo,invocationTarget);
				return GetResult(info,context);

            }
            catch (Exception ex)
            {
                throw new ContainerException("An Unexpected Exception was thrown during the command execution", ex,command);
			
			}finally{

				BaseContextualized.Clean();

			}
        }

		private ExecutionResult GetResult(ServiceInfo info, IExecutionContext context){

			String message = "";

			if(context.ExecutionFailed)
				message = _errMessageResolver.Resolve(context.ErrorCode);
			else
				message = info.SuccessMessage;


			message = context.Keywords.Apply(this,message);
			ExecutionResult result = new ExecutionResult(context,message);
			return result;

		}


        public void Start(IContextFactory contextFactory, IServiceIdentifier serviceIdentifier, IErrorMessageResolver errMessageResolver)
        {
            throw new NotImplementedException();
        }


        private void HandleException(Exception e, IExecutionContext context)
        {
            if (e is CommandExecutionFailException)
            {
                CommandExecutionFailException ex = (CommandExecutionFailException) e;

                //Fail method was explicitly invoked: use the error code set on exception object
                context.ErrorCode = ex.ErrorCode;
                context.ExecutionFailed = true;

            }else
            {

                //Implicit invocation of Fail method: use the default error code
                context.ErrorCode = CommandExecutionException.DEFAULT_ERROR_CODE;
                context.ExecutionFailed = true;

            }    

        }

    }
}
